using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Common.Exceptions
{
    public class ApplicationBadRequestException : Exception
    {
        public ApplicationBadRequestException(string message)
            : base(message)
        {
        }
    }
}
