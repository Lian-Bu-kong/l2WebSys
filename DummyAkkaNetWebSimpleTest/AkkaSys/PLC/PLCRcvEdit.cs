using AkkaBase.Base;
using AkkaSys.Event;
using System;
using static AkkaSys.Message.PLCTrackMsg;

namespace AkkaSys.PLC
{
    public class PLCRcvEdit : BaseActor
    {

    
        private ITrackingEventPusher _trackEventPush;
        
        // Demo用
        private int rcvCount;

        public PLCRcvEdit(ITrackingEventPusher trackEventPush)
        {
            rcvCount = 0;

            _trackEventPush = trackEventPush;

            Receive<byte[]>(message => ProRcvTcpData(message));
        }

        private void ProRcvTcpData(byte[] rcvData)
        {
            Console.WriteLine(" [Info] Handle Tcp Received Edit. message=" + rcvData);


            var trackMap = new TrackMap();

            switch (rcvCount)
            {
                case 1:
                    trackMap.UncoilerSkid1 = "CE2000000100000";
                    break;
                case 0:
                    trackMap.UncoilerSkid2 = "CE2000000100000";
                    break;
                case 2:
                    trackMap.Uncoiler = "CE2000000100000";
                    break;
                case 3:
                    trackMap.Recoiler = "CE2000000100000";
                    break;
                case 5:
                    trackMap.RecoilerSkid1 = "CE2000000100000";
                    break;
                case 4:
                    trackMap.RecoilerSkid2 = "CE2000000100000";
                    break;
            }

            rcvCount++;

            if (rcvCount > 5)
                rcvCount = 0;
            
            _trackEventPush.UpdateTrackingMap(trackMap);
        }



        #region Old Code暫存

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
        #endregion
    }
}
