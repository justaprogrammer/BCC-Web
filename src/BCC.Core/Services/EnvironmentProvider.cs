using System;

namespace BCC.Core.Services
{
    public class EnvironmentProvider : IEnvironmentProvider
    {
        public string GetEnvironmentVariable(string variable)
        {
            return Environment.GetEnvironmentVariable(variable);
        }
    }
}