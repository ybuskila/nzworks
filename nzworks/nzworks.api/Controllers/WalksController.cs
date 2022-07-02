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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDificultyRepository walkDificultyRepository;

        public WalksController(IWalksRepository walksRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDificultyRepository walkDificultyRepository)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDificultyRepository = walkDificultyRepository;
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
            if (!(await ValidateAddWalkAsync(addWalkRequest)))
            {
                return BadRequest(ModelState);
            }
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
            if(!(await ValidateUpdateWalkAsync(updateWalkRequest)))
            {
                return BadRequest(ModelState);
            }
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

       
        private async Task<bool> ValidateAddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            if (addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest), $"Add Walk is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addWalkRequest.FullName))
            {
                ModelState.AddModelError(nameof(addWalkRequest.FullName), $"${nameof(addWalkRequest.FullName)} cannot be null or empty or white space");
            }
            if (addWalkRequest.Length < 0)
            {
                ModelState.AddModelError(nameof(addWalkRequest.Length), $"${nameof(addWalkRequest.Length)} should be greater than zero");
            }

            var region = await regionRepository.GetAsync(addWalkRequest.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId), $"${nameof(addWalkRequest.RegionId)} is invalid");
            }

            var walkdf = await walkDificultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);

            if (walkdf == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId), $"${nameof(addWalkRequest.WalkDifficultyId)} is invalid");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest), $"Add Walk is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkRequest.FullName))
            {
                ModelState.AddModelError(nameof(updateWalkRequest.FullName), $"${nameof(updateWalkRequest.FullName)} cannot be null or empty or white space");
            }
            if (updateWalkRequest.Length < 0)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Length), $"${nameof(updateWalkRequest.Length)} should be greater than zero");
            }

            var region = await regionRepository.GetAsync(updateWalkRequest.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId), $"${nameof(updateWalkRequest.RegionId)} is invalid");
            }

            var walkdf = await walkDificultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);

            if (walkdf == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId), $"${nameof(updateWalkRequest.WalkDifficultyId)} is invalid");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
    }
}
