using CommandAPIAlone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPIAlone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands() 
        {
            var commands = new List<Command>()
            {
                new Command()
                {
                    Id = 1,
                    Description = "Create migration",
                    HowTo = "add-migration",
                    Platform = ".NET Core EF Package Manager"
                },
                new Command()
                {
                    Id = 2,
                    Description = "Remove migration",
                    HowTo = "remove-migration",
                    Platform = ".NET Core EF Package Manager"
                },
                new Command()
                {
                    Id = 3,
                    Description = "Apply migration",
                    HowTo = "update-database",
                    Platform = ".NET Core EF Package Manager"
                },
            };

            return commands;
        }
    }
}
