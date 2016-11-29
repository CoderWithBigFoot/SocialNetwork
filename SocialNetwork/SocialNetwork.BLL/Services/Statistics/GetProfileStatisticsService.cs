using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.BLL.Interfaces.Statistics;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.BLL.Infrastructure.Exceptions;
using AutoMapper;

namespace SocialNetwork.BLL.Services.Statistics
{//need to zero check and null check
    //if zero - there are no posts
    //if null / there are no user
   public class GetProfileStatisticsService : IGetProfileStatistics
    {//во всех чекнуть на наличие постов
        private IUnitOfWork uow;
        public GetProfileStatisticsService(IUnitOfWork uow) {
            this.uow = uow;
        }

        private SocialNetwork.DAL.EF.Profile GetProfile(string identityName) {
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
            if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }
            return profile;
        }
        public int PublishedPostsCount(string identityName)
        {
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
            //if (profile == null) { throw new ProfileNotFoundException("Profile was not found");}
            return profile.PublishedPosts.Count; 
        }
        public IEnumerable<HashtagDTO> AllHashtags(string identityName)
        {
            SocialNetwork.DAL.EF.Profile profile = this.GetProfile(identityName);
            ICollection<SocialNetwork.DAL.EF.Hashtag> allProfileHashtags = new List<SocialNetwork.DAL.EF.Hashtag>();
            if (profile.PublishedPosts.Count == 0) { throw new PublishedPostsNotFoundException("Profile has no published posts"); }

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
        public Dictionary<HashtagDTO, int> EachHashtagCount(string identityName)
        {
            SocialNetwork.DAL.EF.Profile profile = this.GetProfile(identityName);
            Dictionary<SocialNetwork.DAL.EF.Hashtag, int> result = new Dictionary<SocialNetwork.DAL.EF.Hashtag, int>();
            if (profile.PublishedPosts.Count == 0) { throw new PublishedPostsNotFoundException("Profile has no published posts"); }

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
        public IEnumerable<KeyValuePair<HashtagDTO,int>> MostPopularHashtags(string identityName, int count=3)
        {
                Dictionary<HashtagDTO, int> eachHashtagCount = this.EachHashtagCount(identityName);
                return eachHashtagCount.OrderByDescending(x => x.Value).Take(count);    
        }      
        public Dictionary<HashtagDTO, double> MostPopularHashtagsFrequency(string identityName, int count=3,TimeInterval interval=TimeInterval.Week)
        {
            SocialNetwork.DAL.EF.Profile profile = this.GetProfile(identityName);
                int period = 0;
                switch (interval) {
                    case TimeInterval.Day:period = 1;break;
                    case TimeInterval.Week:period = 7;break;
                    case TimeInterval.Month:period = 30;break;
                }

                IEnumerable<KeyValuePair<HashtagDTO, int>> mostPopularHashtags = this.MostPopularHashtags(identityName, count);
                DateTime firstPublicationDate = profile.PublishedPosts.OrderBy(x => x.PublishDate).ElementAt(0).PublishDate;
                Dictionary<HashtagDTO, double> result = new Dictionary<HashtagDTO, double>();
                double frequency = 0;

                foreach (var popularHashtagCount in mostPopularHashtags) {
                    if (!result.ContainsKey(popularHashtagCount.Key)) {
                         frequency = popularHashtagCount.Value/(((DateTime.Now-firstPublicationDate).TotalDays)/period); // maybe here Math.Round() is needed
                         result.Add(popularHashtagCount.Key, frequency);
                         frequency = 0; 
                    }
                }
                return result;
          
        }     
        public Dictionary<HashtagDTO, int> SelectedHashtagsCount(string identityName, IEnumerable<HashtagDTO> hashtags) {
           
                Dictionary<HashtagDTO, int> eachHashtagCount = this.EachHashtagCount(identityName);
                Dictionary<HashtagDTO, int> result = new Dictionary<HashtagDTO, int>();

                if (hashtags == null) { throw new ArgumentNullException("Hashtags must consist of at least one element"); }
                foreach (var currentHashtag in hashtags)
                {
                    if (eachHashtagCount.ContainsKey(currentHashtag))
                    {
                        result.Add(currentHashtag, eachHashtagCount[currentHashtag]);
                    }
                } 
                return result;    
        }
        public void Dispose() {
            uow.Dispose();
        }
    }
}
