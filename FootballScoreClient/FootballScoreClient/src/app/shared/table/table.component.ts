import { Component, Input } from '@angular/core';
import { Standings } from '../../core/standings';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-table',
  imports: [CommonModule],
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss'
})
export class TableComponent {
  @Input() table: Standings[] = [];
}
