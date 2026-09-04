using Microsoft.EntityFrameworkCore;
using OpalineAPI.Models;

namespace OpalineAPI.Data
{
    // Classe que representa o contexto do banco de dados
    // Herdando de DbContext, o EF Core sabe como mapear as entidades para tabelas
    public class AppDbContext : DbContext
    {
        // Construtor recebe opções de configuração (string de conexão, etc.)
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet representa uma tabela no banco de dados
        // Cada DbSet<T> corresponde a uma entidade (classe) que será persistida
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        // Configurações adicionais podem ser feitas aqui
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Chama o método base para garantir que as configurações padrão do EF Core sejam aplicadas

            // Exemplo: definir precisão para o campo Preço
            modelBuilder.Entity<Produto>() // Configura a entidade Produto
                .Property(p => p.Preco) // Configura a propriedade Preco
                .HasColumnType("decimal(18,2)"); // Define o tipo de coluna como decimal com precisão 18 e escala 2

            // Exemplo: relacionamento 1:N entre Categoria e Produto
            modelBuilder.Entity<Categoria>() // Configura a entidade Categoria
                .HasMany(c => c.Produtos) // Define que uma categoria pode ter muitos produtos
                .WithOne(p => p.Categoria) // Define que cada produto tem uma categoria
                .HasForeignKey(p => p.CategoriaId); // Define a chave estrangeira no modelo Produto
        }
    }
}
