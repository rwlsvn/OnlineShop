using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Library.Exceptions
{
    public class InvalidEntityOwnershipException : Exception
    {
        public InvalidEntityOwnershipException
            (string entityName, object entityKey, Guid userId)
            : base($"User '{userId}' does not have ownership or " +
                  $"permission to access the '{entityName}' ({entityKey})")
        { }
    }
}
