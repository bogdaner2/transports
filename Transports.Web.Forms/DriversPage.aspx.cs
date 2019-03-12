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
            Repeater.DataSource = GetDrivers();
            Repeater.DataBind();
        }

        protected List<Driver> GetDrivers()
        {
            return repo.GetAll().ToList();
        }
    }
}