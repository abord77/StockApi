using LearningApi.Data;
using LearningApi.Interfaces;
using LearningApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningApi.Repository {
    public class StockRepository : IStockRepository { // leverages repository pattern from ep 10

        private readonly ApplicationDBContext _dbContext;
        public StockRepository(ApplicationDBContext dbContext) { // dependency injection is ctor based
            _dbContext = dbContext;
        }

        public Task<List<Stock>> GetAllAsync() {
            return _dbContext.Stocks.ToListAsync();
        }
    }
}
