using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Services;
using Backend.Middleware;
using Backend.Model.Interfaces;
using Backend.OpenAI;
using Backend.Services;
using Google.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();
builder.Services.AddSingleton<IAcademyRepository, InMemoryAcademyRepository>();
builder.Services.AddSingleton<IResultsRepository, InMemoryResutlsRepository>();
builder.Services.AddSingleton<IQuestionsProvider, QuestionProvider>();
builder.Services.AddSingleton<IQuestionsRepository, InMemoryQuestionsRepository>();
builder.Services.AddTransient<IFieldsOfStudiesService, FieldsOfStudiesService>();
builder.Services.AddTransient<IQuestionaireGenerator, QuestionaireGenerator>();
builder.Services.AddSingleton<OpenAIQuestionProvider>();
builder.Services.AddSingleton<IQuestionHistoryRepository, InMemoryQuestionHistoryRepository>();
builder.Services.AddTransient<IResultsService, ResultsService>();
builder.Services.AddOpenAISearchParams(opt =>
{
    opt.ApiKey = builder.Configuration["OpenAIApiKey"] ?? string.Empty;
});

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddScoped<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
