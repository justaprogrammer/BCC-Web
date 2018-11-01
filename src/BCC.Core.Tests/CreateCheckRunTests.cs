using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCC.Core.Model.CheckRunSubmission;
using BCC.Core.Tests.Util;
using Bogus;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace BCC.Core.Tests
{
    public class CreateCheckRunTests
    {
        static CreateCheckRunTests()
        {
            Faker = new Faker();
            FakeAnnotation = new Faker<Annotation>()
                .CustomInstantiator(f =>
                {
                    var lineNumber = f.Random.Int(1);
                    return new Annotation(f.System.FileName(), lineNumber,
                        lineNumber, f.PickRandom<CheckWarningLevel>(), f.Lorem.Word());
                });
        }

        public static Faker<Annotation> FakeAnnotation { get; }
        public static Faker Faker { get; }

        private readonly ITestOutputHelper _testOutputHelper;

        public CreateCheckRunTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void EquatableWithNSubtitute()
        {
            var createCheckRun = new CreateCheckRun
            {
                Name = Faker.Lorem.Word(),
                Title = Faker.Lorem.Word(),
                StartedAt = Faker.Date.Past(2),
                CompletedAt = Faker.Date.Past(),
                Conclusion = Faker.Random.Enum<CheckConclusion>(),
                Summary = Faker.Lorem.Paragraph(),
                Annotations = FakeAnnotation.Generate(10).ToArray()
            };

            var copy = new CreateCheckRun()
            {
                Name = createCheckRun.Name,
                Title = createCheckRun.Title,
                StartedAt = createCheckRun.StartedAt,
                CompletedAt = createCheckRun.CompletedAt,
                Conclusion = createCheckRun.Conclusion,
                Summary = createCheckRun.Summary,
                Annotations = createCheckRun.Annotations.Select(annotation => annotation).ToArray(),
            };

            var someListener = Substitute.For<SomeInterface>();
            someListener.Blah(createCheckRun);

            someListener.Received(1).Blah(Arg.Is<CreateCheckRun>(run => run.Equals(copy)));
        }
    }

    public interface SomeInterface
    {
        void Blah(CreateCheckRun createCheckRun);
    }
}
