using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpPost]
        public Result Post(ViewModel viewModel)
        {
            return new Result();
        }
        [HttpPut]
        public Result Put(ViewModel viewModel)
        {
            return new Result();
        }

        //[HttpPut]
        //public Result Put(ViewModel viewModel)
        //{
        //    return new Result();
        //}


        [HttpDelete("{id}")]
        public Result Delete(long id)
        {
            return new Result();
        }
        //[HttpGet]
        //public Result Get(long id)
        //{
        //    return new Result();
        //}

        [HttpGet("{id}")]
        public Result Get(long id)
        {
            return new Result();
        }

        [Route("getlist")]
        [HttpGet]
        public Result GetList()
        {
            return new Result();
        }        
    }
}
