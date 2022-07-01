using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nzworks.api.Models.Domain;
using nzworks.api.Models.DTO;
using nzworks.api.Repositories;

namespace nzworks.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalksRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = mapper.Map<List<Models.DTO.Walk>>(await this.walksRepository.GetAllAsync());
            return Ok(walks);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAync")]
        public async Task<IActionResult> GetAync(Guid id)
        {
            var walk = await this.walksRepository.GetAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Models.DTO.Walk>(walk));
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            var walkDomain = new Models.Domain.Walk()
            {
                FullName = addWalkRequest.FullName,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            walkDomain = await this.walksRepository.AddAsync(walkDomain);

            return CreatedAtAction(nameof(GetAync), new { id = walkDomain.Id }, mapper.Map<Models.DTO.Walk>(walkDomain));
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            var walkDomain = new Models.Domain.Walk()
            {
                Id = id,
                FullName = updateWalkRequest.FullName,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            walkDomain = await this.walksRepository.UpdateAsync(id, walkDomain);

            if (walkDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Models.DTO.Walk>(walkDomain));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> RemoveWalkAsync(Guid id)
        {
            var walkDomain = await this.walksRepository.DeleteAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Models.DTO.Walk>(walkDomain));
        }
    }
}
