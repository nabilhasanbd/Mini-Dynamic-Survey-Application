export interface Survey {
  id: string;
  title: string;
  description?: string;
  slug: string;
  isPublished: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface Question {
  id: string;
  surveyId: string;
  questionText: string;
  questionType: QuestionType;
  isRequired: boolean;
  orderIndex: number;
}

export enum QuestionType {
  ShortText = 0,
  LongText = 1,
  MultipleChoice = 2,
  Checkboxes = 3,
  Dropdown = 4,
  Rating = 5
}
