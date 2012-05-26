namespace Aluetjen.Infrastructure
{
    public interface IHandleMessages<T>
    {
        void Handle(T message);
    }
}
