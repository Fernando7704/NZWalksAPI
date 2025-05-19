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
            ,IMapper mapper 
            ,ILogger<RegionsController> logger)
        {
            this.dbContextcs = dbContext;
            this.RegionRepositories = regionRepositories;
            this.mapper = mapper;
            this._logger = logger;
        }
        [HttpGet]
        //[Authorize(Roles ="Lector,Escritor")]
        public async Task<IActionResult> GetAll()
        {
            //try
            //{
            //    throw new Exception("This i a custom Exception");
                //_logger.LogInformation("GetAll Region Action was Invoked");
                //var regions = new List<Region>
                //{
                //    new Region
                //    {
                //        Id = Guid.NewGuid(),
                //        Name = "Auckland Region",
                //        Code = "AKL",
                //        RegionImagenUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR_ufbsEU3QR3LIQGiUOMx9jl-_53auBtaHcQ&s"
                //    },
                //    new Region
                //    {
                //        Id = Guid.NewGuid(),
                //        Name = "Wellington Region",
                //        Code = "WLG",
                //        RegionImagenUrl = "https://cdn.pensador.com/es/imagenes/buenos-dias-que-tu-dia-sea-liviano-y-lleno-de-sonrisas.jpg"
                //    }
                //};
                //var regionDomain = await dbContextcs.regions.ToListAsync();

                var regionDomain = await RegionRepositories.GetAllAsync();
                //var regionsDTO = new List<RegionDTO>();
                //foreach(var region in regionDomain)
                //{
                //    regionsDTO.Add(new RegionDTO
                //    {
                //        Id = region.Id,
                //        Code = region.Code,
                //        Name= region.Name,
                //        RegionImagenUrl = region.RegionImagenUrl
                //    });
                //}

                var regionsDTO = mapper.Map<List<RegionDTO>>(regionDomain);

                _logger.LogInformation("GetAll Region Action with data:" + JsonSerializer.Serialize(regionsDTO));

                //return Ok(mapper.Map<List<RegionDTO>>(regionDomain));
                return Ok(regionsDTO);

            //}
            //catch(Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //    throw;
            //}
            
           
            
            
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles ="Lector")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContextcs.regions.Find(id);

            //var region = await dbContextcs.regions.FirstOrDefaultAsync(x => x.Id == id);
            var region = await RegionRepositories.GetByIdAsync(id);

            var regionDTO = mapper.Map<RegionDTO>(region);

            //var regionDTO = new RegionDTO
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImagenUrl = region.RegionImagenUrl
            //};

            //if(region == null)
            if(regionDTO == null)
            {
                return NotFound();
            }
            return Ok(regionDTO);
        }

        //´POST TO CREATE A NEW REGION
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles ="Escritor")]
        //public async Task<IActionResult> Add([FromBody]RegionDTO regionDTO)
        public async Task<IActionResult> Add([FromBody] addRegionRequestDto addRegionRequestDto)
        {
            //regionDTO.Id = Guid.NewGuid();
            //var regionDomain = new Region
            //{
            //    Id = regionDTO.Id,
            //    Code = regionDTO.Code,
            //    Name = regionDTO.Name,
            //    RegionImagenUrl = regionDTO.RegionImagenUrl
            //};
            
                var regionReposity = mapper.Map<Region>(addRegionRequestDto);

                //await dbContextcs.regions.AddAsync(regionDomain);
                //await dbContextcs.SaveChangesAsync();
                regionReposity = await RegionRepositories.addRegionAsync(regionReposity);

                //var regionsDTO = new RegionDTO
                //{
                //    Id = regionDomain.Id,
                //    Code = regionDomain.Code,
                //    Name = regionDomain.Name,
                //    RegionImagenUrl = regionDomain.RegionImagenUrl
                //};
                var regionDto = mapper.Map<RegionDTO>(regionReposity);
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);            
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Escritor")]
        public async Task<IActionResult> update([FromRoute]Guid id, [FromBody] RegionDTO regionDTO)
        {
                var regionDomainModel = mapper.Map<Region>(regionDTO);
                regionDomainModel = await RegionRepositories.updateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //regionDomainModel.Code = regionDTO.Code;
                //regionDomainModel.Name = regionDTO.Name;
                //regionDomainModel.RegionImagenUrl = regionDTO.RegionImagenUrl;

                //await dbContextcs.SaveChangesAsync();

                //var regDTO = new RegionDTO
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImagenUrl = regionDomainModel.RegionImagenUrl

                //};
                var regDTO = mapper.Map<RegionDTO>(regionDTO);
                return Ok(regDTO);  
            //var region =await dbContextcs.regions.FirstOrDefaultAsync(x => x.Id == id);
            //var regionDomainModel = new Region
            //{
            //    //Id = regionDTO.Id,
            //    Code = regionDTO.Code,
            //    Name=regionDTO.Name,
            //    RegionImagenUrl = regionDTO.RegionImagenUrl
            //};
           


        }
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Escritor")]
        public async Task<IActionResult> delete([FromRoute]Guid id)
        {
            //var regions = await dbContextcs.regions.FirstOrDefaultAsync(x => x.Id == id);
            var regions = await RegionRepositories.deleteRegionAsync(id);
            if(regions == null)
            {
                return NotFound();
            }
            //dbContextcs.regions.Remove(regions);
            //await dbContextcs.SaveChangesAsync();


            //var reginDTO = new RegionDTO
            //{
            //    Id = regions.Id,
            //    Code = regions.Code,
            //    Name = regions.Name,
            //    RegionImagenUrl = regions.RegionImagenUrl
            //};

            var reginDTO = mapper.Map<RegionDTO>(regions);


            return Ok(reginDTO);

        }
    }
}
