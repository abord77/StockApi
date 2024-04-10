using LearningApi.Data;
using LearningApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace LearningApi.Controllers {
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase {
        private readonly ApplicationDBContext _dbContext;
        public StockController(ApplicationDBContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll() {
            var stock = _dbContext.Stocks.ToList().Select(s => s.ToStockDTO());
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) {
            var stock = _dbContext.Stocks.Find(id);

            if (stock == null) {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }
    }
}
