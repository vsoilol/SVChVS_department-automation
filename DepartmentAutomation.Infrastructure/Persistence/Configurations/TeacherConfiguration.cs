using DepartmentAutomation.Domain.Entities.TeacherInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepartmentAutomation.Infrastructure.Persistence.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasOne(_ => _.Department)
                .WithMany(_ => _.Teachers)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}