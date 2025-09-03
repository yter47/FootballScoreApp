import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Match, MatchResponse, Team } from '../core/match';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MatchService {
  httpClient = inject(HttpClient);

  getRecentMatches(): Observable<MatchResponse> {
    return this.httpClient.get<MatchResponse>(
      'https://localhost:7048/Match/GetRecentMatches'
    );
  }

  getMatchById(id: number): Observable<Match> {
    return this.httpClient.get<Match>(
      `https://localhost:7048/Match/getMatchById?id=${id}`
    );
  }

  getTeamById(id: number): Observable<Team> {
    return this.httpClient.get<Team>(
      `https://localhost:7048/Team/getTeamById?id=${id}`
    );
  }
}
