using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NZWalksApi.CustomActionFilters;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;
using NZWalksApi.Models.DTO;
using NZWalksApi.Repositories;
using System.Text.Json;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContextcs dbContextcs;

        public readonly IRegionRepositories RegionRepositories;

        public readonly IMapper mapper;
        public readonly ILogger _logger;
        public RegionsController(
            NZWalksDbContextcs dbContext,
            IRegionRepositories regionRepositories
            , IMapper mapper
            , ILogger<RegionsController> logger)
        {
            this.dbContextcs = dbContext;
            this.RegionRepositories = regionRepositories;
            this.mapper = mapper;
            this._logger = logger;
        }
        [HttpGet]
        [Authorize(Roles = "Lector,Escritor")]
        public async Task<IActionResult> GetAll()
        {

            var regionDomain = await RegionRepositories.GetAllAsync();


            var regionsDTO = mapper.Map<List<RegionDTO>>(regionDomain);

            _logger.LogInformation("GetAll Region Action with data:" + JsonSerializer.Serialize(regionsDTO));


            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Lector")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            var region = await RegionRepositories.GetByIdAsync(id);

            var regionDTO = mapper.Map<RegionDTO>(region);


            if (regionDTO == null)
            {
                return NotFound();
            }
            return Ok(regionDTO);
        }

        //´POST TO CREATE A NEW REGION
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Escritor")]
        public async Task<IActionResult> Add([FromBody] addRegionRequestDto addRegionRequestDto)
        {


            var regionReposity = mapper.Map<Region>(addRegionRequestDto);
            regionReposity = await RegionRepositories.addRegionAsync(regionReposity);
            var regionDto = mapper.Map<RegionDTO>(regionReposity);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Escritor")]
        public async Task<IActionResult> update([FromRoute] Guid id, [FromBody] RegionDTO regionDTO)
        {
            var regionDomainModel = mapper.Map<Region>(regionDTO);
            regionDomainModel = await RegionRepositories.updateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }       
            var regDTO = mapper.Map<RegionDTO>(regionDTO);
            return Ok(regDTO);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Escritor")]
        public async Task<IActionResult> delete([FromRoute] Guid id)
        {
            
            var regions = await RegionRepositories.deleteRegionAsync(id);
            if (regions == null)
            {
                return NotFound();
            }
            var reginDTO = mapper.Map<RegionDTO>(regions);


            return Ok(reginDTO);

        }
    }
}
