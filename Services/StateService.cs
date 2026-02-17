using WebApplication6.data;
using WebApplication6.Models;
namespace WebApplication6.Services
{
    public sealed class StateService
    {
        private readonly AppDbContext _dbcontext;
        public StateService(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(StateService));
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
    }
}
