using BCC.Core.Services;
using Bogus;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BCC.Core.Tests.Services
{
    public class EnvironmentServiceTests
    {
        static EnvironmentServiceTests()
        {
            Faker = new Faker();
        }

        public static Faker Faker { get; }

        [Fact]
        public void ShouldReturnNull()
        {
            var environmentProvider = Substitute.For<IEnvironmentProvider>();
            var environmentService = new EnvironmentService(environmentProvider);
            var environmentDetails = environmentService.GetEnvironmentDetails();
            environmentDetails.Should().BeNull();
        }

        [Fact]
        public void ShouldDetectAppVeyor()
        {
            var environmentProvider = Substitute.For<IEnvironmentProvider>();
            environmentProvider.GetEnvironmentVariable("APPVEYOR").Returns("True");

            var environmentService = new EnvironmentService(environmentProvider);
            var environmentDetails = environmentService.GetEnvironmentDetails();
            environmentDetails.Should().BeOfType<AppVeyorEnvironmentDetails>();
        }

        [Fact]
        public void ShouldDetectTravis()
        {
            var environmentProvider = Substitute.For<IEnvironmentProvider>();
            environmentProvider.GetEnvironmentVariable("TRAVIS").Returns("True");

            var environmentService = new EnvironmentService(environmentProvider);
            var environmentDetails = environmentService.GetEnvironmentDetails();
            environmentDetails.Should().BeOfType<TravisEnvironmentDetails>();
        }

        [Fact]
        public void ShouldDetectCircle()
        {
            var environmentProvider = Substitute.For<IEnvironmentProvider>();
            environmentProvider.GetEnvironmentVariable("CIRCLECI").Returns("True");

            var environmentService = new EnvironmentService(environmentProvider);
            var environmentDetails = environmentService.GetEnvironmentDetails();
            environmentDetails.Should().BeOfType<CircleEnvironmentDetails>();
        }

        [Fact]
        public void ShouldDetectJenkins()
        {
            var environmentProvider = Substitute.For<IEnvironmentProvider>();
            environmentProvider.GetEnvironmentVariable("JENKINS_HOME").Returns(Faker.System.DirectoryPath());

            var environmentService = new EnvironmentService(environmentProvider);
            var environmentDetails = environmentService.GetEnvironmentDetails();
            environmentDetails.Should().BeOfType<JenkinsEnvironmentDetails>();
        }
    }
}
