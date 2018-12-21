﻿namespace BCC.Core.Services
{
    /// <summary>
    /// Wrapper of Circle environment information
    /// https://circleci.com/docs/2.0/env-vars/#built-in-environment-variables
    /// </summary>
    public class CircleEnvironmentDetails : EnvironmentDetailsBase
    {
        public CircleEnvironmentDetails(IEnvironmentProvider environmentProvider) : base(environmentProvider)
        {
        }

        public override string GitHubRepo => Environment.GetEnvironmentVariable("CIRCLE_PROJECT_REPONAME");

        public override string GitHubOwner => Environment.GetEnvironmentVariable("CIRCLE_PROJECT_USERNAME");

        public override string BuildFolder => Environment.GetEnvironmentVariable("CIRCLE_WORKING_DIRECTORY");

        public override string CommitHash => Environment.GetEnvironmentVariable("CIRCLE_SHA1");
    }
}