using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();
            //var regionsDTO = new List<Models.DTO.Region>();

            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Area= region.Area,
            //        Code= region.Code,
            //        Lat= region.Lat,
            //        Long= region.Long,
            //        Population= region.Population,  
            //    };
            //    regionsDTO.Add(regionDTO);
            //});
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionsDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegionRequest addRegionRequest)
        {
            if (ModelState.IsValid)
            {
            //Request to domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
            };
            //Pass data to Repo
            var response = await regionRepository.AddAsync(region);
            //Conver back to dto
            var regionDto = new Models.DTO.Region()
            {
                Id = response.Id,
                Code = response.Code,
                Name = response.Name,
                Area = response.Area,
                Lat = response.Lat,
                Long = response.Long,
                Population = response.Population,

            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDto.Id }, response);

            } else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var response = await regionRepository.DeleteAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(response);
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            var region = new Models.Domain.Region()
            {
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Code = updateRegionRequest.Code,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population,
            };
            var response = await regionRepository.UpdateAsync(id, region);
            if (response == null)
            {
                return NotFound();
            }
            var regionDTO = new Models.DTO.Region()
            {
                Id = response.Id,
                Code = response.Code,
                Name = response.Name,
                Area = response.Area,
                Lat = response.Lat,
                Long = response.Long,
                Population = response.Population,

            };
            return Ok(regionDTO);
        }
    }
}
