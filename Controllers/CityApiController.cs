using Microsoft.AspNetCore.Mvc;
using WebApplication6.Dtos;
using WebApplication6.Services;

namespace WebApplication6.Controllers;

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
        var city = _cityService.GetCityList();
        return Ok(city);
    }

    [HttpPost]
    [Route("")]
    public IActionResult Create([FromBody] CreateCityRequest request)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var city = _cityService.CreateCity(request);
        return city == null ? Problem("There was some problem. See log for more details.") : Ok(city);
    }

    [HttpPut]
    [Route("{CityID:int}")]
    public IActionResult Update([FromBody] CreateCityRequest request, int CityID)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var city = _cityService.UpdateCity(CityID, request);
        return city == null ? NotFound("City not found.") : Ok(city);
    }

    [HttpDelete]
    [Route("{CityID:int}")]
    public IActionResult DeleteCity(int CityID)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var city = _cityService.DeleteCity(CityID);
        return city == null ? NotFound("City not found") : Ok(city);
    }

    [HttpPatch]
    [Route("{CityID:int}")]
    public IActionResult PatchCity([FromBody] PatchCityRequest request, int CityID)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var city = _cityService.PatchCity(CityID, request);
        return city == null ? NotFound("City not found.") : Ok(city);
    }
}