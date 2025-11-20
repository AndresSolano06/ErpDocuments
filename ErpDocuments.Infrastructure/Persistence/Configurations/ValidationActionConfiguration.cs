using ErpDocuments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpDocuments.Infrastructure.Persistence.Configurations
{
    public class ValidationActionConfiguration : IEntityTypeConfiguration<ValidationAction>
    {
        public void Configure(EntityTypeBuilder<ValidationAction> builder)
        {
            builder.ToTable("ValidationActions");

            builder.HasKey(x => x.Id);

            builder.HasOne<ValidationStep>()
                .WithMany()
                .HasForeignKey(x => x.ValidationStepId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
