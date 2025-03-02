import { inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Match } from '../core/match';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MatchService {
  
  apiService: ApiService;

  constructor() {
    this.apiService = inject(ApiService)
    
  }
  
  getRecentMatches(): Promise<Match[]> {
    console.log("matches")
    return firstValueFrom(this.apiService.getRecentMatches());
  }
}
