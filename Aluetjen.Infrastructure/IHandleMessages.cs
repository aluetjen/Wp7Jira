namespace Aluetjen.Jira.Contexts
{
    public interface IHandleMessages<T>
    {
        void Handle(T message);
    }
}
