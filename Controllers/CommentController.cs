using LearningApi.Interfaces;
using LearningApi.Models;
using Microsoft.AspNetCore.Mvc;
using LearningApi.Mappers;
using LearningApi.DTOs.Comments;

namespace LearningApi.Controllers {
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo) {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() { // episode 13
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDTO());
            return Ok(commentDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) { // episode 13
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null) {
                return NotFound();
            }

            return Ok(comment.ToCommentDTO());
        }

        [HttpPost]
        [Route("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto) { // episode 14
            if (!await _stockRepo.StockExists(stockId)) {
                return BadRequest("Stock does not exist");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel);
        }
    }
}
