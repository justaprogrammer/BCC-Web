using System;
using System.Linq;

namespace BCC.Core.Services
{
    /// <summary>
    /// Wrapper of Travis environment information
    /// https://docs.travis-ci.com/user/environment-variables/
    /// </summary>
    public class TravisEnvironmentDetails : EnvironmentDetailsBase
    {
        public TravisEnvironmentDetails(IEnvironmentProvider environmentProvider) : base(environmentProvider)
        {
        }

        public override string GitHubRepo => Environment
            .GetEnvironmentVariable("TRAVIS_REPO_SLUG")
            .Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .First();

        public override string GitHubOwner => Environment
            .GetEnvironmentVariable("TRAVIS_REPO_SLUG")
            .Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)
            .First();

        public override string BuildFolder => Environment.GetEnvironmentVariable("TRAVIS_BUILD_DIR");

        public override string CommitHash => Environment.GetEnvironmentVariable("TRAVIS_COMMIT");
    }
}