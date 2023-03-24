using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Text.Json;

public class AddFromBodyFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var parameter in context.ActionDescriptor.Parameters)
        {
            if (parameter.BindingInfo?.BindingSource != null)
            {
                continue;
            }

            var type = parameter.ParameterType;

            if (type == typeof(string))
            {
                var value = await GetRequestBody(context.HttpContext.Request.Body);
                context.ActionArguments[parameter.Name] = value;
            }
            else
            {
                var value = Activator.CreateInstance(type);

                var bodyString = await GetRequestBody(context.HttpContext.Request.Body);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                value = System.Text.Json.JsonSerializer.Deserialize(bodyString, type, options);

                context.ActionArguments[parameter.Name] = value;
            }
        }

        await next();
    }

    private async Task<string> GetRequestBody(Stream body)
    {
        using var reader = new StreamReader(body);
        return await reader.ReadToEndAsync();
    }
}