using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public class RouteConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var controllerNamespace = controller.ControllerType.Namespace;
                var controllerName = controller.ControllerName;

                // Apply ApiController attribute to all controllers
                controller.Filters.Add(new ApiControllerAttribute());

                // Apply Route attribute to all actions
                foreach (var action in controller.Actions)
                {
                    var actionName = action.ActionName;

                    // Apply Route attribute with action name as template
                    action.Selectors[0].AttributeRouteModel = new AttributeRouteModel
                    {
                        Template = $"api/{controllerName}/{actionName}"
                    };
                }
            }
        }
    }
}
