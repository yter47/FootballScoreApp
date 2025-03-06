import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Match, MatchResponse } from '../core/match';
import { LeagueResponse } from '../core/league';
import { StandingsResponse } from '../core/standings';

@Injectable({
  providedIn: 'root'
})

export class ApiService {

  httpClient = inject(HttpClient)

  getAvailableLeagues(): Observable<LeagueResponse> {
    return this.httpClient.get<LeagueResponse>("https://localhost:7048/League/GetAvailableCompetitions");
  }

  getMatchesByLeagueId(id: number): Observable<MatchResponse> {
    return this.httpClient.get<MatchResponse>(`https://localhost:7048/League/getMatchesByCompetitionId?id=${id}`)
  }

  getStandingsByLeagueId(id: number): Observable<StandingsResponse> {
       return this.httpClient.get<StandingsResponse>(`https://localhost:7048/League/getStandingsByCompetitionId?id=${id}`)
  }
}
