using BakeryManagementPOSWebApp.Data;
using BakeryManagementPOSWebApp.Data.Enities;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagementPOSWebApp.Services.Products
{
    public class ProductService
    {
        readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Entity Specific
        public async Task<List<Product>> GetAllProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }
        public async Task<List<Product>> GetAvailableProducts()
        {
            return await _dbContext.Products.Where(p => p.IsAvailable == true && p.DateDeleted == null).ToListAsync();
        }
        public async Task<List<Product>> GetExistingProducts()
        {
            return await _dbContext.Products.Where(p => p.DateDeleted == null).ToListAsync();
        }
        public async Task<List<Product>> GetTrashedProducts()
        {
            return await _dbContext.Products.Where(p => p.DateDeleted != null).ToListAsync();
        }

        public async Task<int> CreateProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _dbContext.Products.Where(p => p.Id == id).FirstAsync();
        }

        public async Task<int> UpdateProduct(Product product)
        {
            product.DateUpdated = DateTime.Now;

            _dbContext.Products.Update(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SoftDeleteProduct(Product product)
        {
            product.DateDeleted = DateTime.Now;

            _dbContext.Products.Update(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RecoverProduct(Product product)
        {
            product.DateDeleted = null;

            _dbContext.Products.Update(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> HardDeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
