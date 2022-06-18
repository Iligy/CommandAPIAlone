using CommandAPIAlone.Interfaces;
using CommandAPIAlone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPIAlone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {

        private readonly ICommandRepository _repository;

        public CommandsController(ICommandRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Command>>> GetAllCommands() 
        {
            IEnumerable<Command> commands = await _repository.GetAllCommandsAsync();

            return Ok(commands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Command>> GetCommandById(int id)
        {
            Command command = await _repository.GetCommandByIdAsync(id);

            if (command == null) 
            {
                return NotFound();
            }

            return Ok(command);
        }
    }
}
