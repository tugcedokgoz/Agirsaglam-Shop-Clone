using AgirSaglam.Model;
using AgirSaglam.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            // throw new ApplicationException("test hata");

            List<Comment> items;
            if (!cache.TryGetValue("GetComment", out items))
            {
                items = repo.CommentRepository.FindAll().ToList<Comment>();

                cache.Set("GetComment", items, DateTimeOffset.UtcNow.AddSeconds(20));

                cache.Remove("GetComment");
            }

            return new
            {
                sucess = true,
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

            return Ok(new
            {
                success = true,
                data = comments
            });
        }
    }
}
