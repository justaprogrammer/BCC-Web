namespace BCC.Core.Services
{
    /// <summary>
    /// Wrapper of Jenkins environment information
    /// https://wiki.jenkins.io/display/JENKINS/Building+a+software+project#Buildingasoftwareproject-belowJenkinsSetEnvironmentVariables
    /// </summary>
    public class JenkinsEnvironmentDetails : EnvironmentDetailsBase
    {
        public JenkinsEnvironmentDetails(IEnvironmentProvider environmentProvider) : base(environmentProvider)
        {
        }

        public override string GitHubRepo => null;

        public override string GitHubOwner => null;

        public override string BuildFolder => Environment.GetEnvironmentVariable("WORKSPACE");

        public override string CommitHash => Environment.GetEnvironmentVariable("GIT_COMMIT");
    }
}