import { Question } from './question.model';

export interface Survey {
  id: string;
  title: string;
  description?: string;
  slug: string;
  isPublished: boolean;
  expiryDate?: string;
  createdAt: string;
  updatedAt: string;
  questions: Question[];
}

export interface CreateSurvey {
  title: string;
  description?: string;
  expiryDate?: string;
}

export interface UpdateSurvey {
  title: string;
  description?: string;
  expiryDate?: string;
}
