using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
using WebApplication6.Services;

namespace WebApplication6.Controllers;

public class CityController : Controller
{
    private readonly CityService _cityService;
    private readonly StateService _stateService;

    public CityController(CityService cityService, StateService stateService)
    {
        _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
        _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
    }

    public IActionResult Index()
    {
        var cities = _cityService.GetCities();

        return View(cities);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new CityViewModel
        {
            States = _stateService.GetStates()
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CityViewModel city)
    {
        if (!ModelState.IsValid)
        {
            city.States = _stateService.GetStates(); // 🔥 REQUIRED
            return View(city);
        }

        _cityService.CreateCities(city);
        return RedirectToAction("Index");
    }
}