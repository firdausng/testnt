using System;

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
