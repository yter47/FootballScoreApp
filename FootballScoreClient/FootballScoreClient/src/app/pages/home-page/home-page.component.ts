import { Component, inject } from '@angular/core';
import { MatchResponse } from '../../core/match';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { MatchListComponent } from '../../shared/match-list/match-list.component';
import { MatchService } from '../../services/match.service';

@Component({
  selector: 'app-home-page',
  imports: [CommonModule, MatchListComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

  private matchService = inject(MatchService);
  response$: Observable<MatchResponse>

  constructor() {
    this.response$ = this.matchService.getRecentMatches();
  }
}
