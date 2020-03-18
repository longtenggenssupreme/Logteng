using System;
using ServiceStack.Redis;

namespace RedisMessageQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("消息队列演示");
            RedisClient redisClient = new RedisClient("localhost:6379");
            string ss = "这是redisMQ";
            Console.WriteLine($"消息队列输入内容：{ss}");
            //生产者,LPush左端插入数据，RPush右端插入数据
            redisClient.LPush("mq", System.Text.Encoding.UTF8.GetBytes(ss));

            System.Diagnostics.Process.Start(@"F:\Person\linjie\Logteng\ConsoleApp1\bin\Debug\netcoreapp3.1\redisClientJiFen.exe");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            int i = 0;
            string sn = null;
            while (true)
            {
                sn = $"WG{DateTime.Now.ToString("yyyMMddHHmmssfffffff")}";
                Console.WriteLine($"生成第 {i++} 个订单，订单号：{sn}");
                Console.WriteLine("...........处理订单............");
                Console.WriteLine($"订单 {sn} 处理完成");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                redisClient.LPush("mq", System.Text.Encoding.UTF8.GetBytes(ss + i++));
            }

            //消费者，RPop右端取出数据，LPop左端取出数据
            //byte[] bytes1 = redisClient.RPop("mq");
            //string mr1 = System.Text.Encoding.UTF8.GetString(bytes1);
            //Console.WriteLine($"消息队列获取内容：{mr1}");


            //LPush  -----》RPop
            //RPush  -----》LPop
            //消费者 RPop LPop BRPop BLPop
            //推模型 BRPop BLPop，被动的去接受数据
            //拉模型 RPop LPop，  主动的去获收数据
            //总结，一对一通讯

            //byte[][] bytes = redisClient.BRPop("mq", 60);
            //byte[] bytes = redisClient.BRPopValue("mq", 60);
            //string mr = System.Text.Encoding.UTF8.GetString(bytes);
            //Console.WriteLine($"消息队列获取内容：{mr}");


            //while (true)
            //{
            //    byte[] bytes = redisClient.RPop("mq");
            //    if (bytes != null)
            //    {
            //        string mr = System.Text.Encoding.UTF8.GetString(bytes);
            //        Console.WriteLine($"{mr}");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"消息队列没有数据");
            //    }
            //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            //}
            Console.ReadKey();
        }
    }
}
