namespace BCC.Core.Services
{
    /// <summary>
    /// Wrapper of AppVeyor environment information
    /// https://www.appveyor.com/docs/environment-variables/
    /// </summary>
    public class AppVeyorEnvironmentDetails : IEnvironmentDetails
    {
        private readonly IEnvironmentProvider _environmentProvider;

        public AppVeyorEnvironmentDetails(IEnvironmentProvider environmentProvider)
        {
            _environmentProvider = environmentProvider;
        }
    }
}