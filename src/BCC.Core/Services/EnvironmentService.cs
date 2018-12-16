namespace BCC.Core.Services
{
    public class EnvironmentService: IEnvironmentService
    {
        private readonly IEnvironmentProvider _environmentProvider;

        public EnvironmentService(IEnvironmentProvider environmentProvider)
        {
            _environmentProvider = environmentProvider;
        }

        public IEnvironmentDetails GetEnvironmentDetails()
        {
            if (!string.IsNullOrWhiteSpace(_environmentProvider.GetEnvironmentVariable("APPVEYOR")))
            {
                return new AppVeyorEnvironmentDetails(_environmentProvider);
            }

            if (!string.IsNullOrWhiteSpace(_environmentProvider.GetEnvironmentVariable("TRAVIS")))
            {
                return new TravisEnvironmentDetails(_environmentProvider);
            }

            if (!string.IsNullOrWhiteSpace(_environmentProvider.GetEnvironmentVariable("CIRCLECI")))
            {
                return new CircleEnvironmentDetails(_environmentProvider);
            }

            if (!string.IsNullOrWhiteSpace(_environmentProvider.GetEnvironmentVariable("JENKINS_HOME")))
            {
                return new JenkinsEnvironmentDetails(_environmentProvider);
            }

            return null;
        }
    }
}
