using AutoMapper;
using CommandAPIAlone.Dtos;
using CommandAPIAlone.Models;

namespace CommandAPIAlone.Profiles
{
    public class PracticeModelsProfile : Profile
    {
        public PracticeModelsProfile()
        {
            CreateMap<PracticeModel, PracticeModelReadDto>();
        }
    }
}
