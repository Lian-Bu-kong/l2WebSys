using Akka.Actor;
using Akka.IO;
using AkkaBase;
using AkkaBase.Base;
using AkkaSys.MMS;
using System;

namespace AkkaSys.MMS
{
    public class MMSRcv : BaseServerActor
    {

        private readonly ISysAkkaManager _akkaManager;
        private readonly IActorRef _mmsRcvEditActor;


        public MMSRcv(ISysAkkaManager akkaManager, AkkaSysIP akkaSysIp) : base(akkaSysIp)
        {
            _akkaManager = akkaManager;
            _mmsRcvEditActor = akkaManager.GetActor(nameof(MMSRcvEdit));


        }

        protected override void TcpReceivedData(Tcp.Received msg)
        {
            Console.WriteLine(" [Info] Handle_Tcp_Received. message=" + msg.ToString());
            Console.WriteLine(" [Info] Count=" + msg.Data.Count.ToString());
            _mmsRcvEditActor.Tell(msg.Data.ToArray());

        }
    }
}
