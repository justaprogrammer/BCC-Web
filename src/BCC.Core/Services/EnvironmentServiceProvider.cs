using System;

namespace BCC.Core.Services
{
    public class EnvironmentServiceProvider: IEnvironmentServiceProvider
    {
        private readonly IEnvironmentProvider _environmentProvider;

        public EnvironmentServiceProvider(IEnvironmentProvider environmentProvider)
        {
            _environmentProvider = environmentProvider;
        }

        public IEnvironmentService GetEnvironmentService()
        {
            if (!string.IsNullOrWhiteSpace(_environmentProvider.GetEnvironmentVariable("APPVEYOR")))
            {
                return new AppVeyorEnvironmentService(_environmentProvider);
            }

            if (!string.IsNullOrWhiteSpace(_environmentProvider.GetEnvironmentVariable("TRAVIS")))
            {
                return new TravisEnvironmentService(_environmentProvider);
            }

            if (!string.IsNullOrWhiteSpace(_environmentProvider.GetEnvironmentVariable("CIRCLECI")))
            {
                return new CircleEnvironmentService(_environmentProvider);
            }

            if (!string.IsNullOrWhiteSpace(_environmentProvider.GetEnvironmentVariable("JENKINS_HOME")))
            {
                return new JenkinsEnvironmentService(_environmentProvider);
            }

            throw new EnvironmentServiceNotFoundException();
        }
    }

    public class EnvironmentServiceNotFoundException : Exception
    {
    }
}
