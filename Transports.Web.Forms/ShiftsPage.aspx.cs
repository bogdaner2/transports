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
    public partial class ShiftsPage : System.Web.UI.Page
    {
        private Core.Repositories.ContextRepository<Shift> repo = new Core.Repositories.ContextRepository<Shift>();
        private readonly TransportsServiceClient _wcfClient = new TransportsServiceClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Repeater.DataSource = GetShifts();
                Repeater.DataBind();
            }
        }

        protected List<Shift> GetShifts()
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
                        _wcfClient.RemoveShift(e.CommandArgument.ToString());

                        scope.Complete();
                    }

                    Thread.Sleep(3000);
                    //var guid = new Guid(e.CommandArgument.ToString());
                    //var item = repo.Get(x => x.ShiftId == guid);
                    //repo.Remove(item);
                    Response.Redirect("shifts");
                    break;
                case "Update":
                    Response.Redirect("shiftCreate?ID=" + e.CommandArgument);
                    break;
            }
        }
        protected void OnClick(object sender, EventArgs e)
        {
            Response.Redirect("shiftCreate");
        }
    }
}