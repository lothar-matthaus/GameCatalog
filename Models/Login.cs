using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Models
{
	public class Login
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
