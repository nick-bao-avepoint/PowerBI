﻿using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SharePointPowerBIIntegrationWeb
{
    public partial class Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Redirect uri must match the redirect_uri used when requesting Authorization code.
            string redirectUri = ConfigurationManager.AppSettings["ReplyUrl"];
            string authorityUri = "https://login.windows.net/common/oauth2/authorize/";

            // Get the auth code
            string code = Request.Params.GetValues(0)[0];

            // Get auth token from auth code       
            TokenCache TC = new TokenCache();

            AuthenticationContext AC = new AuthenticationContext(authorityUri, TC);
            ClientCredential cc = new ClientCredential
                (ConfigurationManager.AppSettings["ClientId"],
                ConfigurationManager.AppSettings["ClientSecret"]);

            AuthenticationResult AR = AC.AcquireTokenByAuthorizationCode(code, new Uri(redirectUri), cc);

            //Set Session "authResult" index string to the AuthenticationResult
            Session["authResult"] = AR;

            //Redirect back to Default.aspx
            Response.Redirect("/home/dashboard");
        }
    }
}