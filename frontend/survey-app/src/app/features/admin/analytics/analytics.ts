import { Component, inject, signal, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AnalyticsService } from '../../../core/services/analytics.service';
import { SurveyAnalytics, QuestionAnalytics } from '../../../core/models/analytics.model';
import { Navbar } from '../../../shared/components/navbar/navbar';
import { Spinner } from '../../../shared/components/spinner/spinner';

@Component({
  selector: 'app-analytics',
  imports: [RouterLink, Navbar, Spinner],
  templateUrl: './analytics.html',
  styleUrl: './analytics.scss'
})
export class Analytics implements OnInit {
  private route   = inject(ActivatedRoute);
  private service = inject(AnalyticsService);

  data      = signal<SurveyAnalytics | null>(null);
  loading   = signal(true);
  error     = signal('');
  exporting = signal(false);

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.service.getAnalytics(id).subscribe({
      next:  d  => { this.data.set(d); this.loading.set(false); },
      error: () => { this.error.set('Failed to load analytics.'); this.loading.set(false); }
    });
  }

  exportCsv() {
    this.exporting.set(true);
    const id = this.route.snapshot.paramMap.get('id')!;
    this.service.exportCsv(id).subscribe({
      next: (blob: Blob) => {
        const url = URL.createObjectURL(blob);
        const a   = document.createElement('a');
        a.href     = url;
        a.download = `survey-${id}-responses.csv`;
        a.click();
        URL.revokeObjectURL(url);
        this.exporting.set(false);
      },
      error: () => { this.error.set('Export failed.'); this.exporting.set(false); }
    });
  }

  hasOptions(q: QuestionAnalytics): boolean {
    return ['MultipleChoice','Checkboxes','Dropdown'].includes(q.questionType);
  }

  isRating(q: QuestionAnalytics): boolean {
    return q.questionType === 'Rating';
  }

  stars(avg: number): boolean[] {
    return Array.from({ length: 5 }, (_, i) => i < Math.round(avg));
  }
}
