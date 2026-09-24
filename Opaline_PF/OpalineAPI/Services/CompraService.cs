using Microsoft.AspNetCore.Http.HttpResults;
using OpalineAPI.Models;
using OpalineAPI.Repositories;

namespace OpalineAPI.Services
{
    public class CompraService
    {
        private readonly CompraRepository _compraRepository;

        public CompraService(CompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        public async Task<List<Compra>> GetAllAsync()
        {
            return await _compraRepository.GetAllAsync();
        }

        public async Task<Compra?> GetByIdAsync(Guid id)
        {
            return await _compraRepository.GetByIdAsync(id);
        }

        public async Task<Compra> CreateAsync(Compra compra)
        {
            // 🔎 Regras de negócio:
            // - DataEntrega não pode ser antes da DataCompra
            if (compra.DataEntrega < compra.DataCompra)
                throw new ArgumentException("A data de entrega não pode ser anterior à data da compra.");

            // - Método de pagamento deve ser informado
            if (string.IsNullOrWhiteSpace(compra.MetodoPagamento))
                throw new ArgumentException("O método de pagamento é obrigatório.");

            compra.DataCompra = DateTime.UtcNow;

            // Define a data de entrega como 7 dias após a compra
            compra.DataEntrega = compra.DataCompra.AddDays(7);

            // Remove o horário, deixando apenas dia/mês/ano
            compra.DataEntrega = compra.DataEntrega.Date;

            return await _compraRepository.CreateAsync(compra);
        }

        public async Task<Compra?> UpdateAsync(Guid id, Compra compra)
        {
            // Pode aplicar validações semelhantes às do Create
            if (compra.DataEntrega < compra.DataCompra)
                throw new ArgumentException("A data de entrega não pode ser anterior à data da compra.");

            // Define a data de entrega como 7 dias após a compra
            compra.DataEntrega = compra.DataCompra.AddDays(7);

            // Remove o horário, deixando apenas dia/mês/ano
            compra.DataEntrega = compra.DataEntrega.Date;

            return await _compraRepository.UpdateAsync(id, compra);
        }

        public async Task<Compra?> MarcarComoEntregueAsync(Guid id)
        {
            var compra = await _compraRepository.GetByIdAsync(id);
            if (compra == null) return null;

            // Define a data de entrega como o momento atual
            compra.DataEntregue = DateTime.Now; // apenas dia/mês/ano

            return await _compraRepository.UpdateAsync(id, compra);
        }

        public async Task<Compra?> MarcarComoCanceladoAsync(Guid id)
        {
            var compra = await _compraRepository.GetByIdAsync(id);
            if (compra == null) return null;

            // Define a data de cancelamento como o momento atual
            compra.DataCancelamento = DateTime.Now; // apenas dia/mês/ano

            return await _compraRepository.UpdateAsync(id, compra);
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _compraRepository.DeleteAsync(id);
        }
    }
}
