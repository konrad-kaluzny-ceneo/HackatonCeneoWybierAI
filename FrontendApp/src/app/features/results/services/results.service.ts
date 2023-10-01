import { Injectable } from '@angular/core';
import { MatchResult, Result } from '../models/results';
import { HttpClient } from '@angular/common/http';
import { Observable, delay, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResultsService {

  private baseUrl = 'https://magiczna-flet-api.azurewebsites.net'

  constructor(private httpClient: HttpClient) { }

  public getResults(questionnaireId: number): Observable<Result> {
    return this.httpClient.get<Result>(this.baseUrl + `/api/Results/GetResults/${questionnaireId}`);
  }

}
