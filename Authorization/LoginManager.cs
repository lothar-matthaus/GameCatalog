using GameCatalog.Models;
using GameCatalog.Services;

namespace GameCatalog.Authorization
{
	public class LoginManager
	{
		private readonly IUserRepository _userRepository;

		public LoginManager(IUserRepository userRepository) => this._userRepository = userRepository;

		public bool DoLogin(Login login)
		{
			User user = _userRepository.Get(login.Email);

			if (user != null)
				if (login.Password == user.Password)
					return true;

			return false;
		}
	}
}
