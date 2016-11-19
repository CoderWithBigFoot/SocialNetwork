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
        private SocialNetwork.DAL.EF.Profile GetProfile(string identityName)
        {
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
            if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }
            return profile;
        }

        public void Subscribe(string callerIdentityName, string targetIdentityName) {
            SocialNetwork.DAL.EF.Profile caller = this.GetProfile(callerIdentityName);
            SocialNetwork.DAL.EF.Profile target = this.GetProfile(targetIdentityName);

            if (caller.SubscribedOn.Contains(target)) {
                throw new InvalidSubscriptionException("This user is already exist in your subscriptions");
            }
            caller.SubscribedOn.Add(target);
            uow.Save();
        }
        public void RemoveSubscription(string callerIdentityName,string targetIdentityName) {
            SocialNetwork.DAL.EF.Profile caller = this.GetProfile(callerIdentityName);
            SocialNetwork.DAL.EF.Profile target = this.GetProfile(targetIdentityName);

            if (!caller.SubscribedOn.Contains(target)) { throw new SubscriptionNotFoundException("Such subscribtion was not found"); }

            caller.SubscribedOn.Remove(target);
            uow.Save();
        }
        public void RemoveFollower(string callerIdentityName, string targetIdentityName) {
            SocialNetwork.DAL.EF.Profile caller = this.GetProfile(callerIdentityName);
            SocialNetwork.DAL.EF.Profile target = this.GetProfile(targetIdentityName);

            if (!caller.Followers.Contains(target)) { throw new FollowerNotFoundException("Such follower was not found"); }

            caller.Followers.Remove(target);
            uow.Save();
        }

        public void Dispose() {
            uow.Dispose();
        }
    }
}
