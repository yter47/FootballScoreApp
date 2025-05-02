import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import {
  HttpRequest,
  HttpHandlerFn,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, Subject, catchError, switchMap, take, tap, throwError } from 'rxjs';
import { AuthService } from '../../services/auth.service';

let isRefreshing = false;
const refreshSubject = new Subject<string>();

export const authInterceptor: HttpInterceptorFn = (
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> => {

  const authService = inject(AuthService);
  const token = authService.token;

  const authReq = token
    ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
    : req;

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 && !isRefreshing) {
        isRefreshing = true;

        return authService.refreshTokenRequest().pipe(
          tap((tokens) => {
            authService.setTokens(tokens);
            refreshSubject.next(tokens.accessToken);
            isRefreshing = false;
          }),
          switchMap((tokens) => {
            const cloned = req.clone({
              setHeaders: {
                Authorization: `Bearer ${tokens.accessToken}`,
              },
            });
            return next(cloned);
          }),
          catchError((err) => {
            isRefreshing = false;
            authService.logout();
            return throwError(() => err);
          })
        );
      }

      if (error.status === 401 && isRefreshing) {
        return refreshSubject.pipe(
          take(1),
          switchMap((newToken) => {
            const cloned = req.clone({
              setHeaders: {
                Authorization: `Bearer ${newToken}`,
              },
            });
            return next(cloned);
          })
        );
      }

      return throwError(() => error);
    })
  );
};
