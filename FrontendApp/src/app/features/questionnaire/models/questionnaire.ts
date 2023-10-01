export interface Questionnaire {
    id: number;
    questions: Question[];
}

export interface Question {
    id: number;
    value: string;
    answer: boolean;
}