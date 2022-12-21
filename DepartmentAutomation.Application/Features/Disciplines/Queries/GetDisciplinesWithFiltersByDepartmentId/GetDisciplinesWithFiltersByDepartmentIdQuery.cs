using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Extensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Application.Common.Models;
using DepartmentAutomation.Application.Contracts.Requests;
using DepartmentAutomation.Application.Contracts.Requests.Filters;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Application.PredicateFactories;
using DepartmentAutomation.Domain.Entities;
using MediatR;

namespace DepartmentAutomation.Application.Features.Disciplines.Queries.GetDisciplinesWithFiltersByDepartmentId
{
    public class GetDisciplinesWithFiltersByDepartmentIdQuery : IRequest<PaginatedList<DepartmentHeadDisciplineDto>>
    {
        public GetDisciplinesWithFiltersByDepartmentIdQuery(PaginationRequest request, DisciplinesFilterDto filter)
        {
            Filter = filter;
            PageNumber = request.PageNumber;
            PageSize = request.PageSize;
        }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public DisciplinesFilterDto Filter { get; set; }
    }

    public class GetDisciplinesWithFiltersByDepartmentIdQueryHandler : IRequestHandler<
        GetDisciplinesWithFiltersByDepartmentIdQuery,
        PaginatedList<DepartmentHeadDisciplineDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExpressionBuilder<Discipline, DisciplinesFilterDto> _expressionBuilder;

        public GetDisciplinesWithFiltersByDepartmentIdQueryHandler(IApplicationDbContext context, IMapper mapper,
            IExpressionBuilder<Discipline, DisciplinesFilterDto> expressionBuilder)
        {
            _context = context;
            _mapper = mapper;
            _expressionBuilder = expressionBuilder;
        }
        
        public async Task<PaginatedList<DepartmentHeadDisciplineDto>> Handle(GetDisciplinesWithFiltersByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            var predicate = _expressionBuilder.Build(request.Filter);

            var predicateQuery = predicate is not null
                ? _context.Disciplines.Where(predicate)
                : _context.Disciplines;

            return await predicateQuery
                .OrderByDynamic(request.Filter.PropertyName, request.Filter.SortDirection)
                .ProjectTo<DepartmentHeadDisciplineDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}