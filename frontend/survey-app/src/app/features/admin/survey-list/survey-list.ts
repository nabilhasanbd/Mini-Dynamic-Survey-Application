import { Component, inject, signal, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { DatePipe } from '@angular/common';
import { SurveyService } from '../../../core/services/survey.service';
import { Survey } from '../../../core/models/survey.model';
import { Navbar } from '../../../shared/components/navbar/navbar';
import { Spinner } from '../../../shared/components/spinner/spinner';

@Component({
  selector: 'app-survey-list',
  imports: [RouterLink, Navbar, Spinner, DatePipe],
  templateUrl: './survey-list.html',
  styleUrl: './survey-list.scss'
})
export class SurveyList implements OnInit {
  private service = inject(SurveyService);
  private router  = inject(Router);

  surveys = signal<Survey[]>([]);
  loading = signal(true);
  error   = signal('');

  ngOnInit() { this.load(); }

  load() {
    this.loading.set(true);
    this.service.getAll().subscribe({
      next:  d  => { this.surveys.set(d); this.loading.set(false); },
      error: () => { this.error.set('Failed to load surveys.'); this.loading.set(false); }
    });
  }

  togglePublish(s: Survey) {
    (s.isPublished ? this.service.unpublish(s.id) : this.service.publish(s.id))
      .subscribe(() => this.load());
  }

  delete(id: string) {
    if (!confirm('Delete this survey? This action cannot be undone.')) return;
    this.service.delete(id).subscribe(() => this.load());
  }

  publicLink(slug: string) {
    return `${window.location.origin}/s/${slug}`;
  }

  copyLink(slug: string) {
    navigator.clipboard.writeText(this.publicLink(slug));
  }
}
