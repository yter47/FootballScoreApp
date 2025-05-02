import { inject, Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { catchError, map, Observable, of } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  authService = inject(AuthService);
  router = inject(Router);

  canActivate(): Observable<boolean | UrlTree> {
    const currentUser = this.authService.currentUserSignal();

    if (currentUser) {
      return of(true);
    }

    const token = localStorage.getItem('token');
    if (!token) {
      return of(this.router.parseUrl('/login'));
    }

    return this.authService.authorize().pipe(
      map((user) => {
        if (user) {
          this.authService.currentUserSignal.set(user);
          return true;
        }
        return this.router.parseUrl('/login');
      }),
      catchError(() => of(this.router.parseUrl('/login')))
    );
  }
}
