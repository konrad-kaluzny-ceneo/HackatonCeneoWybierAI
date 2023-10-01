namespace Backend.Model.Interfaces;

public interface ISearchParamsGenerator
{
    Task<GetAcademySearchParams> Generate(List<QuestionWithAnswer> questions);
    Task<GetAcademySearchParams> Generate(List<QuestionAndAnswer> questions);
}
