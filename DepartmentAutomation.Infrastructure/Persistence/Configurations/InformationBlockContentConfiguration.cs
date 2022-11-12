using DepartmentAutomation.Domain.Entities.InformationBlockInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepartmentAutomation.Infrastructure.Persistence.Configurations
{
    public class InformationBlockContentConfiguration : IEntityTypeConfiguration<InformationBlockContent>
    {
        public void Configure(EntityTypeBuilder<InformationBlockContent> builder)
        {
            builder.HasKey(t => new { t.EducationalProgramId, t.InformationBlockId });

            builder.HasOne(pt => pt.EducationalProgram)
                .WithMany(p => p.InformationBlockContents)
                .HasForeignKey(pt => pt.EducationalProgramId);

            builder.HasOne(pt => pt.InformationBlock)
                .WithMany(t => t.InformationBlockContents)
                .HasForeignKey(pt => pt.InformationBlockId);
        }
    }
}