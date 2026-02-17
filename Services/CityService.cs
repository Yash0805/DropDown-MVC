using Microsoft.EntityFrameworkCore;
using WebApplication6.data;
using WebApplication6.Models;


namespace WebApplication6.Services
{
    public class CityService
    {
        private readonly AppDbContext _dbContext;
        public CityService(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public IEnumerable<CityViewModel> GetCities()
        {
            IReadOnlyList<CityViewModel> cities = _dbContext.City
                .Include(c => c.State)
                .Select(c => new CityViewModel
                {
                    CityID = c.CityID,
                    CityName = c.CityName,
                    StateID = c.StateID,
                    States = null
                }).ToList();
            return cities;
        }

        public IEnumerable<CityViewModel> CreateCities(CityViewModel model)
        {
            if (!model.StateID.HasValue)
            {

                throw new ArgumentException("State is required");

            }
            City city = new()
            {
                CityName = model.CityName,
                StateID = model.StateID.Value
            };

            _dbContext.Add(city);
            _dbContext.SaveChanges();
            return GetCities();
        }
    }
}
