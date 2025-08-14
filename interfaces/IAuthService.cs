using System.Threading.Tasks;
using RestApiMantenimientoEF.Modelos.DTOs;
namespace RestApiMantenimientoEF.Interfaces
{
    public interface IAuthService
    {
        // Método para generar el token JWT.
        // Recibe el nombre de usuario y devuelve el token como un string.
        Task<string> GenerateJwtToken(string username);
        
        // Método para validar las credenciales del usuario.
        // Recibe el objeto de login y devuelve true si es válido, false en caso contrario.
        Task<bool> IsValidUser(LoginDTO login);
    }
}