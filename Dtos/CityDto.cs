namespace WebApplication6.Dtos;

public sealed class CityDto(int CityID, string CityName, int StateID,bool IsActive)
{
    public int CityID { get; } = CityID;
    public string CityName { get; } = CityName;
    public int StateID { get; } = StateID;
    public bool IsActive { get; } = IsActive;
}