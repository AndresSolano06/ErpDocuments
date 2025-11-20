using ErpDocuments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErpDocuments.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<RelatedEntity> RelatedEntities { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<ValidationFlow> ValidationFlows { get; set; } = null!;
        public DbSet<ValidationStep> ValidationSteps { get; set; } = null!;
        public DbSet<ValidationAction> ValidationActions { get; set; } = null!;
    }

}
