using AkkaBase.Base;
using AkkaSys.Event;
using System;
using static AkkaSys.Message.PLCTrackMsg;

namespace AkkaSys.PLC
{
    public class PLCRcvEdit : BaseActor
    {

    
        private ITrackingEventPusher _trackEventPush;
        public PLCRcvEdit(ITrackingEventPusher trackEventPush)
        {
         
            _trackEventPush = trackEventPush;

            Receive<byte[]>(message => ProRcvTcpData(message));
        }

        private void ProRcvTcpData(byte[] rcvData)
        {
            Console.WriteLine(" [Info] Handle Tcp Received Edit. message=" + rcvData);

            var trackMap = new TrackMap()
            {
                Uncoiler = "CE0010101010"
            };

            _trackEventPush.UpdateTrackingMap(trackMap);
        }

        // Old Code暫存

        ////private readonly ActorSelection _webClient;
        //private ITrackingEventPusher _trackEventPush;
        //public PLCRcvEdit(ITrackingEventPusher trackEventPush)
        //{
        //    //_webClient = Context.ActorSelection("akka.tcp://WebActorSystem@127.0.0.1:8200/user/WebComm");
        //    _trackEventPush = trackEventPush;

        //    Receive<byte[]>(message => ProRcvTcpData(message));
        //}

        //private void ProRcvTcpData(byte[] rcvData)
        //{
        //    Console.WriteLine(" [Info] Handle Tcp Received Edit. message=" + rcvData);

        //    //_webClient.Tell("PLC RcvEdit Get Message");
        //}

    }
}
