using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.BLL.Interfaces.Interaction;
using SocialNetwork.BLL.Infrastructure.Exceptions;
namespace SocialNetwork.BLL.Services.Interaction
{
   public class ProfileInteractionService : IProfileInteraction
    {
        private IUnitOfWork uow;
        public ProfileInteractionService(IUnitOfWork uow) {
            this.uow = uow;
        }


        public void Subscribe(string callerIdentityName, string targetIdentityName) {
            SocialNetwork.DAL.EF.Profile caller = uow.Profiles.FindByIdentityName(callerIdentityName);
            SocialNetwork.DAL.EF.Profile target = uow.Profiles.FindByIdentityName(targetIdentityName);

            if (caller == null || target == null) {
                throw new ProfileNotFoundException("Profile not found");
            }

            if (caller.SubscribedOn.Contains(target)) {
                throw new InvalidSubscriptionException("This user is already exist in your subscriptions");
            }

            caller.SubscribedOn.Add(target);
            uow.Save();
        }
        public void RemoveSubscription(string callerIdentityName,string targetIdentityName) {
            SocialNetwork.DAL.EF.Profile caller = uow.Profiles.FindByIdentityName(callerIdentityName);
            SocialNetwork.DAL.EF.Profile target = uow.Profiles.FindByIdentityName(targetIdentityName);

            if (caller == null || target == null)
            {
                throw new ProfileNotFoundException("Profile not found");
            }

            if (!caller.SubscribedOn.Contains(target)) { throw new CannotRemoveSubscriptionException("Such subscribtion was not found"); }

            caller.SubscribedOn.Remove(target);
            uow.Save();
        }
        public void RemoveFollower(string callerIdentityName, string targetIdentityName) {
            SocialNetwork.DAL.EF.Profile caller = uow.Profiles.FindByIdentityName(callerIdentityName);
            SocialNetwork.DAL.EF.Profile target = uow.Profiles.FindByIdentityName(targetIdentityName);

            if (caller == null || target == null)
            {
                throw new ProfileNotFoundException("Profile not found");
            }

            if (!caller.Followers.Contains(target)) { throw new CannotRemoveFollowerException("Such follower was not found"); }

            caller.Followers.Remove(target);
            uow.Save();
        }

        public void Dispose() {
            uow.Dispose();
        }
    }
}
