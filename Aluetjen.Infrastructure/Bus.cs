using System;
using System.Collections.Generic;
using System.Threading;
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
                handlerLamda = m =>
                                   {
                                       var handler = handlerFactory();
                                       handler.Handle(m);
                                   };
            }
            else
            {
                handlerLamda = m =>
                                   {
                                       ((Action<TMessage>)handlerAction)(m);

                                       var handler = handlerFactory();
                                       handler.Handle(m);
                                   };
            }

            _handlerFactory[typeof(TMessage)] = handlerLamda;
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