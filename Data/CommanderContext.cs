using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
	public class CommanderContext : DbContext
	{
		public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
		{
			
		}

		//If we would have more models then it should be also placed here. Otherwise models won't be in the database
		public DbSet<Command> Commands { get; set; }
	}
}
