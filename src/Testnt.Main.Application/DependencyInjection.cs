using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Testnt.Common.Mappings;
using Testnt.Main.Application.Middleware.Behaviours;
using Testnt.Main.Application.Seed;

namespace Testnt.Main.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient<DataSeed>();

            AddAutoMapper();

            void AddAutoMapper()
            {
                var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                var applicationAssemblies = allAssemblies.Where(a => a.GetName().Name.StartsWith("Testnt.Main.Application")).ToArray();

                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });

                IMapper mapper = mappingConfig.CreateMapper();
                services.AddSingleton(mapper);
                // not using FluentValidation.AspNetCore package due to issue - https://github.com/JasonGT/NorthwindTraders/issues/76
                // manually register fluent validation
                services.AddValidatorsFromAssemblies(applicationAssemblies);
            }


            return services;
        }
    }
}
