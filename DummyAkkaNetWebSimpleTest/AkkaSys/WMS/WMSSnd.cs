using AkkaBase;
using AkkaBase.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaSys.WMS
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
