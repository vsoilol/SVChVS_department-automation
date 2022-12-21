using DepartmentAutomation.Application.Common.Exceptions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Entities.TeacherInformation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Teachers.Commands.ChangeTeacherPassword
{
    public class ChangeTeacherPasswordCommand : IRequest<NewPasswordDto>
    {
        public string Id { get; set; }
    }

    public class ChangeTeacherPasswordCommandHandler : IRequestHandler<ChangeTeacherPasswordCommand, NewPasswordDto>
    {

        private readonly IIdentityService _identityService;

        public ChangeTeacherPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<NewPasswordDto> Handle(ChangeTeacherPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ChangePasswordAsync(request.Id);

            if (!result.Success)
            {
                throw new NotFoundException(nameof(Teacher), request.Id);
            }

            return new NewPasswordDto
            {
                NewPassword = result.NewPassword
            };
        }
    }
}