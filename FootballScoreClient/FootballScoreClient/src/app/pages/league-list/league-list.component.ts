import { Component, inject, OnInit } from '@angular/core';
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
export class LeagueListComponent implements OnInit{
  private leagueService = inject(ApiService)
  leagues$!: Observable<LeagueResponse>;

  ngOnInit(): void {
    this.leagues$ = this.leagueService.getAvailableLeagues();
  }
}
