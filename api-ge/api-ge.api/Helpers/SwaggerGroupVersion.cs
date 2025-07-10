using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace api_gestiona.Helpers
{
    public class SwaggerGroupVersion : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var nameSpaceController = controller.ControllerType.Namespace;
            var apiVersion = nameSpaceController.Split(".").Last().ToLower();
            controller.ApiExplorer.GroupName = apiVersion;


        }
    }
}
