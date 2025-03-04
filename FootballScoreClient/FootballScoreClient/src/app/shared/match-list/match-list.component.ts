import { Component, Input } from '@angular/core';
import { Match } from '../../core/match';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-match-list',
  imports: [CommonModule],
  templateUrl: './match-list.component.html',
  styleUrl: './match-list.component.scss'
})
export class MatchListComponent {
  @Input() matches: Match[] = [];
}
