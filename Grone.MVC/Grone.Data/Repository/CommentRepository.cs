﻿using Grone.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grone.Data.Models;

namespace Grone.Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public void Add(CommentEntityModel comment)
        {
            using (var context = new GroneEntities())
            {
                var newComment = new CommentEntityModel(comment.Id)
                {
                    ImgSrc = comment.ImgSrc,
                    PostEntityModelId = comment.PostEntityModelId,
                    CommentId = comment.CommentId, //commenting on anotehr comment
                    MemberId = comment.MemberId,
                    Comment = comment.Comment
                };
                // finding the post commented on and adding more total time to it.

                var postCommentedOn = context.Posts.FirstOrDefault(p => p.Id == newComment.PostEntityModelId);

                postCommentedOn.TimeLeft += 60;

                postCommentedOn.TotalTimeAdded += 60;

                context.Comments.Add(newComment);

                context.SaveChanges();
            }
        }
        
        public void Delete(string id)
        {
            using (var context = new GroneEntities())
            {
                var commentToDelete = context.Comments
                    .FirstOrDefault(c => c.Id == Guid.Parse(id));

                context.Comments.Remove(commentToDelete);

                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new GroneEntities())
            {
                var commentToDelete = context.Comments
                    .FirstOrDefault(c => c.Id == id);

                context.Comments.Remove(commentToDelete);

                context.SaveChanges();
            }
        }

        public CommentEntityModel GetByCommentId(string id)
        {
            using (var context = new GroneEntities())
            {
                return context.Comments.FirstOrDefault(c => c.CommentId == Guid.Parse(id));
            }
        }

        public CommentEntityModel GetByCommentId(Guid id)
        {
            using (var context = new GroneEntities())
            {
                return context.Comments.FirstOrDefault(c => c.CommentId == id);
            }
        }

        public CommentEntityModel GetById(string id)
        {
            using (var context = new GroneEntities())
            {
                return context.Comments.FirstOrDefault(c => c.Id == Guid.Parse(id));
            }
        }

        public CommentEntityModel GetById(Guid id)
        {
            using (var context = new GroneEntities())
            {
                return context.Comments.FirstOrDefault(c => c.Id == id);
            }
        }

        public IEnumerable<CommentEntityModel> GetByPostId(string id)
        {
            using (var context = new GroneEntities())
            {
                return context.Comments.Where(c => c.PostEntityModelId == Guid.Parse(id)).ToList();
            }
        }

        public IEnumerable<CommentEntityModel> GetByPostId(Guid id)
        {
            using (var context = new GroneEntities())
            {
                return context.Comments.Where(c => c.PostEntityModelId == id).ToList();
            }
        }

        public void Update(CommentEntityModel model) {
            using (var context = new GroneEntities())
            {
                var entity = context.Comments.FirstOrDefault(x => x.Id == model.Id);

                entity.ImgSrc = model.ImgSrc;
                entity.Comment = model.Comment;

                context.SaveChanges();

            }
        }
     
    }
}
