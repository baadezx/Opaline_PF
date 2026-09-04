// Target framework for the project: .NET 10.0
using Microsoft.AspNetCore.JsonPatch; // Importa o namespace para manipulação de patches JSON (para atualizações parciais)
using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework Core para operações de banco de dados
using OpalineAPI.Models; // Importa o namespace onde estão os modelos de dados (Produto, Categoria, etc.)
using OpalineAPI.Data; // Importa o namespace onde está o contexto do banco de dados (AppDbContext)

namespace OpalineAPI.Services
{
    // Serviço responsável por gerenciar operações relacionadas a produtos
    public class ProdutoService
    {
        private readonly AppDbContext _context; // Contexto do banco de dados (Entity Framework Core)


        public ProdutoService(AppDbContext context) // Construtor que recebe o contexto via injeção de dependência
        {
            _context = context;
        }


        // Retorna todos os produtos
        public async Task<List<Produto>> GetAllAsync()
        {
            return await _context.Produtos // Acessa a tabela de produtos no banco de dados
                .Include(p => p.Categoria) // Inclui dados da categoria relacionada
                .ToListAsync(); // Retorna a lista de produtos
        }


        // Busca um produto pelo Id
        public async Task<Produto?> GetByIdAsync(Guid id)
        {
            return await _context.Produtos // Acessa a tabela de produtos no banco de dados
                .Include(p => p.Categoria) // Inclui dados da categoria relacionada
                .FirstOrDefaultAsync(p => p.Id == id); // Retorna o produto correspondente ao Id ou null se não encontrado
        }


        // Cria um novo produto
        public async Task<Produto> CreateAsync(Produto produto)
        {
            _context.Produtos.Add(produto); // Adiciona o produto ao contexto
            await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados
            return produto; // Retorna o produto criado
        }


        // Atualiza um produto inteiro (PUT)
        public async Task<bool> UpdateAsync(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified; // Marca o objeto como modificado para o EF Core atualizar
            await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados
            return true; // Retorna true indicando sucesso na atualização
        }


        // Atualiza parcialmente um produto (PATCH)
        public async Task<Produto?> PatchAsync(Guid id, JsonPatchDocument<Produto> patchDoc)
        {
            // Busca o produto no banco
            var produto = await _context.Produtos.FindAsync(id); // Busca o produto pelo Id
            if (produto == null) return null; // Retorna null se o produto não for encontrado

            // Aplica as alterações do patch
            patchDoc.ApplyTo(produto);

            // Salva as mudanças
            await _context.SaveChangesAsync();

            return produto; // Retorna o produto atualizado
        }


        // Remove um produto
        public async Task<bool> DeleteAsync(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id); // Busca o produto pelo Id
            if (produto == null) return false; // Retorna false se o produto não for encontrado

            _context.Produtos.Remove(produto); // Remove o produto do contexto
            await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados
            return true; // Retorna true indicando sucesso na exclusão
        }
    }
}
