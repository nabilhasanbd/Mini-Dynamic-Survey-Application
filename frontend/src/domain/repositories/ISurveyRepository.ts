import { Survey } from '../entities/Survey';

export interface ISurveyRepository {
  getAll(): Promise<Survey[]>;
  getById(id: string): Promise<Survey | null>;
  getBySlug(slug: string): Promise<Survey | null>;
  create(survey: Partial<Survey>): Promise<Survey>;
  publish(id: string): Promise<void>;
  unpublish(id: string): Promise<void>;
}
