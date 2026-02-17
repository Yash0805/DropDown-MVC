using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    public class StateController : Controller
    {
        private readonly StateService _stateService;

        public StateController(StateService stateService)
        {
            _stateService = stateService ?? throw new ArgumentNullException(nameof(StateService));
        }
        public IActionResult Index()
        {
            IEnumerable<StateViewModel> states = _stateService.GetStates();
            return View(states);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StateViewModel state)
        {
            if (!ModelState.IsValid)
            {
                return View(state);
            }
            IEnumerable<StateViewModel> result = _stateService.CreateState(state);
            return RedirectToAction(nameof(Index));
        }
    }
}