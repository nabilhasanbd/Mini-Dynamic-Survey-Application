import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Question, CreateQuestion, UpdateQuestion } from '../models/question.model';

@Injectable({ providedIn: 'root' })
export class QuestionService {
  private http = inject(HttpClient);
  private base = (surveyId: string) => `${environment.apiUrl}/surveys/${surveyId}/questions`;

  getBySurvey(surveyId: string)                              { return this.http.get<Question[]>(this.base(surveyId)); }
  create(surveyId: string, dto: CreateQuestion)              { return this.http.post<Question>(this.base(surveyId), dto); }
  update(surveyId: string, id: string, dto: UpdateQuestion)  { return this.http.put<Question>(`${this.base(surveyId)}/${id}`, dto); }
  delete(surveyId: string, id: string)                       { return this.http.delete(`${this.base(surveyId)}/${id}`); }
  reorder(surveyId: string, ids: string[])                   { return this.http.patch(`${this.base(surveyId)}/reorder`, ids); }
}
