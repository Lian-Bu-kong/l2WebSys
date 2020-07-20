using Akka.Actor;
using AkkaBase;
using AkkaBase.Base;
using System;

namespace AkkaSys.MMS
{
    public class MMSMgr : BaseActor
    {
        private readonly IActorRef _mmsRcv;
        private readonly IActorRef _mmsRcvEdit;
        private readonly IActorRef _mmsSnd;
        private readonly IActorRef _mmsSndEdit;

        public MMSMgr(ISysAkkaManager akkaManager)
        {
            _mmsRcv = akkaManager.CreateChildActor<MMSRcv>(Context);
            _mmsRcvEdit = akkaManager.CreateChildActor<MMSRcvEdit>(Context);
            _mmsSnd = akkaManager.CreateChildActor<MMSSnd>(Context);
            _mmsSndEdit = akkaManager.CreateChildActor<MMSSndEdit>(Context);


            Receive<string>(message => ProStr(message));

            ReceiveAny(message => RcvAny(message));
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

        private void ProStr(string msg)
        {
            Console.WriteLine("[Info] MMSMgr Rcv Data " + msg);
        }

        private void RcvAny(object msg)
        {
            Console.WriteLine("[Error] MMSMgr Rcv Any " + msg);
        }
    }
}
