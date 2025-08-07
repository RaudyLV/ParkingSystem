

using System.IdentityModel.Tokens.Jwt;
using Application.Dtos.Users;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Infraestructure.Identity.Helpers;
using Infraestructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infraestructure.Identity.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly JWTHelper _jWTHelper;
        public AccountServices(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, JWTHelper jWTHelper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jWTHelper = jWTHelper;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new NotFoundException("Email no encontrado");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new NotFoundException("El email o contraseña son incorrectos ");

            JwtSecurityToken securityToken = await _jWTHelper.GetJwtAsync(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.Token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            response.Email = user.Email!;
            response.UserName = user.UserName!;

            var roleList = await _userManager.GetRolesAsync(user);
            response.Roles = roleList.ToList();
            response.IsAuthenticated = user.EmailConfirmed;

            return new Response<AuthenticationResponse>(response, "Logeo completado exitosamente.");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var existingUserName = await _userManager.FindByNameAsync(request.UserName);
            if (existingUserName != null)
            {
                throw new AlreadyExistException($"El nombre de usuario ya esta en uso");
            }

            var existingEmai = await _userManager.FindByNameAsync(request.UserName);
            if (existingUserName != null)
            {
                throw new AlreadyExistException($"El email ya esta en uso");
            }

            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Operador.ToString());
                return new Response<string>(user.Id, "Usuario creado correctamente");
            }
            else
            {
                return new Response<string>(result.Errors.Select(e => e.Description).FirstOrDefault()!);
            }
        }
    }
}