namespace BCC.Core.Services
{
    /// <summary>
    /// Wrapper of Jenkins environment information
    /// https://wiki.jenkins.io/display/JENKINS/Building+a+software+project#Buildingasoftwareproject-belowJenkinsSetEnvironmentVariables
    /// </summary>
    public class JenkinsEnvironmentDetails : IEnvironmentDetails
    {
        private readonly IEnvironmentProvider _environmentProvider;

        public JenkinsEnvironmentDetails(IEnvironmentProvider environmentProvider)
        {
            _environmentProvider = environmentProvider;
        }
    }
}