using AutoMapper;
using CommandAPIAlone.Dtos.Command;
using CommandAPIAlone.Interfaces;
using CommandAPIAlone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPIAlone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommandsController : ControllerBase
    {

        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetAllCommands() 
        {
            IEnumerable<Command> commands = await _repository.GetAllCommandsAsync();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
        {
            Command? command = await _repository.GetCommandByIdAsync(id);

            if (command == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandCreateDto commandCreateDto) 
        {
            var command = _mapper.Map<Command>(commandCreateDto);
            await _repository.CreateCommandAsync(command);
            await _repository.SaveChangesAsync();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(int id, CommandUpdateDto commandUpdateDto) 
        {

            var commandToUpdate = await _repository.GetCommandByIdAsync(id);

            if (commandToUpdate == null) 
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commandToUpdate);
            await _repository.UpdateCommandAsync(commandToUpdate);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> ParticalCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc) 
        {
            if (patchDoc != null)
            {
                var commandFromRepo = await _repository.GetCommandByIdAsync(id);

                if (commandFromRepo == null) 
                {
                    return NotFound();
                }

                var commandToPatch = _mapper.Map<CommandUpdateDto>(commandFromRepo);

                patchDoc.ApplyTo(commandToPatch, ModelState);

                if (!TryValidateModel(commandToPatch))
                {
                    return BadRequest(ModelState);
                }

                _mapper.Map(commandToPatch, commandFromRepo);

                await _repository.UpdateCommandAsync(commandFromRepo);
                await _repository.SaveChangesAsync();

                return NoContent();
            }
            else 
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommand(int id)
        {
            var commandToDelete = await _repository.GetCommandByIdAsync(id);

            if (commandToDelete == null) 
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandToDelete);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
