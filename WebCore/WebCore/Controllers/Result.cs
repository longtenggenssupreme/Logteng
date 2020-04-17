namespace WebCore.Controllers
{
    public class Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Result()
        {
            Id = 1;
            Name = "1.jpg";
            Url = "http://localhost:8081/";
        }
    }
}