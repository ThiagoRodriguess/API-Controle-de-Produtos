using ControleEstoque.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ControleEstoque.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ProdutoController(IConfiguration config)
        {

            _config = config;

        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetAllProduto()
        {
            using var db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var produtos = await db.QueryAsync<Produto>("SELECT * FROM Produto");
            return Ok(produtos);
        }

        [HttpGet("searchById/{ProdutoId}")]
        public async Task<ActionResult<Produto>> GetProduto(int ProdutoId)
        {
            using var db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var produto = await db.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produto WHERE Id = @Id", new { Id = ProdutoId });
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpGet("searchByName/{Name}")]
        public async Task<ActionResult<List<Produto>>> GetProdutoByName(string Name)
        {
            using var db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var produto = await db.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produto WHERE Name = @Name", new { Name = Name });
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpGet("searchByPrice/{Price}")]
        public async Task<ActionResult<List<Produto>>> GetProdutoByPrice(double Price)
        {
            using var db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var produtos = await db.QueryAsync<Produto>("SELECT * FROM Produto WHERE Price = @Price", new { Price = Price });
            if (produtos == null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }

        [HttpGet("searchByQuantity/{Quantity}")]
        public async Task<ActionResult<List<Produto>>> GetProdutoByQuantity(int Quantity)
        {
            using var db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var produtos = await db.QueryAsync<Produto>("SELECT * FROM Produto WHERE Quantity = @Quantity", new { Quantity = Quantity });
            if (produtos == null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Produto>>> CreateProduto(Produto produto)
        {
            using var db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await db.ExecuteAsync("INSERT INTO Produto (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)", new { Name = produto.Name, Price = produto.Price, Quantity = produto.Quantity });
            return Ok(await GetAllProduto(db));
        }

        [HttpPut("{ProdutoId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Produto>>> UpdateProduto(int ProdutoId, Produto produto)
        {
            using var db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var produtoToUpdate = await db.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produto WHERE Id = @Id", new { Id = ProdutoId });
            if (produtoToUpdate == null)
            {
                return NotFound();
            }
            await db.ExecuteAsync("UPDATE Produto SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id", new { Name = produto.Name, Price = produto.Price, Quantity = produto.Quantity, Id = ProdutoId });
            return Ok(await GetAllProduto(db));
        }

        [HttpDelete("{ProdutoId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Produto>>> DeleteProduto(int ProdutoId)
        {
            using var db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var produtoToDelete = await db.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produto WHERE Id = @Id", new { Id = ProdutoId });
            if (produtoToDelete == null)
            {
                return NotFound();
            }
            await db.ExecuteAsync("DELETE FROM Produto WHERE Id = @Id", new { Id = ProdutoId });
            return Ok(await GetAllProduto(db));
        }

        private static async Task<IEnumerable<Produto>> GetAllProduto(SqlConnection db)
        {
            return await db.QueryAsync<Produto>("SELECT * FROM Produto");
        }
    }
}
