using test.application.Settings;
using test.application.TransactionServices;
using test.infraestructure.TransactionServices;
using test.domain.interfaces;
using test.infraestructure.RepositoryMongo;
using test.infraestructure.DBContext;
using Microsoft.EntityFrameworkCore;
using test.infraestructure.RepositorySQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITransactionService, TransactionService>();
//Se agrega configuración de Mongo
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
//Se agrega el servicio de la base de datos para que solo dure durante la transacción
builder.Services.AddScoped(typeof(IRepositoryMongo<>),typeof(RepositoryMongo<>));
builder.Services.AddScoped(typeof(IRepositorySQL<>), typeof(RepositorySQL<>));
//Se agrega el servicio del DbContext
builder.Services.AddDbContext<SqlDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));
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
