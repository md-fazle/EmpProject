using EmployeeManagementSystem.Models.Entities;
using EmployeeManagementSystem.Models.UserAuth;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {   }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Rl>Rls { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }

       

    }
}
