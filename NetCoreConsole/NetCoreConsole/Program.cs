using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using NLog.Config;

//Microsoft.EntityFrameworkCore //EFCore 
//Microsoft.Extensions.Logging  // 日志
//Microsoft.Extensions.Logging.Console // 日志输出到Console控制台
//Microsoft.Extensions.Logging.Debug  // 日志输出到调试Debug
//Microsoft.EntityFrameworkCore.Proxies// 懒加载，延迟加载，EFCore没有懒加载，使用该库，可以实现懒加载
//Microsoft.EntityFrameworkCore.SqlServer//EFCore使用的SqlServer数据库
//Pomelo.EntityFrameworkCore.MySql//EFCore使用的MySql数据库


namespace NetCoreConsole
{
    class Program
    {
        //public static Logger logger = LogManager.GetLogger("Program");

        static void Main(string[] args)
        {
            #region NLog
            ////LogManager.Configuration = new XmlLoggingConfiguration(nlogPath);
            ////NLog.Config.XmlLoggingConfiguration.SetCandidateConfigFilePaths(@"F:\Person\linjie\Logteng\NetCoreConsole\NetCoreConsole\NLog.config");
            ////NLog.Web.NLogBuilder.ConfigureNLog(nlogPath);//nlog加载配置文件
            ////用于查看彩色控制台样式，以及日志等级过滤
            ////1、默认加载NLog.config配置文件，不过这个文件要设置，复制到输出目录：始终复制生成操作：内容
            ////2、也可以加载指定目录的配置文件
            //LogManager.Configuration = new XmlLoggingConfiguration(@"F:\Person\linjie\Logteng\NetCoreConsole\NetCoreConsole\NLog.config");
            //var logger = LogManager.GetCurrentClassLogger();
            //logger.Trace("Test For Trace");
            //logger.Debug("Test For Debug");
            //logger.Info("Test For Info");
            //logger.Warn("Test For Warn");
            //logger.Error("Test For Error", new Exception("sss"));
            //logger.Fatal("Test For Fatal", new Exception("sss")); 
            #endregion

            Console.WriteLine("测试  EF core ");
            BlogContext blogContext = new BlogContext();

            #region ef 数据库的创建和删除
            //Console.WriteLine("删除数据库");
            //blogContext.Database.EnsureDeleted();
            //Console.WriteLine("添加数据库");            
            blogContext.Database.EnsureCreated();
            //Console.WriteLine("添加数据库成功"); 
            #endregion

            #region 数据迁移相关
            //IEnumerable<string> vs = blogContext.Database.GetMigrations();
            //blogContext.Database.GetPendingMigrations();
            //blogContext.Database.GetAppliedMigrations();
            //blogContext.Database.Migrate(); 
            //Console.WriteLine("添加数据库成功");
            #endregion

            #region 预先加载，懒加载

            //预先加载(贪婪加载，饥饿加载，一次行加载所有)
            //var list = blogContext.Blog.Include(op => op.Posts).ThenInclude(op => op.Author).ToList();
            //var list = blogContext.Blog.Include(op => op.Posts).ThenInclude(op => op.Author).ToList();
            //var list = blogContext.Blog.Include(op => op.Posts).ThenInclude(op => op.Author).ToList().AsQueryable();
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"blog:{item}");
            //    foreach (var it in item.Posts)
            //    {
            //        Console.WriteLine($"Posts:{it.Title}");
            //        Console.WriteLine($"person:{it.Author.Name}");
            //    }
            //}
            //懒加载(延迟加载)，又称按需加载，net ef core 默认时候没有懒加载的，
            //ef是有的，ef core 要使用懒加载，需要使用proxy代理类UseLazyLoadingProxies，并且导航属性要添加Virtual标记
            //optionsBuilder.UseLazyLoadingProxies() 
            #endregion

            #region IQueryable和IEnumerable不同
            //IQueryable和IEnumerable不同
            //IQueryable和IEnumerable都是linq操作
            //IQueryable和IEnumerable都是延迟执行，tolist的时候在执行
            //只不过操作的位置不同，IQueryable是在数据库一侧操作，而IEnumerable实在应用程序的内存一侧操作，
            //IQueryable继承IEnumerable，IQueryable与IEnumerable 可以相互转换，IQueryable.AsIEnumerable,IEnumerable.AsIQueryable
            //var item = blogContext.Post.OrderBy(op => EF.Property<object>(op, "Title")).AsQueryable().Skip(1).Take(2);
            //IQueryable 在数据库中的 执行脚本，IQueryable和IEnumerable都是延迟执行，tolist的时候在执行
            //exec sp_executesql N'SELECT [p].[PostId], [p].[BlogId], [p].[Content], [p].[PersonId], [p].[Title]
            //FROM[Post] AS[p]
            //ORDER BY[p].[Title]
            //OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY',N'@__p_0 int,@__p_1 int',@__p_0=1,@__p_1=2

            //foreach (var it in item)
            //{
            //    Console.WriteLine($"it:{it.Title}");
            //}

            //var item = blogContext.Post.OrderBy(op => op.Title).AsEnumerable().Skip(1).Take(2);
            //IEnumerable在内存中的 执行脚本，IQueryable和IEnumerable都是延迟执行，tolist的时候在执行
            //SELECT[p].[PostId], [p].[BlogId], [p].[Content], [p].[PersonId], [p].[Title]
            //FROM[Post] AS[p]
            //ORDER BY[p].[Title]

            //foreach (var it in item)
            //{
            //    Console.WriteLine($"it:{it.Title}");
            //} 
            #endregion

            #region 实体跟踪Tracking
            //一种是全局修改之后在修改回来
            //另外一种是，是使用AsNoTracking()直接修改
            //blogContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ////using (IDbContextTransaction transaction = blogContext.Database.BeginTransaction())
            ////{
            //var item = blogContext.Post.Where(op => op.Title.Contains("10"));
            ////var item = blogContext.Post.AsNoTracking().Where(op => op.Title.Contains("10"));
            //foreach (var it in item)
            //    {
            //        Console.WriteLine($"it:{it.Title}");
            //        it.Title = "111";
            //    }
            //    blogContext.SaveChanges();
            ////    transaction.Commit();
            ////}
            //blogContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            #endregion

            #region 模糊查询StartsWith,,EndsWith,Contains,其中Contains和 EF.Functions.Like功能一样，但是sql语句翻译的确实不一样的
            ////但凡使用的计算函数，索引是一定不会被用上的
            //var item = blogContext.Post.Where(op => op.Title.Contains("10")).ToList();
            ////            SELECT[p].[PostId], [p].[BlogId], [p].[Content], [p].[PersonId], [p].[Title]
            ////            FROM[Post] AS[p]
            ////            WHERE CHARINDEX(N'10', [p].[Title]) > 0
            //foreach (var it in item)
            //{
            //    Console.WriteLine($"it:{it.Title}");
            //}
            //item = blogContext.Post.Where(op => EF.Functions.Like(op.Title, "%10%")).ToList();
            ////            SELECT[p].[PostId], [p].[BlogId], [p].[Content], [p].[PersonId], [p].[Title]
            ////            FROM[Post] AS[p]
            ////            WHERE[p].[Title]
            ////            LIKE N'%10%'
            //foreach (var it in item)
            //{
            //    Console.WriteLine($"it:{it.Title}");
            //}
            #endregion

            #region 自定义标量函数，linq一样可以翻译成对应的sql脚本
            ////            SELECT[p].[PostId] AS[id], [dbo].[MyFunction] ([p].[PostId]) AS[name]
            ////FROM[Post] AS[p]
            //var item = blogContext.Post.Select(op => new
            //{
            //    id = op.PostId,
            //    name = BlogContext.MyFunction(op.PostId)//使用标量函数
            //}).ToList();
            //foreach (var it in item)
            //{
            //    Console.WriteLine($"it:{it.name}");
            //}
            #endregion

            #region 编译查询

            #region 普通查询
            //int id = 1;
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //var item = blogContext.Post.Where(op => op.PostId == id).FirstOrDefault();
            //stopwatch.Stop();
            //Console.WriteLine($"普通查询：item:{item.Title},用时：{stopwatch.ElapsedMilliseconds}");
            //            exec sp_executesql N'SELECT TOP(1) [p].[PostId], [p].[BlogId], [p].[Content], [p].[PersonId], [p].[Title]
            //FROM[Post] AS[p]
            //WHERE[p].[PostId] = @__id_0',N'@__id_0 int',@__id_0=1
            #endregion

            #region 编译查询
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            ////var compileQuery = EF.CompileQuery((BlogContext context, int id) => context.Post.Where(c => c.PostId == id).FirstOrDefault());
            ////var item = compileQuery(blogContext, 1);
            //stopwatch.Stop();
            //Console.WriteLine($"普通查询：item:{item.Title},用时：{stopwatch.ElapsedMilliseconds}");
            #endregion

            //普通查询

            Func<BlogContext, Blog> unCompileQuery = context => context.Blog.Include(c => c.Posts).Where(a => a.BlogId == 1).FirstOrDefault();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("普通查询开始");
            for (int i = 0; i < 100000; i++)
            {
                var a = unCompileQuery(blogContext);
            }
            stopwatch.Stop();
            Console.WriteLine($"编译查询 用时：{stopwatch.ElapsedMilliseconds} 毫秒");

            //编译查询 一般是大量数据查询，并且参数不变的情况下比较好，但是不能返回集合
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //Console.WriteLine("编译查询开始");
            //var compileQuery = EF.CompileQuery((BlogContext context, int id) => context.Blog.Include(c => c.Posts).Where(a => a.BlogId == 1).FirstOrDefault());
            //for (int i = 0; i < 100000; i++)
            //{
            //    var item = compileQuery(blogContext, 1);
            //}
            //stopwatch.Stop();
            //Console.WriteLine($"编译查询 用时：{stopwatch.ElapsedMilliseconds} 毫秒");
            #endregion

            //var item = blogContext.Post.Where(op => op.Title.Contains("10"));
            //foreach (var it in item)
            //{
            //    Console.WriteLine($"it:{it.Title}");
            //}
            //var item = blogContext.Post.Where(op => EF.Functions.Like(op.Title,"10"));
            //foreach (var it in item)
            //{
            //    Console.WriteLine($"it:{it.Title}");
            //}

            Console.Read();

        }
    }

    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug());
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(loggerFactory);//添加把数据库操作的信息输出到指定的控制台
            //optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"server=127.0.0.1;database=mynetcore;user=sa;password=sa123;");
            //Microsoft.EntityFrameworkCore.SqlServer
            optionsBuilder.UseSqlServer(@"server=127.0.0.1;database=mynetcore;user=sa;password=sa123;");
            //Pomelo.EntityFrameworkCore.MySql
            //optionsBuilder.UseMySql(@"server=127.0.0.1;database=mynetcore;user=root;password=123456;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Post> posts = new List<Post>();
            for (int i = 0; i < 20; i++)
            {
                posts.Add(new Post { PostId = i + 1, PersonId = i >= 10 ? 2 : 1, Title = $"文章标题:100{i}", Content = $"文章内容:{i}", BlogId = i >= 10 ? 2 : 1 });
            }
            Blog blogs = new Blog { BlogId = 1, Url = "http://www.baidu.com" };
            Blog blogs1 = new Blog { BlogId = 2, Url = "http://www.tianmao.com" };
            Person person = new Person { PersonId = 1, Name = "123" };
            Person person1 = new Person { PersonId = 2, Name = "456" };
            modelBuilder.Entity<Post>().HasData(posts);
            modelBuilder.Entity<Blog>().HasData(blogs, blogs1);
            modelBuilder.Entity<Person>().HasData(person, person1);
        }

        #region 自定义标量函数
        [DbFunction]
        public static string MyFunction(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public int PersonId { get; set; }
        public virtual Person Author { get; set; }
    }
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}
