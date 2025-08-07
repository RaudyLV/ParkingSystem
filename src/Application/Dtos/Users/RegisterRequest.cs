
namespace Application.Dtos.Users
{
    public class RegisterRequest
    {
        public string UserName { get; set; } = "Guest";
        public string Email { get; set; } = "Guest@Email.com";
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; } 
    }
}