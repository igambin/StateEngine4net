using System.Threading.Tasks;
using StateEngine4net.Core.TransitionResults;

namespace StateEngine4net.Core.Transitions.HandlerInterfaces
{
    public interface IAfterSuccessfulTransition<TEntity>  
    {
        Task OnSuccessfulTransition(TEntity statedEntity, TransitionSuccessful result);
    }
}