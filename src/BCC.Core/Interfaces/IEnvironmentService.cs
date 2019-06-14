namespace BCC.Core.Services
{
    public interface IEnvironmentService
    {
        string GitHubRepo { get; }
        string GitHubOwner { get; }
        string CloneRoot { get; }
        string CommitHash { get; }
        string BuildCrossCheckToken { get; }
    }
}