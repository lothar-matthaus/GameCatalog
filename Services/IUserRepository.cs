using GameCatalog.Models;
using System.Collections.Generic;

namespace GameCatalog.Services
{
	public interface IUserRepository
	{
		int Save(User user);
		void Delete(int id);
		void Update(User user);

		User Get(int id);
		User Get(string email);
		List<User> GetAll();
	}
}
