using GitHubRepositories.Models;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace GitHubRepositories.ModelBinder
{
    public class RepositoryModelBinder: DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(Repository))
            {
                var data = GetDocumentContents(controllerContext.HttpContext.Request);

                return JsonConvert.DeserializeObject<Repository>(data);
            }

            return base.BindModel(controllerContext, bindingContext);
        }

        private string GetDocumentContents(System.Web.HttpRequestBase Request)
        {
            string documentContents;
            using (var receiveStream = Request.InputStream)
            {
                using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    readStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    documentContents = readStream.ReadToEnd();
                }
            }
            return documentContents;
        }
    }
}