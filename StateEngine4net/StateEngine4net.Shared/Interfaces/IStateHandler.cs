using System.Threading.Tasks;
using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Core.Interfaces
{
    public interface IStateHandler<in TEntity>
    {
        Task<ITransitionExecutionResult> ExecuteTransition(TEntity statedEntity);
    }
}