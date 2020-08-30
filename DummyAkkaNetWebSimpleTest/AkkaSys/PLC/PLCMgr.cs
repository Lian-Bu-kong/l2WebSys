using Akka.Actor;
using AkkaBase;
using AkkaBase.Base;
using System;

namespace AkkaSys.PLC
{
    public class PLCMgr : BaseActor
    {
        private readonly IActorRef _plcRcv;
        private readonly IActorRef _plcRcvEdit;
        private readonly IActorRef _plcSnd;
        private readonly IActorRef _plcSndEdit;

        public PLCMgr(ISysAkkaManager akkaManager)
        {
            _plcRcv = akkaManager.CreateChildActor<PLCRcv>(Context);
            _plcRcvEdit = akkaManager.CreateChildActor<PLCRcvEdit>(Context);
            _plcSnd = akkaManager.CreateChildActor<PLCSnd>(Context);
            _plcSndEdit = akkaManager.CreateChildActor<PLCSndEdit>(Context);


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
            Console.WriteLine("[Info] PLCMgr Rcv Data " + msg);
        }

        private void RcvAny(object msg)
        {
            Console.WriteLine("[Error] PLCMgr Rcv Any " + msg);
        }
    }
}
