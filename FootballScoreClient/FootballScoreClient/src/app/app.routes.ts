import { Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { LeagueListComponent } from './pages/league-list/league-list.component';
import { LeagueDetailsComponent } from './pages/league-details/league-details.component';

export const routes: Routes = [
    {path: '', redirectTo: 'home', pathMatch: 'full'},
    {path: 'home', component: HomePageComponent},
    {path: 'leagues', component: LeagueListComponent},
    {path: 'league/:id', component: LeagueDetailsComponent}
];
