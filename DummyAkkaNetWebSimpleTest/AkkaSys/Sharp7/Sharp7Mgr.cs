using Akka.Actor;
using AkkaBase;
using AkkaBase.Base;
using AkkaSys.PLC;
using System;

namespace AkkaSys.Sharp7
{
    public class Sharp7Mgr : BaseActor
    {
        public Sharp7Mgr(ISysAkkaManager akkaManager)
        {
            akkaManager.CreateChildActor<Sharp7Service>(Context);
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            // Retries to 10 times within a minute
            // This is the number of times the child actor is allowed to restart within the time window specified. 
            // The negative value means no limit.
            return new OneForOneStrategy(
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromMinutes(1),
                localOnlyDecider: ex =>
                {
                    return ex switch
                    {
                        // Actor Life Operator
                        // the actor will resume as if nothing happened
                        ArgumentException ae => Directive.Resume,
                        //  it will restart the actor and move on. And for any other unknown exception, it will stop the actor.
                        NullReferenceException ne => Directive.Restart, // Restart New Instance
                        _ => Directive.Stop
                    };
                }
                );
        }
    }
}
