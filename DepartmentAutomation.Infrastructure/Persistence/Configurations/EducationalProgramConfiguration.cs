using DepartmentAutomation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepartmentAutomation.Infrastructure.Persistence.Configurations
{
    public class EducationalProgramConfiguration : IEntityTypeConfiguration<EducationalProgram>
    {
        public void Configure(EntityTypeBuilder<EducationalProgram> builder)
        {
            builder.HasMany(_ => _.Lessons)
                .WithOne(_ => _.EducationalProgram)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}