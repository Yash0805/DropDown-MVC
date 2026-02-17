using Microsoft.EntityFrameworkCore;
using WebApplication6.data;
using WebApplication6.Dtos;
using WebApplication6.Models;
namespace WebApplication6.Services
{
    public sealed class StateService
    {
        private readonly AppDbContext _dbcontext;
        private readonly ILogger<StateService> _logger;

        public StateService(AppDbContext dbContext, ILogger<StateService> logger)
        {
            _dbcontext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
        }

        public IEnumerable<StateViewModel> GetStates()
        {
            IReadOnlyList<StateViewModel> states = _dbcontext.State
                .Select(s => new StateViewModel
                {
                    StateID = s.StateID,
                    StateName = s.StateName,
                    Code = s.Code
                }).ToList();
            return states;
        }
        public IEnumerable<StateViewModel> CreateState(StateViewModel model)
        {
            State state = new()
            {
                StateName = model.StateName,
                Code = model.Code
            };
            _dbcontext.State.Add(state);
            _dbcontext.SaveChanges();
            return GetStates();
        }

        public IEnumerable<StateDto> GetStateList()
        {
            IReadOnlyList<StateDto> state = _dbcontext.State
                .Select(state => new StateDto(state.StateID, state.StateName, state.Code)).ToArray();
            return state;
        }
        public StateDto? CreateState(CreateStateRequest request)
        {
            try
            {
                State? state = _dbcontext.State.FirstOrDefault(S => S.Code == request.Code);
                if (state is not null)
                {
                    throw new ConflictException($"State with code {request.Code} already exists.");
                }
                state = new State { StateName = request.StateName, Code = request.Code };
                _dbcontext.State.Add(state);
                _dbcontext.SaveChanges();
                return new StateDto(state.StateID, state.StateName, state.Code);
            }
            catch (ConflictException ex)
            {
                _logger.LogError(ex, "Error while creating state with name {StateName}.Some conflict occured.", request.StateName);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                "Error while creating a state with name {stateName}. Problem in execution of sql query.",
                request.StateName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error While Creating a state with name {state}.", request);
            }
            return null;
        }
        public StateDto? UpdateState(int StateID, CreateStateRequest request)
        {
            try
            {
                State? state = _dbcontext.State.Find(StateID);

                if (state is null)
                {
                    return null;
                }

                state.StateName = request.StateName;
                state.Code = request.Code;

                _dbcontext.SaveChanges();

                return new StateDto(state.StateID, state.StateName, state.Code);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while updating a state with name {stateName} {code}.", request.StateName,request.Code);
                return null;
            }
        }
        public StateDto? GetState(int StateID)
        {
            State? state = _dbcontext.State.Find(StateID);
            if (state is null)
            {
                return null;
            }
            return new StateDto(state.StateID, state.StateName, state.Code);
        }
        public StateDto? DeleteState(int StateID)
        {
            try
            {
                State? state = _dbcontext.State.FirstOrDefault(s => s.StateID == StateID);
                if(state is null)
                {
                    return null;
                }
                _dbcontext.State.Remove(state);
                _dbcontext.SaveChanges();
                return new StateDto(state.StateID, state.StateName, state.Code);
            }
            catch (ConflictException ex)
            {
                _logger.LogError(ex, "Error while Deleting state with ID {StateID}.Some conflict occured.", StateID);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error While Creating a state with ID {StateID}.", StateID);
            }
            return null;
        }
    }
}
