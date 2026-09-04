// Target framework for the project: .NET 10.0
using Microsoft.AspNetCore.JsonPatch; // Para atualizações parciais (PATCH)
using Microsoft.EntityFrameworkCore; // Para operações com o Entity Framework Core
using OpalineAPI.Models; // Modelos de dados (Categoria, Produto, etc.)
using OpalineAPI.Data; // Contexto do banco de dados (AppDbContext)

namespace OpalineAPI.Services
{
    // Serviço responsável por gerenciar operações relacionadas a categorias
    public class CategoriaService
    {
        private readonly AppDbContext _context; // Contexto do banco de dados

        public CategoriaService(AppDbContext context) // Construtor com injeção de dependência
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
                .Include(c => c.Produtos) // Inclui os produtos relacionados
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Cria uma nova categoria
        public async Task<Categoria> CreateAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        // Atualiza uma categoria inteira (PUT)
        public async Task<bool> UpdateAsync(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // Atualiza parcialmente uma categoria (PATCH)
        public async Task<Categoria?> PatchAsync(Guid id, JsonPatchDocument<Categoria> patchDoc)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return null;

            patchDoc.ApplyTo(categoria);
            await _context.SaveChangesAsync();

            return categoria;
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
