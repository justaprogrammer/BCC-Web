namespace BCC.Core.Services
{
    public interface IEnvironmentService
    {
        string GitHubRepo { get; }
        string GitHubOwner { get; }
        string BuildFolder { get; }
        string CommitHash { get; }
        string BuildCrossCheckToken { get; }
    }
}