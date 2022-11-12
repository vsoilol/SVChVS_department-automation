using DepartmentAutomation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepartmentAutomation.Infrastructure.Persistence.Configurations
{
    public class SemesterDistributionConfiguration : IEntityTypeConfiguration<SemesterDistribution>
    {
        public void Configure(EntityTypeBuilder<SemesterDistribution> builder)
        {
            builder.HasKey(t => new { t.DisciplineId, t.SemesterId });

            builder.HasOne(pt => pt.Discipline)
                .WithMany(p => p.SemesterDistributions)
                .HasForeignKey(pt => pt.DisciplineId);

            builder.HasOne(pt => pt.Semester)
                .WithMany(t => t.SemesterDistributions)
                .HasForeignKey(pt => pt.SemesterId);
        }
    }
}