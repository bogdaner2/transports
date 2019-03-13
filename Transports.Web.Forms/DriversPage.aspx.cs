using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Transports.Core.Models.SQL;

namespace Transports.Web.Forms
{
    public partial class DriversPage : System.Web.UI.Page
    {
        private Core.Repositories.ContextRepository<Driver> repo = new Core.Repositories.ContextRepository<Driver>();

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
            return repo.GetAll().ToList();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    var guid = new Guid(e.CommandArgument.ToString());
                    var item = repo.Get(x => x.DriverId == guid);
                    repo.Remove(item);
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