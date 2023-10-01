using Backend.Model.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;
using OpenAI.Extensions;

namespace Backend.OpenAI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddOpenAISearchParams(this IServiceCollection services, Action<OpenAiOptions> options)
    {
        services.AddOpenAIService(settings => options(settings));
        services.AddScoped<ISearchParamsGenerator, OpenAISearchParamsGenerator>();
        services.AddScoped<IQuestionaryStatisticsProvider, OpenAIQuestionaryStatisticsProvider>();
        return services;
    }
}
