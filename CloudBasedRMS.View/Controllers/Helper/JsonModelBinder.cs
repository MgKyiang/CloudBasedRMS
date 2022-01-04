using System.Web.Mvc;
namespace CloudBasedRMS.View.Controllers.Helper
{
    public class JsonModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            // If the request is not of content type "application/json"
            // then use the default model binder.
            if (!IsJSONRequest(controllerContext))
            {
                return base.BindModel(controllerContext, bindingContext);
            }

            // Get the JSON data posted
            var request = controllerContext.HttpContext.Request;
            var jsonStringData =
                new System.IO.StreamReader(request.InputStream).ReadToEnd();

            return new System.Web.Script.Serialization.JavaScriptSerializer()
                .Deserialize(jsonStringData, bindingContext.ModelMetadata.ModelType);
        }

        private bool IsJSONRequest(ControllerContext controllerContext)
        {
            var contentType = controllerContext.HttpContext.Request.ContentType;
            return contentType.Contains("application/json");
        }
    }
}
