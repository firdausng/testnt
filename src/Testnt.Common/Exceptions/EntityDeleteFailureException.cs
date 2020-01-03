using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Common.Exceptions
{
    public class EntityDeleteFailureException : Exception
    {
        public EntityDeleteFailureException(string name, object key, string message)
            : base($"Deletion of entity \"{name}\" ({key}) failed. {message}")
        {
        }
    }
}