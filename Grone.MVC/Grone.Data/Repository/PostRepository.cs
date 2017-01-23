using Grone.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grone.Data.Models;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Grone.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        //GroneEntities _context = new GroneEntities();
        // todo: ta bort bild när post tas bort av admin
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
                        MemberId = post.MemberId,
                        Title = post.Title,
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
            using (var context = new GroneEntities())
            {
                //remove all the comments for the current post
                List<Guid> commentIds = new List<Guid>();

                foreach (var post in context.Posts.Include("Comments"))
                {
                    foreach (var comment in post.Comments)
                    {
                        commentIds.Add(comment.Id);
                    }
                }

                foreach (var commentId in commentIds)
                {
                    var commentToRemove = context.Comments.FirstOrDefault(c => c.Id == commentId);

                    context.Comments.Remove(commentToRemove);
                }

                //remove the post
                var postToRemove = context.Posts.FirstOrDefault(p => p.Id == Guid.Parse(id));

                context.Posts.Remove(postToRemove);

                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new GroneEntities())
            {
                //remove all the comments for the current post
                List<Guid> commentIds = new List<Guid>();

                foreach (var post in context.Posts.Include("Comments"))
                {
                    foreach (var comment in post.Comments)
                    {
                        commentIds.Add(comment.Id);
                    }
                }

                foreach (var commentId in commentIds)
                {
                    var commentToRemove = context.Comments.FirstOrDefault(c => c.Id == commentId);

                    context.Comments.Remove(commentToRemove);
                }

                //remove the post
                var postToRemove = context.Posts.FirstOrDefault(p => p.Id == id);

                context.Posts.Remove(postToRemove);

                context.SaveChanges();
            }
        }

        public IEnumerable<PostEntityModel> GetAll()
        {
            using (var _context = new GroneEntities())
            {
                return _context.Posts.OrderByDescending(p => p.Uploaded).ToList();
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

        public IEnumerable<CommentEntityModel> GetTop3Comments(PostEntityModel post)
        {
            using (var context = new GroneEntities())
            {
                return context.Comments
                    .Where(c => c.PostEntityModelId == post.Id)
                    .OrderByDescending(c => c.Uploaded)
                    .ToList()
                    .Take(3);
            }
        }

        public IEnumerable<CommentEntityModel> GetComments(PostEntityModel post)
        {
            using (var context = new GroneEntities())
            {
                return context.Comments
                    .Where(c => c.PostEntityModelId == post.Id)
                    .OrderByDescending(c => c.Uploaded)
                    .ToList();
            }
        }

        public void RemoveOneFromEveryPost()
        {
            //do
            //{
            //    using (var context = new GroneEntities())
            //    {
            //        foreach (var post in context.Posts)
            //        {
            //            if (post.TimeLeft > 0)
            //                post.TimeLeft -= 1;
            //            else
            //            {
            //                //remove file from img folder
                          

            //                //remove post itself
            //                context.Posts.Remove(post);

            //                context.SaveChanges();
            //            }
            //        }

            //        context.SaveChanges();

            //        Thread.Sleep(60000);
            //    }
            //} while (true);
        }
    }
}
