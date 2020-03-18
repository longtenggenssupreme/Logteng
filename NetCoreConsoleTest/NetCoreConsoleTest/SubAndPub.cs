using StackExchange.Redis;
using System;
namespace NetCoreConsoleTest
{
    /// <summary>
    /// 订阅与发布
    /// </summary>
    public class SubAndPub
    {

        ConnectionMultiplexer multiplexer;

        public SubAndPub()
        {
            multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
        }

        /// <summary>
        /// 订阅
        /// </summary>
        public void Sub(Action<RedisChannel, RedisValue> handler)
        {
            ISubscriber subscriber = multiplexer.GetSubscriber();
            subscriber.Subscribe("pub_sub", handler);
            //subscriber.Subscribe("pub_sub", (channel, message) =>
            //{
            //    //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            //    Console.WriteLine($"66666666666666订单 {message} 666666666");
            //});
        }

        /// <summary>
        /// 发布
        /// </summary>
        public void Pub(RedisValue message)
        {
            ISubscriber subscriber = multiplexer.GetSubscriber();
            subscriber.Publish("pub_sub", message);
        }
    }
}
