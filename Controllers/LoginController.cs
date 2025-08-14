using Microsoft.AspNetCore.Mvc;
using RestApiMantenimientoEF.Modelos.DTOs;
using RestApiMantenimientoEF.Modelos.Responses;
using RestApiMantenimientoEF.Interfaces;
using RestApiMantenimientoEF.Repositories;
namespace RestApiMantenimientoEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO login)
        {
            var isValidUser = await _authService.IsValidUser(login);

            if (!isValidUser)
            {
                return Unauthorized(new AccionResponse<string>
                {
                    Status = 401,
                    Message = "Credenciales incorrectas."
                });
            }

            var token = await _authService.GenerateJwtToken(login.NoColaborador);
            var idAreaS = await _userRepository.GetIdAreaSByIdNoColaborador(int.Parse(login.NoColaborador));
            return Ok(new AccionResponse<LoginResponseDTO>
            {
                Status = 200,
                Message = "Login exitoso.",
                Data = new LoginResponseDTO
                {
                    Token = token,
                    IdAreaS = idAreaS
                }
            });
        }
    }
}