import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatchService } from '../../services/match.service';
import { Observable } from 'rxjs';
import { Team } from '../../core/match';
import { ActivatedRoute } from '@angular/router';
import { OrderByPipe } from '../../shared/pipe/order-by-pipe';

@Component({
  selector: 'app-team-page',
  imports: [CommonModule, OrderByPipe],
  templateUrl: './team-page.component.html',
  styleUrl: './team-page.component.scss'
})
export class TeamPageComponent implements OnInit {
  private teamService = inject(MatchService)
  private route = inject(ActivatedRoute)

  team$!: Observable<Team>

  ngOnInit(): void {
    let teamId = Number(this.route.snapshot.paramMap.get('id'));
    this.team$ = this.teamService.getTeamById(teamId);
  }
}
