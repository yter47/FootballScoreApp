import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  httpClient = inject(HttpClient);

  followTeam(teamId: number): Observable<number> {
    return this.httpClient.post<number>(
      'https://localhost:7048/User/FollowTeam}',
      teamId
    );
  }

  isFollowing(teamId: number): Observable<boolean> {
    return this.httpClient.get<boolean>(`https://localhost:7048/User/IsFollowing?teamId=${teamId}`); 
  }
}
