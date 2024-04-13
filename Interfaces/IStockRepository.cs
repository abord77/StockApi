using LearningApi.Models;

namespace LearningApi.Interfaces {
    public interface IStockRepository { // defind this inferface for the repository pattern which is about abstracting away data access
        Task<List<Stock>> GetAllAsync();
    }
}
