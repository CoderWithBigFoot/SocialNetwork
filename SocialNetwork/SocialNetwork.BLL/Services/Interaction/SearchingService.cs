using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.BLL.Interfaces.Interaction;
using SocialNetwork.BLL.Interfaces.Statistics;
using AutoMapper;
namespace SocialNetwork.BLL.Services.Interaction
{
   public class SearchingService : ISearching
    {
        private IUnitOfWork uow;
        private IGetProfileStatistics profileStatistics;
        public SearchingService(IUnitOfWork uow,IGetProfileStatistics profileStatistics) {
            this.uow = uow;
            this.profileStatistics = profileStatistics;
        }

        /// <summary>
        /// Searching users,that have one of the hashtagsCollection(parameter) like a popular hashtag
        /// </summary>
        /// <param name="hashtags">Popular hashtags</param>
        /// <param name="offset">Offset</param>
        /// <returns></returns>
       public IEnumerable<ProfileDTO> ProfilesByPopularHashtags(IEnumerable<HashtagDTO> hashtags, int offset,int hashtagsCount) {

            try {

                ICollection<SocialNetwork.DAL.EF.Hashtag> existingHashtags = new List<SocialNetwork.DAL.EF.Hashtag>();

                SocialNetwork.DAL.EF.Hashtag hashtag;
                foreach (var currentHashtag in hashtags) {
                    hashtag = uow.Hashtags.FindByName(currentHashtag.Name);
                    if (hashtag != null) {
                        existingHashtags.Add(hashtag);
                    }
                }

                Mapper.Initialize(cfg => {
                    cfg.CreateMap<SocialNetwork.DAL.EF.Hashtag, HashtagDTO>() ;
                    cfg.CreateMap<SocialNetwork.DAL.EF.Profile, ProfileDTO>();
                });
                

                ICollection<SocialNetwork.DAL.EF.Profile> result = new List<SocialNetwork.DAL.EF.Profile>();
                IEnumerable<SocialNetwork.DAL.EF.Profile> allProfiles = uow.Profiles.GetAllItems();

                
                

                foreach (var currentHashtag in Mapper.Map<ICollection<HashtagDTO>>(existingHashtags)) {
                    foreach (var currentProfile in allProfiles) {
                        foreach (var pair in profileStatistics.MostPopularHashtags(currentProfile.IdentityName, hashtagsCount)) {
                            if (pair.Key == currentHashtag) {
                                if (!result.Contains(currentProfile)) {
                                    result.Add(currentProfile);
                                }
                            }
                        }
                       
                    }
                }

                return Mapper.Map<IEnumerable<ProfileDTO>>(result);
                
            }
            catch (NullReferenceException){
                return null;
            }

        }



    }
}
