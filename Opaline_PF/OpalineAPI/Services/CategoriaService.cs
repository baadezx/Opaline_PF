// Target framework for the project: .NET 10.0
using Microsoft.AspNetCore.JsonPatch; // Para atualizações parciais (PATCH)
using Microsoft.EntityFrameworkCore; // Para operações com o Entity Framework Core
using OpalineAPI.Models; // Modelos de dados (Categoria, Produto, etc.)
using OpalineAPI.Repositories; // Repositórios relacionados a categorias

namespace OpalineAPI.Services
{
    // Serviço responsável por gerenciar operações relacionadas a categorias
    public class CategoriaService
    {
        private readonly CategoriaRepository _cRepository; // Repositório de categorias

        public CategoriaService(CategoriaRepository cRepository) // Construtor com injeção de dependência
        {
            _cRepository = cRepository;
        }

        // Retorna todas as categorias
        public async Task<List<Categoria>> GetAll()
        {
            return await _cRepository.GetAll();
        }

        // Busca uma categoria pelo Id
        public async Task<Categoria?> GetById(Guid id)
        {
            return await _cRepository.GetById(id);
        }

        // Cria uma nova categoria
        public async Task<Categoria> Post(Categoria categoria)
        {
            return await _cRepository.Post(categoria);
        }

        // Atualiza uma categoria inteira (PUT)
        public async Task<bool> Put(Categoria categoria)
        {
            return await _cRepository.Put(categoria);
        }

        // Remove uma categoria
        public async Task<bool> Delete(Guid id)
        {
            var categoria = await _cRepository.GetById(id);
            if (categoria == null) return false;

            return await _cRepository.Delete(id);
        }
    }
}