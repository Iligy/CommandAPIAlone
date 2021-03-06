using AutoMapper;
using CommandAPIAlone.Dtos.Command;
using CommandAPIAlone.Models;

namespace CommandAPIAlone.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<CommandReadDto, Command>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}
