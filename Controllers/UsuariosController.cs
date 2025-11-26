using MecanicApp.Api.Models;
using MecanicApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MecanicApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // =============================================
        // POST: api/usuarios/registro
        // =============================================
        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool existe = await _usuarioService.CorreoExisteAsync(usuario.Correo);
            if (existe)
                return BadRequest(new { mensaje = "El correo ya está registrado." });

            await _usuarioService.CrearUsuarioAsync(usuario);

            return Ok(new { mensaje = "Usuario registrado correctamente." });
        }

        // =============================================
        // POST: api/usuarios/login
        // =============================================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario credenciales)
        {
            var usuario = await _usuarioService.LoginAsync(
                credenciales.Correo,
                credenciales.Password
            );

            if (usuario == null)
                return Unauthorized(new { mensaje = "Credenciales incorrectas." });

            return Ok(new
            {
                mensaje = "Inicio de sesión exitoso.",
                usuario = new
                {
                    usuario.Id,
                    usuario.NombreCompleto,
                    usuario.Correo,
                    usuario.Telefono
                }
            });
        }
    }
}
