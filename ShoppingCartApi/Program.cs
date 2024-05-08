using Microsoft.EntityFrameworkCore;
using ShoppingCart.Business;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EcommerceContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceContext")), ServiceLifetime.Scoped);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<ICartService, CartService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
