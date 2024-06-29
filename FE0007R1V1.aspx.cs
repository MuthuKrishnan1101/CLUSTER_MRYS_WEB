using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FE0007R1V1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl home = (HtmlGenericControl)this.Page.Master.FindControl("hdrPageTitle");
            home.InnerText = "Batch Upload Worklist";

        }
    }
}