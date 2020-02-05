using System;
using Testnt.Common.Interface;

namespace Testnt.Main.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
