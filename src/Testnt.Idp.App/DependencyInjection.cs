using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testnt.Common.Mappings;

namespace Testnt.Idp.App
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
        {
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //services.AddTransient<DataSeed>();

            AddAutoMapper();

            void AddAutoMapper()
            {
                var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                var applicationAssemblies = allAssemblies.Where(a => a.GetName().Name.StartsWith("Testnt.Idp.App")).ToArray();

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
