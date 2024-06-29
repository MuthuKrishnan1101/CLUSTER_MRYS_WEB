using CLUSTER_MRTS.CommonFunction;
using G10CertificateValidation;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FC0006R1V1 : System.Web.UI.Page
    {
        public DataSet objDatasetAppsVariables;
        protected void Page_Load(object sender, EventArgs e)
        {

            decimal decimalCancelationCharge = 0;
            decimal decimalTotAmount = 0;
            decimal decimalPaidAmount = 0;
            DataTable objDataTable = null;
            string stringRequestID = "";
            string stringLockFields = "";
            DataRow objDataRow = null;
            try
            {
                if (CommonFunctions.IsActive())
                {
                    if (Session["REQUESTID_CANCELLATION"] != null)
                    {
                        stringRequestID = Session["REQUESTID_CANCELLATION"].ToString();
                        Session["REQUESTID_CANCELLATION"] = stringRequestID;
                    }
                    else
                    {
                        Session["REQUESTID_CANCELLATION"] = null;
                    }
                    if (Request.QueryString["LockFlag"] != null)
                    { stringLockFields = Request.QueryString["LockFlag"].ToString(); }

                    if (!IsPostBack)
                    {
                        CommonFunctions.HeaderName(this, "FC0006R1V1");
                        VerifyAccessRights();

                        txtReject.Enabled = false; 
                        txtReject.CssClass = "form-control ReadOnly";
                        hdfmramount.Value = "";
                        hdfddlBlockBill.Value = "";
                        hdfddlWApp.Value = "";
                        hdfddlWApproved.Value = "";
                        hdfRecallcurreentStatus.Value = "";
                        ResetVariables();
                        LoadPaymentModes();
                        ClearValues();
                        LoadCancelReasons();
                        LoadRegRequestInfo(stringRequestID); 
                        LoadAllDATA(stringRequestID);
                        LoadData();

                        LoadLoadCancelHistory();
                        txtPaidAmt.Text = "0.00";
                        txtRefundAmt.Text = "0.00"; 
                        decimalCancelationCharge = GetReportCancellationCharge(txtRptType.ToolTip.ToString());
                        objDataTable = GetMRPaymentDetails(stringRequestID);
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            pnlPaymentDetails.Visible = true;
                            objDataRow = objDataTable.Rows[0];

                            decimalTotAmount = 0;
                            decimalPaidAmount = 0;

                            decimal.TryParse(objDataRow["total_amt"].ToString(), out decimalTotAmount);
                            txtMRAmt.Text = decimalTotAmount.ToString("0.00");

                            decimal.TryParse(objDataRow["paid_amt"].ToString(), out decimalPaidAmount);
                            txtPaidAmt.Text = decimalPaidAmount.ToString("0.00");
                            txtRefundAmt.Text = decimalPaidAmount.ToString("0.00");

                            txtCancelAmt.Text = decimalCancelationCharge.ToString("0.00");

                            txtRefund.Text = GetRefundTo(objDataRow["mr_pymt_id"].ToString());


                            if (GetPaymentStatus(stringRequestID)) { pnlPaymentDetails.Visible = true; }
                            else { pnlPaymentDetails.Visible = false; }
                        }
                        if (stringLockFields == "TRUE")
                        {
                            LockFields();
                        } 
                        LoadCancellationRecord(stringRequestID);
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
                decimalCancelationCharge = 0;
                decimalTotAmount = 0;
                decimalPaidAmount = 0;
                objDataTable = null;
                stringRequestID = null;
                stringLockFields = null;
                objDataRow = null;
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
            btncreditnotes.Visible = false;
            lnkbtApprove.Visible = false;
            lnkbtnReject.Visible = false;

            imgbtnSave.Enabled = false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FC0006R1V1";
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
                            imgbtnSave.Enabled = true;
                        }
                        if (objDataRow["delete"].ToString().ToUpper() == "ENABLED")
                        { 
                        }
                        if (objDataRow["Export"].ToString().ToUpper() == "ENABLED")
                        {
                            btncreditnotes.Visible = true;
                        }
                    }
                    else
                    {
                        Response.Redirect("PageAccessDenied.aspx", true);
                    }

                }

                stringComponent = new string[1];
                stringComponent[0] = "FC0006R1V2";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        lnkbtApprove.Visible = true;
                        lnkbtnReject.Visible = true;
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
                stringComponent[0] = "FC0009R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        lnkbtnrecalhistory.Enabled = true;
                    }
                    else
                    {
                        lnkbtnrecalhistory.Enabled = false;
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
        private void LoadPaymentModes()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0018R1V1";
            string stringOrderBy = "mrpaytys.ORDER_ID asc,mrpaytys.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            DataTable objdatatableLoadPaymentModes = null;
            try
            {
                ddlPymtMode.Items.Clear();
                stringServiceType = "List1R1V1";
                stringexp012 = "And mrpaytys.be_id= '" + stringbeid + "' And mrpaytys.delmark= 'N' and mrpaytys.allow_refund='Y'";
                 
                if (Session["SSNLOADPAYMENTMODES_01"] != null)
                {
                    objdatatableLoadPaymentModes = (DataTable)Session["SSNLOADPAYMENTMODES_01"];
                }
                if ((objdatatableLoadPaymentModes == null) || (objdatatableLoadPaymentModes != null && objdatatableLoadPaymentModes.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadPaymentModes = objDatasetResult.Tables["t1"];
                            Session["SSNLOADPAYMENTMODES_01"] = objdatatableLoadPaymentModes;
                        }
                        else
                        {
                            Session["SSNLOADPAYMENTMODES_01"] = null;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadPaymentModes != null && objdatatableLoadPaymentModes.Rows.Count > 0)
                {
                    ddlPymtMode.DataTextField = "short_name";
                    ddlPymtMode.DataValueField = "paytyp_id";
                    ddlPymtMode.DataSource = objdatatableLoadPaymentModes;
                    ddlPymtMode.DataBind();
                    ddlPymtMode.Items.Insert(0, new ListItem("", ""));

                    if (ddlPymtMode.Items.Count > 0)
                    {
                        if (ddlPymtMode.Items.FindByValue("CHEQUE") != null)
                        { ddlPymtMode.Items.FindByValue("CHEQUE").Selected = true; }
                    }

                    ddlPymtMode.SelectedIndex = 1;
                }
                else
                {
                    ddlPymtMode.DataSource = null;
                    ddlPymtMode.DataBind();
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
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
                //objdatatableLoadPaymentModes = null;
            }

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
        protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            string stringMRamt = "";  
            DataTable objdatatable = null;
            try
            {
                if (txtMRStatus.Text == "CANCELLED")
                {
                    CommonFunctions.ShowMessageboot(this, "This Record already Cancelled");
                }
                else
                {
                    stringMRamt = hdfmramount.Value.ToString();
                    if (stringMRamt.Length > 0)
                    {
                        decimal decimalmrAmount = 0;
                        if (stringMRamt != null && stringMRamt.Trim().Length > 0)
                        { decimal.TryParse(stringMRamt, out decimalmrAmount); }
                         
                        if (Session["LoadPaymentReceiptsGridFC0001"] == null)
                        {
                            lnkbtApprove_Click(null, null);
                        }
                        else
                        {
                            if (Session["LoadPaymentReceiptsGridFC0001"] != null)
                            {
                                objdatatable = (DataTable)Session["LoadPaymentReceiptsGridFC0001"];
                                if (objdatatable != null && objdatatable.Rows.Count > 0)
                                {
                                    SaveData("PENDING", "");
                                }
                                else
                                {
                                    lnkbtApprove_Click(null, null);
                                }
                            }
                        }
                    }
                    else
                    {
                        lnkbtApprove_Click(null, null);
                    }
                    
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringMRamt = null; 
                objdatatable = null;
            }
        }

        private void SaveData(string StringType, string StringTypeReject)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC1013R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            DataRow objdatarow = null;
            string stringexp = "";
            try
            {
                if (ValidateControls() && ValidateBusinessLogic())
                {
                    stringServiceType = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (objDatasetResult != null && objDatasetResult.Tables["t4"].Rows.Count == 0)
                    {
                        objdatarow = objDatasetResult.Tables["t4"].NewRow();
                        objdatarow["be_id"] = CommonFunctions.GETBussinessEntity().ToString();

                        objdatarow["Request_ID"] = txtRequestNo.Text.Trim();
                        if(ddlCR.SelectedItem!=null&& ddlCR.SelectedValue.ToString().Length > 0)
                        {
                            objdatarow["CR_ID"] = ddlCR.SelectedItem.Value;
                        }
                        objdatarow["reference_1"] = txtReject.Text.Trim();
                        objdatarow["Remarks"] = txtRemarks.Text.Trim();
                        objdatarow["REFUND_TO"] = txtRefund.Text.Trim();
                        objdatarow["CANCELLATION_CHARGES"] = Convert.ToDecimal(txtCancelAmt.Text.Trim());
                        objdatarow["REFUND_AMT"] = Convert.ToDecimal(txtRefundAmt.Text.Trim());
                        objdatarow["MR_CANCEL_BY"] = txtCancelBy.Text.Trim();
                        objdatarow["LONG_NAME"] = "";
                        objdatarow["PRINT_ADVISE"] = "N";
                        objdatarow["INC_CANCEL_CHRG"] = ChkCanCharge.Checked ? "Y" : "N";
                        if (ddlPymtMode.SelectedItem != null && ddlPymtMode.SelectedValue.ToString().Length > 0)
                        {
                            objdatarow["PAYTYP_ID"] = ddlPymtMode.SelectedItem.Value;
                        }
                        if (Session["G11EOSUser_Name"] != null)
                        { objdatarow["PymtCounter_ID"] = Session["G11EOSUser_Name"].ToString(); }
                        else { objdatarow["PymtCounter_ID"] = ""; }

                        objdatarow["delmark"] = "N";
                        objdatarow["SUP_STATUS"] = StringType;
                        CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                        objDatasetResult.Tables["t4"].Rows.Add(objdatarow);
                        objDatasetResult.Tables["t4"].Rows[0].RowState.ToString();

                    }
                    objDatasetResult = objDatasetResult.GetChanges();
                    stringServiceType = "OperationServiceDML";
                    objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);
                    if (intErrorCount == 0)
                    {
                        LoadLoadCancelHistory();
                        if (StringType == "APPROVED")
                        {
                            LoadRegRequestInfo(txtRequestNo.Text.Trim());
                            CommonFunctions.ShowMessageboot(this, "MR Cancelled Successfully"); 
                            ddlCR.Focus();
                            LockFields();
                            ControlsEnableFlag(false);
                        }
                        else if (StringType == "PENDING" && StringTypeReject.Length == 0)
                        {
                            CommonFunctions.ShowMessageboot(this, "Record Saved Successfully"); 
                            ddlCR.Focus(); 
                            LockFields();
                            ControlsEnableFlag(false);
                        }
                        else if (StringTypeReject == "REJECTED")
                        {
                            LockFields(); 
                            ControlsEnableFlag(false);
                            txtReject.Enabled = false;
                            txtReject.CssClass = "form-control ReadOnly";
                            lnkbtnReject.Visible = false;
                            lnkbtApprove.Visible = false;
                        } 
                        LoadRegRequestInfo(txtRequestNo.Text.Trim());
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

        protected void imgBtnSecurity_Click(object sender, ImageClickEventArgs e)//fix
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
            string stringServiceType = "";
            string stringexp0121 = "";
            DataRow objDataRow = null;
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (txtRequestNo.Text.Trim().Length > 0)
                {
                    stringServiceType = "List21R1V1";
                    stringexp0121 = "And mrc.be_id= '" + stringBoID.ToString() + "' And mrc.Request_ID= '" + txtRequestNo.Text.Trim() + "'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp0121, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t2"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            objDataRow = objDataTable.Rows[0];
                            ShowAuditTrail(objDataRow);
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
                objDataTable = null;
                stringBoID = null;
                stringServiceType = null;
                stringexp0121 = null;
                objDataRow = null;
            }
        }
        private void LoadData()//fix
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
            string stringTemp = "";
            string stringRequestID = "";
            string stringBoID = "";
            string stringServiceType = "";
            DataRow objDataRow = null;
            string stringexp0121 = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (txtRequestNo.Text.Trim().Length > 0)
                {
                    if (Session["REQUESTID_CANCELLATION"] != null)
                    {
                        stringRequestID = Session["REQUESTID_CANCELLATION"].ToString();
                    }
                    stringServiceType = "List21R1V1";
                    stringexp0121 = "And mrc.be_id= '" + stringBoID.ToString() + "' And mrc.Request_ID= '" + stringRequestID.Trim() + "'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp0121, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t21"] != null && objDatasetResult.Tables["t21"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t21"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            objDataRow = objDataTable.Rows[0];
                            stringTemp = objDataRow["cancel_rsn"].ToString();
                            if (stringTemp != null)
                                ddlCR.SelectedItem.Text = stringTemp;


                            txtRemarks.Text = objDataRow["remarks"].ToString();
                            txtReject.Text = objDataRow["reference_1"].ToString();
                            txtRefund.Text = objDataRow["refund_to"].ToString();
                            txtCancelBy.Text = objDataRow["mr_cancel_by"].ToString();

                            stringTemp = objDataRow["paytyp_id"].ToString();
                            ddlPymtMode.ClearSelection();
                            if (ddlPymtMode.Items.FindByValue(stringTemp) != null)
                            { ddlPymtMode.Items.FindByValue(stringTemp).Selected = true; }


                            if (objDataRow["refund_amt"] != null && objDataRow["refund_amt"].ToString().Trim().Length > 0)
                            {
                                txtRefundAmt.Text = Convert.ToDecimal(objDataRow["refund_amt"]).ToString("0.00");
                            }

                            if (objDataRow["cancellation_charges"] != null && objDataRow["cancellation_charges"].ToString().Trim().Length > 0)
                            { txtCancelAmt.Text = Convert.ToDecimal(objDataRow["cancellation_charges"]).ToString("0.00"); }

                            if (objDataRow["modified_on"] != null && objDataRow["modified_on"].ToString().Trim().Length > 0)
                            { txtCDate.Text = Convert.ToDateTime(objDataRow["modified_on"]).ToString("dd-MM-yyyy"); }
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
                objDataTable = null;
                stringTemp = null;
                stringRequestID = null;
                stringBoID = null;
                stringServiceType = null;
                objDataRow = null;
                stringexp0121 = null;
            }

        }
        private void LoadCancelReasons()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0001R1V1";
            string stringOrderBy = "mrcanres.ORDER_ID asc,mrcanres.order_id asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableLoadCancelReasons = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            try
            {
                ddlCR.Items.Clear();
                stringServiceType = "List1R1V1";
                stringexp012 = "And mrcanres.be_id= '" + stringbeid + "'  And mrcanres.delmark= 'N'";
                if (Session["SSNLOADCANCELREASONS"] != null)
                {
                    objdatatableLoadCancelReasons = (DataTable)Session["SSNLOADCANCELREASONS"];
                }
                if ((objdatatableLoadCancelReasons == null) || (objdatatableLoadCancelReasons != null && objdatatableLoadCancelReasons.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadCancelReasons = objDatasetResult.Tables["t1"];
                            Session["SSNLOADCANCELREASONS"] = objdatatableLoadCancelReasons;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadCancelReasons != null && objdatatableLoadCancelReasons.Rows.Count > 0)
                {
                    ddlCR.DataTextField = "short_name";
                    ddlCR.DataValueField = "cr_id";
                    ddlCR.DataSource = objdatatableLoadCancelReasons;
                    ddlCR.DataBind();
                    ddlCR.Items.Insert(0, new ListItem("", "")); 
                    ddlCR.SelectedIndex = 1;
                }
                else
                {
                    ddlCR.DataSource = null;
                    ddlCR.DataBind();
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
                objdatatableLoadCancelReasons = null;
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
            }

        }

        private void LoadLoadCancelHistory()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0006R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringServiceType = "";
            string stringexp012 = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            pnlresultgrid.Visible = false;
            gvList.DataSource = null;
            gvList.DataBind();
            lblTotalRecords.InnerText = "0";
            try
            { 
                stringServiceType = "List1R1V1";
                stringexp012 = "And delcan.be_id= '" + stringbeid + "'  And delcan.Request_ID= '"+ txtRequestNo.Text.Trim()+"'";


                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t1"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        pnlresultgrid.Visible = true;
                        gvList.DataSource = objDataTable;
                        gvList.DataBind();
                        lblTotalRecords.InnerText = objDataTable.Rows.Count.ToString();
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
                objDataTable = null;
                stringServiceType = null;
                stringexp012 = null;
                stringbeid = null;
            }
        }
        private void LoadRegRequestInfo(string stringRequestNo)//fix
        {
            DataTable objDataTable = null;
            DataRow objDataRow = null;
            string stringReportTypID = "";
            string stringDelMode = "";
            string stringStatus = "";
            string stringMRamt = "";
            string stringUserID = "";
            string stringUserDesc = "";
            string stringcancellstatus = ""; ViewState["DEPTOU"] = null;
            decimal decimalTotAmount = 0; 
            btncreditnotes.Visible = false;
            try
            {
                if (stringRequestNo != null && stringRequestNo.Trim().Length > 0)
                {
                    objDataTable = GetRequestDetails(stringRequestNo);
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        objDataRow = objDataTable.Rows[0];
                        ViewState["DEPTOU"] = objDataRow["DEPT_ID"].ToString();
                        LoadProcesstabData(objDataRow);
                        ReportRelatedValues(objDataRow);
                        txtHRN.Text = objDataRow["hrn_id"].ToString();
                        txtReqContact.Text = objDataRow["requested_by"].ToString();

                        if (objDataRow["receive_date"] != null && objDataRow["receive_date"].ToString().Trim().Length > 0)
                        { txtRecDate.Text = Convert.ToDateTime(objDataRow["receive_date"]).ToString("dd-MM-yyyy"); }

                        if (objDataRow["due_date"] != null && objDataRow["due_date"].ToString().Trim().Length > 0)
                        { txtDueDate.Text = Convert.ToDateTime(objDataRow["due_date"]).ToString("dd-MM-yyyy"); }

                        stringReportTypID = objDataRow["rpttyp_id"].ToString();
                        txtRptType.ToolTip = stringReportTypID;
                        txtRptType.Text = objDataRow["report_type_short_name"].ToString();
                        stringDelMode = objDataRow["delmod_id"].ToString();
                        txtDelToID.Text = stringDelMode;
                        txtReqEmail.Text = objDataRow["Email"].ToString();
                        stringStatus = objDataRow["mr_status"].ToString();
                        txtMRStatus.Text = stringStatus;
                        if(stringStatus.ToUpper() == "CANCELLED")
                        {
                            btncreditnotes.Visible = true;
                        }
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
                        txtPatName.Text = objDataRow["patient_short_name"].ToString(); 
                        txtpatnamemrn.Text = objDataRow["patient_short_name"].ToString() + " [" + objDataRow["hrn_id"].ToString() + "] ";
                        stringUserID = objDataRow["created_by"].ToString();
                        stringUserDesc = objDataRow["created_by"].ToString();
                        if (stringUserDesc != null && stringUserDesc.Trim().Length > 0) { txtCreateBy.Text = stringUserDesc; }
                        txtCreateBy.Text = objDataRow["created_by"].ToString();
                        txtCreateBy.ToolTip = stringUserID;

                        decimalTotAmount = 0;
                        decimal.TryParse(objDataRow["mr_amount"].ToString(), out decimalTotAmount);
                        txtMRAmt.Text = decimalTotAmount.ToString("0.00");
                        txtCancelAmt.Text = "0.00";

                        stringcancellstatus = objDataRow["sup_status"].ToString();
                        if (stringStatus.Trim().ToUpper() == "FORWARDED" || stringStatus.Trim().ToUpper() == "COLLECTED" || stringStatus.Trim().ToUpper() == "CANCELLED" || stringcancellstatus.ToUpper() == "PENDING")
                        { ControlsEnabledByProStatus("CANCELLED"); }
                        else { }

                        if (txtMRStatus.Text == "CANCELLED")
                        {
                            lnkbtnReject.Enabled = false;
                            lnkbtApprove.Enabled = false;

                        }
                        else
                        {
                            lnkbtnReject.Enabled = true;
                            lnkbtApprove.Enabled = true;
                        }
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
                stringReportTypID = null;
                stringDelMode = null;
                stringStatus = null;
                stringMRamt = null;
                stringUserID = null;
                stringUserDesc = null;
                stringcancellstatus = null;
                decimalTotAmount = 0; 
            }
           
        }
        private string GetRefundTo(string stringMRPaymentID)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3008R1V1";
            string stringOrderBy = "mrpr.created_on asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringServiceType = "";
            string stringexp012 = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                if (stringMRPaymentID != null && stringMRPaymentID.Trim().Length > 0)
                {
                    stringServiceType = "List2R1V1";
                    stringexp012 = "And mrpr.be_id= '" + stringbeid + "'  And mrpr.MR_Pymt_ID= '" + stringMRPaymentID + "' ";
                   
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t2"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            return objDataTable.Rows[0]["payee_name"].ToString();
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
                objDataTable = null;
                stringServiceType = null;
                stringexp012 = null;
                stringbeid = null;
            }

            return "";
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
            string stringbeid = CommonFunctions.GETBussinessEntity();
            DataTable objDataTable = null;
            string stringServiceType1 = "";
            string stringexp012 = "";
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringbeid = null;
                objDataTable = null;
                stringServiceType1 = null;
                stringexp012 = null;
            }
            return null;
        }
        private DataTable GetMRPaymentDetails(string stringReqID)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V2";
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
                    stringServiceType1 = "List2R1V1";
                    stringexp012 = "And mrpt.be_id= '" + stringbeid + "'  And mrpt.request_id= '" + stringReqID.ToString() + "'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t2"];
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
                intToRecord = 0;
                objDataTable = null;
                stringServiceType1 = null;
                stringexp012 = null;
                stringbeid = null;
            }

            return null;
        }

        private decimal GetReportCancellationCharge(string stringReportType)//fix -ok 
        {
            decimal decimalCancelChrges = 0;
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0024R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringcondition = "";
            string stringServiceType = "";
            string stringTemp = "";
            DataRow objDataRow = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                if (stringReportType != null && stringReportType.Trim().Length > 0)
                {
                    stringcondition = "And mrrets.be_id= '" + stringbeid + "'  AND mrrets.delmark='N' AND mrrets.rpttyp_id= '" + stringReportType.ToString() + "'";
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
                            objDataRow = objDataTable.Rows[0];

                            stringTemp = "";

                            stringTemp = objDataRow["cancellation_charges"].ToString();
                            if (stringTemp != null && stringTemp.Trim().Length > 0)
                            { decimal.TryParse(stringTemp, out decimalCancelChrges); }
                            return decimalCancelChrges;
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
                decimalCancelChrges = 0;
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                objDataTable = null;
                stringcondition = null;
                stringServiceType = null;
                stringTemp = null;
                objDataRow = null;
                stringbeid = null;
            }
            return 0;
        }  
        private void LockFields()//fix
        {
            try
            {
                //imgbtnNew.Enabled = false;
                //imgbtnprint.Enabled = false;
                imgbtnSave.Enabled = false;
                //imgbtnDelete.Enabled = false;
                //imgbtnSearch.Enabled = false;
                //imgbtnAudit.Enabled = false;
                imgbtnexport.Enabled = false;
                //ImageButton1.Enabled = false;

                ddlCR.Enabled = false;
                ddlCR.CssClass = "form-control ReadOnly";
                ddlPymtMode.Enabled = false;
                ddlPymtMode.CssClass = "form-control ReadOnly";
                txtRemarks.Enabled = false;
                txtRemarks.CssClass = "form-control ReadOnly";
                txtRefund.Enabled = false;
                txtRefund.CssClass = "form-control ReadOnly";
                txtCancelBy.Enabled = false;
                txtCancelBy.CssClass = "form-control ReadOnly";
                txtCDate.Enabled = false;
                txtCDate.CssClass = "form-control ReadOnly";
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
          
        }

        private void ControlsEnabledByProStatus(string stringStatus)//fix
        {
            switch (stringStatus)
            {
                case "CANCELLED":
                    {
                        //imgbtnNew.Enabled = false;
                        //imgbtnprint.Enabled = false;
                        imgbtnSave.Enabled = false;
                        //imgbtnDelete.Enabled = false;
                        //imgbtnSearch.Enabled = false;
                        //imgbtnAudit.Enabled = false;
                        imgbtnexport.Enabled = false;
                        //ImageButton1.Enabled = false;

                        ddlCR.Enabled = false;
                        ddlCR.CssClass = "form-control ReadOnly";
                        ddlPymtMode.Enabled = false;
                        ddlPymtMode.CssClass = "form-control ReadOnly";
                        txtRemarks.Enabled = false;
                        txtRemarks.CssClass = "form-control ReadOnly";
                        txtRefund.Enabled = false;
                        txtRefund.CssClass = "form-control ReadOnly";
                        txtCancelBy.Enabled = false;
                        txtCancelBy.CssClass = "form-control ReadOnly";
                        txtCDate.Enabled = false;
                        txtCDate.CssClass = "form-control ReadOnly";

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
                        //imgbtnNew.Enabled = true;
                        //imgbtnprint.Enabled = true;
                        imgbtnSave.Enabled = true;
                        //imgbtnDelete.Enabled = true;
                        //imgbtnSearch.Enabled = true;
                        imgBtnPrint.Enabled = true;
                        imgbtnexport.Enabled = true;
                        //ImageButton1.Enabled = true;
                        break;
                    }
            }
        }

        private bool GetPaymentStatus(string stringRequestID)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTablePenItems = null;
            string stringServiceType = "";
            string stringexp012 = "";
            string stringbeid = CommonFunctions.GETBussinessEntity(); 
            try
            { 
                stringServiceType = "List12R1V1";
                stringexp012 = "And mrpend.be_id= '" + stringbeid + "' And mrpend.Request_ID= '" + stringRequestID.ToString() + "'  ";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t12"] != null && objDatasetResult.Tables["t12"].Rows.Count > 0)
                    {
                        objDataTablePenItems = objDatasetResult.Tables["t12"];
                    }
                    if (objDataTablePenItems != null && objDataTablePenItems.Rows.Count > 0)
                    {
                        return true;
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
                objDataTablePenItems = null;
                stringServiceType = null;
                stringexp012 = null;
                stringbeid = null;
            }

            return false;
        }

        private void ShowAuditTrail(DataRow objDataRow)//fix
        {
            string strTmp = "";
            string stringScript = "";
            try
            {
                if (objDataRow != null)
                {
                    strTmp = "";
                    strTmp = strTmp + Convert.ToDateTime(objDataRow["created_on"]).ToString("dd-MM-yyyy HH:mm");
                    strTmp = strTmp + "~" + objDataRow["created_at"].ToString();
                    strTmp = strTmp + "~" + objDataRow["created_user"].ToString();
                    strTmp = strTmp + "~" + objDataRow["created_by"].ToString();
                    strTmp = strTmp + "~" + Convert.ToDateTime(objDataRow["modified_on"]).ToString("dd-MM-yyyy HH:mm");
                    strTmp = strTmp + "~" + objDataRow["modified_at"].ToString();
                    strTmp = strTmp + "~" + objDataRow["modified_user"].ToString();
                    strTmp = strTmp + "~" + objDataRow["modified_by"].ToString();

                    stringScript = "";
                    stringScript = "<script language='JavaScript'> window.open('AuditTrail.aspx?ValueList=" + strTmp;
                    stringScript += "','anycontent','width=300,height=310,left=16,top=16,status,location=no,directories=no,";
                    stringScript += "status=yes,menubar=yes,scrollbars=yes,copyhistory=no');";
                    stringScript += "</script>);";
                    ClientScript.RegisterClientScriptBlock(typeof(String), "", stringScript);
                }
            }
            catch (Exception objException) { CommonFunctions.HandleException(objException); }
            finally
            {
                strTmp = null;
                stringScript = null;
            }
        }

        private void ResetVariables()//fix
        {
            try
            {
                Session["stringDMLIndicator"] = "I";
                Session["stringSortDirection"] = "ASC";
                Session["stringSortExpression"] = "";

                Session["stringSortDirection1"] = "ASC";
                Session["stringSortExpression1"] = "";

                Session["stringFormID"] = "FC1013R1V1";
                Session["stringFormName"] = "MR CANCELLATION";
            }
            catch (Exception objException) { CommonFunctions.HandleException(objException); }
            
        } 
        private void ClearValues()//fix
        {
            try
            {
                ddlCR.ClearSelection();
                txtRemarks.Text = "";
                txtRefund.Text = "";

                ddlPymtMode.ClearSelection();
                if (ddlPymtMode.Items.Count > 0)
                {
                    if (ddlPymtMode.Items.FindByValue("CHEQUE") != null)
                    { ddlPymtMode.Items.FindByValue("CHEQUE").Selected = true; }
                }

                txtCDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtCancelBy.Text = Session["G11EOSUser_Name"].ToString();
                ddlCR.Focus();

                ddlCR.Enabled = true;
                txtRemarks.Enabled = true;
                txtCDate.Enabled = true;
            }
            catch (Exception objException) { CommonFunctions.HandleException(objException); }
            
        }

        private bool ValidateControls()//fix
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";
                if (txtCDate.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Cancelled Date" + "\\r\\n";
                    boolStatus = false;
                }

                if (txtCancelBy.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Cancelled By" + "\\r\\n";
                    boolStatus = false;
                }

                if (ddlCR.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Reason for cancellation" + "\\r\\n";
                    boolStatus = false;
                }

                if (txtRefund.Enabled && txtRefund.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Refund" + "\\r\\n";
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
            catch (Exception objException) { CommonFunctions.HandleException(objException); }
           
            return boolStatus;
        }

        private bool ValidateBusinessLogic()//fix
        {
            bool boolStatus = true;
            decimal decimalAmount = 0;

            try
            {
                if (txtCDate.Text.Trim().Length > 0 && !CommonFunctions.ValidDateTime(txtCDate.Text.Trim()))
                {
                    CommonFunctions.ShowMessageboot(this, "Cancelled date Should be a valid date");
                    txtCDate.Focus();
                    return false;
                }

                if (!decimal.TryParse(txtMRAmt.Text.Trim(), out decimalAmount))
                {
                    CommonFunctions.ShowMessageboot(this, "MR Amount Should be a valid decimal");
                    txtMRAmt.Focus();
                    return false;
                }

                if (!decimal.TryParse(txtPaidAmt.Text.Trim(), out decimalAmount))
                {
                    CommonFunctions.ShowMessageboot(this, "Paid Amount Should be a valid decimal");
                    txtPaidAmt.Focus();
                    return false;
                }

                if (!decimal.TryParse(txtCancelAmt.Text.Trim(), out decimalAmount))
                {
                    CommonFunctions.ShowMessageboot(this, "Cancel Amount Should be a valid decimal");
                    txtCancelAmt.Focus();
                    return false;
                }

                if (!decimal.TryParse(txtRefundAmt.Text.Trim(), out decimalAmount))
                {
                    CommonFunctions.ShowMessageboot(this, "Refund Amount Should be a valid decimal");
                    txtRefundAmt.Focus();
                    return false;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return false;
            } 
            return boolStatus;
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
                stringExpression = " and dach.be_id='" + stringBoID + "' and dach.trans_id='" + txtRequestNo.Text.Trim() + "'  ";

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
                            ModalPopupProcessUpdate.Show();
                            UpdatePanelProcessUpdate.Visible = true;
                        }
                        else if((stringreportverify != "N" && boolnotaccatched && stringprocessname == "Pending forwarding") || (stringprocessname != "Pending forwarding"))
                        {
                            lblupdateprocesscontent.Text = "Are you sure want to update the process?";

                            ViewState["NEXTPROCESSNAME"] = stringprocessname;
                            txtProcessCompletedRemarks.Text = "";
                            ModalPopupProcessUpdate.Show();
                            UpdatePanelProcessUpdate.Visible = true;
                        }
                        else if (!boolnotaccatched && stringprocessname == "Pending forwarding")
                        {
                            lblupdateprocesscontent.Text = "There no Medical Report being attached. Do you want to proceed in completing the process ?";
                            ViewState["NEXTPROCESSNAME"] = stringprocessname;
                            txtProcessCompletedRemarks.Text = "";
                            ModalPopupProcessUpdate.Show();
                            UpdatePanelProcessUpdate.Visible = true;
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
               // boolsmr = false;
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
            string stringRequestID = "";
            string stringexp = "";
            string stringprocessname = "";
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
                stringRequestID = null;
                stringexp = null;
                stringbeid = null;
                stringprocessname = null;
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
            string stringprocessname = "";
            int intToRecord = int.MaxValue;
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
                stringprocessname = null;
                intToRecord = 0;
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
                ModalPopupProcessUpdate.Hide();
                UpdatePanelProcessUpdate.Visible = false;
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
                //boolContinue = false;
                stringTransSattus = null;
                objdatatablePendingItems = null;
                //boolpendingItems = true;
                //boolpendingreport = true;
                //boolEnquirypendingItems = true;
                //boolAssignDoctor = true;
                //boolpaymentpendingItems = true;
                objdatarow = null;
                stringMRamt = null;
                stringbalanceamt = null;
                intbalanceamt = 0; 
                objdatatable = null;
            }
        }
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
                    stringExpression = " and mrasdoc.be_id='" + stringBoID + "' and mrasdoc.request_id='" + stringRequestID + "'and mrasdoc.verify_ref='VERIFIER' and (mrasdoc.status ='IN-PROGRESS' or mrasdoc.status='PENDING')";

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
            string stringOverallMsg = "";
            bool boolcheckvalidationvalidemail = true;
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
                        {
                            //objDatasetResult = objInterfaceServiceClient.SendEmailUserR1V1(txtRequestNo.Text.Trim(), txtsmremailRequestor.Text.Trim(), txtsmremailCC.Text.Trim(), txtsmremailBCC.Text.Trim(), txtsmremailSubject.Text.Trim(), txtsmremailCOntent.Text.Trim(),  objDatasetAppsVariables, out interrorcount, out stringOutputResult);
                            objDatasetResult = objInterfaceServiceClient.SendEmailUserR1V1(txtRequestNo.Text.Trim(), txtsmremailRequestor.Text.Trim(), txtsmremailCC.Text.Trim(), txtsmremailBCC.Text.Trim(), txtsmremailSubject.Text.Trim(), txtsmremailCOntent.Text.Trim(),"N", objDatasetAppsVariables,out objbytearray, out interrorcount, out stringOutputResult);
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
               // boolcheckvalidationvalidemail = true;
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

                        if (objDatasetResult != null && objDatasetResult.Tables["t9"].Rows.Count > 0)
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
                intToRecord = int.MaxValue;
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
            string stringServiceType = "";
            string stringContent = "";
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
                stringServiceType = null;
                stringContent = null;
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
                    //objdatarowCompleteMedicalReport = objDataTableAddReports.Select("TRANS_ID = '" + txtReqNo.Text.Trim().ToString() + "' and CATEGORY = 'COMPLETED MEDICAL REPORTS'   and GW_STATUS = 'SEND'");
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
                stringreqID = null;
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
                    //objdatarowCompleteMedicalReport = objDataTableAddReports.Select("TRANS_ID = '" + txtReqNo.Text.Trim().ToString() + "' and CATEGORY = 'COMPLETED MEDICAL REPORTS'   and GW_STATUS = 'SEND'");
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
                stringOutputResult = null;
            }

            //DataTable objDataTableAddReports = null;
            //DataTable objDataTableCompleteMedicalReport = null;
            //DataRow objdatarowCompleteMedicalReportlatest = null;
            //string stringBoID = "";
            //if (Session["BusinessID"] != null)
            //{
            //    stringBoID = Session["BusinessID"].ToString();
            //}
            //DataRow[] objdatarowCompleteMedicalReport = null;
            //string stringBE_ID, stringFORM_ID, stringTRANS_ID, stringDOC_NAME, stringDOC_TYPE, stringATTACH_ID, StringFileName = string.Empty;
            //string[] stringOutputResult = null;  
            //byte[] objbyteArray = null;

            //DataSet objDatasetAppsVariables = (DataSet)HttpContext.Current.Session["objDatasetlocaldeclaration"];
            //objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FC0010R1V1";
            //string[] objerrormeg = null;
            //string stringerrormessage = null; 
            //string stringSMR_INST_CODE = string.Empty;
            //string stringHRN_ID = string.Empty;
            //string stringSMR_ID = string.Empty;
            //int interrorcount = 0;
            //try
            //{
            //    if (Session["ADD_ATTACHMENTS"] != null)
            //    { objDataTableAddReports = (DataTable)Session["ADD_ATTACHMENTS"]; }


            //    if (objDataTableAddReports != null && objDataTableAddReports.Rows.Count > 0)
            //    {
            //        objdatarowCompleteMedicalReport = objDataTableAddReports.Select("TRANS_ID = '" + txtReqNo.Text.Trim().ToString() + "' and CATEGORY = 'COMPLETED MEDICAL REPORTS' and GW_STATUS = 'SEND'");
            //        if (objdatarowCompleteMedicalReport != null && objdatarowCompleteMedicalReport.Length > 0)
            //        {
            //            objDataTableCompleteMedicalReport = objdatarowCompleteMedicalReport.CopyToDataTable();
            //            objDataTableCompleteMedicalReport.DefaultView.Sort = "MODIFIED_ON desc";
            //            objDataTableCompleteMedicalReport = objDataTableCompleteMedicalReport.DefaultView.ToTable();

            //            if (objDataTableCompleteMedicalReport != null && objDataTableCompleteMedicalReport.Rows.Count > 0)
            //            {
            //                objdatarowCompleteMedicalReportlatest = objDataTableCompleteMedicalReport.Rows[0];
            //            }
            //            if (objdatarowCompleteMedicalReportlatest != null)
            //            {
            //                stringBE_ID = objdatarowCompleteMedicalReportlatest["BE_ID"].ToString();
            //                stringFORM_ID = objdatarowCompleteMedicalReportlatest["FORM_ID"].ToString();
            //                stringTRANS_ID = objdatarowCompleteMedicalReportlatest["TRANS_ID"].ToString();
            //                stringDOC_NAME = objdatarowCompleteMedicalReportlatest["DOC_NAME"].ToString();
            //                stringDOC_TYPE = objdatarowCompleteMedicalReportlatest["DOC_TYPE"].ToString();
            //                stringATTACH_ID = objdatarowCompleteMedicalReportlatest["ATTACH_ID"].ToString();
            //                stringSMR_INST_CODE = objdatarowCompleteMedicalReportlatest["SMR_INS_CODE"].ToString();
            //                stringHRN_ID = objdatarowCompleteMedicalReportlatest["HRN_ID"].ToString();
            //                stringSMR_ID = objdatarowCompleteMedicalReportlatest["SMR_ID"].ToString();

            //                using (GSAPITriggerService.APITriggerServiceClient objFileTransfer1 = new GSAPITriggerService.APITriggerServiceClient(CommonFunctions.CreateServiceBasicHttpBinding(), CommonFunctions.objEndpointAddressAPITriggerService))
            //                {
            //                    objFileTransfer1.SAPPostingR1V3(stringSMR_INST_CODE, stringHRN_ID, stringSMR_ID, objDatasetAppsVariables, out objbyteArray, out objerrormeg, out interrorcount, out stringOutputResult);
            //                    if (objFileTransfer1 != null)
            //                        objFileTransfer1.Close();
            //                }
            //                if (interrorcount == 0)
            //                {
            //                    if (objbyteArray != null)
            //                    {
            //                        StringFileName = stringDOC_NAME;
            //                        CommonFunctions.OpenExportedFileR1V1(this, objbyteArray, StringFileName.ToString(), "ATTACHMENTS");
            //                    }
            //                    else
            //                    {
            //                        if (objerrormeg != null && objerrormeg.Length > 0 && objerrormeg[1] != null && objerrormeg[1].ToString().Length > 0)
            //                        {
            //                            stringerrormessage = objerrormeg[0].ToString() + objerrormeg[1].ToString();
            //                            CommonFunctions.ShowMessageboot(this, stringerrormessage);
            //                        }
            //                        else
            //                        {
            //                            CommonFunctions.ShowMessageboot(this, "Report Not Found.");
            //                        }

            //                    }
            //                }
            //                else
            //                {
            //                    Errorpopup(stringOutputResult);
            //                }
            //            }
            //        }
            //    }

            //}
            //catch (Exception objException)
            //{
            //    CommonFunctions.HandleException(objException);
            //}

        }
        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ModalPopupExtenderpreview.Hide();
                Paneldocpreview.Visible = false;
                mpePdtPlt23.Show();
                Panel10.Visible = true;
            }
            catch (Exception objException)
            {
               CommonFunctions.HandleException(objException);
            }
            
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
            DataRow[] objdatarow = null; ;
            DataTable objdatatable = null;
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
                    ModalPopupProcessUpdate.Hide();
                    UpdatePanelProcessUpdate.Visible = false;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
               // boolContinue = false;
                stringTransSattus = null;
                objdatatablePendingItems = null;
                //boolpendingItems = true;
                //boolpaymentpendingItems = true;
                //boolEnquirypendingItems = true;
                //boolAssignDoctor = true;
                //boolpendingreport = true;
                stringMRamt = null; 
                objdatarow = null; 
                objdatatable = null;
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
                //objDatasetResult = null;
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
        }

        private void LoadProcesstabData(DataRow objDataRow)
        {
            string stringDeliverBy = "";
            string stringduedte = "";
            string stringProcessID = "";
            DateTime objDueDate01 = new DateTime();
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

        protected void lnkbtApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMRStatus.Text == "CANCELLED")
                {
                    CommonFunctions.ShowMessageboot(this, "This Record already Cancelled");
                }
                else
                {
                    SaveData("APPROVED", "");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }

        }

        protected void lnkbtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidationReject())
                {
                    if (txtMRStatus.Text == "CANCELLED")
                    {
                        CommonFunctions.ShowMessageboot(this, "This Record already Cancelled");
                    }
                    else
                    {
                        SaveData("REJECTED", "REJECTED");
                    }
                }
                
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        private bool ValidationReject()
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";

                if (txtReject.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "-  Reject Reason" + "\\r\\n";
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
            finally
            {
                boolStatus = true;
                stringOverallMsg = null;
            }

            return boolStatus;
        }

        private DataTable LoadCancellationRecord(string stringReqID)//fixed
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC1013R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            DataTable objDataTable = null;
            string stringServiceType1 = "";
            string stringexp012 = "";
            string stringCancellationReason = "";
            string stringINC_CANCEL_CHRG = "";
            string stringPAYTYP_ID = "";
            DataTable objuserRole = null;
            lnkbtApprove.Visible = false;
            lnkbtnReject.Visible = false;
            lnkbtApprove.Enabled = false;
            txtReject.Enabled = false;
            txtReject.CssClass = "form-control ReadOnly";
            lnkbtnReject.Enabled = false;
            try
            {
                if (stringReqID != null && stringReqID.Trim().Length > 0)
                {
                    stringServiceType1 = "List4R1V1";
                    stringexp012 = "And mrcans.be_id= '" + stringbeid + "'  And mrcans.request_id= '" + stringReqID.ToString() + "'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t4"] != null && objDatasetResult.Tables["t4"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t4"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {

                            stringCancellationReason = objDataTable.Rows[0]["CR_ID"].ToString();
                            if (ddlCR.Items.FindByValue(stringCancellationReason) != null)
                            { ddlCR.ClearSelection(); ddlCR.Items.FindByValue(stringCancellationReason).Selected = true; }
                            txtRemarks.Text = objDataTable.Rows[0]["Remarks"].ToString();
                            txtRefund.Text = objDataTable.Rows[0]["REFUND_TO"].ToString();
                            txtCancelAmt.Text = objDataTable.Rows[0]["CANCELLATION_CHARGES"].ToString();
                            txtRefundAmt.Text = objDataTable.Rows[0]["REFUND_AMT"].ToString();
                            txtCancelBy.Text = objDataTable.Rows[0]["MR_CANCEL_BY"].ToString();
                            stringINC_CANCEL_CHRG = objDataTable.Rows[0]["INC_CANCEL_CHRG"].ToString();
                            if (stringINC_CANCEL_CHRG == "Y")
                            {
                                ChkCanCharge.Checked = true;
                            }
                            else
                            {
                                ChkCanCharge.Checked = false;
                            }
                            stringPAYTYP_ID = objDataTable.Rows[0]["PAYTYP_ID"].ToString();
                            if (ddlPymtMode.Items.FindByValue(stringPAYTYP_ID) != null)
                            { ddlPymtMode.ClearSelection(); ddlPymtMode.Items.FindByValue(stringPAYTYP_ID).Selected = true; }
                            if(objDataTable.Rows[0]["SUP_STATUS"].ToString() == "PENDING" || objDataTable.Rows[0]["SUP_STATUS"].ToString() == "REJECTED")
                            { 
                                ControlsEnableFlag(false);
                            }
                            else
                            { 
                                ControlsEnableFlag(false);
                            }
                            if (Session["UserRolestable"] != null)
                            {
                                objuserRole = (DataTable)Session["UserRolestable"];

                                if (objuserRole != null && objuserRole.Rows.Count > 0)
                                {
                                    if (objuserRole.Select("GROUP_ID='HIMS SUPERVISOR' or GROUP_ID='HOD' ").Length > 0)
                                    {
                                        if (txtMRStatus.Text.Trim() == "CANCELLED")
                                        {
                                            lnkbtnReject.Visible = false;
                                            lnkbtApprove.Visible = false;
                                        }
                                        else
                                        {
                                            lnkbtApprove.Visible = true;
                                            lnkbtnReject.Visible = true;
                                            txtReject.Enabled = true;
                                            txtReject.CssClass = "form-control ReadOnly"; 
                                        }
                                           
                                    }
                                }
                            }
                            lnkbtApprove.Enabled = true;
                            lnkbtnReject.Enabled = true; 
                            imgbtnSave.ToolTip = "Record Already Saved";
                        }
                        else
                        {
                            if (txtMRStatus.Text.Trim() == "CANCELLED")
                            {
                                ControlsEnableFlag(false);
                            }
                            else
                            {
                                ControlsEnableFlag(true);
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
                intToRecord = 0;
                stringbeid = null;
                objDataTable = null;
                stringServiceType1 = null;
                stringexp012 = null;
                stringCancellationReason = null;
                stringINC_CANCEL_CHRG = null;
                stringPAYTYP_ID = null;
                objuserRole = null;
            }
            return null;
        }

        private void ControlsEnableFlag(bool boolEnable)
        {
            try
            {
                ddlCR.Enabled = boolEnable;
                txtCDate.Enabled = boolEnable;
                ddlPymtMode.Enabled = boolEnable;
                //txtReject.Enabled = boolEnable;
                txtRemarks.Enabled = boolEnable;
                ChkCanCharge.Enabled = boolEnable;
                imgbtnSave.Enabled = boolEnable;
                txtCancelAmt.Enabled = boolEnable;

                if (txtCancelAmt.Enabled == false)
                {
                    txtCancelAmt.CssClass = "form-control ReadOnly";
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            
        }

        protected void ChkCanCharge_CheckedChanged(object sender, EventArgs e)
        {
            double doublePaidAmt = 0;
            double doublecanamt = 0;
            double doubleMRAmount = 0;
            string[] stringValues = null;
            string stringID = "";
            string string1 = "";
            string string2 = "";
            try
            {
                if (ChkCanCharge.Checked)
                {
                    txtCancelAmt.ReadOnly = true;
                    if (txtCancelAmt.ReadOnly == true)
                    {
                        txtCancelAmt.CssClass = "form-control ReadOnly";
                    }

                    doublePaidAmt = 0;
                    doublecanamt = 0;
                    doubleMRAmount = 0;
                    if (txtMRAmt.Text.Length > 0)
                    {
                        double.TryParse(txtMRAmt.Text, out doubleMRAmount);
                    }
                    if (txtPaidAmt.Text.Length > 0)
                    {
                        double.TryParse(txtPaidAmt.Text, out doublePaidAmt);
                    }
                    if (txtCancelAmt.Text.Length > 0)
                    {
                        double.TryParse(txtCancelAmt.Text, out doublecanamt);
                    }
                    if (doubleMRAmount >= doublecanamt)
                    {
                        if (doublePaidAmt >= doublecanamt)
                        {
                            var varrefundamt = (doublePaidAmt - doublecanamt);
                            stringID = "";

                            if (varrefundamt.ToString().Trim().Contains("."))
                            {
                                stringValues = varrefundamt.ToString().Trim().Split('.');
                                if (stringValues != null && stringValues.Length > 0)
                                {
                                    if (stringValues != null && stringValues.Length == 2)
                                    {
                                        string1 = stringValues[0];
                                        string2 = stringValues[1];
                                        if (string1.Length == 0 && string2.Length == 1)
                                        {
                                            stringID = "0" + varrefundamt.ToString().Trim() + "0";
                                        }
                                        else if (string1.Length == 0 && string2.Length == 2)
                                        {
                                            stringID = "0" + varrefundamt.ToString().Trim();
                                        }
                                        if (string2.Length == 0)
                                        {
                                            stringID = string1 + ".00";
                                        }
                                        else if (string2.Length == 1)
                                        {
                                            stringID = string1 + "." + string2 + "0";
                                        }
                                        else
                                        {
                                            decimal number = decimal.Parse(string2);
                                            string2 = number.ToString("00");
                                            stringID = string1 + "." + string2;
                                        }

                                    }
                                    else
                                    {
                                        stringID = stringValues[0] + ".00";
                                    }
                                }
                                else if (varrefundamt.ToString().Length > 0)
                                {
                                    stringID = varrefundamt.ToString() + ".00";
                                }
                            }
                            else if (varrefundamt.ToString().Length > 0)
                            {
                                stringID = varrefundamt.ToString() + ".00";
                            }
                            txtRefundAmt.Text = stringID.ToString();
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Cancellation Amount shount not greater than PAID Amount");
                        }
                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "Cancellation Amount shount not greater than MR Amount");
                    }
                }
                else
                { 
                    txtCancelAmt.ReadOnly = false;
                    txtCancelAmt.CssClass = "form-control ReadOnly";
                    txtRefundAmt.Text = txtPaidAmt.Text;
                    //LoadrefundAmount();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                doublePaidAmt = 0;
                doublecanamt = 0;
                doubleMRAmount = 0;
                stringValues = null;
                stringID = null;
                string1 = null;
                string2 = null;
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringSort = string.Empty;
            DataRowView objdatarowattachments = null;
            DataRow objDataRow = null;
            string stringstatus = "";
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null) { return; }

                objdatarowattachments = ((DataRowView)objGridViewRow.DataItem);
                objDataRow = objdatarowattachments.Row;
               
                if (objDataRow != null)
                {
                    stringstatus = objDataRow["SUP_STATUS"].ToString();
                     
                    if (stringstatus != null && stringstatus.Trim().Length > 0 && stringstatus.Trim().ToUpper() == "PENDING")
                    { ((Label)e.Row.FindControl("lblStatus")).Text = "Pending Supervisor Approval"; }
                    else if (stringstatus != null && stringstatus.Trim().Length > 0 && stringstatus.Trim().ToUpper() == "APPROVED")
                    { ((Label)e.Row.FindControl("lblStatus")).Text = "Supervisor Approved"; }
                    else if (stringstatus != null && stringstatus.Trim().Length > 0 && stringstatus.Trim().ToUpper() == "REJECTED")
                    { ((Label)e.Row.FindControl("lblStatus")).Text = "Supervisor Rejected"; }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringSort = null;
                objdatarowattachments = null;
                objDataRow = null;
                stringstatus = null;
            }
        }
        private void LoadReports(string stringFORMID, string stringreportname)
        {
            byte[] objbytereturn = null;
            string stringFile = string.Empty;
            string stringoutputresult = "";
            int intErrorCount = 0;
            string[] stringOutputResult = null;
            string[] stringInputs = new string[4];
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
            string stringRequetstorothers = "";
            string stringpatientname = "";
            string stringRequestid = "";
            string stringRequestorname = "";
            DataSet objDatasetAppsVariables = null;
            string stringLofoFlag = "";
            try
            {
                if (txtRequestNo.Text.Length > 0)
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


                    stringInputs[0] = stringbeid;
                    stringInputs[1] = txtRequestNo.Text.Trim();
                    stringInputs[2] = "";
                    stringInputs[3] = "";

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

                stringfromdate = null;
                stringtodate = null;

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
         
        protected void btncreditnotes_Click(object sender, EventArgs e)
        {
            if (txtRequestNo.Text.Trim().Length > 0)
            {
                LoadReports("DOP700098R1V1", "Credit Notes");
            }
        }



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

            DateTime objDateTimeFrom;
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
        #endregion
    }
}