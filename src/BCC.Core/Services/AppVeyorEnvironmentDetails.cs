using System;
using System.Linq;

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

        public string GitHubRepo => _environmentProvider
            .GetEnvironmentVariable("APPVEYOR_REPO_NAME")
            .Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .First();

        public string GitHubOwner => _environmentProvider
            .GetEnvironmentVariable("APPVEYOR_REPO_NAME")
            .Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)
            .First();

        public string BuildFolder => _environmentProvider
            .GetEnvironmentVariable("APPVEYOR_BUILD_FOLDER");

        public string CommitHash => _environmentProvider
            .GetEnvironmentVariable("APPVEYOR_REPO_COMMIT");
    }
}