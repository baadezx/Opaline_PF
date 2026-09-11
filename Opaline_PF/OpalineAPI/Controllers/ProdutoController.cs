// Importa namespaces necessários
using Microsoft.AspNetCore.JsonPatch; // Importa o namespace para manipulação de patches JSON (para atualizações parciais)
using Microsoft.AspNetCore.Mvc; // Importa o namespace para funcionalidades do ASP.NET Core MVC
using OpalineAPI.Models; // Importa o namespace onde estão os modelos de dados (Produto, Categoria, etc.)
using OpalineAPI.Data; // Importa o namespace onde está o contexto do banco de dados (AppDbContext)
using OpalineAPI.Services; // Importa o namespace onde estão os serviços relacionados a produtos

namespace OpalineAPI.Controllers
{
    [Route("api/[controller]")] // Define a rota base do controller como "api/produtos"
    [ApiController] // O atributo [ApiController] habilita recursos automáticos de validação e binding
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _pService; // Contexto do banco de dados (Entity Framework Core)


        public ProdutoController(ProdutoService pService) // Construtor que recebe o serviço via injeção de dependência
        {
            _pService = pService;
        }

        // GET: api/produtos
        // Retorna todos os produtos cadastrados
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _pService.GetAll();
            return Ok(produtos); // Retorna 200 com todos os produtos
        }


        // GET: api/produtos/{id}
        // Busca um produto específico pelo seu Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var produto = await _pService.GetById(id);
            if (produto == null) return NotFound(); // Retorna 404 se não encontrar
            return Ok(produto); // Retorna 200 com o produto
        }


        // POST: api/produtos
        // Cria um novo produto
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _pService.Post(produto); // Persistido pelo serviço
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        // PUT: api/produtos/{id}
        // Atualiza um produto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();
            if (id != produto.Id) return BadRequest("Id da rota diferente do Id do produto.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _pService.Put(produto);
            if (!success) return NotFound(); // Serviço retorna false se o produto não existir ou falhar

            return Ok(produto); // Retorna 200 com o produto
        }


        // DELETE: api/produtos/{id}
        // Remove um produto do banco
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produto = await _pService.GetById(id);
            if (produto == null) return NotFound();

            var deleted = await _pService.Delete(id);
            if (!deleted) return StatusCode(StatusCodes.Status500InternalServerError);
    
            return NoContent();
        }
    }
}
