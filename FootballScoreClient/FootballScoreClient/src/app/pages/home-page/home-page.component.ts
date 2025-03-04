import { Component, inject } from '@angular/core';
import { MatchResponse } from '../../core/match';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ApiService } from '../../services/api.service';
import { MatchListComponent } from '../../shared/match-list/match-list.component';

@Component({
  selector: 'app-home-page',
  imports: [CommonModule, MatchListComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

  private matchService = inject(ApiService);
  response$: Observable<MatchResponse>
  
  constructor() {
    this.response$ = this.matchService.getRecentMatches();
  }
}
