using Application.Dtos.Users;
using Application.Features.Auth.Commands.AuthenticationCommands;
using Application.Features.Auth.Commands.RegisterCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.IpAddress = GetIpAddress();
            
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return BadRequest(response.Errors); 

            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync([FromBody] RegisterRequest request)
        {
            var response = await Mediator!.Send(new RegisterCommand
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Origin = Request.Headers["Origin"].FirstOrDefault() ?? string.Empty
            });

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return StatusCode(201, response);
        }


           private string GetIpAddress()
        {
            if (Request.Headers.TryGetValue("X-Forwarded-For", out var forwarderIp) && !string.IsNullOrWhiteSpace(forwarderIp))
            {
                return forwarderIp.ToString();
            }

            var remoteIp = Request.HttpContext.Connection.RemoteIpAddress;
            return remoteIp != null ? remoteIp.MapToIPv4().ToString() : "Ip no valida";
        }
    }
}