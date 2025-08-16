using Microsoft.EntityFrameworkCore;
using ProdutosApi.Models;
using ProdutosApi.Repository;
using ProdutosApi.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//  builder.Services.AddOpenApiDocument();
builder.Services.AddDbContext<ProdutoRepository>(opt =>
    opt.UseInMemoryDatabase("ProdutosDb"));

builder.Services.AddTransient<IProdutoService, ProdutoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // app.UseOpenApi(); 
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
