import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { forkJoin, mergeMap, Observable, of, switchMap, tap } from 'rxjs';
import { Match } from '../../core/match';
import { MatchService } from '../../services/match.service';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-match-page',
  imports: [CommonModule, RouterModule],
  templateUrl: './match-page.component.html',
  styleUrl: './match-page.component.scss',
})
export class MatchPageComponent implements OnInit {
  private matchService = inject(MatchService);
  private userService = inject(UserService);
  private route = inject(ActivatedRoute);
  match$!: Observable<Match>;

  isFollowingHomeTeam$!: Observable<boolean>;
  isFollowingAwayTeam$!: Observable<boolean>;

  ngOnInit() {
    let matchId = Number(this.route.snapshot.paramMap.get('id'));
    this.match$ = this.matchService.getMatchById(matchId);

    this.isFollowingHomeTeam$ = this.match$.pipe(
      mergeMap(match => this.userService.isFollowing(match.homeTeam.id))
    );

    this.isFollowingAwayTeam$ = this.match$.pipe(
      mergeMap(match => this.userService.isFollowing(match.awayTeam.id)),
    );
  }
  
  toggleFollow(teamId: number) {
    this.userService.followTeam(teamId);
  }
}
