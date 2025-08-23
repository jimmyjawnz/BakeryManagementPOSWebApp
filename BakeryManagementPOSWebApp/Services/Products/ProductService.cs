using BakeryManagementPOSWebApp.Data;
using BakeryManagementPOSWebApp.Data.DataTransfers;
using BakeryManagementPOSWebApp.Data.Enities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace BakeryManagementPOSWebApp.Services.Products
{
    public class ProductService
    {
        readonly DatabaseContextService _dbService;
        public ProductService(DatabaseContextService dbService)
        {
            _dbService = dbService;
        }

        // Entity Specific
        public async Task<List<Product>> GetAllProducts(string? nameFilter)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return new List<Product>();
            }

            if (nameFilter == null)
            {
                return await _dbContext.Products.ToListAsync();
            }
            else
            {
                return await _dbContext.Products.Where(p => p.Name.Contains(nameFilter)).ToListAsync();
            }

        }
        public async Task<List<Product>> GetAvailableProducts(string? nameFilter)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return new List<Product>();
            }

            if (nameFilter == null)
            {
                return await _dbContext.Products.Where(p => p.IsAvailable && p.Deleted == null).ToListAsync();
            }
            else
            {
                return await _dbContext.Products.Where(p => p.IsAvailable && p.Deleted == null && p.Name.Contains(nameFilter)).ToListAsync();
            }
        }

        public async Task<List<Product>> GetExistingProducts(string? nameFilter)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return new List<Product>();
            }

            if (nameFilter == null)
            {
                return await _dbContext.Products.Where(p => p.Deleted == null).ToListAsync();
            }
            else
            {
                return await _dbContext.Products.Where(p => p.Deleted == null && p.Name.Contains(nameFilter)).ToListAsync();
            }
        }

        public async Task<List<Product>> GetTrashedProducts(string? nameFilter)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return new List<Product>();
            }

            if (nameFilter == null)
            {
                return await _dbContext.Products.Where(p => p.Deleted != null).ToListAsync();
            }
            else
            {
                return await _dbContext.Products.Where(p => p.Deleted != null && p.Name.Contains(nameFilter)).ToListAsync();
            }
        }

        public async Task<Product?> CreateProduct(ProductDT product)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return null;
            }

            Product createdProduct = new()
            {
                Name = product.Name,
                Price = product.Price,
                IsAvailable = product.Availability,
                Description = product.Description,
                Deleted = null,
                LastUpdated = null,
                Created = DateTime.Now,
            };

            try
            {
                _dbContext.Products.Add(createdProduct);
                await _dbContext.SaveChangesAsync();
                return createdProduct;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] -> Unable to Create Product \"" + product.Name + "\":\n Product already exists and data has changed.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return null;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] -> Unable to Create Product \"" + product.Name + "\":");
                    Console.WriteLine(ex);
                    Console.ForegroundColor = ConsoleColor.White;
                    return null;
                }
            }
        }

        public async Task<Product?> GetProduct(int id)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return null;
            }

            return await _dbContext.Products.SingleAsync(p => p.Id == id);
        }

        public async Task<Product?> GetProduct(string name)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return null;
            }

            return await _dbContext.Products.SingleAsync(p => p.Name.Contains(name));
        }

        public async Task<Product?> UpdateProduct(ProductDT product, int id)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return null;
            }

            try
            {
                var currentProduct = await _dbContext.Products.SingleAsync(p => p.Id == id);

                currentProduct.Name = product.Name;
                currentProduct.Price = product.Price;
                currentProduct.IsAvailable = product.Availability;
                currentProduct.Description = product.Description;
                currentProduct.LastUpdated = DateTime.Now;

                await _dbContext.SaveChangesAsync();
                return currentProduct;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] -> Unable to Update Product \"" + product.Name + "\":");
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }

        public async Task<Product?> SoftDeleteProduct(int id)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return null;
            }

            try
            {
                var currentProduct = await _dbContext.Products.SingleAsync(p => p.Id == id);

                currentProduct.Deleted = DateTime.Now;

                await _dbContext.SaveChangesAsync();
                return currentProduct;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] -> Unable to Soft Delete Product:");
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }

        public async Task<Product?> RestoreProduct(int id)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return null;
            }

            try
            {
                var currentProduct = await _dbContext.Products.SingleAsync(p => p.Id == id);

                currentProduct.Deleted = null;

                await _dbContext.SaveChangesAsync();
                return currentProduct;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] -> Unable to Restore Product:");
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }

        public async Task<Product?> HardDeleteProduct(int id)
        {
            using var _dbContext = await _dbService.CreateDbContext();

            if (_dbContext == null)
            {
                return null;
            }

            try
            {
                var currentProduct = await _dbContext.Products.SingleAsync(p => p.Id == id);

                _dbContext.Products.Remove(currentProduct);

                await _dbContext.SaveChangesAsync();
                return currentProduct;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] -> Unable to Delete Product:");
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }
    }
}
