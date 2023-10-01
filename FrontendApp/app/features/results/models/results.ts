export interface MatchResult {
    name: string;
    percentageMatch: number;
    managingInstitutions: string[];
}
export interface Result {
    id: number;
    fieldOfStudyProposals: MatchResult[];
    expertDescription: string;
}