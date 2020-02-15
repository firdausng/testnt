using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Seed
{
    public class DataSeed
    {
        private readonly TestntDbContext testntDbContext;
        private readonly ILogger<DataSeed> logger;

        public DataSeed(TestntDbContext testntDbContext, ILogger<DataSeed> logger)
        {
            this.testntDbContext = testntDbContext;
            this.logger = logger;
        }

        public void EnsureSeedData()
        {
            logger.LogInformation("Migrating db");
            testntDbContext.Database.Migrate();
            //context.Database.upda;

            //var sampleProject = context.Projects.Where(t => t.Name.Equals("Testnt")).FirstOrDefaultAsync().Result;
            //if (sampleProject == null)
            //{
            //    sampleProject = new TestProject
            //    {
            //        Name = "Testnt",

            //    };
            //    context.Projects.Add(sampleProject);
            //    context.SaveChanges();
            //}

            //var mediator = scope.ServiceProvider.GetService<IMediator>();
            //var getTestProjectList = mediator.Send(new GetTestProjectListQuery()).Result;
            //if (getTestProjectList.Count == 0)
            //{
            //    var CreateTestProjectItem = mediator.Send(new CreateTestProjectItemCommand
            //    {
            //        Name = "Testnt"
            //    }).Result;

            //    if (CreateTestProjectItem.Id != null)
            //    {
            //        Console.WriteLine($"{CreateTestProjectItem.Id} successfully created");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{CreateTestProjectItem.Id} failed to be created");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine($"Sample test project already created");
            //}
        }
    }
}
