using System.Threading.Tasks;

namespace StateEngine4net.Core.Transitions.HandlerInterfaces
{
    public interface IBeforeTransition<TEntity>
    {
        Task OnPrepareTransition(TEntity statedEntity);
    }
}
