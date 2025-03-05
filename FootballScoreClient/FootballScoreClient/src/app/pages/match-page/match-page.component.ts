import { Component, inject } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { Observable } from 'rxjs';
import { Match } from '../../core/match';

@Component({
  selector: 'app-match-page',
  imports: [RouterModule],
  templateUrl: './match-page.component.html',
  styleUrl: './match-page.component.scss'
})
export class MatchPageComponent {

  matchService = inject(ApiService);
  
  match$: Observable<Match>
  constructor(private route: ActivatedRoute) {
    let matchId = Number(this.route.snapshot.paramMap.get('id'));
    // this.match$ = 
  }

}
