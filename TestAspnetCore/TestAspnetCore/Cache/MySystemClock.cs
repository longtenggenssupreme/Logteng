using Microsoft.Extensions.Internal;
using System;

namespace TestAspnetCore.Cache
{
    public class MySystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => DateTime.Now;
    }
}
