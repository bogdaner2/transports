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

        protected void Page_Load(object sender, EventArgs e)
        {
            Driver driver = new Driver();

            if (TryUpdateModel(driver, new FormValueProvider(ModelBindingExecutionContext)))
            {
                driver.Name = driverName.Text;
                driver.Age = int.Parse(driverAge.Text);
                driver.Rang = (RangEnum)int.Parse(driverRang.Text);

                repo.Create(driver);

                Response.Redirect("drivers");
            }
        }
    }
}