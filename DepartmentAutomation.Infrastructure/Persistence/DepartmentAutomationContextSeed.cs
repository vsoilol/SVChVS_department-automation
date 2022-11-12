using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentAutomation.Domain.Entities.TeacherInformation;
using DepartmentAutomation.Domain.Entities.UserInfo;
using DepartmentAutomation.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Infrastructure.Persistence
{
    public class DepartmentAutomationContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await CreateRolesAsync(roleManager);

            var administrator = new ApplicationUser
            {
                UserName = "Админ",
                Surname = "Админ",
                Patronymic = "Админович",
                Email = "admin@admin.com",
            };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "S@1tring");
                await userManager.AddToRolesAsync(administrator, new[] { nameof(Role.Admin) });
            }
            
            var testUser = new ApplicationUser
            {
                UserName = "Тест",
                Surname = "Тест",
                Patronymic = "Тестович",
                Email = "test@test.com",
            };

            if (userManager.Users.All(u => u.UserName != testUser.UserName))
            {
                await userManager.CreateAsync(testUser, "Test@1111");
                await userManager.AddToRolesAsync(testUser, new[] { nameof(Role.Teacher) });
            }
            
            var departmentHead = new ApplicationUser
            {
                UserName = "Заведующий",
                Surname = "Заведующий",
                Patronymic = "Заведующий",
                Email = "head@head.com",
                DepartmentId = 1,
            };

            if (userManager.Users.All(u => u.UserName != departmentHead.UserName))
            {
                await userManager.CreateAsync(departmentHead, "Head@1111");
                await userManager.AddToRolesAsync(departmentHead, new[] { nameof(Role.DepartmentHead) });
            }
        }

        public static async Task SeedSampleDataAsync(DepartmentAutomationContext context,
            UserManager<ApplicationUser> userManager)
        {
            if (!await context.Positions.AnyAsync())
            {
                await CreateDefaultTeachersInformationAsync(context);
            }
        }

        private static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var enumValues = Enum.GetNames(typeof(Role));

            foreach (var role in enumValues)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var creationRole = new IdentityRole(role);
                    await roleManager.CreateAsync(creationRole);
                }
            }
        }

        private static async Task CreateDefaultTeachersInformationAsync(DepartmentAutomationContext context)
        {
            var academicDegrees = new List<AcademicDegree>
            {
                new AcademicDegree()
                {
                    Name = "Кандидат технических наук",
                },
                new AcademicDegree()
                {
                    Name = "Доктор технических наук",
                },
                new AcademicDegree()
                {
                    Name = "Доктор физико-математических наук",
                },
                new AcademicDegree()
                {
                    Name = "Кандидат экономических наук",
                },
                new AcademicDegree()
                {
                    Name = "Кандидат физико-математических наук",
                },
            };

            var academicRanks = new List<AcademicRank>
            {
                new AcademicRank()
                {
                    Name = "Доцент",
                },
                new AcademicRank()
                {
                    Name = "Профессор",
                },
            };

            var positions = new List<Position>
            {
                new Position()
                {
                    Name = "Ассистент-стажер",
                },
                new Position()
                {
                    Name = "Ассистент",
                },
                new Position()
                {
                    Name = "Старший преподаватель",
                },
                new Position()
                {
                    Name = "Доцент",
                },
                new Position()
                {
                    Name = "Профессор",
                },
            };

            await context.AcademicDegrees.AddRangeAsync(academicDegrees);
            await context.AcademicRanks.AddRangeAsync(academicRanks);
            await context.Positions.AddRangeAsync(positions);

            await context.SaveChangesAsync();
        }
    }
}