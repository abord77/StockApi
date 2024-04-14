using LearningApi.Interfaces;
using LearningApi.Models;
using Microsoft.AspNetCore.Mvc;
using LearningApi.Mappers;

namespace LearningApi.Controllers {
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo) {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDTO());
            return Ok(commentDto);
        }
    }
}
