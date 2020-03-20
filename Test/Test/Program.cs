using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("测试linq lambda expression");
            //ParameterExpression parameter = Expression.Parameter(typeof(int), "name");
            ////LambdaExpression lambda = Expression.Lambda(Expression.Add(parameter, Expression.Constant(1)), parameter);
            //LambdaExpression lambda = Expression.Lambda(Expression.LessThan(parameter, Expression.Constant(1)), parameter);
            //Console.WriteLine($"lambda.Body:{lambda.Body}");
            //Console.WriteLine($"lambda.Parameters:{lambda.Parameters}");
            //Console.WriteLine($"lambda:{lambda}");
            //var x = lambda.Compile().DynamicInvoke(666);
            //x = x.ToString();
            //switch (x)
            //{
            //    case int xx: Console.WriteLine($"lambda rrrr:{xx}"); break;
            //    case string ddd: Console.WriteLine($"lambda  sssss:{ddd}"); break;
            //    default:
            //        break;
            //}

            //Expression<Func<int, bool>> expression = i => i <= 8;
            //var dd = expression.Compile();
            //var x = dd(8);
            //Console.WriteLine($"{x}");



            BlockExpression blockExpr = Expression.Block(
                Expression.Call(null, typeof(Console).GetMethod("Write", new Type[] { typeof(String) }), Expression.Constant("Hello ")),
                Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(String) }), Expression.Constant("World!")),
                Expression.Constant(42));

            Console.WriteLine("The result of executing the expression tree:");
            // The following statement first creates an expression tree,
            // then compiles it, and then executes it.           
            var result = Expression.Lambda<Func<int>>(blockExpr).Compile()();

            // Print out the expressions from the block expression.
            Console.WriteLine("The expressions from the block expression:");
            foreach (var expr in blockExpr.Expressions)
                Console.WriteLine(expr.ToString());

            // Print out the result of the tree execution.
            Console.WriteLine("The return value of the block expression:");
            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
