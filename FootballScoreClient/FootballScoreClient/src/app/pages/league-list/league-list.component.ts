import { Component, inject } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Observable } from 'rxjs';
import { LeagueResponse } from '../../core/league';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-league-list',
  imports: [CommonModule, RouterModule],
  templateUrl: './league-list.component.html',
  styleUrl: './league-list.component.scss'
})
export class LeagueListComponent {
  leagueService = inject(ApiService)
  leagues$: Observable<LeagueResponse>;

  constructor() {
    this.leagues$ = this.leagueService.getAvailableLeagues();
  }
}
