using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharePointPowerBIIntegrationWeb.Models
{
    public class TilesModel
    {
        public PBITile[] value { get; set; }
    }
    public class PBITile
    {
        public string id { get; set; }
        public string title { get; set; }
        public string embedUrl { get; set; }
    }
}