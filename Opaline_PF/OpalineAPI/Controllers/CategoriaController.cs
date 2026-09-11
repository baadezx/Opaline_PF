using Microsoft.AspNetCore.Mvc; // Importa o namespace para funcionalidades do ASP.NET Core MVC
using OpalineAPI.Models; // Importa o namespace onde estão os modelos de dados (Categoria, Produto, etc.)
using Microsoft.AspNetCore.JsonPatch; // Importa o namespace para manipulação de patches JSON (para atualizações parciais)
using OpalineAPI.Services; // Importa o namespace onde está o serviço de categorias

namespace OpalineAPI.Controllers
{
    [Route("api/[Controller]")] // Define a rota base do controller como "api/categoria"
    [ApiController] // O atributo [ApiController] habilita recursos automáticos de validação e binding]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _cService; // Serviço de categorias


        public CategoriaController(CategoriaService cService) // Construtor que recebe o serviço via injeção de dependência
        {
            _cService = cService;
        }


        // GET: api/categorias
        // Retorna todas as categorias cadastradas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _cService.GetAll();
            return Ok(categorias); // Retorna 200 com todas as categorias
        }


        // GET: api/categorias/{id}
        // Busca uma categoria específica pelo seu Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var categoria = await _cService.GetById(id);
            if (categoria == null) return NotFound(); // Retorna 404 se não encontrar
            return Ok(categoria); // Retorna 200 com a categoria
        }


        // POST: api/categorias
        // Cria uma nova categoria
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _cService.Post(categoria); // Persistido pelo serviço
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        // PUT: api/categorias/{id}
        // Atualiza uma categoria inteira (todos os campos)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _cService.Put(categoria);
            if (!updated) return NotFound(); // ou BadRequest() dependendo da semântica do serviço

            return NoContent(); // retorna 204 quando atualizado com sucesso
        }


        // DELETE: api/categorias/{id}
        // Remove uma categoria do banco
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _cService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
