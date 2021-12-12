using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Models
{
	public class Game
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "O jogo deve ter um nome.")]
		[MinLength(5, ErrorMessage = "O jogo deve ter ao menos 5 caracteres.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "O jogo deve possuir uma descrição")]
		[MinLength(50, ErrorMessage = "O jogo deve ter ao menos 50 caracteres de descrição")]
		public string Description { get; set; }

		[Required(ErrorMessage = "O jogo deve possuir uma categoria.")]
		[MinLength(1, ErrorMessage = "O jogo deve possuir ao menos uma categoria.")]
		public ICollection<string> Categories { get; set; }

		[Required(ErrorMessage = "O jogo deve ter uma data de lançamento.")]
		public DateTime ReleaseDate { get; set; }

		public DateTime CreationDate { get; set; }

		public Game()
		{
			this.Id = new Random().Next(100000, 999999);
			this.CreationDate = DateTime.Now;
		}
	}
}
