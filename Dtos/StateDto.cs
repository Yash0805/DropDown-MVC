namespace WebApplication6.Dtos
{
    public sealed class StateDto(int StateID, string StateName, string Code)
    {
        public int StateID { get; } = StateID;
        public string StateName { get; } = StateName;
        public string Code { get; } = Code;
    }
}
