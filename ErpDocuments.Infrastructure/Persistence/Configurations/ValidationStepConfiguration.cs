using ErpDocuments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpDocuments.Infrastructure.Persistence.Configurations
{
    public class ValidationStepConfiguration : IEntityTypeConfiguration<ValidationStep>
    {
        public void Configure(EntityTypeBuilder<ValidationStep> builder)
        {
            builder.ToTable("ValidationSteps");

            builder.HasKey(x => x.Id);

            builder.HasOne<ValidationFlow>()
                .WithMany()
                .HasForeignKey(x => x.ValidationFlowId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
