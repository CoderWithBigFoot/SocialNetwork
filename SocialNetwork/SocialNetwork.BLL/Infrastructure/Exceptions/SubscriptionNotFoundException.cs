using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Infrastructure.Exceptions
{
   public class SubscriptionNotFoundException : Exception
    {
        public SubscriptionNotFoundException(string message) : base(message) { }
    }
}
