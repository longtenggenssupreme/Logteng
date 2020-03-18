using StackExchange.Redis;
using System;
using System.Diagnostics;

namespace NetCoreConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("...........网购处理过程............");

            //string sn = $"WG{DateTime.Now.ToString("yyyMMddHHmmssfffffff")}";
            Console.WriteLine("...........处理订单............");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            #region 一般使用

            //Console.WriteLine("...........订单处理完成............");
            //Console.WriteLine("...........处理订单积分............");
            //Order order = new Order();
            //order.AddOrder(sn);

            //Console.WriteLine("...........处理订单发送短息............");
            //SMS sMS = new SMS();
            //sMS.AddSMS(sn); 
            #endregion

            #region pub sub 订阅发布
            string sn = null;
            SubAndPub subAndPub = new SubAndPub();
            for (int i = 0; i < 20; i++)
            {
                sn = $"WG{DateTime.Now.ToString("yyyMMddHHmmssfffffff")}";
                Console.WriteLine($"生成第 {i} 个订单，订单号：{sn}");
                subAndPub.Pub(sn);
               
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            #endregion

            #region 测试
            //ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
            ////multiplexer.GetSubscriber().Publish("pub_sub", "111111");
            //multiplexer.GetSubscriber().Subscribe("pub_sub", (channel, message) =>
            //{
            //    //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            //    Console.WriteLine($"66666666666666订单 {message} 666666666");
            //});

            ////multiplexer.GetSubscriber().Publish("pub_sub", "6666"); 
            #endregion        

            Console.WriteLine("...........网购完成............");
            stopwatch.Stop();
            Console.WriteLine($"...........网购完成，总共用时{stopwatch.ElapsedMilliseconds}............");
            Console.ReadKey();
        }


    }
}
