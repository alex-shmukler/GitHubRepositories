using GitHubRepositories.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GitHubRepositories.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Search(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            string contentType = "application/json";

            string path = "https://api.github.com/search/repositories?q=" + value;

            List<Repository> list = new List<Repository>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                HttpResponseMessage response = await client.GetAsync(path);

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                string json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                try
                {
                    JObject jo = JObject.Parse(json);

                    foreach (JToken jt in jo["items"])
                    {
                        list.Add(new Repository
                        {
                            Name = jt["name"].ToString(),
                            AvatarUrl = jt["owner"]["avatar_url"].ToString()
                        });
                    }
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }

            return Content(JsonConvert.SerializeObject(list), contentType);
        }

        [HttpGet]
        public ActionResult Bookmarks()
        {
            string contentType = "application/json";

            if (Session["repositories"] is List<Repository> repositories)
            {
                return Content(JsonConvert.SerializeObject(repositories), contentType);
            }

            return Content(JsonConvert.SerializeObject(new List<Repository>()), contentType);
        }

        [HttpPost]
        public ActionResult Bookmark(Repository repository)
        {
            if(!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["repositories"] is List<Repository> repositories)
            {
                if (repositories.Any(x => x.Name == repository.Name))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Conflict);
                }

                repositories.Add(repository);
            }
            else
            {
                Session["repositories"] = new List<Repository>
                {
                    repository
                };
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}