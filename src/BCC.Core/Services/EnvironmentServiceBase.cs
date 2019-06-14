namespace BCC.Core.Services
{
    public abstract class EnvironmentServiceBase : IEnvironmentService
    {
        protected IEnvironmentProvider Environment { get; }

        public EnvironmentServiceBase(IEnvironmentProvider environmentProvider)
        {
            Environment = environmentProvider;
        }

        public abstract string GitHubRepo { get; }
        public abstract string GitHubOwner { get; }
        public abstract string CloneRoot { get; }
        public abstract string CommitHash { get; }

        public string BuildCrossCheckToken => Environment
            .GetEnvironmentVariable("BCC_TOKEN");
    }
}