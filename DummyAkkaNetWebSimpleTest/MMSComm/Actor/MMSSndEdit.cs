using AkkaBase.Base;
using System;

namespace MMSComm.Actor
{
    /**
    * Author :ICSC 余士鵬
    * Desc : MMS Snd Edit Actor (解析資料)
    **/
    public class MMSSndEdit : BaseActor
    {
        public MMSSndEdit()
        {
            Receive<String>(message => ProStr(message));
        }

        private void ProStr(String msg)
        {
            Console.WriteLine("[Info] MMSSndEdit Rcv Data " + msg);
        }
    }

  
}
