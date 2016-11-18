using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Infrastructure.Exceptions
{
   public class CannotRemoveFollowerException : Exception
    {
        public CannotRemoveFollowerException(string message) : base(message) { }
    }
}
