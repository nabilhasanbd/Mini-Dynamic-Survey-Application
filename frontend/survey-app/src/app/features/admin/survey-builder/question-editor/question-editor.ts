import { Component, input, output, inject, signal, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators, FormArray } from '@angular/forms';
import { Question, QuestionType, CreateQuestion } from '../../../../core/models/question.model';

const OPTION_TYPES: QuestionType[] = ['MultipleChoice', 'Checkboxes', 'Dropdown'];

@Component({
  selector: 'app-question-editor',
  imports: [ReactiveFormsModule],
  templateUrl: './question-editor.html',
  styleUrl: './question-editor.scss'
})
export class QuestionEditor implements OnInit {
  question  = input<Question | null>(null);
  orderIndex = input(0);
  saved     = output<CreateQuestion>();
  cancelled = output<void>();

  private fb = inject(FormBuilder);

  questionTypes: QuestionType[] = ['ShortText','LongText','MultipleChoice','Checkboxes','Dropdown','Rating'];

  form = this.fb.group({
    questionText: ['', [Validators.required, Validators.maxLength(1000)]],
    questionType: ['ShortText' as QuestionType, Validators.required],
    isRequired:   [false],
    options:      this.fb.array<string>([])
  });

  get options() { return this.form.get('options') as FormArray; }
  get needsOptions() { return OPTION_TYPES.includes(this.form.get('questionType')?.value as QuestionType); }

  ngOnInit() {
    const q = this.question();
    if (q) {
      this.form.patchValue({ questionText: q.questionText, questionType: q.questionType, isRequired: q.isRequired });
      q.options.forEach(o => this.options.push(this.fb.control(o.optionText, Validators.required)));
    }
    this.form.get('questionType')!.valueChanges.subscribe(() => {
      if (!this.needsOptions) this.options.clear();
      else if (this.options.length === 0) this.addOption();
    });
  }

  addOption()         { this.options.push(this.fb.control('', Validators.required)); }
  removeOption(i: number) { this.options.removeAt(i); }

  submit() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const v = this.form.value;
    this.saved.emit({
      questionText: v.questionText!,
      questionType: v.questionType as QuestionType,
      isRequired:   v.isRequired ?? false,
      orderIndex:   this.orderIndex(),
      options:      this.options.value as string[]
    });
  }

  labelFor(type: QuestionType): string {
    const map: Record<QuestionType, string> = {
      ShortText: 'Short Text', LongText: 'Long Text (Paragraph)',
      MultipleChoice: 'Multiple Choice', Checkboxes: 'Checkboxes',
      Dropdown: 'Dropdown', Rating: 'Rating (1–5)'
    };
    return map[type];
  }
}
