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
    public partial class FC0005R1V1 : System.Web.UI.Page
    {
        public int pageIndex = 0;
        public int recFrom = 0;
        public int recTo = 0;
        public string stringformIdPaging = "FC0005R1V1gridviewpagesize";
        public DataSet objDatasetAppsVariables;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            string stringRequestID = "";
            string stringENQUIRYID = "";
            try
            {
                if (CommonFunctions.IsActive())
                {
                    if (Session["REQUESTID_ENQUIRY"] != null)
                    {
                        stringRequestID = Session["REQUESTID_ENQUIRY"].ToString();
                        Session["REQUESTID_ENQUIRY"] = stringRequestID;
                    }
                    else
                    {
                        Session["REQUESTID_ENQUIRY"] = null;
                    }

                    if (Session["ENQUIRYID"] != null)
                    {
                        stringENQUIRYID = Session["ENQUIRYID"].ToString();
                        Session["ENQUIRYID"] = stringENQUIRYID;
                    }
                    recTo = CommonFunctions.GridViewPagesize(stringformIdPaging);
                    if (!IsPostBack)
                    {
                        CommonFunctions.HeaderName(this, "FC0005R1V1");
                        VerifyAccessRights();
                        hdfmramount.Value = "";
                        hdfddlBlockBill.Value = "";
                        hdfddlWApp.Value = "";
                        hdfddlWApproved.Value = "";
                        hdfRecallcurreentStatus.Value = "";
                        ViewState["vsSortDirection"] = "ASC";
                        ViewState["vsSortExpression"] = "";
                        ResetVariables();
                        LoadEnquiryStatus();
                        ClearValues();
                        LoadRegRequestInfo(stringRequestID);
                        LoadGrid(stringRequestID);
                         
                        if (stringRequestID != null && stringRequestID.Trim().Length > 0)
                        { LoadData(stringENQUIRYID); }

                        if (stringENQUIRYID != null && stringENQUIRYID.Trim().Length > 0)
                        { LoadDatavalues(stringENQUIRYID); }
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
                stringENQUIRYID = "";
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
                stringComponent[0] = "FC0005R1V1";
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
        private bool IsRecordExist(string stringSpecialInformationsID)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0003R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            DataTable objDataTable = null;
            string stringExpression = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                stringServiceType = "List1R1V1";

                stringExpression = "And mreh.be_id= '" + stringbeid + "'  and mreh.request_id ='" + stringSpecialInformationsID + "' ";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t1"];
                        return true;
                    }
                    else
                    {
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
                stringServiceType = "";
                objDataTable = null;
                stringExpression = "";
                stringbeid = "";
            }
            return false;
        }

        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                SaveEnquiry();

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private void SaveEnquiry()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0003R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            DataRow objdatarow = null;
            string stringdepid1 = "";
            bool boolsuccuss = false;
            bool boolCanContinue = true;
            string stringServiceType1 = "";
            string stringexp = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringexp012 = "";
            try
            {
                if (ValidateControls() && ValidateBusinessLogic())
                {
                    boolCanContinue = true;
                    stringServiceType1 = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                        {
                            if (Session["mrp_idFC0003R1V1"] == null)
                            {
                                if (Session["mrp_idFC0003R1V1"] == null)
                                {
                                    //boolCanContinue = !IsRecordExist(txtRequestNo.Text.Trim());
                                }
                                if (boolCanContinue)
                                {
                                    objdatarow = objDatasetResult.Tables["t1"].NewRow();

                                    objdatarow["Request_ID"] = txtRequestNo.Text.Trim();
                                    objdatarow["Callers_Name"] = txtCName.Text.Trim().ToUpper();
                                    objdatarow["Callers_Enquiry"] = txtCEnq.Text.Trim().ToUpper();
                                    objdatarow["Staffs_Name"] = txtSName.Text.Trim().ToUpper();
                                    objdatarow["Staffs_Response"] = txtSRes.Text.Trim().ToUpper();
                                    if (ViewState["hidFldEnqID"] != null)
                                    {
                                        objdatarow["MR_Enq_ID"] = ViewState["hidFldEnqID"].ToString();
                                    }
                                    objdatarow["Remarks"] = txtRemarks.Text.Trim().ToUpper();
                                    if (ddlEnqStatus.SelectedItem != null)
                                    {
                                        objdatarow["reference_1"] = ddlEnqStatus.SelectedItem.Value.ToString();
                                    }

                                    objdatarow["delmark"] = "N";
                                    objdatarow["be_id"] = Session["BusinessID"].ToString();

                                    CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                                    objDatasetResult.Tables["t1"].Rows.Add(objdatarow);
                                    boolsuccuss = true;
                                }
                                else
                                {
                                    CommonFunctions.ShowMessageboot(this, "Item already exist");
                                }
                            }
                            else
                            {
                                if (Session["mrp_idFC0003R1V1"] != null)
                                    stringdepid1 = Session["mrp_idFC0003R1V1"].ToString();
                                stringServiceType = "List1R1V1";
                                stringexp012 = "And mreh.be_id= '" + stringbeid + "' And mreh.mr_enq_id = '" + stringdepid1.ToString() + "'";
                                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                                if (interrorcount == 0)
                                {
                                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                                    {
                                        objDatasetResult.Tables["t1"].Rows[0]["Request_ID"] = txtRequestNo.Text.Trim();
                                        objDatasetResult.Tables["t1"].Rows[0]["Callers_Name"] = txtCName.Text.Trim().ToUpper();
                                        objDatasetResult.Tables["t1"].Rows[0]["Callers_Enquiry"] = txtCEnq.Text.Trim().ToUpper();
                                        objDatasetResult.Tables["t1"].Rows[0]["Staffs_Name"] = txtSName.Text.Trim().ToUpper();
                                        objDatasetResult.Tables["t1"].Rows[0]["Staffs_Response"] = txtSRes.Text.Trim().ToUpper();
                                        if (ViewState["hidFldEnqID"] != null)
                                        {
                                            objDatasetResult.Tables["t1"].Rows[0]["MR_Enq_ID"] = ViewState["hidFldEnqID"].ToString();
                                        }

                                        objDatasetResult.Tables["t1"].Rows[0]["Remarks"] = txtRemarks.Text.Trim().ToUpper();
                                        objDatasetResult.Tables["t1"].Rows[0]["reference_1"] = ddlEnqStatus.SelectedItem.Value.ToString();

                                        objDatasetResult.Tables["t1"].Rows[0]["delmark"] = "N";

                                        if (Session["stringComputerName"] != null)
                                            objDatasetResult.Tables["t1"].Rows[0]["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                        if (Session["G11EOSUser_Name"] != null)
                                            objDatasetResult.Tables["t1"].Rows[0]["MODIFIED_BY"] = Session["G11EOSUser_Name"].ToString();
                                        objDatasetResult.Tables["t1"].Rows[0]["MODIFIED_ON"] = DateTime.Now;
                                        objDatasetResult.Tables["t1"].Rows[0].RowState.ToString();
                                        boolsuccuss = true;
                                    }
                                }
                                else
                                {
                                    Errorpopup(stringOutputResult);
                                }
                            }
                        }
                        if (boolsuccuss)
                        {
                            objDatasetResult = objDatasetResult.GetChanges();
                            stringServiceType = "OperationServiceDML";
                            objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                            if (intErrorCount == 0)
                            {
                                LoadGrid(txtRequestNo.Text.Trim());
                                if (Session["stringDMLIndicator"].ToString() == "I")
                                {
                                    CommonFunctions.ShowMessageboot(this, "Record saved Successfully");
                                }
                                else
                                {
                                    CommonFunctions.ShowMessageboot(this, "Record updated Successfully");
                                }
                                Session["mrp_idFC0003R1V1"] = null;

                                ClearValues();
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
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = "";
                objdatarow = null;
                stringdepid1 = "";
                boolsuccuss = false;
                boolCanContinue = true;
                stringServiceType1 = "";
                stringexp = "";
                stringbeid = "";
                stringexp012 = "";
            }
        }

        protected void LkBtnBack_Click(object sender, EventArgs e)//fixed
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

        protected void lnkbtnEnq_Date_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string stringbedep = "";
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
                            stringbedep = stringValues[0];
                            Session["mrp_idFC0003R1V1"] = stringbedep;
                            LoadDatavalues(stringbedep);
                            if (Session["SortTable"] != null)
                            {
                                gvList.DataSource = (DataTable)Session["SortTable"];
                                gvList.DataBind();
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
                stringCmdArgument = "";
                stringbedep = "";
                stringValues = null;
            }
        } 

        private void LoadEnquiryStatus()//fix
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
            string stringcondition = "";
            string stringServiceType = "";
            try
            {

                ddlEnqStatus.Items.Clear();

                stringcondition = "And lst.be_id= '" + stringbeid + "'  AND  lst.LSTGRP_ID like '%MRENQUIRY_STATUS%' AND lst.delmark='N' ";

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
                        ddlEnqStatus.DataTextField = "SHORT_NAME";
                        ddlEnqStatus.DataValueField = "LST_ID";
                        ddlEnqStatus.DataSource = objDataTable;
                        ddlEnqStatus.DataBind();
                        ddlEnqStatus.Items.Insert(0, new ListItem("", ""));
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
                stringbeid = "";
                objDataTable = null;
                stringcondition = "";
                stringServiceType = "";
            }
        }

        private void LoadDatavalues(string stringdesg_id)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0003R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            DataTable objDataTable = null;
            DataRow[] objdatarow = null; 
            string stringTemp = "";
            string stringRequestID = "";
            string stringServiceType = "";
            string stringexp012 = "";
            string stringStatus = "";
            if (Session["REQUESTID_ENQUIRY"] != null)
            {
                stringRequestID = Session["REQUESTID_ENQUIRY"].ToString();
                Session["REQUESTID_ENQUIRY"] = stringRequestID;
            }
            try
            {
                if (Session["SortTable"] != null)
                {
                    objDataTable = (DataTable)Session["SortTable"];
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        ViewState["exportconditiondesig"] = "and be_id= '" + stringbeid + "' And mr_enq_id = '" + stringdesg_id.ToString() + "'";
                        objdatarow = objDataTable.Select("be_id= '" + stringbeid + "' and mr_enq_id = '" + stringdesg_id.ToString() + "'");
                        if (objdatarow != null && objdatarow.Length > 0)
                        {
                            Session["AuditLogdepartment"] = objdatarow;
                              
                            stringTemp = objdatarow[0]["mr_enq_id"].ToString();
                            ViewState["hidFldEnqID"] = objdatarow[0]["mr_enq_id"].ToString();

                            stringServiceType = "List1R1V1";
                            stringexp012 = "And mreh.be_id= '" + stringbeid + "'  And mreh.mr_enq_id= '" + stringRequestID + "' ";

                            objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                            if (interrorcount == 0)
                            {
                                if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                                {
                                    objDataTable = objDatasetResult.Tables["t1"];
                                }
                                if (objDataTable != null && objDataTable.Rows.Count > 0)
                                {
                                    txtRemarks.Text = objDataTable.Rows[0]["remarks"].ToString();

                                }
                            }
                            else
                            {
                                Errorpopup(stringOutputResult);
                            }

                            txtSName.Text = objdatarow[0]["staffs_name"].ToString().Trim().ToUpper();
                            txtCName.Text = objdatarow[0]["callers_name"].ToString().Trim().ToUpper();
                            txtSRes.Text = objdatarow[0]["staffs_response"].ToString().Trim().ToUpper();
                            txtCEnq.Text = objdatarow[0]["callers_enquiry"].ToString().Trim().ToUpper();

                            stringStatus = objdatarow[0]["reference_1"].ToString().Trim().ToUpper();
                            if (stringStatus != null && stringStatus.Trim().Length > 0)
                            {
                                if (ddlEnqStatus.Items.FindByValue(stringStatus) != null)
                                { ddlEnqStatus.ClearSelection(); ddlEnqStatus.Items.FindByValue(stringStatus).Selected = true; }
                            }

                            Session["stringDMLIndicator"] = "U";
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
                stringbeid = "";
                objDataTable = null;
                objdatarow = null;
                stringTemp = "";
                stringRequestID = "";
                stringServiceType = "";
                stringexp012 = "";
                stringStatus = "";
            }
        }
         
        private void LoadRegRequestInfo(string stringRequestNo)//fix
        {
            DataTable objDataTable = null;
            DataRow objDataRow = null;
            string stringReportTypID = "";
            string stringStatus = "";
            string stringMRamt = "";
            string stringUserID = "";
            string stringUserDesc = "";
            string stringcancellstatus = ""; 
            try
            {
                if (stringRequestNo != null && stringRequestNo.Trim().Length > 0)
                {
                    objDataTable = GetRequestDetails(stringRequestNo);
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        objDataRow = objDataTable.Rows[0];
                        LoadProcesstabData(objDataRow);
                        LoadRemarks(stringRequestNo);

                        if (objDataRow["receive_date"] != null && objDataRow["receive_date"].ToString().Trim().Length > 0)
                        { txtRecDate.Text = Convert.ToDateTime(objDataRow["receive_date"]).ToString("dd-MM-yyyy"); }

                        if (objDataRow["due_date"] != null && objDataRow["due_date"].ToString().Trim().Length > 0)
                        { txtDueDate.Text = Convert.ToDateTime(objDataRow["due_date"]).ToString("dd-MM-yyyy"); }


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
                        stringUserDesc = objDataRow["CREATED_BY"].ToString();
                        if (stringUserDesc != null && stringUserDesc.Trim().Length > 0) { txtCreateBy.Text = stringUserDesc; }
                        txtCreateBy.Text = objDataRow["CREATED_BY"].ToString();
                        txtReqName.Text = objDataRow["requested_by"].ToString();
                        txtReqContact.Text = objDataRow["req_by_phno"].ToString();
                        stringcancellstatus = objDataRow["sup_status"].ToString();
                        if (stringStatus.Trim().ToUpper() == "FORWARDED" || stringStatus.Trim().ToUpper() == "COLLECTED" || stringStatus.Trim().ToUpper() == "CANCELLED" || stringcancellstatus.ToUpper() == "PENDING")
                        { ControlsEnabledByProStatus("CANCELLED"); }
                        else { }
                    }
                    else
                    {
                        txtRequestNo.Text = txtMRNumberHEADER.Text = txtMRStatus.Text = "";
                        chkpriorityflag.Checked = false;
                        txtWritingandVerifyingStatus.Text = "";
                    }
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
                stringReportTypID = "";
                stringStatus = "";
                stringMRamt = "";
                stringUserID = "";
                stringUserDesc = "";
                stringcancellstatus = ""; 
            }
        }

        private DataTable LoadRemarks(string stringReqID)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0003R1V1"; 
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            try
            {
                if (Session["stringRequestID1"] != null)
                {
                    stringReqID = (string)Session["stringRequestID1"];
                }
                if (stringReqID != null && stringReqID.Trim().Length > 0)
                {
                    stringServiceType = "List1R1V1";
                    stringexp012 = "And mreh.be_id= '" + stringbeid + "'  And mreh.request_id= '" + stringReqID + "' ";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, "mreh.modified_on DESC", intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t1"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            txtRemarks.Text = objDataTable.Rows[0]["remarks"].ToString();

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
                intFromRecord = 0;
                intToRecord = 0;
                objDataTable = null;
                stringbeid = "";
                stringServiceType = "";
                stringexp012 = "";
            }
            return null;
        }

        private DataTable GetRequestDetails(string stringReqID)//fix
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

        private void ControlsEnabledByProStatus(string stringStatus)//fix
        {
            switch (stringStatus)
            {
                case "CANCELLED":
                    {


                        //imgBtnNew.Enabled = true;
                        //imgBtnSave.Enabled = true;
                        //imgBtnDelete.Enabled = true;
                        //imgBtnClear.Enabled = true;
                        //imgBtnSearch.Enabled = true;
                        //imgBtnPrint.Enabled = true;
                        //imgBtnSetting.Enabled = true;
                        //imgBtnSecurity.Enabled = true;

                        txtRemarks.ReadOnly = false;
                        txtCName.ReadOnly = false;
                        txtCEnq.ReadOnly = false;
                        txtSName.ReadOnly = false;
                        txtSRes.ReadOnly = false;

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

                        txtRemarks.ReadOnly = false;
                        txtCName.ReadOnly = false;
                        txtCEnq.ReadOnly = false;
                        txtSName.ReadOnly = false;
                        txtSRes.ReadOnly = false;
                        break;
                    }
            }
        }

        private void ClearValues()//fix
        {
            try
            {
                txtCName.Text = "";
                txtCEnq.Text = "";
                ddlEnqStatus.SelectedIndex = 0;
                Session["mrp_idFC0003R1V1"] = null;
                Session["stringDMLIndicator"] = "I";
                txtSName.ToolTip = Session["G11EOSUser_Name"].ToString();
                txtSName.Text = Session["G11EOSUser_Name"].ToString();
                txtSRes.Text = "";
                txtRemarks.Text = "";
                if (ddlEnqStatus.Items.FindByValue("PENDING") != null) { ddlEnqStatus.ClearSelection(); ddlEnqStatus.Items.FindByValue("PENDING").Selected = true; }
                ViewState["hidFldEnqID"] = null;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        private void LoadGrid(string stringRequestID)//fix
        {
            string stringBEID = CommonFunctions.GETBussinessEntity().ToString();
            try
            {
                string stringexp012 = "And mreh.be_id= '" + stringBEID + "' And mreh.request_id= '" + stringRequestID + "' ";
                LoadRecord(stringexp012);
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringBEID = "";
            }
        }
        private void Errorpopup(string[] stringOutputResult)//fix
        {
            try
            {
                lblErrorType.Text = stringOutputResult[1];
                lblErrorCode.Text = stringOutputResult[0];
                lblsysseqno.Text = stringOutputResult[3];
                txterrormsg.Text = stringOutputResult[2];
                Modelpopuperror.Show();
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
         

        private bool ValidateControls()//fix
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;

                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";



                if (txtCName.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Callers Name" + "\\r\\n";
                    boolStatus = false;
                }

                if (txtCEnq.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Caller's Enquiry" + "\\r\\n";
                    boolStatus = false;
                }

                if (txtSName.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Staff's Name" + "\\r\\n";
                    boolStatus = false;
                }



                if (txtCName.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Callers Name" + "\\r\\n";
                    boolStatus = false;

                }

                if (ddlEnqStatus.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Enquiry status" + "\\r\\n";
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
            }
            
            return boolStatus;
        }

        private bool ValidateBusinessLogic() 
        {
            bool boolStatus = true;

            try
            {


            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return false;
            }
            finally
            {
                //boolStatus = true;
            }
            return boolStatus;
        }

        private void ResetVariables()//fix
        {
            try
            {
                Session["stringDMLIndicator"] = "I";
                Session["stringSortDirection"] = "ASC";
                Session["stringSortExpression"] = "";
                Session["stringFormID"] = "FC0003R1V1";
                Session["stringFormName"] = "MR Enquiry History";
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
           
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringSort = string.Empty;
            DataRow objDataRow = null;
            string stringTemp = "";
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null) { return; }
                 
                objDataRow = ((DataRowView)e.Row.DataItem).Row;
                if (objDataRow != null)
                {
                    stringTemp = objDataRow["reference_1"].ToString();
                    if (stringTemp.Trim().ToUpper() == "PENDING") { ((Label)e.Row.FindControl("lblreference_1")).Text = "PENDING"; }
                    else if (stringTemp.Trim().ToUpper() == "ANSWERED") { ((Label)e.Row.FindControl("lblreference_1")).Text = "COMPLETED"; }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringSort = string.Empty;
                objDataRow = null;
                stringTemp = "";
            }
        }

        private void LoadRecord(string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {
            DataSet objDataSet = null;
            int intRecordCount = 0;
            try
            {
                ViewState["vsSearchCondition"] = Condition;
                intRecordCount = 0;
                objDataSet = GetRecords(out intRecordCount, Condition, SortExpression, RecordFrom, RecordTo);
                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                {
                    Session["SortTable"] = objDataSet.Tables[0];
                    gvList.DataSource = (DataTable)Session["SortTable"];
                    gvList.DataBind();
                    pnlresultgrid.Visible = true;
                    lblTotalRecords.InnerText = objDataSet.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    lblTotalRecords.InnerText = "0";
                    gvList.DataSource = null;
                    gvList.DataBind();
                    pnlresultgrid.Visible = false;
                    Session["SortTable"] = null; 
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        public DataSet GetRecords(out int intRecordCount, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {

            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0003R1V1";
            intRecordCount = 0;
            string stringServiceType = "List1R1V1";

            int intRecordFrom = 0;
            int intRecordTo = 0;
            try
            {
                intRecordFrom = recFrom;
                intRecordTo = recTo;
                ViewState["vsSearchCondition"] = Condition;
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, Condition, "mreh.modified_on desc", intRecordFrom, intRecordTo, out intRecordCount, out interrorcount, out stringOutputResult);
                PopulatePager(intRecordCount, pageIndex);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[0] != null && objDatasetResult.Tables[0].Rows.Count > 0)
                    {

                        return (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[0].Rows.Count > 0) ? objDatasetResult : null;

                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this,"No Records Found");
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
                stringformid = "FC0003R1V1";
                stringServiceType = "List1R1V1";
                intRecordFrom = 0;
                intRecordTo = 0;
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
                        ViewState["vsSortExpression"] = "appt." + stringColumnName + ViewState["vsSortDirection"].ToString();
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
                stringColumnName = "";
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
                stringBoID = CommonFunctions.GETBussinessEntity().ToString();
                //boolstatus = true;
                stringformid01 = "";
                stringexp0121 = "";
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
            string stringOrderBy = "mrdelas.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringServiceType = "";
            string stringexp012 = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
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
                stringServiceType = "";
                stringexp012 = "";
                stringbeid = "" ;
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
            string stringServiceType = "";
            string stringexp = "";
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringprocessname = "";
            string stringRequestID = "";
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
                        if (objDatasetResult != null && objDatasetResult.Tables["t2"] !=null && objDatasetResult.Tables["t2"].Rows.Count == 0)
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
                stringServiceType = "";
                stringexp = "";
                intToRecord = 0;
                stringbeid = "";
                stringprocessname = "";
                stringRequestID = "";
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
                    CommonFunctions.ShowMessageboot(this, "Remarks Field should not be empty,Please enter value");
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
                            string stringRequestID = txtRequestNo.Text.Trim().ToUpper();
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
                intFromRecord = 0;
                intToRecord = 0;
                stringprocessname = "";
                stringbeid = "";
                stringServiceType = "";
                 stringexp = "";
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
            bool boolEnquirypendingItems = true;
            bool boolpaymentpendingItems = true;
            DataRow[] objdatarow = null;
            string stringMRamt = "";
            string stringbalanceamt = ""; 
            int intbalanceamt = 0;
            DataTable objdatatable = null;
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
                //}
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                boolContinue = false;
                stringTransSattus = "";
                objdatatablePendingItems = null;
                boolpendingItems = true;
                boolAssignDoctor = true;
                boolpendingreport = true;
                boolEnquirypendingItems = true;
                boolpaymentpendingItems = true;
                objdatarow = null;
                stringMRamt = "";
                stringbalanceamt = ""; 
                intbalanceamt = 0;
                objdatatable = null;
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
                boolStatus = false;
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
                        btnPendingReport.ForeColor = btnPendingReleasetoHIMS.BackColor = btnPendingSupVetting.ForeColor = btnPendingforwarding.ForeColor = btnPendingCollectInPerson.ForeColor = Color.FromArgb(57, 114, 121);

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
                    //objDueDate = objDueDate01.AddDays(-1);
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