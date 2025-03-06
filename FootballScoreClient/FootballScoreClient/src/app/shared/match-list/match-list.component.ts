import { Component, Input } from '@angular/core';
import { Match } from '../../core/match';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-match-list',
  imports: [CommonModule, RouterModule],
  templateUrl: './match-list.component.html',
  styleUrl: './match-list.component.scss'
})
export class MatchListComponent {
  @Input() matches: Match[] = [];
}
