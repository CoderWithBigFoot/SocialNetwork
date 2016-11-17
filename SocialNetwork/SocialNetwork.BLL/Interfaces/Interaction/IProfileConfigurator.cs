using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Interfaces.Interaction
{
   public interface IProfileConfigurator : IDisposable // for change account settings
    {
        bool ChangeName(string identityName,string newName);
        bool ChangeSername(string identityName,string newSername);
        bool ChangeDateOfBirth(string identityName, DateTime newDateOfBirth);
        bool ChangeStatus(string identityName, string newStatus);
    }
}
