using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Entities;

namespace Interfaces.Database.Repositories
{
    public interface IPostRepository : IRepository<IPost>
    {
        // Specific Post Repository features.
        bool GetByIdIfExists(int id, out IPost result);

        void RemovePost(IPost post);

        IEnumerable<IPost> GetAllPosts();

        IEnumerable<IPost> GetAllPostsFromTrip(int tripId);

        void CreateAndAddNewPost(string title, string body, string photoFile, int tripId);
    }
}
