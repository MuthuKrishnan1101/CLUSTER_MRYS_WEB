using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FC0007R1V1 : System.Web.UI.Page
    {
        public DataSet objDatasetAppsVariables;
        public int pageIndex = 0;
        public int recFrom = 0;
        public int recTo = 0;
        public string stringformIdPaging = "FC0007R1V1gridviewpagesize";

        protected void Page_Load(object sender, EventArgs e)
        {
            
             
            string stringRequestID = "";
            string stringref_1 = "";
            string stringreqid = "";
            string stringremid = "";
            try
            {
                if (CommonFunctions.IsActive())
                {
                    if (Session["REQUESTID_REMARKS"] != null)
                    {
                        stringRequestID = Session["REQUESTID_REMARKS"].ToString();
                        Session["REQUESTID_REMARKS"] = stringRequestID; 
                    }
                    else
                    {
                        Session["REQUESTID_REMARKS"] = null; 
                    }
                    recTo = CommonFunctions.GridViewPagesize(stringformIdPaging);

                    if (!IsPostBack)
                    {
                        VerifyAccessRights(); 
                        hdfmramount.Value = "";
                        hdfddlBlockBill.Value = "";
                        hdfddlWApp.Value = "";
                        hdfddlWApproved.Value = "";
                        hdfRecallcurreentStatus.Value = "";
                        CommonFunctions.HeaderName(this, "FC0007R1V1");
                        ClearValues();
                        LoadRegRequestInfo(stringRequestID);
                        LoadRemarks();
                        LoadTarget();
                        LoadGrid(stringRequestID);

                        if (Request.QueryString["ID1"] != null) { stringref_1 = Request.QueryString["ID1"].ToString(); }
                        if (Request.QueryString["ID2"] != null) { stringreqid = Request.QueryString["ID2"].ToString(); }
                        if (Request.QueryString["ID3"] != null) { stringremid = Request.QueryString["ID3"].ToString(); }
                       
                        if (stringref_1.Length > 0 && stringref_1.Length > 0 && stringref_1.Length > 0)
                        {
                            LoadData(stringreqid, stringref_1, stringremid);
                            Session["FC0007R1V1_REMARKID"] = stringremid;
                        }
                         
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringRequestID = "";
                stringref_1 = "";
                stringreqid = "";
                stringremid = "";
            }
        }
        #region grid related codes
        private void LoadGrid(string stringRequestID)//fix
        {
            string stringexp012 = "";
            string stringBEID = CommonFunctions.GETBussinessEntity().ToString();
            try
            {
                stringexp012 = "And mrregrmk.be_id= '" + stringBEID + "' And mrregrmk.request_id= '" + stringRequestID + "' ";
                LoadRecord(stringexp012);
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringexp012 = "";
                stringBEID = "";
            }
        }
        private void VerifyAccessRights()
        {

            DataSet objDatasetResult = null;

            string stringstatus = "";
            string[] stringOutputResult = null;
            string[] stringComponent = null;
            DataRow objDataRow = null;
            //imgBtnNew.Enabled = false;
            // imgBtnSave.Enabled = false;
            //imgBtnDelete.Enabled = false;
            // imgBtnPrint.Enabled = false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FC0007R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        if (objDataRow["new"].ToString().ToUpper() == "ENABLED")
                        {
                            // imgBtnNew.Enabled = true;
                            // imgBtnSave.Enabled = true;
                        }
                        if (objDataRow["edit"].ToString().ToUpper() == "ENABLED")
                        {
                            //  imgBtnSave.Enabled = true; 
                        }
                        if (objDataRow["delete"].ToString().ToUpper() == "ENABLED")
                        {
                            //imgBtnDelete.Enabled = true;
                        }
                        if (objDataRow["Export"].ToString().ToUpper() == "ENABLED")
                        {
                            //  imgBtnPrint.Enabled = true;
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
                stringstatus = "";
                stringOutputResult = null;
                stringComponent = null;
            }
        }
        private void LoadRecord(string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {
            DataTable objdatatableremarks = null;
            DataSet objDataSet = null;
            int intRecordCount = 0;
            try
            {
                ViewState["vsSearchCondition"] = Condition;
                intRecordCount = 0;
                objDataSet = GetRecords(out intRecordCount, Condition, SortExpression, RecordFrom, RecordTo);
                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["t9"] != null && objDataSet.Tables["t9"].Rows.Count > 0)
                {
                    objdatatableremarks = objDataSet.Tables["t9"];
                    if(objdatatableremarks!=null && objdatatableremarks.Rows.Count > 0)
                    {
                        foreach (DataRow row in objdatatableremarks.Rows)
                        {
                            if (row["DML_INDICATOR"].ToString() != "I")
                            {
                                row["DML_INDICATOR"] = "U";
                            }
                            else
                            {
                                row["DML_INDICATOR"] = "I";
                            }
                        }

                    }

                    objdatatableremarks.AcceptChanges();
                    Session["ADD_REMARKS"] = objdatatableremarks; 
                    Bindings(objdatatableremarks);
                }
                else
                {
                    Bindings(null);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objdatatableremarks = null;
                objDataSet = null;
                intRecordCount = 0;
            }
        }

        public DataSet GetRecords(out int intRecordCount, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {

            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            intRecordCount = 0;
            string stringServiceType = "List9R1V1";
            int intRecordFrom = 0;
            int intRecordTo = 0;

            try
            {
                intRecordFrom = recFrom;
                intRecordTo = recTo;
                ViewState["vsSearchCondition"] = Condition;
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, Condition, SortExpression, intRecordFrom, intRecordTo, out intRecordCount, out interrorcount, out stringOutputResult);
               
                if (interrorcount == 0)
                { 
                    Session["ADD_REMARKS"] = objDatasetResult.Tables["t9"];
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t9"] != null && objDatasetResult.Tables["t9"].Rows.Count > 0)
                    {

                        return (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t9"].Rows.Count > 0) ? objDatasetResult : null;

                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "No Records Found");
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                    Session["ADD_REMARKS"] = null;
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
                stringformid = "";
                stringServiceType = "";
                intRecordFrom = 0;
                intRecordTo = 0;
            }

            return null;
        }
         
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
                        ViewState["vsSortExpression"] = "mrregrmk." + stringColumnName + ViewState["vsSortDirection"].ToString();
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
                stringColumnName = "";
            }

        }
        #endregion
        #region paging
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
                if (Session["PageIndex1"] != null)
                {
                    pageIndex = Convert.ToInt32(Session["PageIndex1"].ToString());
                    Session["PageIndex"] = pageIndex;
                }
                else
                {
                    pageIndex = int.Parse((sender as LinkButton).CommandArgument);
                    if (pageIndex != 0)
                    {
                        Session["PageIndex"] = pageIndex;
                    }
                }

                if (pageIndex == 1)
                {
                    recFrom = 0;
                    recTo = CommonFunctions.GridViewPagesize(stringformIdPaging);
                }
                else
                {
                    int recFrom1 = (pageIndex * recTo) - recTo;
                    recFrom = recFrom1 + 1;
                    recTo = recFrom1 + CommonFunctions.GridViewPagesize(stringformIdPaging);
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
        #endregion
        #region method
        private void ClearValues()//fixed
        {
            try
            {
                txtDesc.Text = "";
                ddlTarget.ClearSelection();
                ddlRemarks.ClearSelection();
                txtRemarkdte.Text = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy"); 
                Session["FC0007R1V1_REMARKID"] = null; 
                Session["ADD_REMARKS"] = null;
                ddlRemarks.Enabled = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        private void LoadRegRequestInfo(string stringRequestNo)//fixed
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            DataRow objDataRow = null;
            string stringServiceType1 = "";
            string stringexp012 = "";
            string stringReportTypID = "";
            string stringStatus = "";
            string stringMRamt = "";
            string stringUserID = "";
            string stringcancellstatus = "";  
            try
            {
                if (stringRequestNo != null && stringRequestNo.Trim().Length > 0)
                {
                    stringServiceType1 = "List5R1V1";
                    stringexp012 = "And mrreg.be_id= '" + stringbeid + "'  And mrreg.request_id= '" + stringRequestNo.ToString() + "'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t5"] != null && objDatasetResult.Tables["t5"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t5"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            objDataRow = objDataTable.Rows[0];
                            LoadProcesstabData(objDataRow);
                            txtRequestNo.Text = objDataRow["request_id"].ToString();
                            txtReq.Text = objDataRow["RptReq_ID"].ToString();


                            if (objDataRow["receive_date"] != null && objDataRow["receive_date"].ToString().Trim().Length > 0)
                            { txtRecDate.Text = Convert.ToDateTime(objDataRow["receive_date"]).ToString("dd-MM-yyyy"); }


                            stringReportTypID = objDataRow["rpttyp_id"].ToString();
                            txtRptType.ToolTip = stringReportTypID;
                            txtRptType.Text = objDataRow["report_type_short_name"].ToString();

                            stringStatus = objDataRow["mr_status"].ToString();
                            txtMRStatus.Text = stringStatus;
                            stringMRamt = objDataRow["MR_AMOUNT"].ToString();
                            
                            hdfmramount.Value = stringMRamt;
                            hdfddlBlockBill.Value = objDataRow["Block_Billing"].ToString();
                            hdfddlWApp.Value = objDataRow["WAIVER_STATUS_1"].ToString();
                            hdfddlWApproved.Value = objDataRow["WAIVER_APPROVED"].ToString();
                            if (objDataRow["PRIORITY_FLAG"].ToString() == "Y")
                            {
                                chkpriorityflag.Checked = true;
                            }
                            else
                            {
                                chkpriorityflag.Checked = false;
                            }
                            txtMRNumberHEADER.Text = objDataRow["MR_ID"].ToString();
                            txtHRN.Text = objDataRow["hrn_id"].ToString();
                            txtPatName.Text = objDataRow["patient_short_name"].ToString();
                            stringUserID = objDataRow["CREATED_BY"].ToString();

                            stringcancellstatus = objDataRow["sup_status"].ToString();
                            if ( stringStatus.Trim().ToUpper() == "CANCELLED" || stringcancellstatus.ToUpper() == "PENDING") 
                            { ControlsEnabledByProStatus("CANCELLED"); }
                            else if (stringStatus.Trim().ToUpper() == "FORWARDED" || stringStatus.Trim().ToUpper() == "COLLECTED" )
                            {
                                ControlsEnabledByProStatus("FORWARDED");
                            }
                        }
                        else
                        {
                            txtRequestNo.Text = txtMRNumberHEADER.Text = txtMRStatus.Text = "";
                            chkpriorityflag.Checked = false;
                            txtWritingandVerifyingStatus.Text = "";
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
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                objDataTable = null;
                stringbeid = "";
                objDataRow = null;
                stringServiceType1 = "";
                stringexp012 = "";
                stringReportTypID = "";
                stringStatus = "";
                stringMRamt = "";
                stringUserID = "";
                stringcancellstatus = ""; 
            }
        }
        private void ControlsEnabledByProStatus(string stringStatus)//fixed
        {
            switch (stringStatus)
            {
                case "CANCELLED":
                    {
                        //imgBtnNew.Enabled = false;
                        //imgBtnSave.Enabled = false;
                        //imgBtnDelete.Enabled = false;
                        //imgBtnClear.Enabled = false;
                        //imgBtnSearch.Enabled = false;
                        //imgBtnPrint.Enabled = false;
                        //imgBtnSetting.Enabled = false;
                        //imgBtnSecurity.Enabled = true;

                        txtDesc.ReadOnly = true;
                        ddlTarget.Enabled = false;
                        ddlRemarks.Enabled = false;
                        txtRemarkdte.ReadOnly = true;

                        pnlForwarded.Enabled = false;
                        btnPendingTracing.Enabled = false;
                        btnPendingDespatch.Enabled = false;
                        btnPendingAssigned.Enabled = false;
                        btnPendingReport.Enabled = false;
                        btnPendingReleasetoHIMS.Enabled = false;
                        btnPendingSupVetting.Enabled = false;
                        btnPendingforwarding.Enabled = false;
                        btnPendingCollectInPerson.Enabled = false;
                        break;
                    }
                case "MR CREATED":
                    {
                        //imgBtnNew.Enabled = true;
                        //imgBtnSave.Enabled = true;
                        //imgBtnDelete.Enabled = true;
                        //imgBtnClear.Enabled = true;
                        //imgBtnSearch.Enabled = true;
                        //imgBtnPrint.Enabled = true;
                        //imgBtnSetting.Enabled = true;
                        //imgBtnSecurity.Enabled = true;

                        txtDesc.ReadOnly = false;
                        ddlTarget.Enabled = true;
                        ddlRemarks.Enabled = true;
                        txtRemarkdte.ReadOnly = false;
                        break;
                    }
                case "FORWARDED":
                    {
                        //imgBtnNew.Enabled = true;
                        //imgBtnSave.Enabled = true;
                        //imgBtnDelete.Enabled = true;
                        //imgBtnClear.Enabled = true;
                        //imgBtnSearch.Enabled = true;
                        //imgBtnPrint.Enabled = true;
                        //imgBtnSetting.Enabled = true;
                        //imgBtnSecurity.Enabled = true;

                        txtDesc.ReadOnly = false;
                        ddlTarget.Enabled = true;
                        ddlRemarks.Enabled = true;
                        txtRemarkdte.ReadOnly = false;
                        break;
                    }
            }
        }
        private void LoadRemarks()//fixed
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0023R1V1";
            string stringOrderBy = "mreeks.ORDER_ID asc,mreeks.short_name asc";
            int intFromRecord = 0;
            int intToRecord = 5000;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringDeleteMark = "";
            string stringexp012 = "";
            string stringServiceType = "";
            try
            {
                ddlRemarks.Items.Clear();
                stringDeleteMark = "N";

                stringexp012 = "And mreeks.be_id= '" + stringbeid + "' And mreeks.delmark= '" + stringDeleteMark.ToString() + "'";

                stringServiceType = "List1R1V1";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t1"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        ddlRemarks.DataTextField = "short_name";
                        ddlRemarks.DataValueField = "rptrmk_id";
                        ddlRemarks.DataSource = objDataTable;
                        ddlRemarks.DataBind();
                        ddlRemarks.Items.Insert(0, new ListItem("", ""));

                        ddlRemarks.SelectedIndex = 1;
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
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                objDataTable = null;
                stringbeid = "";
                stringDeleteMark = "";
                stringexp012 = "";
                stringServiceType = "";
            }
        }

        private void LoadTarget()//fixed
        {
            DataSet objDataSet = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FAD1012R1V1";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringOrderBy = "ags.ACCESS_GRP_DESC asc";
            string stringexp = "";
            string stringServiceType = "";
            try
            {
                ddlTarget.Items.Clear();
                stringexp = "and ags.be_id= '" + stringbeid + "' ";
                stringServiceType = "List1R1v1AdminServiceClient";
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["t1"] != null && objDataSet.Tables["t1"].Rows.Count > 0)
                    {

                        ddlTarget.DataTextField = "ACCESS_GRP_DESC";
                        ddlTarget.DataValueField = "ACCESS_GRP_ID";
                        ddlTarget.DataSource = objDataSet.Tables[0];
                        ddlTarget.DataBind();
                        ddlTarget.Items.Insert(0, new ListItem("", ""));
                        if (objDataSet.Tables[0].Select("ACCESS_GRP_ID='ALL'").Length == 0)
                        {
                            ddlTarget.Items.Insert(1, new ListItem("ALL", "ALL"));
                        }

                        ddlTarget.SelectedIndex = 1;
                    }
                    else
                    {
                        ddlTarget.DataSource = objDataSet;
                        ddlTarget.DataBind();
                    } 
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, stringOutputResult[0]);
                }


            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringexp = "";
                stringServiceType = "";
            }
             

        }
        private void Errorpopup(string[] stringOutputResult)//fixed
        {
            try
            {
                lblErrorType.Text = stringOutputResult[1];
                lblErrorCode.Text = stringOutputResult[0];
                txterrormsg.Text = stringOutputResult[2];
                Modelpopuperror.Show();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private object[] GetThisSCreenControls(string stringType)
        {
            object[] objControls = null;
            try
            {
                if (stringType.ToUpper() == "TAB1")
                {
                    objControls = new object[] {
                        txtHRN,
                        txtRequestNo,
                        ddlRemarks,
                        ddlTarget,
                    };

                    return objControls;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objControls = null;
            }
            return objControls;
        }
        protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
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
        #endregion
        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                SaveRemarks();

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private void SaveRemarks()
        {
            DataTable objDataTableAddReports = null;
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataSet objDatasetResult1 = null;
            string stringformid = "FC0001R1V1";
            string stringServiceType = "DEFAULT";
            string stringexp = "";
            string stringBoID = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            DataRow objDataRow = null;
            int intErrorCount = 0;
            string stringDMLIndicator = "";
            string stringcustemerid3 = "";
            string stringServiceType1 = "";
            string stringformid1 = "";
            try
            {
                if (Session["ADD_REMARKS"] != null)
                { objDataTableAddReports = (DataTable)Session["ADD_REMARKS"]; }


                if (objDataTableAddReports != null && objDataTableAddReports.Rows.Count > 0)
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t9"].Rows.Count == 0)
                        {
                            for (int intIndex = 0; intIndex < objDataTableAddReports.Rows.Count; intIndex++)
                            {
                                objDataRow = objDatasetResult.Tables["t9"].NewRow();

                                stringDMLIndicator = objDataTableAddReports.Rows[intIndex]["DML_INDICATOR"].ToString();

                                if (stringDMLIndicator != null && stringDMLIndicator.Trim().Length > 0)
                                {
                                    //if (stringDMLIndicator == "U")
                                    //{
                                    //    string stringref_1 = objDataTableAddReports.Rows[intIndex]["reference_1"].ToString();
                                    //    string stringreq_id = objDataTableAddReports.Rows[intIndex]["Request_ID"].ToString();
                                    //    string stringremid = objDataTableAddReports.Rows[intIndex]["REGRMK_ID"].ToString();
                                    //    objDataTableAddReports.Rows[0].Delete();
                                    //    objDataTableAddReports.Rows[0].RowState.ToString();

                                    //}
                                    objDataRow["DELMARK"] = "N";
                                    objDataRow["REFERENCE_5"] = stringDMLIndicator;
                                    objDataRow["Request_ID"] = objDataTableAddReports.Rows[intIndex]["Request_ID"].ToString();
                                    objDataRow["REGRMK_ID"] = objDataTableAddReports.Rows[intIndex]["REGRMK_ID"].ToString();
                                    objDataRow["TARG_AUD"] = objDataTableAddReports.Rows[intIndex]["TARG_AUD"].ToString();
                                    objDataRow["REMARKS_DATE"] = objDataTableAddReports.Rows[intIndex]["REMARKS_DATE"].ToString();
                                    objDataRow["reference_1"] = objDataTableAddReports.Rows[intIndex]["reference_1"].ToString();
                                    objDataRow["Remarks"] = objDataTableAddReports.Rows[intIndex]["Remarks"].ToString();
                                    objDataRow["SHORT_NAME"] = objDataTableAddReports.Rows[intIndex]["SHORT_NAME"].ToString();
                                    objDataRow["LONG_NAME"] = objDataTableAddReports.Rows[intIndex]["Remarks"].ToString();
                                    CommonFunctions.AssignAuditLogDetails(ref objDataRow);

                                    objDatasetResult.Tables["t9"].Rows.Add(objDataRow);

                                    objDatasetResult.Tables["t9"].AcceptChanges();

                                    objDatasetResult.Tables["t9"].Rows[0].RowState.ToString();
                                }
                                objDatasetResult.Tables["t9"].AcceptChanges();


                            }
                            if (objDatasetResult.Tables["t9"].Rows.Count > 0)
                            {
                                for (int intIndex = 0; intIndex < objDatasetResult.Tables["t9"].Rows.Count; intIndex++)
                                {
                                    stringcustemerid3 = objDatasetResult.Tables["t9"].Rows[intIndex]["REFERENCE_5"].ToString();
                                    if (stringcustemerid3 == "D")
                                    {
                                        objDatasetResult.Tables["t9"].Rows[intIndex].Delete();
                                    }
                                    else if (stringcustemerid3 == "I")
                                    {
                                        objDatasetResult.Tables["t9"].Rows[intIndex].SetAdded();
                                    }
                                    else if (stringcustemerid3 == "U")
                                    {
                                        objDatasetResult.Tables["t9"].Rows[intIndex].SetModified();
                                    }
                                }

                                objDatasetResult = objDatasetResult.GetChanges();
                                stringServiceType1 = "OperationServiceDML";
                                stringformid1 = "FC0001R1V1";
                                objDatasetResult1 = CommonFunctions.DataManipulationR1V1(stringServiceType1, objDatasetResult.GetChanges(), stringformid1, out intErrorCount, out string[] stringOutputResult1);
                                if (intErrorCount == 0)
                                {

                                    CommonFunctions.ShowMessageboot(this, "Remarks Saved Successfully..");
                                }
                                else
                                {
                                    Errorpopup(stringOutputResult1);

                                }
                            }
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
                objDataTableAddReports = null;
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                objDatasetResult1 = null;
                stringformid = "";
                stringServiceType = "";
                stringexp = "";
                stringBoID = "";
                objDataRow = null;
                intErrorCount = 0;
                stringDMLIndicator = "";
                stringcustemerid3 = "";
                stringServiceType1 = "";
                stringformid1 = "";
            }
        }

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            DataSet objDatasetResult = null;  
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            int intcount = 0;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringremarksID = "" , stringrequest_id="";
            string stringexp = "";
            string stringServiceType = "";
            DataRow[] objdatarowState = null;
            try
            {
                if (Session["REQUESTID_REMARKS"] != null)
                {
                    stringrequest_id = Session["REQUESTID_REMARKS"].ToString();
                    stringexp = "And mrregrmk.be_id= '" + stringbeid + "'  And mrregrmk.Request_ID='" + stringrequest_id + "'";
                    stringServiceType = "List9R1V1";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult.Tables["t9"] != null && objDatasetResult.Tables["t9"] != null && objDatasetResult.Tables["t9"].Rows.Count > 0)
                        {
                            if (ddlRemarks.SelectedItem != null)
                            {
                                stringremarksID = ddlRemarks.SelectedItem.Value.ToString();

                            }
                            objdatarowState = objDatasetResult.Tables["t9"].Select("REQUEST_ID='" + stringrequest_id + "' and REGRMK_ID='"+ stringremarksID + "'");
                            if (objdatarowState != null && objdatarowState.Length > 0)
                            {
                                for (int intIndex1 = 0; intIndex1 < objdatarowState.Length; intIndex1++)
                                {
                                    objdatarowState[intIndex1]["DML_INDICATOR"] = "D";
                                    intcount++;
                                }
                            }
                        }

                        if (intcount > 0)
                        {
                            objDatasetResult.AcceptChanges();

                            if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t9"].Rows.Count > 0)
                            {
                                for (int intIndex = 0; intIndex < objDatasetResult.Tables.Count; intIndex++)
                                {
                                    objdatarowState = objDatasetResult.Tables["t9"].Select("REQUEST_ID='" + stringrequest_id + "' and REGRMK_ID='" + stringremarksID + "'");
     
                                    if (objdatarowState != null && objdatarowState.Length > 0)
                                    {
                                        for (int intIndex1 = 0; intIndex1 < objdatarowState.Length; intIndex1++)
                                        {
                                            objdatarowState[intIndex1].Delete();
                                        }
                                    }
                                }

                                stringServiceType = "OperationServiceDML";
                                objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                                if (intErrorCount == 0)
                                {

                                    CommonFunctions.ShowMessageboot(this, "Remarks deleted successfully.");
                                    Session["REQUESTID_REMARKS"] = null;
                                    ClearValues();
                                    gvList.DataSource = objDatasetResult;
                                    gvList.DataBind();
                                }
                                else
                                {
                                    Errorpopup(stringOutputResult);
                                }
                            }
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
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringexp = "";
                stringServiceType = "";
                objdatarowState = null;
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        { 
            string stringErrorMsg = "";
            string stringremarksID = ""; 
            DataTable objDataTableAddAttachments = null; 
            string stringbeid = CommonFunctions.GETBussinessEntity().ToString();
            bool boolrecordexits = true;
            bool boolrecrdsave = false;
            string stringexp012 = "";
            DataRow[] objdatarow = null;
            try
            {
                if (CommonFunctions.ValidateASPControls(GetThisSCreenControls("TAB1"), out stringErrorMsg))
                {
                    if (ddlRemarks.SelectedItem != null)
                    {
                        stringremarksID = ddlRemarks.SelectedItem.Value.ToString();
                    }

                    if (Session["ADD_REMARKS"] != null)
                    { objDataTableAddAttachments = (DataTable)Session["ADD_REMARKS"]; }
                    else
                    {
                        stringexp012 = "And mrregrmk.Request_ID= 'DEFAULTSTRUCTURE' ";
                        LoadRecord(stringexp012);
                        if (Session["ADD_REMARKS"] != null)
                        { objDataTableAddAttachments = (DataTable)Session["ADD_REMARKS"]; }
                    }
                    if (objDataTableAddAttachments != null)
                    {
                       
                        if (objDataTableAddAttachments != null && objDataTableAddAttachments.Rows.Count > 0)
                        {
                            if (Session["FC0007R1V1_REMARKID"] == null)
                            {
                                if (objDataTableAddAttachments.Select("Request_ID= '" + txtRequestNo.Text.Trim() + "' AND REGRMK_ID='" + stringremarksID + "'").Length > 0)
                                {
                                    boolrecordexits = false;
                                }
                            }
                            if (boolrecordexits)
                            {

                                objdatarow = objDataTableAddAttachments.Select("Request_ID= '" + txtRequestNo.Text.Trim() + "' AND REGRMK_ID='" + stringremarksID + "'");

                                if (objdatarow != null && objdatarow.Length > 0)
                                {
                                    objdatarow[0]["be_id"] = stringbeid;
                                    objdatarow[0]["Request_ID"] = txtRequestNo.Text.Trim();
                                    if (ddlRemarks.SelectedItem != null)
                                    {
                                        objdatarow[0]["REGRMK_ID"] = stringremarksID.ToString();
                                        Session["RemarksDelete"] = objdatarow[0]["REGRMK_ID"].ToString();
                                    }
                                    if (ddlTarget.SelectedItem != null)
                                    {
                                        objdatarow[0]["TARG_AUD"] = ddlTarget.SelectedItem.Value.ToString();
                                    }
                                    if (txtRemarkdte.Text.Trim().Length > 0)
                                    { objdatarow[0]["REMARKS_DATE"] = CommonFunctions.ConvertToDateTime(txtRemarkdte.Text, "dd-MM-yyyy"); }

                                    objdatarow[0]["remarks"] = txtDesc.Text.Trim();
                                    if (LoadRemarksInfo(txtRequestNo.Text.Trim(), stringremarksID))
                                    {
                                        objdatarow[0]["DML_INDICATOR"] = "I";
                                    }
                                    else
                                    {
                                        objdatarow[0]["DML_INDICATOR"] = "U";
                                    }
                                    objDataTableAddAttachments.AcceptChanges();
                                    boolrecrdsave = true;
                                }
                                else
                                {
                                    AddbtnInsert(stringremarksID, objDataTableAddAttachments, stringbeid);
                                    boolrecrdsave = true;
                                }
                            }
                            else
                            {
                                CommonFunctions.ShowMessageboot(this, "Record already exist");
                            }
                        }
                        else
                        {
                            AddbtnInsert(stringremarksID, objDataTableAddAttachments, stringbeid);
                            boolrecrdsave = true;
                        }
                        if(boolrecrdsave)
                        {
                            imgbtnSave_Click(null, null);
                        }

                        Session["ADD_REMARKS"] = objDataTableAddAttachments;
                        objDataTableAddAttachments.AcceptChanges();
                        Bindings(objDataTableAddAttachments);
                        ClearValuesafteradd();
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, stringErrorMsg);
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringErrorMsg = "";
                stringremarksID = "";
                objDataTableAddAttachments = null;
                stringbeid = "";
                boolrecordexits = true;
                boolrecrdsave = false;
                stringexp012 = "";
                objdatarow = null;
            }
        }
        private void ClearValuesafteradd()//fixed
        {
            try
            {
                txtDesc.Text = "";
                ddlTarget.ClearSelection();
                ddlRemarks.ClearSelection();
                txtRemarkdte.Text = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                Session["FC0007R1V1_REMARKID"] = null; 
                ddlRemarks.Enabled = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        private void Bindings(DataTable objDataTableAddAttachments)
        {
            int intRecordCount = 0;
            DataRow[] objDatarow = null;
            try
            {
                if (objDataTableAddAttachments != null && objDataTableAddAttachments.Rows.Count > 0)
                {
                    objDatarow = objDataTableAddAttachments.Select("DML_INDICATOR<>'D'");
                    if (objDatarow.Length > 0)
                    {
                        objDataTableAddAttachments = objDataTableAddAttachments.Select("DML_INDICATOR<>'D'").CopyToDataTable<DataRow>();
                        intRecordCount = objDataTableAddAttachments.Rows.Count;
                    }
                    else
                    {
                        objDataTableAddAttachments = null;
                        intRecordCount = 0;
                    }

                    PopulatePager(intRecordCount, pageIndex);
                    gvList.DataSource = objDataTableAddAttachments;
                    gvList.DataBind();
                    pnlresultgrid.Visible = true;
                    lblTotalRecords.InnerText = intRecordCount.ToString();
                }
                else
                {
                    PopulatePager(0, pageIndex);
                    lblTotalRecords.InnerText = "0";
                    gvList.DataSource = null;
                    gvList.DataBind();
                    pnlresultgrid.Visible = false;
                }
               

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                intRecordCount = 0;
                objDatarow = null;
            }
           
        }

        private void AddbtnInsert(string stringremarksID, DataTable objDataTableAddAttachments, string stringbeid)
        {
            DataRow objdatarow = null;
            string stringtransid = "";
            try
            {
                objdatarow = objDataTableAddAttachments.NewRow();

                objdatarow["be_id"] = stringbeid;
                objdatarow["Request_ID"] = txtRequestNo.Text.Trim();
                if (ddlRemarks.SelectedItem != null)
                {
                    objdatarow["REGRMK_ID"] = stringremarksID.ToString();
                    objdatarow["SHORT_NAME"] = ddlRemarks.SelectedItem.Text.ToString();
                }
                if (ddlTarget.SelectedItem != null)
                {
                    objdatarow["TARG_AUD"] = ddlTarget.SelectedItem.Value.ToString();
                }
                if (txtRemarkdte.Text.Trim().Length > 0)
                { objdatarow["REMARKS_DATE"] = CommonFunctions.ConvertToDateTime(txtRemarkdte.Text, "dd-MM-yyyy"); }
                stringtransid = DateTime.Now.ToString("HHmmssfff");
                objdatarow["reference_1"] = stringtransid;
                objdatarow["Remarks"] = txtDesc.Text.Trim().ToUpper();

                CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                objdatarow["DML_INDICATOR"] = "I";
                objDataTableAddAttachments.Rows.Add(objdatarow);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objdatarow = null;
                stringtransid = "";
            }
           
        }
         
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRow objDataRow01 = null;
            string stringSort = string.Empty;
            try
            {
                GridViewRow objGridViewRow = e.Row;
                stringSort = string.Empty;

                if (objGridViewRow.DataItem == null) { return; }

                objDataRow01 = ((DataRowView)e.Row.DataItem).Row;
               // ddlReportType = (DropDownList)e.Row.FindControl("ddlReportType");
                
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDataRow01 = null;
                stringSort = "";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable objOrderTable = null;
            DataRow[] objDataRow = null;
            bool boolrecrdsave = false;
            string stringCmdArgument = "";
            string stringref_1 = "";
            string stringreqid = "";
            string stringremid = "";
            string[] stringValues = null;
            try
            { 
                if (sender != null)
                {
                    stringCmdArgument = ((Button)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length > 0)
                        {
                            stringref_1 = stringValues[0];
                            stringreqid = stringValues[1];
                            stringremid = stringValues[2];


                            if (Session["ADD_REMARKS"] != null)
                            {
                                objOrderTable = (DataTable)Session["ADD_REMARKS"];
                            }

                            if (objOrderTable != null && objOrderTable.Rows.Count > 0)
                            {
                                if (stringref_1.Length > 0)
                                {
                                    objDataRow = objOrderTable.Select("reference_1='" + stringref_1 + "' and Request_ID='" + stringreqid + "' and REGRMK_ID='" + stringremid + "'");
                                }
                                
                                if (objDataRow != null && objDataRow.Length > 0)
                                {
                                    if (objDataRow[0]["DML_INDICATOR"].ToString() == "I")
                                    {
                                        objDataRow[0]["DML_INDICATOR"] = "D";
                                        boolrecrdsave = true;
                                    }
                                    else
                                    {
                                        objDataRow[0]["DML_INDICATOR"] = "D";
                                        boolrecrdsave = true;
                                    }
                                }

                                Session["ADD_REMARKS"] = objOrderTable;
                                if (boolrecrdsave)
                                {
                                    imgbtnDelete_Click(null, null);
                                }

                                Session["ADD_REMARKS"] = objOrderTable;
                                objOrderTable.AcceptChanges();
                                Bindings(objOrderTable);
                                ClearValuesafteradd();


                            }

                        }
                    }
                }


            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
            finally
            {
                objOrderTable = null;
                objDataRow = null;
                boolrecrdsave = false;
                stringCmdArgument = "";
                stringref_1 = "";
                stringreqid = "";
                stringremid = "";
                stringValues = null;
            }
        }

        protected void lnkbtnRemarkID_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string stringref_1 = "";
            string stringreqid = "";
            string stringremid = "";
            string[] stringValues = null;
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
                            stringref_1 = stringValues[0];
                            stringreqid = stringValues[1];
                            stringremid = stringValues[2];

                            Session["FC0007R1V1_REMARKID"] = stringremid;
                            LoadData(stringreqid, stringref_1, stringremid);
                            if (Session["ADD_REMARKS"] != null)
                            {
                                gvList.DataSource = (DataTable)Session["ADD_REMARKS"];
                                gvList.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
            finally
            {
                stringCmdArgument = "";
                stringref_1 = "";
                stringreqid = "";
                stringremid = "";
                stringValues = null;
            }
        }
        private void LoadData(string stringreq_id, string stringref_1, string stringremid)
        {
            DataRow[] objdatarow = null;
            DataTable objDataTable = null;
            string stringTARG_AUD = "";
            string stringRemarkID = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                if (stringreq_id.Length > 0 && stringref_1.Length > 0)
                {
                    if (Session["ADD_REMARKS"] != null)
                    {
                        objDataTable = (DataTable)Session["ADD_REMARKS"];
                    }
                    
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    { 
                        objdatarow = objDataTable.Select("be_id= '" + stringbeid + "' and  reference_1='" + stringref_1 + "' and REQUEST_ID='" + stringreq_id + "' and REGRMK_ID='" + stringremid + "'");
                        if (objdatarow != null && objdatarow.Length > 0)
                        { 
                            ddlTarget.ClearSelection();
                            stringTARG_AUD = objdatarow[0]["TARG_AUD"].ToString();
                            if (ddlTarget.Items.FindByValue(stringTARG_AUD) != null)
                                ddlTarget.Items.FindByValue(stringTARG_AUD).Selected = true;

                            ddlRemarks.ClearSelection();
                            stringRemarkID = objdatarow[0]["REGRMK_ID"].ToString();
                            if (ddlRemarks.Items.FindByValue(stringRemarkID) != null)
                                ddlRemarks.Items.FindByValue(stringRemarkID).Selected = true;

                            if (objdatarow[0]["REMARKS_DATE"] != null && objdatarow[0]["REMARKS_DATE"].ToString().Trim().Length > 0)
                            { txtRemarkdte.Text = Convert.ToDateTime(objdatarow[0]["REMARKS_DATE"]).ToString("dd-MM-yyyy"); }
                            
                            txtDesc.Text = objdatarow[0]["remarks"].ToString(); 
                            ddlRemarks.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
            finally
            {
                objdatarow = null;
                objDataTable = null;
                stringTARG_AUD = "";
                stringRemarkID = "";
                stringbeid = "";
            }
        }

        private bool LoadRemarksInfo(string stringRequestNo, string stringremid)//fixed
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue; 
            bool boolidexist = true;
            string stringbeid= CommonFunctions.GETBussinessEntity().ToString();
            string stringServiceType1 = "";
            string stringexp012 = "";
            try
            {
                if (stringRequestNo != null && stringRequestNo.Trim().Length > 0)
                {
                    stringServiceType1 = "List9R1V1";
                    stringexp012 = " and mrregrmk.be_id='" + stringbeid + "' and mrregrmk.REQUEST_ID='" + stringRequestNo + "' and mrregrmk.REGRMK_ID='" + stringremid + "'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t9"] != null && objDatasetResult.Tables["t9"].Rows.Count > 0)
                        {
                            boolidexist = false;
                        } 
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    } 
                }
                return boolidexist;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return boolidexist;
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                boolidexist = true;
                stringbeid = "";
                stringServiceType1 = "";
                stringexp012 = "";
            }
        }

        protected void LkBtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Session["REQUEST_FromSummary"] = txtRequestNo.Text.Trim();
                Response.Redirect("FC0001R1V1.aspx?TO=Y");

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        #region more button 
        protected void lnkbtnpayment_Click(object sender, EventArgs e)
        {
            string stringBlockBill = "";
            string stringWaiverApproved = "";
            string stringWaiverApplications = "";
            string stringBoID = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (txtRequestNo.Text.Trim().Length > 0)
                {
                    stringBlockBill = txtBlockBill.Text.Trim();
                    stringWaiverApproved = txtWApproved.Text.Trim();
                    stringWaiverApplications = txtWApp.Text.Trim();

                    if (stringBlockBill != null && stringBlockBill.Trim().ToUpper() == "YES")
                    {
                        CommonFunctions.ShowMessageboot(this, "Receipt cannot be generated for block billing MR.");
                        return;
                    }

                    if (stringWaiverApplications.ToUpper() == "YES" && stringWaiverApproved != null && stringWaiverApproved.Trim().Length > 0 && (stringWaiverApproved.Trim().ToUpper() == "FULLWAIVER" || stringWaiverApproved.Trim().ToUpper() == "PENDING"))
                    {
                        if (stringWaiverApproved.Trim().ToUpper() == "FULLWAIVER") { CommonFunctions.ShowMessageboot(this, "Waiver had been approved, therefore no payment will be generated."); return; }
                        else if (stringWaiverApproved.Trim().ToUpper() == "PENDING") { CommonFunctions.ShowMessageboot(this, "Waiver still pending, therefore payment cannot be generated now."); return; }
                    }
                    else
                    {
                        Session["REQUESTID_PAYMENT"] = txtRequestNo.Text.Trim();
                        Response.Redirect("FC0003R1V1.aspx");
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringBlockBill = "";
                stringWaiverApproved = "";
                stringWaiverApplications = "";
                stringBoID = "";
            }

        }

        protected void lnkbtnpending_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRequestNo.Text.Trim().Length > 0)
                {
                    Session["REQUESTID_PENDINGITEMS"] = txtRequestNo.Text.Trim();
                    Response.Redirect("FC0008R1V1.aspx");
                }
                else
                {
                    Session["REQUESTID_PENDINGITEMS"] = null;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        protected void lnkbtnAppoint_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRequestNo.Text.Trim().Length > 0)
                {
                    Session["REQUESTID_APPOINMENTS"] = txtRequestNo.Text.Trim();
                    Response.Redirect("FC0004R1V1.aspx");
                }
                else
                {
                    Session["REQUESTID_APPOINMENTS"] = null;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        protected void lnkbtnEnquiry_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRequestNo.Text.Trim().Length > 0)
                {
                    Session["REQUESTID_ENQUIRY"] = txtRequestNo.Text.Trim();
                    Response.Redirect("FC0005R1V1.aspx");
                }
                else
                {
                    Session["REQUESTID_ENQUIRY"] = null;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            string stringMRStatus = "";
            DataTable objDataTable = null;
            string stringBoID = CommonFunctions.GETBussinessEntity().ToString();
            bool boolstatus = true;
            string stringformid01 = "";
            string stringexp0121 = "";
            try
            {

                if (txtRequestNo.Text.Trim().Length > 0)
                {
                    if (txtMRStatus.Text.Trim().Length > 0)
                    {
                        stringMRStatus = txtMRStatus.Text.Trim();

                        if (stringMRStatus.Length > 0 && (stringMRStatus.ToUpper() == "PENDING RELEASE TO HIMS" || stringMRStatus.ToUpper() == "PENDING SUP VETTING" || stringMRStatus.ToUpper() == "PENDING FORWARDING" || stringMRStatus.ToUpper() == "PENDING COLLECT IN PERSON" || stringMRStatus.ToUpper() == "FORWARDED" || stringMRStatus.ToUpper() == "COLLECTED"))
                        {
                            boolstatus = false;
                            Session["REQUESTID_CANCELLATION"] = txtRequestNo.Text.Trim();
                            Response.Redirect("FC0006R1V1.aspx?LockFlag=TRUE");
                        }
                    }
                    if (boolstatus)
                    {
                        stringformid01 = "FC0001R1V1";
                        stringServiceType = "List21R1V1";
                        stringexp0121 = "And mrc.be_id= '" + stringBoID.ToString() + "' And mrc.Request_ID= '" + txtRequestNo.Text.Trim().ToString() + "'";

                        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid01, stringexp0121, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                        if (interrorcount == 0)
                        {
                            if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t21"] != null && objDatasetResult.Tables["t21"].Rows.Count > 0)
                            {
                                objDataTable = objDatasetResult.Tables["t21"];
                            }
                            if (objDataTable != null && objDataTable.Rows.Count > 0)
                            {
                                Session["REQUESTID_CANCELLATION"] = txtRequestNo.Text.Trim();
                                Response.Redirect("FC0006R1V1.aspx?LockFlag=TRUE");
                            }
                            else
                            {
                                Session["REQUESTID_CANCELLATION"] = txtRequestNo.Text.Trim();
                                Response.Redirect("FC0006R1V1.aspx");

                            }
                        }
                        else
                        {
                            Errorpopup(stringOutputResult);
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
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = "";
                stringMRStatus = "";
                objDataTable = null;
                stringBoID = "";
                boolstatus = true;
                stringformid01 = "";
                stringexp0121 = "";
            }
        }
         
        protected void lnkbtnViewMedical_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRequestNo.Text.Trim().Length > 0)
                {
                    Session["REQUESTID_VIEWMEDICAL"] = txtRequestNo.Text.Trim();
                    Response.Redirect("FC0010R1V1.aspx");
                }
                else
                {
                    Session["REQUESTID_VIEWMEDICAL"] = null;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        protected void lnkbtnrecalhistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRequestNo.Text.Trim().Length > 0)
                {
                    Session["REQUESTID_RECALHISTORY"] = txtRequestNo.Text.Trim();
                    Response.Redirect("FC0009R1V1.aspx");
                }
                else
                {
                    Session["REQUESTID_RECALHISTORY"] = null;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        #endregion


        #region Process Button

        #region ProcessCompleted  start
        protected void lnkbtnProcessCompletedTraccing_Click(object sender, EventArgs e)
        {
            string stringprocessname = "";
            try
            {
                if (sender != null)
                {
                    stringprocessname = ((LinkButton)sender).ToolTip;
                    if (stringprocessname.Length > 0)
                    {
                        ViewState["NEXTPROCESSNAME"] = stringprocessname;
                        txtProcessCompletedRemarks.Text = "";
                        Modelpopuperrorsuccess.Show();
                        UpdatePanelModal6success.Visible = true;
                    }
                    else
                    {
                        ViewState["NEXTPROCESSNAME"] = null;
                    }
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringprocessname = "";
            }
        }
        #endregion

        #region delay reason start
        protected void lnkbtnInsertDelayReason_Click(object sender, EventArgs e)
        {
            string stringprocessname = "";
            try
            {
                if (sender != null)
                {
                    stringprocessname = ((LinkButton)sender).ToolTip;
                    if (stringprocessname.Length > 0)
                    {
                        ViewState["DELAYREASONNAME"] = stringprocessname;
                        ddlDelayReason.ClearSelection();
                        LoadDelayReasons();
                        ModelpopupDelayReason.Show();
                        UpdatePanelDelayReason.Visible = true;
                    }
                    else
                    {
                        ViewState["DELAYREASONNAME"] = null;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringprocessname = "";
            }
        }
        private void LoadDelayReasons()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0008R1V1";
            string stringOrderBy = "mrdelas.ORDER_ID asc,mrdelas.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            try
            {
                ddlDelayReason.Items.Clear();
                stringServiceType = "List1R1V1";
                stringexp012 = "And mrdelas.be_id= '" + stringbeid + "' And mrdelas.delmark= 'N'";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t1"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        ddlDelayReason.DataTextField = "short_name";
                        ddlDelayReason.DataValueField = "dr_id";
                        ddlDelayReason.DataSource = objDataTable;
                        ddlDelayReason.DataBind();
                        ddlDelayReason.Items.Insert(0, new ListItem("", ""));
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
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                objDataTable = null;
                stringbeid = "";
                stringServiceType = "";
                stringexp012 = "";
            }

        }
        private bool ValidateDelayReasonControls()//fix
        {
            try
            {
                if (ddlDelayReason.SelectedItem != null && ddlDelayReason.Text.Trim().Length == 0)
                {
                    CommonFunctions.ShowMessageboot(this, "Delay Reason Field should not be empty,please enter value");
                    ddlDelayReason.Focus();
                    ModelpopupDelayReason.Show();
                    UpdatePanelDelayReason.Visible = true;
                    return false;
                }
                return true;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return false;
            }
        }
        protected void btnDelayReasonOK_Click(object sender, EventArgs e)
        {
            DataRow objdatarow = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FC1001R1V2";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            string stringRequestID = "";
            string stringprocessname = "";
            try
            {
                if (ValidateDelayReasonControls())
                {
                    ModelpopupDelayReason.Hide();
                    UpdatePanelDelayReason.Visible = false;


                    stringServiceType = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t2"].Rows.Count == 0)
                        {
                            objdatarow = objDatasetResult.Tables["t2"].NewRow();
                            objdatarow["be_id"] = stringbeid;
                            objdatarow["taskdr_id"] = System.Guid.NewGuid().ToString().ToUpper();
                            objdatarow["Request_ID"] = txtRequestNo.Text.Trim();
                            if (ddlDelayReason.SelectedItem != null)
                            {
                                objdatarow["DR_ID"] = ddlDelayReason.SelectedItem.Value;
                                objdatarow["short_name"] = ddlDelayReason.SelectedItem.Text;
                            }
                            if (ViewState["DELAYREASONNAME"] != null)
                            {
                                stringprocessname = ViewState["DELAYREASONNAME"].ToString();
                                objdatarow["long_name"] = ViewState["DELAYREASONNAME"].ToString();
                            }
                            objdatarow["trans_date"] = DateTime.Now;

                            CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                            objDatasetResult.Tables["t2"].Rows.Add(objdatarow);
                            objDatasetResult.Tables["t2"].Rows[0].RowState.ToString();

                            objDatasetResult = objDatasetResult.GetChanges();
                            stringServiceType = "OperationServiceDML";
                            objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                            if (intErrorCount == 0)
                            {
                                CommonFunctions.ShowMessageboot(this, "Delay Reason Added Successfully");
                                stringRequestID = txtRequestNo.Text.Trim().ToUpper();
                                LoadData(stringRequestID);
                            }
                            else
                            {
                                Errorpopup(stringOutputResult);
                            }


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
                objdatarow = null;
                interrorcount = 0;
                intTotalRecord = 0;
                objDatasetResult = null;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringbeid = "";
                stringServiceType = "";
                stringexp = "";
                stringRequestID = "";
                stringprocessname = "";
            }
        }
        protected void btnDelayReasoncancel_Click(object sender, EventArgs e)
        {
            try
            {
                ModelpopupDelayReason.Hide();
                UpdatePanelDelayReason.Visible = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        #endregion

        #region for Recall
        protected void lnkbtnRecallRequest_Click(object sender, EventArgs e)
        {
            string stringprocessname = "";
            try
            {
                hdfRecallcurreentStatus.Value = "";
                if (sender != null)
                {
                    stringprocessname = ((LinkButton)sender).ToolTip;
                    if (stringprocessname.Length > 0)
                    {
                        hdfRecallcurreentStatus.Value = stringprocessname;
                        txtRecallRemarks.Text = "";
                        ModelpopupRecall.Show();
                        UpdatePanelRecall.Visible = true;
                    }
                }


            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringprocessname = "";
            }
        }

        private bool ValidateRecallControls()//fix
        {
            try
            {
                if (txtRecallRemarks.Text.Trim().Length == 0)
                {
                    CommonFunctions.ShowMessageboot(this, "Remarks Field should not be empty,please enter value");
                    txtRecallRemarks.Focus();
                    ModelpopupRecall.Show();
                    UpdatePanelRecall.Visible = true;
                    return false;
                }
                return true;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return false;
            }
        }
        protected void btnrecallOK_Click(object sender, EventArgs e)
        {
            DataRow objdatarow = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FC1001R1V2";
            string stringOrderBy = "";
            string stringprocessname = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            string stringRequestID = "";
            try
            {
                //if (ValidateRecallControls())
                //{
                ModelpopupRecall.Hide();
                UpdatePanelRecall.Visible = false;


                stringServiceType = "DEFAULT";
                stringexp = "";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables["t3"].Rows.Count == 0)
                    {
                        objdatarow = objDatasetResult.Tables["t3"].NewRow();
                        objdatarow["be_id"] = stringbeid;
                        if (hdfRecallcurreentStatus != null && hdfRecallcurreentStatus.Value.Length > 0)
                        {
                            stringprocessname = hdfRecallcurreentStatus.Value.ToString().ToUpper();
                        }
                        objdatarow["AppsObj_ID"] = stringprocessname;
                        objdatarow["remarks_id"] = System.Guid.NewGuid().ToString().ToUpper();
                        objdatarow["Request_ID"] = txtRequestNo.Text.Trim();
                        objdatarow["MRP_ID"] = txtProcessType.Text.Trim();
                        objdatarow["remarks_desc"] = txtRecallRemarks.Text.Trim();
                        objdatarow["trans_date"] = DateTime.Now;
                        objdatarow["hrn_id"] = "";
                        CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                        objDatasetResult.Tables["t3"].Rows.Add(objdatarow);
                        objDatasetResult.Tables["t3"].Rows[0].RowState.ToString();

                        objDatasetResult = objDatasetResult.GetChanges();
                        stringServiceType = "OperationServiceDML";
                        objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                        if (intErrorCount == 0)
                        {
                            CommonFunctions.ShowMessageboot(this, "Recall Completed Successfully");
                            stringRequestID = txtRequestNo.Text.Trim().ToUpper();
                            LoadData(stringRequestID);
                        }
                        else
                        {
                            Errorpopup(stringOutputResult);
                        }


                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                }

                //}
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objdatarow = null;
                interrorcount = 0;
                intTotalRecord = 0;
                objDatasetResult = null;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                stringprocessname = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringbeid = "";
                stringServiceType = "";
                stringexp = "";
                stringRequestID = "";
            }

        }

        protected void btnrecallcancel_Click(object sender, EventArgs e)
        {
            try
            {
                ModelpopupRecall.Hide();
                UpdatePanelRecall.Visible = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        #endregion

        #region popup Event

        #region For Confirmprocess Function POPUP events

        protected void btnConfirmprocessStatus_Click(object sender, EventArgs e)
        {
            bool boolContinue = false;
            string stringTransSattus = "";
            DataTable objdatatablePendingItems = null;
            bool boolpendingItems = true;
            bool boolAssignDoctor = true;
            bool boolpendingreport = true;
            bool boolpaymentpendingItems = true;
            bool boolEnquirypendingItems = true;
            DataRow[] objdatarow = null;
            DataTable objdatatable = null;
            int intbalanceamt = 0; 
            string stringMRamt = "";
            string stringbalanceamt = "";
            string stringRequestID = "";
            try
            {
                //if (ValidateProcessControls())
                //{
                Modelpopuperrorsuccess.Hide();
                UpdatePanelModal6success.Visible = false;
                if (ViewState["NEXTPROCESSNAME"] != null)
                {
                    stringTransSattus = ViewState["NEXTPROCESSNAME"].ToString();
                }
                if (ViewState["NEXTPROCESSNAME"] != null && stringTransSattus != null && stringTransSattus.Trim().Length > 0)
                {
                    if ((stringTransSattus.ToUpper() == "PENDING DESPATCH" && txtBypassPenItems.Text.ToString().ToUpper() == "N") || stringTransSattus.ToUpper() == "PENDING FORWARDING")
                    {
                        objdatatablePendingItems = (DataTable)Session["PendingItemsList"];
                        if (objdatatablePendingItems != null && objdatatablePendingItems.Rows.Count > 0)
                        {
                            objdatarow = objdatatablePendingItems.Select("Pending_Status = 'PENDING'");
                            if (objdatarow != null && objdatarow.Length > 0)
                            {
                                boolpendingItems = false;
                            }
                        }
                    }
                    if (stringTransSattus.ToUpper() == "PENDING FORWARDING")
                    {
                        if (hdfmramount.Value.Length > 0)
                        {
                            stringMRamt = hdfmramount.Value.ToString();
                            if (stringMRamt.Length > 0)
                            {
                                decimal decimalmrAmount = 0;
                                if (stringMRamt != null && stringMRamt.Trim().Length > 0)
                                { decimal.TryParse(stringMRamt, out decimalmrAmount); }
                                if (hdfddlBlockBill.Value.Length > 0 && hdfddlBlockBill.Value.ToString() == "NO" && hdfddlWApp.Value.Length > 0 && hdfddlWApp.Value.ToString() == "YES" && (hdfddlWApproved.Value.Length > 0 && (hdfddlWApproved.Value.ToString() == "REJECTED" || hdfddlWApproved.Value.ToString() == "HALFWAIVER" || hdfddlWApproved.Value.ToString() == "PENDING")))
                                {
                                    if (decimalmrAmount > 0 && Session["LoadPaymentReceiptsGridFC0001"] == null)
                                    {
                                        boolpaymentpendingItems = false;
                                    }
                                    else
                                    {
                                        if (Session["LoadPaymentReceiptsGridFC0001"] != null)
                                        {
                                            objdatatable = (DataTable)Session["LoadPaymentReceiptsGridFC0001"];
                                            if (objdatatable != null && objdatatable.Rows.Count > 0)
                                            {
                                                objdatatable.DefaultView.Sort = "MODIFIED_ON asc";
                                                objdatatable = objdatatable.DefaultView.ToTable();

                                                stringbalanceamt = objdatatable.Rows[0]["BALANCE_AMT"].ToString();
                                                if (stringbalanceamt.Length > 0)
                                                {
                                                    intbalanceamt = 0;

                                                    double doubleValue = Convert.ToDouble(stringbalanceamt);
                                                    intbalanceamt = Convert.ToInt32(doubleValue);

                                                    if (intbalanceamt > 0)
                                                    {
                                                        boolpaymentpendingItems = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                boolpaymentpendingItems = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (Session["LoadEnquiryFC0001"] != null)
                        {
                            objdatatable = (DataTable)Session["LoadEnquiryFC0001"];
                            if (objdatatable != null && objdatatable.Rows.Count > 0)
                            {
                                objdatatable.DefaultView.Sort = "MODIFIED_ON asc";
                                objdatatable = objdatatable.DefaultView.ToTable();

                                objdatarow = objdatatable.Select("reference_1 = 'PENDING'");
                                if (objdatarow != null && objdatarow.Length > 0)
                                {
                                    boolEnquirypendingItems = false;
                                }
                            }
                        }

                    }

                    if (stringTransSattus.ToUpper() == "PENDING ASSIGNED")
                    {
                        if (LoadDocterandVerifiers(txtRequestNo.Text.Trim()))
                        {
                            boolAssignDoctor = true;
                        }
                        else
                        {
                            boolAssignDoctor = false;
                        }
                    }

                    if (stringTransSattus.ToUpper() == "PENDING REPORT")
                    {
                        boolpendingreport = false;
                        if (CheckDocterComplete(txtRequestNo.Text.Trim()))
                        {
                            boolpendingreport = true;
                        }
                    }
                    if (boolpaymentpendingItems)
                    {
                        if (boolpendingItems)
                        {
                            if (boolEnquirypendingItems)
                            {
                                if (boolAssignDoctor)
                                {
                                    if (boolpendingreport)
                                    {
                                        if (UpdateProcessStatus(stringTransSattus.Trim().ToUpper()))
                                        {
                                            boolContinue = true;
                                        }
                                        if (boolContinue)
                                        {
                                            stringRequestID = txtRequestNo.Text.Trim().ToUpper();
                                            LoadData(stringRequestID);
                                        }
                                    }
                                    else
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Please Complete the Doctor Status");
                                    }
                                }
                                else
                                {
                                    CommonFunctions.ShowMessageboot(this, "Please Assign Doctor");
                                }
                            }
                            else
                            {
                                CommonFunctions.ShowMessageboot(this, "Please Complete Enquiry Status");
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Please Complete Pending Item's");
                        }
                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "Please Complete Payment Details");
                    }
                }
                //}
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
              //  boolContinue = false;
                stringTransSattus = "";
                objdatatablePendingItems = null;
                //boolpendingItems = true;
                //boolAssignDoctor = true;
                //boolpendingreport = true;
                //boolpaymentpendingItems = true;
                //boolEnquirypendingItems = true;
                objdatarow = null;
                objdatatable = null;
                intbalanceamt = 0; 
                stringMRamt = "";
                stringbalanceamt = "";
                stringRequestID = "";

            }
        }

        protected void btnConfirmprocessClose_Click(object sender, EventArgs e)
        {
            try
            {
                Modelpopuperrorsuccess.Hide();
                UpdatePanelModal6success.Visible = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        private bool UpdateProcessStatus(string stringTransSattus)//fix
        {
            bool boolStatus = false;
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V3";
            string[] stringInputs = new string[2];
            try
            {

                if (ViewState["NEXTPROCESSNAME"] != null)
                {
                    stringTransSattus = ViewState["NEXTPROCESSNAME"].ToString();
                }
                if (ViewState["NEXTPROCESSNAME"] != null && stringTransSattus != null && stringTransSattus.Trim().Length > 0)
                {
                    if (Session["objDatasetlocaldeclaration"] != null)
                    {
                        objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                    }
                   
                    objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = stringformid;

                    stringInputs[0] = txtRequestNo.Text.Trim().ToUpper();
                    stringInputs[1] = txtProcessCompletedRemarks.Text.Trim();

                    objDatasetResult = CommonFunctions.UpdateMRRegistrationR1V1("UpdateMRRegistrationR1V1", stringInputs, stringformid, out interrorcount, out stringOutputResult);
                     
                    if (interrorcount == 0)
                    {
                        boolStatus = true;
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                        boolStatus = false;
                    }

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                //boolStatus = false;
                objDatasetResult = null;
                interrorcount = 0;
                stringOutputResult = null;
                stringformid = "";
            }
            return boolStatus;
        }
        private void ProcessTABControlProcesstype(String stringMRProcessID, String stringTYPE)
        {

            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0033R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            DataTable objDataTable = null;
            string stringExpression = "";
            string stringBoID = "";
            string stringMRStatus = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {

                pnlPendingTracing.Visible = false;
                pnlPendingDespatch.Visible = false;
                pnlPendingAssigned.Visible = false;
                pnlPendingReport.Visible = false;
                pnlPendingReleasetoHIMS.Visible = false;
                pnlSupervisorVetting.Visible = false;
                pnlforwarding.Visible = false;
                pnlPendingCollectInPerson.Visible = false;

                if (stringMRProcessID.Length > 0)
                {
                    if ((Session["LoadProcessTABControlProcesstype"] == null) || (stringTYPE.Length > 0 && stringTYPE != "LOAD"))
                    {
                        stringServiceType = "List1R1V1";
                        stringExpression = "And mrstprts.be_id= '" + stringBoID + "' And mrstprts.mrp_id= '" + stringMRProcessID + "' and mrstprts.delmark ='N' ";

                        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                        if (interrorcount == 0)
                        {
                            if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                            {
                                objDataTable = objDatasetResult.Tables["t1"];
                            }

                        }
                        else
                        {
                            Errorpopup(stringOutputResult);
                        }
                    }
                    else
                    {
                        if (HttpContext.Current.Session["LoadProcessTABControlProcesstype"] != null)
                        {
                            objDataTable = (DataTable)HttpContext.Current.Session["LoadProcessTABControlProcesstype"];
                        }
                    }


                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        objDataTable.DefaultView.Sort = "seq_id asc";
                        objDataTable = objDataTable.DefaultView.ToTable();

                        foreach (DataRow objDataRow in objDataTable.Rows)
                        {
                            stringMRStatus = objDataRow["TRANS_STATUS"].ToString();

                            if (stringMRStatus == "PENDING TRACING")
                            {
                                pnlPendingTracing.Visible = true;
                            }
                            else if (stringMRStatus == "PENDING DESPATCH")
                            {
                                pnlPendingDespatch.Visible = true;
                            }
                            else if (stringMRStatus == "PENDING ASSIGNED")
                            {
                                pnlPendingAssigned.Visible = true;
                            }
                            else if (stringMRStatus == "PENDING REPORT")
                            {
                                pnlPendingReport.Visible = true;
                            }
                            else if (stringMRStatus == "PENDING RELEASE TO HIMS")
                            {
                                pnlPendingReleasetoHIMS.Visible = true;
                            }
                            else if (stringMRStatus == "PENDING SUP VETTING")
                            {
                                pnlSupervisorVetting.Visible = true;
                            }
                            else if (stringMRStatus == "PENDING FORWARDING")
                            {
                                pnlforwarding.Visible = true;
                            }
                            else if (stringMRStatus == "PENDING COLLECT IN PERSON")
                            {
                                pnlPendingCollectInPerson.Visible = true;

                            }
                            //else if (stringMRStatus == "FORWARDED" || stringMRStatus == "COLLECTED")
                            //{
                            //    pnlForwarded.Visible = false;
                            //}
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
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = "";
                objDataTable = null;
                stringExpression = "";
                stringBoID = "";
                stringMRStatus = "";
            }
        }
        private void ProcessControlOverDueIndicator(String stringMRStatus, string stringDueduedays)
        {

            try
            {
                if (stringMRStatus.Length > 0)
                {
                    if (stringMRStatus == "PENDING TRACING")
                    {
                        if (stringDueduedays.Length > 0 && stringDueduedays == "RED")
                        {
                            imgbtnoverduewithoutdelayreasonTracing.Visible = true;
                        }
                        else if (stringDueduedays.Length > 0 && stringDueduedays == "BLUE")
                        {
                            imgbtnoverduewithdelayreasonTracing.Visible = true;
                        }
                    }
                    else if (stringMRStatus == "PENDING DESPATCH")
                    {
                        if (stringDueduedays.Length > 0 && stringDueduedays == "RED")
                        {
                            imgbtnoverduewithoutdelayreasoDespatch.Visible = true;
                        }
                        else if (stringDueduedays.Length > 0 && stringDueduedays == "BLUE")
                        {
                            imgbtnoverduewithdelayreasonDespatch.Visible = true;
                        }
                    }
                    else if (stringMRStatus == "PENDING ASSIGNED")
                    {
                        if (stringDueduedays.Length > 0 && stringDueduedays == "RED")
                        {
                            imgbtnoverduewithoutdelayreasonPendingAssigned.Visible = true;
                        }
                        else if (stringDueduedays.Length > 0 && stringDueduedays == "BLUE")
                        {
                            imgbtnoverduewithdelayreasonPendingAssigned.Visible = true;
                        }
                    }
                    else if (stringMRStatus == "PENDING REPORT")
                    {
                        if (stringDueduedays.Length > 0 && stringDueduedays == "RED")
                        {
                            imgbtnoverduewithoutdelayreasonPendingReport.Visible = true;
                        }
                        else if (stringDueduedays.Length > 0 && stringDueduedays == "BLUE")
                        {
                            imgbtnoverduewithdelayreasonReport.Visible = true;
                        }
                    }
                    else if (stringMRStatus == "PENDING RELEASE TO HIMS")
                    {
                        if (stringDueduedays.Length > 0 && stringDueduedays == "RED")
                        {
                            imgbtnoverduewithoutdelayreasonReleasetoHIMS.Visible = true;
                        }
                        else if (stringDueduedays.Length > 0 && stringDueduedays == "BLUE")
                        {
                            imgbtnoverduewithdelayreasonReleasetoHIMS.Visible = true;
                        }
                    }
                    else if (stringMRStatus == "PENDING SUP VETTING")
                    {
                        if (stringDueduedays.Length > 0 && stringDueduedays == "RED")
                        {
                            imgbtnoverduewithoutdelayreasonSupVetting.Visible = true;
                        }
                        else if (stringDueduedays.Length > 0 && stringDueduedays == "BLUE")
                        {
                            imgbtnoverduewithdelayreasonSupVetting.Visible = true;
                        }
                    }
                    else if (stringMRStatus == "PENDING FORWARDING")
                    {
                        if (stringDueduedays.Length > 0 && stringDueduedays == "RED")
                        {
                            imgbtnoverduewithoutdelayreasonforwarding.Visible = true;
                        }
                        else if (stringDueduedays.Length > 0 && stringDueduedays == "BLUE")
                        {
                            imgbtnoverduewithdelayreasonforwarding.Visible = true;
                        }
                    }
                    else if (stringMRStatus == "PENDING COLLECT IN PERSON")
                    {
                        if (stringDueduedays.Length > 0 && stringDueduedays == "RED")
                        {
                            imgbtnoverduewithoutdelayreasonCollectInPerson.Visible = true;
                        }
                        else if (stringDueduedays.Length > 0 && stringDueduedays == "BLUE")
                        {
                            imgbtnoverduewithdelayreasonCollectInPerson.Visible = true;
                        }

                    }

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private void ProcessControl(String stringMRStatus, string stringDeliverBy, DateTime? objDueDate)
        {
            try
            {
                if (stringMRStatus.Length > 0)
                {
                    pnlprocess.Visible = false;
                    pnlForwarded.Visible = false;
                    btnPendingTracing.Enabled = false;
                    btnPendingDespatch.Enabled = false;
                    btnPendingAssigned.Enabled = false;
                    btnPendingReport.Enabled = false;
                    btnPendingReleasetoHIMS.Enabled = false;
                    btnPendingSupVetting.Enabled = false;
                    btnPendingforwarding.Enabled = false;
                    btnPendingCollectInPerson.Enabled = false;

                    if (stringDeliverBy.Length > 0 && stringDeliverBy.ToUpper() == "INPERSON")
                    {
                        pnlPendingCollectInPerson.Visible = true;
                    }
                    else
                    {
                        pnlPendingCollectInPerson.Visible = false;
                    }

                    if (stringMRStatus == "PENDING TRACING")
                    {
                        pnlprocess.Visible = true;
                        btnPendingTracing.Enabled = true;
                        btnPendingDespatch.Enabled = false;
                        btnPendingAssigned.Enabled = false;
                        btnPendingReport.Enabled = false;
                        btnPendingReleasetoHIMS.Enabled = false;
                        btnPendingSupVetting.Enabled = false;
                        btnPendingforwarding.Enabled = false;
                        btnPendingCollectInPerson.Enabled = false;

                        if (objDueDate < DateTime.Now.Date)
                        {
                            btnPendingTracing.BackColor = Color.FromArgb(255, 77, 77);
                            btnPendingTracing.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            btnPendingTracing.BackColor = Color.FromArgb(42, 167, 237);
                            btnPendingTracing.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.BackColor = btnPendingforwarding.BackColor = btnPendingCollectInPerson.BackColor = Color.FromArgb(255, 255, 255);
                        btnPendingDespatch.ForeColor = btnPendingAssigned.ForeColor = btnPendingReport.ForeColor = btnPendingReleasetoHIMS.ForeColor = btnPendingSupVetting.ForeColor = btnPendingforwarding.ForeColor = btnPendingCollectInPerson.ForeColor = Color.FromArgb(57, 114, 121);
                    }
                    else if (stringMRStatus == "PENDING DESPATCH")
                    {
                        pnlprocess.Visible = true;
                        btnPendingTracing.Enabled = false;
                        btnPendingDespatch.Enabled = true;
                        btnPendingAssigned.Enabled = false;
                        btnPendingReport.Enabled = false;
                        btnPendingReleasetoHIMS.Enabled = false;
                        btnPendingSupVetting.Enabled = false;
                        btnPendingforwarding.Enabled = false;
                        btnPendingCollectInPerson.Enabled = false;

                        btnPendingTracing.BackColor = Color.FromArgb(186, 228,  252);
                        btnPendingTracing.ForeColor = Color.FromArgb(57, 114, 121);
                        if (objDueDate < DateTime.Now.Date)
                        {
                            btnPendingDespatch.BackColor = Color.FromArgb(255, 77, 77);
                            btnPendingDespatch.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            btnPendingDespatch.BackColor = Color.FromArgb(42, 167, 237);
                            btnPendingTracing.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        btnPendingAssigned.BackColor = btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.BackColor = btnPendingforwarding.BackColor = btnPendingCollectInPerson.BackColor = Color.FromArgb(255, 255, 255);
                        btnPendingAssigned.ForeColor = btnPendingReport.ForeColor = btnPendingReleasetoHIMS.ForeColor = btnPendingSupVetting.ForeColor = btnPendingforwarding.ForeColor = btnPendingCollectInPerson.ForeColor = Color.FromArgb(57, 114, 121);

                    }
                    else if (stringMRStatus == "PENDING ASSIGNED")
                    {
                        pnlprocess.Visible = true;
                        btnPendingTracing.Enabled = false;
                        btnPendingDespatch.Enabled = false;
                        btnPendingAssigned.Enabled = true;
                        btnPendingReport.Enabled = false;
                        btnPendingReleasetoHIMS.Enabled = false;
                        btnPendingSupVetting.Enabled = false;
                        btnPendingforwarding.Enabled = false;
                        btnPendingCollectInPerson.Enabled = false;

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = Color.FromArgb(186, 228,  252);
                        btnPendingTracing.ForeColor = btnPendingDespatch.ForeColor = Color.FromArgb(57, 114, 121);
                        if (objDueDate < DateTime.Now.Date)
                        {
                            btnPendingAssigned.BackColor = Color.FromArgb(255, 77, 77);
                            btnPendingAssigned.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            btnPendingAssigned.BackColor = Color.FromArgb(42, 167, 237);
                            btnPendingAssigned.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.BackColor = btnPendingforwarding.BackColor = btnPendingCollectInPerson.BackColor = Color.FromArgb(255, 255, 255);
                        btnPendingReport.ForeColor = btnPendingReleasetoHIMS.ForeColor = btnPendingSupVetting.ForeColor = btnPendingforwarding.ForeColor = btnPendingCollectInPerson.ForeColor = Color.FromArgb(57, 114, 121);

                    }
                    else if (stringMRStatus == "PENDING REPORT")
                    {
                        pnlprocess.Visible = true;
                        btnPendingTracing.Enabled = false;
                        btnPendingDespatch.Enabled = false;
                        btnPendingAssigned.Enabled = false;
                        btnPendingReport.Enabled = true;
                        btnPendingReleasetoHIMS.Enabled = false;
                        btnPendingSupVetting.Enabled = false;
                        btnPendingforwarding.Enabled = false;
                        btnPendingCollectInPerson.Enabled = false;

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = Color.FromArgb(186, 228,  252);
                        btnPendingTracing.ForeColor = btnPendingDespatch.ForeColor = btnPendingAssigned.ForeColor = Color.FromArgb(57, 114, 121);
                        if (objDueDate < DateTime.Now.Date)
                        {
                            btnPendingReport.BackColor = Color.FromArgb(255, 77, 77);
                            btnPendingReport.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            btnPendingReport.BackColor = Color.FromArgb(42, 167, 237);
                            btnPendingReport.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.BackColor = btnPendingforwarding.BackColor = btnPendingCollectInPerson.BackColor = Color.FromArgb(255, 255, 255);
                        btnPendingReleasetoHIMS.ForeColor = btnPendingSupVetting.ForeColor = btnPendingforwarding.ForeColor = btnPendingCollectInPerson.ForeColor = Color.FromArgb(57, 114, 121);

                    }
                    else if (stringMRStatus == "PENDING RELEASE TO HIMS")
                    {
                        pnlprocess.Visible = true;
                        btnPendingTracing.Enabled = false;
                        btnPendingDespatch.Enabled = false;
                        btnPendingAssigned.Enabled = false;
                        btnPendingReport.Enabled = false;
                        btnPendingReleasetoHIMS.Enabled = true;
                        btnPendingSupVetting.Enabled = false;
                        btnPendingforwarding.Enabled = false;
                        btnPendingCollectInPerson.Enabled = false;

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = Color.FromArgb(186, 228,  252);
                        btnPendingTracing.ForeColor = btnPendingDespatch.ForeColor = btnPendingAssigned.ForeColor = btnPendingReport.ForeColor = Color.FromArgb(57, 114, 121);
                        if (objDueDate < DateTime.Now.Date)
                        {
                            btnPendingReleasetoHIMS.BackColor = Color.FromArgb(255, 77, 77);
                            btnPendingReleasetoHIMS.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            btnPendingReleasetoHIMS.BackColor = Color.FromArgb(42, 167, 237);
                            btnPendingReleasetoHIMS.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        btnPendingSupVetting.BackColor = btnPendingforwarding.BackColor = btnPendingCollectInPerson.BackColor = Color.FromArgb(255, 255, 255);
                        btnPendingSupVetting.ForeColor = btnPendingforwarding.ForeColor = btnPendingCollectInPerson.ForeColor = Color.FromArgb(57, 114, 121);
                    }
                    else if (stringMRStatus == "PENDING SUP VETTING")
                    {
                        pnlprocess.Visible = true;
                        btnPendingTracing.Enabled = false;
                        btnPendingDespatch.Enabled = false;
                        btnPendingAssigned.Enabled = false;
                        btnPendingReport.Enabled = false;
                        btnPendingReleasetoHIMS.Enabled = false;
                        btnPendingSupVetting.Enabled = true;
                        btnPendingforwarding.Enabled = false;
                        btnPendingCollectInPerson.Enabled = false;

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = Color.FromArgb(186, 228,  252);
                        btnPendingTracing.ForeColor = btnPendingDespatch.ForeColor = btnPendingAssigned.ForeColor = btnPendingReport.ForeColor = btnPendingReleasetoHIMS.ForeColor = Color.FromArgb(57, 114, 121);
                        if (objDueDate < DateTime.Now.Date)
                        {
                            btnPendingSupVetting.BackColor = Color.FromArgb(255, 77, 77);
                            btnPendingSupVetting.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            btnPendingSupVetting.BackColor = Color.FromArgb(42, 167, 237);
                            btnPendingSupVetting.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        btnPendingforwarding.BackColor = btnPendingCollectInPerson.BackColor = Color.FromArgb(255, 255, 255);
                        btnPendingforwarding.ForeColor = btnPendingCollectInPerson.ForeColor = Color.FromArgb(57, 114, 121);
                    }
                    else if (stringMRStatus == "PENDING FORWARDING")
                    {
                        pnlprocess.Visible = true;
                        btnPendingTracing.Enabled = false;
                        btnPendingDespatch.Enabled = false;
                        btnPendingAssigned.Enabled = false;
                        btnPendingReport.Enabled = false;
                        btnPendingReleasetoHIMS.Enabled = false;
                        btnPendingSupVetting.Enabled = false;
                        btnPendingforwarding.Enabled = true;
                        btnPendingCollectInPerson.Enabled = false;

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.BackColor = Color.FromArgb(186, 228,  252);
                        btnPendingTracing.ForeColor = btnPendingDespatch.ForeColor = btnPendingAssigned.ForeColor = btnPendingReport.ForeColor = btnPendingReleasetoHIMS.ForeColor = btnPendingSupVetting.ForeColor = Color.FromArgb(57, 114, 121);
                        if (objDueDate < DateTime.Now.Date)
                        {
                            btnPendingforwarding.BackColor = Color.FromArgb(255, 77, 77);
                            btnPendingforwarding.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            btnPendingforwarding.BackColor = Color.FromArgb(42, 167, 237);
                            btnPendingforwarding.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        btnPendingCollectInPerson.BackColor = Color.FromArgb(255, 255, 255);
                        btnPendingCollectInPerson.ForeColor = Color.FromArgb(57, 114, 121);
                    }
                    else if (stringMRStatus == "PENDING COLLECT IN PERSON")
                    {
                        pnlprocess.Visible = true;
                        btnPendingTracing.Enabled = false;
                        btnPendingDespatch.Enabled = false;
                        btnPendingAssigned.Enabled = false;
                        btnPendingReport.Enabled = false;
                        btnPendingReleasetoHIMS.Enabled = false;
                        btnPendingSupVetting.Enabled = false;
                        btnPendingforwarding.Enabled = false;
                        btnPendingCollectInPerson.Enabled = true;

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.BackColor = btnPendingforwarding.BackColor = Color.FromArgb(186, 228,  252);
                        btnPendingTracing.ForeColor = btnPendingDespatch.ForeColor = btnPendingAssigned.ForeColor = btnPendingReport.ForeColor = btnPendingReleasetoHIMS.ForeColor = btnPendingSupVetting.ForeColor = btnPendingforwarding.ForeColor = Color.FromArgb(57, 114, 121);
                        if (objDueDate < DateTime.Now.Date)
                        {
                            btnPendingCollectInPerson.BackColor = Color.FromArgb(255, 77, 77);
                            btnPendingCollectInPerson.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            btnPendingCollectInPerson.BackColor = Color.FromArgb(42, 167, 237);
                            btnPendingCollectInPerson.ForeColor = Color.FromArgb(255, 255, 255);
                        }

                    }
                    else if (stringMRStatus == "FORWARDED" || stringMRStatus == "COLLECTED")
                    {
                        pnlprocess.Visible = true;
                        pnlForwarded.Visible = true;
                        if (stringMRStatus == "FORWARDED")
                        {
                            btnForwarded.Text = "Forwarded";
                        }
                        else if (stringMRStatus == "COLLECTED")
                        {
                            btnForwarded.Text = "Collected";
                        }

                        btnPendingTracing.Enabled = false;
                        btnPendingDespatch.Enabled = false;
                        btnPendingAssigned.Enabled = false;
                        btnPendingReport.Enabled = false;
                        btnPendingReleasetoHIMS.Enabled = false;
                        btnPendingSupVetting.Enabled = false;
                        btnPendingforwarding.Enabled = false;
                        btnPendingCollectInPerson.Enabled = false;

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.BackColor = btnPendingforwarding.BackColor = btnPendingCollectInPerson.BackColor = Color.FromArgb(186, 228,  252);
                        btnPendingTracing.ForeColor = btnPendingDespatch.ForeColor = btnPendingAssigned.ForeColor = btnPendingReport.ForeColor = btnPendingReleasetoHIMS.ForeColor = btnPendingSupVetting.ForeColor = btnPendingforwarding.ForeColor = btnPendingCollectInPerson.ForeColor = Color.FromArgb(57, 114, 121);
                    }

                }
                else
                {
                    pnlprocess.Visible = false;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private void LoadData(String stringRequestID)
        {
            DataTable objDataTable = null;
            DataRow objDataRow = null;
            try
            {
                objDataTable = GetRequestDetails(stringRequestID);
                if (objDataTable != null && objDataTable.Rows.Count > 0)
                {
                    objDataRow = objDataTable.Rows[0];

                    LoadProcesstabData(objDataRow);

                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDataTable = null;
                objDataRow = null;
            }
        }

        private void LoadProcesstabData(DataRow objDataRow)
        {
            DateTime objDueDate01 = new DateTime();
            string stringDeliverBy = "";
            string stringduedte = "";
            string stringProcessID = "";
            try
            {
                txtRequestNo.Text = objDataRow["request_id"].ToString();
                txtProcessType.Text = objDataRow["mrp_id"].ToString();
                txtBypassPenItems.Text = objDataRow["Bypass_Pen_Items"].ToString();
                txtMRStatus.Text = objDataRow["MR_STATUS"].ToString();
                txtBlockBill.Text = objDataRow["block_billing"].ToString();
                txtWApproved.Text = objDataRow["WAIVER_APPROVED"].ToString();
                txtWApp.Text = objDataRow["WAIVER_STATUS_1"].ToString();
                stringDeliverBy = objDataRow["delmod_id"].ToString();

                if (objDataRow["end_date"] != null && objDataRow["end_date"].ToString().Trim().Length > 0)
                {
                    stringduedte = Convert.ToDateTime(objDataRow["end_date"]).ToString("dd-MM-yyyy");
                    objDueDate01 = CommonFunctions.ConvertToDateTime(stringduedte, "dd-MM-yyyy");
                    ////objDueDate = objDueDate01.AddDays(-1);
                }
                stringProcessID = objDataRow["MRP_ID"].ToString();
                LoadProcessHistory(txtRequestNo.Text.Trim(), "NONLOAD");
                ProcessTABControlProcesstype(stringProcessID, "NONLOAD");
                ProcessControl(txtMRStatus.Text.Trim(), stringDeliverBy, objDueDate01);

                if (objDataRow["EMR"].ToString().ToUpper() == "Y")
                {
                    pnlPendingTracing.Visible = false;
                    lnkbtnRecallRequest2.Visible = false;

                }
                else if (objDataRow["EMR"].ToString().ToUpper() == "N")
                {
                    pnlPendingTracing.Visible = true;
                    lnkbtnRecallRequest2.Visible = true;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringDeliverBy = "";
                stringduedte = "";
                stringProcessID = "";
            }
        }
        private void LoadProcessHistory(string stringRequestID, string stringTYPE)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0002R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            DataTable objDataTable = null;
            string stringExpression = "";
            string stringBoID = "";
            string stringDueduedays = "";
            string stringMRSTSTUS = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (stringRequestID.Trim().Length > 0)
                {
                    if ((Session["LoadProcessHistoryFC0001"] == null) || (stringTYPE.Length > 0 && stringTYPE != "LOAD"))
                    {
                        stringServiceType = "List2R1V1";
                        stringExpression = "And mrpdt.be_id= '" + stringBoID + "' And mrpdt.request_id= '" + stringRequestID + "' ";

                        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                        if (interrorcount == 0)
                        {
                            if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                            {
                                objDataTable = objDatasetResult.Tables["t2"];
                            }

                        }
                        else
                        {
                            Errorpopup(stringOutputResult);
                        }
                    }
                    else
                    {
                        if (HttpContext.Current.Session["LoadProcessHistoryFC0001"] != null)
                        {
                            objDataTable = (DataTable)HttpContext.Current.Session["LoadProcessHistoryFC0001"];
                        }
                    }


                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow objDataRow in objDataTable.Rows)
                        {
                            stringDueduedays = objDataRow["reference_3"].ToString();
                            stringMRSTSTUS = objDataRow["REFERENCE_2"].ToString();
                            ProcessControlOverDueIndicator(stringMRSTSTUS, stringDueduedays);
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
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = "";
                objDataTable = null;
                stringExpression = "";
                stringBoID = "";
                stringDueduedays = "";
                stringMRSTSTUS = "";
            }
        }
        private bool LoadDocterandVerifiers(string stringRequestID)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V3";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringBoID = "";
            string stringServiceType = "";
            string stringExpression = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (stringRequestID.Length > 0)
                {
                    stringServiceType = "List2R1V1";
                    stringExpression = " and mrasdoc.be_id='" + stringBoID + "' and mrasdoc.request_id='" + stringRequestID + "'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                        return false;
                    }
                }

                return false;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return false;
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringBoID = "";
                stringServiceType = "";
                stringExpression = "";
            }
        }
        private DataTable GetRequestDetails(string stringReqID)//fixed
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringServiceType1 = "";
            string stringexp012 = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                if (stringReqID != null && stringReqID.Trim().Length > 0)
                {
                    stringServiceType1 = "List5R1V1";
                    stringexp012 = "And mrreg.be_id= '" + stringbeid + "'  And mrreg.request_id= '" + stringReqID.ToString() + "'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t5"] != null && objDatasetResult.Tables["t5"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t5"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            return objDataTable;
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
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                objDataTable = null;
                stringServiceType1 = "";
                stringexp012 = "";
                stringbeid = "";
            }
            return null;
        }


        private bool CheckDocterComplete(string stringRequestID)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V3";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringBoID = "";
            string stringServiceType = "";
            string stringExpression = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (stringRequestID.Length > 0)
                {
                    stringServiceType = "List2R1V1";
                    stringExpression = " and mrasdoc.be_id='" + stringBoID + "' and mrasdoc.request_id='" + stringRequestID + "'and mrasdoc.verify_ref='DOCTOR' and mrasdoc.status ='COMPLETED'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                        return false;
                    }
                }

                return false;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return false;
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringBoID = "";
                stringServiceType = "";
                stringExpression = "";
            }
        }
        #endregion

        #endregion

        #endregion
    }
}