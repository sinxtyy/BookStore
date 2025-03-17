using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Models
{
	public class Transaction
	{
		public Transaction(){ }

		public Transaction(string addresser, string destination,
			decimal amount, string type, DateTime date)
		{
			Addresser = addresser;
			Destination = destination;
			Amount = amount;
			Type = type;
			Date = date;
		}

		[Key]
		public Guid Id { get; }

		public string Addresser { get; set; } = string.Empty;

		public string Destination { get; set; } = string.Empty;

		public decimal Amount { get; set; }

		public string Type { get; set; } = string.Empty;

		public DateTime Date { get; set; }
	}
}
