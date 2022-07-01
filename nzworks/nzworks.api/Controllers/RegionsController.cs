using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nzworks.api.Models.Domain;
using nzworks.api.Models.DTO;
using nzworks.api.Repositories;

namespace nzworks.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            /*var regions = new List<Region>()
            {
                new Region
                {
                    Id=Guid.NewGuid(),
                    Name="Wellington",
                    Code  ="WLG",
                    Area = 227755,
                    Lat = -1.8822,
                    Long = 299.88,
                    Population = 500000
                },
                      new Region
                {
                    Id=Guid.NewGuid(),
                    Name="Auckland",
                    Code = "AuCK",
                    Area = 227755,
                    Lat = -1.8822,
                    Long = 299.88,
                    Population = 500000
                
            };*/

            var regions = await regionRepository.GetAllAsync();

            //return DTO Regions 
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region => {
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Code = region.Code,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
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
            if(region == null)
            {
                return NotFound();
            }
            var regionDTO =  mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAync(AddRegionRequest addRegionRequest)
        {
            //Request to domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };
            //Pass details to repository
            region = await regionRepository.AddAsync(region);
            //Convert back to DTO
            var regionDTO = new Models.DTO.Region() {
                Id = region.Id,
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get Region from databse

            var region = await regionRepository.DeleteAsync(id);

            //if null notfound
            if(region == null)
            {
                return NotFound();
            }

            //Convert response back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //Return OK Reponse
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to Domain Model
            Models.Domain.Region region = new Models.Domain.Region() { 
                Id = id,
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat =  updateRegionRequest.Lat,
                Long= updateRegionRequest.Long,
                Population=  updateRegionRequest.Population
            };
            //Update Region using repository
            region = await regionRepository.UpdateAsync(id, region);
            //if null then not found
            if(region == null)
            {
                return NotFound();
            }
            //Convert Domain back to DTO
            Models.DTO.Region region1 = new Models.DTO.Region()
            {
                Id=region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            //return OK response
            return Ok(region1);
        }
    }
}
