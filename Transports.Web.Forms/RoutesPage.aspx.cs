using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Transports.Core.Models.SQL;
using Transports.Web.Forms.Proxy;

namespace Transports.Web.Forms
{
    public partial class RoutesPage : System.Web.UI.Page
    {
        private Core.Repositories.ContextRepository<Route> repo = new Core.Repositories.ContextRepository<Route>();
        private readonly TransportsServiceClient _wcfClient = new TransportsServiceClient();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Repeater.DataSource = Getroutes();
                Repeater.DataBind();
            }
        }

        protected List<Route> Getroutes()
        {
            return repo.GetAll().ToList();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        _wcfClient.RemoveRoute(e.CommandArgument.ToString());

                        scope.Complete();
                    }

                    Thread.Sleep(3000);
                    //var guid = new Guid(e.CommandArgument.ToString());
                    //var item = repo.Get(x => x.RouteId == guid);
                    //repo.Remove(item);
                    Response.Redirect("routes");
                    break;
                case "Update":
                    Response.Redirect("routeCreate?ID=" + e.CommandArgument);
                    break;
            }
        }
        protected void OnClick(object sender, EventArgs e)
        {
            Response.Redirect("routeCreate");
        }
    }
}