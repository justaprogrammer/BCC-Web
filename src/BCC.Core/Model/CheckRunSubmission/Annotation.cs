namespace BCC.Core.Model.CheckRunSubmission
{
    public class Annotation
    {
        public string Filename { get; set;  }
        public CheckWarningLevel CheckWarningLevel { get; set; }
        public string Title { get; set; }
        public string RawDetails { get; set; }
        public string Message { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }

        public Annotation()
        {
        }

        public Annotation(string filename,
            int startLine, int endLine, CheckWarningLevel checkWarningLevel, string message)
        {
            Filename = filename;
            CheckWarningLevel = checkWarningLevel;
            Message = message;
            StartLine = startLine;
            EndLine = endLine;
        }
    }
}