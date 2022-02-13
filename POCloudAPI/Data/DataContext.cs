using Microsoft.EntityFrameworkCore;
using POCloudAPI.Entities;

namespace POCloudAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<APIUser> Users { get; set; }
        public DbSet<APIFile> Files { get; set; }
    }
}
