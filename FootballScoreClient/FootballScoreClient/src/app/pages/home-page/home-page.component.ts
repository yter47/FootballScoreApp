import { Component, inject } from '@angular/core';
import { Match } from '../../core/match';
import { MatchService } from '../../services/match.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-home-page',
  imports: [CommonModule, HttpClientModule],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

  matchService = inject(MatchService);
  matches: Match[] = []

  getMatches() {
    this.matchService.getRecentMatches().then(match => {
      this.matches = match;
    });
  }
}
