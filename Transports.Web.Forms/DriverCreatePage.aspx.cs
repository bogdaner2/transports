using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Transports.Core.Models;
using Transports.Core.Models.SQL;

namespace Transports.Web.Forms
{
    public partial class DriverCreatePage : System.Web.UI.Page
    {
        private Core.Repositories.ContextRepository<Driver> repo = new Core.Repositories.ContextRepository<Driver>();
        private Driver _loadedDriver;

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["ID"];

            if (id != null)
            {
                var guid = Guid.Parse(id);
                _loadedDriver = repo.Get(x => x.DriverId == guid);

                driverName.Text = _loadedDriver.Name;
                driverAge.Text = _loadedDriver.Age.ToString();
                driverRang.Text = _loadedDriver.Rang.ToString();

                btnCreate.Visible = false;
            }
            else
            {
                btnUpdate.Visible = false;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Driver driver = new Driver();

            driver.Name = driverName.Text;
            driver.Age = int.Parse(driverAge.Text);
            driver.Rang = (RangEnum)int.Parse(driverRang.Text);

            repo.Create(driver);

            Response.Redirect("drivers");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            _loadedDriver.Name = driverName.Text;
            _loadedDriver.Age = int.Parse(driverAge.Text);
            _loadedDriver.Rang = (RangEnum)int.Parse(driverRang.Text);

            repo.Save();

            Response.Redirect("drivers");
        }
    }
}