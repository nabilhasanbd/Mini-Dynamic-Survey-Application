using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Application.Interfaces.Services;
using SurveyApp.Application.Services;
using SurveyApp.Application.Validators;

namespace SurveyApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();

            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

            return services;
        }
    }
}
