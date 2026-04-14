import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Survey, CreateSurvey, UpdateSurvey } from '../models/survey.model';

@Injectable({ providedIn: 'root' })
export class SurveyService {
  private http = inject(HttpClient);
  private base = `${environment.apiUrl}/surveys`;

  getAll()                          { return this.http.get<Survey[]>(this.base); }
  getById(id: string)               { return this.http.get<Survey>(`${this.base}/${id}`); }
  getBySlug(slug: string)           { return this.http.get<Survey>(`${this.base}/slug/${slug}`); }
  create(dto: CreateSurvey)         { return this.http.post<Survey>(this.base, dto); }
  update(id: string, dto: UpdateSurvey) { return this.http.put<Survey>(`${this.base}/${id}`, dto); }
  delete(id: string)                { return this.http.delete(`${this.base}/${id}`); }
  publish(id: string)               { return this.http.patch(`${this.base}/${id}/publish`, {}); }
  unpublish(id: string)             { return this.http.patch(`${this.base}/${id}/unpublish`, {}); }
}
