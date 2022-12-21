using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQuery : IRequest<List<DepartmentDto>>
    {
    }

    public class GetAllDepartmentsQueryHandle : IRequestHandler<GetAllDepartmentsQuery, List<DepartmentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandle(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DepartmentDto>> Handle(GetAllDepartmentsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Departments
                .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}