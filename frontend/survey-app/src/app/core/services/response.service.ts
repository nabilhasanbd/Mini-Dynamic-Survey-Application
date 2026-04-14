import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { SubmitResponsePayload, ResponseDetail } from '../models/response.model';

@Injectable({ providedIn: 'root' })
export class ResponseService {
  private http = inject(HttpClient);

  submit(slug: string, dto: SubmitResponsePayload) {
    return this.http.post<{ responseId: string }>(`${environment.apiUrl}/s/${slug}/respond`, dto);
  }

  getBySurvey(surveyId: string) {
    return this.http.get<ResponseDetail[]>(`${environment.apiUrl}/surveys/${surveyId}/responses`);
  }
}
