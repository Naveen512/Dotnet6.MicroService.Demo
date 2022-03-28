using MassTransit;
using Microsoft.EntityFrameworkCore;
using SalesBusiness.Api.Consumer;
using SalesBusiness.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SalesBusinessContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("SalesBusinessDBConnection"));
});

builder.Services.AddMassTransit(x => {
    x.AddConsumer<ProductCreatedConsumer>();
    x.UsingRabbitMq((cxt, cfg) => {
        cfg.Host(new Uri("rabbitmq://localhost:4001"), h => {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("event-listener", e =>
        {
            e.ConfigureConsumer<ProductCreatedConsumer>(cxt);
        }); 
    });
});

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
