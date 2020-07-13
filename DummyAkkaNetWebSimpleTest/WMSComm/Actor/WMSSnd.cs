﻿using AkkaBase;
using AkkaBase.Base;

namespace WMSComm.Actor
{
    /**
    * Author :ICSC 余士鵬
    * Desc : WMS Snd Actor (TCP連線發送)
    **/
    public class WMSSnd : BaseClientActor
    {
        public WMSSnd(AkkaSysIP akkaSysIp) : base(akkaSysIp)
        {
            
        }
    }
}
