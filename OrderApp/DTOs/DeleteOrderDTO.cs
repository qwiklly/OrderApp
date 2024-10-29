using System.ComponentModel.DataAnnotations;

namespace TestTask1.DTOs
{
	public class DeleteOrderDTO
	{
		[Required]
		public int UnicId { get; set; }
	}
}
