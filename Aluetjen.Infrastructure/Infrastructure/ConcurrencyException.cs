using System;

namespace Aluetjen.Jira.Infrastructure
{
    public class ConcurrencyException : Exception
    {
        public object CurrentDocument { get; set; }
    }
}