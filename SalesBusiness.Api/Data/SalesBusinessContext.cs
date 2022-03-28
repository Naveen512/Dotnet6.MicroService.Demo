using Microsoft.EntityFrameworkCore;
using SalesBusiness.Api.Data.Entities;

namespace SalesBusiness.Api.Data;
public class SalesBusinessContext: DbContext
{
    public SalesBusinessContext(DbContextOptions<SalesBusinessContext> options):base(options)
    {
        
    }
    public DbSet<Orders> Orders{get;set;}
    public DbSet<Products> Products{get;set;}
}