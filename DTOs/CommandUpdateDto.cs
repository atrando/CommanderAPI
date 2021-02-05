﻿using System.ComponentModel.DataAnnotations;

namespace Commander.DTOs
{
	public class CommandUpdateDto
	{
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
