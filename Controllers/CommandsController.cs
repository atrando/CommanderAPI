﻿using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
	//Could be done by "api/[controller] -> [controller] cuts the "Commands" part from the CommandsController class name. 
	//There can be a problem if the implementation of this class would change. Then api endpoint will be changed as well
	[Route("api/commands")]
	[ApiController]
	public class CommandsController : ControllerBase
	{
		private readonly ICommanderRepo _repository;
		private readonly IMapper _mapper;

		public CommandsController(ICommanderRepo repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		//GET api/commands
		[HttpGet]
		public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
		{
			var commandItems = _repository.GetAllCommands();
			return Ok(commandItems);
		}

		//GET api/commands
		[HttpGet("{check}", Name = "CheckAPI")]
		public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands(string check)
		{
			return Ok(check);
		}

		//GET api/commands/{id}
		[HttpGet("{id}", Name = "GetCommandById")]
		public ActionResult<CommandReadDto> GetCommandById(int id)
		{
			var commandItem = _repository.GetCommandById(id);
			if (commandItem != null)
			{
				return Ok(_mapper.Map<CommandReadDto>(commandItem));
			}

			return NotFound();
		}

		//POST api/commands/
		[HttpPost]
		public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
		{
			var commandModel = _mapper.Map<Command>(commandCreateDto);
			_repository.CreateCommand(commandModel);
			_repository.SaveChanges();

			var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
			return CreatedAtRoute(nameof(GetCommandById), new {commandReadDto.Id}, commandReadDto);
		}

		//PUT api/commands/{id} //requires to fill each field of the command even when we want to update one of the fields
		[HttpPut("{id}")]
		public ActionResult<CommandReadDto> UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
		{
			var commandModelFromRepo = _repository.GetCommandById(id);
			if (commandModelFromRepo == null)
			{
				return NotFound();
			}

			_mapper.Map(commandUpdateDto, commandModelFromRepo); //entity framework knows that there was a change in the command model object
			_repository.UpdateCommand(commandModelFromRepo); //Doing nothing due to the EF possibilities 
			_repository.SaveChanges();

			return NoContent();
		}

		//PATCH api/commands/{id} //Patch allows to update the command field not without filling each command field
		[HttpPatch("{id}")]
		public ActionResult<CommandReadDto> PatchCommand(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
		{
			var commandModelFromRepo = _repository.GetCommandById(id);
			if (commandModelFromRepo == null)
			{
				return NotFound();
			}

			var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
			patchDoc.ApplyTo(commandToPatch, ModelState);

			if (!TryValidateModel(commandToPatch))
			{
				return ValidationProblem(ModelState);
			}

			_mapper.Map(commandToPatch, commandModelFromRepo);
			_repository.UpdateCommand(commandModelFromRepo);
			_repository.SaveChanges();

			return NoContent();
		}

		//DELETE api/commands/{id}
		[HttpDelete("{id}")]
		public ActionResult<CommandReadDto> DeleteCommand(int id)
		{
			var commandModelFromRepo = _repository.GetCommandById(id);
			if (commandModelFromRepo == null)
			{
				return NotFound();
			}

			_repository.DeleteCommand(commandModelFromRepo);
			_repository.SaveChanges();

			return Ok(_mapper.Map<CommandReadDto>(commandModelFromRepo));
		}
	}
}