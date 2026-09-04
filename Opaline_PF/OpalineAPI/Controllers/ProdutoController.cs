// Importa namespaces necessários
using Microsoft.AspNetCore.JsonPatch; // Importa o namespace para manipulação de patches JSON (para atualizações parciais)
using Microsoft.AspNetCore.Mvc; // Importa o namespace para funcionalidades do ASP.NET Core MVC
using OpalineAPI.Models; // Importa o namespace onde estão os modelos de dados (Produto, Categoria, etc.)
using OpalineAPI.Data; // Importa o namespace onde está o contexto do banco de dados (AppDbContext)

namespace OpalineAPI.Controllers
{
    [Route("api/[controller]")] // Define a rota base do controller como "api/produtos"
    [ApiController] // O atributo [ApiController] habilita recursos automáticos de validação e binding
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context; // Contexto do banco de dados (Entity Framework Core)


        public ProdutoController(AppDbContext context) // Construtor que recebe o contexto via injeção de dependência
        {
            _context = context;
        }

        // GET: api/produtos
        // Retorna todos os produtos cadastrados
        [HttpGet]
        public IActionResult GetProdutos() => Ok(_context.Produtos.ToList());


        // GET: api/produtos/{id}
        // Busca um produto específico pelo seu Id
        [HttpGet("{id}")]
        public IActionResult GetProduto(Guid id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null) return NotFound(); // Retorna 404 se não encontrar
            return Ok(produto); // Retorna 200 com o produto
        }


        // POST: api/produtos
        // Cria um novo produto
        [HttpPost]
        public IActionResult PostProduto([FromBody] Produto produto)
        {
            // Verifica se os dados enviados são válidos (baseado nas DataAnnotations)
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Produtos.Add(produto); // Adiciona ao contexto
            _context.SaveChanges(); // Persiste no banco

            // Retorna 201 Created com a rota para acessar o novo produto
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }


        // PUT: api/produtos/{id}
        // Atualiza um produto existente
        [HttpPut("{id}")]
        public IActionResult PutProduto(Guid id, [FromBody] Produto produto)
        {
            if (id != produto.Id) return BadRequest(); // Garante que o Id da URL bate com o do objeto

            // Marca o objeto como modificado para o EF Core atualizar
            _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent(); // Retorna 204 (sem conteúdo), indicando sucesso
        }


        // PATCH: api/produtos/{id}
        // Atualiza parcialmente um produto (somente os campos enviados)
        [HttpPatch("{id}")]
        public IActionResult PatchProduto(Guid id, [FromBody] JsonPatchDocument<Produto> patchDoc)
        {
            // Se não veio nada no corpo da requisição, retorna erro 400
            if (patchDoc == null) return BadRequest();

            // Busca o produto no banco
            var produto = _context.Produtos.Find(id);
            if (produto == null) return NotFound(); // Se não existe, retorna 404

            // Aplica as alterações do patch no objeto encontrado
            patchDoc.ApplyTo(produto, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState); // Aplica o patch e valida os dados

            // Valida se o objeto continua válido após o patch
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Salva as mudanças no banco
            _context.SaveChanges();

            // Retorna o objeto atualizado
            return Ok(produto);
        }


        // DELETE: api/produtos/{id}
        // Remove um produto do banco
        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(Guid id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null) return NotFound(); // Se não existe, retorna 404

            _context.Produtos.Remove(produto); // Remove do contexto
            _context.SaveChanges(); // Persiste a exclusão

            return NoContent(); // Retorna 204, indicando que foi removido
        }
    }
}
