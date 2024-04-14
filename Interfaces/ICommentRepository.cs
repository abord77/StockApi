using LearningApi.Models;

namespace LearningApi.Interfaces {
    public interface ICommentRepository {
        Task<List<Comment>> GetAllAsync();
    }
}
