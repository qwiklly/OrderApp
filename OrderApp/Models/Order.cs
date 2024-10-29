namespace TestTask1.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int UnicId { get; set; }
		public double Weight { get; set; }
		public string Location { get; set; } = string.Empty;
		public DateTime DateTime { get; set; }
	}
}
