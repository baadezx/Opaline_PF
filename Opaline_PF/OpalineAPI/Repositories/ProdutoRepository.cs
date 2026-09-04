using Microsoft.EntityFrameworkCore;
using OpalineAPI.Data;
using OpalineAPI.Models;

namespace OpalineAPI.Repositories
{
    // Repositório responsável por operações de persistência relacionadas a Livro (Produto)
    public class LivroRepository
    {
        private readonly AppDbContext _context;

        public LivroRepository(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos os livros
        public async Task<List<Produto>> GetAllAsync()
        {
            return await _context.Produtos
                .Include(p => p.Categoria) // Inclui a categoria relacionada
                .ToListAsync();
        }

        // Busca um livro pelo Id
        public async Task<Produto?> GetByIdAsync(Guid id)
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Cria um novo livro
        public async Task<Produto> CreateAsync(Produto livro)
        {
            _context.Produtos.Add(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        // Atualiza um livro existente
        public async Task<bool> UpdateAsync(Produto livro)
        {
            _context.Entry(livro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove um livro
        public async Task<bool> DeleteAsync(Guid id)
        {
            var livro = await _context.Produtos.FindAsync(id);
            if (livro == null) return false;

            _context.Produtos.Remove(livro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
