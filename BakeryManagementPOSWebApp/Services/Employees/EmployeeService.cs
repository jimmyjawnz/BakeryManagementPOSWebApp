using BakeryManagementPOSWebApp.Data;
using BakeryManagementPOSWebApp.Data.Enities;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagementPOSWebApp.Services.Employees
{
    public class EmployeeService
    {
        readonly ApplicationDbContext _dbContext;
        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<int> CreateEmployee(Employee Employee)
        {
            await _dbContext.Employees.AddAsync(Employee);
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
