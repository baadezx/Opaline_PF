// Importa atributos para validação de dados (Required, StringLength, DataType)
using System.ComponentModel.DataAnnotations;

namespace OpalineAPI.Models
{
    public class Compra
    {
        public Guid Id { get; set; } // Identificador único da compra


        [Required] // Indica que o campo seguinte é obrigatório
        public Guid ProdutoId { get; set; } // Identificador do produto comprado (chave estrangeira)


        [Required] // Indica que o campo seguinte é obrigatório
        [StringLength(50)] // Limita o tamanho máximo da string para 50 caracteres
        public string MetodoPagamento { get; set; } // Método de pagamento utilizado na compra


        public DateTime DataCompra { get; set; } // Data da compra


        public DateTime DataEntrega { get; set; } // Data prevista para entrega


        public DateTime? DataEntregue { get; set; } // Data que o produto foi entregue


        public DateTime? DataCancelamento { get; set; } // Data que a compra foi cancelada

        public bool IsCancelled => DataCancelamento.HasValue; // Propriedade calculada para verificar se a compra foi cancelada

        public bool EstaEntregue => DataEntregue.HasValue; // Propriedade calculada para verificar se a compra foi entregue
    }
}
