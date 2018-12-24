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
            var environmentServiceProvider = new EnvironmentServiceProvider(environmentProvider);
            Assert.Throws<EnvironmentServiceNotFoundException>(() => environmentServiceProvider.GetEnvironmentService());
        }

        [Fact]
        public void ShouldDetectAppVeyor()
        {
            var environmentProvider = Substitute.For<IEnvironmentProvider>();
            environmentProvider.GetEnvironmentVariable("APPVEYOR").Returns("True");

            var environmentServiceProvider = new EnvironmentServiceProvider(environmentProvider);
            var environmentService = environmentServiceProvider.GetEnvironmentService();
            environmentService.Should().BeOfType<AppVeyorEnvironmentService>();
        }

        [Fact]
        public void ShouldDetectTravis()
        {
            var environmentProvider = Substitute.For<IEnvironmentProvider>();
            environmentProvider.GetEnvironmentVariable("TRAVIS").Returns("True");

            var environmentServiceProvider = new EnvironmentServiceProvider(environmentProvider);
            var environmentService = environmentServiceProvider.GetEnvironmentService();
            environmentService.Should().BeOfType<TravisEnvironmentService>();
        }

        [Fact]
        public void ShouldDetectCircle()
        {
            var environmentProvider = Substitute.For<IEnvironmentProvider>();
            environmentProvider.GetEnvironmentVariable("CIRCLECI").Returns("True");

            var environmentServiceProvider = new EnvironmentServiceProvider(environmentProvider);
            var environmentService = environmentServiceProvider.GetEnvironmentService();
            environmentService.Should().BeOfType<CircleEnvironmentService>();
        }

        [Fact]
        public void ShouldDetectJenkins()
        {
            var environmentProvider = Substitute.For<IEnvironmentProvider>();
            environmentProvider.GetEnvironmentVariable("JENKINS_HOME").Returns(Faker.System.DirectoryPath());

            var environmentServiceProvider = new EnvironmentServiceProvider(environmentProvider);
            var environmentService = environmentServiceProvider.GetEnvironmentService();
            environmentService.Should().BeOfType<JenkinsEnvironmentService>();
        }
    }
}
