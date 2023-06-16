using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.CustomActionFilter;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        // api/Walks?filterOn=Name&filterQuery=Track
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortby, [FromQuery] bool? isAsc,
            [FromQuery] int pageNumber =1 , [FromQuery] int pageSize = 1000)
        {
            var walks = await walkRepository.GetAll(filterOn,filterQuery, sortby, isAsc ?? true,pageNumber, pageSize);
            var walkDto = mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walkDto);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var walk = await walkRepository.GetAsync(Id);
            if(walk == null) return NotFound();
            var walkdto = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkdto);
        }

        [HttpPost]
        [ValidateModelAttribute]
        public async Task<IActionResult> AddWalk([FromBody] AddWalkRequest newWalk)
        { 
            var domWalk = new Models.Domain.Walk()
            {
                Name = newWalk.Name,
                Length = newWalk.Length,
                RegionId = newWalk.RegionId,
                WalkDifficultyId = newWalk.WalkDifficultyId ,
            };
            var walk = await walkRepository.AddAsync(domWalk);
            if(walk == null) return NotFound();
            var walkdto = mapper.Map<Models.DTO.Walk>(walk);
            return CreatedAtAction(nameof(GetById), new { Id = walkdto.Id }, walkdto); 
 
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateWalk (Guid Id,[FromBody] UpdateWalkRequest newWalk)
        {
            var domWalk = new Models.Domain.Walk()
            {
                Name = newWalk.Name,
                Length = newWalk.Length,
                RegionId = newWalk.RegionId,
                WalkDifficultyId = newWalk.WalkDifficultyId,
            };
            var walk = await walkRepository.UpdateAsync(Id, domWalk);
            if(walk == null) return NotFound();
            var walkdto = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkdto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWalk (Guid Id)
        {
            var walk =await walkRepository.DeleteAsync(Id);
            if(walk == null) return NotFound();
            var walkDto = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDto);
        }
    }
}
