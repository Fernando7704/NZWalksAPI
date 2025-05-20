using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Models.DTO;
using NZWalksApi.Repositories;

namespace NZWalksApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class DifficultyController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IDifficulty _dificulty;
        public DifficultyController(
            IMapper maper,
            IDifficulty dificulty
            )
        {
            _mapper = maper;
            _dificulty = dificulty;

        }
        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            var modelDomain = await _dificulty.GetDifficultyListAsync();
            var modelDTO = _mapper.Map<List<Models.DTO.DifficultyDTO>>(modelDomain);
            return Ok(modelDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var modelDomain = await _dificulty.GetDifficultyById(id);
            if (modelDomain == null)
            {
                return NotFound();
            }
            var modelDTO = _mapper.Map<Models.DTO.DifficultyDTO>(modelDomain);
            return Ok(modelDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.DTO.DifficultyDTO addDifficultyDTO)
        {
            if (!ValidateDifficulty(addDifficultyDTO))
            {
                return BadRequest();
            }
            addDifficultyDTO.id = Guid.NewGuid();
            var modelDomain = _mapper.Map<Models.Domain.Difficulty>(addDifficultyDTO);
            var modelDomainResponse = await _dificulty.CreateDifficultyAsync(modelDomain);
            var modelDTO = _mapper.Map<Models.DTO.DifficultyDTO>(modelDomainResponse);
            return Ok(modelDTO);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Models.DTO.DifficultyDTO updateDifficultyDTO)
        {
            updateDifficultyDTO.id = id;
            var modelDomain = _mapper.Map<Models.Domain.Difficulty>(updateDifficultyDTO);
            var modelDomainResponse = await _dificulty.UpdateDifficultyAsync(id, modelDomain);
            if (modelDomainResponse == null)
            {
                return NotFound();
            }
            var modelDTO = _mapper.Map<Models.DTO.DifficultyDTO>(modelDomainResponse);
            return Ok(modelDTO);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            
            var modelDomainResponse = await _dificulty.DeleteDifficultyAsync(id);
            if (modelDomainResponse == null)
            {
                return NotFound();
            }
            var modelDTO = _mapper.Map<Models.DTO.DifficultyDTO>(modelDomainResponse);
            return Ok(modelDTO);
        }

        #region Private Methods
        private bool ValidateDifficulty(DifficultyDTO difficultyDTO)
        {
            if (Guid.TryParse(difficultyDTO.id.ToString(), out Guid id) == false)
            {
                ModelState.AddModelError(nameof(difficultyDTO.id), $"{nameof(difficultyDTO.Name)} El id no es un formato valido.");
            }
            if (difficultyDTO == null)
            {
                ModelState.AddModelError(nameof(difficultyDTO), $"{nameof(difficultyDTO.Name)} La dificultad es requerida!");
            }
            if (string.IsNullOrWhiteSpace(difficultyDTO.Name))
            {
                ModelState.AddModelError(nameof(difficultyDTO.Name), $"{nameof(difficultyDTO.Name)} No puede ser espacios vacios.");
            }
            if (difficultyDTO.id == Guid.Empty)
            {
                ModelState.AddModelError(nameof(difficultyDTO.id), $"{nameof(difficultyDTO.Name)} El id no puede estar en blanco.");
            }
           
            return true;
        }

        #endregion

    }
}
