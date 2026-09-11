// Target framework for the project: .NET 10.0
using Microsoft.AspNetCore.JsonPatch; // Importa o namespace para manipulação de patches JSON (para atualizações parciais)
using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework Core para operações de banco de dados
using OpalineAPI.Models; // Importa o namespace onde estão os modelos de dados (Produto, Categoria, etc.)

using OpalineAPI.Repositories; // Importa o namespace onde estão os repositórios relacionados a produtos

namespace OpalineAPI.Services
{
    // Serviço responsável por gerenciar operações relacionadas a produtos
    public class ProdutoService
    {
        private readonly ProdutoRepository _pRepository; // Contexto do banco de dados (Entity Framework Core)


        public ProdutoService(ProdutoRepository pRepository) // Construtor que recebe o contexto via injeção de dependência
        {
            _pRepository = pRepository;
        }


        // Retorna todos os produtos
        public async Task<List<Produto>> GetAll()
        {
            return await _pRepository.GetAll(); // Chama o método GetAll do repositório
        }


        // Busca um produto pelo Id
        public async Task<Produto?> GetById(Guid id)
        {
            return await _pRepository.GetById(id); // Chama o método GetById do repositório
        }       


        // Cria um novo produto
        public async Task<Produto> Post(Produto produto)
        {
            return await _pRepository.Post(produto); // Chama o método Post do repositório
        }


        // Atualiza um produto inteiro (PUT)
        public async Task<bool> Put(Produto produto)
        {
            return await _pRepository.Put(produto); // Chama o método Put do repositório
        }


        // Remove um produto
        public async Task<bool> Delete(Guid id)
        {
            var produto = await _pRepository.GetById(id);
            if (produto == null) return false;

            // Assume que o repositório já persiste a remoção
            return await _pRepository.Delete(id);
        }
    }
}
