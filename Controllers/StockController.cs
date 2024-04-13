using LearningApi.Data;
using LearningApi.Mappers;
using LearningApi.DTOs.Stocks;
using Microsoft.AspNetCore.Mvc;
using LearningApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningApi.Controllers {
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase { // changing all endpoints to async is ep 9
                                                    // note: anyuthing that needs to go out and find other information should be wrapped in await (ex. anything in this controller going to the db should be async)
        private readonly ApplicationDBContext _dbContext;
        public StockController(ApplicationDBContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() { // ep 4
            var stock = await _dbContext.Stocks.ToListAsync();
            var stockDto = stock.Select(s => s.ToStockDTO());
            return Ok(stock);
        }
         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) { // ep 4
            var stock = await _dbContext.Stocks.FindAsync(id);

            if (stock == null) {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEntry([FromBody] CreateStockRequestDto stockDto) { // ep 6
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _dbContext.Stocks.AddAsync(stockModel);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) { // ep 7
            var stockModel = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null) {
                return NotFound();
            }


            // this takes the payload to this endpoint and modifies the id using that body
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            await _dbContext.SaveChangesAsync();
            return Ok(stockModel.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) { // ep 8
            var stockModel = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null) {
                return NotFound();
            }

            _dbContext.Stocks.Remove(stockModel); // delete is not an asynchronous function since it is simply marking it for deletion, saving the change is a real I/O operation
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
