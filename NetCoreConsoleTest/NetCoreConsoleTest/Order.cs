using System;

namespace NetCoreConsoleTest
{
    /// <summary>
    /// 积分处理
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 积分订单
        /// </summary>
        public void AddOrder(string sn) {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.WriteLine($"订单 {sn} 处理积分完成");
        }
    }
}
