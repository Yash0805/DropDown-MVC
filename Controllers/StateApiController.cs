using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication6.Dtos;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    [Route("api/master/state")]
    public sealed class StateApiController : ControllerBase
    {
        private readonly StateService _stateService;
        public StateApiController(StateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            IEnumerable<StateDto> states = _stateService.GetStateList();
            return Ok(states);
        }

        [HttpGet]
        [Route("{StateID:int}")]
        public IActionResult Get(int StateID)
        {
            StateDto? state = _stateService.GetState(StateID);
            return state == null ? NotFound("State not found.") : Ok(state);
        }


        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] CreateStateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            StateDto? state = _stateService.CreateState(request);
            return state is null ? Problem("There was some problem. See log for more details.") : Ok(state);
        }

        [HttpPut]
        [Route("{StateID:int}")]
        public IActionResult Update([FromBody] CreateStateRequest request, int StateID)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            StateDto? state = _stateService.UpdateState(StateID, request);
            return state == null ? NotFound("State not found.") : Ok(state);
        }

        [HttpDelete]
        [Route("{StateID:int}")]
        public IActionResult DeleteState( int StateID)
        {
            if(!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            StateDto? state = _stateService.DeleteState(StateID);
            return state == null ? NotFound("State not found.") : Ok(state);
        }
    }
}
