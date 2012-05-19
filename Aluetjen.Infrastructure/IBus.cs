using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts
{
    public interface IBus
    {
        void Publish<T>(T message) where T : Message;
        void RegisterHandler<T, TMessage>() where T : IHandleMessages<TMessage>;
    }
}