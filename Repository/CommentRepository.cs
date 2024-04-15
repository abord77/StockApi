using LearningApi.Data;
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
    }
}
