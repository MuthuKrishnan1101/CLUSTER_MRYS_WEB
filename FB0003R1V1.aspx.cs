using CLUSTER_MRTS.CommonFunction;
using G10CertificateValidation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FB0003R1V1 : System.Web.UI.Page
    {
        public DataSet objDatasetAppsVariables;
        public int intpageIndex = 0;
        public int intrecFrom = 0;
        public int intrecTo = 0;
        public string stringformIdPaging = "FC0005R1V1gridviewpagesize";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (CommonFunctions.IsActive())
            {
                try
                {
                    
                    intrecTo = CommonFunctions.GridViewPagesize(stringformIdPaging);

                    if (!IsPostBack)
                    {
                        VerifyAccessRights();
                        CommonFunctions.HeaderName(this, "FB0003R1V1");
                        ViewState["vsSortDirection"] = " ASC";
                        ViewState["vsSortExpression"] = "";
                        ViewState["stringSessionID"] = null;
                        ClearValues();
                        LoadRecord("AND tccc.TRANS_STATUS='OPEN'");
                        LoadGetSupervisorData();
                    }
                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }

            }
        }

        private void LoadGetSupervisorData()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V1";
            string stringSessionID = "";
            string[] stringInputs = new string[2];
            DataTable objDataTable = null;
            try
            {
                if (Session["objDatasetlocaldeclaration"] != null)
                {
                    objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                }
                objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = stringformid;
                lblTotalRecords.InnerText = "0";
                objDatasetResult = CommonFunctions.UpdateRegistrationR1V2("UpdateRegistrationR1V2", stringformid, out stringSessionID, out interrorcount, out stringOutputResult);
                 
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[0] != null && objDatasetResult.Tables[0].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables[0];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        gvPymt.DataSource = objDataTable;
                        gvPymt.DataBind();
                        ViewState["stringSessionID"] = stringSessionID;
                        lblTotalRecords.InnerText = objDataTable.Rows.Count.ToString();
                    }
                    else
                    {
                        gvPymt.DataSource = null;
                        gvPymt.DataBind();
                        ViewState["stringSessionID"] = null;
                        lblTotalRecords.InnerText = "0";
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
                stringOutputResult = null;
                stringformid = null;
                stringSessionID = null;
                stringInputs = null;
                objDataTable = null;
            }

        }
        private void Errorpopup(string[] stringOutputResult)
        {
            try
            {
                lblErrorType.Text = stringOutputResult[1];
                lblErrorCode.Text = stringOutputResult[0];  
                txterrormsg.Text = stringOutputResult[2];
                Modelpopuperror.Show();
                if(pnlcopyrequest.Visible == true)
                {
                    mdlpopupsupervisor.Show();
                    pnlcopyrequest.Visible = true;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private void ClearValues()
        {
            string stringUserID = "";
            try
            {
                stringUserID = Session["G11EOSUser_Name"].ToString();
                txtDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                txtCloseBy.Text = stringUserID;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringUserID = null;
            }
        }

        protected void btncloseall_Click(object sender, EventArgs e)
        {
            try
            {
                modelpopupsupervisercounter.Show(); 
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                pnlcopyrequest.Visible = true;
            }
        }
        private void LoadUserProfiles()//fix -ok
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FB0003R1V1";
            string stringOrderBy = "USR.user_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableLoadDelayReasons = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            try
            {
                ddlactionby.Items.Clear();
                stringServiceType = "List1R1v1AuditServiceClient";
                stringexp = "And USR.be_id= '" + stringbeid + "' And USR.delmark= 'N' And (GROUP_ID= 'HIMS SUPERVISOR' or GROUP_ID= 'HIMS USERS' or GROUP_ID= 'ALL')";
                if (Session["SSNLOADSUPERVISOR"] != null)
                {
                    objdatatableLoadDelayReasons = (DataTable)Session["SSNLOADSUPERVISOR"];
                }
                if ((objdatatableLoadDelayReasons == null) || (objdatatableLoadDelayReasons != null && objdatatableLoadDelayReasons.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadDelayReasons = objDatasetResult.Tables["t1"];
                            Session["SSNLOADSUPERVISOR"] = objdatatableLoadDelayReasons;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadDelayReasons != null && objdatatableLoadDelayReasons.Rows.Count > 0)
                {
                    ddlactionby.DataValueField = "user_name";
                    ddlactionby.DataTextField = "user_name";
                    ddlactionby.DataSource = objdatatableLoadDelayReasons;
                    ddlactionby.DataBind();
                    ddlactionby.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlactionby.DataSource = null;
                    ddlactionby.DataBind();
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
                objdatatableLoadDelayReasons = null;
                stringbeid = null;
                stringServiceType = null;
                stringexp = null;
            } 

        }
      
        protected void BtnCloseConfirm_Click(object sender, EventArgs e)
        {
            string stringResult = "";
            int intSuccessCount = 0;
            DataRow objdatarow = null;
            DataSet objDatasetResult = null;
            DataSet objDatasetResult01 = null;
            DataTable objDataTable = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            string stringBoID = "";
            string stringSessionID = "";
            string stringSessionID01 = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            string stringUserID = Session["G11EOSUser_Name"].ToString();
            string stringexp = "";
            string[] stringComponent = null;
            try
            {
                stringServiceType = "DEFAULT";
                stringexp = "";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (objDatasetResult != null && objDatasetResult.Tables["t4"].Rows.Count == 0)
                {
                    objdatarow = objDatasetResult.Tables["t4"].NewRow();
                    objdatarow["be_id"] = CommonFunctions.GETBussinessEntity().ToString();
                    if (ViewState["stringSessionID"] != null)
                    {
                        objdatarow["SESSION_ID"] = ViewState["stringSessionID"].ToString();
                    }
                    objdatarow["PymtCounter_ID"] = stringUserID;
                    objdatarow["Trans_Date"] = DateTime.Now;
                    objdatarow["Close_By"] = stringUserID;
                    objdatarow["delmark"] = "N";

                    CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                    objDatasetResult.Tables["t4"].Rows.Add(objdatarow);
                    objDatasetResult.Tables["t4"].Rows[0].RowState.ToString();
                    intSuccessCount++;

                    if (intSuccessCount > 0)
                    {
                        objDatasetResult = objDatasetResult.GetChanges();
                        stringServiceType = "OperationServiceDML";
                        objDatasetResult01 = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);
                        if (intErrorCount == 0)
                        {
                            intSuccessCount++;
                        }
                        else
                        {
                            stringResult = "FAILED";
                            Errorpopup(stringOutputResult);
                        }
                    }
                }

                if (intSuccessCount > 0)
                { 
                    stringComponent = new string[1];
                    if (ViewState["stringSessionID"] != null)
                    {
                        stringSessionID01 = ViewState["stringSessionID"].ToString();
                        stringComponent[0] = stringSessionID01.ToString();
                    } 
                    objDatasetResult = CommonFunctions.UpdateRegistrationR1V3("UpdateRegistrationR1V3", stringComponent, stringformid, out stringSessionID, out interrorcount, out stringOutputResult);
                      
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[0] != null && objDatasetResult.Tables[0].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables[0];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            gvPymt.DataSource = objDataTable;
                            gvPymt.DataBind();
                        }
                        else
                        {
                            gvPymt.DataSource = null;
                            gvPymt.DataBind();
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }

                    CommonFunctions.ShowMessageboot(this, "Counter(s) Closed Successfully.");
                    LoadRecord("AND tccc.SUPSESSION_ID='" + stringSessionID01 + "'AND trans_status = 'CLOSED' and convert(date, modified_on) = convert(date, GETDATE())");
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, stringResult);
                    btncloseall.Enabled = true;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringResult = null;
                intSuccessCount = 0;
                objdatarow = null;
                objDatasetResult = null;
                objDatasetResult01 = null;
                objDataTable = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = null;
                stringBoID = null;
                stringSessionID = null;
                stringSessionID01 = null;
                stringUserID = null;
                stringexp = null;
                stringComponent = null;
            }
        }
      
        protected void btnno_Click(object sender, EventArgs e)
        {
            try
            {
                modelpopupsupervisercounter.Hide();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdPaging);
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
                rptPager.DataSource = pages;
                rptPager.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["intpageIndex1"] != null)
                {
                    intpageIndex = Convert.ToInt32(Session["intpageIndex1"].ToString());
                    Session["intpageIndex"] = intpageIndex;
                }
                else
                {
                    intpageIndex = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndex != 0)
                    {
                        Session["intpageIndex"] = intpageIndex;
                    }
                }

                if (intpageIndex == 1)
                {
                    intrecFrom = 0;
                    intrecTo = CommonFunctions.GridViewPagesize(stringformIdPaging);
                }
                else
                {
                    int intrecFrom1 = (intpageIndex * intrecTo) - intrecTo;
                    intrecFrom = intrecFrom1 + 1;
                    intrecTo = intrecFrom1 + CommonFunctions.GridViewPagesize(stringformIdPaging);
                }

                if (ViewState["vsSearchCondition"] != null && ViewState["vsSortExpression"] != null)
                {
                    LoadRecord((string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString());
                }
                else
                {
                    LoadRecord();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private void LoadRecord(string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {
            DataSet objDataSet = null;
            int intRecordCount = 0;
            try
            {
                ViewState["vsSearchCondition"] = Condition;
                objDataSet = GetRecords(out intRecordCount, Condition, SortExpression, RecordFrom, RecordTo);
                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[1] != null && objDataSet.Tables[1].Rows.Count > 0)
                {
                    gvList.DataSource = objDataSet.Tables[1];
                    gvList.DataBind();
                } 
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDataSet = null;
                intRecordCount = 0;
            }
        }

        public DataSet GetRecords(out int intRecordCount, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {

            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V1";
            intRecordCount = 0;
            string stringServiceType = "List2R1V1";
            int intRecordFrom = 0;
            int intRecordTo = 0;
            try
            {
                intRecordFrom = intrecFrom;
                intRecordTo = intrecTo;
                ViewState["vsSearchCondition"] = Condition;
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, Condition, SortExpression, intRecordFrom, intRecordTo, out intRecordCount, out interrorcount, out stringOutputResult);
                PopulatePager(intRecordCount, intpageIndex);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[1] != null && objDatasetResult.Tables[1].Rows.Count > 0)
                    {

                        return (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[1].Rows.Count > 0) ? objDatasetResult : null;

                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "No Records Found");
                        gvList.DataSource = null;
                        gvList.DataBind();
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
                stringOutputResult = null;
                stringformid = null;
                stringServiceType = null;
            }
            return null;
        }

        //paging

        protected void LnkbtnSort_Click(object sender, EventArgs e)//fixed
        {
            DataSet objDataSetSort1 = null;
            objDataSetSort1 = new DataSet();
            string stringColumnName = "";
            try
            {
                if (sender != null)
                {
                    stringColumnName = ((LinkButton)sender).CommandArgument;
                    if (stringColumnName != null && stringColumnName.Trim().Length > 0)
                    {

                        ViewState["vsSortDirection"] = (ViewState["vsSortDirection"] != null && ViewState["vsSortDirection"].ToString().Trim() == "ASC") ? " DESC" : " ASC";
                        ViewState["vsSortExpression"] = "tccc." + stringColumnName + ViewState["vsSortDirection"].ToString();
                        LoadRecord((string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString());
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDataSetSort1 = null;
                stringColumnName = null;

            }
        }
         
        private void VerifyAccessRights()
        { 
            DataSet objDatasetResult = null; 
            string stringstatus = "";
            string[] stringOutputResult = null;
            string[] stringComponent = null;
            DataRow objDataRow = null; 
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FB0003R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        if (objDataRow["new"].ToString().ToUpper() == "ENABLED")
                        { 
                        }
                        if (objDataRow["edit"].ToString().ToUpper() == "ENABLED")
                        { 
                        }
                        if (objDataRow["delete"].ToString().ToUpper() == "ENABLED")
                        { 
                        }
                        if (objDataRow["Export"].ToString().ToUpper() == "ENABLED")
                        { 
                        }
                    }
                    else
                    {
                        Response.Redirect("PageAccessDenied.aspx", true);
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
        private void LoadReports(string stringFORMID, string stringreportname)
        { 
            byte[] objbytereturn = null;
            string stringFile = string.Empty;
            string stringoutputresult = "";
            int intErrorCount = 0;
            string[] stringOutputResult = null;
            string[] stringInputs = new string[11];
            string[] stringSettings = new string[21];
            string stringbeid = "";
            string stringaddress = "";
            string stringTelephoneNo = "";
            string stringfromdate = ""; 
            string stringtodate = "";
            string stringLofoFlag = "";
            stringbeid = CommonFunctions.GETBussinessEntity(); 
            stringaddress = ConfigurationManager.AppSettings["BEIDAddress"];
            stringTelephoneNo = ConfigurationManager.AppSettings["BEIDTelephoneNo"];
            string stringUserDisplayName = "";
            string StringddlExportFormat = string.Empty; 
            string stringuserID = ""; 
            DateTime objDateTimeFrom;
            try
            { 
                if (Session["G11EOSUser_Name"] != null)
                {
                    stringUserDisplayName = Session["G11EOSUser_Name"].ToString();
                }
                StringddlExportFormat = "PDF";

                if (Session["stringUserDisplayName"] != null)
                {
                    stringUserDisplayName = Session["stringUserDisplayName"].ToString();
                }
                if (txtcloseDate.Text.Trim().Length > 0 && CommonFunctions.ValidDateTime(txtcloseDate.Text.Trim()))
                {
                    objDateTimeFrom = CommonFunctions.ConvertToDateTime(txtcloseDate.Text.Trim(), "dd-MM-yyyy");
                    stringfromdate = CommonFunctions.ConvertDateTimetoString(objDateTimeFrom);
                }
                if (ddlactionby.SelectedItem != null && ddlactionby.SelectedValue.Length > 0 && ddlactionby.SelectedItem.Text.Length > 0)
                {
                    stringuserID = ddlactionby.SelectedItem.Text.ToString();
                }
                stringInputs[0] = stringbeid;
                stringInputs[1] = stringuserID;
                stringInputs[2] = stringfromdate;
                stringInputs[3] = "";
                stringInputs[4] = "";
                stringInputs[5] = "0";
                stringInputs[6] = "";
                stringInputs[7] = "";
                stringInputs[8] = "";
                stringInputs[9] = "";
                stringInputs[10] = "";


                stringSettings[0] = stringbeid;
                stringSettings[1] = stringaddress;
                stringSettings[2] = "";
                stringSettings[3] = "";
                stringSettings[4] = "";
                stringSettings[5] = "";
                stringSettings[6] = ConfigurationManager.AppSettings["copyright"].ToString();
                stringSettings[7] = stringFORMID; 
                if (StringddlExportFormat == "PDF")
                {
                    stringSettings[8] = "PORTALBLEDOCFORMAT";
                }
                else if (StringddlExportFormat == "EXCEL DATA")
                {
                    stringSettings[8] = "EXCELRECORD";
                }
                else if (StringddlExportFormat == "EXCEL")
                {
                    stringSettings[8] = "EXCEL";
                }
                else if (StringddlExportFormat == "WORD")
                {
                    stringSettings[8] = "WORDFORWINDOW";
                }
                stringSettings[9] = stringreportname;
                stringSettings[10] = "";
                stringSettings[11] = "";
                stringSettings[12] = stringTelephoneNo;
                stringSettings[13] = "";
                stringSettings[14] = stringreportname;

                stringSettings[15] = "param_from_date" + "-->" + stringfromdate;
                stringSettings[16] = "param_to_date" + "-->" + stringtodate;
                stringSettings[17] = "LoginUserID" + "-->" + stringUserDisplayName.Trim();

                stringSettings[18] = "pat_address" + "-->" + stringaddress.Trim();
                stringLofoFlag = LoadINSTLogo();
                stringSettings[19] = "Print_flag" + "-->" + stringLofoFlag.ToString();
                stringSettings[20] = "";
                
                objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = stringFORMID;

               
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

                        mdlpopupsupervisor.Show();
                        pnlcopyrequest.Visible = true; 
                    }
                    else
                    {
                        CommonFunctions.ShowMessage("Report Not Found.");
                        mdlpopupsupervisor.Show();
                        pnlcopyrequest.Visible = true;
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

                mdlpopupsupervisor.Show();
                pnlcopyrequest.Visible = true;
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
                stringfromdate = null;
                stringtodate = null;
            }
        }
         
        protected void btnSupervisorCounterReport_Click(object sender, EventArgs e)
        {
            ddlactionby.ClearSelection();
            txtcloseDate.Text = "";
            rbsummary.Checked = true;
            rbDetail.Checked = false;
            LoadUserProfiles();
            mdlpopupsupervisor.Show();
            pnlcopyrequest.Visible = true;
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
        protected void btnexport_Click(object sender, EventArgs e)
        {
            string stringPDFTITLE1 = "";
            string stringFORMID1 = "";
            try
            {
                if (rbsummary.Checked)
                {
                    stringPDFTITLE1 = "DOP700094R1V1";
                    stringFORMID1 = "CashierHandOver Summary";
                    LoadReports(stringPDFTITLE1, stringFORMID1);
                }
                else if (rbDetail.Checked)
                {
                    stringPDFTITLE1 = "DOP700093R1V1";
                    stringFORMID1 = "CashierHandOver Detail";
                    LoadReports(stringPDFTITLE1, stringFORMID1);
                }
               
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringPDFTITLE1 = null;
                stringFORMID1 = null;
            }

        }
    }
}