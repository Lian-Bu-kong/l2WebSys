using Akka.Actor;
using Akka.IO;
using System;
using System.Net;

namespace MMSComm
{
    public class MMSRcv : ReceiveActor
    {    
        private IPEndPoint _ipEndPoint;
        private IActorRef _rcvConnActor;

        public MMSRcv()
        {
          
            // Call Tcp manager create tcp listenr (LocaIP and LocalPort)        
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(Configure.AkaSysPort), Configure.OutSysPort);
            Context.System.Tcp().Tell(new Tcp.Bind(Self, _ipEndPoint));


            Receive<Tcp.Connected>(message => TCPConnected(message));
            Receive<Tcp.CommandFailed>(msg => TCPCommandFail(msg));


        }


        private void TCPConnected(Tcp.Connected message)
        {
            Console.WriteLine(" [Info] Tcp.Connected. message=" + message.ToString());
            Console.WriteLine(" [Info] message.LocalAddress=" + message.LocalAddress.ToString());
            Console.WriteLine(" [Info] message.RemoteAddress=" + message.RemoteAddress.ToString());

            //斷開重連處理
            if (_rcvConnActor != null)
                Context.Stop(_rcvConnActor);

            _rcvConnActor = Context.ActorOf(Props.Create(() => new MMSRcvConn(Sender)));

        }
        private void TCPCommandFail(Tcp.CommandFailed msg)
        {
            Console.WriteLine($"[Error] Tcp Command Failed. {msg.Cmd}");
        }


        public class MMSRcvConn : ReceiveActor
        {
            // TODO Ack
            //private IActorRef _tcpWorker; 
            private IActorRef _decodeActor;

            public MMSRcvConn(IActorRef conn)
            {
                // TODO Ack
                //if (conn != null)
                //    _tcpWorker = conn;


                Receive<Tcp.Received>(message => TcpReceivedData(message));
                Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
                Receive<Tcp.CommandFailed>(message => TcpCommandFailed(message));
            }

            private void TcpReceivedData(Tcp.Received msg)
            {
                Console.WriteLine(" [Info] Handle_Tcp_Received. message=" + msg.ToString());
                Console.WriteLine(" [Info] ByteString=" + msg.Data.ToString());
                Console.WriteLine(" [Info] Count=" + msg.Data.Count.ToString());

            }

            private void TcpConnectionClosed(Tcp.ConnectionClosed message)
            {
                Console.WriteLine(" [Alarm] Handle_Tcp_ConnectionClosed. message=" + message.ToString());
                Console.WriteLine(" [Alarm] message.cause=" + message.Cause);
                Console.WriteLine(" [Alarm] message.IsAborted=" + message.IsAborted.ToString());
                Console.WriteLine(" [Alarm] message.IsConfirmed=" + message.IsConfirmed.ToString());
                Console.WriteLine(" [Alarm] message.IsErrorClosed=" + message.IsErrorClosed.ToString());
                Console.WriteLine(" [Alarm] message.IsPeerClosed=" + message.IsPeerClosed.ToString());           
            }

            private void TcpCommandFailed(Tcp.CommandFailed message)
            {
                Console.WriteLine(" [Alarm] Handle_Tcp_CommandFailed. message=" + message.ToString());
                Console.WriteLine(" [Alarm] message.Cmd=" + message.Cmd.ToString());
                Console.WriteLine(" [Alarm] message.Cmd.FailureMessage=" + message.Cmd.FailureMessage);
            }
        }
    }
}
