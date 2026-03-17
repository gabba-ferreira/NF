

using Microsoft.EntityFrameworkCore;
using NF.Data;
using NF.Repositories;
using NF.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DB init e config
builder.Services.AddDbContext<NFContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
builder.Services.AddScoped<IPecaRepository, PecaRepository>();
builder.Services.AddScoped<IOrdemServicoPecaRepository, OrdemServicoPecaRepository>();





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
