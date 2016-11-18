using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Infrastructure.Exceptions
{
   public class FollowerNotFoundException : Exception
    {
        public FollowerNotFoundException(string message) : base(message) { }
    }
}
