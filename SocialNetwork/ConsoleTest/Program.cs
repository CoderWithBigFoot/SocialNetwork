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

                #region
                /*   Profile prof1 = context.Profiles.FirstOrDefault(x => x.Name == "zheka");
                   Profile prof2 = context.Profiles.FirstOrDefault(x => x.Name == "ser");
                   */



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

                /* Post post1 = context.Posts.FirstOrDefault(x=>x.Content == "post1 content");
                 Post post2 = context.Posts.FirstOrDefault(x=>x.Content == "post2 content");
                 //Console.WriteLine(post2.Publisher.Name);
                 Hashtag hash1 = context.Hashtags.FirstOrDefault(x => x.Name == "dog");
                 Hashtag hash2 = context.Hashtags.FirstOrDefault(x => x.Name == "cat");

                 /*post1.Hashtags.Add(hash1);
                 post1.Hashtags.Add(hash2);*/
                /* context.Hashtags.Remove(hash2);
                 post1.Hashtags.Remove(hash2);
                 context.SaveChanges();
                 Console.WriteLine("Count of hashtags is " + post1.Hashtags.Count);*/

                // Console.WriteLine(post1.Publisher.Name);

                //Console.WriteLine(context.SaveChanges());
                #endregion
                using (SocialNetworkContext context = new SocialNetworkContext()) {
                    #region profiles initializing
                    /*Profile prof1 = new Profile() {
                        Name = "zheka",
                        Sername = "korsakas",
                        IdentityName = "prof1",
                        CustomInfo = "some info",
                        DateOfBirth = DateTime.Now
                    };
                    Profile prof2 = new Profile()
                    {
                        Name = "ser",
                        Sername = "kononovich",
                        IdentityName = "prof2",
                        CustomInfo = "some info",
                        DateOfBirth = DateTime.Now
                    };
                     Profile prof3 = new Profile()
                    {
                        Name = "artyem",
                        Sername = "kuis",
                        IdentityName = "prof3",
                        CustomInfo = "some info",
                        DateOfBirth = DateTime.Now
                    };
                    context.Profiles.AddRange(new List<Profile>() { prof1, prof2 });*/
                    #endregion

                    Profile prof1 = context.Profiles.FirstOrDefault(x => x.IdentityName == "prof1");
                    Profile prof2 = context.Profiles.FirstOrDefault(x => x.IdentityName == "prof2");
                    #region Followers and subscribedOn tests
                    //Profile prof3 = context.Profiles.FirstOrDefault(x => x.IdentityName == "prof3");

                    /*prof1.Followers.Add(prof2);
                    prof1.Followers.Add(prof3);*/
                    /* prof3.SubscribedOn.Add(prof1);
                     prof3.Followers.Add(prof1);
                     prof3.Followers.Add(prof2);
                         */
                    /*prof1.SubscribedOn.Remove(prof3);
                    prof1.Followers.Remove(prof3);
                    prof2.Followers.Remove(prof3);
                    prof2.SubscribedOn.Remove(prof3);

                    context.Profiles.Remove(prof3);
                    context.SaveChanges();

                    Console.WriteLine("prof1 has followers " + prof1.Followers.Count);
                    Console.WriteLine("prof2 has subscribedOn" + prof2.SubscribedOn.Count);*/
                    /*Console.WriteLine("prof3 has subscribedOn" + prof3.SubscribedOn.Count);
                    Console.WriteLine("prof3 has followers "+prof3.Followers.Count);*/
                    #endregion

                    #region post initializing
                    /*Post post1 = new Post() {
                        Content = "post1",
                        PublishDate = DateTime.Now
                    };
                    Post post2 = new Post() {
                        Content = "post2",
                        PublishDate = DateTime.Now
                    };
                    post1.Publisher = prof1;
                    post2.Publisher = prof2;
                    context.Posts.AddRange(new List<Post>() { post1,post2}); 
                    */
                    #endregion

                    Post post1 = context.Posts.FirstOrDefault(x => x.Content == "post1");
                    Post post2 = context.Posts.FirstOrDefault(x => x.Content == "post2");

                    Console.WriteLine(post1.Publisher.Name);
                    Console.WriteLine(post2.Publisher.Name);

                    /*post1.LikeVoices.Add(prof2);
                    post1.Reposters.Add(prof2);


                    post1.Comments.Add(new Comment() {
                        Content = "post1 from prof2 comment",
                        Commentator = prof2,
                        Post = post1
                    });
                    */
                    Console.WriteLine($"post 1 has {post1.Reposters.Count} reposts" );
                    Console.WriteLine($"post 1 has {post1.LikeVoices.Count} likes");
                    Console.WriteLine($"prof2 has {prof2.Comments.Count} comments");
                    //context.SaveChanges();


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
