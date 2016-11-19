using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.Interfaces.ServicesProviders;
using SocialNetwork.BLL.Services.Interaction;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.BLL.Interfaces.Interaction;
using SocialNetwork.BLL.Interfaces.Statistics;

namespace SocialNetwork.BLL.ServicesProviders
{
   public class InteractionProvider : IInteraction
    {
        private PostInteractionService postInteractionService;
        private ProfileConfigurationService profileConfigurationService;
        private ProfileInteractionService profileInteractionService;
        private SearchingService searchingService;
        private IUnitOfWork uow;
        private IGetProfileStatistics profileStatistics;

        public InteractionProvider(IUnitOfWork uow,IGetProfileStatistics profileStatistics) {
            this.uow = uow;
            this.profileStatistics = profileStatistics;
        }

       public IPostInteraction PostInteractionService {
            get {
                if (postInteractionService == null) {
                    postInteractionService = new PostInteractionService(uow);
                }
                return postInteractionService;
            }
        }
       public IProfileConfiguration ProfileConfigurationService {
            get {
                if (profileConfigurationService == null) {
                    profileConfigurationService = new ProfileConfigurationService(uow);
                }
                return profileConfigurationService;
            }
        }
       public IProfileInteraction ProfileInteractionService {
            get {
                if (profileInteractionService == null) {
                    profileInteractionService = new ProfileInteractionService(uow);
                }
                return profileInteractionService;
            }
        }
       public ISearching SearchingService {
            get {
                if (searchingService == null) {
                    searchingService = new Services.Interaction.SearchingService(uow,profileStatistics);
                }
                return searchingService;
            }
        }

        public void Dispose() {
            uow.Dispose();
        }
    }
}
