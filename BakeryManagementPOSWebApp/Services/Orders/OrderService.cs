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
        readonly CustomerService _customerService;
        readonly UserManager<Employee> _userManager;
        public OrderService(ApplicationDbContext dbContext, CustomerService customerService, UserManager<Employee> userManager)
        {
            _dbContext = dbContext;
            _customerService = customerService;
            _userManager = userManager;
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
                .Include(o => o.ProcessedBy)
                .Where(p => p.DateDeleted == null).ToListAsync();
        }
        public async Task<List<Order>> GetTrashedOrders()
        {
            return await _dbContext.Orders.Where(p => p.DateDeleted != null).ToListAsync();
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
                .Include(o => o.OrderItems)
                .Include(o => o.ProcessedBy)
                .Where(o => o.Id == id).FirstAsync();
        }

        public async Task<int> UpdateOrder(Order order)
        {
            order.DateUpdated = DateTime.Now;

            _dbContext.Orders.Update(order);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SoftDeleteOrder(Order order)
        {
            order.DateDeleted = DateTime.Now;

            _dbContext.Orders.Update(order);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RecoverOrder(Order order)
        {
            order.DateDeleted = null;

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
