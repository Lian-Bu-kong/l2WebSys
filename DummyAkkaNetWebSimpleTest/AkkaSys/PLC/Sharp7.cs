using Akka.Actor;
using AkkaBase.Base;
using AkkaSys.Event;
using AkkaSys.Message;
using System;
using static AkkaSys.Message.PLCTrackMsg;

namespace AkkaSys.PLC
{
    public class Sharp7 : BaseActor
    {
        private ITrackingEventPusher _trackEventPush;

        // Demo用
        private int rcvCount;

        private bool SndSwitchOpen = false;

        private ICancelable _tmr7Read;

        public Sharp7(ITrackingEventPusher trackEventPush)
        {
            _trackEventPush = trackEventPush;


            Receive<Read7TimerMsg>(ReadPlcMemory);
            Receive<Read7MsgSwitch>(Read7SwitchMechinsm);
        }

        private void ReadPlcMemory(Read7TimerMsg msg)
        {
            // 判斷
            if (SndSwitchOpen)
                SndDataToWeb();
        }

        private void SndDataToWeb()
        {
         
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

        private void Read7SwitchMechinsm(Read7MsgSwitch msg)
        {
            if (msg.Open)
                StatrRead7Timer();
            else
                _tmr7Read?.Cancel();
        }

        private void StatrRead7Timer()
        {
            _tmr7Read?.Cancel();
            var interval = TimeSpan.FromSeconds(1);
            var initDelay = TimeSpan.FromSeconds(1);
            _tmr7Read = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(initDelay, interval, Self, new Read7TimerMsg(), Self);
        }
    }
}
