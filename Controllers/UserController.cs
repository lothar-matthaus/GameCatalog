using GameCatalog.Authorization;
using GameCatalog.Models;
using GameCatalog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GameCatalog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly ITokenService _tokenService;

		public UserController(IUserRepository userRepository, ITokenService tokenService)
		{
			this._userRepository = userRepository;
			this._tokenService = tokenService;
		}

		[Route("Login")]
		[HttpPost]
		public IActionResult Login(Login login)
		{
			TryValidateModel(login);

			if (ModelState.IsValid)
			{
				try
				{
					if (new LoginManager(_userRepository).DoLogin(login))
					{
						User user = _userRepository.Get(login.Email);
						string token = _tokenService.GenerateToken(user);

						return Ok(new
						{
							Success = true,
							Token = token
						});
					}
					else
					{
						return BadRequest(new
						{
							Success = false,
							Message = "Usuário ou senha incorretos."
						});
					}
				}
				catch (Exception ex)
				{
					return BadRequest(new
					{
						Success = false,
						Message = "Ocorreu um erro interno do sistema. Tente novamente mais tarde.",
						Errors = ex.Message
					});
				}
			}
			else
			{
				return BadRequest(new
				{
					Success = false,
					Message = "As informações informadas estão inválidas.",
					Errors = ModelState.Values.SelectMany(errors => errors.Errors)
				});
			}
		}

		[HttpGet]
		[Route("GetByEmail")]
		public IActionResult Get(string email)
		{
			User user = _userRepository.Get(email);

			if(user != null)
			{
				return Ok(new {
					Success = true,
					Message = $"Usuário '{user.Name}' foi encontrado."
				});
			}
			else
			{
				return Ok(new
				{
					Success = false,
					Message = $"Não foi encontrado usuários com o E-Mail '{email}' informado."
				});
			}
		}

		[HttpPost]
		public IActionResult Post(User user)
		{
			TryValidateModel(user);

			if (ModelState.IsValid)
			{
				_userRepository.Save(user);

				return Ok(new
				{
					Success = true,
					Message = $"O usuário '{user.Name}' foi cadastrado com sucesso.",
					Id = user.Id
				});
			}
			else
			{
				return BadRequest(new
				{
					Success = false,
					Message = "As informações informadas estão inválidas.",
					Errors = ModelState.Values.SelectMany(errors => errors.Errors)
				});
			}
		}
	}
}
