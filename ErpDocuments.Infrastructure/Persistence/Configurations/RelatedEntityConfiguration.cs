using ErpDocuments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpDocuments.Infrastructure.Persistence.Configurations
{
    public class RelatedEntityConfiguration : IEntityTypeConfiguration<RelatedEntity>
    {
        public void Configure(EntityTypeBuilder<RelatedEntity> builder)
        {
            builder.ToTable("RelatedEntities");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
