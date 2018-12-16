namespace BCC.Core.Services
{
    /// <summary>
    /// Wrapper of Travis environment information
    /// https://docs.travis-ci.com/user/environment-variables/
    /// </summary>
    public class TravisEnvironmentDetails : IEnvironmentDetails
    {
        private readonly IEnvironmentProvider _environmentProvider;

        public TravisEnvironmentDetails(IEnvironmentProvider environmentProvider)
        {
            _environmentProvider = environmentProvider;
        }
    }
}