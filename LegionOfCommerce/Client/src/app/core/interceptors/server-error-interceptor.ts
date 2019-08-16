import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, switchMap, take, filter } from 'rxjs/operators';
import { serverError } from '../helpers/error-handler';
import { AuthService } from '../services/auth.service';
import { AuthResult } from '../models/auth-result';

@Injectable()
export class ServerErrorInterceptor implements HttpInterceptor {
  private refreshTokenInProgress = false;
  private refreshTokenSubject = new BehaviorSubject(null);

  constructor(public authService: AuthService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: any) => {
        if (
          request.url.includes('refresh-auth') ||
          request.url.includes('login')
        ) {
          if (request.url.includes('refreshtoken')) {
            this.authService.logout();
          }
          return throwError(error);
        }
        if (error.status !== 401) {
          return throwError(error);
        }
        if (!this.authService.isTokenExpired()) {
          return throwError(error);
        }

        if (this.refreshTokenInProgress) {
          return this.refreshTokenSubject.pipe(
            filter(result => result !== null),
            take(1),
            switchMap(() => next.handle(this.addAuthenticationToken(request)))
          );
        } else {
          this.refreshTokenInProgress = true;
          this.refreshTokenSubject.next(null);

          return this.authService.refreshToken().pipe(
            switchMap((authResult: AuthResult) => {
              this.authService.storeTokens(authResult);
              this.refreshTokenInProgress = false;
              this.refreshTokenSubject.next(authResult.refreshToken);
              return next.handle(this.addAuthenticationToken(request));
            }),
            catchError((err: any) => {
              this.refreshTokenInProgress = false;
              this.authService.logout();
              console.log('Failed to refresh token', err);
              return throwError(error);
            })
          );
        }
      })
    );
  }

  addAuthenticationToken(request: HttpRequest<any>): HttpRequest<any> {
    const token = this.authService.getToken();

    if (!token) {
      return request;
    }

    const newReq = request.clone({
      headers: this.authService.getHeaders()
    });
    return newReq;
  }
}
