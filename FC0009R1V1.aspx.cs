using CLUSTER_MRTS.CommonFunction;
using G10CertificateValidation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FC0009R1V1 : System.Web.UI.Page
    {
        public DataSet objDatasetAppsVariables;
        public int intpageIndex = 0;
        public int intrecFrom = 0;
        public int intrecTo = 0;

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)//fix
        {
            string stringRequestID = "";
            string stringformId = "";
            try
            {
                
                if (CommonFunctions.IsActive())
                {
                    if (Session["REQUESTID_RECALHISTORY"] != null)
                    {
                        stringRequestID = Session["REQUESTID_RECALHISTORY"].ToString();
                        Session["REQUESTID_RECALHISTORY"] = stringRequestID;
                    }

                    stringformId = "FC0009R1V1gridviewpagesize";
                    intrecTo = CommonFunctions.GridViewPagesize(stringformId);

                    if (!IsPostBack)
                    {
                        CommonFunctions.HeaderName(this, "FC0009R1V1");
                        VerifyAccessRights(); 
                        hdfmramount.Value = "";
                        hdfddlBlockBill.Value = "";
                        hdfddlWApp.Value = "";
                        hdfddlWApproved.Value = "";
                        hdfRecallcurreentStatus.Value = "";
                        ResetVariables();
                        LoadRegRequestInfo(stringRequestID); 
                        LoadAllDATA(stringRequestID);
                        LoadDataRecallHistory(stringRequestID);
                        LoadDelarReasonGrid(stringRequestID); 
                    }
                    else
                    { 
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

        }
        private void LoadAllDATA(string striongREQID)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "";
            string stringOrderBy = "";
            int intFromRecord = 0;
            string stringServiceType = "List1R1V1";
            int intToRecord = int.MaxValue;
            DataTable objdatatablerequest = null;
            try
            {
                if (striongREQID.Contains("DRAFT"))
                {
                    stringformid = "FC0001R1V5";
                }
                else
                {
                    stringformid = "FC0001R1V2";
                }
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, striongREQID, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[10] != null && objDatasetResult.Tables[10].Rows.Count > 0)
                        {
                            objdatatablerequest = objDatasetResult.Tables[10];
                            Session["defauledviewstatus"] = objdatatablerequest;
                        }
                        else
                        {
                            Session["defauledviewstatuss"] = null;
                        }
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t11"] != null && objDatasetResult.Tables["t11"].Rows.Count > 0)
                        {
                            objdatatablerequest = objDatasetResult.Tables["t11"];
                            Session["Cancelledrecord"] = objdatatablerequest;
                        }
                        else
                        {
                            Session["Cancelledrecord"] = null;
                        }
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
                stringServiceType = null;
                objdatatablerequest = null;
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
                stringComponent[0] = "FC0009R1V1";
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


                stringComponent = new string[1];
                stringComponent[0] = "FC0004R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        lnkbtnAppoint.Enabled = true;
                    }
                    else
                    {
                        lnkbtnAppoint.Enabled = false;
                    }
                }

                stringComponent = new string[1];
                stringComponent[0] = "FC0003R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        lnkbtnpayment.Enabled = true;
                    }
                    else
                    {
                        lnkbtnpayment.Enabled = false;
                    }
                }
                stringComponent = new string[1];
                stringComponent[0] = "FC0006R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        lnkbtnCancel.Enabled = true;
                    }
                    else
                    {
                        lnkbtnCancel.Enabled = false;
                    }
                }
                stringComponent = new string[1];
                stringComponent[0] = "FC0010R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        lnkbtnViewMedical.Enabled = true;
                    }
                    else
                    {
                        lnkbtnViewMedical.Enabled = false;
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
                        if (objDataRow["Export"].ToString().ToUpper() == "ENABLED")
                        {
                            imgBtnPrint.Enabled = true;
                        }
                        else
                        {
                            imgBtnPrint.Enabled = false;
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
                stringstatus = null;
                stringOutputResult = null;
                stringComponent = null;
            }
        }
        #endregion

        #region Image Button


        protected void imgBtnNew_Click(object sender, ImageClickEventArgs e)//fix
        { 
        }

        protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)//fix
        { 
        }
        protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)//fix
        {
        }

        protected void imgBtnSecurity_Click(object sender, ImageClickEventArgs e)//fix
        {
        }

        private void Errorpopup(string[] stringOutputResult)//fix
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
                CommonFunctions.HandleException(objException);
            }
        }
         
        #endregion

        #region Link Button

        protected void LkBtnBack_Click(object sender, EventArgs e)//fix
        {
            try
            {
                Response.Redirect("FC0001R1V1.aspx?RequestID=" + txtRequestNo.Text, true);
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            
        }

        #endregion

        

        #region Gridview

       
        private void ResetVariables()//fix
        {
            Session["stringDMLIndicator"] = "I";
            Session["stringSortDirection"] = "ASC";
            Session["stringSortExpression"] = "";
            Session["stringFormID"] = "FC0004R1V1";
            Session["stringFormName"] = "MR PENDING ITEMS";
        }

        #endregion

        #region Methods/Functions
        private void LoadRegRequestInfo(string stringRequestNo)//fix
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
            string stringServiceType1 = "";
            string stringexp012 = "";
            DataRow objDataRow = null;
            string stringStatus = "";
            string stringMRamt = ""; 
            string stringDelMode = "";
            string stringUserID = "";
            string stringcancellstatus = ""; ViewState["DEPTOU"] = null;
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
                            if (objDataTable != null && objDataTable.Rows.Count > 0)
                            {
                                objDataRow = objDataTable.Rows[0];
                                ViewState["DEPTOU"] = objDataRow["DEPT_ID"].ToString();
                                LoadProcesstabData(objDataRow);
                                ReportRelatedValues(objDataRow);
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
                                //task detail
                                stringDelMode = objDataRow["delmod_id"].ToString();
                                txtDelToID.Text = stringDelMode;
                                txtReqEmail.Text = objDataRow["Email"].ToString();
                                txttaskname.Text = objDataRow["MR_STATUS"].ToString();
                                if (objDataRow["DUE_DATE"] != null && objDataRow["DUE_DATE"].ToString().Trim().Length > 0)
                                { txtLastRemainderDate.Text = Convert.ToDateTime(objDataRow["DUE_DATE"]).ToString("dd-MM-yyyy"); }

                                if (objDataRow["START_DATE"] != null && objDataRow["START_DATE"].ToString().Trim().Length > 0)
                                { txtTaskStartDate.Text = Convert.ToDateTime(objDataRow["START_DATE"]).ToString("dd-MM-yyyy"); }

                                if (objDataRow["END_DATE"] != null && objDataRow["END_DATE"].ToString().Trim().Length > 0)
                                { txtTaskDueDate.Text = Convert.ToDateTime(objDataRow["END_DATE"]).ToString("dd-MM-yyyy"); }

                                txtpatnamemrn.Text = objDataRow["patient_short_name"].ToString() + " [" + objDataRow["hrn_id"].ToString() + "] ";
                                stringUserID = objDataRow["CREATED_BY"].ToString();
                                stringcancellstatus = objDataRow["sup_status"].ToString();
                                if (stringStatus.Trim().ToUpper() == "FORWARDED" || stringStatus.Trim().ToUpper() == "COLLECTED" || stringStatus.Trim().ToUpper() == "CANCELLED" || stringcancellstatus.ToUpper() == "PENDING")
                                { ControlsEnabledByProStatus("CANCELLED"); }
                                else { }


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
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord =0;
                objDataTable = null;
                stringbeid = null;
                stringServiceType1 = null;
                stringexp012 = null;
                objDataRow = null;
                stringStatus = null;
                stringMRamt = null; 
                stringDelMode = null;
                stringUserID = null;
                stringcancellstatus = null;
            }
        }
        private void ControlsEnabledByProStatus(string stringStatus)//fixed
        {
            switch (stringStatus)
            {
                case "CANCELLED":
                    { 
                        imgbtnSave.Enabled = false; 

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

                        break;
                    }
            }
        }

        private void LoadDataRecallHistory(string stringSequenceID)//fix
        { 
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC1001R1V2";
            string stringOrderBy = "rrh.modified_on desc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            DataTable objDataTable = null;
            string stringExpression = "";
            string stringBoID = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            } 
            try
            {
                if (stringSequenceID != null && stringSequenceID.Trim().Length > 0)
                {
                    stringServiceType = "List1R1V1";

                    stringExpression = "and rrh.be_id='" + stringBoID + "' and rrh.request_id='" + stringSequenceID + "' ";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t1"];
                            ViewState["SortTableRECALL"] = objDataTable;
                            gvlistrecall.DataSource = objDataTable;
                            gvlistrecall.DataBind();
                            TotalrecallCount.InnerText = objDataTable.Rows.Count.ToString();
                        }
                        else
                        {
                            ViewState["SortTableRECALL"] = null;
                            gvlistrecall.DataSource = null;
                            gvlistrecall.DataBind();
                            TotalrecallCount.InnerText = "0";
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
                objDataTable = null;
                stringExpression = null;
                stringBoID = null;
            }

        }

        private void LoadDelarReasonGrid(string stringRequestID)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC1001R1V2";
            string stringOrderBy = "mrpd.modified_on desc";
           
            string stringServiceType = "";
            DataTable objDataTable = null;
            string stringExpression = "";
            string stringBoID = "";
            int intFromRecord = 0;
            int intToRecord = 0;

            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (stringRequestID.Trim().Length > 0)
                {
                    stringServiceType = "List2R1V1";
                    stringExpression = "and mrpd.be_id='" + stringBoID + "' and mrpd.request_id='" + stringRequestID + "' ";
                    intFromRecord = intrecFrom;
                    intToRecord = intrecTo;

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    PopulatePager(intTotalRecord, intpageIndex);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t2"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            ViewState["SortTableDelarReason"] = objDataTable;
                            gvList.DataSource = objDataTable;
                            gvList.DataBind();
                            lblTotalRecords.InnerText = objDataTable.Rows.Count.ToString();
                        }
                        else
                        {
                            ViewState["SortTableDelarReason"] = null;
                            gvList.DataSource = null;
                            gvList.DataBind();
                            lblTotalRecords.InnerText = "0";
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
                stringServiceType = null;
                objDataTable = null;
                stringExpression = null;
                stringBoID = null;
                intFromRecord = 0;
                intToRecord = 0;
            }

        }
         
           
        #endregion

        private void PopulatePager(int recordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5; 
                double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(15));
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
                //if(intpageIndex==0)
                //{
                //   intpageIndex = 1;
                //}
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
                CommonFunctions.HandleException(objException);
            }
        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            int intrecFrom1 = 0;
            string stringRequestID = "";
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
                    intrecTo = CommonFunctions.GridViewPagesize("FC0009R1V1gridviewpagesize");
                }
                else
                {
                    intrecFrom1 = (intpageIndex * intrecTo) - intrecTo;
                    intrecFrom = intrecFrom1 + 1;
                    intrecTo = intrecFrom1 + CommonFunctions.GridViewPagesize("FC0009R1V1gridviewpagesize");
                }

                if (Session["REQUESTID_RECALHISTORY"] != null)
                {
                    stringRequestID = Session["REQUESTID_RECALHISTORY"].ToString();
                    LoadDelarReasonGrid(stringRequestID);
                }
              
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                intrecFrom1 = 0;
                stringRequestID = null;
            }
        }

        protected void LkBtnBack_Click1(object sender, EventArgs e)
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

        protected void lnkbtnSort_Click(object sender, EventArgs e)
        {
            DataSet objDataSetSort = null;
            DataSet objDataSetSort1 = null;
            objDataSetSort1 = new DataSet();
            string stringColumnName = "";
            DataTable objDataTableSort = null;
            try
            {
                if (sender != null)
                {
                    stringColumnName = ((LinkButton)sender).CommandArgument;
                    if (stringColumnName != null && stringColumnName.Trim().Length > 0)
                    {
                        Session["stringSortExpression"] = stringColumnName;
                        if (Session["stringSortDirection"].ToString() == "ASC") { Session["stringSortDirection"] = "DESC"; }
                        else { Session["stringSortDirection"] = "ASC"; }

                        objDataSetSort = (DataSet)ViewState["SortTableDelarReason"];
                        if (objDataSetSort != null)
                        {
                            objDataTableSort = objDataSetSort.Tables["t2"];
                        }
                       
                        if (objDataTableSort != null && objDataTableSort.Rows.Count > 0)
                        {
                            objDataTableSort.DefaultView.Sort = stringColumnName + " " + Session["stringSortDirection"].ToString();
                            objDataTableSort = objDataTableSort.DefaultView.ToTable();

                            gvList.DataSource = objDataTableSort;
                            gvList.DataBind(); 
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
                objDataSetSort = null;
                objDataSetSort1 = null;
                stringColumnName = null;
                objDataTableSort = null;
            }
        }
        protected void lnkbtnSortrecall_Click(object sender, EventArgs e)
        {
            DataSet objDataSetSort = null;
            DataSet objDataSetSort1 = null;
            objDataSetSort1 = new DataSet();
            string stringColumnName = "";
            DataTable objDataTableSort = null;
            try
            {
                if (sender != null)
                {
                    stringColumnName = ((LinkButton)sender).CommandArgument;
                    if (stringColumnName != null && stringColumnName.Trim().Length > 0)
                    {
                        Session["stringSortExpression"] = stringColumnName;
                        if (Session["stringSortDirection"].ToString() == "ASC") { Session["stringSortDirection"] = "DESC"; }
                        else { Session["stringSortDirection"] = "ASC"; }

                        objDataSetSort = (DataSet)ViewState["SortTableRECALL"];
                        if (objDataSetSort != null)
                        {
                            objDataTableSort = objDataSetSort.Tables["t1"];
                        }
                        if (objDataTableSort != null && objDataTableSort.Rows.Count > 0)
                        {
                            objDataTableSort.DefaultView.Sort = stringColumnName + " " + Session["stringSortDirection"].ToString();
                            objDataTableSort = objDataTableSort.DefaultView.ToTable();

                            gvlistrecall.DataSource = objDataTableSort;
                            gvlistrecall.DataBind();
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
                objDataSetSort = null;
                objDataSetSort1 = null;
                stringColumnName = null;
                objDataTableSort = null;
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRow objDataRow = null;
            DataRowView objDRV = null;
            string stringSort = string.Empty;
            string stringdelayreason = "";
            string stringTaskname = "";
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null) { return; }

                objDRV = ((DataRowView)objGridViewRow.DataItem);
                objDataRow = objDRV.Row;
                if (objDataRow != null)
                {
                    stringdelayreason = objDataRow["short_name"].ToString();
                    stringTaskname = objDataRow["long_name"].ToString();
                    if (stringdelayreason != null && stringdelayreason.Trim().Length > 0)
                    {
                        ((Label)e.Row.FindControl("lbldelayreason")).Text = stringdelayreason;
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
                objDRV = null;
                stringSort = null;
                stringdelayreason = null;
                stringTaskname = null;
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
                        CommonFunctions.ShowMessageboot(this, "Receipt cannot be generated for block billing MR");
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
                stringBlockBill = null;
                stringWaiverApproved = null;
                stringWaiverApplications = null;
                stringBoID = null;
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
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = null;
                stringMRStatus = null;
                objDataTable = null;
                stringformid01 = null;
                stringexp0121 = null;
            }
        }

        protected void lnkbtnRemark_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRequestNo.Text.Trim().Length > 0)
                {
                    Session["REQUESTID_REMARKS"] = txtRequestNo.Text.Trim();
                    Response.Redirect("FC0007R1V1.aspx");
                }
                else
                {
                    Session["REQUESTID_REMARKS"] = null;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
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
        #endregion


        #region Process Button

        #region ProcessCompleted  start
        private DataTable checkverifier()
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
            string stringBoID = "";
            string stringCategory = "";

            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            DataTable objDataTableAddAttachments = new DataTable();
            DataSet objdataset = new DataSet();
            string[] stringOutResult = new string[3];
            Session["Documentattach"] = null;
            string stringServiceType = "";
            string stringExpression = "";
            try
            {
                if (Session["stringCategory"] != null)
                {
                    stringCategory = Session["stringCategory"].ToString();
                }
                stringServiceType = "List18R1V1";
                stringExpression = " and dach.be_id='" + stringBoID + "' and dach.trans_id='" + txtRequestNo.Text.Trim() + "' ";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t18"] != null && objDatasetResult.Tables["t18"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t18"];
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                }
                return objDataTable;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return null;
            }
        }
        protected void lnkbtnProcessCompletedTraccing_Click(object sender, EventArgs e)
        {
            DataTable objDataTableAddReports = null;
            DataTable objDataTableCompleteMedicalReport = null;
            DataRow[] objdatarowCompleteMedicalReport = null;
            string stringprocessname = "";
            bool boolsmr = false;
            string stringreportverify = "";
            bool boolnotaccatched = false;
            try
            {
                if (sender != null)
                {
                    stringprocessname = ((LinkButton)sender).ToolTip;
                    if (stringprocessname.Length > 0)
                    {
                        objDataTableAddReports = checkverifier();

                        if (objDataTableAddReports != null && objDataTableAddReports.Rows.Count > 0)
                        {
                            objdatarowCompleteMedicalReport = objDataTableAddReports.Select("TRANS_ID = '" + txtRequestNo.Text.Trim().ToString() + "' and CATEGORY = 'COMPLETED MEDICAL REPORTS' ");
                            if (objdatarowCompleteMedicalReport != null && objdatarowCompleteMedicalReport.Length > 0)
                            {
                                objDataTableCompleteMedicalReport = objdatarowCompleteMedicalReport.CopyToDataTable();
                                objDataTableCompleteMedicalReport.DefaultView.Sort = "MODIFIED_ON desc";
                                objDataTableCompleteMedicalReport = objDataTableCompleteMedicalReport.DefaultView.ToTable();

                                if (objDataTableCompleteMedicalReport != null && objDataTableCompleteMedicalReport.Rows.Count > 0)
                                {
                                    boolsmr = true;
                                    boolnotaccatched = true;
                                    stringreportverify = objDataTableCompleteMedicalReport.Rows[0]["SMR_DOC_VERIFY"].ToString();
                                    if (stringreportverify.Length == 0)
                                    {
                                        stringreportverify = "N";
                                    }
                                }
                            }
                        }
                        if (stringreportverify != "N" && boolnotaccatched && boolsmr == true && stringprocessname.Length > 0 && stringprocessname == "Pending forwarding" && (txtDelToID.Text == "EMAIL"))
                        {
                            lblupdateprocesscontent.Text = "Do you want to email the COMPLETED MEDICAL REPORT to requestor?";
                            ViewState["NEXTPROCESSNAME"] = stringprocessname;
                            txtProcessCompletedRemarks.Text = "";
                            Modelpopuperrorsuccess.Show();
                            UpdatePanelModal6success.Visible = true;
                        }
                        else if ((stringreportverify != "N" && boolnotaccatched && stringprocessname == "Pending forwarding") || (stringprocessname != "Pending forwarding"))
                        {
                            lblupdateprocesscontent.Text = "Are you sure want to update the process?";

                            ViewState["NEXTPROCESSNAME"] = stringprocessname;
                            txtProcessCompletedRemarks.Text = "";
                            Modelpopuperrorsuccess.Show();
                            UpdatePanelModal6success.Visible = true;
                        }
                        else if (!boolnotaccatched && stringprocessname == "Pending forwarding")
                        {
                            lblupdateprocesscontent.Text = "There no Medical Report being attached. Do you want to proceed in completing the process ?";
                            ViewState["NEXTPROCESSNAME"] = stringprocessname;
                            txtProcessCompletedRemarks.Text = "";
                            Modelpopuperrorsuccess.Show();
                            UpdatePanelModal6success.Visible = true;
                        } 
                        else if (stringreportverify == "N" && stringprocessname == "Pending forwarding")
                        {
                            string stringmsg = "Medical Report is not verified. Please verify the medical report";
                            CommonFunctions.ShowMessageboot(this, stringmsg);
                        }

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
                objDataTableAddReports = null;
                objDataTableCompleteMedicalReport = null;
                objdatarowCompleteMedicalReport = null;
                stringprocessname = null;
                //boolsmr = false;
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
                stringprocessname = null;
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
            DataTable objdatatableLoadDelayReasons = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            try
            {
                ddlDelayReason.Items.Clear();
                stringServiceType = "List1R1V1";
                stringexp = "And mrdelas.be_id= '" + stringbeid + "' And mrdelas.delmark= 'N'";

                if (Session["SSNLOADDELAYREASONS"] != null)
                {
                    objdatatableLoadDelayReasons = (DataTable)Session["SSNLOADDELAYREASONS"];
                }
                if ((objdatatableLoadDelayReasons == null) || (objdatatableLoadDelayReasons != null && objdatatableLoadDelayReasons.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadDelayReasons = objDatasetResult.Tables["t1"];
                            Session["SSNLOADDELAYREASONS"] = objdatatableLoadDelayReasons;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadDelayReasons != null && objdatatableLoadDelayReasons.Rows.Count > 0)
                {
                    ddlDelayReason.DataTextField = "short_name";
                    ddlDelayReason.DataValueField = "dr_id";
                    ddlDelayReason.DataSource = objdatatableLoadDelayReasons;
                    ddlDelayReason.DataBind();
                    ddlDelayReason.Items.Insert(0, new ListItem("", ""));
                    ddlDelayReason.SelectedIndex = 1;
                }
                else
                {
                    ddlDelayReason.DataSource = null;
                    ddlDelayReason.DataBind();
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
                objdatatableLoadDelayReasons = null;
                stringbeid = null;
                stringServiceType = null;
                stringexp = null;
            }
        }
        private bool ValidateDelayReasonControls()//fix
        {
            try
            {
                if (ddlDelayReason.SelectedItem != null && ddlDelayReason.Text.Trim().Length == 0)
                {
                    CommonFunctions.ShowMessageboot(this, "Delay Reason Field should not be empty,Please enter value");
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
            string stringServiceType = "";
            string stringexp = "";
            string stringprocessname = "";
            string stringRequestID = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
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
                        if (objDatasetResult != null && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count == 0)
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
                                LoadDelarReasonGrid(stringRequestID);
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = null;
                stringexp = null;
                stringprocessname = null;
                stringRequestID = null;
                stringbeid = null;
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
                stringprocessname = null;
            }
        }
        private bool ValidateRecallControls()//fix
        {
            try
            {
                if (txtRecallRemarks.Text.Trim().Length == 0)
                {
                    CommonFunctions.ShowMessageboot(this, "Recall Reason should not be empty,Please enter value");
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
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringprocessname = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            string stringRequestID = "";
            try
            {
                if (ValidateRecallControls())
                {
                    ModelpopupRecall.Hide();
                    UpdatePanelRecall.Visible = false;


                    stringServiceType = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t3"] != null && objDatasetResult.Tables["t3"].Rows.Count == 0)
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
                                LoadDataRecallHistory(stringRequestID);
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringprocessname = null;
                stringbeid = null;
                stringServiceType = null;
                stringexp = null;
                stringRequestID = null;
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
        private bool CheckVerifierComplete(string stringRequestID)
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
                    stringExpression = " and mrasdoc.be_id='" + stringBoID + "' and mrasdoc.request_id='" + stringRequestID + "'and mrasdoc.verify_ref='VERIFIER'  and (mrasdoc.status ='IN-PROGRESS' or mrasdoc.status='PENDING')";

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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringBoID = null;
                stringServiceType = null;
                stringExpression = null;
            }
        }
        protected void btnConfirmprocessStatus_Click(object sender, EventArgs e)
        {
            bool boolContinue = false;
            string stringTransSattus = "";
            DataTable objdatatablePendingItems = null;
            bool boolpendingItems = true;
            bool boolpendingreport = true;
            bool boolEnquirypendingItems = true;
            bool boolAssignDoctor = true;
            bool boolpaymentpendingItems = true;
            DataRow[] objdatarow = null;
            string stringMRamt = ""; 
            string stringbalanceamt = "";
            int intbalanceamt = 0;
            DataTable objdatatable = null;
            bool boomdeptou = true;
            bool boolpendingverifier = true;
            string stringdeptou = "";
            if (ViewState["DEPTOU"] != null)
            {
                stringdeptou = ViewState["DEPTOU"].ToString();
            }
            try
            {
                //if (ValidateProcessControls())
                //{
                Modelpopuperrorsuccess.Hide();
                UpdatePanelModal6success.Visible = false;
                if (lblupdateprocesscontent.Text == "Do you want to email the COMPLETED MEDICAL REPORT to requestor?")
                {
                    if (ViewState["NEXTPROCESSNAME"] != null)
                    {
                        stringTransSattus = ViewState["NEXTPROCESSNAME"].ToString();
                    }
                    if (ViewState["NEXTPROCESSNAME"] != null && stringTransSattus != null && stringTransSattus.Trim().Length > 0)
                    {
                        if (btnPendingDespatch.Visible == true && stringTransSattus.ToUpper() == "PENDING DESPATCH")
                        {
                            if (stringdeptou.Length == 0)
                            {
                                boomdeptou = false;
                            }
                        }
                        if (((btnPendingDespatch.Visible == true) && stringTransSattus.ToUpper() == "PENDING DESPATCH" && txtBypassPenItems.Text.ToString().ToUpper() == "N") || stringTransSattus.ToUpper() == "PENDING FORWARDING")
                        {
                            if (Session["PendingItemsList"] != null)
                            {
                                objdatatablePendingItems = (DataTable)Session["PendingItemsList"];
                            }

                            if (objdatatablePendingItems != null && objdatatablePendingItems.Rows.Count > 0)
                            {
                                objdatarow = objdatatablePendingItems.Select("Pending_Status = 'PENDING'");
                                if (objdatarow != null && objdatarow.Length > 0)
                                {
                                    boolpendingItems = false;
                                }
                            }
                        }

                        if ((btnPendingforwarding.Visible == true) && (stringTransSattus.ToUpper() == "PENDING FORWARDING"))
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
                            if (CheckVerifierComplete(txtRequestNo.Text.Trim()))
                            {
                                boolpendingverifier = false;
                            }
                        }
                        if ((btnPendingAssigned.Visible == true) && (stringTransSattus.ToUpper() == "PENDING ASSIGNED"))
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
                        if ((btnPendingReport.Visible == true) && (stringTransSattus.ToUpper() == "PENDING REPORT"))
                        {
                            boolpendingreport = false;
                            if (CheckDocterComplete(txtRequestNo.Text.Trim()))
                            {
                                boolpendingreport = true;
                            }
                            if ((btnPendingAssigned.Visible == false))
                            {
                                boolpendingreport = true;
                            }
                        }

                        if (boolpendingverifier)
                        {
                            if (boomdeptou)
                            {
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
                                                    LoadSMREMAILNOTIFICATION();
                                                    if (txtsmremailRequestor.Text.Trim().Length == 0)
                                                    {
                                                        txtsmremailRequestor.Text = txtReqEmail.Text.Trim();
                                                    }
                                                    mpePdtPlt23.Show();
                                                    Panel10.Visible = true;
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
                                        CommonFunctions.ShowMessageboot(this, "Please Complete Pending Items");
                                    }
                                }
                                else
                                {
                                    CommonFunctions.ShowMessageboot(this, "Please Complete Payment Details");
                                }
                            }
                            else
                            {
                                CommonFunctions.ShowMessageboot(this, "Please Select Department OU");
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Please Complete the Verifier Status");
                        }
                    }

                }
                else
                {
                    if (ViewState["NEXTPROCESSNAME"] != null)
                    {
                        stringTransSattus = ViewState["NEXTPROCESSNAME"].ToString();
                    }
                    if (ViewState["NEXTPROCESSNAME"] != null && stringTransSattus != null && stringTransSattus.Trim().Length > 0)
                    {
                        if (btnPendingDespatch.Visible == true && stringTransSattus.ToUpper() == "PENDING DESPATCH")
                        {
                            if (stringdeptou.Length == 0)
                            {
                                boomdeptou = false;
                            }
                        }
                        if (((btnPendingDespatch.Visible == true) && stringTransSattus.ToUpper() == "PENDING DESPATCH" && txtBypassPenItems.Text.ToString().ToUpper() == "N") || stringTransSattus.ToUpper() == "PENDING FORWARDING")
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
                        if ((btnPendingforwarding.Visible == true) && (stringTransSattus.ToUpper() == "PENDING FORWARDING"))
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
                            if (CheckVerifierComplete(txtRequestNo.Text.Trim()))
                            {
                                boolpendingverifier = false;
                            }
                        }
                        if ((btnPendingAssigned.Visible == true) && (stringTransSattus.ToUpper() == "PENDING ASSIGNED"))
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
                        if ((btnPendingReport.Visible == true) && (stringTransSattus.ToUpper() == "PENDING REPORT"))
                        {
                            boolpendingreport = false;
                            if (CheckDocterComplete(txtRequestNo.Text.Trim()))
                            {
                                boolpendingreport = true;
                            }
                            if ((btnPendingAssigned.Visible == false))
                            {
                                boolpendingreport = true;
                            }
                        }
                        if (boolpendingverifier)
                        {
                            if (boomdeptou)
                            {
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
                                                        string stringRequestID = txtRequestNo.Text.Trim().ToUpper();
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
                            else
                            {
                                CommonFunctions.ShowMessageboot(this, "Please Select Department OU");
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Please Complete the Verifier Status");
                        }
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
                boolContinue = false;
                stringTransSattus = null;
                objdatatablePendingItems = null;
                objdatarow = null;
                stringMRamt = null; 
                stringbalanceamt = null;
                intbalanceamt = 0;
                objdatatable = null;
            }
        }

        #region sentCOMPLETESMRDOC_Click
        public bool IsValidEmailAddress(string email)
        {
            try
            {
                Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                RegexOptions.CultureInvariant | RegexOptions.Singleline);
                bool isValidEmail = regex.IsMatch(email);
                if (isValidEmail)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        protected void btnsentCOMPLETESMRDOC_Click(object sender, EventArgs e)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V3";
            string stringTransSattus = "";
            bool boolcheckvalidationvalidemail = true;
            string stringOverallMsg = "";
            byte[] objbytearray = null;
            try
            {
                if (txtsmremailRequestor.Text.Trim().Length > 0)
                {
                    if (txtDelToID.Text.Trim().Length > 0 && txtDelToID.Text == "EMAIL" && txtsmremailRequestor.Text.Trim().Length > 0)
                    {
                        if (!IsValidEmailAddress(txtsmremailRequestor.Text.Trim()))
                        {
                            boolcheckvalidationvalidemail = false;
                        }
                    }
                    if (boolcheckvalidationvalidemail)
                    {
                        objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                        objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = stringformid;

                        clsCertificateValidation.EnableTrustedHosts();
                        using (GSInterfaceService.InterfaceServiceClient objInterfaceServiceClient = new GSInterfaceService.InterfaceServiceClient(CommonFunctions.CreateServiceBasicHttpBinding(), CommonFunctions.objEndpointAddressInterfaceService))
                        {   objDatasetResult = objInterfaceServiceClient.SendEmailUserR1V1(txtRequestNo.Text.Trim(), txtsmremailRequestor.Text.Trim(), txtsmremailCC.Text.Trim(), txtsmremailBCC.Text.Trim(), txtsmremailSubject.Text.Trim(), txtsmremailCOntent.Text.Trim(),"N", objDatasetAppsVariables,out objbytearray, out interrorcount, out stringOutputResult);
                            if (objInterfaceServiceClient != null)
                                objInterfaceServiceClient.Close();
                        }
                        SaveRemarksSMREMAIL(interrorcount, stringOutputResult);
                        if (interrorcount == 0)
                        { 
                            mpePdtPlt23.Show();
                            Panel10.Visible = true;
                            if (UpdateProcessStatus(stringTransSattus.Trim().ToUpper()))
                            {
                                CommonFunctions.ShowMessageboot(this, "Document Submitted to Email Server");
                                LoadData(txtRequestNo.Text.Trim());
                                mpePdtPlt23.Hide();
                                Panel10.Visible = false;
                            }
                        }
                        else
                        {
                            mpePdtPlt23.Hide();
                            Panel10.Visible = false;
                            Errorpopup(stringOutputResult); 
                        }
                    }
                    else
                    {
                        stringOverallMsg = "You must enter the Valid Recipient Email" + "\\r\\n";
                        CommonFunctions.ShowMessageboot(this, stringOverallMsg);
                        mpePdtPlt23.Show();
                        Panel10.Visible = true;
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, "You must enter the value for Recipient Email");
                    mpePdtPlt23.Show();
                    Panel10.Visible = true;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);

                mpePdtPlt23.Show();
                Panel10.Visible = true;
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                stringOutputResult = null;
                stringformid = null;
                stringTransSattus = null;
                stringOverallMsg = null;
            }
        }
        private void SaveRemarksSMREMAIL(int interrorcountemail, string[] stringOutputResultemail)
        {
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
            string stringcustemerid3 = "";
            string stringServiceType1 = "";
            string stringformid1 = "";
            string stringremarksmessge = "";
            try
            {
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables["t9"] != null && objDatasetResult.Tables["t9"].Rows.Count == 0)
                    {
                        objDataRow = objDatasetResult.Tables["t9"].NewRow();

                        objDataRow["DELMARK"] = "N";
                        objDataRow["REFERENCE_5"] = "I";
                        objDataRow["be_id"] = stringBoID;
                        objDataRow["Request_ID"] = txtRequestNo.Text.Trim();
                        objDataRow["REGRMK_ID"] = "REQUESTOREMAIL";
                        objDataRow["TARG_AUD"] = "ALL";
                        objDataRow["SHORT_NAME"] = "Recipient Email";
                        objDataRow["Long_NAME"] = "Recipient Email";
                        objDataRow["REMARKS_DATE"] = DateTime.Now;
                        if (interrorcountemail == 0)
                        {
                            objDataRow["remarks"] = "RECIPIENT EMAIL :" + txtsmremailRequestor.Text.Trim().ToUpper() + " STATUS: EMAIL SENT TO EMAIL SERVER SUCCESSFULLY";
                        }
                        else
                        {
                            if(stringOutputResultemail != null && stringOutputResultemail[2] != null && stringOutputResultemail[2].ToString().Length > 0)
                            {
                                stringremarksmessge = stringOutputResultemail[2].ToString();
                                objDataRow["remarks"] = "RECIPIENT EMAIL :" + txtsmremailRequestor.Text.Trim().ToUpper() + " " + stringremarksmessge.ToUpper();
                            }
                        }
                        CommonFunctions.AssignAuditLogDetails(ref objDataRow);

                        objDatasetResult.Tables["t9"].Rows.Add(objDataRow);

                        objDatasetResult.Tables["t9"].AcceptChanges();

                        objDatasetResult.Tables["t9"].Rows[0].RowState.ToString(); 

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
                intFromRecord = 0;
                intToRecord = 0;
                objDatasetResult1 = null;
                stringformid = null;
                stringServiceType = null;
                stringexp = null;
                stringBoID = null;
                objDataRow = null;
                intErrorCount = 0;
                stringcustemerid3 = null;
                stringServiceType1 = null;
                stringformid1 = null;
            }
        }
        private void LoadSMREMAILNOTIFICATION()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0014R1V1";
            string stringOrderBy = "lst.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            DataTable objDataTable = null;
            DataRow[] objdatarow = null;
            DataRow[] objdatarowsub = null;
            string stringcondition = "";
            string stringContent = "";
            string stringServiceType = "";
            try
            {

                stringcondition = "And lst.be_id= '" + stringbeid + "'  AND  lst.LSTGRP_ID like '%COMPLETEMEDREPEMAILNOTIFICATION%' AND lst.delmark='N' ";

                stringServiceType = "List1R1V1";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t1"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        if (objDataTable.Select("lst_id= 'SMREMAILNOTIFICATION'").Length > 0)
                        {
                            objdatarow = objDataTable.Select("lst_id= 'SMREMAILNOTIFICATION'");

                            stringContent = objdatarow[0]["REMARKS"].ToString();
                            txtsmremailCOntent.Text = stringContent;
                        }
                        if (objDataTable.Select("lst_id= 'SMREMAILNOTIFICATIONSUBJECT'").Length > 0)
                        {
                            objdatarowsub = objDataTable.Select("lst_id= 'SMREMAILNOTIFICATIONSUBJECT'");

                            stringContent = objdatarowsub[0]["REMARKS"].ToString();
                            txtsmremailSubject.Text = stringContent;
                        }
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                }

                txtsmremailCC.Text = "";
                txtsmremailBCC.Text = "";
                LoadSMRAttachmentsEmail();

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
                stringbeid = null;
                objDataTable = null;
                objdatarow = null;
                objdatarowsub = null;
                stringcondition = null;
                stringContent = null;
                stringServiceType = null;
            }
        }

        private void LoadSMRAttachmentsEmail()
        {
            DataTable objDataTableAddReports = null;
            DataTable objDataTableCompleteMedicalReport = null;
            DataRow objdatarowCompleteMedicalReportlatest = null;
            string stringBoID = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            DataRow[] objdatarowCompleteMedicalReport = null;
            string stringBE_ID, stringFORM_ID, stringTRANS_ID, stringDOC_NAME, stringDOC_TYPE, stringATTACH_ID, StringFileName = string.Empty;
            DataSet objDatasetAppsVariables = (DataSet)HttpContext.Current.Session["objDatasetlocaldeclaration"];
            objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FINT0003R1V1";
            string stringreqID = "";
            try
            {
                if (Session["ADD_ATTACHMENTS"] != null)
                { objDataTableAddReports = (DataTable)Session["ADD_ATTACHMENTS"]; }


                if (objDataTableAddReports != null && objDataTableAddReports.Rows.Count > 0)
                {
                     objdatarowCompleteMedicalReport = objDataTableAddReports.Select("TRANS_ID = '" + txtRequestNo.Text.Trim().ToString() + "' and CATEGORY = 'COMPLETED MEDICAL REPORTS' ");
                    if (objdatarowCompleteMedicalReport != null && objdatarowCompleteMedicalReport.Length > 0)
                    {
                        objDataTableCompleteMedicalReport = objdatarowCompleteMedicalReport.CopyToDataTable();
                        objDataTableCompleteMedicalReport.DefaultView.Sort = "MODIFIED_ON desc";
                        objDataTableCompleteMedicalReport = objDataTableCompleteMedicalReport.DefaultView.ToTable();

                        if (objDataTableCompleteMedicalReport != null && objDataTableCompleteMedicalReport.Rows.Count > 0)
                        {
                            objdatarowCompleteMedicalReportlatest = objDataTableCompleteMedicalReport.Rows[0];
                        }
                        if (objdatarowCompleteMedicalReportlatest != null)
                        {
                            stringBE_ID = objdatarowCompleteMedicalReportlatest["BE_ID"].ToString();
                            stringFORM_ID = objdatarowCompleteMedicalReportlatest["FORM_ID"].ToString();
                            stringTRANS_ID = objdatarowCompleteMedicalReportlatest["TRANS_ID"].ToString();
                            stringDOC_NAME = objdatarowCompleteMedicalReportlatest["DOC_NAME"].ToString();
                            stringDOC_TYPE = objdatarowCompleteMedicalReportlatest["DOC_TYPE"].ToString();
                            stringATTACH_ID = objdatarowCompleteMedicalReportlatest["ATTACH_ID"].ToString();
                            StringFileName = stringBE_ID + @"\" + stringFORM_ID + @"\" + stringATTACH_ID + @"\" +  stringDOC_NAME;
                            if (txtRequestNo.Text.Trim().Length > 0)
                            {
                                stringreqID = txtRequestNo.Text.Trim().ToString().Replace('/', '_');
                                if (stringreqID.Length > 0)
                                {
                                    stringDOC_NAME = stringreqID + "_" + stringDOC_NAME;
                                }
                            }
                            lnkbtnsmremailfilename.Text = stringDOC_NAME;
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
                objDataTableAddReports = null;
                objDataTableCompleteMedicalReport = null;
                objdatarowCompleteMedicalReportlatest = null;
                stringBoID = null;
                objdatarowCompleteMedicalReport = null;
                stringBE_ID = null;
                stringFORM_ID = null;
                stringTRANS_ID = null;
                stringDOC_NAME = null;
                stringDOC_TYPE = null;
                stringATTACH_ID = null;
                StringFileName = null;
            }

        }
        protected void btncancelsmrfile_Click(object sender, ImageClickEventArgs e)
        {
            pnlattachmentsmr.Visible = false;
        }
        protected void lnkbtnsmremailfilename_Click(object sender, EventArgs e)
        {
            DataTable objDataTableAddReports = null;
            DataTable objDataTableCompleteMedicalReport = null;
            DataRow objdatarowCompleteMedicalReportlatest = null;
            string stringBoID = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            DataRow[] objdatarowCompleteMedicalReport = null;
            string stringBE_ID, stringFORM_ID, stringTRANS_ID, stringDOC_NAME, stringDOC_TYPE, stringATTACH_ID, StringFileName = string.Empty;
            DataSet objDatasetAppsVariables = (DataSet)HttpContext.Current.Session["objDatasetlocaldeclaration"];
            objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FINT0003R1V1";

            long longlength;
            string stringFilepath = "";
            byte[] objbyteArray = null;
            string[] stringOutputResult = null;
            string base64Pdf = "";
            try
            {
                if (Session["ADD_ATTACHMENTS"] != null)
                { objDataTableAddReports = (DataTable)Session["ADD_ATTACHMENTS"]; }


                if (objDataTableAddReports != null && objDataTableAddReports.Rows.Count > 0)
                { 
                    objdatarowCompleteMedicalReport = objDataTableAddReports.Select("TRANS_ID = '" + txtRequestNo.Text.Trim().ToString() + "' and CATEGORY = 'COMPLETED MEDICAL REPORTS' ");
                    if (objdatarowCompleteMedicalReport != null && objdatarowCompleteMedicalReport.Length > 0)
                    {
                        objDataTableCompleteMedicalReport = objdatarowCompleteMedicalReport.CopyToDataTable();
                        objDataTableCompleteMedicalReport.DefaultView.Sort = "MODIFIED_ON desc";
                        objDataTableCompleteMedicalReport = objDataTableCompleteMedicalReport.DefaultView.ToTable();

                        if (objDataTableCompleteMedicalReport != null && objDataTableCompleteMedicalReport.Rows.Count > 0)
                        {
                            objdatarowCompleteMedicalReportlatest = objDataTableCompleteMedicalReport.Rows[0];
                        }
                        if (objdatarowCompleteMedicalReportlatest != null)
                        {
                            stringBE_ID = objdatarowCompleteMedicalReportlatest["BE_ID"].ToString();
                            stringFORM_ID = objdatarowCompleteMedicalReportlatest["FORM_ID"].ToString();
                            stringTRANS_ID = objdatarowCompleteMedicalReportlatest["TRANS_ID"].ToString();
                            stringDOC_NAME = objdatarowCompleteMedicalReportlatest["DOC_NAME"].ToString();
                            stringDOC_TYPE = objdatarowCompleteMedicalReportlatest["DOC_TYPE"].ToString();
                            stringATTACH_ID = objdatarowCompleteMedicalReportlatest["ATTACH_ID"].ToString();
                            StringFileName = stringBE_ID + @"\" + stringFORM_ID + @"\" + stringATTACH_ID + @"\" +  stringDOC_NAME;

                            clsCertificateValidation.EnableTrustedHosts();
                            using (GSFileTransferService.FileTransferServiceClient objFileTransfer1 = new GSFileTransferService.FileTransferServiceClient())
                            {
                                objFileTransfer1.DownloadFileFromServerR1V1(ref objDatasetAppsVariables, stringATTACH_ID, ref StringFileName, stringDOC_NAME, txtRequestNo.Text.Trim(), out longlength, out stringFilepath, out stringOutputResult, out objbyteArray);

                                if (objFileTransfer1 != null)
                                    objFileTransfer1.Close();
                            }
                            if (stringFilepath != null && stringFilepath.Length > 0)
                            {
                            }
                            else
                            {
                                Errorpopup(stringOutputResult);
                            }
                            CommonFunctions.OpenExportedFileR1V1(this, objbyteArray, StringFileName.ToString(), "ATTACHMENTS");
                        }
                    }
                }
                mpePdtPlt23.Show();
                Panel10.Visible = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDataTableAddReports = null;
                objDataTableCompleteMedicalReport = null;
                objdatarowCompleteMedicalReportlatest = null;
                stringBoID = null;
                objdatarowCompleteMedicalReport = null;
                stringBE_ID = null;
                stringFORM_ID = null;
                stringTRANS_ID = null;
                stringDOC_NAME = null;
                stringDOC_TYPE = null;
                stringATTACH_ID = null;
                StringFileName = null;
                stringFilepath = null;
                objbyteArray = null;
                stringOutputResult = null;
            } 
        } 
        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtenderpreview.Hide();
            Paneldocpreview.Visible = false;
            mpePdtPlt23.Show();
            Panel10.Visible = true;
        }
        #endregion

        protected void btnConfirmprocessClose_Click(object sender, EventArgs e)
        {
            bool boolContinue = false;
            string stringTransSattus = "";
            DataTable objdatatablePendingItems = null;
            bool boolpendingItems = true;
            bool boolpaymentpendingItems = true;
            bool boolEnquirypendingItems = true;
            bool boolAssignDoctor = true;
            bool boolpendingreport = true;
            string stringMRamt = ""; 
            DataTable objdatatable = null;
            DataRow[] objdatarow = null;
            string stringbalanceamt = "";
            int intbalanceamt = 0;
            string stringRequestID = "";
            bool boomdeptou = true;
            bool boolpendingverifier = true;
            string stringdeptou = "";
            if (ViewState["DEPTOU"] != null)
            {
                stringdeptou = ViewState["DEPTOU"].ToString();
            }
            try
            {
                if (lblupdateprocesscontent.Text == "Do you want to email the COMPLETED MEDICAL REPORT to requestor?")
                {
                    if (ViewState["NEXTPROCESSNAME"] != null)
                    {
                        stringTransSattus = ViewState["NEXTPROCESSNAME"].ToString();
                    }
                    if (ViewState["NEXTPROCESSNAME"] != null && stringTransSattus != null && stringTransSattus.Trim().Length > 0)
                    {
                        if (btnPendingDespatch.Visible == true && stringTransSattus.ToUpper() == "PENDING DESPATCH")
                        {
                            if (stringdeptou.Length == 0)
                            {
                                boomdeptou = false;
                            }
                        }
                        if (((btnPendingDespatch.Visible == true) && stringTransSattus.ToUpper() == "PENDING DESPATCH" && txtBypassPenItems.Text.ToString().ToUpper() == "N") || stringTransSattus.ToUpper() == "PENDING FORWARDING")
                        {
                            if (Session["PendingItemsList"] != null)
                            {
                                objdatatablePendingItems = (DataTable)Session["PendingItemsList"];
                            }
                            if (objdatatablePendingItems != null && objdatatablePendingItems.Rows.Count > 0)
                            {
                                objdatarow = objdatatablePendingItems.Select("Pending_Status = 'PENDING'");
                                if (objdatarow != null && objdatarow.Length > 0)
                                {
                                    boolpendingItems = false;
                                }
                            }
                        }
                        if ((btnPendingforwarding.Visible == true) && (stringTransSattus.ToUpper() == "PENDING FORWARDING"))
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
                            if (CheckVerifierComplete(txtRequestNo.Text.Trim()))
                            {
                                boolpendingverifier = false;
                            }
                        }
                        if ((btnPendingAssigned.Visible == true) && (stringTransSattus.ToUpper() == "PENDING ASSIGNED"))
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
                        if ((btnPendingReport.Visible == true) && (stringTransSattus.ToUpper() == "PENDING REPORT"))
                        {
                            boolpendingreport = false;
                            if (CheckDocterComplete(txtRequestNo.Text.Trim()))
                            {
                                boolpendingreport = true;
                            }
                            if ((btnPendingAssigned.Visible == false))
                            {
                                boolpendingreport = true;
                            }
                        }
                        if (boolpendingverifier)
                        {
                            if (boomdeptou)
                            {
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
                            else
                            {
                                CommonFunctions.ShowMessageboot(this, "Please Select Department OU");
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Please Complete the Verifier Status");
                        }
                    }

                }
                else
                {
                    Modelpopuperrorsuccess.Hide();
                    UpdatePanelModal6success.Visible = false;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringTransSattus = null;
                objdatatablePendingItems = null;
                stringMRamt = null;  
                objdatatable = null;
                objdatarow = null;
                stringbalanceamt = null;
                intbalanceamt = 0;
                stringRequestID = null;
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
                    objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
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
                objDatasetResult = null;
                interrorcount = 0;
                stringOutputResult = null;
                stringformid = null;
                stringInputs = null;
            }
            return boolStatus;
        }
        private void ProcessHeaderDynamicName(DataTable objDataTable)
        {
            string stringTRANS_STATUS = "";
            int intNoOfDays = 0;
            int intForwarded = 0;
            try
            {
                for (int intIndex3 = 0; intIndex3 < objDataTable.Rows.Count; intIndex3++)
                {
                    stringTRANS_STATUS = objDataTable.Rows[intIndex3]["TRANS_STATUS"].ToString();
                    if (stringTRANS_STATUS.ToUpper() == "PENDING TRACING")
                    {
                        btnPendingTracing.Text = objDataTable.Rows[intIndex3]["seq_id"].ToString();
                    }
                    else if (stringTRANS_STATUS.ToUpper() == "PENDING DESPATCH")
                    {
                        btnPendingDespatch.Text = objDataTable.Rows[intIndex3]["seq_id"].ToString();
                    }
                    else if (stringTRANS_STATUS.ToUpper() == "PENDING ASSIGNED")
                    {
                        btnPendingAssigned.Text = objDataTable.Rows[intIndex3]["seq_id"].ToString();
                    }
                    else if (stringTRANS_STATUS.ToUpper() == "PENDING REPORT")
                    {
                        btnPendingReport.Text = objDataTable.Rows[intIndex3]["seq_id"].ToString();
                    }
                    else if (stringTRANS_STATUS.ToUpper() == "PENDING RELEASE TO HIMS")
                    {
                        btnPendingReleasetoHIMS.Text = objDataTable.Rows[intIndex3]["seq_id"].ToString();
                    }
                    else if (stringTRANS_STATUS.ToUpper() == "PENDING SUP VETTING")
                    {
                        btnPendingSupVetting.Text = objDataTable.Rows[intIndex3]["seq_id"].ToString();
                    }
                    else if (stringTRANS_STATUS.ToUpper() == "PENDING FORWARDING")
                    {
                        btnPendingforwarding.Text = objDataTable.Rows[intIndex3]["seq_id"].ToString();
                    }
                    else if (stringTRANS_STATUS.ToUpper() == "PENDING COLLECT IN PERSON")
                    {
                        btnPendingCollectInPerson.Text = objDataTable.Rows[intIndex3]["seq_id"].ToString();
                    }
                    else if (stringTRANS_STATUS.ToUpper() == "PENDING FORWARDED")
                    {
                        btnForwarded.Text = objDataTable.Rows[intIndex3]["seq_id"].ToString();
                    }
                }
                if (txtDelToID.Text.Trim().Length == 0 && btnPendingforwarding.Text.Length > 0)
                {
                    intNoOfDays = Convert.ToInt32(btnPendingforwarding.Text);
                    intForwarded = intNoOfDays + 1;
                    btnForwarded.Text = intForwarded.ToString();
                }
                else if (txtDelToID.Text.Trim().Length > 0 && btnPendingforwarding.Text.Length > 0 && txtDelToID.Text.Trim().ToUpper() != "INPERSON")
                {
                    intNoOfDays = Convert.ToInt32(btnPendingforwarding.Text);
                    intForwarded = intNoOfDays + 1;
                    btnForwarded.Text = intForwarded.ToString();
                }
                else if (txtDelToID.Text.Trim().Length > 0 && btnPendingforwarding.Text.Length > 0 && txtDelToID.Text.Trim().ToUpper() == "INPERSON")
                {
                    intNoOfDays = Convert.ToInt32(btnPendingCollectInPerson.Text);
                    intForwarded = intNoOfDays + 1;
                    btnForwarded.Text = intForwarded.ToString();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
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
                                ProcessHeaderDynamicName(objDataTable);
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = null;
                objDataTable = null;
                stringExpression = null;
                stringBoID = null;
                stringMRStatus = null;
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

                        btnPendingTracing.BackColor = Color.FromArgb(186 ,228, 252);
                        btnPendingTracing.ForeColor = Color.FromArgb(57, 114, 121);
                        if (objDueDate < DateTime.Now.Date)
                        {
                            btnPendingDespatch.BackColor = Color.FromArgb(255, 77, 77);
                            btnPendingDespatch.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            btnPendingDespatch.BackColor = Color.FromArgb(42, 167, 237);
                            btnPendingDespatch.ForeColor = Color.FromArgb(255, 255, 255);
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

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = Color.FromArgb(186 ,228, 252);
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

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = Color.FromArgb(186 ,228, 252);
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

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = Color.FromArgb(186 ,228, 252);
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

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = Color.FromArgb(186 ,228, 252);
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

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.BackColor = Color.FromArgb(186 ,228, 252);
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

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.BackColor = btnPendingforwarding.BackColor = Color.FromArgb(186 ,228, 252);
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
                            lblForwardStatus.Text = "Forwarded";
                        }
                        else if (stringMRStatus == "COLLECTED")
                        {
                            lblForwardStatus.Text = "Collected";
                        }

                        btnPendingTracing.Enabled = false;
                        btnPendingDespatch.Enabled = false;
                        btnPendingAssigned.Enabled = false;
                        btnPendingReport.Enabled = false;
                        btnPendingReleasetoHIMS.Enabled = false;
                        btnPendingSupVetting.Enabled = false;
                        btnPendingforwarding.Enabled = false;
                        btnPendingCollectInPerson.Enabled = false;

                        btnPendingTracing.BackColor = btnPendingDespatch.BackColor = btnPendingAssigned.BackColor = btnPendingReport.BackColor = btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.BackColor = btnPendingforwarding.BackColor = btnPendingCollectInPerson.BackColor = Color.FromArgb(186 ,228, 252);
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
                    //objDueDate = objDueDate01.AddDays(-1);
                }
                stringProcessID = objDataRow["MRP_ID"].ToString();
                LoadProcessHistory(txtRequestNo.Text.Trim(), "NONLOAD");
                ProcessTABControlProcesstype(stringProcessID, "NONLOAD");
                ProcessControl(txtMRStatus.Text.Trim(), stringDeliverBy, objDueDate01);
                if (CheckDocterComplete(txtRequestNo.Text.Trim()))
                {
                    lnkbtnRecallRequest4.Visible = false;
                }
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
                stringDeliverBy = null;
                stringduedte = null;
                stringProcessID = null;
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = null;
                objDataTable = null;
                stringExpression = null;
                stringBoID = null;
                stringDueduedays = null;
                stringMRSTSTUS = null;
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringBoID = null;
                stringServiceType = null;
                stringExpression = null;
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
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType1 = "";
            string stringexp012 = "";
            try
            {
                if (stringReqID != null && stringReqID.Trim().Length > 0)
                {
                    stringServiceType1 = "List5R1V1";
                    stringexp012 = " And mrreg.be_id= '" + stringbeid + "'  And mrreg.request_id= '" + stringReqID.ToString() + "'";

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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord =0;
                objDataTable = null;
                stringbeid = null;
                stringServiceType1 = null;
                stringexp012 = null;
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringBoID = null;
                stringServiceType = null;
                stringExpression = null;
            }
        }
        #endregion

        #endregion

        #endregion


        #region Print Report

        protected void imgbtnAudit_Click(object sender, ImageClickEventArgs e)
        {
            DataTable objdatatabledefault = null;
            DataTable objdatatablecancelled = null;
            DataRow[] objdatrow = null;
            string stringStatus = "";
            DataRow[] objdatarowcancelled = null;
            try
            {

                if (Session["defauledviewstatus"] != null)
                {
                    objdatatabledefault = (DataTable)Session["defauledviewstatus"];
                    if (objdatatabledefault != null)
                    {
                        objdatrow = objdatatabledefault.Select("STATUS='APPT0001'");
                        if (objdatrow != null && objdatrow.Length > 0)
                        {
                            rbtndefaultclinicfinal.Enabled = true;
                            rbtnmrassesment.Enabled = true;
                            rbtndefaultclinicreview.Enabled = true;
                        }
                        else
                        {
                            rbtndefaultclinicfinal.Enabled = false;
                            rbtnmrassesment.Enabled = false;
                            rbtndefaultclinicreview.Enabled = false;
                        }
                        //Session["defauledviewstatus"] = null;
                    }
                }
                if (Session["Cancelledrecord"] != null)
                {
                    objdatatablecancelled = (DataTable)Session["Cancelledrecord"];
                    if (objdatatablecancelled != null && objdatatablecancelled.Rows.Count > 0)
                    {
                        objdatarowcancelled = objdatatablecancelled.Select("MR_STATUS='CANCELLED'");
                        if (objdatarowcancelled != null && objdatarowcancelled.Length > 0)
                        {
                            rbtncancellation.Enabled = true;
                        }
                        else
                        {
                            rbtncancellation.Enabled = false;
                        }
                    }
                }
                stringStatus = txtMRStatus.Text.Trim().ToString();
                if (stringStatus.Trim().ToUpper() == "FORWARDED" || stringStatus.Trim().ToUpper() == "COLLECTED" || stringStatus.Trim().ToUpper() == "CANCELLED")
                {
                    rbtnnorecord.Enabled = false;
                    rbtnoutstanding.Enabled = false;
                }
                mdlrbt.Show();
                pnlreportpopup.Visible = true;
                rbtnrequestormail.Checked = true;
                AccessenableReports();
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }


        }

        private void AccessenableReports()
        {
            DataRow[] objdatarow01 = null;
            DataRow[] objdatarowpaymentdetail = null;
            string stringnotreadyforaccessment = "";
            DataTable objdatatablerpttype = null;
            DataTable objdatattablepaymentdetail = null;
            try
            {
                CommonFunctions.LoadReportType("SESSION");
                if (Session["ReportType0001DATATABLE"] != null)
                {
                    objdatatablerpttype = (DataTable)Session["ReportType0001DATATABLE"];

                }
                if (objdatatablerpttype != null && objdatatablerpttype.Rows.Count > 0)
                {
                    objdatarow01 = objdatatablerpttype.Select("rpttyp_id = '" + hdfreporttype.Value.ToString() + "'");
                    if (objdatarow01 != null && objdatarow01.Length > 0)
                    {
                        stringnotreadyforaccessment = objdatarow01[0]["WIC_FLAG"].ToString();// need to change workmenletter
                        if (stringnotreadyforaccessment == "Y")
                        {
                            rbtnworkcompensation.Enabled = true;
                        }
                        else
                        {
                            rbtnworkcompensation.Enabled = false;
                        }

                    }
                }
                //if (hdfappoinmentdate.Value != null && hdfappoinmentdate.Value.ToString().Trim().Length > 0)
                //{
                //    rbtndefaultclinicreview.Enabled = true; rbtndefaultclinicfinal.Enabled = true;
                //}
                //else
                //{
                //    rbtndefaultclinicreview.Enabled = false;
                //    rbtndefaultclinicfinal.Enabled = false;

                //}
                //if (objDataRow["first_rem_print_date"] != null && objDataRow["first_rem_print_date"].ToString().Trim().Length > 0)
                //{ txtFirstRemPrintedOn.Text = Convert.ToDateTime(objDataRow["first_rem_print_date"]).ToString("dd-MM-yyyy"); }

                //if (objDataRow["second_rem_printed_date"] != null && objDataRow["second_rem_printed_date"].ToString().Trim().Length > 0)
                //{ txtSecRemPrintedOn.Text = Convert.ToDateTime(objDataRow["second_rem_printed_date"]).ToString("dd-MM-yyyy"); }

                //if (txtSecRemPrintedOn.Text.Length > 0)
                //{
                //    btnSecRemLetter.Enabled = false;

                //    btnSecRemLetter.BackColor = System.Drawing.Color.FromArgb(190, 125, 65);
                //}
                //else
                //{
                //    btnSecRemLetter.Enabled = true;
                //}
                if (Session["LoadPaymentReceiptsGridFC0001"] != null)
                {
                    objdatattablepaymentdetail = (DataTable)Session["LoadPaymentReceiptsGridFC0001"];
                    if (objdatattablepaymentdetail != null && objdatattablepaymentdetail.Rows.Count > 0)
                    {
                        objdatarowpaymentdetail = objdatattablepaymentdetail.Select("PAYMENT_STATUS='PARTIAL PAID' or PAYMENT_STATUS = 'PAID'");
                        if (objdatarowpaymentdetail != null && objdatarowpaymentdetail.Length > 0)
                        {
                            rbtnpaymentAcknowlege.Enabled = true;
                        }
                        else
                        {
                            rbtnpaymentAcknowlege.Enabled = false;
                        }
                    }
                }
                else
                {
                    rbtnpaymentAcknowlege.Enabled = false;
                }

                ControlByStatus(txtMRStatus.Text.Trim(), txtDelToID.Text.Trim());

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        private void ControlByStatus(string stringStatus, string stringDeliveryMode)//completed
        {
            switch (txtMRStatus.Text.Trim().ToUpper())
            {
                case "MR CREATED":
                    { 
                        break;
                    }
                case "TRACED":
                    { 
                        break;
                    }
                case "DESPATCHED":
                    { 
                        break;
                    }
                case "RECEIVED":
                    { 
                        break;
                    }
                case "FORWARDED":
                    { 
                        break;
                    }
                case "COLLECTED":
                    { 
                        break;
                    }
                case "CANCELLED":
                    {
                        int intnoofdoc = 0;

                        if (hdfCANRefundamt.Value != null && hdfCANRefundamt.Value.ToString().Trim().Length > 0)
                        {
                            double refundAmount;
                            if (double.TryParse(hdfCANRefundamt.Value.ToString(), out refundAmount))
                            {
                                intnoofdoc = Convert.ToInt32(refundAmount);
                            }
                            if (intnoofdoc > 0)
                            {
                                rbtnrefundletter.Enabled = true;
                                rbtnpartialrefundletter.Enabled = true;
                            }
                            else
                            {
                                rbtnrefundletter.Enabled = false;
                                rbtnrefundletter.Enabled = false;

                            }

                        }
                        else
                        {
                            rbtndefaultclinicreview.Enabled = false;
                            rbtndefaultclinicfinal.Enabled = false;

                        } 
                        break;
                    }
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string stringreport = ""; 
            try
            {

                stringreport = txtRequestNo.Text.ToString();
                if (rbtnacknoeledge.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700011R1V1", "acknowledgement_letter");
                    }
                }
                else if (rbtnhospitalization.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700020R1V1", "hospitalisation_letter");
                    }
                }
                else if (rbtnrefundletter.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700010R1V1", "refund_letter");
                    }
                }
                else if (rbtnnorecord.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700022R1V1", "no_records_letter");
                    }
                }
                else if (rbtndefaultclinicreview.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700018R1V1", "defaulter");
                    }
                }
                else if (rbtndefaultclinicfinal.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700019R1V1", "final_defaulter");
                    }
                }
                else if (rbtncoverletter.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700008R1V1", "cover_letter");
                    }
                }
                else if (rbtnpartialrefundletter.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700009R1V1", "partial_refund_letter");
                    }
                }
                else if (rbtnworkcompensation.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700021R1V1", "WMC_letter");
                    }
                }
                else if (rbtnnotreportassenment.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700025R1V1", "assessment_report");
                    }
                }
                else if (rbtnpendingitemfirst.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        if (txtFirstRemPrintedOn.Text.Trim().Length == 0)
                        {
                            if (UpdateFirstReminderPrintDate(stringreport.Trim().ToUpper()))
                            {
                                LoadRegRequestInfo(stringreport);
                            }
                            LoadReportsprinterprinter("DOP700003R1V1", "FirstReminderLetter");
                        }
                        else
                        {
                            LoadReportsprinterprinter("DOP700003R1V1", "FirstReminderLetter");
                        }
                    }
                }
                else if (rbtnpendingitemfinal.Checked == true)
                {
                    if (txtFirstRemPrintedOn.Text.Trim().Length > 0)
                    {
                        DateTime objDateTimeFirstRem = CommonFunctions.ConvertToDateTime(txtFirstRemPrintedOn.Text.Trim(), "dd-MM-yyyy");

                        TimeSpan daycalc = objDateTimeFirstRem.Subtract(DateTime.Now);
                        int intday = (int)daycalc.TotalDays;

                        if (intday >= 14)
                        {
                            SecondRemLetterPrint();
                        }
                        else
                        {
                            //now temp hide
                            SecondRemLetterPrint();
                            //string stringd1stdate = CommonFunctions.ConvertDateTimetoStringShowDate(objDateTimeFirstRem);
                            //lblMsgLine2.Text = "Final Reminder is not due, First Reminder sent on " + stringd1stdate + " only. Do you wants to generate Final Reminder?";
                            //Modelpopupeconfirm.Show();
                        }
                    }
                    else
                    {
                        CommonFunctions.ShowMessage("First Reminder not yet generated");
                    }

                }
                else if (rbtncancellation.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700005R1V1", "cancellation_requests");
                    }
                }
                else if (rbtnEnvelopletter.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700016R1V1", "envelope_letter");
                    }
                }
                else if (rbtnwaiverform.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700026R1V1", "waiver_letter");
                    }
                }
                else if (rbtnmrassesment.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700006R1V1", "mr_assessment_defaulter");
                    }
                }
                else if (rbtnsimplemedicalreort.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700013R1V1", "simple_medical_report");
                    }
                }
                else if (rbtnoutstanding.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700007R1V1", "outstanding_status");
                    }
                }
                else if (rbtnconsent.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700002R1V1", "consent_form");
                    }
                }
                else if (rbtnmedicalreport.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700015R1V1", "mr_processing");
                    }
                }
                //else if (rbtnletterundertaking.Checked == true)
                //{
                //    string stringreport = txtReqNo.Text.ToString();
                //    if (stringreport.Trim().Length > 0 && stringreport != null)
                //    {
                //        LoadReportsprinterprinter("DOP700027R1V1", "undertaking_letter");
                //    }
                //}
                else if (rbtnsplistreport.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700028R1V1", "Specialist_letter");
                    }
                }
                //else if (rbtnoverpayment.Checked == true)
                //{
                //    stringreport = txtReqNo.Text.ToString();
                //    if (stringreport.Trim().Length > 0 && stringreport != null)
                //    {
                //        LoadReportsprinterprinter("DOP700029R1V1", "Overpayment_Letter");
                //    }
                //}
                else if (rbtnnodletter.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700030R1V1", "NOD_Letter");
                    }
                }
                else if (rbtnstandardnodletter.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700031R1V1", "Standard_NOD_Letter");
                    }
                }
                else if (rbtnpaymentAcknowlege.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReportsprinterprinter("DOP700032R1V1", "Payment_Acknowledgement_Letter");
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, "Please Choose a Report");
                    mdlrbt.Show();
                    pnlreportpopup.Visible = true;
                    rbtnrequestormail.Checked = true;
                }

                Reportpopupclearvalues();

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringreport = null;
            }
        }

        private void Reportpopupclearvalues()
        {
            try
            {
                rbtnmedicalreport.Checked = false;
                //rbtnletterundertaking.Checked = false;
                rbtnpendingitemfirst.Checked = false;
                rbtnconsent.Checked = false;
                rbtnpaymentAcknowlege.Checked = false;
                rbtnpendingitemfinal.Checked = false;
                rbtnnorecord.Checked = false;
                rbtndefaultclinicreview.Checked = false;
                rbtncancellation.Checked = false;
                rbtndefaultclinicfinal.Checked = false;
                rbtnoutstanding.Checked = false;
                rbtnmrassesment.Checked = false;
                rbtnacknoeledge.Checked = false;
                rbtnworkcompensation.Checked = false;
                rbtnwaiverform.Checked = false;
                rbtnrefundletter.Checked = false;
                rbtnhospitalization.Checked = false;
                rbtnsimplemedicalreort.Checked = false;
                rbtnpadientadd.Checked = false;
                rbtnrequestor.Checked = false;
                rbtncoverletter.Checked = false;
                rbtnpartialrefundletter.Checked = false;
                rbtnnotreportassenment.Checked = false;
                rbtnworkcompensation.Checked = false;
                rbtnEnvelopletter.Checked = false;
                rbtnnodletter.Checked = false;
                // rbtnoverpayment.Checked = false;
                rbtnsplistreport.Checked = false;
                rbtnstandardnodletter.Checked = false;

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private void LoadReportsprinterprinter(string stringFORMID, string stringreportname)//completed
        {

            byte[] objbytereturn = null;
            string stringFile = string.Empty;
            string stringoutputresult = "";
            int intErrorCount = 0;
            string[] stringOutputResult = null;
            string[] stringInputs = new string[5];
            string[] stringSettings = new string[21];
            string stringbeid = "";
            string stringaddress = "";
            string stringTelephoneNo = "";
             
            string stringfromdate = "";
            string stringtodate = "";

            stringbeid = CommonFunctions.GETBussinessEntity();
            stringaddress = ConfigurationManager.AppSettings["BEIDAddress"];
            stringTelephoneNo = ConfigurationManager.AppSettings["BEIDTelephoneNo"];
            string stringUserDisplayName = "";
            string stringPatientAddress = "";
            string stringRQQPatientAddress = "";
            string stringmailaddPatientAddress = "";
            string stringADDRESS = "";
            string stringRequestID01 = txtRequestNo.Text.ToString(); 
            string StringddlExportFormat = string.Empty; 
            string stringRequetstorothers = "";
            string stringRequestid = "";
            string stringRequestorname = "";
            string stringreqID = "";

            try
            {
                if (stringRequestID01.Length > 0)
                {
                    if (Session["G11EOSUser_Name"] != null)
                    {
                        stringUserDisplayName = Session["G11EOSUser_Name"].ToString();
                    } 
                    if (Session["PAT_ADDRESS"] != null)
                    { stringPatientAddress = Session["PAT_ADDRESS"].ToString(); }

                    if (Session["ReqAdd_ADDRESS"] != null)
                    { stringRQQPatientAddress = Session["ReqAdd_ADDRESS"].ToString(); }

                    if (Session["MailAdd_ADDRESS"] != null)
                    { stringmailaddPatientAddress = Session["MailAdd_ADDRESS"].ToString(); }


                    if (rbtnrequestor.Checked)
                    {
                        stringADDRESS = stringRQQPatientAddress.Trim();
                        if (ViewState["REQUESTOR_SHORT_NAME"] != null)
                        {
                            stringRequestorname = ViewState["REQUESTOR_SHORT_NAME"].ToString();
                        }
                        if (ViewState["rptreq_id"] != null)
                        {
                            stringRequestid = ViewState["rptreq_id"].ToString();
                        }

                        if (ViewState["REQUEST_OTHERS"] != null)
                        {
                            stringRequetstorothers = ViewState["REQUEST_OTHERS"].ToString();
                        }
                    }
                    else if (rbtnpadientadd.Checked)
                    {
                        stringADDRESS = stringPatientAddress.Trim();
                    }
                    else if (rbtnrequestormail.Checked)
                    {
                        stringADDRESS = stringmailaddPatientAddress.Trim();
                        if (ViewState["REQUESTOR_SHORT_NAME"] != null)
                        {
                            stringRequestorname = ViewState["REQUESTOR_SHORT_NAME"].ToString();
                        }
                        if (ViewState["rptreq_id"] != null)
                        {
                            stringRequestid = ViewState["rptreq_id"].ToString();
                        }

                        if (ViewState["REQUEST_OTHERS"] != null)
                        {
                            stringRequetstorothers = ViewState["REQUEST_OTHERS"].ToString();
                        }
                    }
                    if (stringFORMID == "DOP700009R1V1")
                    {
                        stringInputs = new string[7];
                        stringInputs[0] = CommonFunctions.GETBussinessEntity();
                        stringInputs[1] = stringRequestID01;
                        stringInputs[2] = stringUserDisplayName.ToString();
                        stringInputs[3] = "";
                        stringInputs[4] = "";
                        stringInputs[5] = "";
                        stringInputs[6] = "";
                    }
                    else if (stringFORMID == "DOP700025R1V1")
                    {
                        stringInputs = new string[6];
                        stringInputs[0] = CommonFunctions.GETBussinessEntity();
                        stringInputs[1] = stringRequestID01;
                        stringInputs[2] = stringUserDisplayName.ToString();
                        stringInputs[3] = txtnomonths.Text.ToString();
                        stringInputs[4] = "";
                        stringInputs[5] = "";
                    }
                    else if (stringFORMID == "DOP700003R1V1")
                    {
                        stringInputs = new string[4];
                        stringInputs[0] = CommonFunctions.GETBussinessEntity();
                        stringInputs[1] = stringRequestID01;
                        stringInputs[2] = "";
                        stringInputs[3] = ""; 
                    }
                    else if (stringFORMID == "DOP700004R1V1")
                    {
                        stringInputs = new string[4];
                        stringInputs[0] = CommonFunctions.GETBussinessEntity();
                        stringInputs[1] = stringRequestID01;
                        stringInputs[2] = "";
                        stringInputs[3] = ""; 
                    }
                    else if (stringFORMID == "DOP700002R1V1")
                    {
                        stringInputs = new string[4];
                        stringInputs[0] = CommonFunctions.GETBussinessEntity();
                        stringInputs[1] = stringRequestID01;
                        stringInputs[2] = "";
                        stringInputs[3] = ""; 
                    }
                    else if (stringFORMID == "DOP700015R1V1")
                    {
                        stringInputs = new string[4];
                        stringInputs[0] = CommonFunctions.GETBussinessEntity();
                        stringInputs[1] = stringRequestID01;
                        stringInputs[2] = "";
                        stringInputs[3] = ""; 
                    }

                    else
                    {
                        stringInputs[0] = CommonFunctions.GETBussinessEntity();
                        stringInputs[1] = stringRequestID01;
                        stringInputs[2] = stringUserDisplayName.ToString();
                        stringInputs[3] = "";
                        stringInputs[4] = "";
                    }

                    if (Session["PAT_ADDRESS"] != null)
                    { stringPatientAddress = Session["PAT_ADDRESS"].ToString(); }
                    if (Session["ReqAdd_ADDRESS"] != null)
                    { stringRQQPatientAddress = Session["ReqAdd_ADDRESS"].ToString(); }

                    if (Session["MailAdd_ADDRESS"] != null)
                    { stringmailaddPatientAddress = Session["MailAdd_ADDRESS"].ToString(); }
                    if (rbtnrequestor.Checked)
                    {
                        stringADDRESS = stringRQQPatientAddress.Trim();
                        if (ViewState["REQUESTOR_SHORT_NAME"] != null)
                        {
                            stringRequestorname = ViewState["REQUESTOR_SHORT_NAME"].ToString();
                        }
                        if (ViewState["rptreq_id"] != null)
                        {
                            stringRequestid = ViewState["rptreq_id"].ToString();
                        }

                        if (ViewState["REQUEST_OTHERS"] != null)
                        {
                            stringRequetstorothers = ViewState["REQUEST_OTHERS"].ToString();
                        }
                    }
                    else if (rbtnpadientadd.Checked)
                    {
                        stringADDRESS = stringPatientAddress.Trim();
                    }
                    else if (rbtnrequestormail.Checked)
                    {
                        stringADDRESS = stringmailaddPatientAddress.Trim();
                        if (ViewState["REQUESTOR_SHORT_NAME"] != null)
                        {
                            stringRequestorname = ViewState["REQUESTOR_SHORT_NAME"].ToString();
                        }
                        if (ViewState["rptreq_id"] != null)
                        {
                            stringRequestid = ViewState["rptreq_id"].ToString();
                        }

                        if (ViewState["REQUEST_OTHERS"] != null)
                        {
                            stringRequetstorothers = ViewState["REQUEST_OTHERS"].ToString();
                        }
                    }
                    if (stringFORMID == "DOP700011R1V1" || stringFORMID == "DOP700028R1V1" || stringFORMID == "DOP700013R1V1" || stringFORMID == "DOP700007R1V1" || stringFORMID == "DOP700020R1V1" || stringFORMID == "DOP700008R1V1" || stringFORMID == "DOP700025R1V1" || stringFORMID == "DOP700003R1V1" || stringFORMID == "DOP700004R1V1" || stringFORMID == "DOP700022R1V1" || stringFORMID == "DOP700018R1V1" || stringFORMID == "DOP700019R1V1")
                    {
                        stringSettings = new string[21];
                    }

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

                    string stringLofoFlag = LoadINSTLogo();
                    stringSettings[19] = "Print_flag" + "-->" + stringLofoFlag.ToString();
                    if (stringRequestid != null && stringRequestid == "SELF")
                    {
                        //stringSettings[20] = "PatientName" + "-->" + txtPatientNameHEADER.Text.ToString();
                        stringSettings[20] = "PatientName" + "-->" + stringRequestorname.ToString();
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
                        stringSettings[20] = "PatientName" + "-->" + hdfpatientname.Value.ToString();
                    }



                    objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                    objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = stringFORMID;
                    clsCertificateValidation.EnableTrustedHosts();

                    using (GSReportingService.ReportingServiceClient objSGHCCESServiceClient = new GSReportingService.ReportingServiceClient(CommonFunctions.CreateServiceBasicHttpBinding(), CommonFunctions.objEndpointAddressReportingService))
                    {
                        stringoutputresult = objSGHCCESServiceClient.GetDocumentReport(stringInputs, objDatasetAppsVariables, stringSettings, out objbytereturn, out intErrorCount, out stringOutputResult);
                        if (objSGHCCESServiceClient != null)
                            objSGHCCESServiceClient.Close();
                    }
                    if (intErrorCount == 0)
                    {
                        if (stringoutputresult != null && stringoutputresult.Length > 0)
                        {
                            if (stringRequestID01.Length > 0)
                            {
                                stringreqID = stringRequestID01.Trim().ToString().Replace('/', '_');
                            }
                            CommonFunctions.OpenExportedFileR1V1LETTERReport(this, objbytereturn, stringreqID, stringreportname.ToString(), "REPORT");
                            rbtnrequestormail.Checked = true;
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Report Not Found");
                        }
                        mdlrbt.Show();

                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                        mdlrbt.Show();
                        pnlreportpopup.Visible = true;
                    }
                    mdlrbt.Show();
                    pnlreportpopup.Visible = true;
                    txtnomonths.Text = "";
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {


            }
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            try
            {
                mdlrbt.Hide();
                pnlreportpopup.Visible = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }
        private bool UpdateFirstReminderPrintDate(string stringRequestID) 
        {
            DataRow objdatarow = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FC0006R1V2";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            bool boolStatus = false;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                if (stringRequestID != null && stringRequestID.Trim().Length > 0)
                {

                    string stringServiceType = "DEFAULT";
                    string stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                    if (objDatasetResult != null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                    {

                        objdatarow = objDatasetResult.Tables["t1"].NewRow();

                        objdatarow["be_id"] = stringbeid;
                        objdatarow["request_id"] = stringRequestID.Trim().ToUpper();
                        // objdatarow["remarks"] = txtRemarks.Text.Trim().ToUpper();                     
                        objdatarow["delmark"] = "N";

                        if (Session["stringComputerName"] != null)
                            objdatarow["CREATED_AT"] = Session["stringComputerName"].ToString();
                        if (Session["stringUserID"] != null)
                            objdatarow["CREATED_BY"] = Session["stringUserID"].ToString();
                        objdatarow["CREATED_ON"] = DateTime.Now;
                        if (Session["stringComputerName"] != null)
                            objdatarow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                        if (Session["stringUserID"] != null)
                            objdatarow["MODIFIED_BY"] = Session["stringUserID"].ToString();
                        objdatarow["MODIFIED_ON"] = DateTime.Now;
                        objDatasetResult.Tables["t1"].Rows.Add(objdatarow);

                        boolStatus = true;

                        if (boolStatus == true)
                        {
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
                    }


                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objdatarow = null;
                interrorcount = 0;
                intTotalRecord = 0;
                objDatasetResult = null;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;

            }


            return false;
        }

        private bool UpdateSecondReminderPrintDate(string stringRequestID) 
        {

            DataRow objdatarow = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FC0006R1V2";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            bool boolStatus = false;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                if (stringRequestID != null && stringRequestID.Trim().Length > 0)
                {

                    string stringServiceType = "DEFAULT";
                    string stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                    if (objDatasetResult != null && objDatasetResult.Tables["t2"].Rows.Count == 0)
                    {

                        objdatarow = objDatasetResult.Tables["t2"].NewRow();

                        objdatarow["be_id"] = stringbeid;
                        objdatarow["request_id"] = stringRequestID.Trim().ToUpper();
                        // objdatarow["remarks"] = txtRemarks.Text.Trim().ToUpper();                     
                        objdatarow["delmark"] = "N";

                        if (Session["stringComputerName"] != null)
                            objdatarow["CREATED_AT"] = Session["stringComputerName"].ToString();
                        if (Session["stringUserID"] != null)
                            objdatarow["CREATED_BY"] = Session["stringUserID"].ToString();
                        objdatarow["CREATED_ON"] = DateTime.Now;
                        if (Session["stringComputerName"] != null)
                            objdatarow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                        if (Session["stringUserID"] != null)
                            objdatarow["MODIFIED_BY"] = Session["stringUserID"].ToString();
                        objdatarow["MODIFIED_ON"] = DateTime.Now;
                        objDatasetResult.Tables["t2"].Rows.Add(objdatarow);

                        boolStatus = true;

                        if (boolStatus == true)
                        {
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
                    }


                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objdatarow = null;
                interrorcount = 0;
                intTotalRecord = 0;
                objDatasetResult = null;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;

            }


            return false;
        }

        private void SecondRemLetterPrint()
        {
            string stringreport = "";
            bool boolPrint = true;
            DateTime objDateTimeFirstRem;
            DateTime objDateTimeSecRem; 
            try
            {
                stringreport = txtRequestNo.Text.ToString();
                if (txtSecRemPrintedOn.Text.Trim().Length > 0)
                {
                    if (txtFirstRemPrintedOn.Text.Trim().Length > 0)
                    {
                        objDateTimeFirstRem = CommonFunctions.ConvertToDateTime(txtFirstRemPrintedOn.Text.Trim(), "dd-MM-yyyy");
                        objDateTimeSecRem = CommonFunctions.ConvertToDateTime(txtSecRemPrintedOn.Text.Trim(), "dd-MM-yyyy");

                        if (objDateTimeSecRem.Date > DateTime.Now.Date)
                        {
                            boolPrint = false;
                            CommonFunctions.ShowMessageboot(this, "First reminder printed on " + objDateTimeFirstRem.ToString("dd-MMM-yyyy") + ". You cannot print second reminder before " + objDateTimeSecRem.ToString("dd-MMM-yyyy"));

                        }
                    }

                }

                if (boolPrint)
                {
                    if (txtSecRemPrintedOn.Text.Trim().Length == 0)
                    {
                        if (UpdateSecondReminderPrintDate(stringreport.Trim().ToUpper()))
                        {
                            LoadRegRequestInfo(stringreport);
                        }
                        LoadReportsprinterprinter("DOP700004R1V1", "SecondReminderLetter");
                    }
                    else
                    {
                        LoadReportsprinterprinter("DOP700004R1V1", "SecondReminderLetter");
                    }

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }


        private void ReportRelatedValues(DataRow objDataRow)
        {
            try
            {
                hdfreporttype.Value = objDataRow["rpttyp_id"].ToString();//rpt
                hdfpatientname.Value = objDataRow["patient_short_name"].ToString();//rpt
                hdfCANRefundamt.Value = objDataRow["REFUND_AMT"].ToString();//rpt
                if (objDataRow["first_rem_print_date"] != null && objDataRow["first_rem_print_date"].ToString().Trim().Length > 0)//rpt
                { txtFirstRemPrintedOn.Text = Convert.ToDateTime(objDataRow["first_rem_print_date"]).ToString("dd-MM-yyyy"); }//rpt

                if (objDataRow["second_rem_printed_date"] != null && objDataRow["second_rem_printed_date"].ToString().Trim().Length > 0)//rpt
                { txtSecRemPrintedOn.Text = Convert.ToDateTime(objDataRow["second_rem_printed_date"]).ToString("dd-MM-yyyy"); }

                if (txtSecRemPrintedOn.Text.Length > 0)//rpt
                {
                    rbtnpendingitemfirst.Enabled = false;
                }
                else//rpt
                {
                    rbtnpendingitemfirst.Enabled = true;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
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

        #endregion
    }
}