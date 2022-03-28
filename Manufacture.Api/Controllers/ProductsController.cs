using Manufacture.Api.Data;
using Manufacture.Api.Data.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.RabbitMqModel;

namespace Manufacture.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ManufactureContext _manufactureContext;
    private readonly IPublishEndpoint _publishEndpoint;
    public ProductsController(ManufactureContext manufactureContext,
    IPublishEndpoint publishEndpoint
    )
    {
        _manufactureContext = manufactureContext;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var products = await _manufactureContext.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var product = await _manufactureContext.Products.FindAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Products newProduct)
    {
        _manufactureContext.Products.Add(newProduct);
        await _manufactureContext.SaveChangesAsync();
        await _publishEndpoint.Publish<ProductCreated>(new ProductCreated
        {
            Id = newProduct.Id,
            Name = newProduct.Name
        });

        return CreatedAtAction("Get", new { id = newProduct.Id }, newProduct);
    }
}