using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Mappings;
using Testnt.Common.Models;
using Testnt.Idp.Domain.Entities;
using Testnt.Idp.Infra.Data;

namespace Testnt.Idp.App.Admin.Account.Query.List
{
    public class GetAccountListQuery : IRequest<GetObjectListWithTenantVm<GetAccountListVm>>
    {
        public Guid TenantId { get; }
        public GetAccountListQuery(Guid tenantId)
        {
            TenantId = tenantId;
        }
    }

    public class GetAccountListQueryHandler : IRequestHandler<GetAccountListQuery, GetObjectListWithTenantVm<GetAccountListVm>>
    {
        private readonly TestntIdentityDbContext dbContext;
        private readonly IMapper mapper;

        public GetAccountListQueryHandler(TestntIdentityDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<GetObjectListWithTenantVm<GetAccountListVm>> Handle(GetAccountListQuery request, CancellationToken cancellationToken)
        {
            var tenant = await dbContext
                .Tenants
                .FirstOrDefaultAsync(t => t.Id.Equals(request.TenantId));

            if (tenant == null)
            {
                //return RedirectToPage("/Account/Login");
            }

            var users = await dbContext.Users
                .Where(u => u.TenantId.Equals(request.TenantId))
                .ProjectTo<GetAccountListVm>(mapper.ConfigurationProvider)
                .ToListAsync();

            var vm = new GetObjectListWithTenantVm<GetAccountListVm>
            {
                Data = users,
                Tenant = new TenantDto
                {
                    Name = tenant.Name
                },
                Count = users.Count
            };
            return vm;
        }
    }

    public class GetAccountListVm : IMapFrom<ApplicationUser>
    {
        public Guid Id { get; set; }
        public bool IsEnabled { get; set; }
        public Guid TenantId { get; set; }
        public DateTimeOffset LastLogin { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, GetAccountListVm>();
        }
    }

    public class GetObjectListWithTenantVm<T>: GetObjectListVm<T>
    {
        public TenantDto Tenant { get; set; }
    }

    public class TenantDto
    {
        public string Name { get; set; }
    }
}
