using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nzworks.api.Models.DTO;
using nzworks.api.Repositories;

namespace nzworks.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDificultyRepository walkDificultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDificultyRepository walkDificultyRepository, IMapper mapper)
        {
            this.walkDificultyRepository = walkDificultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var allWDDomain = await this.walkDificultyRepository.GetAllAsync();
            var allWDDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(allWDDomain);
            return Ok(allWDDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var wdDomain = await walkDificultyRepository.GetAsync(id);
            if(wdDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Models.DTO.WalkDifficulty>(wdDomain));
        }

        [HttpPost]
        public async Task<IActionResult> AddSync(AddWalkDificultyRequest addWalkDificultyRequest)
        {
            var walkDFDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDificultyRequest.Code
            };
            walkDFDomain = await this.walkDificultyRepository.AddSync(walkDFDomain);
            var walkDFDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDFDomain);
            return CreatedAtAction(nameof(GetAsync), new { id = walkDFDTO.Id}, walkDFDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var wdDomain = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };

            wdDomain = await this.walkDificultyRepository.UpdateSync(id, wdDomain);
            if(wdDomain == null)
            {
                return NotFound();
            }

            var wdDTO = mapper.Map<Models.DTO.WalkDifficulty>(wdDomain);
            return Ok(wdDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var wdDomain = await this.walkDificultyRepository.DeleteAsync(id);

            if(wdDomain == null)
            {
                return NotFound();
            }

            return Ok(wdDomain);
        }
    }
}
