using System.ComponentModel.DataAnnotations;

namespace Commander.DTOs
{
	public class CommandCreateDto
	{
		public int Id { get; set; } //normally we shouldn't place Id here as it is auto filled by DB

		[Required]
		[MaxLength(250)]
		public string HowTo { get; set; }

		[Required]
		[MaxLength(250)]
		public string Line { get; set; }

		[Required]
		[MaxLength(250)]
		public string Platform { get; set; }
	}
}