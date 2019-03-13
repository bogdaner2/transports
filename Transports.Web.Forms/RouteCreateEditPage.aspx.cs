using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Transports.Core.Models.SQL;

namespace Transports.Web.Forms
{
    public partial class RouteCreateEditPage : System.Web.UI.Page
    {
        private Core.Repositories.ContextRepository<Route> repo = new Core.Repositories.ContextRepository<Route>();
        private Core.Repositories.ContextRepository<Shift> repoShifts = new Core.Repositories.ContextRepository<Shift>();

        private Route _loadedRoute;
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
            Route Route = new Route();

            Route.IsTrafficJam = bool.Parse(traffic.Text);
            Route.Length = int.Parse(routeLength.Text);
            Route.EstimatedTime = int.Parse(estimate.Text);

            var shiftId = new Guid(dropdown.SelectedValue);

            Route.Shift = repoShifts.Get(x => x.ShiftId == shiftId);

            repo.Create(Route);

            Response.Redirect("Routes");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var Route = repo.Get(x => x.RouteId == _id);

            Route.IsTrafficJam = bool.Parse(traffic.Text);
            Route.Length = int.Parse(routeLength.Text);
            Route.EstimatedTime = int.Parse(estimate.Text);

            var shiftId = new Guid(dropdown.SelectedValue);

            Route.Shift = repoShifts.Get(x => x.ShiftId == shiftId);

            repo.Update(Route);

            Response.Redirect("Routes");
        }

        protected void dropdown_OnSelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}