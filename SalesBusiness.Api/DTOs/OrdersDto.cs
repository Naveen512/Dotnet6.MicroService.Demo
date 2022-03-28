namespace SalesBusiness.Api.DTOs;

public class OrdersDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime? OrderDate { get; set; }
    public ProductDto ProductInfo { get; set; }
}

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}