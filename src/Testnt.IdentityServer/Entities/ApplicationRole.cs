using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testnt.IdentityServer.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public const string Admin = "Administrator";
        public const string TestLead = "Test Lead";
        public const string TestEngineer = "Test Engineer";
        public const string TestManager = "Test Manager";
        public const string Other = "Other";

        public ApplicationRole(string name)
        {
            Name = name;
        }

        public string Description { get; set; }
    }
}
