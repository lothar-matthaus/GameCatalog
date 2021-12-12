using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Models
{
	public class Login
	{
		[Required(ErrorMessage = "E-mail é obrigatório.")]
		[MinLength(2, ErrorMessage = "O nome de usuário deve possuir ao menos 2 caracteres.")]
		[EmailAddress(ErrorMessage = "Formato de E-mail não é válido.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "A senha é obrigatória.")]
		[MinLength(8, ErrorMessage = "A senha deve possuir ao menos 8 caracteres.")]
		public string Password { get; set; }
	}
}
