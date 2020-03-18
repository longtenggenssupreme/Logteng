using System;

namespace NetCoreConsoleTest
{
    /// <summary>
    /// 短信处理
    /// </summary>
    public class SMS
    {
        /// <summary>
        /// 短信订单
        /// </summary>
        public void AddSMS(string sn)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.WriteLine($"订单 {sn} 处理发送短信完成");
        }
    }
}
