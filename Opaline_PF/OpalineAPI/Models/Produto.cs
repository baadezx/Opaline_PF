// Importa atributos para validação de dados (Required, StringLength, DataType)
using System.ComponentModel.DataAnnotations;

// Declaração do namespace onde a classe Produto está localizada
namespace OpalineAPI.Models;

// Declaração da classe Produto que representa um item no catálogo
public class Produto
{
    public Guid Id { get; set; } // Identificador único do produto


    [Required] // Indica que o campo seguinte é obrigatório
    [StringLength(50)] // Define tamanho máximo da string (50 caracteres)
    public string Nome { get; set; } // string para nome do produto


    [Required] // Indica que o campo seguinte é obrigatório
    [StringLength(100)] // Define tamanho máximo da string (100 caracteres)
    public string Descricao { get; set; } // string para descrição do produto


    [Required] // Indica que o campo seguinte é obrigatório
    [DataType(DataType.Currency)] // Define o tipo de dado como moeda para formatação
    public decimal Preco { get; set; } // Preço do produto (tipo decimal para valores monetários)


    public int? Estoque { get; set; } // Quantidade em estoque; int? permite valor nulo quando não informado

  
    public Guid CategoriaId { get; set; } // Identificador da categoria relacionada (chave estrangeira)

  
    public Categoria? Categoria { get; set; } // Referência à entidade Categoria associada a este produto
}
