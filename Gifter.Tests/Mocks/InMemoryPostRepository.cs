using System;
using System.Collections.Generic;
using System.Linq;
using Gifter.Models;
using Gifter.Repositories;

namespace Gifter.Tests.Mocks
{
    class InMemoryPostRepository : IPostRepository
    {
        private readonly List<Post> _data;

        public List<Post> InternalData
        {
            get
            {
                return _data;
            }
        }

        public InMemoryPostRepository(List<Post> startingData)
        {
            _data = startingData;
        }

        public void Add(Post post)
        {
            var lastPost = _data.Last();
            post.Id = lastPost.Id + 1;
            _data.Add(post);
        }

        public void Delete(int id)
        {
            var postTodelete = _data.FirstOrDefault(p => p.Id == id);
            if (postTodelete == null)
            {
                return;
            }

            _data.Remove(postTodelete);
        }

        public List<Post> GetAll()
        {
            return _data;
        }

        public Post GetById(int id)
        {
            return _data.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Post post)
        {
            var currentPost = _data.FirstOrDefault(p => p.Id == post.Id);
            if (currentPost == null)
            {
                return;
            }

            currentPost.Caption = post.Caption;
            currentPost.Title = post.Title;
            currentPost.DateCreated = post.DateCreated;
            currentPost.ImageUrl = post.ImageUrl;
            currentPost.UserProfileId = post.UserProfileId;
        }

        public List<Post> Search(string criterion, bool sortDescending)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllWithComments()
        {
            throw new NotImplementedException();
        }

        public Post GetPostIdWithComments(int id)
        {
            throw new NotImplementedException();
        }
    }
}
