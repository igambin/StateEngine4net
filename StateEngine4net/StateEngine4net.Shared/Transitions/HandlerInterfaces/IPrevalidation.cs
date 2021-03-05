using System.Threading.Tasks;
using StateEngine4net.Core.TransitionResults;
using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Core.Transitions.HandlerInterfaces
{
    public interface IPrevalidation<TEntity>
    {
        Task<ITransitionValidationResult> OnValidating(TEntity statedEntity);
    
        Task OnValidationFailed(TEntity statedEntity, TransitionValidationFailed result);
    }
}