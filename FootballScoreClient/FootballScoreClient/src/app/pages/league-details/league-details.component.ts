import { Component, inject, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiService } from '../../services/api.service';
import { ActivatedRoute } from '@angular/router';
import { Match, MatchResponse } from '../../core/match';
import { CommonModule } from '@angular/common';
import { MatchListComponent } from '../../shared/match-list/match-list.component';
import { StandingsResponse } from '../../core/standings';
import { TableComponent } from '../../shared/table/table.component';

@Component({
  selector: 'app-league-details',
  imports: [CommonModule, MatchListComponent, TableComponent],
  templateUrl: './league-details.component.html',
  styleUrl: './league-details.component.scss'
})
export class LeagueDetailsComponent implements OnInit{
  
  private leagueService = inject(ApiService)
  private route = inject(ActivatedRoute)

  matches$!: Observable<MatchResponse>
  todaysMatches$!: Observable<Match[]>
  upcomingMatches$!: Observable<Match[]>
  playedMatches$!: Observable<Match[]>

  standings$!: Observable<StandingsResponse>
  
  currentDate = new Date();
  selectedCategory: string = 'today';
  categories = [
    {label: 'Todays Matches', value: 'today'},
    {label: 'Upcoming Matches', value: 'upcoming'},
    {label: 'Played Games', value: 'played'},
    {label: 'Standings', value: 'standings'},
  ]

  ngOnInit(): void {
    let leagueId = Number(this.route.snapshot.paramMap.get('id'));
    this.matches$ = this.leagueService.getMatchesByLeagueId(leagueId);
    this.todaysMatches$ = this.matches$.pipe(
      map((matches) => 
        matches.matches.filter((match) => {
          const matchDate = new Date(match.utcDate);
          const currentDate = new Date(this.currentDate);
    
          return matchDate.getUTCFullYear() === currentDate.getUTCFullYear() &&
                 matchDate.getUTCMonth() === currentDate.getUTCMonth() &&
                 matchDate.getUTCDate() === currentDate.getUTCDate();
        })
      )
    );
    this.upcomingMatches$ = this.matches$.pipe(map((matches) => matches.matches.filter((match => match.status !== 'FINISHED'))));
    this.playedMatches$ = this.matches$.pipe(map((matches) => matches.matches.filter((match => match.status === 'FINISHED')))).pipe(map(matches => matches.reverse()));
    this.standings$ = this.leagueService.getStandingsByLeagueId(leagueId);
  }

  getMatchesByMatchDay(matchday: number): Observable<Match[]> {
    return this.matches$.pipe(map((matches) => matches.matches.filter((match) => match.matchday == matchday)));
  }

  getMatches() {
    return this.matches$;
  }

  showCategory(category: string) {
    this.selectedCategory = category;
  }
}
