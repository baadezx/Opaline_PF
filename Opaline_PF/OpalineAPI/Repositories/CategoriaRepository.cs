using Microsoft.EntityFrameworkCore;
using OpalineAPI.Data;
using OpalineAPI.Models;

namespace OpalineAPI.Repositories
{
    // Repositório responsável por operações de persistência relacionadas a Categoria
    public class CategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todas as categorias
        public async Task<List<Categoria>> GetAllAsync()
        {
            return await _context.Categorias
                .Include(c => c.Produtos) // Inclui os produtos relacionados
                .ToListAsync();
        }

        // Busca uma categoria pelo Id
        public async Task<Categoria?> GetByIdAsync(Guid id)
        {
            return await _context.Categorias
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Cria uma nova categoria
        public async Task<Categoria> CreateAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        // Atualiza uma categoria existente
        public async Task<bool> UpdateAsync(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove uma categoria
        public async Task<bool> DeleteAsync(Guid id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return false;

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
