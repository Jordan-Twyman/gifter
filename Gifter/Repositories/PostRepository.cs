using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Gifter.Models;
using Gifter.Utils;
using Microsoft.Data.SqlClient;

namespace Gifter.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(IConfiguration configuration) : base(configuration) { }

        public List<Post> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated, 
                       p.ImageUrl AS PostImageUrl, p.UserProfileId as PostUserProfileId,

                       up.Name as UserProfileName, up.Bio, up.Email, up.DateCreated AS UserProfileDateCreated, 
                       up.ImageUrl AS UserProfileImageUrl
                  FROM Post p 
                       LEFT JOIN UserProfile up ON p.UserProfileId = up.id
              ORDER BY p.DateCreated";

                    var reader = cmd.ExecuteReader();

                    var posts = new List<Post>();
                    while (reader.Read())
                    {
                        posts.Add(NewPostFromReader(reader));
                    }

                    reader.Close();

                    return posts;
                }
            }
        }
        public List<Post> GetAllWithComments()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated,
                       p.ImageUrl AS PostImageUrl, p.UserProfileId AS PostUserProfileId,

                       up.Id as UserProfileId, up.Name as UserProfileName, up.Bio, up.Email, up.DateCreated AS UserProfileDateCreated,
                       up.ImageUrl AS UserProfileImageUrl,

                       c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId,
                       cup.Name as CommentUserName
                  FROM Post p
                       LEFT JOIN UserProfile up ON p.UserProfileId = up.id
                       LEFT JOIN Comment c on c.PostId = p.id
                        LEFT JOIN UserProfile cup ON c.UserProfileId = cup.id
              ORDER BY p.DateCreated";

                    var reader = cmd.ExecuteReader();

                    var posts = new List<Post>();
                    while (reader.Read())
                    {
                        var postId = DbUtils.GetInt(reader, "PostId");

                        var existingPost = posts.FirstOrDefault(p => p.Id == postId);
                        if (existingPost == null)
                        {
                            existingPost = NewPostFromReader(reader);

                            posts.Add(existingPost);
                        }

                        if (DbUtils.IsNotDbNull(reader, "CommentId"))
                        {
                            existingPost.Comments.Add(new Comment()
                            {
                                Id = DbUtils.GetInt(reader, "CommentId"),
                                Message = DbUtils.GetString(reader, "Message"),
                                PostId = postId,
                                UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "CommentUserProfileId"),
                                    Name = DbUtils.GetString(reader, "CommentUserName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
                                    ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
                                }
                            });
                        }
                    }

                    reader.Close();

                    return posts;
                }
            }
        }

        public Post GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated,
                       p.ImageUrl AS PostImageUrl, p.UserProfileId AS PostUserProfileId,

                       up.Name  as UserProfileName, up.Bio, up.Email, up.DateCreated AS UserProfileDateCreated,
                       up.ImageUrl AS UserProfileImageUrl

                       FROM Post p
                       LEFT JOIN UserProfile up ON p.UserProfileId = up.id

                           WHERE p.Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Post post = null;
                    if (reader.Read())
                    {
                        post = NewPostFromReader(reader);
                        
                    }
                    reader.Close();

                    return post;
                }
            }
        }
        public Post GetPostIdWithComments(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated,
                       p.ImageUrl AS PostImageUrl, p.UserProfileId AS PostUserProfileId,
                       up.Name as UserProfileName, up.Bio, up.Email, up.DateCreated AS UserProfileDateCreated,
                       up.ImageUrl AS UserProfileImageUrl,
                       c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId,
                       cup.Name as CommentUserName
                        
                  FROM Post p
                       LEFT JOIN UserProfile up ON p.UserProfileId = up.id
                       LEFT JOIN Comment c on c.PostId = p.id
                       LEFT JOIN UserProfile cup on c.UserProfileId = cup.Id
                       WHERE p.Id = @Id
              ORDER BY p.DateCreated";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Post post = null;
                    while (reader.Read())
                    {
                        if (post == null)
                        {
                            post = NewPostFromReader(reader);

                        }

                        if (DbUtils.IsNotDbNull(reader, "CommentId"))
                        {
                            post.Comments.Add(new Comment()
                            {
                                Id = DbUtils.GetInt(reader, "CommentId"),
                                Message = DbUtils.GetString(reader, "Message"),
                                PostId = DbUtils.GetInt(reader, "PostId"),
                                UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "CommentUserProfileId"),
                                    Name = DbUtils.GetString(reader, "CommentUserName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
                                    ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
                                }
                            });
                        }
                    }

                    reader.Close();

                    return post;
                }
            }
        }

        public void Add(Post post)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Post (Title, Caption, DateCreated, ImageUrl, UserProfileId)
                        OUTPUT INSERTED.ID
                        VALUES (@Title, @Caption, @DateCreated, @ImageUrl, @UserProfileId)";

                    DbUtils.AddParameter(cmd, "@Title", post.Title);
                    DbUtils.AddParameter(cmd, "@Caption", post.Caption);
                    DbUtils.AddParameter(cmd, "@DateCreated", DateTime.Now);
                    DbUtils.AddParameter(cmd, "@ImageUrl", post.ImageUrl);
                    DbUtils.AddParameter(cmd, "@UserProfileId", post.UserProfileId);

                    post.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Post post)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Post
                           SET Title = @Title,
                               Caption = @Caption,
                               DateCreated = @DateCreated,
                               ImageUrl = @ImageUrl,
                               UserProfileId = @UserProfileId
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Title", post.Title);
                    DbUtils.AddParameter(cmd, "@Caption", post.Caption);
                    DbUtils.AddParameter(cmd, "@DateCreated", post.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", post.ImageUrl);
                    DbUtils.AddParameter(cmd, "@UserProfileId", post.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Id", post.Id);

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
                    cmd.CommandText = "DELETE FROM Post WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public List<Post> Search(string criterion, bool sortDescending)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    var sql =
                        @"SELECT p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated, 
                        p.ImageUrl AS PostImageUrl, p.UserProfileId as PostUserProfileId,

                        up.Name as UserProfileName, up.Bio, up.Email, up.DateCreated AS UserProfileDateCreated, 
                        up.ImageUrl AS UserProfileImageUrl
                    FROM Post p 
                        LEFT JOIN UserProfile up ON p.UserProfileId = up.id
                    WHERE p.Title LIKE @Criterion OR p.Caption LIKE @Criterion";

                    if (sortDescending)
                    {
                        sql += " ORDER BY p.DateCreated DESC";
                    }
                    else
                    {
                        sql += " ORDER BY p.DateCreated";
                    }
                  
                    cmd.CommandText = sql;
                    DbUtils.AddParameter(cmd, "@Criterion", $"%{criterion}%");

                    var reader = cmd.ExecuteReader();

                    var posts = new List<Post>();
                    while (reader.Read())
                    {
                        posts.Add(NewPostFromReader(reader));
                    }

                    reader.Close();

                    return posts;
                }
            }
        }
        private Post NewPostFromReader(SqlDataReader reader)
        {
            return new Post()
            {
                
                Id = DbUtils.GetInt(reader, "PostId"),
                Title = DbUtils.GetString(reader, "Title"),
                Caption = DbUtils.GetString(reader, "Caption"),
                DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
                ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),
                UserProfileId = DbUtils.GetInt(reader, "PostUserProfileId"),
                UserProfile = new UserProfile()
                {
                    Name = DbUtils.GetString(reader, "UserProfileName"),
                    Email = DbUtils.GetString(reader, "Email"),
                    DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
                    ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
                },
                Comments = new List<Comment>()
            };
        }
    }
}