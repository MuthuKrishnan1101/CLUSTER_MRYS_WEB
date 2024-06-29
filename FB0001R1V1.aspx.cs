using CLUSTER_MRTS.CommonFunction;
using G10CertificateValidation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FB0001R1V1 : System.Web.UI.Page
    { 

        public int intpageIndexBBGeneration = 0;
        public int intrecFromBBGeneration = 0;
        public int intrecToBBGeneration = 0;

        public int intpageIndexBBgenerateDetail = 0;
        public int intrecFromBBgenerateDetail = 0;
        public int intrecToBBgenerateDetail = 0;

        public int intpageIndexblockbillingsearch = 0;
        public int intrecFromblockbillingsearch = 0;
        public int intrecToblockbillingsearch = 0;

        public int intintpageIndexblockbillingsearchDetail = 0;
        public int intrecFromblockbillingsearchDetail = 0;
        public int intrecToblockbillingsearchDetail = 0;

        public string stringformIdPagingBBGeneration = "MRPaymentViewPopupPaging"; 
        public string stringformIdPagingBBgenerateDetail = "MRPaymentViewgvlistPopupPaging";

        public string stringformIdPagingblockbillingsearch = "MRPaymentViewgvInvoicePopupPaging";
        public string stringformIdPagingblockbillingsearchDetail = "MRPaymentViewgvlistPopupPaging";

        public DataSet objDatasetAppsVariables;

        public int pageIndexdropdownpopup = 0;
        public int recFromdropdownpopup = 0;
        public int recTodropdownpopup = 10;
        public string stringformIdddlpopup = "FC0001RropdownpopupPaging";

        public bool boolbouser = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CommonFunctions.IsActive())
            {
                try
                {
                    recTodropdownpopup = CommonFunctions.GridViewPagesize(stringformIdddlpopup);

                    intrecToBBGeneration = CommonFunctions.GridViewPagesize(stringformIdPagingBBGeneration);
                    intrecToBBgenerateDetail = CommonFunctions.GridViewPagesize(stringformIdPagingBBgenerateDetail);

                    intrecToblockbillingsearch = CommonFunctions.GridViewPagesize(stringformIdPagingblockbillingsearch);
                    intrecToblockbillingsearchDetail = CommonFunctions.GridViewPagesize(stringformIdPagingblockbillingsearchDetail);

                    if (Session["ssnUserRole"] != null)
                    {
                        DataTable objuserRole = (DataTable)Session["UserRolestable"];

                        if (objuserRole != null && objuserRole.Rows.Count > 0)
                        {
                            if (objuserRole.Select("Group_ID= 'FINANCE'").Length > 0)
                            {
                                boolbouser = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "HideTab1", "document.getElementById('tab2').style.display = 'none';", true);
                                string script = "<script type='text/javascript'>activatetab('#tab_1');</script>";
                                ClientScript.RegisterStartupScript(this.GetType(), "ActivateTab2", script);

                            }

                        }
                    }
                    if (!IsPostBack)
                    { 
                        VerifyAccessRights();
                        CommonFunctions.HeaderName(this, "FB0001R1V1");
                        
                        ClientScript.RegisterStartupScript(typeof(String), "ClientScript", "<script language='JavaScript'> ;SetTab('#custom-nav-profile');</script>");
                        ViewState["vsSortDirection"] = " ASC";
                        ViewState["vsSortExpression"] = "";
                        ClearValues();
                         
                        LoadRequestorTypes(); 
                        LoadStatus(); 
                    }

                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }

            }

        }
        #region Loaddata
        private void ClearValues()//fix
        {
            //txtDateFrm.Text = "22-" + DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
            //txtDateTo.Text = "21-" + DateTime.Now.ToString("MM-yyyy");
            txtDateFrm.Text = "";
            txtDateTo.Text = "";
            Session["stringDMLIndicator"] = "I";
            txtDateFrm.Focus();


            txtInv.Text = "";
            txtReqNo.Text = "";
            txtHRNID.Text = "";
            txtDateFrmSearch.Text = "";
            txtDateToSearch.Text = "";
            ddlStatus.ClearSelection();
            txtReqname.Text = "";
            txtReqID.Text = "";
            txtSName.Text = "";
            ddlRequestorType.ClearSelection();
            txtInv.Focus();

        }
      
        private void LoadRequestorTypes()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0028R1V1";
            string stringOrderBy = "short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            objDatasetResult = new DataSet();
            DataTable objdatatableLoadRequestorTypes = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringexp = "";
            string stringServiceType = "";
            try
            {

                stringexp = "And mrreqts.be_id= '" + stringbeid + "' And mrreqts.delmark= 'N'";
                stringServiceType = "List1R1V1";
                if (Session["SSNLOADREQUESTORTYPES"] != null)
                {
                    objdatatableLoadRequestorTypes = (DataTable)Session["SSNLOADREQUESTORTYPES"];
                }
                if ((objdatatableLoadRequestorTypes == null) || (objdatatableLoadRequestorTypes != null && objdatatableLoadRequestorTypes.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadRequestorTypes = objDatasetResult.Tables["t1"];
                            Session["SSNLOADREQUESTORTYPES"] = objdatatableLoadRequestorTypes;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadRequestorTypes != null && objdatatableLoadRequestorTypes.Rows.Count > 0)
                {
                    ddlRequestorType.DataValueField = "reqtyp_id";
                    ddlRequestorType.DataTextField = "short_name";
                    ddlRequestorType.DataSource = objdatatableLoadRequestorTypes;
                    ddlRequestorType.DataBind();
                    ddlRequestorType.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlRequestorType.DataSource = null;
                    ddlRequestorType.DataBind();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                objdatatableLoadRequestorTypes = null;
                stringbeid = null;
                stringexp = null;
                stringServiceType = null;
            }
        }
        private void LoadStatus()
        {
            try
            {
                ddlStatus.Items.Clear();
                ddlStatus.Items.Insert(0, "");
                ddlStatus.Items.Insert(1, "PENDING");
                ddlStatus.Items.Insert(2, "PAID");
                ddlStatus.SelectedIndex = 0;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        #endregion
        #region BBGeneration
        protected void BtnGen_Click(object sender, EventArgs e)
        {
            try
            {
                SearchRecordsBBGeneration();
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        private bool ValidateControlsBBGeneration() 
        {
            try
            {
                bool boolStatus = true;
                string stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";

                if (txtDateFrm.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Date From" + "\\r\\n";
                    boolStatus = false;
                }

                if (txtDateTo.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Date To" + "\\r\\n";
                    boolStatus = false;
                }

                if (!boolStatus)
                {
                    if (stringOverallMsg.Trim().Length > 0)
                    {
                        stringOverallMsg = stringOverallMsg.Trim() + " ";
                        stringOverallMsg = stringOverallMsg.Remove(stringOverallMsg.Length - 1, 1);
                        CommonFunctions.ShowMessageboot(this, stringOverallMsg);
                        return boolStatus;
                    }
                }

                return true;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return false;
            }

        }
        private bool ValidateBusinessLogicBBGeneration() 
        {
            try
            {
                bool boolStatus = true;
                string stringOverallMsg = "";

                if (txtDateFrm.Text.Trim().Length > 0 && !CommonFunctions.ValidDateTime(txtDateFrm.Text.Trim()))
                {
                    stringOverallMsg += "Date From should be a valid date.";
                    boolStatus = false;
                }

                if (txtDateTo.Text.Trim().Length > 0 && !CommonFunctions.ValidDateTime(txtDateTo.Text.Trim()))
                {
                    stringOverallMsg += "Date To should be a valid date.";
                    boolStatus = false;
                }

                if (boolStatus)
                {
                    DateTime objDateTimeFrom = DateTime.Now;
                    DateTime objDateTimeTo = DateTime.Now;

                    if (txtDateFrm.Text.Trim().Length > 0 && txtDateTo.Text.Trim().Length > 0)
                    {
                        if (!CommonFunctions.IsValidDateTime(txtDateFrm.Text.Trim(), "dd/MM/yyyy HH:mm:ss", out objDateTimeFrom))
                        {
                            boolStatus = false;
                            CommonFunctions.ShowMessageboot(this, "\"Date From\" is a Invalid date.");
                            txtDateFrm.Focus();
                        }
                        else if (!CommonFunctions.IsValidDateTime(txtDateTo.Text.Trim(), "dd/MM/yyyy HH:mm:ss", out objDateTimeTo))
                        {
                            boolStatus = false;
                            CommonFunctions.ShowMessageboot(this, "\"Date To\" is a Invalid date.");
                            txtDateTo.Focus();
                        }
                        else
                        {
                            TimeSpan objTimeSpan = objDateTimeTo.Subtract(objDateTimeFrom);
                            if (objTimeSpan.TotalDays < 0)
                            {
                                boolStatus = false;
                                CommonFunctions.ShowMessageboot(this, "\"Date To\" should be greater than Request \"Date From\".");
                            }
                            else if (objTimeSpan.TotalDays > 90)
                            {
                                boolStatus = false;
                                CommonFunctions.ShowMessageboot(this, "Date range should be less than 90 days");
                            }
                        }

                    }

                }

                if (!boolStatus)
                {
                    if (stringOverallMsg.Trim().Length > 0)
                    {
                        stringOverallMsg = stringOverallMsg.Trim() + " ";
                        stringOverallMsg = stringOverallMsg.Remove(stringOverallMsg.Length - 1, 1);
                        CommonFunctions.ShowMessageboot(this, stringOverallMsg);
                        return boolStatus;
                    }
                    else
                    {
                        return boolStatus;
                    }
                }

                return true;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return false;
            }

        }
        private void SearchRecordsBBGeneration()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3002R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            DataRow objdatarow = null;
            string stringexp = "";
            try
            { 
                if (ValidateControlsBBGeneration() && ValidateBusinessLogicBBGeneration())
                {
                    stringServiceType = "DEFAULT"; 
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t4"] != null && objDatasetResult.Tables["t4"].Rows.Count == 0)
                        {
                            objdatarow = objDatasetResult.Tables["t4"].NewRow();
                            objdatarow["be_id"] = CommonFunctions.GETBussinessEntity().ToString();
                            objdatarow["Generated_By"] = Session["G11EOSUser_Name"].ToString();
                            if (txtDateFrm.Text.Trim().Length > 0 && txtDateTo.Text.Trim().Length > 0)
                            {
                                objdatarow["From_Date"] = CommonFunctions.ConvertToDateTime(txtDateFrm.Text.Trim(), "dd-MM-yyyy");
                                objdatarow["To_Date"] = CommonFunctions.ConvertToDateTime(txtDateTo.Text.Trim(), "dd-MM-yyyy");
                            }

                            objdatarow["delmark"] = "N";
                            CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                            objDatasetResult.Tables["t4"].Rows.Add(objdatarow);
                            objDatasetResult.Tables["t4"].Rows[0].RowState.ToString();

                        }
                        objDatasetResult = objDatasetResult.GetChanges();
                        stringServiceType = "MRISService_DataManipulationR1V1";
                        objDatasetResult = CommonFunctions.DataManipulationExcelR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);
                        if (intErrorCount == 0)
                        {
                            SaveBlockBillingHistory();
                            BlockBillingSearch();
                        }
                        else
                        {
                            Errorpopup(stringOutputResult);
                        } 
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = null;
                objdatarow = null;
                stringexp = null;
            }
        }
        private bool SaveBlockBillingHistory()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3002R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            DataRow objdatarow = null;
            string stringexp = "";
            try
            {
                stringServiceType = "DEFAULT"; 
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                    {
                        objdatarow = objDatasetResult.Tables["t1"].NewRow();
                        objdatarow["be_id"] = CommonFunctions.GETBussinessEntity().ToString();

                        objdatarow["ID"] = DateTime.Now.ToString("yyyyMMddHHmmss");
                        if (txtDateFrm.Text.Trim().Length > 0 && txtDateTo.Text.Trim().Length > 0)
                        {
                            objdatarow["DATE_FROM"] = CommonFunctions.ConvertToDateTime(txtDateFrm.Text.Trim(), "dd-MM-yyyy");
                            objdatarow["DATE_TO"] = CommonFunctions.ConvertToDateTime(txtDateTo.Text.Trim(), "dd-MM-yyyy");
                        }
                        objdatarow["TRANS_DATE"] = DateTime.Now;
                        objdatarow["Remarks"] = "Block Billing Generated By ." + Session["G11EOSUser_Name"].ToString() + " on " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                        objdatarow["delmark"] = "N";
                        CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                        objDatasetResult.Tables["t1"].Rows.Add(objdatarow);
                        objDatasetResult.Tables["t1"].Rows[0].RowState.ToString();

                    }
                    objDatasetResult = objDatasetResult.GetChanges();
                    stringServiceType = "OperationServiceDML";
                    objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);
                    if (intErrorCount == 0)
                    {
                        return true;
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                        return false;
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                }


            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = null;
                objdatarow = null;
                stringexp = null;
            }

            return false;
        }
        private void BlockBillingSearch()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringOrderBy = "";
            DataTable objDataTable = null;
            pnlBBGeneration.Visible = false;
            int intRecordFrom = 0;
            int intRecordTo = 0;
            string stringformid01 = "";
            string stringServiceType = "";
            try
            {
                intRecordFrom = intrecFromBBGeneration;
                intRecordTo = intrecToBBGeneration;
                stringformid01 = "FC3003R1V1";
                stringServiceType = "List1R1V1";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid01, PrepareSearchExpressionBBGeneration(), stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);

                PopulatePagerBBGeneration(intTotalRecord, intpageIndexBBGeneration);
                lblTotalRecordsBBGeneration.InnerText = intTotalRecord.ToString();
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t1"];
                        objDataTable.DefaultView.Sort = "invoice_date desc";
                        objDataTable = objDataTable.DefaultView.ToTable();
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        pnlBBGeneration.Visible = true;
                        gvgridBBGeneration.DataSource = objDataTable;
                        gvgridBBGeneration.DataBind();
                    }
                    else
                    {
                        gvgridBBGeneration.DataSource = null;
                        gvgridBBGeneration.DataBind();
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                }


            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringOrderBy = null;
                objDataTable = null;
                intRecordFrom = 0;
                intRecordTo = 0;
                stringformid01 = null;
                stringServiceType = null;
            }
        }

        private string PrepareSearchExpressionBBGeneration()//fix
        {
            string stringExpression = null;
            try
            {
                if (txtDateFrm.Text.Trim().Length > 0)
                {
                    DateTime objDateFrom = CommonFunctions.ConvertToDateTime(txtDateFrm.Text.Trim(), "dd-MM-yyyy");
                    stringExpression += "AND CONVERT(date,Blk.From_Date)  >= CONVERT(date,'" + Convert.ToDateTime(objDateFrom).ToString("dd-MMM-yyyy") + "') ";
                }
                if (txtDateTo.Text.Trim().Length > 0)
                {
                    DateTime objDateTo = CommonFunctions.ConvertToDateTime(txtDateTo.Text.Trim(), "dd-MM-yyyy");
                    stringExpression += "AND CONVERT(date,Blk.To_Date)  <= CONVERT(date,'" + Convert.ToDateTime(objDateTo).ToString("dd-MMM-yyyy") + "') ";

                }

                return stringExpression;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return null;
            }
        }

        private void PopulatePagerBBGeneration(int recordCount, int currentPage)
        {
            ViewState["lastpagepagig"] = true;
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdPagingBBGeneration);
                double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(intpaging));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                var ssn = dblPageCount.ToString(CultureInfo.InvariantCulture).Split('.');
                if (ssn[0] == "0")
                {
                    pageCount = (int)Math.Round(dblPageCount);
                }
                if (currentPage == 0)
                {
                    currentPage = 1;
                    startIndex = 1;
                }
                else
                {
                    startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
                }

                endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
                if (currentPage > pagerSpan % 2)
                {
                    if (currentPage == 2)
                    {
                        endIndex = 5;
                    }
                    else
                    {
                        endIndex = currentPage + 2;
                    }
                }
                else
                {
                    endIndex = (pagerSpan - currentPage) + 1;
                }
                if (currentPage != 0)
                {
                    if (endIndex - (pagerSpan - 1) > startIndex)
                    {
                        startIndex = endIndex - (pagerSpan - 1);
                    }
                }
                if (endIndex > pageCount)
                {
                    endIndex = pageCount;
                    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
                }
                //Add the First Page Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("First", "1"));
                }
                //Add the Previous Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("<", (currentPage - 1).ToString()));
                }
                for (int i = startIndex; i <= endIndex; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                //Add the Next Button.
                if (currentPage < pageCount)
                {
                    pages.Add(new ListItem(">", (currentPage + 1).ToString()));
                }
                //Add the Last Button.
                if (currentPage != pageCount)
                {
                    pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                if (pageCount == 1 || pageCount == 0)
                {
                    ViewState["lastpagepagig"] = false;
                    pages.Clear();
                    //pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                rptPagergvBBGeneration.DataSource = pages;
                rptPagergvBBGeneration.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        protected void lnkPagegvInvoice_Click(object sender, EventArgs e)
        {
            int recFrom1 = 0;
            try
            {
                if (Session["PageIndex1"] != null)
                {
                    intpageIndexBBGeneration = Convert.ToInt32(Session["PageIndex1"].ToString());
                    Session["PageIndex"] = intpageIndexBBGeneration;
                }
                else
                {
                    intpageIndexBBGeneration = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndexBBGeneration != 0)
                    {
                        Session["PageIndex"] = intpageIndexBBGeneration;
                    }
                }

                if (intpageIndexBBGeneration == 1)
                {
                    intrecFromBBGeneration = 0;
                    intrecToBBGeneration = CommonFunctions.GridViewPagesize(stringformIdPagingBBGeneration);
                }
                else
                {
                    recFrom1 = (intpageIndexBBGeneration * intrecToBBGeneration) - intrecToBBGeneration;
                    intrecFromBBGeneration = recFrom1 + 1;
                    intrecToBBGeneration = recFrom1 + CommonFunctions.GridViewPagesize(stringformIdPagingBBGeneration);
                }


                BlockBillingSearch();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                recFrom1 = 0;
            }
        }


        #endregion


        #region BBgenerateDetail

        protected void lnkbtnBBGenerationGrid_Click(object sender, EventArgs e)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringOrderBy = "";
            DataTable objDataTable = null;
            string stringBoID = "";
          
            pnlBBgenerateDetail.Visible = false;
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringValue = "";
            int intRecordFrom = 0;
            int intRecordTo = 0;
            string stringformid01 = "";
            string stringServiceType = "";
            string stringexp0121 = "";
            try
            {
                if (Session["BusinessID"] != null)
                {
                    stringBoID = Session["BusinessID"].ToString();
                }
                if (sender != null)
                {
                    stringCmdArgument = ((LinkButton)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length > 0)
                        {
                            stringValue = stringValues[0];
                            intRecordFrom = intrecFromBBgenerateDetail;
                            intRecordTo = intrecToBBgenerateDetail;
                            stringformid01 = "FC3002R1V1";
                            stringServiceType = "List3R1V1";
                            stringexp0121 = "And BlkBilling.be_id= '" + stringBoID.ToString() + "' And BlkBilling.Inv_ID= '" + stringValue + "'";

                            objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid01, stringexp0121, stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);
                            PopulatePagerBBgenerateDetail(intTotalRecord, intpageIndexBBgenerateDetail);
                            intTotalRecordpaymentgenerateDetail.InnerText = intTotalRecord.ToString();
                            if (interrorcount == 0)
                            {
                                if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t3"] != null && objDatasetResult.Tables["t3"].Rows.Count > 0)
                                {
                                    objDataTable = objDatasetResult.Tables["t3"];
                                }
                                if (objDataTable != null && objDataTable.Rows.Count > 0)
                                {
                                    pnlBBgenerateDetail.Visible = true;
                                    gvGridBBgenerateDetail.DataSource = objDataTable;
                                    gvGridBBgenerateDetail.DataBind();
                                }
                                else
                                {
                                    gvGridBBgenerateDetail.DataSource = null;
                                    gvGridBBgenerateDetail.DataBind();

                                }
                            }
                            else
                            {
                                Errorpopup(stringOutputResult);
                            }
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringOrderBy = null;
                objDataTable = null;
                stringBoID = null;
            }
        }
        protected void lnkbtnRequest_IDBBgenerateDetail_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringRequestID = "";
            try
            {
                if (sender != null)
                {
                    stringCmdArgument = ((LinkButton)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length > 0)
                        {
                            stringRequestID = stringValues[0];
                            Session["REQUEST_FromSummary"] = stringRequestID.Trim();
                            Response.Redirect("FC0001R1V1.aspx?TO=Y");
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringCmdArgument = null;
                stringValues = null;
                stringRequestID = null;
            }
        }

        protected void lnkPageBBgenerateDetail_Click(object sender, EventArgs e)
        {
            int recFrom1 = 0;
            try
            {
                if (Session["PageIndex1"] != null)
                {
                    intpageIndexBBgenerateDetail = Convert.ToInt32(Session["PageIndex1"].ToString());
                    Session["PageIndex"] = intpageIndexBBgenerateDetail;
                }
                else
                {
                    intpageIndexBBgenerateDetail = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndexBBgenerateDetail != 0)
                    {
                        Session["PageIndex"] = intpageIndexBBgenerateDetail;
                    }
                }

                if (intpageIndexBBgenerateDetail == 1)
                {
                    intrecFromBBgenerateDetail = 0;
                    intrecToBBgenerateDetail = CommonFunctions.GridViewPagesize(stringformIdPagingBBgenerateDetail);
                }
                else
                {
                    recFrom1 = (intpageIndexBBgenerateDetail * intrecToBBgenerateDetail) - intrecToBBgenerateDetail;
                    intrecFromBBgenerateDetail = recFrom1 + 1;
                    intrecToBBgenerateDetail = recFrom1 + CommonFunctions.GridViewPagesize(stringformIdPagingBBgenerateDetail);
                }

                lnkbtnBBGenerationGrid_Click(null, null);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                recFrom1 = 0;
            }
        }
        private void PopulatePagerBBgenerateDetail(int recordCount, int currentPage)
        {
            ViewState["lastpagepagig"] = true;
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdPagingBBgenerateDetail);
                double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(intpaging));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                var ssn = dblPageCount.ToString(CultureInfo.InvariantCulture).Split('.');
                if (ssn[0] == "0")
                {
                    pageCount = (int)Math.Round(dblPageCount);
                }
                if (currentPage == 0)
                {
                    currentPage = 1;
                    startIndex = 1;
                }
                else
                {
                    startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
                }

                endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
                if (currentPage > pagerSpan % 2)
                {
                    if (currentPage == 2)
                    {
                        endIndex = 5;
                    }
                    else
                    {
                        endIndex = currentPage + 2;
                    }
                }
                else
                {
                    endIndex = (pagerSpan - currentPage) + 1;
                }
                if (currentPage != 0)
                {
                    if (endIndex - (pagerSpan - 1) > startIndex)
                    {
                        startIndex = endIndex - (pagerSpan - 1);
                    }
                }
                if (endIndex > pageCount)
                {
                    endIndex = pageCount;
                    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
                }
                //Add the First Page Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("First", "1"));
                }
                //Add the Previous Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("<", (currentPage - 1).ToString()));
                }
                for (int i = startIndex; i <= endIndex; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                //Add the Next Button.
                if (currentPage < pageCount)
                {
                    pages.Add(new ListItem(">", (currentPage + 1).ToString()));
                }
                //Add the Last Button.
                if (currentPage != pageCount)
                {
                    pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                if (pageCount == 1 || pageCount == 0)
                {
                    ViewState["lastpagepagig"] = false;
                    pages.Clear();
                    //pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                rptPagerBBgenerateDetail.DataSource = pages;
                rptPagerBBgenerateDetail.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        
        #endregion

        #region BBSearch

        protected void Btnblockbillingsearch_Click(object sender, EventArgs e)
        {

            bool boolstatus = true;
            try
            {
                if (txtHRNID.Text.Trim().Length > 0 && !DoNonCGHHrnValidation())
                { 
                    boolstatus = false;
                }
                if (boolstatus)
                {
                    ShowSpecialInfobillingsearch();
                    BlockBillingSearchRecords();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private string PrepareSearchExpressionSEARCH()
        {
            string stringExpression = null;
            string stringInput = "";
            string stringEncrypyValue = "";
            try
            {
                if (ddlRequestorType.SelectedItem != null && ddlRequestorType.SelectedValue.Length > 0  )
                {
                    stringExpression += ddlRequestorType.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Req.ReqTyp_ID)  LIKE UPPER('%" + ddlRequestorType.SelectedValue.Trim() + "%'))" : "";
                }
              
                if (txtReqID.Text.Length > 0 && txtReqID.Text.Trim() != "%")
                {
                    stringExpression += "And Blk.RptReq_ID='" + txtReqID.Text.Trim().ToUpper() + "'";
                }
                if (ddlStatus.SelectedItem != null && ddlStatus.SelectedValue.Length > 0)
                {
                    stringExpression += ddlStatus.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Blk.Trans_Status)  LIKE UPPER('%" + ddlStatus.SelectedValue.Trim() + "%'))" : "";
                }
                stringInput = txtHRNID.Text.Trim();
                stringEncrypyValue = "";
                if (stringInput.Length > 0)
                {
                    stringEncrypyValue = CommonFunctions.HRNtoEncrypyValue(stringInput);
                }
                if (stringEncrypyValue.Length > 0)
                {
                    stringExpression += "And Reg.patient_id='" + stringEncrypyValue.Trim() + "'";
                } 

                if (txtDateFrmSearch.Text.Trim().Length > 0)
                {
                    DateTime objDateFrom = CommonFunctions.ConvertToDateTime(txtDateFrmSearch.Text.Trim(), "dd-MM-yyyy");
                    stringExpression += "AND CONVERT(date,Blk.trans_date)  >= CONVERT(date,'" + Convert.ToDateTime(objDateFrom).ToString("yyyy-MM-dd") + "') ";
                }
                if (txtDateToSearch.Text.Trim().Length > 0)
                {
                    DateTime objDateTo = CommonFunctions.ConvertToDateTime(txtDateToSearch.Text.Trim(), "dd-MM-yyyy");
                    stringExpression += "AND CONVERT(date,Blk.trans_date)  <= CONVERT(date,'" + Convert.ToDateTime(objDateTo).ToString("yyyy-MM-dd") + "') ";

                }
                if (txtInv.Text.Length > 0 && txtInv.Text.Trim() != "%")
                {
                    stringExpression += "And  Reg.Inv_ID='" + txtInv.Text.Trim().ToUpper() + "'";
                }


                if (txtReqNo.Text.Length > 0 && txtReqNo.Text.Trim() != "%")
                {
                    stringExpression += "And Reg.Request_ID='" + txtReqNo.Text.Trim().ToUpper() + "'";
                }
                if (txtSName.Text.Length > 0 && txtSName.Text.Trim() != "%")
                {
                    stringExpression += "And inref.INDEX_VALUE='" + txtSName.Text.Trim().ToUpper().Replace("'", "''") + "'";
                }

                ViewState["exportconditiondesig"] = stringExpression;
                return stringExpression;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return null;
            }
        }
        private void BlockBillingSearchRecords()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringOrderBy = "";
            DataTable objDataTable = null;
            string stringformid01 = "";
            string stringServiceType = "";
            int intRecordFrom = 0;
            int intRecordTo = int.MaxValue;
            pnlblockbillingsearch.Visible = false;
            try
            {
                if (ValidateControlsbillingsearch() && ValidateBusinessLogicbillingsearch())
                {
                    stringformid01 = "FC3003R1V1";
                    stringServiceType = "List1R1V1";

                    intRecordFrom = intrecFromblockbillingsearch;
                    intRecordTo = intrecToblockbillingsearch;
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid01, PrepareSearchExpressionSEARCH(), stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);
                    PopulatePagerblockbillingsearch(intTotalRecord, intpageIndexblockbillingsearch);
                    lbltotalrecblockbillingsearch.InnerText = intTotalRecord.ToString();
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t1"];
                            objDataTable.DefaultView.Sort = "Inv_ID desc";
                            objDataTable = objDataTable.DefaultView.ToTable();
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            pnlblockbillingsearch.Visible = true;
                            gvgridblockbillingsearch.DataSource = objDataTable;
                            gvgridblockbillingsearch.DataBind();
                            imgbtnprint.Visible = true;
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "No Records Found");
                            gvgridblockbillingsearch.DataSource = null;
                            gvgridblockbillingsearch.DataBind();
                            imgbtnprint.Visible = false;

                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringOrderBy = null;
                objDataTable = null;
                stringformid01 = null;
                stringServiceType = null;
                intRecordFrom = 0;
                intRecordTo = 0;
            }
        }

        private bool ValidateControlsbillingsearch()//fix
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";

                if (!boolStatus)
                {
                    if (stringOverallMsg.Trim().Length > 0)
                    {
                        stringOverallMsg = stringOverallMsg.Trim() + " ";
                        stringOverallMsg = stringOverallMsg.Remove(stringOverallMsg.Length - 1, 1);
                        CommonFunctions.ShowMessageboot(this, stringOverallMsg);
                        return boolStatus;
                    }
                }

                return true;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return false;
            }
            finally
            {
                //boolStatus = true;
                //stringOverallMsg = "";
            }
        }

        private bool ValidateBusinessLogicbillingsearch()//fix
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "";

                if (txtDateFrmSearch.Text.Trim().Length > 0 && !CommonFunctions.ValidDateTime(txtDateFrmSearch.Text.Trim()))
                {
                    stringOverallMsg += "Date From should be a valid date.";
                    boolStatus = false;
                }

                if (txtDateToSearch.Text.Trim().Length > 0 && !CommonFunctions.ValidDateTime(txtDateToSearch.Text.Trim()))
                {
                    stringOverallMsg += "Date To should be a valid date.";
                    boolStatus = false;
                }

                if (boolStatus)
                {
                    DateTime objDateTimeFrom = DateTime.Now;
                    DateTime objDateTimeTo = DateTime.Now;


                    if (txtDateFrmSearch.Text.Trim().Length > 0 && txtDateToSearch.Text.Trim().Length > 0)
                    {
                        if (!CommonFunctions.IsValidDateTime(txtDateFrmSearch.Text.Trim(), "dd/MM/yyyy HH:mm:ss", out objDateTimeFrom))
                        {
                            boolStatus = false;
                            CommonFunctions.ShowMessageboot(this, "\"Date From\" is a Invalid date.");
                            txtDateFrmSearch.Focus();
                        }
                        else if (!CommonFunctions.IsValidDateTime(txtDateToSearch.Text.Trim(), "dd/MM/yyyy HH:mm:ss", out objDateTimeTo))
                        {
                            boolStatus = false;
                            CommonFunctions.ShowMessageboot(this, "\"Date To\" is a Invalid date.");
                            txtDateToSearch.Focus();
                        }
                        else
                        {
                            TimeSpan objTimeSpan = objDateTimeTo.Subtract(objDateTimeFrom);
                            if (objTimeSpan.TotalDays < 0)
                            {
                                boolStatus = false;
                                CommonFunctions.ShowMessageboot(this, "\"Date To\" should be greater than Request \"Date From\".");
                            }
                            else if (objTimeSpan.TotalDays > 90)
                            {
                                boolStatus = false;
                                CommonFunctions.ShowMessageboot(this, "Date range should be less than 90 days");
                            }
                        }

                    }

                }

                if (!boolStatus)
                {
                    if (stringOverallMsg.Trim().Length > 0)
                    {
                        stringOverallMsg = stringOverallMsg.Trim() + " ";
                        stringOverallMsg = stringOverallMsg.Remove(stringOverallMsg.Length - 1, 1);
                        CommonFunctions.ShowMessageboot(this, stringOverallMsg); 
                    }
                    return boolStatus;
                }

                return true;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return false;
            }
            finally
            {
                //boolStatus = true;
                //stringOverallMsg = "";
            }

        }

        private void ShowSpecialInfobillingsearch()//fix
        {
            string stringBoID = "";
            string stringHRNID = "";
            string stringSpecialInfo = "";
            try
            {
                if (Session["BusinessID"] != null)
                {
                    stringBoID = Session["BusinessID"].ToString();
                }
                if (txtHRNID.Text.Trim().Length > 0)
                {
                    stringHRNID = txtHRNID.Text.Trim().ToUpper();
                    stringSpecialInfo = CommonFunctions.GetSpecialInfo(stringBoID, stringHRNID, true);
                    if (stringSpecialInfo != null && stringSpecialInfo.Trim().Length > 0) { CommonFunctions.ShowMessageboot(this, stringSpecialInfo); }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringBoID = null;
                stringHRNID = null;
                stringSpecialInfo = null;
            }
           
        }

        protected void lnkPageblockbillingsearch_Click(object sender, EventArgs e)
        {
            int recFrom1 = 0;
            try
            {
                if (Session["PageIndex1"] != null)
                {
                    intpageIndexblockbillingsearch = Convert.ToInt32(Session["PageIndex1"].ToString());
                    Session["PageIndex"] = intpageIndexblockbillingsearch;
                }
                else
                {
                    intpageIndexblockbillingsearch = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndexblockbillingsearch != 0)
                    {
                        Session["PageIndex"] = intpageIndexblockbillingsearch;
                    }
                }

                if (intpageIndexblockbillingsearch == 1)
                {
                    intrecFromblockbillingsearch = 0;
                    intrecToblockbillingsearch = CommonFunctions.GridViewPagesize(stringformIdPagingblockbillingsearch);
                }
                else
                {
                    recFrom1 = (intpageIndexblockbillingsearch * intrecToblockbillingsearch) - intrecToblockbillingsearch;
                    intrecFromblockbillingsearch = recFrom1 + 1;
                    intrecToblockbillingsearch = recFrom1 + CommonFunctions.GridViewPagesize(stringformIdPagingblockbillingsearch);
                }

                BlockBillingSearchRecords();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                recFrom1 = 0;
            }
        }
        private void PopulatePagerblockbillingsearch(int recordCount, int currentPage)
        {
            ViewState["lastpagepagig"] = true;
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdPagingblockbillingsearch);
                double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(intpaging));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                var ssn = dblPageCount.ToString(CultureInfo.InvariantCulture).Split('.');
                if (ssn[0] == "0")
                {
                    pageCount = (int)Math.Round(dblPageCount);
                }
                if (currentPage == 0)
                {
                    currentPage = 1;
                    startIndex = 1;
                }
                else
                {
                    startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
                }

                endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
                if (currentPage > pagerSpan % 2)
                {
                    if (currentPage == 2)
                    {
                        endIndex = 5;
                    }
                    else
                    {
                        endIndex = currentPage + 2;
                    }
                }
                else
                {
                    endIndex = (pagerSpan - currentPage) + 1;
                }
                if (currentPage != 0)
                {
                    if (endIndex - (pagerSpan - 1) > startIndex)
                    {
                        startIndex = endIndex - (pagerSpan - 1);
                    }
                }
                if (endIndex > pageCount)
                {
                    endIndex = pageCount;
                    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
                }
                //Add the First Page Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("First", "1"));
                }
                //Add the Previous Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("<", (currentPage - 1).ToString()));
                }
                for (int i = startIndex; i <= endIndex; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                //Add the Next Button.
                if (currentPage < pageCount)
                {
                    pages.Add(new ListItem(">", (currentPage + 1).ToString()));
                }
                //Add the Last Button.
                if (currentPage != pageCount)
                {
                    pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                if (pageCount == 1 || pageCount == 0)
                {
                    ViewState["lastpagepagig"] = false;
                    pages.Clear();
                    //pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                rptPagerblockbillingsearch.DataSource = pages;
                rptPagerblockbillingsearch.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }


        #endregion

        #region bbsearch detail
        protected void lnkbtnblockbillingsearchGrid_Click(object sender, EventArgs e)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringOrderBy = "";
            DataTable objDataTable = null;
            string stringBoID = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            pnlblockbillingsearchDetail.Visible = false;
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringValue = "";
            int intRecordFrom = 0;
            int intRecordTo = 0;
            string stringformid01 = "";
            string stringServiceType = "";
            string stringexp0121 = "";
            try
            {
                if (sender != null)
                {
                    stringCmdArgument = ((LinkButton)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length > 0)
                        {
                            stringValue = stringValues[0];
                            intRecordFrom = intrecFromblockbillingsearchDetail;
                            intRecordTo = intrecToblockbillingsearchDetail;
                            stringformid01 = "FC3002R1V1";
                            stringServiceType = "List3R1V1";
                            stringexp0121 = "And BlkBilling.be_id= '" + stringBoID.ToString() + "' And BlkBilling.Inv_ID= '" + stringValue + "'";

                            objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid01, stringexp0121, stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);
                            PopulatePagerblockbillingsearchDetail(intTotalRecord, intintpageIndexblockbillingsearchDetail);
                            intTotalRecordrecblockbillingsearchDetail.InnerText = intTotalRecord.ToString();
                            if (interrorcount == 0)
                            {
                                if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t3"] != null && objDatasetResult.Tables["t3"].Rows.Count > 0)
                                {
                                    objDataTable = objDatasetResult.Tables["t3"];
                                }
                                if (objDataTable != null && objDataTable.Rows.Count > 0)
                                {
                                    pnlblockbillingsearchDetail.Visible = true;
                                    gvgridblockbillingDetail.DataSource = objDataTable;
                                    gvgridblockbillingDetail.DataBind();
                                }
                                else
                                {
                                    gvgridblockbillingDetail.DataSource = null;
                                    gvgridblockbillingDetail.DataBind();

                                }
                            }
                            else
                            {
                                Errorpopup(stringOutputResult);
                            }
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
               objDatasetResult = null;
               interrorcount = 0;
               intTotalRecord = 0;
               stringOutputResult = null;
               stringOrderBy = null;
               objDataTable = null;
               stringBoID = null;
            }
        }

        protected void lnkPageblockbillingsearchDetail_Click(object sender, EventArgs e)
        {
            int recFrom1 = 0;
            try
            {
                if (Session["PageIndex1"] != null)
                {
                    intintpageIndexblockbillingsearchDetail = Convert.ToInt32(Session["PageIndex1"].ToString());
                    Session["PageIndex"] = intintpageIndexblockbillingsearchDetail;
                }
                else
                {
                    intintpageIndexblockbillingsearchDetail = int.Parse((sender as LinkButton).CommandArgument);
                    if (intintpageIndexblockbillingsearchDetail != 0)
                    {
                        Session["PageIndex"] = intintpageIndexblockbillingsearchDetail;
                    }
                }

                if (intintpageIndexblockbillingsearchDetail == 1)
                {
                    intrecFromblockbillingsearchDetail = 0;
                    intrecToblockbillingsearchDetail = CommonFunctions.GridViewPagesize(stringformIdPagingblockbillingsearchDetail);
                }
                else
                {
                    recFrom1 = (intintpageIndexblockbillingsearchDetail * intrecToblockbillingsearchDetail) - intrecToblockbillingsearchDetail;
                    intrecFromblockbillingsearchDetail = recFrom1 + 1;
                    intrecToblockbillingsearchDetail = recFrom1 + CommonFunctions.GridViewPagesize(stringformIdPagingblockbillingsearchDetail);
                }

                lnkbtnblockbillingsearchGrid_Click(null,null);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                recFrom1 = 0;
            }
        }
        private void PopulatePagerblockbillingsearchDetail(int recordCount, int currentPage)
        {
            ViewState["lastpagepagig"] = true;
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdPagingblockbillingsearchDetail);
                double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(intpaging));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                var ssn = dblPageCount.ToString(CultureInfo.InvariantCulture).Split('.');
                if (ssn[0] == "0")
                {
                    pageCount = (int)Math.Round(dblPageCount);
                }
                if (currentPage == 0)
                {
                    currentPage = 1;
                    startIndex = 1;
                }
                else
                {
                    startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
                }

                endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
                if (currentPage > pagerSpan % 2)
                {
                    if (currentPage == 2)
                    {
                        endIndex = 5;
                    }
                    else
                    {
                        endIndex = currentPage + 2;
                    }
                }
                else
                {
                    endIndex = (pagerSpan - currentPage) + 1;
                }
                if (currentPage != 0)
                {
                    if (endIndex - (pagerSpan - 1) > startIndex)
                    {
                        startIndex = endIndex - (pagerSpan - 1);
                    }
                }
                if (endIndex > pageCount)
                {
                    endIndex = pageCount;
                    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
                }
                //Add the First Page Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("First", "1"));
                }
                //Add the Previous Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("<", (currentPage - 1).ToString()));
                }
                for (int i = startIndex; i <= endIndex; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                //Add the Next Button.
                if (currentPage < pageCount)
                {
                    pages.Add(new ListItem(">", (currentPage + 1).ToString()));
                }
                //Add the Last Button.
                if (currentPage != pageCount)
                {
                    pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                if (pageCount == 1 || pageCount == 0)
                {
                    ViewState["lastpagepagig"] = false;
                    pages.Clear();
                    //pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                rptPagerblockbillingsearchDetail.DataSource = pages;
                rptPagerblockbillingsearchDetail.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }


        protected void lnkbtnRequest_IDgvlist_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringRequestID = "";
            try
            {
                if (sender != null)
                {
                    stringCmdArgument = ((LinkButton)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length > 0)
                        {
                            stringRequestID = stringValues[0];
                            Session["REQUEST_FromSummary"] = stringRequestID.Trim();
                            Response.Redirect("FC0001R1V1.aspx?TO=Y");
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringCmdArgument = null;
                stringValues = null;
                stringRequestID = null;
            }
        }

        #endregion
        #region validate HRN

        public bool DoNonCGHHrnValidation()
        {
            bool boolMROHRN = false;
            bool boolStatus = true;
            string stringInput = "";
            try
            {
                boolMROHRN = false;
                boolStatus = true;

                if (txtHRNID.Text.Trim().Length > 0)
                {
                    stringInput = txtHRNID.Text.Trim().ToUpper();
                    if (!stringInput.StartsWith("X") && !stringInput.StartsWith("Y") && !stringInput.StartsWith("MRO"))
                    {
                        stringInput = stringInput.Replace(" ", "");
                        if (stringInput.Trim().Length != 9)
                        {
                            boolStatus = false;
                        }
                    }
                    else if (stringInput.StartsWith("X") || stringInput.StartsWith("Y"))
                    {
                        stringInput = stringInput.Replace(" ", "");
                        if (stringInput.Trim().Length > 0 && stringInput.Trim().Length != 12)
                        {
                            boolStatus = false;
                        }
                        else
                        {
                            if (stringInput != null && stringInput.Length > 2)
                            {
                                if (char.IsLetter(stringInput[0]) && char.IsLetter(stringInput[1]))
                                {
                                    txtHRNID.Text = stringInput[0] + stringInput.Substring(2, stringInput.Length - 2) + stringInput[1];
                                }
                                else if (stringInput.Length == 12 && char.IsLetter(stringInput[0]) && char.IsLetter(stringInput[11]))
                                { }
                                else { boolStatus = false; }
                            }

                        }
                    }
                    else if (stringInput.StartsWith("MRO"))
                    {
                        stringInput = stringInput.Replace(" ", "");
                        if (stringInput.Trim().Length > 0 && stringInput.Trim().Length != 9)
                        {
                            boolStatus = false;
                        }
                        else
                        {
                            if (stringInput.Trim().Length == 9)
                            {
                                stringInput = stringInput.Trim();
                                long longTemp = 0;
                                if (!long.TryParse(stringInput.Remove(0, 3), out longTemp))
                                {
                                    CommonFunctions.ShowMessageboot(this, "Invalid MRN");
                                    return false;
                                }
                                else
                                {
                                    boolMROHRN = true;
                                }
                            }

                        }
                    }

                    if (boolStatus)
                    {
                        if (!boolMROHRN)
                        {
                            string stringResult = CommonFunctions.ValidateHRN(txtHRNID.Text.Trim().ToUpper(), out string stringFormmatHrnID);
                            if (stringResult != "SUCCESS" && stringResult != "")
                            {
                                CommonFunctions.ShowMessageboot(this, "Invalid MRN");
                                return false;
                            }
                            else if (stringResult == "SUCCESS")
                            {
                                txtHRNID.Text = ArrangeHRNNumber(stringFormmatHrnID);
                                return true;
                            }
                            else
                            {
                                txtHRNID.Text = ArrangeHRNNumber(stringResult);
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }


                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "Invalid MRN");
                        txtHRNID.Focus();
                        // SelectText(txtHRNID);
                        return false;
                    }
                }


                return false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                //boolMROHRN = false;
                //boolStatus = true;
                //stringInput = "";
            }
            return false;
        }


        private string ArrangeHRNNumber(string stringHRN)//fix
        {
            try
            {
                if (stringHRN.Trim().Length > 0)
                {
                    if (stringHRN.ToUpper().StartsWith("X") || stringHRN.ToUpper().StartsWith("Y"))
                    {
                        if (stringHRN.ToUpper().Length == 10 || stringHRN.ToUpper().Length == 12)
                        {
                            string stringSub1 = stringHRN.Substring(0, 2);
                            string stringSub2 = stringHRN.Substring(2, stringHRN.Length - 2);
                            string stringResult = stringSub1.Trim()[0].ToString() + stringSub2.Trim() + stringSub1.Trim()[1];
                            return stringResult;
                        } 
                    }
                    else if (stringHRN.Trim().Length == 10)
                    {
                        string stringSub1 = stringHRN.Substring(0, 3);
                        string stringSub2 = stringHRN.Substring(3, stringHRN.Length - 3);
                        string stringResult = stringSub1.Trim()[0].ToString() + stringSub2.Trim() + stringSub1.Trim()[1];
                        return stringResult;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }

            return stringHRN;
        }
        #endregion



        protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ClearValues();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                 
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        
         
        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgbtnAudit_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

        } 

        protected void LnkbtnSort_Click(object sender, EventArgs e)
        {
            

        } 


        private void Errorpopup(string[] stringOutputResult)
        {
            try
            {
                lblModalTile5.Text = "Error Message Summary";
                lblErrorType.Text = stringOutputResult[1];
                lblErrorCode.Text = stringOutputResult[0];
               // 
                txterrormsg.Text = stringOutputResult[2];
                Modelpopuperror.Show();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void imgbtnSearch_Click1(object sender, ImageClickEventArgs e)
        {

        }

        #region popup

        //txtboxclear Dynamic
        protected void imgbtnCleardropdowntxtboxvalue_Click(object sender, ImageClickEventArgs e)
        {
            string buttonId = "";
            string ToolTip = "";
            string[] stringValues = null;
            string stringToolTip = "";
            string ID = "";
            string Name = "";
            try
            {
                if (sender != null)
                {
                    ImageButton ButtonID = (ImageButton)sender;
                    if (ButtonID != null)
                    {
                        buttonId = ButtonID.ID;
                        ToolTip = ButtonID.AlternateText;
                        if (ToolTip.Length > 0)
                        {
                            stringValues = ToolTip.Split('_');
                            if (stringValues != null && stringValues.Length == 3)
                            {
                                stringToolTip = stringValues[0];
                                ID = stringValues[1];
                                Name = stringValues[2];
                                Control foundControlupdpnl = upnl11.ContentTemplateContainer.FindControl(ID);
                                Control foundControlNAme = upnl11.ContentTemplateContainer.FindControl(Name);
                                if (foundControlupdpnl != null && foundControlNAme != null)
                                {
                                    if (foundControlupdpnl is TextBox)
                                    {
                                        if (foundControlNAme is TextBox)
                                        {
                                            TextBox textBoxID = (TextBox)foundControlupdpnl;
                                            TextBox textBoxName = (TextBox)foundControlNAme;
                                            textBoxID.Text = textBoxName.Text = string.Empty;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            } 
        }

        protected void lnkbtnddlpopupID_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringId = "";
            string stringNAME = "";
            string txtID = "";
            string txtnameID = "";
            string updatepnlID = "";
            try
            {
                if (sender != null)
                {
                    stringCmdArgument = ((LinkButton)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length >= 2)
                        {
                            stringId = stringValues[0];
                            stringNAME = stringValues[1];
                            if (hdnPopupDropdownValue != null && hdnPopupDropdownValue.Value.Length > 0)//&& hdnPopupDropdownValue.Value == "DOCTORINSTITUTION"
                            {
                                if (hdnPopuptxtboxValue != null && hdnPopuptxtboxValue.Value.Length > 0)
                                {
                                    if (hdnPopupupdatepnlValue != null && hdnPopupupdatepnlValue.Value.Length > 0)
                                    {
                                        if (hdnPopuptxtboxnameValue != null && hdnPopuptxtboxnameValue.Value.Length > 0)
                                        {
                                            txtID = hdnPopuptxtboxValue.Value.ToString();
                                            txtnameID = hdnPopuptxtboxnameValue.Value.ToString();
                                            updatepnlID = hdnPopupupdatepnlValue.Value.ToString();
                                            Control foundControltxtID = upnl11.ContentTemplateContainer.FindControl(txtID);
                                            Control foundControltxtNameID = upnl11.ContentTemplateContainer.FindControl(txtnameID);
                                            Control foundControlupdatepnlID = upnl11.ContentTemplateContainer.FindControl(updatepnlID);
                                            if (foundControltxtID != null && foundControltxtNameID != null)
                                            {
                                                if (foundControltxtID is TextBox)
                                                {
                                                    if (foundControltxtNameID is TextBox)
                                                    {
                                                        TextBox textBoxID = (TextBox)foundControltxtID;
                                                        TextBox textBoxName = (TextBox)foundControltxtNameID;
                                                        textBoxID.Text = stringId;
                                                        textBoxName.Text = stringNAME;
                                                    }
                                                }
                                            }
                                            if (foundControlupdatepnlID != null)
                                            {
                                                if (foundControlupdatepnlID is UpdatePanel)
                                                {
                                                    UpdatePanel UpdatePanel = (UpdatePanel)foundControlupdatepnlID;
                                                    UpdatePanel.Update();
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            mdlpnlddlpopup.Hide();
                            Panel3.Visible = false;

                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringCmdArgument = null;
                stringValues = null; 
            }
        }
        protected void btnclosOrganisationpopup_Click(object sender, EventArgs e)
        {
            mdlpnlddlpopup.Hide();
            Panel3.Visible = false;

        }

        private void PopulatePagerdropdownpopup(int recordCount, int currentPage, int recto)
        {
            ViewState["lastpagepagig"] = true;
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdddlpopup);
                double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(intpaging));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                var ssn = dblPageCount.ToString(CultureInfo.InvariantCulture).Split('.');
                if (ssn[0] == "0")
                {
                    pageCount = (int)Math.Round(dblPageCount);
                }
                if (currentPage == 0)
                {
                    currentPage = 1;
                    startIndex = 1;
                }
                else
                {
                    startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
                }

                endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
                if (currentPage > pagerSpan % 2)
                {
                    if (currentPage == 2)
                    {
                        endIndex = 5;
                    }
                    else
                    {
                        endIndex = currentPage + 2;
                    }
                }
                else
                {
                    endIndex = (pagerSpan - currentPage) + 1;
                }

                if (currentPage != 0)
                {
                    if (endIndex - (pagerSpan - 1) > startIndex)
                    {
                        startIndex = endIndex - (pagerSpan - 1);
                    }
                }
                if (endIndex > pageCount)
                {
                    endIndex = pageCount;
                    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
                }
                //Add the First Page Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("First", "1"));
                }
                //Add the Previous Button.
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
                }
                for (int i = startIndex; i <= endIndex; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                //Add the Next Button.
                if (currentPage < pageCount)
                {
                    pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
                }
                //Add the Last Button.
                if (currentPage != pageCount)
                {
                    pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                if (pageCount == 1 || pageCount == 0)
                {
                    ViewState["lastpagepagig"] = false;
                    pages.Clear();
                    //pages.Add(new ListItem("Last", pageCount.ToString()));
                }
                Repeater8.DataSource = pages;
                Repeater8.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void imgbtntrigerPopup_Click(object sender, ImageClickEventArgs e)
        {
            string buttonId = "";
            string ToolTip = "";
            string[] stringValues = null;
            string ddlID = "";
            string txtID = "";
            string txtNAmeID = "";
            string updatepnlID = "";
            try
            {
                if (sender != null)
                {
                    ImageButton ButtonID = (ImageButton)sender;
                    if (ButtonID != null)
                    {
                        buttonId = ButtonID.ID;
                        ToolTip = ButtonID.AlternateText;
                        if (ToolTip.Length > 0)
                        {
                            stringValues = ToolTip.Split('_');
                            if (stringValues != null && stringValues.Length == 4)
                            {
                                ddlID = stringValues[0];
                                txtID = stringValues[1];
                                txtNAmeID = stringValues[2];
                                updatepnlID = stringValues[3];
                                if (ddlID.Length > 0)
                                {
                                    if (ddlID == "REQUESTOR")
                                    {
                                        lblpopupname.Text = "Requestor";
                                    }
                                    else if (ddlID == "REQ")
                                    {
                                        lblpopupname.Text = "Requestor";
                                    }
                                    else if (ddlID == "DOCTOR")
                                    {
                                        lblpopupname.Text = "Doctor Selection";
                                    }
                                    else if (ddlID == "REQUESTORTYPE")
                                    {
                                        lblpopupname.Text = "Report Type Selection";
                                    }
                                    //
                                    else if (ddlID == "HOSTINS")
                                    {
                                        lblpopupname.Text = "Doctor Host Institution";
                                    }
                                    else if (ddlID == "COSTCENTER")
                                    {
                                        lblpopupname.Text = "Doctor Host Institution Cost Centre";
                                    }

                                    hdnPopupDropdownValue.Value = ddlID;
                                    hdnPopuptxtboxValue.Value = txtID;
                                    hdnPopuptxtboxnameValue.Value = txtNAmeID;
                                    hdnPopupupdatepnlValue.Value = updatepnlID;
                                    txtddlpopupvalue.Text = "";
                                    LoadProduct("");
                                }
                            }


                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                buttonId = null;
                ToolTip = null;
                stringValues = null;
                ddlID = null;
                txtID = null;
                txtNAmeID = null;
                updatepnlID = null;
            }
        }

        private void LoadProduct(string stringexp01, int? RecordFrom = null, int? RecordTo = null)
        {
            int interrorcount = 0;
            int intrecordcount = 0;
            DataSet objDataSet = null;
            string[] stringOutputResult = null;
            string stringOrderBy = "";
            string stringformid = "";
            string stringServiceType = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            stringServiceType = "List1R1V1";
            int intRecordFrom = 0;
            int intRecordTo = 0;
            try
            {

                intRecordFrom = recFromdropdownpopup;
                intRecordTo = recTodropdownpopup;
                stringOutputResult = new string[3];
                if (hdnPopupDropdownValue != null && hdnPopupDropdownValue.Value.Length > 0)
                {
                    if (hdnPopupDropdownValue.Value == "REQUESTOR")
                    {
                        stringformid = "FA0027R1V1";
                        stringOrderBy = "mrreq.short_name asc";

                        stringexp01 += "And mrreq.be_id= '" + stringbeid + "'  AND mrreq.delmark='N' ";
                       
                    }
                    else if (hdnPopupDropdownValue.Value == "REQ")
                    {
                        stringformid = "FA0027R1V1";
                        stringOrderBy = "mrreq.short_name asc";

                        stringexp01 += "And mrreq.be_id= '" + stringbeid + "'  AND mrreq.delmark='N' ";

                    }
                    else if (hdnPopupDropdownValue.Value == "DOCTOR")
                    {
                        stringformid = "FA0011R1V1";
                        stringOrderBy = "mrd.short_name asc"; 
                        stringexp01 += " And mrd.delmark= 'N'";
                    }
                    else if (hdnPopupDropdownValue.Value == "REQUESTOR")
                    {
                        stringformid = "FA0027R1V1";
                        stringOrderBy = "mrreq.order_id asc";
                        stringexp01 += "And mrreq.be_id= '" + stringbeid + "' And mrreq.delmark='N' ";
                    }
                }
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp01, stringOrderBy, intRecordFrom, intRecordTo, out intrecordcount, out interrorcount, out stringOutputResult);

                PopulatePagerdropdownpopup(intrecordcount, pageIndexdropdownpopup, recTodropdownpopup);

                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                    {
                        gvlistPopUppurpose.DataSource = objDataSet.Tables[0];
                        gvlistPopUppurpose.DataBind();
                        mdlpnlddlpopup.Show();
                        Panel3.Visible = true;
                    }
                    else
                    {
                        gvlistPopUppurpose.DataSource = objDataSet.Tables[0];
                        gvlistPopUppurpose.DataBind();
                        CommonFunctions.ShowMessageboot(this, "No Records Found");
                        mdlpnlddlpopup.Show();
                        Panel3.Visible = true;
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                interrorcount = 0;
                intrecordcount = 0;
                objDataSet = null;
                stringOutputResult = null;
                stringOrderBy = null;
                stringformid = null;
                stringServiceType = null;
                stringbeid = null;
                intRecordFrom = 0;
                intRecordTo = 0;
            }
        }

        protected void btnfindddlpopupRecord_Click(object sender, EventArgs e)
        {
            DropDownSearchCndition();
        }

        private void DropDownSearchCndition()
        {
            string stringExpression = "";
            string stringColumn1 = "";
            string stringColumn2 = "";
            try
            {
                if (hdnPopupDropdownValue != null && hdnPopupDropdownValue.Value.Length > 0)
                {
                    if (hdnPopupDropdownValue.Value == "REQUESTOR")
                    {
                        stringColumn1 = "mrreq.rptreq_id";
                        stringColumn2 = "mrreq.short_name";
                    }
                    else if (hdnPopupDropdownValue.Value == "REQ")
                    {
                        stringColumn1 = "mrreq.rptreq_id";
                        stringColumn2 = "mrreq.short_name";
                    }
                    else if (hdnPopupDropdownValue.Value == "DIVSIONCODE")
                    {
                        stringColumn1 = "lst.lst_id";
                        stringColumn2 = "lst.short_name";
                    }
                    else if (hdnPopupDropdownValue.Value == "DOCTOR")
                    {
                        stringColumn1 = "mrd.EMP_NO";
                        stringColumn2 = "mrd.DESIGNATION_DESC";
                    }
                    else if (hdnPopupDropdownValue.Value == "REQUESTOR")
                    {
                        stringColumn1 = "mrreq.rptreq_id";
                        stringColumn2 = "mrreq.short_name";
                    }

                    if (stringColumn1.Length > 0 && stringColumn2.Length > 0)
                    {
                        stringExpression = "AND ( " + stringColumn1 + " Like '%" + txtddlpopupvalue.Text.Trim().ToUpper() + "%' or " + stringColumn2 + " Like '%" + txtddlpopupvalue.Text.Trim().ToUpper() + "%'" + " )";
                    }

                    LoadProduct(stringExpression);

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            } 
        }

        protected void gvlistPopUppurpose_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringID = "";
            string stringdesc = "";
            string stringSort = string.Empty;
            DataRowView objDRV = null;
            DataRow objDataRow = null;
            try
            {
                GridViewRow objGridViewRow = e.Row;
                stringSort = string.Empty;
                if (objGridViewRow.DataItem == null) { return; }

                objDRV = ((DataRowView)objGridViewRow.DataItem);
                objDataRow = objDRV.Row;
                if (objDataRow != null)
                {
                    if (hdnPopupDropdownValue != null && hdnPopupDropdownValue.Value.Length > 0)
                    {
                        if (hdnPopupDropdownValue.Value == "REQUESTOR")
                        {
                            stringID = objDataRow["rptreq_id"].ToString();
                            stringdesc = objDataRow["short_name"].ToString();

                        }
                        else if (hdnPopupDropdownValue.Value == "REQ")
                        {
                            stringID = objDataRow["rptreq_id"].ToString();
                            stringdesc = objDataRow["short_name"].ToString();
                        }
                        else if (hdnPopupDropdownValue.Value == "DIVSIONCODE")
                        {
                            stringID = objDataRow["lst_id"].ToString();
                            stringdesc = objDataRow["SHORT_NAME"].ToString();
                        }
                        else if (hdnPopupDropdownValue.Value == "DOCTOR")
                        {
                            stringID = objDataRow["EMP_NO"].ToString();
                            stringdesc = objDataRow["DESIGNATION_DESC"].ToString();
                        }
                        else if (hdnPopupDropdownValue.Value == "REQUESTOR")
                        {
                            stringID = objDataRow["rptreq_id"].ToString();
                            stringdesc = objDataRow["short_name"].ToString();
                        }
                        if (stringID != null && stringID.Trim().Length > 0)
                        { ((LinkButton)e.Row.FindControl("lnkbtnddlpopupID")).Text = stringID; }

                        if (stringdesc != null && stringdesc.Trim().Length > 0)
                        { ((Label)e.Row.FindControl("lnkbtnddlpopupdesc")).Text = stringdesc; }


                        LinkButton objButtonName = (LinkButton)e.Row.FindControl("lnkbtnddlpopupID");
                        objButtonName.CommandArgument = stringID + "," + stringdesc;

                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringID = null;
                stringdesc = null;
                stringSort = null;
                objDRV = null;
                objDataRow = null;
            }
        }

        protected void lnkPagedropdownpopup_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PageIndex1"] != null)
                {
                    pageIndexdropdownpopup = Convert.ToInt32(Session["PageIndex1"].ToString());
                    Session["PageIndex"] = pageIndexdropdownpopup;
                }
                else
                {
                    pageIndexdropdownpopup = int.Parse((sender as LinkButton).CommandArgument);
                    if (pageIndexdropdownpopup != 0)
                    {
                        Session["PageIndex"] = pageIndexdropdownpopup;
                    }
                }

                if (pageIndexdropdownpopup == 1)
                {
                    recFromdropdownpopup = 0;
                }
                else
                {
                    int recFromProcessHistory1 = (pageIndexdropdownpopup * recTodropdownpopup) - recTodropdownpopup;
                    recFromdropdownpopup = recFromProcessHistory1 + 1;
                    recTodropdownpopup = recFromProcessHistory1 + CommonFunctions.GridViewPagesize(stringformIdddlpopup);

                }
                hdnClickEvent.Value = "true";

                if (hdnClickEvent.Value == "true")
                {
                    DropDownSearchCndition();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        } 

        #endregion
        private void LoadReports(string stringFORMID, string stringreportname)
        {

            byte[] objbytereturn = null;
            string stringFile = string.Empty;
            string stringoutputresult = "";
            int intErrorCount = 0;
            string[] stringOutputResult = null;
            string[] stringInputs = new string[7];
            string[] stringSettings = new string[21];
            string stringbeid = "";
            string stringaddress = "";
            string stringTelephoneNo = "";

            string stringfromdate = "";
            string stringtodate = "";
            DateTime objDateTimeFrom;
            stringbeid = CommonFunctions.GETBussinessEntity();
            stringaddress = ConfigurationManager.AppSettings["BEIDAddress"];
            stringTelephoneNo = ConfigurationManager.AppSettings["BEIDTelephoneNo"];
            string stringUserDisplayName = "";
            string stringPatientAddress = "";
            string stringRQQPatientAddress = "";
            string stringmailaddPatientAddress = "";
            string stringADDRESS = "";
            string stringRequetstorothers = "";
            string stringpatientname = "";
            string stringRequestid = "";
            string stringRequestorname = "";
            DataSet objDatasetAppsVariables = null;
            string stringLofoFlag = "";
            try
            {
                if (Session["G11EOSUser_Name"] != null)
                {
                    stringUserDisplayName = Session["G11EOSUser_Name"].ToString();
                }
                if (ViewState["PatientName"] != null)
                {
                    stringpatientname = ViewState["PatientName"].ToString();
                }
                if (Session["PAT_ADDRESS"] != null)
                { stringPatientAddress = Session["PAT_ADDRESS"].ToString(); }

                if (Session["MailAdd_ADDRESS"] != null)
                { stringmailaddPatientAddress = Session["MailAdd_ADDRESS"].ToString(); }
                if (Session["ReqAdd_ADDRESS"] != null)
                {
                    stringRQQPatientAddress = Session["ReqAdd_ADDRESS"].ToString();

                }
                if (txtDateFrmSearch.Text.Trim().Length > 0 && CommonFunctions.ValidDateTime(txtDateFrmSearch.Text.Trim()))
                {
                    objDateTimeFrom = CommonFunctions.ConvertToDateTime(txtDateFrmSearch.Text.Trim(), "dd-MM-yyyy");
                    stringfromdate = CommonFunctions.ConvertDateTimetoString(objDateTimeFrom);
                }

                if (txtDateToSearch.Text.Trim().Length > 0 && CommonFunctions.ValidDateTime(txtDateToSearch.Text.Trim()))
                {
                    objDateTimeFrom = CommonFunctions.ConvertToDateTime(txtDateToSearch.Text.Trim(), "dd-MM-yyyy");
                    stringtodate = CommonFunctions.ConvertDateTimetoString(objDateTimeFrom);
                }


                stringInputs[0] = stringbeid;
                stringInputs[1] = stringfromdate;
                stringInputs[2] = stringtodate;
                stringInputs[3] = txtInv.Text.Trim().ToString();
                stringInputs[4] = txtReqID.Text.Trim().ToString();
                stringInputs[5] = "";
                stringInputs[6] = "";

                stringSettings[0] = stringbeid;
                stringSettings[1] = stringaddress;
                stringSettings[2] = "";
                stringSettings[3] = "";
                stringSettings[4] = "";
                stringSettings[5] = "";
                stringSettings[6] = ConfigurationManager.AppSettings["copyright"].ToString();
                stringSettings[7] = stringFORMID;
                stringSettings[8] = "PORTALBLEDOCFORMAT";
                stringSettings[9] = stringreportname;
                stringSettings[10] = "";
                stringSettings[11] = "";
                stringSettings[12] = stringTelephoneNo;
                stringSettings[13] = "";
                stringSettings[14] = stringreportname;

                stringSettings[15] = "param_from_date" + "-->" + stringfromdate;
                stringSettings[16] = "param_to_date" + "-->" + stringtodate;
                stringSettings[17] = "LoginUserID" + "-->" + stringUserDisplayName.Trim();

                stringSettings[18] = "pat_address" + "-->" + stringADDRESS.Trim();
                stringLofoFlag = LoadINSTLogo();
                stringSettings[19] = "Print_flag" + "-->" + stringLofoFlag.ToString();
                if (stringRequestid != null && stringRequestid == "SELF")
                {
                    stringSettings[20] = "PatientName" + "-->" + stringpatientname.ToString();
                }
                else if (stringRequestid != null && stringRequestid == "OTHERS")
                {
                    stringSettings[20] = "PatientName" + "-->" + stringRequetstorothers.ToString();
                }
                else if (stringRequestorname != null && stringRequestorname.Length > 0)
                {
                    stringSettings[20] = "PatientName" + "-->" + stringRequestorname.ToString();
                }
                else
                {
                    stringSettings[20] = "PatientName" + "-->" + stringpatientname.ToString();
                }
                objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = stringFORMID;

                clsCertificateValidation.EnableTrustedHosts();
                using (GSReportingService.ReportingServiceClient objReportingServiceClient = new GSReportingService.ReportingServiceClient(CommonFunctions.CreateServiceBasicHttpBinding(), CommonFunctions.objEndpointAddressReportingService))
                {
                    stringoutputresult = objReportingServiceClient.GetDocumentReport(stringInputs, objDatasetAppsVariables, stringSettings, out objbytereturn, out intErrorCount, out stringOutputResult);
                    if (objReportingServiceClient != null)
                        objReportingServiceClient.Close();
                }
                if (intErrorCount == 0)
                {
                    if (stringoutputresult != null && stringoutputresult.Length > 0)
                    {
                        CommonFunctions.OpenExportedFileR1V1(this, objbytereturn, stringreportname.ToString(), "REPORT");
                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "Report Not Found");
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                } 
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringFile = null;
                stringoutputresult = null;
                intErrorCount = 0;
                stringOutputResult = null;
                stringbeid = null;
                stringaddress = null;
                stringTelephoneNo = null;
                stringUserDisplayName = null;
                stringPatientAddress = null;
                stringRQQPatientAddress = null;
                stringmailaddPatientAddress = null;
                stringADDRESS = null;
                stringRequetstorothers = null;
                stringpatientname = null;
                stringRequestid = null;
                stringRequestorname = null;
                objDatasetAppsVariables = null;
                stringLofoFlag = null;

            }
        }

        private string LoadINSTLogo()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0034R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            objDatasetResult = new DataSet();
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringexp = "";
            string stringServiceType = "";
            try
            {

                stringexp = "And INST.be_id= '" + stringbeid + "' And INST.INS_ID= '" + stringbeid + "' And INST.delmark= 'N'";
                stringServiceType = "List1R1V1";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        return objDatasetResult.Tables["t1"].Rows[0]["DOC_GEN_LOGO_FLAG"].ToString();

                    }

                }
                else
                {
                    Errorpopup(stringOutputResult);
                }
                return "N";
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return "N";
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringbeid = null;
                stringexp = null;
                stringServiceType = null;
            }

        }
        protected void imgbtnprint_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LoadReports("DOP700095R1V1", "Block Billing Invoice");
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void gvgridblockbillingsearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringSort = string.Empty;
            DataRowView objDRV = null;
            DataRow objDataRow = null;
            bool boolisReadOnly = true;
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null) { return; }

                objDRV = ((DataRowView)objGridViewRow.DataItem);
                objDataRow = objDRV.Row;

                LinkButton objbtnedit = e.Row.FindControl("lnkbtnRequest_IDgvlist") as LinkButton;
                Label objlabe = e.Row.FindControl("lblblockillingsearch") as Label;
                if (Session["ssnUserRole"] != null)
                {
                    DataTable objuserRole = (DataTable)Session["UserRolestable"];

                    if (objuserRole != null && objuserRole.Rows.Count > 0)
                    {
                        if (objuserRole.Select("Group_ID= 'FINANCE'").Length > 0)
                        {
                            objlabe.Visible = true;
                        }
                        else
                        {
                            objbtnedit.Visible = true;
                        }

                    }
                    else
                    {
                        objbtnedit.Visible = true;
                    }

                } 
                if (objDataRow != null)
                {
                    LinkButton lnkbtnReceiptNo = (LinkButton)e.Row.FindControl("lnkbtnRequest_IDgvlist");

                    if (lnkbtnReceiptNo != null)
                    {
                        lnkbtnReceiptNo.Enabled = false;
                        if (ViewState["boolaccess"] != null)
                        {
                            boolisReadOnly = (bool)ViewState["boolaccess"];
                            lnkbtnReceiptNo.Enabled = boolisReadOnly;
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringSort = null;
                objDRV = null;
                objDataRow = null;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Btnblockbillingsearch_Click(null, null);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearValues();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void gvGridBBgenerateDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool boolisReadOnly = true;
            DataRow objDataRow = null;
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null) { return; }

                objDataRow = ((DataRowView)e.Row.DataItem).Row;
                if (objDataRow != null)
                {
                    LinkButton lnkbtnReceiptNo = (LinkButton)e.Row.FindControl("lnkbtnRequest_IDBBgenerateDetail");

                    if (lnkbtnReceiptNo != null)
                    {
                        lnkbtnReceiptNo.Enabled = false;
                        if (ViewState["boolaccess"] != null)
                        {
                            boolisReadOnly = (bool)ViewState["boolaccess"];
                            lnkbtnReceiptNo.Enabled = boolisReadOnly;
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDataRow = null;
            }
        }


        private void VerifyAccessRights()
        {
            DataSet objDatasetResult = null;

            string stringstatus = "";
            string[] stringOutputResult = null;
            string[] stringComponent = null;
            DataRow objDataRow = null;
            ViewState["boolaccess"] = false;
            try
            {
                stringComponent = new string[1];
                stringComponent[0] = "FB0001R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        if (objDataRow["Export"].ToString().ToUpper() == "ENABLED")
                        {
                            imgbtnprint.Enabled = true;
                        }
                        else
                        {
                            imgbtnprint.Enabled = false;
                        }
                    }
                    else
                    {
                        Response.Redirect("PageAccessDenied.aspx", true);
                    }
                }

                stringComponent = new string[1];
                stringComponent[0] = "FB0001R1V2";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        litab2.Visible = true;
                    }
                    else
                    {
                        litab2.Visible = false;
                    }

                }

                stringComponent = new string[1];
                stringComponent[0] = "FC0001R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        ViewState["boolaccess"] = true;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDatasetResult = null;
                stringstatus = null;
                stringOutputResult = null;
                stringComponent = null;
            }
        }
    }
}