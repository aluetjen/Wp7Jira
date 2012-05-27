using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Funq;

namespace Aluetjen.Infrastructure
{
    public class Bus : IBus
    {
        private readonly Container _container;
        private readonly Dictionary<Type, object> _handlerFactory;

        public Bus(Container container)
        {
            _container = container;
            _handlerFactory = new Dictionary<Type, object>();
        }

        public void RegisterHandler<T, TMessage>() where T : IHandleMessages<TMessage>
        {
            Func<T> handlerFactory = _container.LazyResolve<T>();

            object handlerAction;
            Action<TMessage> handlerLamda;
            if (!_handlerFactory.TryGetValue(typeof(TMessage), out handlerAction))
            {
                handlerLamda = m => InstanciateAndCall(m, handlerFactory);
            }
            else
            {
                handlerLamda = m =>
                                   {
                                       ((Action<TMessage>)handlerAction)(m);

                                       InstanciateAndCall(m, handlerFactory);
                                   };
            }

            _handlerFactory[typeof(TMessage)] = handlerLamda;
        }

        private static void InstanciateAndCall<T, TMessage>(TMessage m, Func<T> handlerFactory) where T : IHandleMessages<TMessage>
        {
            var handler = handlerFactory();

            if (typeof (IHandleUiMessage<TMessage>).IsAssignableFrom(typeof (T)))
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => handler.Handle(m));
            }
            else
            {
                handler.Handle(m);
            }
        }

        public void Publish<T>(T message) where T : Message
        {
            object handler;

            if (_handlerFactory.TryGetValue(typeof(T), out handler))
            {
                message.PublishedOn = DateTime.UtcNow;

                ThreadPool.QueueUserWorkItem(x => ((Action<T>)handler)(message));
            }
        }
    }
}