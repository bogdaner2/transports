using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly TransportsServiceClient _wcfClient = new TransportsServiceClient();


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
            return _repo.GetAll().ToList();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        _wcfClient.RemoveDriver(e.CommandArgument.ToString());
                        scope.Complete();
                    }

                    Thread.Sleep(3000);
                    //var guid = new Guid(e.CommandArgument.ToString());
                    //var item = _repo.Get(x => x.DriverId == guid);
                    //_repo.Remove(item);
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