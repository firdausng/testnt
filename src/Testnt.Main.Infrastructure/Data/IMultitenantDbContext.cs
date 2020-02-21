using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Common.Interface;

namespace Testnt.Main.Infrastructure.Data
{
    public interface IMultitenantDbContext
    {
        ICurrentUserService CurrentUserService { get; }
    }
}
