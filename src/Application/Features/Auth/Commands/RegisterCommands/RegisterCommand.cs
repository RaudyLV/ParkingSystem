

using Application.Dtos.Users;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.RegisterCommands
{
    public class RegisterCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Origin { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly IAccountServices _accountServices;

        public RegisterCommandHandler(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _accountServices.RegisterAsync(new RegisterRequest
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword
            }, request.Origin);
        }
    }
}