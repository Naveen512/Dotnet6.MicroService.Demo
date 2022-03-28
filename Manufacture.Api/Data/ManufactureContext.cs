using Manufacture.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manufacture.Api.Data;

public class ManufactureContext: DbContext
{
    public ManufactureContext(DbContextOptions<ManufactureContext> options) : base(options)
    {
        
    }

    public DbSet<Products> Products{get;set;}
}