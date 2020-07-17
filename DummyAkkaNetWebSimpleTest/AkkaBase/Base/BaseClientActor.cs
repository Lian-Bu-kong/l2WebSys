using Akka.Actor;
using Akka.IO;
using System;

namespace AkkaBase.Base
{
    /**
    * Author : ICSC 丁豪霆, 
    *          Modify by ICSC 余士鵬
    * Desc : 基底Actor. Client連線使用
    **/
    public class BaseClientActor : BaseActor
    {
        public IActorRef SndActor;

        protected IActorRef _connection;

        private readonly AkkaSysIP _akkaSysIp;

        public BaseClientActor(AkkaSysIP akkaSysIp)
        {
            _akkaSysIp = akkaSysIp;            
            Connect();

            Receive<Tcp.Connected>(message => TCPConnected(message));
            Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
            Receive<Tcp.CommandFailed>(message => TcpCommandFailed(message));
            Receive<Tcp.Received>(message => TcpReceivedData(message));

        }


        protected virtual void TcpReceivedData(Tcp.Received msg)
        {
            Console.WriteLine(" [Info] Handle_Tcp_Received. message=" + msg.ToString());
            Console.WriteLine(" [Info] Count=" + msg.Data.Count.ToString());


        }
        protected virtual void TCPConnected(Tcp.Connected message)
        {
            Console.WriteLine(" [Info] Tcp.Connected. message=" + message.ToString());
            Console.WriteLine(" [Info] message.LocalAddress=" + message.LocalAddress.ToString());
            Console.WriteLine(" [Info] message.RemoteAddress=" + message.RemoteAddress.ToString());

            _connection = Sender;
            _connection.Tell(new Tcp.Register(Self));

            // Snd Test
            var byteStr = ByteString.FromString("Connected");
            var snd = Tcp.Write.Create(byteStr);
            _connection.Tell(snd);
        }   
        protected virtual void TcpConnectionClosed(Tcp.ConnectionClosed message)
        {
            Console.WriteLine(" [Alarm] Handle_Tcp_ConnectionClosed. message=" + message.ToString());
            Console.WriteLine(" [Alarm] message.cause=" + message.Cause);
            Console.WriteLine(" [Alarm] message.IsAborted=" + message.IsAborted.ToString());
            Console.WriteLine(" [Alarm] message.IsConfirmed=" + message.IsConfirmed.ToString());
            Console.WriteLine(" [Alarm] message.IsErrorClosed=" + message.IsErrorClosed.ToString());
            Console.WriteLine(" [Alarm] message.IsPeerClosed=" + message.IsPeerClosed.ToString());
            Connect();
        }
        protected virtual void TcpCommandFailed(Tcp.CommandFailed message)
        {
            Console.WriteLine(" [Alarm] Handle_Tcp_CommandFailed. message=" + message.ToString());
            Console.WriteLine(" [Alarm] message.Cmd=" + message.Cmd.ToString());
            Console.WriteLine(" [Alarm] message.Cmd.FailureMessage=" + message.Cmd.FailureMessage);
            Connect();
        }

        /// <summary>
        ///     連線
        /// </summary>
        protected void Connect()
        {
            Context.System.Tcp().Tell(new Tcp.Connect(_akkaSysIp.RemoteIpEndPoint));
        }
    }
}
