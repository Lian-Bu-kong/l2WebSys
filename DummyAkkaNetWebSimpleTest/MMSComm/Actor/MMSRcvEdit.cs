using Akka.Actor;
using System;

namespace MMSComm
{
    /**
    * Author :ICSC 余士鵬
    * Desc : MMS Rcv Edit Actor(負責TCP接收資料解析)
    **/
    public class MMSRcvEdit : ReceiveActor
    {
        private readonly ActorSelection _webClient;

        public MMSRcvEdit()
        {
            _webClient = Context.ActorSelection("akka.tcp://WebActorSystem@127.0.0.1:8200/user/WebComm");

            Receive<byte[]>(message => ProRcvTcpData(message));
        }

        private void ProRcvTcpData(byte[] rcvData)
        {
            Console.WriteLine(" [Info] Handle Tcp Received Edit. message=" + rcvData);

            _webClient.Tell("MMS RcvEdit Get Message");
        }
    }
}
