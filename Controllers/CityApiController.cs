using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication6.data;
using WebApplication6.Dtos;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    [Route("api/master/city")]
    public sealed class CityApiController : ControllerBase
    {
        private readonly CityService _cityService;
        public CityApiController(CityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            IEnumerable<CityDto> city = _cityService.GetCityList();
            return Ok(city);
        }
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody]  CreateCityRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            CityDto? city = _cityService.CreateCity(request);
            return city == null ? Problem("There was some problem. See log for more details.") : Ok(city);
        }
        [HttpPut]
        [Route("{CityID:int}")]
        public IActionResult Update([FromBody] CreateCityRequest request, int CityID)
        {
            if(!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            CityDto? city = _cityService.UpdateCity(CityID, request);
            return city == null ? NotFound("City not found.") : Ok(city);   
        }

        [HttpDelete]
        [Route("{CityID:int}")]
        public IActionResult DeleteCity(int CityID)
        {
            if(!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            CityDto? city = _cityService.DeleteCity(CityID);
            return city == null ? NotFound("City not found") : Ok(city);
        }
    }
}
