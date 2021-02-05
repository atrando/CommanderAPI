using AutoMapper;
using Commander.DTOs;
using Commander.Models;

namespace Commander.Profiles
{
	//Class to connect the Command with CommandReadDto to provide correct mapping 
	//One profile class for each domain object
	public class CommandsProfile : Profile
	{
		public CommandsProfile()
		{
			//From automapper - method mapping the source object to target object
			CreateMap<Command, CommandReadDto>(); 
			CreateMap<CommandCreateDto, Command>();
			CreateMap<CommandUpdateDto, Command>();
			CreateMap<Command, CommandUpdateDto>();
		}
	}
}