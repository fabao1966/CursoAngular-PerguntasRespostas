using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.DTO;
using BackEnd.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
	[Route("api/controller")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		private readonly IUsuarioService _usuarioService;
		public UsuarioController(IUsuarioService usuarioService)
		{
			_usuarioService = usuarioService;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Usuario usuario)
		{
			try
			{
				var validateExistence = await _usuarioService.ValidateExistence(usuario);
				if (validateExistence)
				{
					return BadRequest(new { message = "O usuário "+ usuario.NombreUsuario + " já existe!" });
				}

				usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
				await _usuarioService.SaveUser(usuario);

				return Ok(new { message = "Usuário registrado com sucesso" });
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[Route("CambiarPassword")]
		[HttpPut]
		public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDTO cambiarPassword )
		{
			try
			{
				int idUsuario = 3;

				string passwordEncriptado = Encriptar.EncriptarPassword(cambiarPassword.passwordAnterior);

				var usuario = await _usuarioService.ValidatePassword(idUsuario, passwordEncriptado);

				if (usuario == null)
				{
					return BadRequest(new { message = "Senha incorreta!" });
				}
				else
				{
					usuario.Password = Encriptar.EncriptarPassword(cambiarPassword.nuebaPassword);

					await _usuarioService.UpdatePassword(usuario);

					return Ok(new { message = "Senha atualizada com sucesso!" });
				}

				;

			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
	}
}
