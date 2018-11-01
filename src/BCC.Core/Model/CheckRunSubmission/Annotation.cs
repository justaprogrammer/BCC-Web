using System;
using System.Diagnostics.CodeAnalysis;

namespace BCC.Core.Model.CheckRunSubmission
{
    public class Annotation : IEquatable<Annotation>
    {
        public string Filename { get; set;  }
        public CheckWarningLevel CheckWarningLevel { get; set; }
        public string Title { get; set; }
        public string RawDetails { get; set; }
        public string Message { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
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

        public bool Equals(Annotation other)
        {
            if (other == null)
                return false;

            return Filename == other.Filename &&
                   CheckWarningLevel == other.CheckWarningLevel &&
                   Title == other.Title &&
                   RawDetails == other.RawDetails &&
                   Message == other.Message &&
                   StartLine == other.StartLine &&
                   EndLine == other.EndLine;
        }
    }
}