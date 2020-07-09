﻿using Akka.Actor;
using System;

namespace MMSComm
{
    /**
    * Author :ICSC 余士鵬
    * Desc : MMS Rcv Edit Actor(負責TCP接收資料解析)
    **/
    public class MMSRcvEdit : ReceiveActor
    {

        public MMSRcvEdit()
        {
            Receive<byte[]>(message => ProRcvTcpData(message));
        }

        private void ProRcvTcpData(byte[] rcvData)
        {
            Console.WriteLine(" [Info] Handle Tcp Received Edit. message=" + rcvData);
        }
    }
}
