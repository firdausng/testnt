﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Idp.Domain.Entities;

namespace Testnt.Idp.Infra.Data
{
    public class TestntUserStore : UserStore<ApplicationUser, ApplicationRole, TestntIdentityDbContext, Guid>
    {
        public Guid TenantId { get; set; }
        public TestntUserStore(TestntIdentityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (user.TenantId == null)
            {
                user.TenantId = this.TenantId;
                //throw new ArgumentNullException(nameof(user.TenantId));
            }

            return base.CreateAsync(user, cancellationToken);
        }
    }
}
