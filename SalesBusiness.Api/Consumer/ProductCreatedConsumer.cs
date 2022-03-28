using MassTransit;
using SalesBusiness.Api.Data;
using SalesBusiness.Api.Data.Entities;
using Shared.Models.RabbitMqModel;

namespace SalesBusiness.Api.Consumer;

public class ProductCreatedConsumer : IConsumer<ProductCreated>
{
    private readonly SalesBusinessContext _salesBusinessContext;
    public ProductCreatedConsumer(SalesBusinessContext salesBusinessContext)
    {
        _salesBusinessContext = salesBusinessContext;
    }
    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        var newProduct = new Products{
            Id = context.Message.Id,
            Name = context.Message.Name
        };
        _salesBusinessContext.Add(newProduct);
        await _salesBusinessContext.SaveChangesAsync();
    }
}