using System.Diagnostics.CodeAnalysis;

namespace BCC.Core.Model.CheckRunSubmission
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum CheckWarningLevel
    {
        Notice = 0,
        Warning = 1,
        Failure = 2,
    }
}