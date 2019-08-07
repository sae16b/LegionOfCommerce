import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginInfo } from '../models/login-info';
import { serverError } from '../helpers/error-handler';
import { environment } from 'environments/environment';
import { take, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  private authUrl = environment.apiUrl + '/api/user';
  constructor(private http: HttpClient) {}

  login(loginInfo: LoginInfo): Observable<LoginInfo> {
    return this.http
      .post<LoginInfo>(this.authUrl + '/login', loginInfo, this.httpOptions)
      .pipe(take(1));
  }
}
