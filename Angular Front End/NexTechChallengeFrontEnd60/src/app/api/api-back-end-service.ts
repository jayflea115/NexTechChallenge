import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Story } from '../models/story';

@Injectable({
    providedIn: 'root'
})
export class ApiBackEndService { 
  constructor(private httpClient: HttpClient) {}

  getNewestStories(): any {
    const url = this.retrievePath() + 'Stories';
    return this.httpClient.get<[Story]>(url);      
  }

  getStoryDetails(storyList: any): any {
    const url = this.retrievePath() + 'Stories/Details/';
    return this.httpClient.put<[Story]>(url, storyList);      
  }

  retrievePath(): string {
    return environment.localhostOverride ? 'https://localhost:7265/' : environment.baseUrl;
  }
}