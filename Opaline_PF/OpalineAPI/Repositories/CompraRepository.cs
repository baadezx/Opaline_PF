using Microsoft.EntityFrameworkCore;
using OpalineAPI.Data;
using OpalineAPI.Models;

namespace OpalineAPI.Repositories
{
    public class CompraRepository
    {
        private readonly AppDbContext _context;

        public CompraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Compra>> GetAllAsync()
        {
            return await _context.Compras
                .ToListAsync();
        }

        public async Task<Compra?> GetByIdAsync(Guid id)
        {
            return await _context.Compras
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Compra> CreateAsync(Compra compra)
        {
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();
            return compra;
        }

        public async Task<Compra?> UpdateAsync(Guid id, Compra compra)
        {
            var existente = await _context.Compras.FindAsync(id);
            if (existente == null) return null;

            existente.ProdutoId = compra.ProdutoId;
            existente.MetodoPagamento = compra.MetodoPagamento;
            existente.DataCompra = compra.DataCompra;
            existente.DataEntrega = compra.DataEntrega;
            existente.DataEntregue = compra.DataEntregue;
            existente.DataCancelamento = compra.DataCancelamento;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<Compra?> MarcarComoEntregueAsync(Guid id)
        {
            var existente = await _context.Compras.FindAsync(id);
            if (existente == null) return null;

            existente.DataEntregue = DateTime.Now.Date;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<Compra?> MarcarComoCanceladoAsync(Guid id)
        {
            var existente = await _context.Compras.FindAsync(id);
            if (existente == null) return null;

            existente.DataCancelamento = DateTime.Now.Date;

            await _context.SaveChangesAsync();
            return existente;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var existente = await _context.Compras.FindAsync(id);
            if (existente == null) return false;

            _context.Compras.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
