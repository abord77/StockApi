using LearningApi.Models;

namespace LearningApi.Interfaces {
    public interface ICommentRepository {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
    }
}
