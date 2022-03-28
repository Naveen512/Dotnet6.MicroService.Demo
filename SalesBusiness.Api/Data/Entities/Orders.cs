namespace SalesBusiness.Api.Data.Entities;

public class Orders
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime? OrderDate { get; set; }
}