  
//  <autogenerated>
//
//      This file was generated by T4 code generator 'StateEngineTamplate.tt'
//      Any changes made to this file manually will be PERSISTED.
//      This file will NOT be OVERWRITTEN at the next generation.
//      Partial methods for new actions will have to be added manually.
//
//</autogenerated>

using System.CodeDom.Compiler;
using System.Threading.Tasks;
using StateEngine4net.Core.TransitionResults;
using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Tests.TestModel.TransitionHandlers
{
    /// <summary>
    ///
    /// 	This TransitionHandler provides the action that is to be executed
    /// 	for the state transition StartCleanup. This allows 
    ///     modifications and interactions in and of the stated object when 
    ///     its state is about to change.
    ///
    /// </summary>
    [GeneratedCode("StateEngineTemplate.tt", "1.0.0.0")]
    public class StatedTestObjectStartCleanupHandler : IStatedTestObjectStartCleanupHandler
//  optionally uncomment any of these to implement: 
//                                              ,IPrevalidation<StatedTestObject>
//                                              ,IBeforeTransition<StatedTestObject>
//                                              ,IAfterSuccessfulTransition<StatedTestObject> 
//                                              ,IAfterFailedTransition<StatedTestObject>
    {

        // NOTE: add specific dependencies here as private properties and 
        //       then use the following constructor in order to inject according
        //       interfaces in order to handle the specified transition
        //       e. g. repositories, logger, integrations, etc.
        public StatedTestObjectStartCleanupHandler(/* add dependencies here */)
        {
        }

        public async Task<ITransitionExecutionResult> ExecuteTransition(StatedTestObject statedEntity)        
        {
            await Task.Delay(0); // dummy to avoid await-warning CS1998

            if (false /* anything goes wrong */ )
            {
                var failedResult = new TransitionFailed
                {
                    MessageKey = "errorMessageKey"
                };
                failedResult.AddMessageArg("key1", "value1")
                            .AddMessageArg("key2", "value2");
                return failedResult;
            }
            return new TransitionSuccessful();
        }
    }
}

