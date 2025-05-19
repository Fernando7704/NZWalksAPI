using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.CustomActionFilters;
using NZWalksApi.Models.Domain;
using NZWalksApi.Models.DTO;
using NZWalksApi.Repositories;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalksRepositories walksRepositories;
        public WalksController(
            IMapper mapper,
            IWalksRepositories walksRepositories
            )
        {
            this._mapper = mapper;
            this.walksRepositories = walksRepositories;

        }
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Escritor")]
        public async Task<IActionResult> Create([FromBody] addRequestWalkDTO addRequestWalkDTO) 
        {
           
                var modelMapperdomain = _mapper.Map<Walk>(addRequestWalkDTO);

                var modelDomain = await walksRepositories.CreateAsync(modelMapperdomain);
                var modelDTO = _mapper.Map<Walk, WalksDTO>(modelDomain);

                return Ok(modelDTO);
            
        }

        [HttpGet]
        [Authorize(Roles = "Lector")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? ascendente,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pagesize=1000)
        {
            var modelDomain  = await walksRepositories.GetAllAsync(
                filterOn,
                filterQuery,
                sortBy,
                ascendente ??true,
                pageNumber,
                pagesize);

            var modelDTO = _mapper.Map<List<WalksDTO>>(modelDomain);

            return Ok(modelDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Lector")]
        public async Task<IActionResult> GetByID([FromRoute]Guid id)
        {
            var modelDomain = await walksRepositories.GetByIdAsync(id);

            if(modelDomain == null)
            {
                return NotFound();
            }

            return Ok(modelDomain);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Escritor")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
        {   
                var walkDomain = _mapper.Map<Walk>(updateWalkRequestDTO);
                await walksRepositories.UpdateAsync(id, walkDomain);
                if (walkDomain == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<WalksDTO>(walkDomain));           
          
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Escritor")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var deleteWalk= await walksRepositories.DeleteAsync(id);
            if (deleteWalk == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalksDTO>(deleteWalk));
        }
    }
}
