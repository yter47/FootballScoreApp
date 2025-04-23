import { Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { LeagueListComponent } from './pages/league-list/league-list.component';
import { LeagueDetailsComponent } from './pages/league-details/league-details.component';
import { MatchPageComponent } from './pages/match-page/match-page.component';
import { TeamPageComponent } from './pages/team-page/team-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';

export const routes: Routes = [
    {path: '', redirectTo: 'login', pathMatch: 'full'},
    {path: 'login', component: LoginPageComponent},
    {path: 'register', component: RegisterPageComponent},
    {path: 'home', component: HomePageComponent},
    {path: 'leagues', component: LeagueListComponent},
    {path: 'league/:id', component: LeagueDetailsComponent},
    {path: 'match/:id', component: MatchPageComponent},
    {path: 'team/:id', component: TeamPageComponent}
];
