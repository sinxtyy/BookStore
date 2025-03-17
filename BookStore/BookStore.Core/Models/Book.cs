using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Models
{
	public class Book
	{
		public Book() { }

		public Book(string title, string autor, decimal price, string descritption,
			string publisher, string genre)
		{
			Title = title;
			Autor = autor;
			Price = price;
			Genre = genre;
			Description = descritption;
			Publisher = publisher;
		}

		[Key]
		public Guid Id { get; }

		public string Title { get; set; } = string.Empty;

		public string Autor { get; set; } = string.Empty;

		public decimal Price { get; set; }

		public string Genre { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string Publisher { get; set; } = string.Empty;

	}
}
