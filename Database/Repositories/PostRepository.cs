﻿using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Entities;
using Interfaces.Database.Repositories;
using Database.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DbContext _context;

        private DatabaseContext Context { get => (DatabaseContext)_context; }


        public PostRepository(DbContext context)
        {
            _context = context;
        }

        public bool Add(IPost entity)
        {
            Context.Add((Post)entity);

            return true;
        }

        public bool GetByIdIfExists(int id, out IPost result)
        {
            bool idExists;

            idExists = Context.Posts.Any(r => r.Id == id);

            if (!idExists) // If Doesn't exist
            {
                result = null;
                return false;
            }

            result = Context.Posts.First(r => r.Id == id);

            return true;
        }

        public void RemovePost(IPost post)
        {
            Context.Remove(post);
        }

        public IEnumerable<IPost> GetAllPosts()
        {
            return Context.Posts.ToList();
        }

        public void CreateAndAddNewPost(string title, string body, string photoFile, int tripId)
        {
            var newPost = new Post()
            {
                Title = title,
                BodyText = body,
                PhotoFileName = photoFile,
                TripId = tripId
            };

            Context.Posts.Add(newPost);
        }
    }
}