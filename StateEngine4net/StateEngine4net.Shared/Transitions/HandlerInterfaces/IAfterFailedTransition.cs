using System.Threading.Tasks;
using StateEngine4net.Core.TransitionResults;

namespace StateEngine4net.Core.Transitions.HandlerInterfaces
{
    public interface IAfterFailedTransition<TEntity>
    {
        Task OnFailedTransition(TEntity statedEntity, TransitionFailed result);
    }
}