﻿using Grone.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grone.Data.Models;

namespace Grone.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        //GroneEntities _context = new GroneEntities();

        public void AddOrUpdate(PostEntityModel post)
        {
            using (var _context = new GroneEntities())
            {
                if (_context.Posts.FirstOrDefault(p => p.Id == post.Id) == null)
                {
                    var newPost = new PostEntityModel()
                    {
                        Description = post.Description,
                        ImgSrc = post.ImgSrc,
                        TimeAdded = 0,
                        TotalAdded = 0,
                        TimeLeft = 120,
                        MemberId = (Guid.NewGuid()).ToString(),
                        Title = post.Title,
                        Uploaded = DateTime.Now
                    };

                    _context.Posts.Add(newPost);

                    _context.SaveChanges();
                }
                else
                {
                    var postToUpdate = _context.Posts.FirstOrDefault();
                    postToUpdate.Description = post.Description;
                    postToUpdate.ImgSrc = post.ImgSrc;
                    postToUpdate.Title = post.Title;

                    _context.SaveChanges();
                }

            }
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostEntityModel> GetAll()
        {
            using (var _context = new GroneEntities())
            {
                return _context.Posts.ToList();
            }
        }

        public PostEntityModel GetById(string id)
        {
            using (var context = new GroneEntities())
            {
                return context.Posts.Include("Comments").FirstOrDefault(p => p.Id == Guid.Parse(id));
            }
        }

        public PostEntityModel GetById(Guid id)
        {
            using (var context = new GroneEntities())
            {
                return context.Posts.Include("Comments").FirstOrDefault(p => p.Id == id);
            }
        }
    }
}
