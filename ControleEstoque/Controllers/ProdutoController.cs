using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ControleEstoque.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{ProdutoId}")]
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

        [HttpPost]
        public async Task<ActionResult<List<Produto>>> CreateProduto(Produto produto)
        {
            using var db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await db.ExecuteAsync("INSERT INTO Produto (Name, Preco, Quantidade) VALUES (@Name, @Preco, @Quantidade)", new { Name = produto.Name, Preco = produto.Preco, Quantidade = produto.Quantidade });
            return Ok(await GetAllProduto(db));
        }

        [HttpPut("{ProdutoId}")]
        public async Task<ActionResult<List<Produto>>> UpdateProduto(int ProdutoId, Produto produto)
        {
            using var db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var produtoToUpdate = await db.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produto WHERE Id = @Id", new { Id = ProdutoId });
            if (produtoToUpdate == null)
            {
                return NotFound();
            }
            await db.ExecuteAsync("UPDATE Produto SET Name = @Name, Preco = @Preco, Quantidade = @Quantidade WHERE Id = @Id", new { Name = produto.Name, Preco = produto.Preco, Quantidade = produto.Quantidade, Id = ProdutoId });
            return Ok(await GetAllProduto(db));
        }

        [HttpDelete("{ProdutoId}")]
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
