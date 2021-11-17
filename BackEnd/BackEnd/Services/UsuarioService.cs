using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services
{
	public class UsuarioService : Domain.IServices.IUsuarioService
	{
		private readonly IUsuarioRepository _usuarioRepository;

		public UsuarioService(Domain.IRepositories.IUsuarioRepository usuarioRepository)
		{
			_usuarioRepository = usuarioRepository;
		}

		public async Task SaveUser(Usuario usuario)//salvando usuario
		{
			await _usuarioRepository.SaveUser(usuario);
		}

		public async Task UpdatePassword(Usuario usuario)
		{
			await _usuarioRepository.UpdatePassword(usuario);
		}

		public async Task<bool> ValidateExistence(Usuario usuario)
		{
			return await _usuarioRepository.ValidateExistence(usuario);
		}

		public async Task<Usuario> ValidatePassword(int idUsuario, string passwordAnterior)
		{
			return await _usuarioRepository.ValidatePassword(idUsuario, passwordAnterior);
		}
	}
}
