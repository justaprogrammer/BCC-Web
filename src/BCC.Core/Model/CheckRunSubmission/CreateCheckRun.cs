using System;

namespace BCC.Core.Model.CheckRunSubmission
{
    public class CreateCheckRun
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Text { get; set; }
        public CheckConclusion Conclusion { get; set; }
        public Annotation[] Annotations { get; set; }
        public CheckRunImage[] Images { get; set; }
        public DateTimeOffset StartedAt { get; set; }
        public DateTimeOffset CompletedAt { get; set; }

        public CreateCheckRun()
        {
        }

        public CreateCheckRun(string name, string title, string summary, CheckConclusion conclusion, DateTimeOffset startedAt, DateTimeOffset completedAt)
        {
            Name = name;
            Title = title;
            Summary = summary;
            Conclusion = conclusion;
            StartedAt = startedAt;
            CompletedAt = completedAt;
        }
    }
}