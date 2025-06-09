using BakeryManagementPOSWebApp.Components.Database.Pages;
using BakeryManagementPOSWebApp.Data;
using BakeryManagementPOSWebApp.Data.Enities;
using BakeryManagementPOSWebApp.Services.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagementPOSWebApp.Services.Orders
{
    public class OrderService
    {
        readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Entity Specific
        public async Task<List<Order>> GetAllOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }
        public async Task<List<Order>> GetExistingOrders()
        {
            return await _dbContext.Orders
                .Include(o => o.OrderedBy)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.ProcessedBy)
                .Where(p => p.Deleted == null).ToListAsync();
        }
        public async Task<List<Order>> GetTrashedOrders()
        {
            return await _dbContext.Orders.Where(p => p.Deleted != null).ToListAsync();
        }

        public async Task<int?> CreateOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderedBy)
                .Include(o => o.ProcessedBy)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.Id == id).FirstAsync();
        }

        public async Task<int> UpdateOrder(Order order)
        {
            order.LastUpdated = DateTime.Now;

            _dbContext.Orders.Update(order);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SoftDeleteOrder(Order order)
        {
            order.Deleted = DateTime.Now;

            _dbContext.Orders.Update(order);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RecoverOrder(Order order)
        {
            order.Deleted = null;

            _dbContext.Orders.Update(order);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> HardDeleteOrder(Order order)
        {
            _dbContext.Orders.Remove(order);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
