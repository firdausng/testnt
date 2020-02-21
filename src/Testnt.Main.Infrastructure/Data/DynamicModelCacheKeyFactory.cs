using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Infrastructure.Data
{
    public class DynamicModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context)
        {
            var castedContext = context as IMultitenantDbContext;
            if (castedContext == null)
            {
                throw new Exception("Unknown DBContext type");
            }

            return new { castedContext.CurrentUserService.TenantId };
        }
    }
}
