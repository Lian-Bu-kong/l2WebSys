using Akka.Actor;
using Akka.IO;
using AkkaBase;
using AkkaBase.Base;
using System;

namespace WMSComm
{
    /**
    * Author :ICSC 余士鵬
    * Desc : WMS Rcv Actor(負責TCP連線與接收資料)
    **/
    public class WMSRcv : BaseServerActor
    {

        ISysAkkaManager _akkaManager;
        IActorRef _wmsRcvEditActor;

        public IUntypedActorContext GetContext { get; } = Context;

        public WMSRcv(ISysAkkaManager akkaManager, AkkaSysIP akkaSysIp) : base(akkaSysIp)
        {         
            _akkaManager = akkaManager;
            _wmsRcvEditActor = akkaManager.GetActor(nameof(WMSRcvEdit));      
        }

        protected override void TcpReceivedData(Tcp.Received msg)
        {
            Console.WriteLine(" [Info] Handle_Tcp_Received. message=" + msg.ToString());
            Console.WriteLine(" [Info] Count=" + msg.Data.Count.ToString());
            _wmsRcvEditActor.Tell(msg.Data.ToArray());
        }


    }
}
