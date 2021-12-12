using GameCatalog.Models;
using GameCatalog.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace GameCatalog.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly string _filePath = "/User.bin";
		private readonly string _folderPath;
		private readonly string _path;

		public UserRepository(IConfiguration configuration)
		{
			_folderPath = configuration["RepositoryPath"];
			_path = _folderPath + _filePath;
			InitializeRepositoryFile();
		}

		public void Delete(int id)
		{
			throw new System.NotImplementedException();
		}

		public User Get(int id)
		{
			throw new System.NotImplementedException();
		}

		public User Get(string email)
		{
			FileStream fileStream = null;
			StreamReader streamReader = null;
			User user = null;

			try
			{
				fileStream = new(_path, FileMode.Open, FileAccess.Read);
				streamReader = new(fileStream, Encoding.UTF8);

				while (!streamReader.EndOfStream)
				{
					user = JsonSerializer.Deserialize<User>(streamReader.ReadLine());

					if (user.Email.Equals(email))
					{
						fileStream.Flush();
						streamReader.Close();

						return user;
					}
					else
						user = null;
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
			return user;
		}

		public List<User> GetAll()
		{
			throw new System.NotImplementedException();
		}

		public int Save(User user)
		{
			FileStream fileStream = null;
			StreamWriter streamWriter = null;

			try
			{
				fileStream = new(_path, FileMode.Append, FileAccess.Write);
				streamWriter = new(fileStream, Encoding.UTF8);

				streamWriter.WriteLine(JsonSerializer.Serialize<User>(user));

				fileStream.Flush();
				streamWriter.Close();

				return user.Id;
			}
			catch (IOException ex)
			{
				fileStream.Flush();
				streamWriter.Close();

				throw new IOException(ex.Message);
			}
		}

		public void Update(User user)
		{
			throw new System.NotImplementedException();
		}

		private void InitializeRepositoryFile()
		{

			if (!Directory.Exists(_folderPath))
			{
				Directory.CreateDirectory(_folderPath);
			}

			if (!File.Exists(_path))
			{
				FileStream fileStream = new FileStream(_path, FileMode.Create);
				fileStream.Close();
			}
		}


	}
}
