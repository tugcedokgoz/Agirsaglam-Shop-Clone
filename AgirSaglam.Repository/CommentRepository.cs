using AgirSaglam.Model.Models;
using AgirSaglam.Model.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class CommentRepository : RepositoryBase<Comment>
    {
        public CommentRepository(RepositoryContext context) : base(context)
        {

        }
        //silme
        public void RemoveComment(int commentId)
        {
            RepositoryContext.Comments.Where(r => r.Id == commentId).ExecuteDelete();
        }

        //kullanici id ye göre yorum getirme
        public async Task<List<Comment>> GetCommentsByUserId(int userId)
        {
            var comments = await RepositoryContext.Comments
                .Where(c => c.UserId == userId)
                .Include(c => c.User)    
                .Include(c => c.Product) 
                .ToListAsync();

            return comments;
        }

        ////ürün id ye göre yorum getirme
        //public async Task<List<Comment>> GetCommentsByProductId(int productId)
        //{
        //    var comments = await RepositoryContext.Comments
        //        .Where(c => c.ProductId == productId)
        //        .ToListAsync();

        //    return comments;
        //}

        ////ürün id ye göre yorum getirme
        public async Task<List<Comment>> GetCommentsByProductId(int productId)
        {
            var comments = await RepositoryContext.Comments
                .Where(c => c.ProductId == productId)
                .Include(c => c.User)    
                .Include(c => c.Product) 
                .ToListAsync();

            return comments;
        }

        public List<V_CommentAdminList> GetCommentAdminList()
        {
            var comments = RepositoryContext.CommentAdminLists
                .Select(c => new V_CommentAdminList
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    ProductId = c.ProductId,
                    Explanation = c.Explanation,
                    Date = c.Date,
                    Point = c.Point,
                    Answer = c.Answer,
                    Status = c.Status,
                    StatusDate = c.StatusDate,
                    ConfirmUserId = c.ConfirmUserId,
                    UserName = RepositoryContext.Users.FirstOrDefault(u => u.Id == c.UserId).UserName,
                    ProductName = RepositoryContext.Products.FirstOrDefault(p => p.Id == c.ProductId).Name
                })
                .ToList();

            return comments;
        }

        public List<V_CommentAdminList> GetCommentByName(string name)
        {
            var comments = RepositoryContext.Comments
                 .Where(r => r.Explanation.Contains(name))
                 .Select(c => new V_CommentAdminList
                 {
                     Id = c.Id,
                     UserId = c.UserId,
                     ProductId = c.ProductId,
                     Explanation = c.Explanation,
                     Date = c.Date,
                     Point = c.Point,
                     Answer = c.Answer,
                     Status = c.Status,
                     StatusDate = c.StatusDate,
                     ConfirmUserId = c.ConfirmUserId,
                     UserName = RepositoryContext.Users.FirstOrDefault(u => u.Id == c.UserId).UserName,
                     ProductName = RepositoryContext.Products.FirstOrDefault(p => p.Id == c.ProductId).Name
                 })
                .ToList();

            return comments;
        }

    }
}
