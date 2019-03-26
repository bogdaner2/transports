
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Transports.Core.Models.SQL;
using Transports.Web.Forms.Proxy;

namespace Transports.Web.Forms
{
    public partial class ShiftAddEditPage : System.Web.UI.Page
    {
        private Core.Repositories.ContextRepository<Shift> repo = new Core.Repositories.ContextRepository<Shift>();
        private readonly TransportsServiceClient _wcfClient = new TransportsServiceClient();

        private Shift _loadedShift;
        private Guid _id;

        private static DateTime _startDate;
        private static DateTime _endDate;


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
                    _loadedShift = repo.Get(x => x.ShiftId == _id);

                    Calendar1.SelectedDate = _loadedShift.Start;
                    Calendar2.SelectedDate = _loadedShift.End;

                    btnCreate.Visible = false;
                    Label.Text = "Update Shift";
                }
                else
                {
                    btnUpdate.Visible = false;
                    Label.Text = "Create new Shift";
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var shift = new Shift();

            shift.ShiftId = Guid.NewGuid();
            shift.Start = _startDate;
            shift.End = _endDate;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var serialized = JsonConvert.SerializeObject(shift);

                _wcfClient.CreateShift(serialized);

                scope.Complete();
            }

            Thread.Sleep(3000);

            //repo.Create(shift);

            Response.Redirect("Shifts");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var shift = repo.Get(x => x.ShiftId == _id);

            shift.Start = _startDate;
            shift.End = _endDate;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var serialized = JsonConvert.SerializeObject(shift);

                _wcfClient.UpdateShift(serialized);

                scope.Complete();
            }

            // repo.Update(shift);

            Response.Redirect("Shifts");
        }

        protected void Calendar1_OnSelectionChanged(object sender, EventArgs e)
        {
            _startDate = Calendar1.SelectedDate;
        }

        protected void Calendar2_OnSelectionChanged(object sender, EventArgs e)
        {
            _endDate = Calendar2.SelectedDate;
        }
    }
}