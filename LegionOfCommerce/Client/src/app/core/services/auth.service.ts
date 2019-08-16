import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEvent } from '@angular/common/http';
import { LoginInfo } from '../models/login-info';
import { serverError } from '../helpers/error-handler';
import { environment } from 'environments/environment';
import { take, catchError, switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RegistrationInfo } from '../models/registration-info';
import * as jwt_decode from 'jwt-decode';
import { getLocaleDateTimeFormat } from '@angular/common';
import { AuthRequest } from '../models/auth-request';
import { AuthResult } from '../models/auth-result';
import { headersToString } from 'selenium-webdriver/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  private TOKEN_NAME = 'token';
  private REFRESH_TOKEN_NAME = 'refresh_token';
  private authUrl = environment.apiUrl + '/api/user';

  isAuth = false;

  static isPasswordStrong(password: string) {
    if (password.length >= 6) {
      return true;
    }
  }

  constructor(private http: HttpClient) {
    this.isAuthenticated();
    this.setHeaders();
  }

  getHeaders() {
    return this.httpOptions.headers;
  }
  setHeaders() {
    this.httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        authorization: 'Bearer ' + this.getToken() || ''
      })
    };
  }

  logout() {
    localStorage.removeItem(this.TOKEN_NAME);
    localStorage.removeItem(this.REFRESH_TOKEN_NAME);
    // Probably gonna add in some logic on backend to
    // invalidate refresh token at a later date
    this.isAuth = false;
  }

  isAuthenticated() {
    this.isTokenExpired();
    const token = this.getToken();
    this.isAuth = token != null;
    return this.isAuth;
  }

  isTokenExpired() {
    const token = this.getToken();
    if (token) {
      const decodedToken: DecodedToken = jwt_decode(token);
      console.log('Time left on token:', decodedToken.exp - Date.now() / 1000);
      if (decodedToken.exp <= Date.now() / 1000) {
        console.log('expired');
        return true;
      }
      return false;
    }
  }

  getUserInfo() {
    return this.http
      .get<any>(this.authUrl + '/info', this.httpOptions)
      .pipe(take(1));
  }

  refreshToken(): Observable<any> {
    const authRequest: AuthRequest = {
      token: localStorage.getItem(this.TOKEN_NAME) || '',
      refreshToken: localStorage.getItem(this.REFRESH_TOKEN_NAME) || ''
    };
    return this.http
      .post<AuthResult>(
        this.authUrl + '/refresh-auth',
        authRequest,
        this.httpOptions
      )
      .pipe(take(1));
  }

  getToken() {
    const token = localStorage.getItem(this.TOKEN_NAME);
    return token;
  }
  getRefreshToken() {
    const refreshToken = localStorage.getItem(this.REFRESH_TOKEN_NAME);
    return refreshToken;
  }

  storeTokens(authResult: AuthResult) {
    localStorage.setItem(this.TOKEN_NAME, authResult.token);
    localStorage.setItem(this.REFRESH_TOKEN_NAME, authResult.refreshToken);
    this.setHeaders();

    this.isAuthenticated();
  }

  login(loginInfo: LoginInfo): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .post<AuthResult>(this.authUrl + '/login', loginInfo, this.httpOptions)
        .pipe(take(1))
        .subscribe(
          authResult => {
            this.storeTokens(authResult);
            // visual studio appears to believe this is an HttpEvent,
            // not sure why but it is really just an AuthResult
            // (as seen in a console.log)
            resolve();
          },
          err => {
            reject(err);
          }
        );
    });
  }

  register(registrationInfo: RegistrationInfo): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .post<RegistrationInfo>(
          this.authUrl + '/register',
          registrationInfo,
          this.httpOptions
        )
        .pipe(take(1))
        .subscribe(
          res => {
            const loginInfo: LoginInfo = {
              emailOrUserName: registrationInfo.email,
              password: registrationInfo.password
            };
            this.login(loginInfo)
              .then(result => {
                resolve(result);
              })
              .catch(error => {
                reject(error);
              });
          },
          err => {
            reject(err);
          }
        );
    });
  }
}
