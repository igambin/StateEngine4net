using LightInject;
using StateEngine4net.Events;
using StateEngine4net.Events.Interfaces;

namespace StateEngine4net.Core.IoC
{
    public static class IServiceRegistryExtension
    {
        public static IServiceRegistry RegisterDomainEvent<TEvent>(this IServiceRegistry serviceRegistry)
            where TEvent : class, IStateEvent
        {
            if (serviceRegistry != null)
            {
                _ = serviceRegistry.Register<IStateEventBus<TEvent>, StateEventBus<TEvent>>(new PerContainerLifetime());
                serviceRegistry.Initialize(
                    reg => typeof(IStateEventListener<TEvent>).IsAssignableFrom(reg.ImplementingType),
                    (factory, instance) =>
                    {
                        factory
                            .GetInstance<IStateEventBus<TEvent>>()
                            .RegisterForEvent((IStateEventListener<TEvent>)instance);
                    });
            }
            return serviceRegistry;
        }
    }
}