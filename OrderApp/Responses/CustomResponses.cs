namespace TestTask1.Responses
{
	public class CustomResponses
	{
		public record BaseResponse(bool Flag = true, string Message = null!);
	}
}
