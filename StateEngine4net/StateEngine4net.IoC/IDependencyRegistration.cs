using LightInject;
using IServiceContainer = System.ComponentModel.Design.IServiceContainer;

namespace StateEngine4net.IoC
{
    public interface IDependencyRegistration : ICompositionRoot
    {
        void RegisterDependencies(IServiceContainer serviceContainer);

    }
}