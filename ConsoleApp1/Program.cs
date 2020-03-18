using ServiceStack.Redis;
using System;

namespace redisClientJiFen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RedisClient redisClient = new RedisClient("localhost:6379");
            Console.WriteLine($"RedisClient连接成功");
            while (true)
            {
                byte[] bytes = redisClient.RPop("mq");
                if (bytes != null)
                {
                    string mr = System.Text.Encoding.UTF8.GetString(bytes);
                    Console.WriteLine($"订单 {mr} 处理积分完成");
                }
                else
                {
                    Console.WriteLine($"消息队列没有数据");
                }
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}
