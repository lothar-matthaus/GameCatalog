using System;
using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Models
{
	public class User
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Nome de usuário é obrigatório.")]
		[MinLength(2, ErrorMessage = "O nome de usuário deve possuir ao menos 2 caracteres.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "E-mail é obrigatório.")]
		[MinLength(2, ErrorMessage = "O nome de usuário deve possuir ao menos 2 caracteres.")]
		[EmailAddress(ErrorMessage = "Formato de E-mail não é válido.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "A senha é obrigatória.")]
		[MinLength(8, ErrorMessage = "A senha deve possuir ao menos 8 caracteres.")]
		public string Password { get; set; }

		public DateTime CreationDate { get; set; }

		public User()
		{
			this.Id = new Random().Next(100000, 999999);
			this.CreationDate = DateTime.Now;
		}
	}
}
