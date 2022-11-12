using DepartmentAutomation.Domain.Entities.EvaluationToolInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepartmentAutomation.Infrastructure.Persistence.Configurations
{
    public class EvaluationToolsConfiguration : IEntityTypeConfiguration<EvaluationTool>
    {
        public void Configure(EntityTypeBuilder<EvaluationTool> builder)
        {
            builder.HasKey(t => new { t.EducationalProgramId, t.EvaluationToolTypeId });

            builder.HasOne(pt => pt.EducationalProgram)
                .WithMany(p => p.EvaluationTools)
                .HasForeignKey(pt => pt.EducationalProgramId);

            builder.HasOne(pt => pt.EvaluationToolType)
                .WithMany(t => t.EvaluationTools)
                .HasForeignKey(pt => pt.EvaluationToolTypeId);
        }
    }
}