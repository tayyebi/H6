using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebCore.Controllers
{
    public class ValuesController : ApiController
    {

        string path = HttpContext.Current.Server.MapPath("~/App_Data/Posts/");
        public IEnumerable<PostModel> Get()
        {

            List<PostModel> pmList = new List<PostModel>();
            foreach (string folder in Directory.GetDirectories(path))
            {
                string p = path;
                PostModel pm = new PostModel();
                string f = folder.Substring(p.Length, folder.Length - p.Length);
                pm.id = Guid.Parse(f);
                p = p + pm.id.ToString();
                pm.Title = File.ReadAllText(p + "\\" + "Title.txt");
                pm.SubmitDateTime = DateTime.Parse(File.ReadAllText(p + "\\" + "DateTime.txt"));
                pm.Body = File.ReadAllText(p + "\\" + "Body.txt");
                p += "\\Images";
                string image = Directory.GetFiles(p)[0];
                pm.Image = File.ReadAllBytes(image);

                pmList.Add(pm);
            }
            pmList = pmList.OrderByDescending(x => x.SubmitDateTime).ToList();
            pmList = pmList.Take(10).ToList();
            return pmList;
        }

        public PostModel Get(Guid id)
        {
            return null;
        }

        public async Task<HttpResponseMessage> Post()
        {
            var request = await Request.Content.ReadAsMultipartAsync();

            string Id = await request.Contents[0].ReadAsStringAsync();
            string Title = await request.Contents[1].ReadAsStringAsync();
            string Body = await request.Contents[2].ReadAsStringAsync();

            string FileName = request.Contents[3].Headers.ContentDisposition.FileName;
            FileName = FileName.Substring(1, FileName.Length - 2);
            byte[] FileByteArray = await request.Contents[3].ReadAsByteArrayAsync();

            path += Id + "\\";
            Directory.CreateDirectory(path);
            File.WriteAllText(path + "Title.txt", Title);
            File.WriteAllText(path + "Body.txt", Body);
            File.WriteAllText(path + "DateTime.txt", DateTime.Now.ToString());

            path += "Images\\";
            Directory.CreateDirectory(path);
            File.WriteAllBytes(path + FileName, FileByteArray);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public void Delete(Guid id)
        {
            Directory.Delete(path + id.ToString(), true);
        }
    }
}
