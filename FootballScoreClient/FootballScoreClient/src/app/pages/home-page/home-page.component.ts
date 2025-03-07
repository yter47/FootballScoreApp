import { Component, inject, OnInit } from '@angular/core';
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
export class HomePageComponent implements OnInit {

  private matchService = inject(MatchService);
  response$!: Observable<MatchResponse>

  ngOnInit(): void {
    this.response$ = this.matchService.getRecentMatches();
  }
}
