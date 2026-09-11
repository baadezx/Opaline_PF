using Microsoft.EntityFrameworkCore;
using OpalineAPI.Data;
using OpalineAPI.Models;

namespace OpalineAPI.Repositories
{
    // Repositório responsável por operações de persistência relacionadas a Livro (Produto)
    public class ProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos os livros
        public async Task<List<Produto>> GetAll()
        {
            return await _context.Produtos
                .Include(p => p.Categoria) // Inclui a categoria relacionada
                .ToListAsync(); // Lista todos os produtos encontrados
        }

        // Busca um produto pelo Id
        public async Task<Produto?> GetById(Guid id)
        {
            return await _context.Produtos
                .Include(p => p.Categoria) // Inclui a categoria relacionada ao produto
                .FirstOrDefaultAsync(p => p.Id == id); // Puxa o produto com o Id em questão
        }

        /// <summary>
        /// Cria um novo produto no banco de dados
        /// </summary>
        /// <param name="produto">O produto a ser inserido</param>
        /// <returns>O produto criado com seus dados persistidos</returns>
        public async Task<Produto> Post(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        /// <summary>
        /// Atualiza um produto existente no banco de dados
        /// </summary>
        /// <param name="produto">O produto com os dados atualizados</param>
        /// <returns>True se atualizado com sucesso, false caso contrário</returns>
        public async Task<bool> Put(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Remove um produto do banco de dados pelo identificador
        /// </summary>
        /// <param name="id">GUID do produto a ser removido</param>
        /// <returns>True se removido com sucesso, false se produto não encontrado</returns>
        public async Task<bool> Delete(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
