using System;
using ServiceStack.Redis;

namespace RedisMessageQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            RedisClient redisClient = new RedisClient("localhost:6379");
            string ss = "这是redisMQ";
            Console.WriteLine($"消息队列输入内容：{ss}");
            //生产者,LPush左端插入数据，RPush右端插入数据
            redisClient.LPush("mq", System.Text.Encoding.UTF8.GetBytes(ss));

            //消费者，RPop右端取出数据，LPop左端取出数据
            //byte[] bytes = redisClient.RPop("mq");
            //string mr = System.Text.Encoding.UTF8.GetString(bytes);
            //Console.WriteLine($"消息队列获取内容：{mr}");


            //LPush  -----》RPop
            //RPush  -----》LPop
            //消费者 RPop LPop BRPop BLPop
            //推模型 BRPop BLPop，被动的去接受数据
            //拉模型 RPop LPop，  主动的去获收数据
            //总结，一对一通讯

            //byte[][] bytes = redisClient.BRPop("mq", 60);
            byte[] bytes = redisClient.BRPopValue("mq", 60);
            string mr = System.Text.Encoding.UTF8.GetString(bytes);
            Console.WriteLine($"消息队列获取内容：{mr}");
            Console.ReadKey();
        }
    }
}
