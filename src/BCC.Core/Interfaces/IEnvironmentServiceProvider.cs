namespace BCC.Core.Services
{
    public interface IEnvironmentServiceProvider
    {
        IEnvironmentService GetEnvironmentService();
    }
}