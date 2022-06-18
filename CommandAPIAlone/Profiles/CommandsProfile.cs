using AutoMapper;
using CommandAPIAlone.Dtos;
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
        }
    }
}
