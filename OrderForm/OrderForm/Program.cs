using NetCoreConsoleTest;
using System;

namespace OrderForm
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("积分");
            SubAndPub subAndPub = new SubAndPub();
            subAndPub.Sub((a, b) =>
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.WriteLine($"订单 {b}, 处理积分完成");
            });
            Console.ReadKey();
        }
    }
}
