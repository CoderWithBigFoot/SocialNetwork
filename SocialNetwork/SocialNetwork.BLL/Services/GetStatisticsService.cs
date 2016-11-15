using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.BLL.Interfaces;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Interfaces;
using AutoMapper;

namespace SocialNetwork.BLL.Services
{
   public class GetStatisticsService : IGetStatistics
    {
        private IUnitOfWork uow;
        public GetStatisticsService(IUnitOfWork uow) {
            this.uow = uow;
        }

        public int PublishedPostsCount(string identityName)
        {
            try
            {
                SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
                return profile.PublishedPosts.Count;
            }
            catch (NullReferenceException) {
                return 0;
            }
        }
        public ICollection<HashtagDTO> AllHashtags(string identityName)
        {
            try
            {
                SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
                ICollection<SocialNetwork.DAL.EF.Hashtag> allProfileHashtags = new List<SocialNetwork.DAL.EF.Hashtag>();

                foreach (var currentPost in profile.PublishedPosts)
                {
                    foreach (var currentHashtag in currentPost.Hashtags)
                    {

                        if (!allProfileHashtags.Contains(currentHashtag))
                        {
                            allProfileHashtags.Add(currentHashtag);
                        }
                    }
                }

                Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Hashtag, HashtagDTO>());
                return Mapper.Map<ICollection<HashtagDTO>>(allProfileHashtags);
            }
            catch (NullReferenceException) {
                return null;
            }
        }
        public Dictionary<HashtagDTO, int> EachHashtagCount(string identityName)
        {
            try {
                SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);

                
                Dictionary<SocialNetwork.DAL.EF.Hashtag, int> result = new Dictionary<SocialNetwork.DAL.EF.Hashtag, int>();

                foreach (var currentPost in profile.PublishedPosts) {
                    foreach (var currentHashtag in currentPost.Hashtags) {
                        if (!result.ContainsKey(currentHashtag)) {//////////////////////////////////
                            result.Add(currentHashtag, 1);
                        }
                        if (result.ContainsKey(currentHashtag)) {
                            result[currentHashtag]++;
                        }
                    }                  
                }

                Mapper.Initialize(cfg => cfg.CreateMap<SocialNetwork.DAL.EF.Hashtag, HashtagDTO>());
                return Mapper.Map<Dictionary<HashtagDTO, int>>(result);
            }
            catch (NullReferenceException) {
                return null;
            }
        }



        public IEnumerable<KeyValuePair<HashtagDTO,int>> MostPopularHashtags(string identityName, int count)
        {
            try
            {
                Dictionary<HashtagDTO, int> eachHashtagCount = this.EachHashtagCount(identityName);

                var result = eachHashtagCount.OrderByDescending(x => x.Value).Take(count);

            

            }
            catch (NullReferenceException) {
                return null;
            }
        }






        public Dictionary<HashtagDTO, int> MostPopularHashtagsFrequency(string identityName, int count)
        {
            throw new NotImplementedException();
        }



        public void Dispose() {
            uow.Dispose();
        }
    }
}
