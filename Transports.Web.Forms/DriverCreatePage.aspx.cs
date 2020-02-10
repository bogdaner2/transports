using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Transactions;
using Newtonsoft.Json;
using Transports.Core.Models;
using Transports.Core.Models.SQL;
using Transports.Web.Forms.Proxy;

namespace Transports.Web.Forms
{
    public partial class DriverCreatePage : System.Web.UI.Page
    {
        //private Core.Repositories.ContextRepository<Driver> repo = new Core.Repositories.ContextRepository<Driver>();
        //private readonly TransportsServiceClient _wcfClient = new TransportsServiceClient();
        private readonly string Url = "http://localhost:51727/TransportService.svc/api/drivers";

        private Driver _loadedDriver;
        private Guid _id;

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["ID"];

            //_wcfClient.Open();


            if (id != null)
            {
                _id = Guid.Parse(id);

            }

            if (!IsPostBack)
            {

                if (id != null)
                {

                    var client = new HttpClient();
                    var res = client.GetAsync(Url).GetAwaiter().GetResult();
                    var drivers = JsonConvert.DeserializeObject<List<Driver>>(res.Content.ReadAsStringAsync().Result);
                    var driver = drivers.FirstOrDefault(x => x.DriverId == _id);
                    _loadedDriver = driver;

                    driverName.Text = _loadedDriver.Name;
                    driverAge.Text = _loadedDriver.Age.ToString();
                    driverRang.Text = _loadedDriver.Rang.ToString();

                    btnCreate.Visible = false;
                    Label.Text = "Update driver";
                }
                else
                {
                    btnUpdate.Visible = false;
                    Label.Text = "Create new driver";
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var driver = new Driver();

            driver.Name = driverName.Text;
            driver.Age = int.Parse(driverAge.Text);
            Enum.TryParse(driverRang.Text, out RangEnum rang);
            driver.Rang = rang;

            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(driver);
            var buffer = Encoding.UTF8.GetBytes(content);
            var bac = new ByteArrayContent(buffer);
            bac.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.PostAsync(Url, bac).GetAwaiter().GetResult();

            Response.Redirect("drivers");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var driver = new Driver
            {
                DriverId = _id,
                Name = driverName.Text
            };
            driver.Age = int.Parse(driverAge.Text);
            Enum.TryParse(driverRang.Text, out RangEnum rang);
            driver.Rang = rang;

            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(driver);
            var buffer = Encoding.UTF8.GetBytes(content);
            var bac = new ByteArrayContent(buffer);
            bac.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.PutAsync(Url, bac).GetAwaiter().GetResult();

            Response.Redirect("drivers");
        }
    }
}