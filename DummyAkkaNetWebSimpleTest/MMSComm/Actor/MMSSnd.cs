using AkkaBase;
using AkkaBase.Base;

namespace MMSComm.Actor
{
    /**
    * Author :ICSC 余士鵬
    * Desc : MMS Snd Actor (TCP連線發送)
    **/
    public class MMSSnd : BaseClientActor
    {
        public MMSSnd(AkkaSysIP akkaSysIp) : base(akkaSysIp)
        {
            
        }
    }
}
