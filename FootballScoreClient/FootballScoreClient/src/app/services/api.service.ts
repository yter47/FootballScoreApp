import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Match } from '../core/match';

@Injectable({
  providedIn: 'root'
})

export class ApiService {

  httpClient = inject(HttpClient)

  getRecentMatches(): Observable<Match[]> {
    return this.httpClient.get<Match[]>("https://api.football-data.org/v4/matches")
  }
}
