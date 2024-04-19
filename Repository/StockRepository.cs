using LearningApi.Data;
using LearningApi.DTOs.Stocks;
using LearningApi.Helpers;
using LearningApi.Interfaces;
using LearningApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningApi.Repository {
    public class StockRepository : IStockRepository { // leverages repository pattern from ep 10

        private readonly ApplicationDBContext _dbContext;
        public StockRepository(ApplicationDBContext dbContext) { // dependency injection is ctor based
            _dbContext = dbContext;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query) {
            var stocks = _dbContext.Stocks.Include(c => c.Comments).AsQueryable(); // rewriting this method to perform a query

            if (!string.IsNullOrWhiteSpace(query.CompanyName)) {
                stocks = stocks.Where(x => x.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol)) {
                stocks = stocks.Where(x => x.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy)) {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase)) {
                    stocks = query.IsDescending ? stocks.OrderByDescending(x => x.Symbol) : stocks.OrderBy(x => x.Symbol);
                }

                if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase)) {
                    stocks = query.IsDescending ? stocks.OrderByDescending(x => x.CompanyName) : stocks.OrderBy(x => x.CompanyName);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id) {
            return await _dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto) {
            var existingStock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null) {
                return null;
            }

            // this takes the payload to this endpoint and modifies the id using that body
            //existingStock.Symbol = stockDto.Symbol;
            //existingStock.CompanyName = stockDto.CompanyName;
            //existingStock.Purchase = stockDto.Purchase;
            //existingStock.LastDiv = stockDto.LastDiv;
            //existingStock.Industry = stockDto.Industry;
            //existingStock.MarketCap = stockDto.MarketCap;
            _dbContext.Entry(existingStock).CurrentValues.SetValues(stockDto); // this line does exactly the top line but more concisely

            await _dbContext.SaveChangesAsync();
            return existingStock;
        }

        public async Task<Stock> CreateAsync(Stock stockModel) {
            await _dbContext.Stocks.AddAsync(stockModel);
            await _dbContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id) {
            var stockModel = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null) {
                return null;
            }

            _dbContext.Stocks.Remove(stockModel); // delete is not an asynchronous function since it is simply marking it for deletion, saving the change is a real I/O operation
            await _dbContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<bool> StockExists(int id) {
            return await _dbContext.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}
