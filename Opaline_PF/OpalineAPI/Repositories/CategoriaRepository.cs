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
        public async Task<List<Categoria>> GetAll()
        {
            return await _context.Categorias
                .ToListAsync();
        }

        // Busca uma categoria pelo Id
        public async Task<Categoria?> GetById(Guid id)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Cria uma nova categoria
        public async Task<Categoria> Post(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        // Atualiza uma categoria existente
        public async Task<bool> Put(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove uma categoria
        public async Task<bool> Delete(Guid id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return false;

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
