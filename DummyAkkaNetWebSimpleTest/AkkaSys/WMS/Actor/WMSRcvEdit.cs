using AkkaBase.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaSys.WMS
{
    /**
   * Author :ICSC 余士鵬
   * Desc : WMS Rcv Edit Actor(負責TCP接收資料解析)
   **/
    public class WMSRcvEdit : BaseActor
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
