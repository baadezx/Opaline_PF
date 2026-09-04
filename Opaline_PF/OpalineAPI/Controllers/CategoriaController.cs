using Microsoft.AspNetCore.Mvc; // Importa o namespace para funcionalidades do ASP.NET Core MVC
using OpalineAPI.Models; // Importa o namespace onde estão os modelos de dados (Categoria, Produto, etc.)
using Microsoft.AspNetCore.JsonPatch; // Importa o namespace para manipulação de patches JSON (para atualizações parciais)
using OpalineAPI.Data; // Importa o namespace onde está o contexto do banco de dados (AppDbContext)

namespace OpalineAPI.Controllers
{
    [Route("api/[Controller]")] // Define a rota base do controller como "api/categoria"
    [ApiController] // O atributo [ApiController] habilita recursos automáticos de validação e binding]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context; // Contexto do banco de dados (Entity Framework Core)


        public CategoriaController(AppDbContext context) // Construtor que recebe o contexto via injeção de dependência
        {
            _context = context;
        }


        // GET: api/categorias
        // Retorna todas as categorias cadastradas
        [HttpGet]
        public IActionResult GetCategorias() => Ok(_context.Categorias.ToList());


        // GET: api/categorias/{id}
        // Busca uma categoria específica pelo seu Id
        [HttpGet("{id}")]
        public IActionResult GetCategoria(Guid id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound(); // Retorna 404 se não encontrar
            return Ok(categoria); // Retorna 200 com a categoria
        }


        // POST: api/categorias
        // Cria uma nova categoria
        [HttpPost]
        public IActionResult PostCategoria([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Valida os dados enviados com base nas DataAnnotations

            _context.Categorias.Add(categoria); // Adiciona a categoria ao contexto
            _context.SaveChanges(); // Salva as alterações no banco de dados

            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria); // Retorna 201 com a localização/rota da nova categoria
        }


        // PUT: api/categorias/{id}
        // Atualiza uma categoria inteira (todos os campos)
        [HttpPut("{id}")]
        public IActionResult PutCategoria(Guid id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id) return BadRequest(); // Garante que o Id da URL bate com o objeto

            // Marca o objeto como modificado para o EF Core atualizar
            _context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent(); // Retorna 204 indicando sucesso
        }


        // PATCH: api/categorias/{id}
        // Atualiza parcialmente uma categoria (somente os campos enviados)
        [HttpPatch("{id}")]
        public IActionResult PatchCategoria(Guid id, [FromBody] JsonPatchDocument<Categoria> patchDoc)
        {
            if (patchDoc == null) return BadRequest(); // Se não veio nada, retorna 400

            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound(); // Se não existe, retorna 404

            // Aplica as alterações do patch no objeto encontrado
            patchDoc.ApplyTo(categoria, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState); // Aplica o patch e valida o modelo

            // Valida se o objeto continua válido após o patch
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.SaveChanges(); // Persiste as mudanças

            return Ok(categoria); // Retorna a categoria atualizada
        }


        // DELETE: api/categorias/{id}
        // Remove uma categoria do banco
        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria(Guid id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound(); // Se não existe, retorna 404

            _context.Categorias.Remove(categoria); // Remove do contexto
            _context.SaveChanges(); // Persiste a exclusão

            return NoContent(); // Retorna 204 indicando que foi removida
        }
    }
}
