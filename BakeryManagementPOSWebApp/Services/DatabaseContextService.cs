using BakeryManagementPOSWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagementPOSWebApp.Services
{
    public class DatabaseContextService
    {
        readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public DatabaseContextService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<ApplicationDbContext?> CreateDbContext()
        {
            try
            {
                return await _dbFactory.CreateDbContextAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] -> Unable to create Database Context:");
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }
    }
}
