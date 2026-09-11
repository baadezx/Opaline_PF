using Microsoft.EntityFrameworkCore;
using OpalineAPI.Data; // onde está o ApplicationDbContext
using OpalineAPI.Repositories; // onde estão os repositórios
using OpalineAPI.Services; // onde estão os serviços de negócio
using System.Text.Json.Serialization; // para configuração da serialização JSON

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar DbContext com a connection string proveniente do appsettings.json
//    Usa SQL Server (UseSqlServer) com a chave "DefaultConnection".
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Extra. Registra os serviços de controllers e configura opções de serialização JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configura o serializador para ignorar referências cíclicas entre objetos
        // Evita exceções ao serializar entidades com relacionamentos bidirecionais
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        
        // Formata o JSON com indentação para melhor legibilidade
        // Útil durante desenvolvimento e debugging
        options.JsonSerializerOptions.WriteIndented = true;
    });

// 2. Registrar repositórios (injeção de dependência scoped)
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<ProdutoRepository>();

// 3. Registrar serviços de negócio (injeta os repositórios acima)
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoService>();

// 4. Configurar controllers MVC para a API
builder.Services.AddControllers();

// 5. Configurar Swagger/OpenAPI para documentação automática da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 6. Middleware: habilitar Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 7. Middleware de infra: redirecionamento HTTPS e autorização (autenticação não configurada aqui)
app.UseHttpsRedirection();
app.UseAuthorization();

// 8. Mapear endpoints dos controllers
app.MapControllers();

// 9. Iniciar a aplicação
app.Run();

/* var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados ao container de injeção de dependência
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
*/