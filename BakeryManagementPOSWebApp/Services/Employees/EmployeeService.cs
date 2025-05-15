using BakeryManagementPOSWebApp.Data;
using BakeryManagementPOSWebApp.Data.Enities;
using BakeryManagementPOSWebApp.Migrations;
using BakeryManagementPOSWebApp.Services.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagementPOSWebApp.Services.Employees
{
    public class EmployeeService
    {
        readonly ApplicationDbContext _dbContext;
        readonly CustomerService _customerService;
        readonly UserManager<Employee> _userManager;
        readonly IUserStore<Employee> _userStore;
        public EmployeeService(ApplicationDbContext dbContext, CustomerService customerService, UserManager<Employee> userManager, IUserStore<Employee> userStore)
        {
            _dbContext = dbContext;
            _customerService = customerService;
            _userManager = userManager;
            _userStore = userStore;
        }

        // Entity Specific
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _dbContext.Employees.ToListAsync();
        }
        public async Task<List<Employee>> GetExistingEmployees()
        {
            return await _dbContext.Employees.Where(p => p.DateDeleted == null).ToListAsync();
        }
        public async Task<List<Employee>> GetTrashedEmployees()
        {
            return await _dbContext.Employees.Where(p => p.DateDeleted != null).ToListAsync();
        }

        public async Task<int?> CreateEmployee(Employee employee, string password)
        {
            // Get customer with the same PhoneNumber
            Customer? linkedCustomer = await _customerService.GetCustomer(employee.PhoneNumber!);

            // If no match then Create a new Customer
            if (linkedCustomer is null)
            {
                linkedCustomer = await _customerService.CreateCustomer(new Customer()
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    PhoneNumber = employee.PhoneNumber!,
                    EmailAddress = employee.Email!
                });
            }

            // Link the Employee to the Customer
            employee.CustomerId = linkedCustomer.Id;

            // Set Username and create hash password/user
            await _userStore.SetUserNameAsync(employee, employee.FullName.Replace(" ", ""), CancellationToken.None);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(employee);
            await _userManager.ConfirmEmailAsync(employee, code);
            var result = await _userManager.CreateAsync(employee, password).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                Console.WriteLine($"Error: {string.Join(", ", result.Errors.Select(error => error.Description))}");
                return null;
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployee(string id)
        {
            return await _dbContext.Employees.Where(p => p.Id == id).FirstAsync();
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            employee.DateUpdated = DateTime.Now;

            _dbContext.Employees.Update(employee);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SoftDeleteEmployee(Employee employee)
        {
            employee.DateDeleted = DateTime.Now;

            _dbContext.Employees.Update(employee);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RecoverEmployee(Employee employee)
        {
            employee.DateDeleted = null;

            _dbContext.Employees.Update(employee);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> HardDeleteEmployee(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
