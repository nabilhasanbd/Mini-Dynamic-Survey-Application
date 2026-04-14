import { Component, inject, signal, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SurveyService } from '../../../core/services/survey.service';
import { ResponseService } from '../../../core/services/response.service';
import { Survey } from '../../../core/models/survey.model';
import { Question } from '../../../core/models/question.model';
import { Spinner } from '../../../shared/components/spinner/spinner';

@Component({
  selector: 'app-take-survey',
  imports: [ReactiveFormsModule, Spinner],
  templateUrl: './take-survey.html',
  styleUrl: './take-survey.scss'
})
export class TakeSurvey implements OnInit {
  private route    = inject(ActivatedRoute);
  private surveys  = inject(SurveyService);
  private response = inject(ResponseService);
  private fb       = inject(FormBuilder);

  survey    = signal<Survey | null>(null);
  loading   = signal(true);
  submitting = signal(false);
  submitted  = signal(false);
  error      = signal('');

  form!: FormGroup;
  ratings = [1, 2, 3, 4, 5];

  ngOnInit() {
    const slug = this.route.snapshot.paramMap.get('slug')!;
    this.surveys.getBySlug(slug).subscribe({
      next: s => {
        this.survey.set(s);
        this.buildForm(s.questions);
        this.loading.set(false);
      },
      error: () => { this.error.set('Survey not found or no longer available.'); this.loading.set(false); }
    });
  }

  private buildForm(questions: Question[]) {
    const group: Record<string, any> = {};
    for (const q of questions) {
      if (q.questionType === 'Checkboxes') {
        const optGroup: Record<string, any> = {};
        q.options.forEach(o => optGroup[o.id] = [false]);
        group[q.id] = this.fb.group(optGroup);
      } else {
        group[q.id] = [null, q.isRequired ? Validators.required : null];
      }
    }
    this.form = this.fb.group(group);
  }

  setRating(questionId: string, value: number) {
    this.form.get(questionId)?.setValue(value);
  }

  getRating(questionId: string): number {
    return this.form.get(questionId)?.value ?? 0;
  }

  submit() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }

    const questions = this.survey()!.questions;
    const answers = questions.map(q => {
      const val = this.form.get(q.id)?.value;
      if (q.questionType === 'Rating') {
        return { questionId: q.id, ratingValue: Number(val) };
      }
      if (q.questionType === 'Checkboxes') {
        const selectedOptionIds = q.options
          .filter(o => val?.[o.id])
          .map(o => o.id);
        return { questionId: q.id, selectedOptionIds };
      }
      if (q.questionType === 'MultipleChoice' || q.questionType === 'Dropdown') {
        return { questionId: q.id, selectedOptionIds: [val] };
      }
      return { questionId: q.id, answerText: val };
    });

    this.submitting.set(true);
    this.response.submit(this.survey()!.slug, { answers }).subscribe({
      next:  () => { this.submitted.set(true); this.submitting.set(false); },
      error: () => { this.error.set('Submission failed. Please try again.'); this.submitting.set(false); }
    });
  }
}
