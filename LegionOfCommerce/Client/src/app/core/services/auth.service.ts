import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginInfo } from '../models/login-info';
import { serverError } from '../helpers/error-handler';
import { environment } from 'environments/environment';
import { take, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RegistrationInfo } from '../models/registration-info';
import * as jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  private authUrl = environment.apiUrl + '/api/user';

  static isPasswordStrong(password: string) {
    if (password.length >= 6) {
      return true;
    }
  }

  constructor(private http: HttpClient) {}

  isAuthenticated() {
    return !this.isTokenExpired();
  }

  isTokenExpired() {
    const token = this.getToken();
    if (token) {
      const decodedToken: DecodedToken = jwt_decode(token);
      const currentTime = Date.now() / 1000;
      console.log(decodedToken.exp - currentTime);
      return decodedToken.exp < currentTime;
    }
    return true;
  }

  getToken() {
    const token = localStorage.getItem('token');
    return token;
  }

  storeToken(token: string) {
    const decodedToken: DecodedToken = jwt_decode(token);
    console.log(decodedToken, Date.now() - decodedToken.exp);
    localStorage.setItem('token', token);
  }

  login(loginInfo: LoginInfo): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .post<any>(this.authUrl + '/login', loginInfo, this.httpOptions)
        .pipe(take(1))
        .subscribe(
          res => {
            this.storeToken(res.token);
            console.log('is auth', this.isAuthenticated());
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
