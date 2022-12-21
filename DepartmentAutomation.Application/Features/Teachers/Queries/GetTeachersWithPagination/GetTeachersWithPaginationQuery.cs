using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Application.Common.Models;
using DepartmentAutomation.Application.Contracts.Requests;
using MediatR;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Extensions;
using DepartmentAutomation.Application.Contracts.Requests.Filters;
using DepartmentAutomation.Application.PredicateFactories;
using DepartmentAutomation.Domain.Entities.TeacherInformation;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;

namespace DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersWithPagination
{
    public class GetTeachersWithPaginationQuery : IRequest<PaginatedList<TeacherBriefDto>>
    {
        public GetTeachersWithPaginationQuery(PaginationRequest request, TeacherFilterDto filter)
        {
            Filter = filter;
            PageNumber = request.PageNumber;
            PageSize = request.PageSize;
        }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public TeacherFilterDto Filter { get; set; }
    }

    public class
        GetTeachersWithPaginationQueryHandle : IRequestHandler<GetTeachersWithPaginationQuery,
            PaginatedList<TeacherBriefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExpressionBuilder<Teacher, TeacherFilterDto> _expressionBuilder;

        public GetTeachersWithPaginationQueryHandle(IApplicationDbContext context, IMapper mapper,
            IExpressionBuilder<Teacher, TeacherFilterDto> expressionBuilder)
        {
            _context = context;
            _mapper = mapper;
            _expressionBuilder = expressionBuilder;
        }

        public async Task<PaginatedList<TeacherBriefDto>> Handle(GetTeachersWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            var predicate = _expressionBuilder.Build(request.Filter);

            var predicateQuery = predicate is not null
                ? _context.Teachers.Where(predicate)
                : _context.Teachers;

            return await predicateQuery
                .OrderByDynamic(request.Filter.PropertyName, request.Filter.SortDirection)
                .ProjectTo<TeacherBriefDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}