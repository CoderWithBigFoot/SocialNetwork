using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Interfaces.Interaction
{
   public interface IProfileConfiguration : IDisposable // for change account settings
    {
        void ChangeName(string identityName,string newName);
        void ChangeSername(string identityName,string newSername);
        void ChangeDateOfBirth(string identityName, DateTime newDateOfBirth);
        void ChangeStatus(string identityName, string newStatus);
    }
}
