import { Injectable } from '@angular/core';
import { Questionnaire } from '../models/questionnaire';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { QuestionnaireAnswer, QuestionnaireAnswers } from '../models/questionnaire-answers';

@Injectable({
  providedIn: 'root'
})
export class QuestionnaireService {

  private baseUrl = 'https://magiczna-flet-api.azurewebsites.net'

  constructor(private httpClient: HttpClient) { }

  public getQuestionnaire(): Observable<Questionnaire> {
    return this.httpClient.get<Questionnaire>(this.baseUrl + '/api/Quiz/GetQuizQuestions');
  }

  public sendFilledQuestionnaire(questionnaire: Questionnaire): Observable<number> {
    const body = this.mapQuestionnaireToAnswers(questionnaire);
    return this.httpClient.post<number>(this.baseUrl + '/api/Quiz/PostQuizResults', body);
  }

  private mapQuestionnaireToAnswers(questionnaire: Questionnaire): QuestionnaireAnswers {
    const questionnaireAnswers = questionnaire.questions.map(q => {
     return <QuestionnaireAnswer> {
       questionId: q.id,
       answer: q.answer
     }
   });

   const result = <QuestionnaireAnswers> {answers: questionnaireAnswers};
   return result;
  }
  
}
