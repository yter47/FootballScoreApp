import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { IAuthUserTokens } from '../core/user.interface';
import { RegisterUser } from '../core/registerUser';
import { LoginUser } from '../core/loginUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  httpClient = inject(HttpClient)

  currentUserSignal = signal<IAuthUserTokens | undefined | null>(undefined);

  registerUser(user: RegisterUser): Observable<IAuthUserTokens> {
    return this.httpClient.post<IAuthUserTokens>("https://localhost:7048/Auth/RegisterUser", user);
  }

  loginUser(user: LoginUser): Observable<IAuthUserTokens> {
    return this.httpClient.post<IAuthUserTokens>("https://localhost:7048/Auth/LoginUser", user);
  }

  authorize():Observable<IAuthUserTokens> {
    return this.httpClient.get<IAuthUserTokens>("https://localhost:7048/User/Authorize");
  }
}
