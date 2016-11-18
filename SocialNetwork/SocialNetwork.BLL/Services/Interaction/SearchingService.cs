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

        private ICollection<SocialNetwork.DAL.EF.Hashtag> GetExistingHashtags(IEnumerable<HashtagDTO> hashtags) {
            ICollection<SocialNetwork.DAL.EF.Hashtag> existingHashtags = new List<SocialNetwork.DAL.EF.Hashtag>();
            SocialNetwork.DAL.EF.Hashtag hashtag;
            foreach (var currentHashtag in hashtags)
            {
                hashtag = uow.Hashtags.FindByName(currentHashtag.Name);
                if (hashtag != null)
                {
                    existingHashtags.Add(hashtag);
                }
            }
            return existingHashtags;
        }

        /// <summary>
        /// Searching users,that have one of the hashtagsCollection(parameter) like a popular hashtag
        /// </summary>
        /// <param name="hashtags">Popular hashtags</param>
        /// <param name="offset">Offset</param>
        /// <returns></returns>
       public IEnumerable<ProfileDTO> ProfilesByPopularHashtags(IEnumerable<HashtagDTO> hashtags, int offset,int profilesCount,int hashtagsCount) {

            try {

                ICollection<SocialNetwork.DAL.EF.Hashtag> existingHashtags = GetExistingHashtags(hashtags);

                

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

                return Mapper.Map<IEnumerable<ProfileDTO>>(result).Skip(offset).Take(profilesCount);              
            }
            catch (NullReferenceException){
                return null;
            }

        }


        public IEnumerable<PostDTO> PostsByHashtags(IEnumerable<HashtagDTO> hashtags, int offset,int postsCount) {

            try
            {
                ICollection<SocialNetwork.DAL.EF.Hashtag> existingHashtags = GetExistingHashtags(hashtags);

                ICollection<SocialNetwork.DAL.EF.Post> resultPosts = new List<SocialNetwork.DAL.EF.Post>();

                foreach (var hashtag in existingHashtags) {
                    foreach (var hashtagsPost in hashtag.Posts) {
                        if (!resultPosts.Contains(hashtagsPost)) {
                            resultPosts.Add(hashtagsPost);
                        }
                    }
                }

                Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Post,PostDTO>());
                return Mapper.Map<IEnumerable<PostDTO>>(resultPosts.OrderByDescending(x => x.Hashtags.Count).Skip(offset).Take(postsCount));

            }
            catch (NullReferenceException) {
                return null;
            }

        }


    }
}
