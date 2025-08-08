

using System.Text.Json.Serialization;
using Application.Dtos.Users;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.AuthenticationCommands
{
    public class AuthenticationCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string Email { get; set; } 
        public string Password { get; set; }
        [JsonIgnore]
        public string? IpAddress { get; set; }
    }

    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, Response<AuthenticationResponse>>
    {
        private readonly IAccountServices _accountServices;

        public AuthenticationCommandHandler(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        public async Task<Response<AuthenticationResponse>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            return await _accountServices.AuthenticateAsync(new AuthenticationRequest
            {
                Email = request.Email,
                Password = request.Password
            }, request.IpAddress);
        }
    }
}