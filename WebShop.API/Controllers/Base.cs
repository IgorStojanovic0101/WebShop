using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Reflection;


namespace WebShop.API.Controllers
{
  
    public class Base<T> : ControllerBase where T : class, new()
    {
       
     /*   public Expression<Func<T, object>> wsPost { get; private set; }
        // public BankAccountsTransactionsMain ReturnNumberOfTransactions(SearchModel model) => Call<BankAccountsTransactionsMain>(x => x.ReturnNumberOfTransactions(model));
        public R Call<R>(Expression<Func<T, R>> p)
        {
            var instance = new T();
            var methodName = ((MethodCallExpression)p.Body).Method.Name;
            var methodInfo = typeof(T).GetMethod(methodName);
            var instanceParam = Expression.Parameter(typeof(T), "instance");
            var call = Expression.Call(instanceParam, methodInfo);
            var lambda = Expression.Lambda<Func<T, R>>(call, instanceParam).Compile();
            return lambda(instance);
        }
     */
        ///  public R Call<R, A>(Expression<Func<T, A, R>> p, A arg)
        //  {
        // var methodCall = (MethodCallExpression)p.Body;
        // var instance = new T();
        // var argExpression = Expression.Constant(arg, typeof(A));
        // var methodCallExpression = Expression.Call(instance, methodCall.Method, argExpression);
        // var lambda = Expression.Lambda<Func<R>>(methodCallExpression);
        // var compiledLambda = lambda.Compile();
        // return null();
        //  }

        /*   public int Call(Expression<Func<T, int>> value, Product product)
           {
               var instance = new T();
               var methodCall = (MethodCallExpression)value.Body;
               var instanceExpression = Expression.Constant(instance, typeof(T));
               var argumentExpression = Expression.Constant(product, typeof(Product));
               var methodCallExpression = Expression.Call(instanceExpression, methodCall.Method, argumentExpression);
               var lambda = Expression.Lambda<Func<int>>(methodCallExpression);
               var compiledLambda = lambda.Compile();
               return compiledLambda();
           }*/
        /*public R Call<R, T1>(Expression<Func<T, R>> value, T1 inst)
        {
            var instance = new T();
            var methodCall = (MethodCallExpression)value.Body;
           // var obj = methodCall.Arguments[0];
            var instanceExpression = Expression.Constant(instance, typeof(T));
            var argumentExpression = Expression.Constant(inst, typeof(T1));
            var methodCallExpression = Expression.Call(instanceExpression, methodCall.Method, argumentExpression);
            var lambda = Expression.Lambda<Func<R>>(methodCallExpression);
            var compiledLambda = lambda.Compile();
            return compiledLambda();
        }*/

        public R Call<R>(Expression<Func<T, R>> value)
        {
            var instance = new T();

            var instanceExpression = Expression.Constant(instance, typeof(T));

            var methodCall = (MethodCallExpression)value.Body;
            object argumentValue;
           
          
            MethodCallExpression? methodCallExpression;
            if (methodCall.Arguments.Count > 0)
            {
                var argumentsExpression = new List<ConstantExpression>();
                foreach (var argument in methodCall.Arguments)
                {

                    if (argument is ConstantExpression constantExpression)
                    {
                        // Argument is a constant value
                        argumentValue = constantExpression.Value;
                    }
                    else if (argument is MemberExpression memberExpression)
                    {
                        // Argument is a member access expression
                        var field = (FieldInfo)memberExpression.Member;
                        var instanceExpression1 = (ConstantExpression)memberExpression.Expression;
                        var instanceValue = instanceExpression1.Value;
                        argumentValue = field.GetValue(instanceValue);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid argument expression type");
                    }

                    var argumentExpression = Expression.Constant(argumentValue, argument.Type);

                    argumentsExpression.Add(argumentExpression);
                }

                methodCallExpression = Expression.Call(instanceExpression, methodCall.Method, argumentsExpression);

            }
            else
                methodCallExpression = Expression.Call(instanceExpression, methodCall.Method);



            var lambda = Expression.Lambda<Func<R>>(methodCallExpression);
            var compiledLambda = lambda.Compile();
            return compiledLambda();
        }
    }
}
