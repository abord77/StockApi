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
    }
}
