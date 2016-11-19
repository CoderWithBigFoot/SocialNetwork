using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Interfaces.Interaction;
namespace SocialNetwork.BLL.Interfaces.ServicesProviders
{
   public interface IInteraction
    {
        IPostInteraction PostInteractionService { get; }
        IProfileConfiguration ProfileConfigurationService { get; }
        IProfileInteraction ProfileInteractionService { get; }
        ISearching SearchingService { get; }
    }
}
