using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using SharePointPowerBIIntegrationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SharePointPowerBIIntegrationWeb.Controllers
{
    public class PowerBITileController : Controller
    {
        private static readonly string baseUri = "https://api.powerbi.com/beta/myorg/";

        public AuthenticationResult authResult { get; set; }

        public ActionResult Index()
        {
            if (Session["authResult"] != null)
            {
                //Get the authentication result from the session
                authResult = (AuthenticationResult)Session["authResult"];
            }
            else
            {
                return View();
            }

            string dashboardId = "1d136449-b827-4ed2-8a09-755675dfb55e";

            //Configure datasets request
            System.Net.WebRequest request = System.Net.WebRequest.Create(String.Format("{0}Dashboards/{1}/Tiles", baseUri, dashboardId)) as System.Net.HttpWebRequest;
            request.Method = "GET";
            request.ContentLength = 0;
            request.Headers.Add("Authorization", String.Format("Bearer {0}", authResult.AccessToken));

            //Get datasets response from request.GetResponse()
            using (var response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                //Get reader from response stream
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    string responseContent = reader.ReadToEnd();

                    //Deserialize JSON string
                    TilesModel tiles = JsonConvert.DeserializeObject<TilesModel>(responseContent);
                    if (tiles != null)
                    {
                        return View(tiles);
                    }
                    else
                    {
                        return View(new TilesModel() { value = new PBITile[0] });
                    }
                }
            }
        }
    }

}
