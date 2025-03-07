import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { Match } from '../../core/match';
import { MatchService } from '../../services/match.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-match-page',
  imports: [CommonModule, RouterModule],
  templateUrl: './match-page.component.html',
  styleUrl: './match-page.component.scss'
})
export class MatchPageComponent implements OnInit{

  private matchService = inject(MatchService);
  private route = inject(ActivatedRoute);
  match$!: Observable<Match>
  
  ngOnInit() {
    let matchId = Number(this.route.snapshot.paramMap.get('id'));
    this.match$ = this.matchService.getMatchById(matchId);
  }
}
