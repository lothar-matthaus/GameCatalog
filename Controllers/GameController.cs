using GameCatalog.Models;
using GameCatalog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCatalog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class GameController : ControllerBase
	{
		private readonly IGameRepository _gameRepository;

		public GameController(IGameRepository gameRepository)
		{
			this._gameRepository = gameRepository;
		}

		[HttpDelete("Delete")]
		public IActionResult Delete(int id)
		{
			try
			{
				Game game = _gameRepository.Get(id);

				if (game != null)
				{
					_gameRepository.Delete(id);

					return Ok(new
					{
						Success = true,
						Message = $"O título '{game.Name}' foi removido com sucesso."
					});
				}
				else
				{
					return Ok(new
					{
						Success = false,
						Message = $"Não foi encontrado o jogo com o identificador '{id}' informado."
					});
				}

			}
			catch (Exception ex)
			{
				return BadRequest(new
				{
					Success = false,
					Message = ex.Message
				});
			}
		}

		[HttpPatch]
		[Route("Update")]
		public IActionResult Patch(Game game)
		{
			TryValidateModel(game);

			if (ModelState.IsValid)
			{
				try
				{
					bool wasUpdated = _gameRepository.Update(game);

					if (wasUpdated)
					{
						return Ok(new
						{
							Success = true,
							Message = $"O título {0} foi atualizado com sucesso.",
							game.Name,
							Id = game.Id
						});
					}
					else
					{
						return BadRequest(new
						{
							Success = false,
							Message = $"Não foi possível atualizar o título '{game.Name}'. Verifique as informações corretamente.",
							Id = game.Id
						});
					}
					
				}
				catch (Exception ex)
				{
					return BadRequest(new
					{
						Success = false,
						Message = ex.Message
					});
				}
			}
			else
			{
				return BadRequest(new
				{
					Success = false,
					Message = ModelState.GetValidationState("errors")
				});
			}
		}

		[AllowAnonymous]
		[HttpGet("GetById")]
		public IActionResult Get(int id)
		{
			Game result = _gameRepository.Get(id);

			if (result == null)
				return Ok(new
				{
					Success = true,
					Message = "Não foi encontrado nenhum título."
				});

			else
			{
				return Ok(result);
			}
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Get()
		{
			ICollection<Game> results = _gameRepository.GetAll();

			if (results.Count == 0)
				return Ok(new
				{
					Success = true,
					Message = "Não foi encontrado nenhum título."
				});

			else
			{
				return Ok(results);
			}
		}

		[HttpPost]
		public IActionResult Post(Game game)
		{
			TryValidateModel(game);

			if (ModelState.IsValid)
			{
				try
				{
					_gameRepository.Save(game);

					return Ok(new
					{
						Success = true,
						Message = $"O jogo foi {game.Name} cadastrado com sucesso.",
						Id = game.Id
					});
				}
				catch (Exception ex)
				{
					return BadRequest(new
					{
						Message = ex.Message,
						StatusCode = 400
					});
				}
			}
			else
			{
				return BadRequest(new
				{
					Message = ModelState.GetFieldValidationState("erros"),
					status = 400
				});
			}
		}
	}
}
