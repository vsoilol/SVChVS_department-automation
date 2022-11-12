using DepartmentAutomation.Domain.Entities.KnowledgeControlFormInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepartmentAutomation.Infrastructure.Persistence.Configurations
{
    public class KnowledgeAssessmentConfiguration : IEntityTypeConfiguration<KnowledgeAssessment>
    {
        public void Configure(EntityTypeBuilder<KnowledgeAssessment> builder)
        {
            builder.HasKey(t => new { t.WeekId, t.KnowledgeControlFormId });

            builder.HasOne(pt => pt.Week)
                .WithMany(p => p.KnowledgeAssessments)
                .HasForeignKey(pt => pt.WeekId);

            builder.HasOne(pt => pt.KnowledgeControlForm)
                .WithMany(t => t.KnowledgeAssessments)
                .HasForeignKey(pt => pt.KnowledgeControlFormId);
        }
    }
}