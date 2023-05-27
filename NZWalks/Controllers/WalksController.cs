﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkRepository.GetAll();
            var walkDto = mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walkDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var walk = await walkRepository.GetAsync(Id);
            if(walk == null) return NotFound();
            var walkdto = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkdto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalk(AddWalkRequest newWalk)
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
        public async Task<IActionResult> UpdateWalk (Guid Id, UpdateWalkRequest newWalk)
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
