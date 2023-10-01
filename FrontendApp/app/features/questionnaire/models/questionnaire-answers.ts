export interface QuestionnaireAnswers {
    answers: QuestionnaireAnswer[];
}

export interface QuestionnaireAnswer {
    questionId: number;
    answer: boolean;
}