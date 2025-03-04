import { Component, inject } from '@angular/core';
import { filter, map, Observable } from 'rxjs';
import { ApiService } from '../../services/api.service';
import { ActivatedRoute } from '@angular/router';
import { Match, MatchResponse } from '../../core/match';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-league-details',
  imports: [CommonModule],
  templateUrl: './league-details.component.html',
  styleUrl: './league-details.component.scss'
})
export class LeagueDetailsComponent {
  
  private leagueService = inject(ApiService)

  matches$: Observable<MatchResponse>
  todaysMatches$: Observable<Match[]>
  upcomingMatches$: Observable<Match[]>
  playedMatches$: Observable<Match[]>
  
  currentDate = new Date();
  selectedCategory: string = 'today';
  categories = [
    {label: 'Todays Matches', value: 'today'},
    {label: 'Upcoming Matches', value: 'upcoming'},
    {label: 'Played Games', value: 'played'},
  ]

  constructor(private route: ActivatedRoute) {
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
