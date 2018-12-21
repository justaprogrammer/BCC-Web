namespace BCC.Core.Services
{
    public abstract class EnvironmentDetailsBase : IEnvironmentDetails
    {
        protected IEnvironmentProvider Environment { get; }

        public EnvironmentDetailsBase(IEnvironmentProvider environmentProvider)
        {
            Environment = environmentProvider;
        }

        public abstract string GitHubRepo { get; }
        public abstract string GitHubOwner { get; }
        public abstract string BuildFolder { get; }
        public abstract string CommitHash { get; }

        public string BuildCrossCheckToken => Environment
            .GetEnvironmentVariable("BCC_TOKEN");
    }
}