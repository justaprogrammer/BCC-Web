namespace BCC.Core.Services
{
    /// <summary>
    /// Wrapper of Circle environment information
    /// https://circleci.com/docs/2.0/env-vars/#built-in-environment-variables
    /// </summary>
    public class CircleEnvironmentDetails : IEnvironmentDetails
    {
        private readonly IEnvironmentProvider _environmentProvider;

        public CircleEnvironmentDetails(IEnvironmentProvider environmentProvider)
        {
            _environmentProvider = environmentProvider;
        }

        public string GitHubRepo => throw new System.NotImplementedException();

        public string GitHubOwner => throw new System.NotImplementedException();

        public string BuildFolder => throw new System.NotImplementedException();

        public string CommitHash => throw new System.NotImplementedException();
    }
}