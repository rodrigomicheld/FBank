using Microsoft.EntityFrameworkCore;

namespace FBank.Infrastructure
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        //public DbSet<Model> Models { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //new ModelMapping().Initialize(modelBuilder.Entity<Model>());
        }
    }
}
