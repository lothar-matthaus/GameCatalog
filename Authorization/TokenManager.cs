using GameCatalog.Models;
using GameCatalog.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameCatalog.Authorization
{
	public class TokenManager : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenManager(IConfiguration configuration) => this._configuration = configuration;

		public string GenerateToken(User user)
		{
			try
			{
				JwtSecurityTokenHandler tokenHandler = new();

				byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

				SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

				SecurityTokenDescriptor securityTokenDescriptor = new()
				{
					Issuer = _configuration["JWT:Issuer"],
					Audience = _configuration["JWT:Audience"],
					Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:Expiration"])),
					SigningCredentials = signingCredentials
				};

				SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);

				return tokenHandler.WriteToken(token);
			}
			catch 
			{
				throw new Exception("Erro ao gerar o Token.");
			}
		}
	}
}
