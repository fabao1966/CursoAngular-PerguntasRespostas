using BackEnd.Domain.IRepositories;
using BackEnd.Domain.Models;
using BackEnd.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Persistence.Repositories
{
	public class UsuarioReposity : IUsuarioRepository
	{
		private readonly AplicationDbContext _context;
		public UsuarioReposity(AplicationDbContext context)
		{
			_context = context;
		}

		public async Task SaveUser(Usuario usuario)// salvando usuario
		{
			_context.Add(usuario);
			await _context.SaveChangesAsync();
		}

		public async Task UpdatePassword(Usuario usuario)
		{
			_context.Update(usuario);

			await _context.SaveChangesAsync();
		}

		public async Task<bool> ValidateExistence(Usuario usuario)
		{
			var validateExistence = await _context.Usuario.AnyAsync(options => 
																	options.NombreUsuario == usuario.NombreUsuario);

			return validateExistence;// retorna true ou false
		}

		public async Task<Usuario> ValidatePassword(int idUsuario, string passwordAnterior)
		{
			var usuario = await _context.Usuario.Where(x =>
						   x.Id == idUsuario && x.Password == passwordAnterior).FirstOrDefaultAsync();

			return usuario;
		}
	}
}
