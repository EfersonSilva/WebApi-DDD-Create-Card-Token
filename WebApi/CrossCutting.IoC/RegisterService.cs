using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Application.Command;
using WebApi.Application.CreateToken;
using WebApi.Application.Interfaces;
using WebApi.Application.Query.CheckToken;
using WebApi.Application.Validator;
using WebApi.Infra.Context;
using WebApi.Infra.Repository;

namespace WebApi.CrossCutting.RegisterService
{
    public static class RegisterService
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CardRequest>, ValidatorCard>();
            services.AddTransient<IValidator<CheckTokenRequest>, ValidatorToken>();

            services.AddScoped<ICardApplication, CardApplication>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ICreateToken, CreateToken>();
            services.AddScoped<Context, Context>();

            services.AddScoped<ICheckTokenApplication, CheckTokenApplication>();
        }
    }
}
