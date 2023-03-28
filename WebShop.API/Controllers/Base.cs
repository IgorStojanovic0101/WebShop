using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Reflection;
using WebShop.DataAccess.Repository.IRepository;

namespace WebShop.API.Controllers
{
  
    public class Base<T> : ControllerBase where T : class, new()
    {
        private List<object> args;
        public Base(List<object> objs)
        {
            this.args = objs;
        }

        public R Call<R>(Expression<Func<T, R>> value)
        {
            var instance = (T)Activator.CreateInstance(typeof(T), args);


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
