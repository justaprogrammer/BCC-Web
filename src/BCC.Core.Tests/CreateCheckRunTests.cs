using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BCC.Core.Model.CheckRunSubmission;
using Bogus;
using FluentAssertions;
using FluentAssertions.Common;
using Xunit;
using Xunit.Abstractions;

namespace BCC.Core.Tests
{
    public class CreateCheckRunTests
    {
        [SuppressMessage("ReSharper", "ArgumentsStyleOther")]
        [SuppressMessage("ReSharper", "ArgumentsStyleNamedExpression")]
        static CreateCheckRunTests()
        {
            Faker = new Faker();
            FakeAnnotation = new Faker<Annotation>()
                .CustomInstantiator(f =>
                {
                    var lineNumber = f.Random.Int(1);
                    return new Annotation(
                        filename: f.System.FileName(),
                        startLine: lineNumber,
                        endLine: lineNumber,
                        checkWarningLevel: f.PickRandom<CheckWarningLevel>(),
                        message: f.Lorem.Word())
                    {
                        Title = f.Random.Words(3)
                    };
                });

            FakeCheckRunImage = new Faker<CheckRunImage>()
                .CustomInstantiator(f => new CheckRunImage(alt:f.Random.Words(3), imageUrl:f.Internet.Url())
                {
                    Caption = f.Random.Words(3)
                });
        }


        public static Faker<Annotation> FakeAnnotation { get; }
        public static Faker<CheckRunImage> FakeCheckRunImage { get; set; }
        public static Faker Faker { get; }

        [Fact]
        public void EquatableTest()
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

            createCheckRun.Equals(copy).Should().BeTrue();
        }
    }
}
