using Akka.Actor;
using Akka.IO;
using AkkaBase;
using AkkaBase.Base;
using AkkaSys.MMS;
using System;

namespace AkkaSys.PLC
{
    public class PLCRcv : BaseServerActor
    {

        private readonly ISysAkkaManager _akkaManager;
        private readonly IActorRef _plcRcvEditActor;


        public PLCRcv(ISysAkkaManager akkaManager, AkkaSysIP akkaSysIp) : base(akkaSysIp)
        {
            _akkaManager = akkaManager;
            _plcRcvEditActor = akkaManager.GetActor(nameof(PLCRcvEdit));


        }

        protected override void TcpReceivedData(Tcp.Received msg)
        {
            Console.WriteLine(" [Info] Handle_Tcp_Received. message=" + msg.ToString());
            Console.WriteLine(" [Info] Count=" + msg.Data.Count.ToString());
            _plcRcvEditActor.Tell(msg.Data.ToArray());

        }
    }
}
