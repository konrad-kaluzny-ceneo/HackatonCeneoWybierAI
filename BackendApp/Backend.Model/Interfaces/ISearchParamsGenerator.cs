namespace Backend.Model.Interfaces;

public interface ISearchParamsGenerator
{
    Task<GetAcademySearchParams> Generate(List<QuestionWithAnswer> questions);
}
