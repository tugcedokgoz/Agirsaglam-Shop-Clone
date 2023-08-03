using AgirSaglam.Model;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseController
    {
        public CommentController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        //tüm yorumları getirme
        [HttpGet("GetComment")]
        public dynamic GetComment()
        {
            var items = cache.GetOrCreate("GetComment", entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
                return repo.CommentRepository.FindAll()
                    .Include(c => c.User)
                    .Include(c => c.Product)
                    .Select(c => new
                    {
                        c.Id,
                        c.UserId,
                        c.ProductId,
                        c.Explanation,
                        c.Date,
                        c.Point,
                        c.Answer,
                        c.Status,
                        c.StatusDate,
                        c.ConfirmUserId,
                        User = new
                        {
                            c.User.Id,
                            c.User.UserName,
                        },
                        Product = new
                        {
                            c.Product.Id,
                            c.Product.Name,
                        }
                    })
                    .ToList();
            });

            return new
            {
                success = true,
                data = items
            };
        }


        //kaydetme-update

        [HttpPost("Save")]

        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Comment item = new Comment()
            {
                Id = json.Id,
                UserId = json.UserId,
                ProductId = json.ProductId,
                Explanation = json.Explanation,
                Date = json.Date,
                Point = json.Point,
                Answer = json.Answer,
                Status = json.Status,
                StatusDate = json.StatusDate,
                ConfirmUserId = json.ConfirmUserId,

            };
            if (item.Id > 0)
                repo.CommentRepository.Update(item);
            else
                repo.CommentRepository.Create(item);
            repo.SaveChanges();
            cache.Remove("Comment");
            return new
            {
                success = true
            };
        }


        //silme

        [HttpPost("Delete")]
        public dynamic Delete(int id)
        {
            if (id < 0)
                return new
                {
                    success = false,
                    message = "Invalid Id"
                };
            repo.CommentRepository.RemoveComment(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }

        //kullanici İd ye göre yorum getirme
        [HttpGet("GetCommentsByUserId/{userId}")]
        public async Task<IActionResult> GetCommentsByUserId(int userId)
        {

            var comments = await repo.CommentRepository.GetCommentsByUserId(userId);

            if (comments == null || comments.Count == 0)
            {
                return NotFound();
            }

            var commentsWithRelatedProperties = comments.Select(c => new
            {
                c.Id,
                c.UserId,
                c.ProductId,
                c.Explanation,
                c.Date,
                c.Point,
                c.Answer,
                c.Status,
                c.StatusDate,
                c.ConfirmUserId,
                User = c.User != null ? new
                {
                    c.User.Id,
                    c.User.UserName,
                    // Diğer User özelliklerini ekleyin
                    // Örneğin: c.User.FirstName, c.User.LastName, vs.
                } : null,
                Product = c.Product != null ? new
                {
                    c.Product.Id,
                    c.Product.Name,
                    // Diğer Product özelliklerini ekleyin
                    // Örneğin: c.Product.Description, c.Product.Price, vs.
                } : null
            });

            return Ok(new
            {
                success = true,
                data = commentsWithRelatedProperties
            });
        }



        [HttpGet("GetCommentsByProductId/{productId}")]
        public async Task<IActionResult> GetCommentsByProductId(int productId)
        {
            var comments = await repo.CommentRepository.GetCommentsByProductId(productId);

            if (comments == null || comments.Count == 0)
            {
                return NotFound();
            }

            var commentsWithRelatedProperties = comments.Select(c => new
            {
                c.Id,
                c.UserId,
                c.ProductId,
                c.Explanation,
                c.Date,
                c.Point,
                c.Answer,
                c.Status,
                c.StatusDate,
                c.ConfirmUserId,
                User = c.User != null ? new
                {
                    c.User.Id,
                    c.User.UserName,
                    // Diğer User özelliklerini ekleyin
                    // Örneğin: c.User.FirstName, c.User.LastName, vs.
                } : null,
                Product = c.Product != null ? new
                {
                    c.Product.Id,
                    c.Product.Name,
                    // Diğer Product özelliklerini ekleyin
                    // Örneğin: c.Product.Description, c.Product.Price, vs.
                } : null
            });

            return Ok(new
            {
                success = true,
                data = commentsWithRelatedProperties
            });
        }



    }
}
