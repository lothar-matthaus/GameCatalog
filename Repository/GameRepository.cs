using GameCatalog.Models;
using GameCatalog.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameCatalog.Repository
{
	public class GameRepository : IGameRepository
	{
		private readonly string _path;

		public GameRepository(IConfiguration configuration)
		{
			_path = configuration["RepositoryPath"] + "/Game.data";
			InitializeRepositoryFile();
		}

		private void InitializeRepositoryFile()
		{
			if (!File.Exists(_path))
			{
				FileStream fileStream = new FileStream(_path, FileMode.Create);
				fileStream.Close();
			}
		}

		public void Delete(int id)
		{
			FileStream fileStream = null;
			StreamWriter streamWriter = null;
			List<Game> gameList = (List<Game>)GetAll();

			gameList.Remove(gameList.Find(game => game.Id == id));

			try
			{
				fileStream = new(_path, FileMode.Truncate, FileAccess.Write);
				streamWriter = new(fileStream, Encoding.UTF8);

				foreach (Game item in gameList)
					streamWriter.WriteLine(JsonSerializer.Serialize<Game>(item));

				fileStream.Flush();
				streamWriter.Close();
			}
			catch (Exception ex)
			{
				fileStream.Flush();
				streamWriter.Close();

				throw new IOException(ex.Message);
			}

		}

		public List<Game> GetAll()
		{
			FileStream fileStream = null;
			StreamReader streamReader = null;
			List<Game> gameList = new List<Game>();

			try
			{
				fileStream = new(_path, FileMode.Open, FileAccess.Read);
				streamReader = new(fileStream, Encoding.UTF8);

				while (!streamReader.EndOfStream)
					gameList.Add(JsonSerializer.Deserialize<Game>(streamReader.ReadLine()));

				fileStream.Flush();
				streamReader.Close();

				return gameList;
			}
			catch (Exception ex)
			{
				fileStream.Flush();
				streamReader.Close();

				throw new IOException(ex.Message);
			}

		}

		public Game Get(int id)
		{
			FileStream fileStream = null;
			StreamReader streamReader = null;
			Game game = null;

			try
			{
				fileStream = new(_path, FileMode.Open, FileAccess.Read);
				streamReader = new(fileStream, Encoding.UTF8);

				while (!streamReader.EndOfStream)
				{
					game = JsonSerializer.Deserialize<Game>(streamReader.ReadLine());

					if (game.Id == id)
					{
						fileStream.Flush();
						streamReader.Close();

						return game;
					}
					else
						game = null;

				}

				fileStream.Flush();
				streamReader.Close();
			}
			catch (Exception ex)
			{
				fileStream.Flush();
				streamReader.Close();

				throw new IOException(ex.Message);
			}

			return game;
		}

		public int Save(Game game)
		{
			FileStream fileStream = null;
			StreamWriter streamWriter = null;

			try
			{
				fileStream = new(_path, FileMode.Append, FileAccess.Write);
				streamWriter = new(fileStream, Encoding.UTF8);

				streamWriter.WriteLine(JsonSerializer.Serialize<Game>(game));

				fileStream.Flush();
				streamWriter.Close();

				return game.Id;
			}
			catch (IOException ex)
			{
				fileStream.Flush();
				streamWriter.Close();

				throw new IOException(ex.Message);
			}
		}

		public void Update(Game game)
		{
			FileStream fileStream = null;
			StreamWriter streamWriter = null;
			List<Game> gameList = GetAll();
			bool canBeUpdate = false;

			for (int index = 0; index < gameList.Count; index++)
			{
				if (gameList[index].Id == game.Id)
				{
					gameList[index] = game;
					canBeUpdate = true;
				}
			}

			if (canBeUpdate)
				try
				{
					fileStream = new(_path, FileMode.Truncate, FileAccess.Write);
					streamWriter = new(fileStream, Encoding.UTF8);

					foreach (Game item in gameList)
						streamWriter.WriteLine(JsonSerializer.Serialize<Game>(item));

					fileStream.Flush();
					streamWriter.Close();
				}
				catch (Exception ex)
				{
					fileStream.Flush();
					streamWriter.Close();

					throw new IOException(ex.Message);
				}
		}
	}
}
