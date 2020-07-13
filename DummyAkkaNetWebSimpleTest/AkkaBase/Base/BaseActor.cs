using Akka.Actor;
using System;

namespace AkkaBase.Base
{
    /**
     * Author : ICSC 丁豪霆, 
     *          Modify by ICSC 余士鵬
     * Desc : Actor Base. 底層Acotr生命週期,Log處理(待整理)
     **/
    public class BaseActor : ReceiveActor
    {
        /// <summary>
        ///     嘗試執行函式使用
        /// </summary>
        /// <param name="action"> 執行函式 </param>
        protected void TryFlow(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ex.Message={ex.Message}");
                Console.WriteLine($"ex.StackTrace={ex.StackTrace}");
            }
        }

        protected override void PreStart()
        {
            base.PreStart();
            Console.WriteLine($"PreStart. start");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
            Console.WriteLine($"PostRestart. start");
            Console.WriteLine($"reason.Message={reason.Message}");
            Console.WriteLine($"message.GetType={message.GetType().Name}");
        }

        protected override void PostStop()
        {
            base.PostStop();
            Console.WriteLine($"PostStop. start");
        }

        protected override void PostRestart(Exception reason)
        {
            base.PostRestart(reason);
            Console.WriteLine($"PostRestart. start");
            Console.WriteLine($"reason.Message={reason.Message}");
        }
    }
}
