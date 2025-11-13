using System.ComponentModel.DataAnnotations;
using inmobiliariaApp.Domain.Entities;

namespace inmobiliariaApp.Application.Dtos
{
    public class UserCreateDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Range(0, 1, ErrorMessage = "El rol solo puede ser 0 (Admin) o 1 (Client).")]
        public Role Role{ get; set; }
    }
}
