namespace BCC.Core.Services
{
    public interface IEnvironmentDetails
    {
        string GitHubRepo { get; }
        string GitHubOwner { get; }
        string BuildFolder { get; }
        string CommitHash { get; }
    }
}