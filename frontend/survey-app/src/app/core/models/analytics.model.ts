export interface OptionCount {
  optionId: string;
  optionText: string;
  count: number;
  percentage: number;
}

export interface QuestionAnalytics {
  questionId: string;
  questionText: string;
  questionType: string;
  totalAnswers: number;
  optionCounts: OptionCount[];
  averageRating?: number;
}

export interface SurveyAnalytics {
  surveyId: string;
  title: string;
  totalResponses: number;
  questions: QuestionAnalytics[];
}
