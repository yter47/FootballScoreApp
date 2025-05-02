import { inject, Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { catchError, map, Observable, of } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  private authService = inject(AuthService);
  private router = inject(Router);

  canActivate(): Observable<boolean | UrlTree> {
    const currentUser = this.authService.currentUserSignal();

    if (currentUser) {
      return of(true);
    }

    const token = localStorage.getItem('token');
    const refreshToken = localStorage.getItem('refreshToken');

    if (!token || !refreshToken) {
      return of(this.router.parseUrl('/login'));
    }

    return this.authService.authorize().pipe(
      map((user) => {
        if (user) {
          this.authService.setTokens(user);
          return true;
        }
        return this.router.parseUrl('/home');
      }),
      catchError(() => {
        return this.authService.refreshTokenRequest().pipe(
          map((tokens) => {
            if (tokens?.accessToken) {
              this.authService.setTokens(tokens);
              return true;
            }
            return this.router.parseUrl('/login');
          }),
          catchError(() => of(this.router.parseUrl('/login')))
        );
      })
    );
  }
}
