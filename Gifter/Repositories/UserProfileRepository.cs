using Gifter.Models;
using Gifter.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Gifter.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Id, Name, Email, ImageUrl, Bio, DateCreated
                  FROM UserProfile  
              ORDER BY DateCreated";

                    var reader = cmd.ExecuteReader();

                    var users = new List<UserProfile>();
                    while (reader.Read())
                    {
                        users.Add(new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            Bio = DbUtils.GetString(reader, "Bio")

                        });
                    }

                    reader.Close();

                    return users;
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                      SELECT Id, Name, Email, ImageUrl, Bio, DateCreated
                            FROM UserProfile 
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    UserProfile user = null;
                    if (reader.Read())
                    {
                        user = new UserProfile()
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            Bio = DbUtils.GetString(reader, "Bio"),
                        };
                    }
                    reader.Close();

                    return user;
                }
            }
        }
        public UserProfile GetUserProfileIdWithPostsAndComments(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT  p.Id As PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated,
                       p.ImageUrl AS PostImageUrl, p.UserProfileId AS PostUserProfileId,

                      up.Id AS UserProfileId, up.Name, up.Bio, up.Email, up.DateCreated AS UserProfileDateCreated,
                       up.ImageUrl AS UserProfileImageUrl,
                       c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId, c.PostId AS CommentPostId
                        

                  FROM UserProfile up
                       LEFT JOIN Post p ON p.UserProfileId = up.id
                        LEFT JOIN Comment c on c.PostId = p.id
                        LEFT JOIN Comment cu on cu.UserProfileId = up.id
                       WHERE up.Id = @Id
              ORDER BY up.DateCreated";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    UserProfile user = null;
                    var posts = new List<Post>();
                    while (reader.Read())
                   {
                        if (user == null)

                        {
                            user = new UserProfile()
                            {


                                Id = id,
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
                                Bio = DbUtils.GetString(reader, "Bio"),

                                Posts = posts

                            };

                        }

                        var postId = DbUtils.GetInt(reader, "PostId");

                        if (DbUtils.IsNotDbNull(reader, "PostUserProfileId") && !posts.Contains(posts.FirstOrDefault(p => p.Id == postId)))
                        {

                            user.Posts.Add(new Post()
                            {
                                Id = postId,
                                Title = DbUtils.GetString(reader, "Title"),
                                Caption = DbUtils.GetString(reader, "Caption"),
                                DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),
                                UserProfileId = id,
                                UserProfile = new UserProfile()
                                {
                                    Id = id,
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
                                    ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
                                },

                                Comments = new List<Comment>()



                            });
                         

                        }

                        foreach (var post in posts)
                        {
                           
                            
                            if (DbUtils.IsNotDbNull(reader, "CommentId") && post.Id == postId)
                            {
                                post.Comments.Add(new Comment()
                                {
                                    Id = DbUtils.GetInt(reader, "CommentId"),
                                    Message = DbUtils.GetString(reader, "Message"),
                                    PostId = postId,
                                    UserProfileId = id,
                                    UserProfile = new UserProfile()
                                    {
                                        Id = DbUtils.GetInt(reader, "CommentUserId"),
                                        Name = DbUtils.GetString(reader, "CommentUserName"),
                                        Email = DbUtils.GetString(reader, "Email"),
                                        DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
                                        ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
                                    }
                                });
                            }
                        
                        }

                    }
                   
                    reader.Close();

                    return user;
                }
            }
           
        }
        public UserProfile GetByEmail(string email)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.Id, u.Name, u.DateCreated, u.ImageUrl, u.Email,
                              u.Bio
                         FROM UserProfile u
                        WHERE email = @email";
                    cmd.Parameters.AddWithValue("@email", email);

                    UserProfile userProfile = null;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            Bio = DbUtils.GetString(reader, "Bio")

                        };
                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }
        public void Add(UserProfile user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile (Name, Email, DateCreated, ImageUrl, Bio)
                        OUTPUT INSERTED.ID
                        VALUES (@Name, @Email, @DateCreated, @ImageUrl, @Bio)";

                    DbUtils.AddParameter(cmd, "@Name", user.Name);
                    DbUtils.AddParameter(cmd, "@Email", user.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", user.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", user.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Bio", user.Bio);

                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                           SET Name = @Name,
                               Email = @Email,
                               DateCreated = @DateCreated,
                               ImageUrl = @ImageUrl,
                               Bio = @Bio
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", user.Name);
                    DbUtils.AddParameter(cmd, "@Email", user.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", user.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", user.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Bio", user.Bio);
                    DbUtils.AddParameter(cmd, "@Id", user.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

