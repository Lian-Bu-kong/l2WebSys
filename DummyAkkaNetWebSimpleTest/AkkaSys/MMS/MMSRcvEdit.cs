using AkkaBase.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaSys.MMS
{
    public class MMSRcvEdit : BaseActor
    {
        //private readonly ActorSelection _webClient;

        public MMSRcvEdit()
        {
            //_webClient = Context.ActorSelection("akka.tcp://WebActorSystem@127.0.0.1:8200/user/WebComm");

            Receive<byte[]>(message => ProRcvTcpData(message));
        }

        private void ProRcvTcpData(byte[] rcvData)
        {
            Console.WriteLine(" [Info] Handle Tcp Received Edit. message=" + rcvData);

            //_webClient.Tell("MMS RcvEdit Get Message");
        }

    }
}
