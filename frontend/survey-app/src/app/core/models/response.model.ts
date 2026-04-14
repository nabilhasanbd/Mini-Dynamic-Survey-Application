export interface AnswerSubmit {
  questionId: string;
  answerText?: string;
  selectedOptionIds?: string[];
  ratingValue?: number;
}

export interface SubmitResponsePayload {
  answers: AnswerSubmit[];
}

export interface AnswerDetail {
  questionId: string;
  questionText: string;
  answerText?: string;
  selectedOptionIds?: string[];
  ratingValue?: number;
}

export interface ResponseDetail {
  id: string;
  submittedAt: string;
  answers: AnswerDetail[];
}
