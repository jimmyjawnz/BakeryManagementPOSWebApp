using BakeryManagementPOSWebApp.Data;
using BakeryManagementPOSWebApp.Data.Enities;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagementPOSWebApp.Services.Customers
{
    public class CustomerService
    {
        readonly ApplicationDbContext _dbContext;

        public CustomerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Entity Specific
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _dbContext.Customers.ToListAsync();
        }
        public async Task<List<Customer>> GetAvailableCustomers()
        {
            return await _dbContext.Customers.Where(p => p.DateDeleted == null).ToListAsync();
        }
        public async Task<List<Customer>> GetExistingCustomers(string filter)
        {
            return await _dbContext.Customers.Where(p => p.DateDeleted == null && p.PhoneNumber.Contains(filter)).ToListAsync();
        }
        public async Task<List<Customer>> GetTrashedCustomers()
        {
            return await _dbContext.Customers.Where(p => p.DateDeleted != null).ToListAsync();
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _dbContext.Customers.Where(p => p.Id == id).FirstAsync();
        }

        public async Task<Customer?> GetCustomer(string phoneNumber)
        {
            try
            {
                return await _dbContext.Customers.Where(p => p.PhoneNumber == phoneNumber).FirstAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            customer.DateUpdated = DateTime.Now;

            _dbContext.Customers.Update(customer);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SoftDeleteCustomer(Customer customer)
        {
            customer.DateDeleted = DateTime.Now;

            _dbContext.Customers.Update(customer);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RecoverCustomer(Customer customer)
        {
            customer.DateDeleted = null;

            _dbContext.Customers.Update(customer);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> HardDeleteCustomer(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
