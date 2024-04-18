using LearningApi.Data;
using LearningApi.DTOs.Comments;
using LearningApi.DTOs.Stocks;
using LearningApi.Interfaces;
using LearningApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningApi.Repository {
    public class CommentRepository : ICommentRepository {
        private readonly ApplicationDBContext _dbContext;

        public CommentRepository(ApplicationDBContext dbContext) { // dependency injection is ctor based
            _dbContext = dbContext;
        }

        public async Task<List<Comment>> GetAllAsync() {
            return await _dbContext.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id) {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (comment == null) {
                return null;
            }

            return comment;
        }

        public async Task<Comment> CreateAsync(Comment commentModel) {
            await _dbContext.Comments.AddAsync(commentModel);
            await _dbContext.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto updateDto) {
            var existingComment = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (existingComment == null) {
                return null;
            }

            _dbContext.Entry(existingComment).CurrentValues.SetValues(updateDto);

            await _dbContext.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comment?> DeleteAsync(int id) {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (comment == null) {
                return null;
            }

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> CommentExists(int id) {
            return await _dbContext.Comments.AnyAsync(c => c.Id == id);
        }
    }
}
