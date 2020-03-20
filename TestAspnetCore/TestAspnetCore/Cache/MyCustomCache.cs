using Microsoft.Extensions.Caching.Memory;

namespace TestAspnetCore.Cache
{
    public class MyCustomCache 
    {
        public MemoryCache MemoryCache { get; set; }

        public MyCustomCache(IMemoryCache cache)
        {
            MemoryCache = new MemoryCache(new MemoryCacheOptions()
            {
                //单位可以自定，这个标记的目的就是说，最大的缓存有个限制
                SizeLimit = 100
            }); ;
        }
    }
}
