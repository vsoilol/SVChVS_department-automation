using AutoMapper;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.Application.Features.EducationalPrograms.Queries.GetProgramWordDocument;
using DepartmentAutomation.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;
using DepartmentAutomation.Application.Converters;
using EducationalProgram = DepartmentAutomation.Domain.Entities.EducationalProgram;

namespace DepartmentAutomation.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<EducationalProgram, Models.WordDocument.EducationalProgram>().ConvertUsing<EducationalProgramToWordDocumentConverter>();
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                                 ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}