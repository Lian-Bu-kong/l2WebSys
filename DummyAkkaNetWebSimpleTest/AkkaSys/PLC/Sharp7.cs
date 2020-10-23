using Akka.Actor;
using AkkaBase.Base;
using AkkaSys.Event;
using AkkaSys.Message;
using DotNetty.Common.Internal;
using Sharp7;
using System;
using System.Text;
using static AkkaSys.Message.PLCTrackMsg;

namespace AkkaSys.PLC
{
    public class Sharp7Service : BaseActor
    {
        private ITrackingEventPusher _trackEventPush;

        // Demo用
        private int rcvCount;

        private string plcMemoryData = string.Empty;

        private bool SndSwitchOpen = true;

        private ICancelable _tmr7Read;
        private ICancelable _tmr7Conn;

        private readonly S7Client _s7Client;
        private readonly string _ip = "192.168.1.20";
        private readonly int _rack = 0;
        private readonly int _slot = 0;
        private int _result = -1;       //  連線狀態

        private int detectDisConnectCnt = 0;

        public Sharp7Service(ITrackingEventPusher trackEventPush, S7Client s7Client)
        {
            _trackEventPush = trackEventPush;
            _s7Client = s7Client;

           // StatrRead7Timer();

            Receive<Read7TimerMsg>(ReadPlcMemory);
            Receive<Read7ConnectMsg>(Connect);
            Receive<Read7MsgSwitch>(Read7SwitchMechinsm);

            Connect(new Read7ConnectMsg());
        }

        private void Connect(Read7ConnectMsg msg)
        {
            if (_result != 0)
            {
                _result = _s7Client.ConnectTo(_ip, _rack, _slot);
                StatrRead7Timer();
            }
   
        }

        private void ReadPlcMemory(Read7TimerMsg msg)
        {
            // 判斷
            if (SndSwitchOpen)
            {
                var readStr = ReadArea();

                if (!plcMemoryData.Equals(readStr) && !readStr.Equals(string.Empty))
                {
                    SndDataToWeb();
                    detectDisConnectCnt = 0;
                }
                    
                else
                {
                    detectDisConnectCnt++;
                    if (detectDisConnectCnt > 3)
                    {
                        _result = -1;
                    }
                        
                }
                    

                plcMemoryData = readStr;
            }
        }

        private void Read7SwitchMechinsm(Read7MsgSwitch msg)
        {
            if (msg.Open)
                StatrRead7Timer();
            else
                _tmr7Read?.Cancel();
        }

        /* Private method */

        /// <summary>
        ///     讀取 plc 資料
        /// </summary>
        private string ReadArea()
        {
            var buffer = new byte[65536];
            var sizeRead = 0;
            var dBNumber = 3;
            var amount = 10;
            var result = _s7Client.ReadArea(S7Consts.S7AreaDB, dBNumber, 0, amount, S7Consts.S7WLByte, buffer, ref sizeRead);

            if (result == 0)
               return HexDump(buffer, sizeRead);

            return string.Empty;
        }

        /// <summary>
        ///     Dump 出讀取的資料
        /// </summary>
        /// <param name="bytes"> 讀取的資料 </param>
        /// <param name="size"> 資料大小 </param>
        private string HexDump(byte[] bytes, int size)
        {
            if (bytes == null)
                return string.Empty;

            var bytesLength = size;
            var bytesPerLine = 16;

            var hexChars = "0123456789ABCDEF".ToCharArray();

            var firstHexColumn =
                  8     // 8 characters for the address
                + 3;    // 3 spaces

            var firstCharColumn = firstHexColumn
                + bytesPerLine * 3          // - 2 digit for the hexadecimal value and 1 space
                + (bytesPerLine - 1) / 8    // - 1 extra space every 8 characters from the 9th
                + 2;                        // 2 spaces 

            var lineLength = firstCharColumn
                + bytesPerLine                  // - characters to show the ascii value
                + Environment.NewLine.Length;   // Carriage return and line feed (should normally be 2)

            var line = (new string(' ', lineLength - 2) + Environment.NewLine).ToCharArray();
            var expectedLines = (bytesLength + bytesPerLine - 1) / bytesPerLine;
            var result = new StringBuilder(expectedLines * lineLength);

            for (int i = 0; i < bytesLength; i += bytesPerLine)
            {
                line[0] = hexChars[(i >> 28) & 0xF];
                line[1] = hexChars[(i >> 24) & 0xF];
                line[2] = hexChars[(i >> 20) & 0xF];
                line[3] = hexChars[(i >> 16) & 0xF];
                line[4] = hexChars[(i >> 12) & 0xF];
                line[5] = hexChars[(i >> 8) & 0xF];
                line[6] = hexChars[(i >> 4) & 0xF];
                line[7] = hexChars[(i >> 0) & 0xF];

                int hexColumn = firstHexColumn;
                int charColumn = firstCharColumn;

                for (int j = 0; j < bytesPerLine; j++)
                {
                    if (j > 0 && (j & 7) == 0) hexColumn++;
                    if (i + j >= bytesLength)
                    {
                        line[hexColumn] = ' ';
                        line[hexColumn + 1] = ' ';
                        line[charColumn] = ' ';
                    }
                    else
                    {
                        byte b = bytes[i + j];
                        line[hexColumn] = hexChars[(b >> 4) & 0xF];
                        line[hexColumn + 1] = hexChars[b & 0xF];
                        line[charColumn] = (b < 32 ? '·' : (char)b);
                    }
                    hexColumn += 3;
                    charColumn++;
                }
                result.Append(line);
            }
            //DumpBox.Text = result.ToString();
            return result.ToString();
        }

        private void SndDataToWeb()
        {
            Random random = new Random();
            

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

            trackMap.ActualRollForce = random.Next(200, 300).ToString();
            trackMap.ActualElongation = random.Next(200, 300).ToString();
            trackMap.SetupRollForce = random.Next(100, 200).ToString();
            trackMap.SetupElongation = random.Next(100, 200).ToString();


            rcvCount++;

            if (rcvCount > 5)
                rcvCount = 0;

            _trackEventPush.UpdateTrackingMap(trackMap);
        }

        private void StatrRead7Timer()
        {
            _tmr7Read?.Cancel();
            var interval = TimeSpan.FromSeconds(1);
            var initDelay = TimeSpan.FromSeconds(1);
            _tmr7Read = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(initDelay, interval, Self, new Read7TimerMsg(), Self);
        }

        protected override void PreStart()
        {
            base.PreStart();

            _tmr7Conn?.Cancel();
            var interval = TimeSpan.FromSeconds(1);
            var initDelay = TimeSpan.FromSeconds(1);
            _tmr7Conn = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(initDelay, interval, Self, new Read7ConnectMsg(), Self);
        }
    }
}
