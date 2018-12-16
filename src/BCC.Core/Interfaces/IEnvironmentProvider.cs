namespace BCC.Core.Services
{
    public interface IEnvironmentProvider
    {
        string GetEnvironmentVariable(string variable);
    }
}