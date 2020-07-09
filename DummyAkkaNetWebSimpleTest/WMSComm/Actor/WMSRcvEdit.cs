using Akka.Actor;
using System;

namespace WMSComm
{
    /**
    * Author :ICSC 余士鵬
    * Desc : WMS Rcv Edit Actor(負責TCP接收資料解析)
    **/
    public class WMSRcvEdit : ReceiveActor
    {

        public WMSRcvEdit()
        {
            Receive<byte[]>(message => ProRcvTcpData(message));
        }

        private void ProRcvTcpData(byte[] rcvData)
        {
            Console.WriteLine(" [Info] Handle Tcp Received Edit. message=" + rcvData);
        }
    }
}
