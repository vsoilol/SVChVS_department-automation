using DepartmentAutomation.Domain.Entities.LiteratureInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepartmentAutomation.Infrastructure.Persistence.Configurations
{
    public class LiteratureTypeInfoConfiguration : IEntityTypeConfiguration<LiteratureTypeInfo>
    {
        public void Configure(EntityTypeBuilder<LiteratureTypeInfo> builder)
        {
            builder.HasKey(t => new { t.LiteratureId, t.EducationalProgramId });

            builder.HasOne(pt => pt.Literature)
                .WithMany(p => p.LiteratureTypeInfos)
                .HasForeignKey(pt => pt.LiteratureId);

            builder.HasOne(pt => pt.EducationalProgram)
                .WithMany(t => t.LiteratureTypeInfos)
                .HasForeignKey(pt => pt.EducationalProgramId);
        }
    }
}