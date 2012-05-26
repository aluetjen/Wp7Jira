using System;

namespace Aluetjen.Infrastructure
{
    public abstract class Message
    {
        public DateTime PublishedOn { get; set; }
    }
}