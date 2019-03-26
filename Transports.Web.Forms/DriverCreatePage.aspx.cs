using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Transports.Core.Models;
using Transports.Core.Models.SQL;
using Transports.Web.Forms.Proxy;

namespace Transports.Web.Forms
{
    public partial class DriverCreatePage : System.Web.UI.Page
    {
        private Core.Repositories.ContextRepository<Driver> repo = new Core.Repositories.ContextRepository<Driver>();
        private readonly TransportsServiceClient _wcfClient = new TransportsServiceClient();

        private Driver _loadedDriver;
        private Guid _id;

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["ID"];

            if (id != null)
            {
                _id = Guid.Parse(id);

            }

            if (!IsPostBack)
            {

                if (id != null)
                { 
                    _loadedDriver = repo.Get(x => x.DriverId == _id);

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
            Driver driver = new Driver();

            driver.Name = driverName.Text;
            driver.Age = int.Parse(driverAge.Text);
            Enum.TryParse(driverRang.Text, out RangEnum rang);
            driver.Rang = rang;

            // repo.Create(driver);

            _wcfClient.Open();

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var serialized = JsonConvert.SerializeObject(driver);
                Debug.WriteLine(serialized);

                _wcfClient.CreateDriver(serialized);

                Thread.Sleep(1000);

                scope.Complete();
            }

            _wcfClient.Close();

            Response.Redirect("drivers");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var driver = repo.Get(x => x.DriverId == _id);
            driver.Name = driverName.Text;
            driver.Age = int.Parse(driverAge.Text);
            Enum.TryParse(driverRang.Text, out RangEnum rang);
            driver.Rang = rang;

            repo.Update(driver);

            Response.Redirect("drivers");
        }
    }
}