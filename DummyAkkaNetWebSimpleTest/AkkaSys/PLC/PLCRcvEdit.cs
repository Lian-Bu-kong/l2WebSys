using AkkaBase.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaSys.PLC
{
    public class PLCRcvEdit : BaseActor
    {
        //private readonly ActorSelection _webClient;

        public PLCRcvEdit()
        {
            //_webClient = Context.ActorSelection("akka.tcp://WebActorSystem@127.0.0.1:8200/user/WebComm");

            Receive<byte[]>(message => ProRcvTcpData(message));
        }

        private void ProRcvTcpData(byte[] rcvData)
        {
            Console.WriteLine(" [Info] Handle Tcp Received Edit. message=" + rcvData);

            //_webClient.Tell("PLC RcvEdit Get Message");
        }

    }
}
