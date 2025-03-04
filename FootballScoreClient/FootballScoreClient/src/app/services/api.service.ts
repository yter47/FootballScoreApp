import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Match, MatchResponse } from '../core/match';
import { League, LeagueResponse } from '../core/league';

@Injectable({
  providedIn: 'root'
})

export class ApiService {

  httpClient = inject(HttpClient)

  getRecentMatches(): Observable<MatchResponse> {
    return this.httpClient.get<MatchResponse>("https://localhost:7048/League/GetRecentMatches");
  }
  
  getAvailableLeagues(): Observable<LeagueResponse> {
    return this.httpClient.get<LeagueResponse>("https://localhost:7048/League/GetAvailableCompetitions");
  }

  getMatchesByLeagueId(id: number): Observable<MatchResponse> {
    return this.httpClient.get<MatchResponse>(`https://localhost:7048/League/getMatchesByCompetitionId?id=${id}`)
  }
}
