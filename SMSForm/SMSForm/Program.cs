using NetCoreConsoleTest;
using System;

namespace SMSForm
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("短信");
            SubAndPub subAndPub = new SubAndPub();
            subAndPub.Sub((a, b) =>
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.WriteLine($"订单 {b} 发送短信完成");
            });
            Console.ReadKey();
        }
    }
}
