import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { SurveyAnalytics } from '../models/analytics.model';

@Injectable({ providedIn: 'root' })
export class AnalyticsService {
  private http = inject(HttpClient);

  getAnalytics(surveyId: string) {
    return this.http.get<SurveyAnalytics>(`${environment.apiUrl}/surveys/${surveyId}/analytics`);
  }

  exportCsv(surveyId: string) {
    return this.http.get(`${environment.apiUrl}/surveys/${surveyId}/analytics/export`, {
      responseType: 'blob'
    });
  }
}
