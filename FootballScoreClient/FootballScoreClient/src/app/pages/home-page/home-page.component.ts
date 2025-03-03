import { Component, inject } from '@angular/core';
import { Match, MatchResponse } from '../../core/match';
import { MatchService } from '../../services/match.service';
import { CommonModule } from '@angular/common';
import { map, Observable } from 'rxjs';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-home-page',
  imports: [CommonModule],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

  matchService = inject(ApiService);
  response$: Observable<MatchResponse>
  
  constructor() {
    this.response$ = this.matchService.getRecentMatches();
  }
}
