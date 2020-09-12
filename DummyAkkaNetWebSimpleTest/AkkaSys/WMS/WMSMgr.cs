using Akka.Actor;
using AkkaBase;
using AkkaBase.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaSys.WMS
{
    public class WMSMgr : BaseActor
    {

        private readonly IActorRef _wmsRcv;
        private readonly IActorRef _wmsRcvEdit;
        private readonly IActorRef _wmsSnd;
        private readonly IActorRef _wmsSndEdit;

        public WMSMgr(ISysAkkaManager akkaManager)
        {
            //_wmsRcv = akkaManager.CreateChildActor<WMSRcv>(Context);
            //_wmsRcvEdit = akkaManager.CreateChildActor<WMSRcvEdit>(Context);
            //_wmsSnd = akkaManager.CreateChildActor<WMSSnd>(Context);
            //_wmsSndEdit = akkaManager.CreateChildActor<WMSSndEdit>(Context);
        }
        // 子Actor Expection Handle
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
