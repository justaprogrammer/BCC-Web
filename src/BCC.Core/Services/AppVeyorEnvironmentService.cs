﻿using System;
using System.Linq;

namespace BCC.Core.Services
{
    /// <summary>
    /// Wrapper of AppVeyor environment information
    /// https://www.appveyor.com/docs/environment-variables/
    /// </summary>
    public class AppVeyorEnvironmentService : EnvironmentServiceBase
    {
        public AppVeyorEnvironmentService(IEnvironmentProvider environmentProvider) : base(environmentProvider)
        {
        }

        public override string GitHubRepo => Environment
            .GetEnvironmentVariable("APPVEYOR_REPO_NAME")
            .Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .First();

        public override string GitHubOwner => Environment
            .GetEnvironmentVariable("APPVEYOR_REPO_NAME")
            .Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)
            .First();

        public override string CloneRoot => Environment
            .GetEnvironmentVariable("APPVEYOR_BUILD_FOLDER");

        public override string CommitHash => Environment
            .GetEnvironmentVariable("APPVEYOR_REPO_COMMIT");
    }
}