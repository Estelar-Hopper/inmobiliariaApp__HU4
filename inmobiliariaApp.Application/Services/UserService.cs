using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using inmobiliariaApp.Domain.Entities;
using inmobiliariaApp.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace inmobiliariaApp.Application
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        // Obtener un usuario por ID
        public async Task<User?> GetUserById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de usuario no es válido");

            return await _userRepository.GetUserById(id);
        }

        // Obtener todos los usuarios
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        // Crear un usuario nuevo
        public async Task<User> AddUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("El nombre de usuario es obligatorio");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("El email es obligatorio");

            var existingUsers = await _userRepository.GetAll();
            if (existingUsers.Any(u => u.Email == user.Email))
                throw new InvalidOperationException("Ya existe un usuario con ese correo electrónico");

            return await _userRepository.Add(user);
        }

        // Actualizar usuario
        public async Task<bool> UpdateUser(User user)
        {
            if (user.Id <= 0)
                throw new ArgumentException("El ID de usuario no es válido");

            var existing = await _userRepository.GetUserById(user.Id);
            if (existing == null)
                return false;

            existing.Username = user.Username;
            existing.Password = user.Password;
            existing.Email = user.Email;
            existing.Role = user.Role;

            await _userRepository.Update(existing);
            return true;
        }

        // Eliminar usuario
        public async Task<bool> DeleteUser(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de usuario no es válido");

            var existing = await _userRepository.GetUserById(id);
            if (existing == null)
                return false;

            await _userRepository.Delete(id);
            return true;
        }



        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public async Task SaveRefreshTokenAsync(User user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.Update(user);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var users = await _userRepository.GetAll();
            return users.FirstOrDefault(u => u.RefreshToken == refreshToken);
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _userRepository.Update(user);
            return true;
        }
    }
}
