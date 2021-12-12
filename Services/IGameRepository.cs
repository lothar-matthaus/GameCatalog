using GameCatalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCatalog.Services
{
	public interface IGameRepository
	{
		int Save(Game game);
		void Delete(int id);
		void Update(Game game);

		Game Get(int id);
		List<Game> GetAll();
	}
}
