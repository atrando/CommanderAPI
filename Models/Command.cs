using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
	public class Command
	{
		[Key] //We don't need to really do it - Entity Framework knows it from the start
		public int Id { get; set; }

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