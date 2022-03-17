using Gifter.Models;
using System.Collections.Generic;

namespace Gifter.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile user);
        void Delete(int id);
        List<UserProfile> GetAll();
        UserProfile GetByEmail(string email);
        UserProfile GetById(int id);
        UserProfile GetUserProfileIdWithPostsAndComments(int id);
        void Update(UserProfile user);
    }
}