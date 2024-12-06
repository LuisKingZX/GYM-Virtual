using BCrypt.Net;

namespace BL.Services
{
    public class EncryptionService
    {
        public string HashPassword(string password)
        {
            // Encripta la contraseña usando BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Verifica si la contraseña coincide con el hash almacenado
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
