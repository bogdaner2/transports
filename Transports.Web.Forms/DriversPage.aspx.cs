using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Transports.Core.Models.SQL;
using Transports.Web.Forms.Proxy;

namespace Transports.Web.Forms
{
    public partial class DriversPage : System.Web.UI.Page
    {
        private readonly Core.Repositories.ContextRepository<Driver> _repo = new Core.Repositories.ContextRepository<Driver>();
        //private readonly TransportsServiceClient _wcfClient = new TransportsServiceClient();
        private readonly string Url = "http://localhost:51727/TransportService.svc/api/drivers";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
               
                Repeater.DataSource = GetDrivers();
                Repeater.DataBind();
            }
        }

        protected List<Driver> GetDrivers()
        {
            var client = new HttpClient();

            var res = client.GetAsync(Url).GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<List<Driver>>(res.Content.ReadAsStringAsync().Result);
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    var client = new HttpClient();
                    var content = JsonConvert.SerializeObject(e.CommandArgument);
                    var buffer = Encoding.UTF8.GetBytes(content);
                    var bac = new ByteArrayContent(buffer);
                    bac.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.PutAsync(Url+ "/delete", bac).GetAwaiter().GetResult();

                    Response.Redirect("drivers");
                    break;
                case "Update":
                    Response.Redirect("driverCreate?ID=" + e.CommandArgument);
                    break;
            }
        }
        protected void OnClick(object sender, EventArgs e)
        {
            Response.Redirect("driverCreate");
        }
    }
}