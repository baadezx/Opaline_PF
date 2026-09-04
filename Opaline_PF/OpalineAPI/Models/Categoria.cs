using System.ComponentModel.DataAnnotations; // Importa atributos para validação de dados (Required, StringLength, etc.)

namespace OpalineAPI.Models
{
    public class Categoria
    {
        public Guid Id { get; set; } // Identificador único da categoria


        [Required] // Indica que o campo seguinte é obrigatório
        [StringLength(50)] // Define tamanho máximo da string (50 caracteres)
        public string Nome { get; set; } // Nome da categoria
        

        [Required] // Indica que o campo seguinte é obrigatório
        [StringLength(100)] // Define tamanho máximo da string (100 caracteres)
        public string Descricao { get; set; } // Descrição da categoria


        public ICollection<Produto>? Produtos { get; set; } // Coleção de produtos associados a esta categoria - Relacionamento 1:N
    }
}
