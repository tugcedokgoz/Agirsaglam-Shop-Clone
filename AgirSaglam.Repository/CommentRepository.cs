using AgirSaglam.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class CommentRepository:RepositoryBase<Comment>
    {
        public CommentRepository(RepositoryContext context):base(context)
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
                .ToListAsync();

            return comments;
        }
    }
}
