using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebApplication6.data;
using WebApplication6.Dtos;
using WebApplication6.Models;


namespace WebApplication6.Services
{
    public sealed class CityService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<CityService> _logger;
        public CityService(AppDbContext dbContext, ILogger<CityService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
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
        public IEnumerable<CityDto> GetCityList()
        {
            IReadOnlyList<CityDto> Cities = _dbContext.City
                .Include(static c => c.State)
                .Select(c => new CityDto(c.CityID, c.CityName, c.StateID)).ToArray();
            return Cities;
        }
        public CityDto? CreateCity(CreateCityRequest request)
        {
            try
            {
                City? city = _dbContext.City.FirstOrDefault(C => C.CityName == request.CityName);
                if (city is not null)
                {
                    throw new InvalidOperationException($"City with name {request.CityName} already exists.");
                }
                city = new City { CityName = request.CityName, StateID = request.StateID };
                _dbContext.City.Add(city);
                _dbContext.SaveChanges();
                return new CityDto(city.CityID, city.CityName, city.StateID);
            }
            catch (ConflictException ex)
            {
                _logger.LogError(ex, "Error while creating city with name {CityName}.Some conflict occured.", request.CityName);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                "Error while creating a city with name {CityName}. Problem in execution of sql query.",
                request.CityName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error While Creating a state with name {state}.", request);
            }
            return null;
        }
        public CityDto? UpdateCity(int CityID, CreateCityRequest request)
        {
            try
            {
                City? city = _dbContext.City.Find(CityID);
                if(city is null)
                {
                    return null;
                }
                city.CityName = request.CityName;
                city.StateID = request.StateID;
                _dbContext.SaveChanges();
                return new CityDto(city.CityID, city.CityName, city.StateID);   
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while updating a City name with State id  {CityName} {StateID}.", request.CityName, request.StateID);
                return null;
            }
        }
        public CityDto? GetCity(int CityID)
        {
            City? city = _dbContext.City.Find(CityID);
            if(city is null)
            {
                return null;
            }
            return new CityDto(city.CityID, city.CityName, city.StateID);
        }
        public CityDto? DeleteCity(int CityID)
        {
            try
            {
                City? city = _dbContext.City.FirstOrDefault(c => c.CityID == CityID);
                {
                    if (city is null)
                    {
                        return null;
                    }
                    _dbContext.City.Remove(city);
                    _dbContext.SaveChanges();
                    return new CityDto(city.CityID, city.CityName, city.StateID);
                }
            }
            catch (ConflictException ex)
            {
                _logger.LogError(ex, "Error while Deleting City with ID {CityId}.Some conflict occured.", CityID);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error While Creating a City with ID {CityId}.", CityID);
            }
            return null;
        }
    }
}
