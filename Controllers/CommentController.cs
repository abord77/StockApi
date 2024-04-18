﻿using LearningApi.Interfaces;
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
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDTO());
            return Ok(commentDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) { // episode 13
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null) {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDTO());
        }

        [HttpPost]
        [Route("{stockId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto) { // episode 14
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (!await _stockRepo.StockExists(stockId)) {
                return BadRequest("Stock does not exist");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto updateDto) { // episode 15
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var commentModel = await _commentRepo.UpdateAsync(id, updateDto);

            if (commentModel == null) {
                return NotFound("Comment not found");
            }

            return Ok(commentModel.ToCommentDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) { // episode 16
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var commentModel = await _commentRepo.DeleteAsync(id);

            if (commentModel == null) {
                return NotFound("Comment not found");
            }

            return Ok(commentModel.ToCommentDTO()); // NoContent() is also fine here just like in the StockController.cs
        }
    }
}
