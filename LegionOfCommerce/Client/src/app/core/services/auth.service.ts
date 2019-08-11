import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginInfo } from '../models/login-info';
import { serverError } from '../helpers/error-handler';
import { environment } from 'environments/environment';
import { take, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RegistrationInfo } from '../models/registration-info';
import * as jwt_decode from 'jwt-decode';
import { getLocaleDateTimeFormat } from '@angular/common';
import { AuthRequest } from '../models/auth-request';
import { AuthResult } from '../models/auth-result';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
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
  }

  logout() {
    localStorage.removeItem(this.TOKEN_NAME);
    localStorage.removeItem(this.REFRESH_TOKEN_NAME);
    this.isAuth = false;
  }

  isAuthenticated() {
    const token = this.getToken();
    console.log('hi');
    this.isTokenExpired(); // For testing purposes
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
        this.refreshToken();
      }
    }
  }

  refreshToken(): Promise<any> {
    const authRequest: AuthRequest = {
      token: localStorage.getItem(this.TOKEN_NAME) || '',
      refreshToken: localStorage.getItem(this.REFRESH_TOKEN_NAME) || ''
    };
    return new Promise((resolve, reject) => {
      this.http
        .post<AuthResult>(
          this.authUrl + '/refresh-auth',
          authRequest,
          this.httpOptions
        )
        .pipe(take(1))
        .subscribe(
          authResult => {
            this.storeTokens(authResult);
            resolve(authResult);
          },
          err => {
            this.logout(); // logout if can't refresh token
            reject(err);
          }
        );
    });
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
