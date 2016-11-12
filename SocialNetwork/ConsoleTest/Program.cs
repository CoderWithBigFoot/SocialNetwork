using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SocialNetwork.DAL.EF;

namespace ConsoleTest
{
    class Program //когда скипаешь то надо руками остальное скипатЬ(само не скипнется)
    {
        static void Main(string[] args)
        {
            try
            {
                using (SocialNetworkContext context = new SocialNetworkContext()) {
                    
                    

                    Profile prof1 = context.Profiles.FirstOrDefault(x => x.Name == "zheka");
                    Profile prof2 = context.Profiles.FirstOrDefault(x => x.Name == "ser");


                    #region

                    /* Post post1 = new Post() {
                         Content = "post1 content",
                         Publisher = prof1,
                         PublishDate = DateTime.Now
                     };
                     Post post2 = new Post() {
                         Content = "post2 content",
                         Publisher = prof2,
                         PublishDate = DateTime.Now
                     };

                     context.Posts.AddRange(new List<Post>() { post1,post2});



                     Hashtag hash1 = new Hashtag() {
                         Name = "dog"
                     };
                     Hashtag hash2 = new Hashtag() {
                         Name = "cat"
                     };
                     context.Hashtags.AddRange(new List<Hashtag>() { hash1,hash2});
                     */
                    #endregion
                    Post post1 = context.Posts.FirstOrDefault(x=>x.Content == "post1 content");
                    Post post2 = context.Posts.FirstOrDefault(x=>x.Content == "post2 content");
                    //Console.WriteLine(post2.Publisher.Name);
                    Hashtag hash1 = context.Hashtags.FirstOrDefault(x => x.Name == "dog");
                    Hashtag hash2 = context.Hashtags.FirstOrDefault(x => x.Name == "cat");

                    /*post1.Hashtags.Add(hash1);
                    post1.Hashtags.Add(hash2);*/
                    context.Hashtags.Remove(hash2);
                    post1.Hashtags.Remove(hash2);
                    context.SaveChanges();
                    Console.WriteLine("Count of hashtags is " + post1.Hashtags.Count);
                  
                   // Console.WriteLine(post1.Publisher.Name);

                    Console.WriteLine(context.SaveChanges());



                }

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }


            Console.ReadKey();
        }
    }
}
