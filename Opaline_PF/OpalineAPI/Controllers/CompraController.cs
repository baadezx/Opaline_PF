using Microsoft.AspNetCore.Mvc;
using OpalineAPI.Models;
using OpalineAPI.Services;

namespace OpalineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : ControllerBase
    {
        private readonly CompraService _compraService;

        public CompraController(CompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var compras = await _compraService.GetAllAsync();
            return Ok(compras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var compra = await _compraService.GetByIdAsync(id);
            if (compra == null) return NotFound();
            return Ok(compra);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Compra compra)
        {
            var novaCompra = await _compraService.CreateAsync(compra);
            return CreatedAtAction(nameof(GetById), new { id = novaCompra.Id }, novaCompra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Compra compra)
        {
            var atualizada = await _compraService.UpdateAsync(id, compra);
            if (atualizada == null) return NotFound();
            return Ok(atualizada);
        }

        [HttpPut("{id}/entregue")]
        public async Task<IActionResult> MarcarComoEntregue(Guid id)
        {
            var compra = await _compraService.MarcarComoEntregueAsync(id);
            if (compra == null) return NotFound();

            return Ok(compra);
        }

        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> MarcarComoCancelado(Guid id)
        {
            var compra = await _compraService.MarcarComoCanceladoAsync(id);
            if (compra == null) return NotFound();

            return Ok(compra);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removida = await _compraService.DeleteAsync(id);
            if (!removida) return NotFound();
            return Ok();
        }
    }
}

