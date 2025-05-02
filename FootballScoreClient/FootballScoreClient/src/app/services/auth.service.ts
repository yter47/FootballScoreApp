import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { IAuthUserTokens } from '../core/user.interface';
import { RegisterUser } from '../core/registerUser';
import { LoginUser } from '../core/loginUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  httpClient = inject(HttpClient)
  private currentUserSubject = signal<IAuthUserTokens | undefined | null>(undefined);

  currentUserSignal = this.currentUserSubject.asReadonly();

  setTokens(tokens: IAuthUserTokens) {
    localStorage.setItem('token', tokens.accessToken);
    localStorage.setItem('refreshToken', tokens.refreshToken);
    this.currentUserSubject.set(tokens);
  }

  get token() {
    return localStorage.getItem('token');
  }

  get refreshToken() {
    return localStorage.getItem('refreshToken');
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    this.currentUserSubject.set(null);
  }

  refreshTokenRequest(): Observable<IAuthUserTokens> {
    return this.httpClient.post<IAuthUserTokens>('/auth/refreshToken', {
      accessToken: this.token,
      refreshToken: this.refreshToken
    });
  }

  registerUser(user: RegisterUser): Observable<IAuthUserTokens> {
    return this.httpClient.post<IAuthUserTokens>("https://localhost:7048/Auth/RegisterUser", user);
  }

  loginUser(user: LoginUser): Observable<IAuthUserTokens> {
    return this.httpClient.post<IAuthUserTokens>("https://localhost:7048/Auth/LoginUser", user).pipe(
      tap((x) => console.log(x))
    );
  }

  authorize():Observable<IAuthUserTokens> {
    return this.httpClient.get<IAuthUserTokens>("https://localhost:7048/Auth/Authorize");
  }
}
