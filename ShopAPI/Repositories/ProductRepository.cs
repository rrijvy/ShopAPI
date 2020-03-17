using Microsoft.EntityFrameworkCore;
using ShopAPI.Data;
using ShopAPI.IRepositories;
using ShopAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopAPI.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Product> GetAllProductsWithCategory()
        {
            return _context.Products.Include(x => x.ProductCategory).ToList();
        }
    }
}
