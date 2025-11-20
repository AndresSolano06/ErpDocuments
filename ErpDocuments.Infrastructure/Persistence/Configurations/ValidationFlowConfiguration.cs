using ErpDocuments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpDocuments.Infrastructure.Persistence.Configurations
{
    public class ValidationFlowConfiguration : IEntityTypeConfiguration<ValidationFlow>
    {
        public void Configure(EntityTypeBuilder<ValidationFlow> builder)
        {
            builder.ToTable("ValidationFlows");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne<Document>()
                .WithMany()
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
