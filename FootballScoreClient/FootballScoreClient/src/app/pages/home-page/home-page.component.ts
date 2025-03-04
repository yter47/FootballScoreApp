import { Component, inject } from '@angular/core';
import { MatchResponse } from '../../core/match';
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

  private matchService = inject(ApiService);
  response$: Observable<MatchResponse>
  
  constructor() {
    this.response$ = this.matchService.getRecentMatches();
  }
}
