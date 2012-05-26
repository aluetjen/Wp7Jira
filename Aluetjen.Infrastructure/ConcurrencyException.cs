using System;

namespace Aluetjen.Infrastructure
{
    public class ConcurrencyException : Exception
    {
        public object CurrentDocument { get; set; }
    }
}