using System;
using System.Threading;
using System.Transactions;
using Newtonsoft.Json;
using Transports.Core.Models.SQL;
using Transports.Web.Forms.Proxy;

namespace Transports.Web.Forms
{
    public partial class RouteCreateEditPage : System.Web.UI.Page
    {
        private Core.Repositories.ContextRepository<Route> repo = new Core.Repositories.ContextRepository<Route>();
        private Core.Repositories.ContextRepository<Shift> repoShifts = new Core.Repositories.ContextRepository<Shift>();
        private readonly TransportsServiceClient _wcfClient = new TransportsServiceClient();


        private Route _loadedRoute;
        private Guid _id;


        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["ID"];
            _wcfClient.Open();

            if (id != null)
            {
                _id = Guid.Parse(id);

            }

            if (!IsPostBack)
            {

                if (id != null)
                {
                    _loadedRoute = repo.Get(x => x.RouteId == _id);

                    traffic.Text = _loadedRoute.IsTrafficJam.ToString();
                    routeLength.Text = _loadedRoute.Length.ToString();
                    estimate.Text = _loadedRoute.EstimatedTime.ToString();

                    dropdown.SelectedValue = _loadedRoute.Shift.ShiftId.ToString();

                    btnCreate.Visible = false;
                    Label.Text = "Update Route";
                }
                else
                {
                    btnUpdate.Visible = false;
                    Label.Text = "Create new Route";
                }

                dropdown.DataSource = repoShifts.GetAll();
                DataBind();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var route = new Route();

            route.ShiftId = Guid.NewGuid();
            route.IsTrafficJam = bool.Parse(traffic.Text);
            route.Length = int.Parse(routeLength.Text);
            route.EstimatedTime = int.Parse(estimate.Text);

            var shiftId = new Guid(dropdown.SelectedValue);
            route.ShiftId = shiftId;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var serialized = JsonConvert.SerializeObject(route);

                _wcfClient.CreateRoute(serialized);

                scope.Complete();
            }

            Thread.Sleep(3000);

            // Route.Shift = repoShifts.Get(x => x.ShiftId == shiftId);

            // repo.Create(Route);

            Response.Redirect("Routes");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var route = repo.Get(x => x.RouteId == _id);

            route.IsTrafficJam = bool.Parse(traffic.Text);
            route.Length = int.Parse(routeLength.Text);
            route.EstimatedTime = int.Parse(estimate.Text);

            var shiftId = new Guid(dropdown.SelectedValue);
            route.ShiftId = shiftId;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var serialized = JsonConvert.SerializeObject(route);

                _wcfClient.UpdateRoute(serialized);

                scope.Complete();
            }

            //Route.Shift = repoShifts.Get(x => x.ShiftId == shiftId);

            //repo.Update(Route);

            Response.Redirect("Routes");
        }

        protected void dropdown_OnSelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}