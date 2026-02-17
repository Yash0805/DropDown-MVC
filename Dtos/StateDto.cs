namespace WebApplication6.Dtos;

public sealed class StateDto(int StateID, string StateName, string Code, bool IsActive)
{
    public int StateID { get; } = StateID;
    public string StateName { get; } = StateName;
    public string Code { get; } = Code;
    public bool IsActive { get; } = IsActive;
}