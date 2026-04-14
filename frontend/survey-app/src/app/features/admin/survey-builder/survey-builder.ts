import { Component, inject, signal, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SurveyService } from '../../../core/services/survey.service';
import { QuestionService } from '../../../core/services/question.service';
import { Survey } from '../../../core/models/survey.model';
import { Question, CreateQuestion } from '../../../core/models/question.model';
import { Navbar } from '../../../shared/components/navbar/navbar';
import { Spinner } from '../../../shared/components/spinner/spinner';
import { QuestionEditor } from './question-editor/question-editor';

@Component({
  selector: 'app-survey-builder',
  imports: [ReactiveFormsModule, Navbar, Spinner, QuestionEditor],
  templateUrl: './survey-builder.html',
  styleUrl: './survey-builder.scss'
})
export class SurveyBuilder implements OnInit {
  private route   = inject(ActivatedRoute);
  private router  = inject(Router);
  private surveys = inject(SurveyService);
  private qSvc    = inject(QuestionService);
  private fb      = inject(FormBuilder);

  survey    = signal<Survey | null>(null);
  questions = signal<Question[]>([]);
  loading   = signal(true);
  saving    = signal(false);
  saved     = signal(false);
  error     = signal('');
  isNew     = signal(true);
  editingQ  = signal<Question | null | 'new'>(null);

  form = this.fb.group({
    title:       ['', [Validators.required, Validators.maxLength(500)]],
    description: [''],
    expiryDate:  [null as string | null]
  });

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isNew.set(false);
      this.surveys.getById(id).subscribe({
        next: s => {
          this.survey.set(s);
          this.questions.set([...s.questions].sort((a, b) => a.orderIndex - b.orderIndex));
          this.form.patchValue({ title: s.title, description: s.description ?? '', expiryDate: s.expiryDate ?? null });
          this.loading.set(false);
        },
        error: () => { this.error.set('Survey not found.'); this.loading.set(false); }
      });
    } else {
      this.loading.set(false);
    }
  }

  saveSurveyInfo() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving.set(true);
    const dto = this.form.value as any;
    const action = this.isNew()
      ? this.surveys.create(dto)
      : this.surveys.update(this.survey()!.id, dto);

    action.subscribe({
      next: s => {
        this.survey.set(s);
        this.saving.set(false);
        this.saved.set(true);
        if (this.isNew()) {
          this.isNew.set(false);
          this.router.navigate(['/admin/surveys', s.id], { replaceUrl: true });
        }
        setTimeout(() => this.saved.set(false), 2000);
      },
      error: () => { this.error.set('Failed to save.'); this.saving.set(false); }
    });
  }

  addQuestion() { this.editingQ.set('new'); }

  editQuestion(q: Question) { this.editingQ.set(q); }

  onQuestionSaved(dto: CreateQuestion) {
    const surveyId = this.survey()?.id;
    if (!surveyId) { this.error.set('Save the survey info first.'); return; }

    const editing = this.editingQ();
    const isEdit  = editing && editing !== 'new';
    const action  = isEdit
      ? this.qSvc.update(surveyId, (editing as Question).id, dto)
      : this.qSvc.create(surveyId, { ...dto, orderIndex: this.questions().length });

    action.subscribe({
      next: q => {
        const list = this.questions();
        if (isEdit) {
          this.questions.set(list.map(x => x.id === q.id ? q : x));
        } else {
          this.questions.set([...list, q]);
        }
        this.editingQ.set(null);
      },
      error: () => this.error.set('Failed to save question.')
    });
  }

  deleteQuestion(id: string) {
    if (!confirm('Delete this question?')) return;
    this.qSvc.delete(this.survey()!.id, id).subscribe(() => {
      this.questions.set(this.questions().filter(q => q.id !== id));
    });
  }

  moveUp(index: number) {
    if (index === 0) return;
    const list = [...this.questions()];
    [list[index - 1], list[index]] = [list[index], list[index - 1]];
    this.questions.set(list);
    this.saveOrder(list.map(q => q.id));
  }

  moveDown(index: number) {
    const list = [...this.questions()];
    if (index >= list.length - 1) return;
    [list[index], list[index + 1]] = [list[index + 1], list[index]];
    this.questions.set(list);
    this.saveOrder(list.map(q => q.id));
  }

  private saveOrder(ids: string[]) {
    this.qSvc.reorder(this.survey()!.id, ids).subscribe();
  }

  labelFor(type: string): string {
    const map: Record<string, string> = {
      ShortText: 'Short Text', LongText: 'Long Text',
      MultipleChoice: 'Multiple Choice', Checkboxes: 'Checkboxes',
      Dropdown: 'Dropdown', Rating: 'Rating'
    };
    return map[type] ?? type;
  }
}
