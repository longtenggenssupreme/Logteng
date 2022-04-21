using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttplistenerConsole
{
    class Program
    {
        static HttpListener httpListener;
        static readonly int port = 8088;
        static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("开始启动监听服务");
                httpListener = new HttpListener();
                httpListener.Prefixes.Add("http://+:" + port + "/");//+ 号 表示可以监听本机的所有ip
                httpListener.Start();
                Console.WriteLine("启动监听服务成功");
                while (true)
                {
                    IAsyncResult result = httpListener.BeginGetContext(MyAsyncCallback, httpListener);
                    result.AsyncWaitHandle.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"启动监听服务出错，错误信息：{e}");
                throw;
            }
        }
        static void MyAsyncCallback(IAsyncResult ar)
        {
            HttpListener http = (HttpListener)ar.AsyncState;
            HttpListenerContext context = http.EndGetContext(ar);
            MyProcessContext(context);
        }

        static void MyProcessContext(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            if (request.InputStream != null)
            {
                StreamReader reader = new StreamReader(request.InputStream, Encoding.UTF8);
                var resultstring = reader.ReadToEnd();
                //处理收到信息
                Console.WriteLine($"哈哈----收到前端信息：{resultstring}");
                //Console.WriteLine($"收到前端信息{request.FILES.get('image')}"); 
                ExcuteResult result = new ExcuteResult();
                //byte[] senddata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                byte[] senddata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(resultstring));
                //处理信息之后返回信息
                context.Response.AppendHeader("Access-Control-Allow-Origin", "*");//处理跨域Access-Control-Allow-Origin
                context.Response.OutputStream.Write(senddata, 0, senddata.Length);
                context.Response.OutputStream.Close();
            }
            #region WebSocket
            //if (request.IsWebSocketRequest)
            //{
            //    var xx = await context.AcceptWebSocketAsync("");
            //    var websocket = xx.WebSocket;
            //    ArraySegment<byte> receiveas = new ArraySegment<byte>();//接收数据的容器
            //    var ww = await websocket.ReceiveAsync(receiveas, CancellationToken.None);
            //    ArraySegment<byte> sendas = new ArraySegment<byte>();

            //    await websocket.SendAsync(sendas, WebSocketMessageType.Text, false, CancellationToken.None);
            //} 
            #endregion
        }


    }
    public class ExcuteResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public ExcuteResult()
        {
            Id = 1;
            Name = "1.jpg";
            Url = "https://fuss10.elemecdn.com/3/63/4e7f3a15429bfda99bce42a18cdd1jpeg.jpeg?imageMogr2/thumbnail/360x360/format/webp/quality/100";
        }
    }
}
