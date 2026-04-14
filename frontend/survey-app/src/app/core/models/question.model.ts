export type QuestionType =
  | 'ShortText'
  | 'LongText'
  | 'MultipleChoice'
  | 'Checkboxes'
  | 'Dropdown'
  | 'Rating';

export interface Option {
  id: string;
  optionText: string;
  orderIndex: number;
}

export interface Question {
  id: string;
  questionText: string;
  questionType: QuestionType;
  isRequired: boolean;
  orderIndex: number;
  options: Option[];
}

export interface CreateQuestion {
  questionText: string;
  questionType: QuestionType;
  isRequired: boolean;
  orderIndex: number;
  options: string[];
}

export interface UpdateQuestion {
  questionText: string;
  questionType: QuestionType;
  isRequired: boolean;
  orderIndex: number;
  options: string[];
}
