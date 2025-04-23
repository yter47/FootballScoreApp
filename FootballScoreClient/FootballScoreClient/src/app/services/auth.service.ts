import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from '../core/user.interface';
import { RegisterUser } from '../core/registerUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  httpClient = inject(HttpClient)

  registerUser(user: RegisterUser): Observable<IUser> {
    return this.httpClient.post<IUser>("https://localhost:7048/Auth/RegisterUser", user);
  }
}
