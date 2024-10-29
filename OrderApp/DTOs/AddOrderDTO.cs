using System.ComponentModel.DataAnnotations;

namespace TestTask1.DTO
{
	public class AddOrderDTO
	{
		public int Id { get; set; }	
		[Required]
		public int UnicId { get; set; }

		[Required]
		public double Weight { get; set; }

		[Required]
		public string Location { get; set; } = string.Empty;

		public DateTime DateTime { get; set; }
	}
}
