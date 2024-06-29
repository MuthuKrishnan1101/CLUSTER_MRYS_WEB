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
    public partial class FC0003R1V1 : System.Web.UI.Page
    {
        public DataSet objDatasetAppsVariables;
        public bool boolbouser = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            string stringLockFields = "N";
            string stringReceipt = "N";
            string stringRequestID = "";
            string stringStatus = "";
            string stringcancellstatus = ""; 
            DataRow objDataRow = null;
            try
            {
                
                if (CommonFunctions.IsActive())
                {
                    if (Session["REQUESTID_PAYMENT"] != null)
                    {
                        stringRequestID = Session["REQUESTID_PAYMENT"].ToString();
                        Session["REQUESTID_PAYMENT"] = stringRequestID;
                    }
                    else
                    {
                        Session["REQUESTID_PAYMENT"] = null;
                    }
                    if (Request.QueryString["ReceiptID"] != null)
                    {
                        stringReceipt = Request.QueryString["ReceiptID"].ToString();
                        Session["ReceiptID"] = stringReceipt;
                    }
                    else
                    {
                        Session["ReceiptID"] = "";
                    }

                    if (Request.QueryString["LockFlag"] != null)
                    {
                        stringLockFields = Request.QueryString["LockFlag"].ToString();
                        Session["stringLockFields"] = stringLockFields.ToString();
                    }
                    else
                    {
                        Session["stringLockFields"] = null;
                    }


                    if (!IsPostBack)
                    {
                        ViewState["DEPTOU"] = null;
                        VerifyAccessRights();
                        CommonFunctions.HeaderName(this, "FC0003R1V1");
                        hdfmramount.Value = "";
                        hdfddlBlockBill.Value = "";
                        hdfddlWApp.Value = "";
                        hdfddlWApproved.Value = "";
                        hdfddlWApproved.Value = "";
                        hdfreporttype.Value = "";
                        ResetVariables();
                        ClearValues();

                        LoadCurrencies();
                        LoadPaidAtLocation();
                        LoadPaymentModes();
                        LoadBanks();

                        if (Session["REQUESTID_PAYMENT"] != null)
                        {
                            LkBtnBack.Visible = true;
                            stringRequestID = Session["REQUESTID_PAYMENT"].ToString();
                            objDataRow = GetRequestDetails(stringRequestID);
                            if (objDataRow != null )
                            {
                                LoadAllDATA(stringRequestID);
                                string stringddldoctorwaiver = objDataRow["DOCTOR_WAIVER"].ToString();
                                string stringddlhospitalrwaiver = objDataRow["HOSPITAL_WAIVER"].ToString();
                                txtpatnamemrn.Text = objDataRow["patient_short_name"].ToString() + " [" + objDataRow["hrn_id"].ToString() + "] ";
                                ViewState["DEPTOU"] = objDataRow["DEPT_ID"].ToString();
                                LoadProcesstabData(objDataRow);
                                CheckFlagStatus(txtRequestNo.Text); 
                                ReportRelatedValues(objDataRow);
                                stringStatus = objDataRow["mr_status"].ToString(); 
                                stringcancellstatus = objDataRow["sup_status"].ToString();
                               
                                if (stringStatus.Trim().ToUpper() == "FORWARDED" || stringStatus.Trim().ToUpper() == "COLLECTED" || stringStatus.Trim().ToUpper() == "CANCELLED" || stringcancellstatus.ToUpper() == "PENDING")
                                { ControlsEnabledByProStatus("CANCELLED"); }
                                else { }

                                LoadReceiptDefaultValues(txtRequestNo.Text,out string stringdocwaiver, out string stringhoswaiver);
                                ComputeMRPaymentsave();


                                LoadDataPageload(txtRequestNo.Text.Trim().ToUpper(), stringddlhospitalrwaiver , stringddldoctorwaiver);
                            }
                        }

                        if (Request.QueryString.Get("ReceiptID") != null)
                        {
                            LoadReceiptData(Request.QueryString.Get("ReceiptID"));
                        }

                        if (stringLockFields == "TRUE")
                        {
                            LockFields();
                        }

                        if (Session["ssnUserRole"] != null)
                        {
                            DataTable objuserRole = (DataTable)Session["UserRolestable"];

                            if (objuserRole != null && objuserRole.Rows.Count > 0)
                            {
                                if (objuserRole.Select("Group_ID= 'FINANCE'").Length > 0)
                                {
                                    boolbouser = false;
                                    pnlheadertab.Visible = false;
                                    LkBtnBack.Visible = false;
                                    btnGenReceipt.Visible = false;
                                    pnlprocess.Visible = false;
                                }

                            }
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
                stringLockFields = null;
                stringReceipt = null;
                stringRequestID = null;
                stringStatus = null;
                stringcancellstatus = null; 
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
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private string ComputeMRPaymentsave()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V2";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            string stringRequestID1 = "";
            string stringexp = "";
            DataRow objdatarow = null;
            try
            {
                stringServiceType = "DEFAULT";
                stringexp = "";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables["t3"] != null &&  objDatasetResult.Tables["t3"].Rows.Count == 0)
                    {
                        objdatarow = objDatasetResult.Tables["t3"].NewRow();
                        objdatarow["be_id"] = CommonFunctions.GETBussinessEntity().ToString();

                        if (Session["REQUESTID_PAYMENT"] != null)
                        {
                            stringRequestID1 = (string)Session["REQUESTID_PAYMENT"];
                        }
                        objdatarow["Request_ID"] = stringRequestID1;
                        objdatarow["Additonal_MRStatus"] = "True";

                        objdatarow["delmark"] = "N";

                        CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                        objDatasetResult.Tables["t3"].Rows.Add(objdatarow);
                        objDatasetResult.Tables["t3"].Rows[0].RowState.ToString();

                    }
                    if (objDatasetResult != null && objDatasetResult.Tables["t3"].Rows.Count > 0)
                    {
                        objDatasetResult = objDatasetResult.GetChanges();
                        stringServiceType = "OperationServiceDML";
                        objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);
                        if (intErrorCount == 0)
                        {
                            return "";
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
                stringRequestID1 = null;
                stringexp = null;
                objdatarow = null;
            }
            return "";
        }
        private void LoadDataPageload(string stringRequestID , string stringddlhospitalrwaiver , string stringddldoctorwaiver) 
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V2";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTableMrPayment = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            DataRow objDataRowPymtDt = null;
            decimal decimalBalAnount = 0;
            decimal decimallessAnount = 0;
            decimal decimalRoundingAmt = 0;
            try
            {

                stringServiceType = "List2R1V1";
                stringexp012 = "And mrpt.be_id= '" + stringbeid + "'  And mrpt.request_id= '" + txtRequestNo.Text.Trim().ToUpper() + "' ";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                    {
                        objDataTableMrPayment = objDatasetResult.Tables["t2"];
                    }
                    if (objDataTableMrPayment != null && objDataTableMrPayment.Rows.Count > 0)
                    {
                        objDataRowPymtDt = objDataTableMrPayment.Rows[0];

                        decimalBalAnount = 0;
                        decimalRoundingAmt = 0; 
                        hidFldMRPayID.Value = objDataRowPymtDt["mr_pymt_id"].ToString();
                        if (objDataRowPymtDt["gross_amt"] != null && objDataRowPymtDt["gross_amt"].ToString().Trim().Length > 0)
                        { txtSubTotal.Text = Convert.ToDecimal(objDataRowPymtDt["gross_amt"]).ToString("##,##0.00"); }

                        if (objDataRowPymtDt["tax_amt"] != null && objDataRowPymtDt["tax_amt"].ToString().Trim().Length > 0)
                        { txtGST.Text = Convert.ToDecimal(objDataRowPymtDt["tax_amt"]).ToString("##,##0.00"); }

                        if (objDataRowPymtDt["reference_1"] != null && objDataRowPymtDt["reference_1"].ToString().Trim().Length > 0)
                        { lblGSTvalue.Text = Convert.ToDecimal(objDataRowPymtDt["reference_1"]).ToString("##,##0") + "%"; }

                        if (objDataRowPymtDt["total_amt"] != null && objDataRowPymtDt["total_amt"].ToString().Trim().Length > 0)
                        { txtAmtPayable.Text = Convert.ToDecimal(objDataRowPymtDt["total_amt"]).ToString("##,##0.00"); }
                        if (objDataRowPymtDt["paid_amt"] != null && objDataRowPymtDt["paid_amt"].ToString().Trim().Length > 0)
                        { txtpaidamt.Text = Convert.ToDecimal(objDataRowPymtDt["paid_amt"]).ToString("##,##0.00"); }

                        if (objDataRowPymtDt["balance_amt"] != null && objDataRowPymtDt["balance_amt"].ToString().Trim().Length > 0)
                        {
                            decimalBalAnount = Convert.ToDecimal(objDataRowPymtDt["balance_amt"]);
                            txtBalAmt.Text = decimalBalAnount.ToString("##,##0.00");
                        } 
                        if (objDataRowPymtDt["Rounding_Total"] != null && objDataRowPymtDt["Rounding_Total"].ToString().Trim().Length > 0)
                        {
                            decimalRoundingAmt = Convert.ToDecimal(objDataRowPymtDt["Rounding_Total"]);
                            txtRndAmtPayable.Text = decimalRoundingAmt.ToString("##,##0.00");
                        } 
                        if(decimalBalAnount > 0)
                        {
                            if (boolbouser)
                            {
                                btnGenReceipt.Visible = true;
                            }
                        }
                        else
                        { 
                            btnGenReceipt.Visible = false;
                        }

                        if(txtWApproved.Text == "HALFWAIVER")
                        {
                            pnlhalfvaiverdetail.Visible = true;
                             
                            if (objDataRowPymtDt["WAIVER_LESS_AMOUNT"] != null && objDataRowPymtDt["WAIVER_LESS_AMOUNT"].ToString().Trim().Length > 0)
                            {
                                decimallessAnount = Convert.ToDecimal(objDataRowPymtDt["WAIVER_LESS_AMOUNT"]);
                                txtLesswaiver.Text = decimallessAnount.ToString("##,##0.00");
                            }
                            string strngcomplewavier = "";
                            if(stringddlhospitalrwaiver  == "APPROVED")
                            {
                                strngcomplewavier = "HOSPITAL";
                            }
                            else if(stringddldoctorwaiver == "APPROVED")
                            {
                                strngcomplewavier = "DOCTOR";
                            }
                            if (strngcomplewavier.Length > 0)
                            { 
                                lbllesswaver.Text = " Less Wavier ( " + strngcomplewavier + " )";
                            } 
                        }
                        else
                        {
                            pnlhalfvaiverdetail.Visible = false;
                        }
                        LoadBillingItemsGrid();
                        BtnPymt_Click(btnPymt, null);
                        if (decimalBalAnount == 0) { btnPymt.Enabled = false; btnGenReceipt.Enabled = false; }
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
                objDataTableMrPayment = null;
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
                objDataRowPymtDt = null;
                decimalBalAnount = 0;
                decimalRoundingAmt = 0;
            }

        }
        private void LoadDatasavedata(string stringRequestID, string stringddlhospitalrwaiver, string stringddldoctorwaiver)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V2";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTablePymtDt = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            DataRow objDataRowPymtDt = null;
            decimal decimalBalAmount = 0;
            decimal decimallessAnount = 0;
            decimal decimalRoundingAmt = 0;
            try
            {

                stringServiceType = "List2R1V1";
                stringexp012 = "And mrpt.be_id= '" + stringbeid + "'  And mrpt.request_id= '" + stringRequestID.Trim().ToUpper() + "' ";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                    {
                        objDataTablePymtDt = objDatasetResult.Tables["t2"];
                    }
                    if (objDataTablePymtDt != null && objDataTablePymtDt.Rows.Count > 0)
                    {
                        objDataRowPymtDt = objDataTablePymtDt.Rows[0];

                        decimalBalAmount = 0;
                        decimalRoundingAmt = 0;

                        hidFldMRPayID.Value = objDataRowPymtDt["mr_pymt_id"].ToString();
                        if (objDataRowPymtDt["gross_amt"] != null && objDataRowPymtDt["gross_amt"].ToString().Trim().Length > 0)
                        { txtSubTotal.Text = Convert.ToDecimal(objDataRowPymtDt["gross_amt"]).ToString("##,##0.00"); }

                        if (objDataRowPymtDt["tax_amt"] != null && objDataRowPymtDt["tax_amt"].ToString().Trim().Length > 0)
                        { txtGST.Text = Convert.ToDecimal(objDataRowPymtDt["tax_amt"]).ToString("##,##0.00"); }

                        if (objDataRowPymtDt["total_amt"] != null && objDataRowPymtDt["total_amt"].ToString().Trim().Length > 0)
                        { txtAmtPayable.Text = Convert.ToDecimal(objDataRowPymtDt["total_amt"]).ToString("##,##0.00"); }
                        if (objDataRowPymtDt["paid_amt"] != null && objDataRowPymtDt["paid_amt"].ToString().Trim().Length > 0)
                        { txtpaidamt.Text = Convert.ToDecimal(objDataRowPymtDt["paid_amt"]).ToString("##,##0.00"); }

                        if (objDataRowPymtDt["balance_amt"] != null && objDataRowPymtDt["balance_amt"].ToString().Trim().Length > 0)
                        {
                            decimalBalAmount = Convert.ToDecimal(objDataRowPymtDt["balance_amt"]);
                            txtBalAmt.Text = decimalBalAmount.ToString("##,##0.00");
                        }
                        if (txtWApproved.Text == "HALFWAIVER")
                        {
                            pnlhalfvaiverdetail.Visible = true;
                             
                            if (objDataRowPymtDt["WAIVER_LESS_AMOUNT"] != null && objDataRowPymtDt["WAIVER_LESS_AMOUNT"].ToString().Trim().Length > 0)
                            {
                                decimallessAnount = Convert.ToDecimal(objDataRowPymtDt["WAIVER_LESS_AMOUNT"]);
                                txtLesswaiver.Text = decimallessAnount.ToString("##,##0.00");
                            }

                            string strngcomplewavier = "";
                            if (stringddlhospitalrwaiver == "APPROVED")
                            {
                                strngcomplewavier = "HOSPITAL";
                            }
                            else if (stringddldoctorwaiver == "APPROVED")
                            {
                                strngcomplewavier = "DOCTOR";
                            }
                            if (strngcomplewavier.Length > 0)
                            {
                                lbllesswaver.Text = " Less Wavier ( " + strngcomplewavier + " )";
                            }
                        } 
                        else
                        {
                            pnlhalfvaiverdetail.Visible = false;
                        }
                        //if (objDataRowPymtDt["rounding_amt"] != null && objDataRowPymtDt["rounding_amt"].ToString().Trim().Length > 0)
                        //{ decimalRoundingAmt = Convert.ToDecimal(objDataRowPymtDt["rounding_amt"]); }

                        //decimal dblBalAmt = decimalBalAmount - decimalRoundingAmt;
                        //if (decimalBalAmount == 0) { dblBalAmt = decimalBalAmount; }
                        //txtRndAmtPayable.Text = dblBalAmt.ToString("##,##0.00");
                        if (objDataRowPymtDt["Rounding_Total"] != null && objDataRowPymtDt["Rounding_Total"].ToString().Trim().Length > 0)
                        {
                            decimalRoundingAmt = Convert.ToDecimal(objDataRowPymtDt["Rounding_Total"]);
                            txtRndAmtPayable.Text = decimalRoundingAmt.ToString("##,##0.00");
                        }
                        if (decimalBalAmount > 0)
                        {
                            if (boolbouser)
                            {
                                btnGenReceipt.Visible = true;
                            }
                        }
                        else
                        {
                            btnGenReceipt.Visible = false; 
                        }
                        LoadBillingItemsGrid();

                        if (decimalBalAmount <= 0)
                        {
                            btnGenReceipt.Enabled = false;
                        }
                        else
                        {
                            btnGenReceipt.Enabled = true;
                        }
                    }
                    else
                    {


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
                objDataTablePymtDt = null;
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
                objDataRowPymtDt = null;
                decimalBalAmount = 0;
                decimalRoundingAmt = 0;
            }
        }


        #region Image Button

        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)//fix
        {

        }

        protected void imgBtnNew_Click(object sender, ImageClickEventArgs e)//fix
        {
            try
            {

                ClearValues();
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)//fix
        {
            try
            {
                if (ValidateControls01())
                {
                    if (ValidateControls())
                    {

                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
           
        }

        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)//fix
        {

        }

        protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)//fix
        {
            try
            {
                ClearValues();
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        protected void imgBtnPrint_Click(object sender, ImageClickEventArgs e)//fix
        {
            try
            {
                GenerateReceipt(txtRequestNo.Text.Trim(), hidFldMRPayID.Value, txtReceiptNo.Text.Trim(), true);

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        protected void imgBtnSecurity_Click(object sender, ImageClickEventArgs e)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3008R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            DataRow objDataRow = null;
            try
            {
                if (txtReceiptNo.Text.Trim().Length > 0)
                {
                    stringServiceType = "List2R1V1";
                    stringexp012 = "And mrpr.be_id= '" + stringbeid + "'  And mrpr.request_id= '" + txtReceiptNo.Text.Trim() + "' ";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
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
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
                objDataRow = null;
            }
        }

        protected void imgBtnHelp_Click(object sender, ImageClickEventArgs e)//fix
        {

        }

        #endregion

        #region Button

        protected void BtnPymt_Click(object sender, EventArgs e)//fix
        {

            string stringRequestID = "";
            try
            {
                Panel1.Visible = true;
                if (Session["REQUESTID_PAYMENT"] != null)
                {
                     stringRequestID = Session["REQUESTID_PAYMENT"].ToString(); 
                } 
                LoadReceiptDefaultValues(stringRequestID, out string stringdocwaiver, out string stringhoswaiver);
                LoadPaymentReceiptsGrid(hidFldMRPayID.Value);
                txtPayName.Focus();
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringRequestID = null;
            }
        }

        protected void BtnGenReceipt_Click(object sender, EventArgs e)//fix
        {
            decimal decimalBalance = 0;
            decimal decimalPayAmount = 0;
            try
            {
                if (ValidateControls01())
                {
                    if (ValidateControls() && ValidateBusinessLogic())
                    {
                        decimalBalance = 0;
                        decimalPayAmount = 0;
                        if (txtBalAmt.Text.Trim().Length > 0 && txtPayAmt.Text.Trim().Length > 0)
                        {
                            decimal.TryParse(txtBalAmt.Text.Trim(), out decimalBalance);
                            decimal.TryParse(txtPayAmt.Text.Trim(), out decimalPayAmount);

                            lblMsgLine1.Text = "";
                            lblMsgLine2.Text = "";
                            lblMsgLine3.Text = "";

                            if (decimalPayAmount != decimalBalance)
                            {
                                lblMsgLine1.ToolTip = "PARTIAL";

                                if (txtCardNo.Text.Trim().Length > 0 && ddlBank.Text.Trim().Length > 0)
                                {
                                    lblMsgLine1.Text = "You are paying $ " + txtPayAmt.Text.Trim() + "by " + ddlPayType.Text.Trim() + "(" + ddlBank.Text.Trim() + ")" + ".";
                                    lblMsgLine2.Text = "Cheque/Card No is " + txtCardNo.Text.Trim().ToUpper();
                                }
                                else
                                {
                                    lblMsgLine1.Text = "You are paying $" + txtPayAmt.Text.Trim() + " by " + ddlPayType.Text.Trim();
                                    lblMsgLine2.Text = "";
                                }

                                lblMsgLine3.Text = "The amount you have entered is not tally. Are you sure you want to pay the \"Partial Payment\"?";
                                ModalPopupExtender01.Show();
                            }
                            else
                            {
                                lblMsgLine1.ToolTip = "NORMAL";
                                if (txtCardNo.Text.Trim().Length > 0 && ddlBank.Text.Trim().Length > 0)
                                {
                                    lblMsgLine1.Text = "You are paying $" + txtPayAmt.Text.Trim() + " by " + ddlPayType.Text.Trim() + "(" + ddlBank.Text.Trim() + ")" + ".";
                                    lblMsgLine2.Text = "Cheque/Card No is " + txtCardNo.Text.Trim().ToUpper();
                                }
                                else
                                {
                                    lblMsgLine1.Text = "You are paying $" + txtPayAmt.Text.Trim() + " by " + ddlPayType.Text.Trim();
                                    lblMsgLine2.Text = "";
                                }

                                lblMsgLine3.Text = "Are you sure you want to continue payment?";
                                ModalPopupExtender01.Show();

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
         
        protected void BtnOk_Click(object sender, EventArgs e)//fix
        { 
            try
            {
                GenerateReceipt(); 
                Response.Redirect("FC0003R1V1.aspx?" + Page.ClientQueryString);
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            } 
        }

        protected void BtnNo_Click(object sender, EventArgs e)//fix
        {
            try
            {
                Response.Redirect("FC0003R1V1.aspx?" + Page.ClientQueryString);
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        #endregion

        #region Dropdown

        protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)//fix
        {
            string stringPayTypID = "";
            DataTable objDataTable = null;
            DataRow objDataRow = null;
            DataRow[] objDataRows = null;
            string stringEnableBank = "";
            string stringEnableCheque = "";
            try
            {
                if (ddlPayType.SelectedIndex > 0)
                {
                    stringPayTypID = ddlPayType.SelectedValue;
                    if (Session["SSNLOADPAYMENTMODES"] != null)
                    {
                        objDataTable = (DataTable)Session["SSNLOADPAYMENTMODES"];
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            if (stringPayTypID != null && stringPayTypID.Trim().Length > 0)
                            {
                                objDataRows = objDataTable.Select("paytyp_id='" + stringPayTypID + "' and delmark='N'");
                                if (objDataRows != null && objDataRows.Length > 0)
                                {
                                    objDataRow = objDataRows[0];
                                    stringEnableBank = objDataRow["enable_bank"].ToString();
                                    stringEnableCheque = objDataRow["enable_chequeno"].ToString();

                                    if (stringEnableBank != null && stringEnableBank.Trim().ToUpper() == "Y") { ddlBank.Enabled = true; }
                                    else { ddlBank.Enabled = false; }

                                    txtCardNo.Text = "";
                                    if (stringEnableCheque != null && stringEnableCheque.Trim().ToUpper() == "Y") { txtCardNo.Enabled = true; }
                                    else { txtCardNo.Enabled = false; }

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
                stringPayTypID = null;
                objDataTable = null;
                objDataRow = null;
                objDataRows = null;
                stringEnableBank = null;
                stringEnableCheque = null;
            }
        }

        protected void ddlPaidAt_SelectedIndexChanged(object sender, EventArgs e)//fix
        {
            string stringPayTypID = "";
            DataTable objDataTable = null;
            DataRow objDataRow = null;
            DataRow[] objDataRows = null;
            string stringExtReceipt = "";
            try
            {
                if (ddlPayType.SelectedIndex > 0)
                {
                    stringPayTypID = ddlPayType.SelectedValue;
                    if (Session["SSNLOADPAIDAT"] != null)
                    {
                        objDataTable = (DataTable)Session["SSNLOADPAIDAT"];
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            if (stringPayTypID != null && stringPayTypID.Trim().Length > 0)
                            {
                                objDataRows = objDataTable.Select("loc_id='" + stringPayTypID + "' and delmark='N'");
                                if (objDataRows != null && objDataRows.Length > 0)
                                {
                                    objDataRow = objDataRows[0];
                                    stringExtReceipt = objDataRow["allow_external_receipt"].ToString();
                                    if (stringExtReceipt != null && stringExtReceipt.Trim().ToUpper() == "Y")
                                    { txtExtReceiptNo.Enabled = true; }
                                    else { txtExtReceiptNo.Enabled = false; }
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
                stringPayTypID = null;
                objDataTable = null;
                objDataRow = null;
                objDataRows = null;
                stringExtReceipt = null;
            }
        }

        #endregion

        #region Link button
        protected void LkBtnBack_Click(object sender, EventArgs e)//fix
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

        #endregion
         
        #region Methods/Functions
        private void GenerateReceipt()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V2";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            string stringexp = "";
            DataRow objdatarow = null;
            string stringRequestID = "";
            string stringReceiptID = "";
            DataSet objDataSet = null;
            DataRow[] objdatarow1 = null;
            int intbalanceamt = 0;
            try
            {
                stringServiceType = "DEFAULT";
                stringexp = "";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (objDatasetResult != null && objDatasetResult.Tables["t4"] != null && objDatasetResult.Tables["t4"].Rows.Count == 0)
                {
                    objdatarow = objDatasetResult.Tables["t4"].NewRow();
                    objdatarow["be_id"] = CommonFunctions.GETBussinessEntity().ToString();

                    objdatarow["Receipt_ID"] = txtReceiptNo.Text.Trim();
                    objdatarow["MR_Pymt_ID"] = hidFldMRPayID.Value;
                    if (Session["G11EOSUser_Name"] != null)
                    { objdatarow["PymtCounter_ID"] = Session["G11EOSUser_Name"].ToString(); }
                    else { objdatarow["PymtCounter_ID"] = ""; }
                    if (ddlCurrency.SelectedItem != null && ddlCurrency.SelectedValue.ToString().Length > 0)
                    {
                        objdatarow["Cur_ID"] = ddlCurrency.SelectedItem.Value;
                    }
                    if (ddlPayType.SelectedItem != null && ddlPayType.SelectedValue.ToString().Length > 0)
                    {
                        objdatarow["PayTyp_ID"] = ddlPayType.SelectedItem.Value;
                    }
                    
                    objdatarow["Payee_Name"] = txtPayName.Text;

                    objdatarow["Rcvd_Amt"] = Convert.ToDecimal(txtPayAmt.Text);
                    // objdatarow["RcvdDate"] = DateTime.Today.Date;
                    objdatarow["Rcvd_By"] = Session["G11EOSUser_Name"].ToString();
                    if (ddlBank.SelectedItem != null && ddlBank.SelectedValue.ToString().Length > 0)
                    {
                        objdatarow["Bank_Name"] = ddlBank.SelectedItem.Value;
                    }
                   
                    objdatarow["Cheque_no"] = txtCardNo.Text.Trim().ToUpper();
                    objdatarow["Long_Name"] = "";
                    objdatarow["Remarks"] = "";
                    if (ddlPaidAt.SelectedItem != null && ddlPaidAt.SelectedValue.ToString().Length > 0)
                    {
                        objdatarow["Paid_At"] = ddlPaidAt.SelectedItem.Value;
                    }
                   
                    objdatarow["Ext_Receipt_ID"] = txtExtReceiptNo.Text;
                    if (txtOverpayAmt.Text == "") { objdatarow["Overpay_Amt"] = 0; }
                    else { objdatarow["Overpay_Amt"] = Convert.ToDecimal(txtOverpayAmt.Text); }

                     if (txtDORefund.Text.Trim().Length > 0) { objdatarow["Date_Of_Refund"] = CommonFunctions.ConvertToDateTime(txtDORefund.Text.Trim(), "dd-MM-yyyy"); }

                    objdatarow["delmark"] = "N";
                    CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                    objDatasetResult.Tables["t4"].Rows.Add(objdatarow);
                    objDatasetResult.Tables["t4"].Rows[0].RowState.ToString();

                }
                objDatasetResult = objDatasetResult.GetChanges();
                stringServiceType = "OperationServiceDML";
                objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);
                if (intErrorCount == 0)
                {
                    PrintReceiptDupicate(txtReceiptNo.Text.Trim());
                    ClearValues(); 
                    stringRequestID = "";
                    if (Session["REQUESTID_PAYMENT"] != null)
                    {
                        stringRequestID = Session["REQUESTID_PAYMENT"].ToString();
                    }
                    LoadReceiptDefaultValues(stringRequestID,out string stringdocwaiver, out string stringhoswaiver);
                    LoadPaymentReceiptsGrid(hidFldMRPayID.Value);
                    LoadDatasavedata(stringRequestID, stringhoswaiver, stringdocwaiver);
                    ClearValues();

                    stringReceiptID = "";
                    objdatarow1 = null;
                    stringReceiptID = "";
                    if (Session["LoadPaymentReceiptsGrid"] != null)
                    {
                        objDataSet = (DataSet)Session["LoadPaymentReceiptsGrid"];

                        if (objDataSet != null && objDataSet.Tables[1] != null && objDataSet.Tables[1].Rows.Count > 0)
                        {
                            objdatarow1 = objDataSet.Tables[1].Select("MR_Pymt_ID = '" + hidFldMRPayID.Value.ToString() + "'");
                            if (objdatarow1 != null && objdatarow1.Length > 0)
                            {
                                stringReceiptID = objdatarow1[0]["Receipt_ID"].ToString();
                                txtReceiptNo.Text = stringReceiptID;
                            }
                        }
                    }
                    Session["LoadPaymentReceiptsGrid"] = null; 

                    if(txtBalAmt.Text.Length > 0)
                    {
                        intbalanceamt = 0;

                        double doubleValue = Convert.ToDouble(txtBalAmt.Text.Trim());
                        intbalanceamt = Convert.ToInt32(doubleValue);

                        if (intbalanceamt == 0)
                        {
                            LoadPendingItems(stringRequestID);
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
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = null;
                stringexp = null;
                objdatarow = null;
                stringRequestID = null;
                stringReceiptID = null;
                objDataSet = null;
                objdatarow1 = null;
                intbalanceamt = 0;
            }
        }
        private void LoadPendingItems(string stringRequestID)//fix
        {

            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "mrpend.modified_on desc";
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
                if (stringRequestID.Trim().Length > 0)
                {
                    stringServiceType = "List12R1V1";
                    stringExpression = "and mrpend.be_id='" + stringBoID + "' and mrpend.request_id='" + stringRequestID + "' ";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, 0, int.MaxValue, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t12"] != null && objDatasetResult.Tables["t12"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t12"];
                            Session["PendingItemsList"] = objDataTable;
                        }
                        else
                        {
                            Session["PendingItemsList"] = null;
                        }
                    }
                    else
                    {
                        Session["PendingItemsList"] = null;
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
            }
        }
        private void LoadReceiptData(string stringReceiptID)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3008R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringTemp = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            DataRow objDataRow = null;
            try
            {
                if (stringReceiptID != null && stringReceiptID.Trim().Length > 0)
                { 
                    stringServiceType = "List2R1V1";
                    stringexp012 = "And mrpr.be_id= '" + stringbeid + "'  And mrpr.receipt_id= '" + stringReceiptID + "'  ";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t2"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            objDataRow = objDataTable.Rows[0];

                            txtReceiptNo.Text = stringReceiptID;
                            txtPayName.Text = objDataRow["payee_name"].ToString();

                            if (objDataRow["rcvd_amt"] != null && objDataRow["rcvd_amt"].ToString().Length > 0)
                            { txtPayAmt.Text = Convert.ToDecimal(objDataRow["rcvd_amt"]).ToString("##,##0.00"); }

                            if (objDataRow["rcvd_date"] != null && objDataRow["rcvd_date"].ToString().Length > 0)
                            { txtPayAmt.Text = Convert.ToDateTime(objDataRow["rcvd_date"]).ToString("dd-MM-yyyy"); }

                            stringTemp = objDataRow["bank_name"].ToString();
                            ddlBank.ClearSelection();
                            if (ddlBank.Items.FindByValue(stringTemp) != null)
                            { ddlBank.Items.FindByValue(stringTemp).Selected = true; }

                            txtCardNo.Text = objDataRow["cheque_no"].ToString();

                            stringTemp = objDataRow["cur_id"].ToString();
                            ddlCurrency.ClearSelection();
                            if (ddlCurrency.Items.FindByValue(stringTemp) != null)
                                ddlCurrency.Items.FindByValue(stringTemp).Selected = true;
                            else ddlCurrency.SelectedIndex = 0;

                            stringTemp = objDataRow["paytyp_id"].ToString();
                            ddlPayType.ClearSelection();
                            if (ddlPayType.Items.FindByValue(stringTemp) != null)
                                ddlPayType.Items.FindByValue(stringTemp).Selected = true;
                            else ddlPayType.SelectedIndex = 0;

                            stringTemp = objDataRow["paid_at"].ToString();
                            ddlPaidAt.ClearSelection();
                            if (ddlPaidAt.Items.FindByValue(stringTemp) != null)
                                ddlPaidAt.Items.FindByValue(stringTemp).Selected = true;
                            else ddlPaidAt.SelectedIndex = 0;

                            txtExtReceiptNo.Text = objDataRow["external_receipt_id"].ToString();

                            if (objDataRow["overpay_amt"] != null && objDataRow["overpay_amt"].ToString().Length > 0)
                            { txtOverpayAmt.Text = Convert.ToDecimal(objDataRow["overpay_amt"]).ToString("##,##0.00"); }
                             
                            if (objDataRow["date_of_refund"] != null && objDataRow["date_of_refund"].ToString().Length > 0)
                            { txtDORefund.Text = Convert.ToDateTime(objDataRow["date_of_refund"]).ToString("dd-MM-yyyy"); }

                            if (objDataRow["rcvd_amt"] != null && objDataRow["rcvd_amt"].ToString().Length > 0)
                            { txtPayAmt.Text = Convert.ToDecimal(objDataRow["rcvd_amt"]).ToString("0.00"); }

                            if (objDataRow["overpay_amt"] != null && objDataRow["overpay_amt"].ToString().Length > 0)
                            { txtOverpayAmt.Text = Convert.ToDecimal(objDataRow["overpay_amt"]).ToString("0.00"); }

                            if (objDataRow["Rcvd_Date"] != null && objDataRow["Rcvd_Date"].ToString().Length > 0)
                            { txtPayDate.Text = Convert.ToDateTime(objDataRow["Rcvd_Date"]).ToString("dd-MM-yyyy"); }

                            txtPayName.Focus();
                            Session["stringDMLIndicator"] = "U";
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
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
                objDataRow = null;
            }
        }

        private void LoadReceiptDefaultValues(string stringRequestID,out string stringddldoctorwaiver,out string stringddlhospitalrwaiver) 
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0027R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTableReq = null;
            string stringBoID = ""; 
            DataRow objDataRow = null;
            DataRow objDataRowReq = null;
            string stringRptReqID = "";
            string stringRequestBy = "";
            string stringReqOthers = "";
            string stringPatAddress = "";
            string stringPatientName = "";
            string stringhealthhub = "";
            string stringmailAddresspayment = "";
            string stringpayeename = "";
            string stringServiceType = "";
            string stringexp012 = "";
            string stringReqID = "";
            string stringReqName = "";
            stringddldoctorwaiver = "";
            stringddlhospitalrwaiver = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            { 
                objDataRow = GetRequestDetails(stringRequestID);
                if (objDataRow != null)
                { 
                    stringddldoctorwaiver = objDataRow["DOCTOR_WAIVER"].ToString();
                    stringddlhospitalrwaiver = objDataRow["HOSPITAL_WAIVER"].ToString();
                    stringRptReqID = objDataRow["rptreq_id"].ToString();
                    stringRequestBy = objDataRow["requested_by"].ToString();
                    stringReqOthers = objDataRow["REQUEST_OTHERS"].ToString();
                    stringPatAddress = objDataRow["req_address"].ToString();
                    stringPatientName = objDataRow["patient_short_name"].ToString();
                    stringhealthhub = objDataRow["received_from"].ToString();
                    stringmailAddresspayment = objDataRow["REQ_MAIL_ADDRESS"].ToString();
                    stringpayeename = objDataRow["Requested_by"].ToString();
                    if (stringRptReqID != null && stringRptReqID.Trim().Length > 0)
                    { 
                        stringServiceType = "List1R1V1";
                        stringexp012 = "and mrreq.be_id ='" + stringBoID + "' and mrreq.RptReq_ID='" + stringRptReqID + "' And mrreq.delmark= 'N'";

                        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                        if (interrorcount == 0)
                        {
                            if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                            {
                                objDataTableReq = objDatasetResult.Tables["t1"];
                            }
                            if (objDataTableReq != null && objDataTableReq.Rows.Count > 0)
                            {
                                objDataRowReq = objDataTableReq.Rows[0];
                                stringReqID = objDataRowReq["rptreq_id"].ToString();
                                stringReqName = objDataRowReq["short_name"].ToString();


                                if (stringReqName != null)
                                {
                                    if (stringReqName.Trim().ToUpper() == "OTHERS")
                                    {
                                        txtRequestor.Text = stringReqOthers;
                                        txtPayName.Text = stringReqOthers;
                                        txtAddress.Text = stringPatAddress;
                                    }
                                    else if (stringReqID.Trim().ToUpper() == "SELF")
                                    {
                                        txtRequestor.Text = stringPatientName;
                                        txtPayName.Text = stringPatientName;
                                        txtAddress.Text = stringPatAddress;
                                    }
                                    else
                                    {
                                        txtRequestor.Text = stringReqName;
                                        txtPayName.Text = stringReqName;

                                        txtAddress.Text = objDataRowReq["address"].ToString();
                                    }
                                }
                                if (stringhealthhub != null && stringhealthhub == "HealthHub")
                                {
                                    txtPayName.Text = stringpayeename;
                                    txtAddress.Text = stringmailAddresspayment;
                                }

                                txtPayDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                                txtPayAmt.Text = "0.00";
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                objDataTableReq = null;
                stringBoID = null; 
                objDataRow = null;
                objDataRowReq = null;
                stringRptReqID = null;
                stringRequestBy = null;
                stringReqOthers = null;
                stringPatAddress = null;
                stringPatientName = null;
                stringhealthhub = null;
                stringmailAddresspayment = null;
                stringpayeename = null;
                stringServiceType = null;
                stringexp012 = null;
                stringReqID = null;
                stringReqName = null;
            }
        }

        private void LoadPaymentReceiptsGrid(string stringMRPymtID)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3008R1V1";
            string stringOrderBy = "mrpr.Rcvd_Date asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            btnprint.Visible = false;
            try
            {
                stringServiceType = "List2R1V1";
                stringexp012 = "And mrpr.be_id= '" + stringbeid + "'  And mrpr.MR_Pymt_ID= '" + stringMRPymtID + "' ";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t2"];
                        Session["LoadPaymentReceiptsGrid"] = objDatasetResult;
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        Session["LoadPaymentReceiptsGridFC0001"] = objDataTable;
                        gvReceipt.DataSource = objDataTable;
                        gvReceipt.DataBind();

                        btnprint.Visible = true;
                        pnlresultgrid.Visible = true;
                        lblTotalRecords.InnerText = objDataTable.Rows.Count.ToString();
                    }
                    else
                    {
                        btnprint.Visible = false;
                        lblTotalRecords.InnerText = "0";
                        gvReceipt.DataSource = null;
                        gvReceipt.DataBind();
                        pnlresultgrid.Visible = false;
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
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
            }
        }

        private void LoadBillingItemsGrid()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V2";
            string stringOrderBy = "mrpd.Int_Row_ID asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            try
            {
                stringServiceType = "List4R1V1";
                stringexp012 = "And mrpd.be_id= '" + stringbeid + "'  And mrpd.mr_pymt_id= '" + hidFldMRPayID.Value + "' ";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t4"] != null && objDatasetResult.Tables["t4"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t4"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        gvList.DataSource = objDataTable;
                        gvList.DataBind();
                        for (int intCount = 0; intCount < gvList.Rows.Count; intCount++)
                        { gvList.Rows[intCount].Cells[0].Text = Convert.ToString(intCount + 1); }
                        pnlrestgridreporttype.Visible = true;
                        lblTotalrecCount.InnerText = objDataTable.Rows.Count.ToString();
                    }
                    else
                    {
                        lblTotalrecCount.InnerText = "0";
                        gvList.DataSource = null;
                        gvList.DataBind();
                        pnlrestgridreporttype.Visible = false;
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
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
            }
        }

        private void LoadPaymentModes()//fixed
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0018R1V1";
            string stringOrderBy = "mrpaytys.ORDER_ID asc,mrpaytys.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableLoadPaymentModes = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            try
            { 
                ddlPayType.Items.Clear(); 
                stringServiceType = "List1R1V1";
                stringexp012 = "And mrpaytys.be_id= '" + stringbeid + "' And mrpaytys.delmark= 'N'";
                if (Session["SSNLOADPAYMENTMODES"] != null)
                {
                    objdatatableLoadPaymentModes = (DataTable)Session["SSNLOADPAYMENTMODES"];
                }
                if ((objdatatableLoadPaymentModes == null) || (objdatatableLoadPaymentModes != null && objdatatableLoadPaymentModes.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadPaymentModes = objDatasetResult.Tables["t1"];
                            Session["SSNLOADPAYMENTMODES"] = objdatatableLoadPaymentModes;
                        } 
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                } 
                if (objdatatableLoadPaymentModes != null && objdatatableLoadPaymentModes.Rows.Count > 0)
                {
                    ddlPayType.DataTextField = "short_name";
                    ddlPayType.DataValueField = "paytyp_id";
                    ddlPayType.DataSource = objdatatableLoadPaymentModes;
                    ddlPayType.DataBind();
                    ddlPayType.Items.Insert(0, new ListItem("", ""));

                    if (ddlPayType.Items.FindByValue("CASH") != null)
                    { ddlPayType.ClearSelection(); ddlPayType.Items.FindByValue("CASH").Selected = true; }
                }
                else
                {
                    ddlPayType.DataSource = null;
                    ddlPayType.DataBind();
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
                objdatatableLoadPaymentModes = null;
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
            }
        }

        private void LoadCurrencies()//fixed
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0006R1V1";
            string stringOrderBy = "mrcurry.ORDER_ID asc,mrcurry.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableLoadCurrencies = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            try
            {
                ddlCurrency.Items.Clear();  
                stringServiceType = "List1R1V1";
                stringexp012 = "And mrcurry.be_id= '" + stringbeid + "' And mrcurry.delmark= 'N' ";
                if (Session["SSNLOADCURRENCIES"] != null)
                {
                    objdatatableLoadCurrencies = (DataTable)Session["SSNLOADCURRENCIES"];
                }
                if ((objdatatableLoadCurrencies == null) || (objdatatableLoadCurrencies != null && objdatatableLoadCurrencies.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadCurrencies = objDatasetResult.Tables["t1"];
                            Session["SSNLOADCURRENCIES"] = objdatatableLoadCurrencies;
                        }

                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }

                if (objdatatableLoadCurrencies != null && objdatatableLoadCurrencies.Rows.Count > 0)
                {
                    ddlCurrency.DataTextField = "short_name";
                    ddlCurrency.DataValueField = "cur_id";
                    ddlCurrency.DataSource = objdatatableLoadCurrencies;
                    ddlCurrency.DataBind();
                    ddlCurrency.Items.Insert(0, new ListItem("", ""));

                    if (ddlCurrency.Items.FindByValue("SGD") != null)
                    { ddlCurrency.ClearSelection(); ddlCurrency.Items.FindByValue("SGD").Selected = true; }

                    ddlCurrency.SelectedIndex = 1;
                }
                else
                {
                    ddlCurrency.DataSource = null;
                    ddlCurrency.DataBind();
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
                objdatatableLoadCurrencies = null;
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
            }
        }

        private void LoadPaidAtLocation()//fix - ok
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0016R1V1";
            string stringOrderBy = "palon.ORDER_ID asc,palon.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue; 
            DataTable objdatatableLoadPaidAt = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            try
            { 
                ddlPaidAt.Items.Clear(); 
                stringServiceType = "List1R1V1";
                stringexp012 = "And palon.be_id= '" + stringbeid + "'   And palon.delmark= 'N'";  
                if (Session["SSNLOADPAIDAT"] != null)
                {
                    objdatatableLoadPaidAt = (DataTable)Session["SSNLOADPAIDAT"];
                }
                if ((objdatatableLoadPaidAt == null) || (objdatatableLoadPaidAt != null && objdatatableLoadPaidAt.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadPaidAt = objDatasetResult.Tables["t1"];
                            Session["SSNLOADPAIDAT"] = objdatatableLoadPaidAt;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadPaidAt != null && objdatatableLoadPaidAt.Rows.Count > 0)
                {
                    objdatatableLoadPaidAt.DefaultView.Sort = "ORDER_ID asc,short_name asc";
                    objdatatableLoadPaidAt = objdatatableLoadPaidAt.DefaultView.ToTable();

                    ddlPaidAt.DataTextField = "short_name";
                    ddlPaidAt.DataValueField = "loc_id";
                    ddlPaidAt.DataSource = objdatatableLoadPaidAt;
                    ddlPaidAt.DataBind();
                    ddlPaidAt.Items.Insert(0, new ListItem("", "")); 
                    if (ddlPaidAt.Items.FindByValue("MRO") != null)
                    { ddlPaidAt.ClearSelection(); ddlPaidAt.Items.FindByValue("MRO").Selected = true; }
                    ddlPaidAt.SelectedIndex = 1;
                }
                else
                {
                    ddlPaidAt.DataSource = null;
                    ddlPaidAt.DataBind();
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
                objdatatableLoadPaidAt = null;
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
            }
        }

        private void LoadBanks()//fixed -ok 
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0014R1V1";
            string stringOrderBy = "lst.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableLoadBanks = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                ddlBank.Items.Clear();

                stringcondition = "And lst.be_id= '" + stringbeid + "'  AND  lst.LSTGRP_ID like '%BANKS%' AND lst.delmark='N' "; 
                stringServiceType = "List1R1V1";
                if (Session["SSNLOADBANKS"] != null)
                {
                    objdatatableLoadBanks = (DataTable)Session["SSNLOADBANKS"];
                }
                if ((objdatatableLoadBanks == null) || (objdatatableLoadBanks != null && objdatatableLoadBanks.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadBanks = objDatasetResult.Tables["t1"];
                            Session["SSNLOADBANKS"] = objdatatableLoadBanks;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                } 
                if (objdatatableLoadBanks != null && objdatatableLoadBanks.Rows.Count > 0)
                {
                    ddlBank.DataTextField = "short_name";
                    ddlBank.DataValueField = "lst_id";
                    ddlBank.DataSource = objdatatableLoadBanks;
                    ddlBank.DataBind();
                    ddlBank.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlBank.DataSource = null;
                    ddlBank.DataBind();
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
                objdatatableLoadBanks = null;
                stringbeid = null;
                stringcondition = null;
                stringServiceType = null;
            }


        }
        private DataRow GetRequestDetails(string stringReqID)//f
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
            try
            {
                if (stringReqID != null && stringReqID.Trim().Length > 0)
                {
                    stringServiceType1 = "List5R1V1";
                    stringexp012 = "And mrreg.be_id= '" + stringbeid + "' And mrreg.request_id= '" + stringReqID.ToString() + "'";

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
                            return objDataRow;
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
                stringbeid = null;
                stringServiceType1 = null;
                stringexp012 = null;
            }
            return null;
        }

        private void ResetVariables()//
        {
            try
            {
                Session["stringDMLIndicator"] = "I";
                Session["stringSortDirection"] = "ASC";
                Session["stringSortExpression"] = "";
                Session["stringFormID"] = "FC3004R1V2";
                Session["stringFormName"] = "MR PAYMENT GENERATION";
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
           
        }

        private void ShowMessage(string stringMessageContent)//
        {
            try
            {
                if (stringMessageContent.Trim().Length > 0)
                {
                    stringMessageContent = stringMessageContent.Replace("\r\n", "\\r\\n"); stringMessageContent = stringMessageContent.Replace("\n", "\\n");
                    stringMessageContent = stringMessageContent.Replace("'", "");
                    CommonFunctions.ShowMessageboot(this, stringMessageContent);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            
        }

        private void ClearValues()//
        {
            string stringRequestID = "";
            try
            {
                txtReceiptNo.Text = "";
                txtRequestor.Text = "";
                txtPayName.Text = "";
                txtAddress.Text = "";
                ddlCurrency.ClearSelection();
                if (ddlCurrency.Items.FindByValue("SGD") != null)
                { ddlCurrency.Items.FindByValue("SGD").Selected = true; }

                ddlPayType.ClearSelection();
                if (ddlPayType.Items.FindByValue("CASH") != null)
                { ddlPayType.Items.FindByValue("CASH").Selected = true; }

                ddlPaidAt.ClearSelection();
                if (ddlPaidAt.Items.FindByValue("MRO") != null)
                { ddlPaidAt.Items.FindByValue("MRO").Selected = true; }
                else { ddlPaidAt.SelectedIndex = 0; }

                txtExtReceiptNo.Text = "";
                ddlBank.ClearSelection();
                txtCardNo.Text = "";
                txtPayDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtPayAmt.Text = "0.00";
                txtOverpayAmt.Text = "0.00";
                txtDORefund.Text = "";
                Session["Reprint"] = "N";
                stringRequestID = "";
                if (Session["REQUESTID_PAYMENT"] != null)
                {
                    stringRequestID = Session["REQUESTID_PAYMENT"].ToString();
                }
                LoadReceiptDefaultValues(stringRequestID, out string stringdocwaiver, out string stringhoswaiver);

                Session["stringDMLIndicator"] = "I";
                txtRequestNo.Focus();
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringRequestID = null;
            }
        }

        private void LockFields()//
        {
            try
            {
                //imgBtnNew.Enabled = false;
                imgBtnSave.Enabled = false;
                // imgBtnDelete.Enabled = false;
                // imgBtnSearch.Enabled = false;
                //imgBtnClear.Enabled = false;
                imgBtnSecurity.Enabled = true;
                //imgBtnHelp.Enabled = true;

                txtReceiptNo.Enabled = false;
                txtRequestor.Enabled = false;
                txtPayName.Enabled = false;
                txtAddress.Enabled = false;
                ddlCurrency.Enabled = false;
                //ddlCurrency.CssClass = "form-control ReadOnly";
                ddlPayType.Enabled = false;
                //ddlPayType.CssClass = "form-control ReadOnly";
                ddlPaidAt.Enabled = false;
                txtExtReceiptNo.Enabled = false;
                ddlBank.Enabled = false;
                txtCardNo.Enabled = false;
                txtPayDate.Enabled = false;
                txtPayAmt.Enabled = false;
                btnGenReceipt.Enabled = false;
                txtDORefund.Enabled = false;
                txtOverpayAmt.Enabled = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
           
        }

        private void ControlsEnabledByProStatus(string stringStatus)//
        {
            switch (stringStatus)
            {
                case "CANCELLED":
                    {
                        //imgBtnNew.Enabled = false;
                        imgBtnSave.Enabled = false;
                       // imgBtnDelete.Enabled = false;
                        //imgBtnClear.Enabled = false;
                        //imgBtnSearch.Enabled = false;
                        // imgBtnSetting.Enabled = false;
                        imgBtnSecurity.Enabled = true;

                        txtReceiptNo.ReadOnly = true;
                        txtPayName.ReadOnly = true;
                        txtPayAmt.ReadOnly = true;
                        txtPayAmt.ReadOnly = true;
                        txtCardNo.ReadOnly = true;
                        txtExtReceiptNo.ReadOnly = true;
                        txtOverpayAmt.ReadOnly = true; 
                        txtDORefund.ReadOnly = true;
                        txtPayAmt.ReadOnly = true;
                        txtOverpayAmt.ReadOnly = true;
                        txtPayDate_CalendarExtender.Enabled = false;
                        txtPayDate.ReadOnly = true;

                        ddlBank.Enabled = false;
                        ddlCurrency.Enabled = false;
                        ddlPayType.Enabled = false;
                        ddlPaidAt.Enabled = false;
                        btnGenReceipt.Enabled = false;

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
                        imgBtnSave.Enabled = true;
                        //imgBtnDelete.Enabled = true;
                        //imgBtnClear.Enabled = true;
                        //imgBtnSearch.Enabled = true;
                        //imgBtnSetting.Enabled = true;
                        imgBtnSecurity.Enabled = true;
                        break;
                    }
            }
        }
        private bool ValidateControls0111()
        {
            bool boolStatus = true;
            try
            {
                boolStatus = true;

                if (txtReceiptNo.Text.Trim().Length > 0)
                {
                    CommonFunctions.ShowMessageboot(this, "No. of Days should be a valid integer");

                    boolStatus = false;
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                boolStatus = true;
            }
            return boolStatus;
        }


        private void GenerateReceipt(string stringRequestNo, string stringMRPaymentID, string stringReceiptID, bool boolReprinted)//Report need
        {
            byte[] objbytereturn = null;
            string stringFile = string.Empty;
            string stringoutputresult = "";
            int intErrorCount = 0;
            string[] stringOutputResult = null;
            string[] stringInputs = new string[5];
            string[] stringSettings = new string[21]; 
            string stringaddress = "";
            string stringTelephoneNo = ""; 
            string stringfromdate = "";
            string stringtodate = "";
            string stringbeid = CommonFunctions.GETBussinessEntity(); 
            stringaddress = ConfigurationManager.AppSettings["BEIDAddress"];
            stringTelephoneNo = ConfigurationManager.AppSettings["BEIDTelephoneNo"];
            string stringUserDisplayName = "";
            string stringreportname = "PAYMENT_RECEIPT";
            string stringformid = "DOP700001R1V1";
            DataSet objDatasetAppsVariables = null;
            string stringReprintSticker = "";
            string stringrec = "";
            string stringLofoFlag = "";
            try
            {
                if (stringRequestNo.Trim().Length > 0 && stringMRPaymentID.Trim().Length > 0)
                { 
                    if (Session["G11EOSUser_Name"] != null)
                    {
                        stringUserDisplayName = Session["G11EOSUser_Name"].ToString();
                    }
                    stringReprintSticker = "";
                    if (boolReprinted) { stringReprintSticker = "*** DUPLICATE ***"; }

                    stringInputs[0] = stringbeid;
                    stringrec = DBNull.Value.ToString();
                    stringInputs[1] = stringrec;
                    stringInputs[2] = stringRequestNo;
                    stringInputs[3] = stringMRPaymentID;
                    if (stringReprintSticker.Length > 0)
                    {
                        stringInputs[4] = stringReprintSticker;
                    }
                    else
                    {
                        stringInputs[4] = stringrec;
                    }

                    stringSettings[0] = stringbeid;
                    stringSettings[1] = stringaddress;
                    stringSettings[2] = "";
                    stringSettings[3] = "";
                    stringSettings[4] = "";
                    stringSettings[5] = "";
                    stringSettings[6] = ConfigurationManager.AppSettings["copyright"].ToString();
                    stringSettings[7] = stringformid;
                    stringSettings[8] = "PORTALBLEDOCFORMAT";
                    stringSettings[9] = stringreportname;
                    stringSettings[10] = "";
                    stringSettings[11] = "";
                    stringSettings[12] = stringTelephoneNo;
                    stringSettings[13] = "";
                    stringSettings[14] = stringreportname;

                    stringSettings[15] = "param_from_date" + "-->" + stringfromdate;
                    stringSettings[16] = "param_to_date" + "-->" + stringtodate;
                    stringSettings[17] = "print_by" + "-->" + stringUserDisplayName.Trim();
                    stringSettings[18] = "additional2" + "-->" + stringReprintSticker.Trim();
                    stringSettings[19] = "additional3" + "-->" + txtPayName.Text.Trim().ToUpper().Trim();
                    stringLofoFlag = LoadINSTLogo();
                    stringSettings[20] = "Print_flag" + "-->" + stringLofoFlag.ToString();
                    objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                    objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = stringformid;
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

                else
                {
                    ShowMessage("Select receipt and then print");
                }
            }

            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringFile = null; 
                intErrorCount = 0;
                stringOutputResult = null;
                stringaddress = null;
                stringTelephoneNo = null;
                stringfromdate = null;
                stringtodate = null;
                stringbeid = null;
                stringrec = null;
                stringLofoFlag = null;
            }
        } 

        private void ShowAuditTrail(DataRow objDataRow)
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
        private void VerifyAccessRights()
        {

            DataSet objDatasetResult = null; 
            string stringstatus = "";
            string[] stringOutputResult = null;
            string[] stringComponent = null;
            DataRow objDataRow = null; 
            imgBtnSave.Enabled = false;
            btnprint.Visible = false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FC0003R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null &&  objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        if (objDataRow["new"].ToString().ToUpper() == "ENABLED")
                        { 
                            imgBtnSave.Enabled = true;
                        }
                        if (objDataRow["edit"].ToString().ToUpper() == "ENABLED")
                        {
                            imgBtnSave.Enabled = true;
                            Session["boolModifyRights"] = true;
                        }
                        else { Session["boolModifyRights"] = false; } 
                        if (objDataRow["Export"].ToString().ToUpper() == "ENABLED")
                        {
                            btnprint.Visible = true;
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
                objDataRow = null;
            }
        }
        private bool ValidateControls()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0016R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable1 = null;
            bool boolStatus = true;

            string stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";
            string stringEnableBank = "";
            string stringEnableCheque = "";
            string stringAllowRounding = "";
            string stringAllowExtReceipt = "";
            string stringIsExtReceiptMadatory = "";
            string stringPaidLocID = ddlPaidAt.SelectedItem.Value;
            string stringPayTypID = ddlPayType.SelectedValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            DataTable objDataTable = null;
            DataRow[] objDataRows = null;
            DataRow objDataRow = null;
            string stringServiceType = "";
            string stringExpression = "";
            try
            {
                if (Session["SSNLOADPAYMENTMODES"] != null)
                {
                    objDataTable = (DataTable)Session["SSNLOADPAYMENTMODES"];
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        if (stringPayTypID != null && stringPayTypID.Trim().Length > 0)
                        {
                           objDataRows = objDataTable.Select("be_id= '" + stringbeid + "' and paytyp_id='" + stringPayTypID + "' and delmark='N'");
                            if (objDataRows != null && objDataRows.Length > 0)
                            {
                                objDataRow = objDataRows[0];
                                stringEnableBank = objDataRow["enable_bank"].ToString();
                                stringEnableCheque = objDataRow["enable_chequeno"].ToString();
                                stringAllowRounding = objDataRow["allow_rounding"].ToString();
                                stringIsExtReceiptMadatory = objDataRow["reference_1"].ToString();
                            }
                        }
                    }
                }

                if (stringPaidLocID != null && stringPaidLocID.Trim().Length > 0)
                {
                    stringServiceType = "List1R1V1";
                    stringExpression = " And palon.be_id= '" + stringbeid + "' and palon.loc_id='" + stringPaidLocID + "' And palon.delmark= 'N'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objDataTable1 = objDatasetResult.Tables["t1"];
                        }
                        if (objDataTable1 != null && objDataTable1.Rows.Count > 0)
                        {
                            objDataRow = objDataTable1.Rows[0];
                            stringAllowExtReceipt = objDataRow["allow_external_receipt"].ToString();
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                     
                }


                if (stringAllowExtReceipt != null && stringAllowExtReceipt.Trim().ToUpper() == "Y")
                {
                    if (txtExtReceiptNo.Text.Trim().Length == 0)
                    {
                        stringOverallMsg += "- External Receipt No." + "\\r\\n";
                        boolStatus = false;
                    }
                }

                if (stringEnableBank != null && stringEnableBank.Trim().ToUpper() == "Y" && ddlBank.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Bank Name" + "\\r\\n";
                    boolStatus = false;
                }

                if (stringEnableCheque != null && stringEnableCheque.Trim().ToUpper() == "Y" && txtCardNo.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Cheque / Card No." + "\\r\\n";
                    boolStatus = false;
                }
                if (stringEnableCheque != null && stringEnableCheque.Trim().ToUpper() == "Y" && (txtCardNo.Text.Trim().Length > 0 && txtCardNo.Text.Trim().Length < 4))
                {
                    stringOverallMsg += "- Cheque / Card No Is Invalid. " + "\\r\\n";
                    boolStatus = false;
                }

                if (stringIsExtReceiptMadatory != null && stringIsExtReceiptMadatory.Trim().ToUpper() == "Y" && txtExtReceiptNo.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- External Receipt No." + "\\r\\n";
                    boolStatus = false;
                }

                if (!boolStatus)
                {
                    if (stringOverallMsg.Trim().Length > 0)
                    {
                        stringOverallMsg = stringOverallMsg.Trim() + " ";
                        stringOverallMsg = stringOverallMsg.Remove(stringOverallMsg.Length - 1, 1);
                        ShowMessage(stringOverallMsg);
                        return boolStatus;
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
                objDataTable1 = null;
                boolStatus = true;

                stringOverallMsg = null;
                stringEnableBank = null;
                stringEnableCheque = null;
                stringAllowRounding = null;
                stringAllowExtReceipt = null;
                stringIsExtReceiptMadatory = null;
                stringPaidLocID = null;
                stringPayTypID = null;
                stringbeid = null;
                objDataTable = null;
                objDataRows = null;
                objDataRow = null;
                stringServiceType = null;
                stringExpression = null;
            }

            return true;
        }
        private bool ValidateControls01()
        {
            bool boolStatus = true;
            bool boolStatus1 = true;
            string stringOverallMsg = "";
            string stringOverallMsg1 = "";
            try
            {
                boolStatus = true;
                boolStatus1 = true;
                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";
                stringOverallMsg1 = " Amount must be greater than 0.";

                if (txtPayAmt.Text.Trim().Length == 0)
                {
                    stringOverallMsg1 += "- Amount S$" + "\\r\\n";
                    boolStatus1 = false;
                }
                if (ddlCurrency.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Currency" + "\\r\\n";
                    boolStatus = false;
                }

                if (ddlPayType.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Payment Type" + "\\r\\n";
                    boolStatus = false;
                }

                if (txtPayName.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Payee Name" + "\\r\\n";
                    boolStatus = false;
                }

                if (!boolStatus1)
                {
                    if (stringOverallMsg1.Trim().Length > 0)
                    {
                        stringOverallMsg1 = stringOverallMsg1.Trim();
                        CommonFunctions.ShowMessageboot(this, stringOverallMsg1);
                        return false;
                    }
                }



                if (!boolStatus)
                {
                    if (stringOverallMsg.Trim().Length > 0)
                    {
                        stringOverallMsg = stringOverallMsg.Trim();
                        CommonFunctions.ShowMessageboot(this, stringOverallMsg);
                        return false;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                boolStatus = true;
                boolStatus1 = true;
                stringOverallMsg = null;
                stringOverallMsg1 = null;
            }
           
            return true;
        }

        private bool ValidateBusinessLogic()//fix
        {
            string stringEnableBank = "";
            string stringEnableCheque = "";
            string stringAllowRounding = "";
            string stringIsExtReceiptMadatory = "";
            string stringPayTypID = ddlPayType.SelectedValue;

            decimal decimalReceivedAmount = 0;
            decimal decimalAmtPayable = 0;
            decimal decimalBalanceAmount = 0;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            DataTable objDataTable = null;
            DataRow[] objDataRows = null;
            DataRow objDataRow = null;
            decimal decimalBalance = 0;
            decimal decimalPayAmount = 0;
            try
            {
                if (Session["SSNLOADPAYMENTMODES"] != null)
                {
                    objDataTable = (DataTable)Session["SSNLOADPAYMENTMODES"];
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        if (stringPayTypID != null && stringPayTypID.Trim().Length > 0)
                        {
                            objDataRows = objDataTable.Select("be_id= '" + stringbeid + "' and paytyp_id='" + stringPayTypID + "' and delmark='N'");
                            if (objDataRows != null && objDataRows.Length > 0)
                            {
                                objDataRow = objDataRows[0];
                                stringEnableBank = objDataRow["enable_bank"].ToString();
                                stringEnableCheque = objDataRow["enable_chequeno"].ToString();
                                stringAllowRounding = objDataRow["allow_rounding"].ToString();
                                stringIsExtReceiptMadatory = objDataRow["reference_1"].ToString();
                            }
                        }
                    }
                }

                if (txtRndAmtPayable.Text.Trim().Length > 0)
                {
                    if (!decimal.TryParse(txtRndAmtPayable.Text.Trim(), out decimalAmtPayable))
                    {
                        ShowMessage("Rounding balance amount should be a valid decimal.");
                        txtRndAmtPayable.Focus();
                        return false;
                    }
                }

                if (txtBalAmt.Text.Trim().Length > 0)
                {
                    if (!decimal.TryParse(txtBalAmt.Text.Trim(), out decimalBalanceAmount))
                    {
                        ShowMessage("Balance amount should be a valid decimal.");
                        txtBalAmt.Focus();
                        return false;
                    }
                }

                if (txtPayAmt.Text.Trim().Length > 0)
                {
                    if (!decimal.TryParse(txtPayAmt.Text.Trim(), out decimalReceivedAmount))
                    {
                        ShowMessage("Amount should be a valid decimal.");
                        txtPayAmt.Focus();
                        return false;
                    }
                }

                if (stringAllowRounding != null && stringAllowRounding.Trim().ToUpper() == "Y")
                {
                    if (decimalReceivedAmount > decimalAmtPayable)
                    {
                        ShowMessage("Amount cannot be greater than suggested rounding balance amount.");
                        txtPayAmt.Focus();
                        return false;
                    }
                }
                else
                {
                    if (decimalBalanceAmount > Convert.ToDecimal(txtBalAmt.Text))
                    {
                        ShowMessage("Amount cannot be greater than balance amount available.");
                        txtPayAmt.Focus();
                        return false;
                    }
                }

                decimalBalance = 0;
                decimalPayAmount = 0;
                if (txtBalAmt.Text.Trim().Length > 0 && txtPayAmt.Text.Trim().Length > 0)
                {
                    decimal.TryParse(txtBalAmt.Text.Trim(), out decimalBalance);
                    decimal.TryParse(txtPayAmt.Text.Trim(), out decimalPayAmount);

                    if (decimalPayAmount <= 0)
                    {
                        ShowMessage("Amount should be greater than 0.");
                        txtPayAmt.Focus();
                        return false;
                    }
                    else if (decimalPayAmount > decimalBalance)
                    {
                        ShowMessage("Amount should be less than the balance amount.");
                        txtPayAmt.Focus();
                        return false;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringEnableBank = null;
                stringEnableCheque = null;
                stringAllowRounding = null;
                stringIsExtReceiptMadatory = null;
                stringPayTypID = null;

                decimalReceivedAmount = 0;
                decimalAmtPayable = 0;
                decimalBalanceAmount = 0;
                stringbeid = null;
                objDataTable = null;
                objDataRows = null;
                objDataRow = null;
                decimalBalance = 0;
                decimalPayAmount = 0;
            }
            return true;
        }


        #endregion

        protected void lnkbtnPayeeName_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string stringReciptid = "";
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
                            stringReciptid = stringValues[0]; 

                            if (stringReciptid.Length > 0 )
                            {
                                LoadReceiptData(stringReciptid);
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
                stringCmdArgument = null;
                stringReciptid = null;
                stringValues = null;
            }
        }
          
        protected void lnkbtnedit_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string stringReceiptID = "";
            string stringNewReqID = "";
            string stringpaymetstatus = "";
            string[] stringValues = null;
            try
            {
                Session["REQUESTID_PAYMENTEDIT"] = null;
                Session["RECEIPTNO_PAYMENTEDIT"] = null;
                if (sender != null)
                {
                    stringCmdArgument = ((LinkButton)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length > 0)
                        {
                            stringReceiptID = stringValues[0];

                            if (stringReceiptID != null && stringReceiptID.Trim().Length > 0)
                            {
                                stringNewReqID = txtRequestNo.Text.Trim();

                                Session["REQUESTID_PAYMENTEDIT"] = stringNewReqID;
                                Session["RECEIPTNO_PAYMENTEDIT"] = stringReceiptID;
                                stringpaymetstatus = Loadpayments();
                                if (stringpaymetstatus.ToString().ToUpper() != "CLOSED")
                                {
                                    if (Request.QueryString["LockFlag"] != null)
                                    {
                                        Response.Redirect("FC0003R1V2.aspx?LockFlag='TRUE'");
                                    }
                                    else
                                    {
                                        Response.Redirect("FC0003R1V2.aspx");

                                    }
                                }
                                else
                                {
                                    ShowMessage("Counter is closed, no editing for payment details anymore");
                                }
                             
                               
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
                stringCmdArgument = null;
                stringReceiptID = null;
                stringNewReqID = null;
                stringValues = null;
            }
        }

        private string Loadpayments()
        {
            DataSet objDataSet = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0003R1V1";
            string stringOrderBy = "";
            string stringrpttypeid = "";
            string stringexp = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableHOD = null;
            string stringServiceType = "";
            string stringreceiptid = "";
            try
            {
                if (Session["RECEIPTNO_PAYMENTEDIT"] != null)
                {
                    stringreceiptid = Session["RECEIPTNO_PAYMENTEDIT"].ToString();
                }
                stringexp = "And CASH.be_id='"+ CommonFunctions.GETBussinessEntity().ToString() +"'and MRPAY.RECEIPT_ID='"+ stringreceiptid+"'";
                stringServiceType = "List2R1V1";
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["t2"] != null && objDataSet.Tables["t2"].Rows.Count > 0)
                    {
                        objdatatableHOD = objDataSet.Tables["t2"];
                        stringrpttypeid = objdatatableHOD.Rows[0]["TRANS_STATUS"].ToString();
                        return stringrpttypeid;
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
            return stringrpttypeid;
        }

        private void PrintReceipt(string stringReceiptID)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V2";

            string[] stringInputs = new string[2];
            decimal decimalBalance = 0;
            decimal.TryParse(txtBalAmt.Text.Trim(), out decimalBalance);
            try
            {
                if (stringReceiptID != null)
                {
                    //allow user to print one time now temp hide 
                    if (GetPrintStatus(txtRequestNo.Text.Trim()) == "N")
                    {
                        stringInputs[0] = txtRequestNo.Text.Trim();
                        stringInputs[1] = "Y";
                        objDatasetResult = CommonFunctions.UpdateMRRegistrationR1V1("UpdateMRRegistrationR1V1", stringInputs, stringformid, out interrorcount, out stringOutputResult);

                        if (interrorcount == 0)
                        {
                            CheckFlagStatus(txtRequestNo.Text.Trim());
                        }
                        else
                        {
                            Errorpopup(stringOutputResult);
                        }

                        LoadReceiptData(stringReceiptID);
                        GenerateReceipt(txtRequestNo.Text.Trim(), hidFldMRPayID.Value, stringReceiptID, false);
                    }
                    else
                    {
                        ShowMessage("Original receipt printed already. Please print the duplicate");
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
                stringOutputResult = null;
                stringformid = null;
                decimalBalance = 0;
            }

        }
        private void PrintReceiptDupicate(string stringReceiptID)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V2";

            string[] stringInputs = new string[2];
            decimal decimalBalance = 0;
            decimal.TryParse(txtBalAmt.Text.Trim(), out decimalBalance);
            try
            {
                if (stringReceiptID != null)
                { 
                    if (GetPrintStatus(txtRequestNo.Text.Trim()) == "Y")
                    {
                        stringInputs[0] = txtRequestNo.Text.Trim();
                        stringInputs[1] = "N";
                        objDatasetResult = CommonFunctions.UpdateMRRegistrationR1V1("UpdateMRRegistrationR1V1", stringInputs, stringformid, out interrorcount, out stringOutputResult);

                        if (interrorcount == 0)
                        {
                            CheckFlagStatus(txtRequestNo.Text.Trim());
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
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDatasetResult = null;
                interrorcount = 0;
                stringOutputResult = null;
                stringformid = null;
                decimalBalance = 0;
            }

        }
        private void PrintDuplicateReceipt(string stringReceiptID)//fix
        {
            decimal decimalBalance = 0;

            try
            {
                decimalBalance = 0;
                decimal.TryParse(txtBalAmt.Text.Trim(), out decimalBalance);

                if (stringReceiptID != null)
                {
                    //allow user to print duplicate oncethey print original  time now temp hide 
                    if (GetPrintStatus(txtRequestNo.Text.Trim()) == "Y")
                    {
                        LoadReceiptData(stringReceiptID);
                        GenerateReceipt(txtRequestNo.Text.Trim(), hidFldMRPayID.Value, stringReceiptID, true);
                    }
                    else
                    {
                        ShowMessage("Before Print duplicate receipt.Please print the Original receipt.");
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                decimalBalance = 0;
            }
           
        }

        private string GetPrintStatus(string stringRequestID)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V2";
            string stringOrderBy = "";
            string stringResult = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringServiceType = "";
            string stringexp012 = "";
            try
            {
                stringServiceType = "List1R1V1";
                stringexp012 = "And mrreg.request_id= '" + stringRequestID + "' And mrreg.flag= 'Y' ";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t1"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        stringResult = objDataTable.Rows[0]["flag"].ToString();
                    }

                    if (stringResult.Length > 0)
                    {
                        if (stringResult.Trim().ToUpper() == "Y")
                        { return "Y"; }
                        else { return "N"; }
                    }
                    else
                    {
                        return "N";
                    }
                }
                else
                {

                    Errorpopup(stringOutputResult);
                    return "N";
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
            }
            return "N";
        }
        private void CheckFlagStatus(string stringRequestID)//fix -ok
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3004R1V2";
            string stringOrderBy = "";
            string stringResult = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp012 = "";
            try
            {
                stringServiceType = "List1R1V1";
                stringexp012 = "And mrreg.be_id= '" + stringbeid + "' And mrreg.request_id= '" + stringRequestID + "' and mrreg.flag= 'Y'";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t1"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        stringResult = objDataTable.Rows[0]["flag"].ToString();
                    }

                    if (stringResult.Length > 0)
                    {
                        if (stringResult.Trim().ToUpper() == "Y")
                        {
                            btnprint.Text = "Duplicate Receipt";
                        }
                        else
                        {
                            btnprint.Text = "Print Receipt";
                        }
                    }
                    else
                    {
                        btnprint.Text = "Duplicate Receipt";
                    }


                }
                else
                {
                    btnprint.Text = "Duplicate Receipt";
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
                stringResult = null;
                stringbeid = null;
                stringServiceType = null;
                stringexp012 = null;
            }
        }


        #region more button 

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
                stringBoID = null;
                boolstatus = true;
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

            DataTable objDataTableAddAttachments = new DataTable();
            DataSet objdataset = new DataSet();
            string[] stringOutResult = new string[3];
            Session["Documentattach"] = null;
            string stringServiceType = "";
            string stringExpression = "";
            try
            {
                if (Session["BusinessID"] != null)
                {
                    stringBoID = Session["BusinessID"].ToString();
                }
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
            bool boolsmr = false;
            string stringprocessname = "";
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

                        LoadBillingItemsGrid();
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
                boolsmr = false;
                stringprocessname = null;
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
                        LoadBillingItemsGrid();
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
            string stringOrderBy = "mrdelas.short_name asc";
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
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
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
                    LoadBillingItemsGrid();
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
                stringbeid = null;
                stringServiceType = null;
                stringexp = null;
                stringprocessname = null;
                stringRequestID = null;
            }
        }
        protected void btnDelayReasoncancel_Click(object sender, EventArgs e)
        {
            try
            {
                ModelpopupDelayReason.Hide();
                UpdatePanelDelayReason.Visible = false;
                LoadBillingItemsGrid();
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
            DataTable objdatatable = null; 
            int intbalanceamt = 0;
            string stringMRamt = "";
            string stringRequestID = "";
            string stringbalanceamt = "";

            bool boomdeptou = true;
            bool boolpendingverifier = true;
            string stringdeptou = ""; 
            if(ViewState["DEPTOU"] != null )
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
                LoadBillingItemsGrid();
                //}
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringTransSattus = null;
                objdatatablePendingItems = null;
                objdatarow = null;
                objdatatable = null; 
                intbalanceamt = 0;
                stringMRamt = null;
                stringRequestID = null;
                stringbalanceamt = null;
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
                        { objDatasetResult = objInterfaceServiceClient.SendEmailUserR1V1(txtRequestNo.Text.Trim(), txtsmremailRequestor.Text.Trim(), txtsmremailCC.Text.Trim(), txtsmremailBCC.Text.Trim(), txtsmremailSubject.Text.Trim(), txtsmremailCOntent.Text.Trim(),"N", objDatasetAppsVariables, out objbytearray, out interrorcount, out stringOutputResult);
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
                boolcheckvalidationvalidemail = true;
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
            DataRow objDataRow = null;
            int intErrorCount = 0;
            string stringcustemerid3 = "";
            string stringServiceType1 = "";
            string stringformid1 = "";
            string stringremarksmessge = "";
            try
            {
                if (Session["BusinessID"] != null)
                {
                    stringBoID = Session["BusinessID"].ToString();
                }
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
                {    objdatarowCompleteMedicalReport = objDataTableAddReports.Select("TRANS_ID = '" + txtRequestNo.Text.Trim().ToString() + "' and CATEGORY = 'COMPLETED MEDICAL REPORTS' ");
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
            try
            {
                pnlattachmentsmr.Visible = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            
        }

        protected void lnkbtnsmremailfilename_Click(object sender, EventArgs e)
        {
            DataTable objDataTableAddReports = null;
            DataTable objDataTableCompleteMedicalReport = null;
            DataRow objdatarowCompleteMedicalReportlatest = null;
            string stringBoID = ""; 
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
                if (Session["BusinessID"] != null)
                {
                    stringBoID = Session["BusinessID"].ToString();
                }
                if (Session["ADD_ATTACHMENTS"] != null)
                { objDataTableAddReports = (DataTable)Session["ADD_ATTACHMENTS"]; }


                if (objDataTableAddReports != null && objDataTableAddReports.Rows.Count > 0)
                {   objdatarowCompleteMedicalReport = objDataTableAddReports.Select("TRANS_ID = '" + txtRequestNo.Text.Trim().ToString() + "' and CATEGORY = 'COMPLETED MEDICAL REPORTS' ");
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
                stringOutputResult = null;
            } 
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
            DataRow[] objdatarow = null;
            DataTable objdatatable = null;
            int intbalanceamt = 0;
            string stringbalanceamt = null;
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
                LoadBillingItemsGrid();
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
                boolpendingItems = true;
                boolpaymentpendingItems = true;
                boolEnquirypendingItems = true;
                boolAssignDoctor = true;
                boolpendingreport = true;
                stringMRamt = null; 
                objdatarow = null;
                objdatatable = null;
                intbalanceamt = 0;
                stringbalanceamt = null;
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
                interrorcount = 0;
                stringOutputResult = null;
                stringformid = null;
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
                            btnPendingTracing.BackColor = Color.FromArgb(42,167,237);
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
                            btnPendingDespatch.BackColor = Color.FromArgb(42,167,237);
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
                            btnPendingAssigned.BackColor = Color.FromArgb(42,167,237);
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
                            btnPendingReport.BackColor = Color.FromArgb(42,167,237);
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
                            btnPendingReleasetoHIMS.BackColor = Color.FromArgb(42,167,237);
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
                            btnPendingSupVetting.BackColor = Color.FromArgb(42,167,237);
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
                            btnPendingforwarding.BackColor = Color.FromArgb(42,167,237);
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
                            btnPendingCollectInPerson.BackColor = Color.FromArgb(42,167,237);
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

                if(!boolbouser)
                {
                    pnlprocess.Visible = false;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
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
        private void LoadData(String stringRequestID)
        { 
            DataRow objDataRow = null;
            try
            {
                objDataRow = GetRequestDetails(stringRequestID);
                if (objDataRow != null)
                {
                    LoadProcesstabData(objDataRow);

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

        private void LoadProcesstabData(DataRow objDataRow)
        {
            DateTime objDueDate01 = new DateTime();
            string stringDelMode = "";
            string stringMRamt = "";
            string stringDeliverBy = "";
            string stringduedte = "";
            string stringProcessID = ""; 
            try
            {
                txtRequestNo.Text = objDataRow["request_id"].ToString();
                stringDelMode = objDataRow["delmod_id"].ToString();
                txtDelToID.Text = stringDelMode;
                txtReqEmail.Text = objDataRow["Email"].ToString();
                txtProcessType.Text = objDataRow["mrp_id"].ToString();
                txtBypassPenItems.Text = objDataRow["Bypass_Pen_Items"].ToString();
                txtMRStatus.Text = objDataRow["MR_STATUS"].ToString();
                stringMRamt = objDataRow["MR_AMOUNT"].ToString();
                hdfddlBlockBill.Value = objDataRow["Block_Billing"].ToString();
                hdfddlWApp.Value = objDataRow["WAIVER_STATUS_1"].ToString();
                hdfddlWApproved.Value = objDataRow["WAIVER_APPROVED"].ToString();

                hdfmramount.Value = stringMRamt;
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
                stringDelMode = null;
                stringMRamt = null;
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
        #endregion

        #endregion

        #endregion

        protected void gvReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringSort = string.Empty;
            DataRowView objDRV = null;
            DataRow objDataRow = null;
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null) { return; }

                objDRV = ((DataRowView)objGridViewRow.DataItem);
                objDataRow = objDRV.Row;

                LinkButton objbtnedit = e.Row.FindControl("lnkbtnedit") as LinkButton;
                if (txtMRStatus.Text.Trim().ToUpper() == "CANCELLED")
                {
                    objbtnedit.Enabled = false;
                }
                if (Session["ssnUserRole"] != null)
                {
                    DataTable objuserRole = (DataTable)Session["UserRolestable"];

                    if (objuserRole != null && objuserRole.Rows.Count > 0)
                    {
                        if (objuserRole.Select("Group_ID= 'FINANCE'").Length > 0)
                        {
                            int columnIndexToHide = 10;

                            if (columnIndexToHide >= 0 && columnIndexToHide < gvReceipt.Columns.Count)
                            {
                                ((TemplateField)gvReceipt.Columns[columnIndexToHide]).Visible = false;
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
                stringSort = null;
                objDRV = null;
                objDataRow = null;
            }
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnprint.Text.ToUpper() == "DUPLICATE RECEIPT")
                {
                    PrintDuplicateReceipt(txtReceiptNo.Text.Trim());
                }
                else if (btnprint.Text.ToUpper() == "PRINT RECEIPT")
                {
                    PrintReceipt(txtReceiptNo.Text.Trim());
                }
                CheckFlagStatus(txtRequestNo.Text);
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
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
            DataRow objDataRow = null;
            try
            {

                stringreport = txtRequestNo.Text.ToString();
                if (rbtnacknoeledge.Checked == true)
                {
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700011R1V1", "acknowledgement_letter");
                    }
                }
                else if (rbtnhospitalization.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700020R1V1", "hospitalisation_letter");
                    }
                }
                else if (rbtnrefundletter.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700010R1V1", "refund_letter");
                    }
                }
                else if (rbtnnorecord.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700022R1V1", "no_records_letter");
                    }
                }
                else if (rbtndefaultclinicreview.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700018R1V1", "defaulter");
                    }
                }
                else if (rbtndefaultclinicfinal.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700019R1V1", "final_defaulter");
                    }
                }
                else if (rbtncoverletter.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700008R1V1", "cover_letter");
                    }
                }
                else if (rbtnpartialrefundletter.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700009R1V1", "partial_refund_letter");
                    }
                }
                else if (rbtnworkcompensation.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700021R1V1", "WMC_letter");
                    }
                }
                else if (rbtnnotreportassenment.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700025R1V1", "assessment_report");
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
                                objDataRow = GetRequestDetails(stringreport);
                                if (objDataRow != null)
                                {
                                    ReportRelatedValues(objDataRow);
                                }
                            }
                            LoadReports("DOP700003R1V1", "FirstReminderLetter");
                        }
                        else
                        {
                            LoadReports("DOP700003R1V1", "FirstReminderLetter");
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
                        LoadReports("DOP700005R1V1", "cancellation_requests");
                    }
                }
                else if (rbtnEnvelopletter.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700016R1V1", "envelope_letter");
                    }
                }
                else if (rbtnwaiverform.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700026R1V1", "waiver_letter");
                    }
                }
                else if (rbtnmrassesment.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700006R1V1", "mr_assessment_defaulter");
                    }
                }
                else if (rbtnsimplemedicalreort.Checked == true)
                {  
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700013R1V1", "simple_medical_report");
                    }
                }
                else if (rbtnoutstanding.Checked == true)
                {  
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700007R1V1", "outstanding_status");
                    }
                }
                else if (rbtnconsent.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700002R1V1", "consent_form");
                    }
                }
                else if (rbtnmedicalreport.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700015R1V1", "mr_processing");
                    }
                }
                //else if (rbtnletterundertaking.Checked == true)
                //{
                //    string stringreport = txtReqNo.Text.ToString();
                //    if (stringreport.Trim().Length > 0 && stringreport != null)
                //    {
                //        LoadReports("DOP700027R1V1", "undertaking_letter");
                //    }
                //}
                else if (rbtnsplistreport.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700028R1V1", "Specialist_letter");
                    }
                }
                //else if (rbtnoverpayment.Checked == true)
                //{
                //    stringreport = txtReqNo.Text.ToString();
                //    if (stringreport.Trim().Length > 0 && stringreport != null)
                //    {
                //        LoadReports("DOP700029R1V1", "Overpayment_Letter");
                //    }
                //}
                else if (rbtnnodletter.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700030R1V1", "NOD_Letter");
                    }
                }
                else if (rbtnstandardnodletter.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700031R1V1", "Standard_NOD_Letter");
                    }
                }
                else if (rbtnpaymentAcknowlege.Checked == true)
                { 
                    if (stringreport.Trim().Length > 0 && stringreport != null)
                    {
                        LoadReports("DOP700032R1V1", "Payment_Acknowledgement_Letter");
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

        private void LoadReports(string stringFORMID, string stringreportname)//completed
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
                        //stringInputs[4] = "";
                        //stringInputs[5] = "";
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
            string stringexp = "";
            string stringServiceType = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {

                stringexp = "And INST.be_id= '" + stringbeid + "' And INST.INS_ID= '" + stringbeid + "'";
                stringServiceType = "List1R1V1";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
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
                stringexp = null;
                stringServiceType = null;
                stringbeid = null;
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
            DataRow objDataRow = null;
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
                            objDataRow = GetRequestDetails(stringreport);
                            if (objDataRow != null)
                            {
                                ReportRelatedValues(objDataRow);
                            }
                        }
                        LoadReports("DOP700004R1V1", "SecondReminderLetter");
                    }
                    else
                    {
                        LoadReports("DOP700004R1V1", "SecondReminderLetter");
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