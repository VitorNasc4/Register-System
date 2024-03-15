using ProjectName.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ProjectName.Infrastructure.Persistence
{
    public class ProjectNameDbContext : DbContext
    {
        public ProjectNameDbContext(DbContextOptions<ProjectNameDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
