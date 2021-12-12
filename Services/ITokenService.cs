using GameCatalog.Models;

namespace GameCatalog.Services
{
	public interface ITokenService
	{
		string GenerateToken(User user);
	}
}
