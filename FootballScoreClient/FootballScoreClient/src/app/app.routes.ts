import { Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { LeagueListComponent } from './pages/league-list/league-list.component';
import { LeagueDetailsComponent } from './pages/league-details/league-details.component';
import { MatchPageComponent } from './pages/match-page/match-page.component';
import { TeamPageComponent } from './pages/team-page/team-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { AuthGuard } from './shared/interceptors/auth.guard';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';

export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'home', component: HomePageComponent },
      { path: 'leagues', component: LeagueListComponent },
      { path: 'league/:id', component: LeagueDetailsComponent },
      { path: 'match/:id', component: MatchPageComponent },
      { path: 'team/:id', component: TeamPageComponent },
    ],
  },
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: 'login', component: LoginPageComponent },
      { path: 'register', component: RegisterPageComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];
