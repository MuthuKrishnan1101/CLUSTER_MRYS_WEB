using System;
using System.Web.UI.HtmlControls;

namespace CLUSTER_MRTS
{
    public partial class FB0004R1V1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl home = (HtmlGenericControl)this.Page.Master.FindControl("hdrPageTitle");
            home.InnerText = "Batch Payment Upload";
        }
    }
}