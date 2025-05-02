import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './features/navbar/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, HttpClientModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'FootballScoreClient';
  authService = inject(AuthService);

  ngOnInit(): void {
    this.authService.authorize().subscribe({
      next: (response) => {
        this.authService.currentUserSignal.set(response);
      },
      error: () => {
        this.authService.currentUserSignal.set(null);
      },
    });
  }

  logout() {
    localStorage.setItem('token', '');
    this.authService.currentUserSignal.set(null);
  }
}
