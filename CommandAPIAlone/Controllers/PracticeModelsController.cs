using AutoMapper;
using CommandAPIAlone.Dtos;
using CommandAPIAlone.Interfaces;
using CommandAPIAlone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPIAlone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeModelsController : ControllerBase
    {

        private readonly IPracticeModelRepository _repository;
        private readonly IMapper _mapper;

        public PracticeModelsController(IPracticeModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PracticeModelReadDto>>> GetAllPracticeModels() 
        {
            IEnumerable<PracticeModel> practiceModels = await _repository.GetAllPracticeModelsAsync();

            return Ok(_mapper.Map<IEnumerable<PracticeModelReadDto>>(practiceModels));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PracticeModel>>> GetAllPracticeModelsById(int id)
        {
            var practiceModel = await _repository.GetPracticeModelByIdAsync(id);

            if (practiceModel == null) 
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<PracticeModel>(practiceModel));
        }
    }
}
