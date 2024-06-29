using CLUSTER_MRTS.CommonFunction;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        #region Declarations
         
        public DataSet objDatasetAppsVariables;

        #endregion

        #region Events

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)//fix
        {
            if (CommonFunctions.IsActive())
            {
                string stringRequestID = "";
                string stringReceiptNo = "";
                string stringExp = "";
                DataRow[] objdatarow = null;
                DataTable objuserRole = null;
                try
                {
                    if (!IsPostBack)
                    {
                        VerifyAccessRights();
                        ClearValues(1);
                        ResetVariables();
                        LoadPaymentModes();
                        LoadBanks();
                        if (Session["UserRolestable"] != null)
                        {
                            objuserRole = (DataTable)Session["UserRolestable"];

                            if (objuserRole != null && objuserRole.Rows.Count > 0)
                            {
                                if (Session["REQUESTID_PAYMENTEDIT"] != null)
                                {
                                    stringRequestID = Session["REQUESTID_PAYMENTEDIT"].ToString();
                                }
                                if (Session["RECEIPTNO_PAYMENTEDIT"] != null)
                                {
                                    stringReceiptNo = Session["RECEIPTNO_PAYMENTEDIT"].ToString();
                                } 
                                stringExp = "";
                                objdatarow = null;
                                if(objuserRole.Select(stringExp).Length > 0)
                                {
                                    objdatarow = objuserRole.Select(stringExp);
                                }
                                if (objdatarow != null && objdatarow.Length > 0)
                                {
                                    pnlSupervisorUpdate.Visible = true;
                                    if (stringRequestID.Trim().Length > 0 && stringReceiptNo.Length > 0)
                                    {
                                        LoadReceiptData(stringReceiptNo.Trim().ToUpper(), false);
                                    }
                                }
                                else
                                {

                                    pnlNormalUpdate.Visible = true;
                                    if (stringRequestID.Trim().Length > 0 && stringReceiptNo.Length > 0)
                                    {
                                        LoadReceiptData(stringReceiptNo.Trim().ToUpper(), true);
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
                    stringRequestID = null;
                    stringReceiptNo = null;
                    stringExp = null;
                    objdatarow = null;
                    objuserRole = null;
                }
            }
        }

        #endregion

        #region Image Button
        protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
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

        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {

        }

        #endregion

        #region Button

        protected void BtnGetReceipt_Click(object sender, EventArgs e)//fix
        {
            try
            {
                if (txtB1RequestNo.Text.Trim().Length == 0)
                {
                    CommonFunctions.ShowMessageboot(this, "MR No. should not be empty");
                    txtB1RequestNo.Focus();
                }
                else if (txtB1ReceiptNo.Text.Trim().Length == 0)
                {
                    CommonFunctions.ShowMessageboot(this, "Receipt No. should not be empty");
                    txtB1ReceiptNo.Focus();
                }
                else if (txtB1RequestNo.Text.Trim().Length > 0 && txtB1ReceiptNo.Text.Trim().Length > 0)
                {
                    LoadReceiptData(txtB1ReceiptNo.Text.Trim().ToUpper(), true);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
           
        }

        protected void BtnUpdateReceiptInfo_Click(object sender, EventArgs e)//fix
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
            DataRow objdatarow = null;
            string stringLogMessage = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringexp = "";
            try
            {
                if (ValidateControls1())
                {
                    stringServiceType = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (objDatasetResult != null && objDatasetResult.Tables["t4"] != null && objDatasetResult.Tables["t4"].Rows.Count == 0)
                    {
                        objdatarow = objDatasetResult.Tables["t4"].NewRow();
                        objdatarow["be_id"] = stringbeid;
                        objdatarow["Receipt_ID"] = txtB1ReceiptNo.Text.Trim().ToUpper();
                        objdatarow["Payee_Name"] = txtB1NewPayName.Text.Trim().ToUpper();

                        if (Session["G11EOSUser_Name"] != null)
                        { objdatarow["PymtCounter_ID"] = Session["G11EOSUser_Name"].ToString(); }
                        else { objdatarow["PymtCounter_ID"] = ""; }

                        if (ddlB1NewBank.SelectedItem!=null && ddlB1NewBank.SelectedValue.ToString().Length > 0)
                        {
                            objdatarow["Bank_Name"] = ddlB1NewBank.SelectedItem.Value;
                        }
                        objdatarow["Cheque_no"] = txtB1NewCardNo.Text.Trim().ToUpper();
                        objdatarow["Long_Name"] = "";
                        objdatarow["Remarks"] = "";

                        stringLogMessage = " Receipt No " + txtB1ReceiptNo.Text.Trim().ToUpper() + " updated to the following values." + Environment.NewLine;
                        stringLogMessage += " Old Payee Name: " + txtB1OldPayName.Text.Trim().ToUpper() + " to New Payee Name: " + txtB1NewPayName.Text.Trim().ToUpper() + Environment.NewLine;
                        stringLogMessage += " Old Bank Name:  " + ddlB1OldBank.Text.Trim().ToUpper() + " to New Bank Name: " + ddlB1NewBank.Text.Trim().ToUpper() + Environment.NewLine;
                        stringLogMessage += " Old Cheque/Card No: " + txtB1OldCardNo.Text.Trim().ToUpper() + " to New Cheque/Card No: " + txtB1NewCardNo.Text.Trim().ToUpper() + Environment.NewLine;
                         
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

                        ClearValues(1);
                        CommonFunctions.ShowMessageboot(this, "Receipt updated successfully");
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
                stringLogMessage = null;
                stringbeid = null;
                stringexp = null;
            }

        }

        protected void BtnSoftDelete_Click(object sender, EventArgs e)//fix //need to confirm 
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3008R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = ""; 
            string stringexp = ""; 
            DataRow objdatarow = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                if (txtB2RequestNo.Text.Trim().Length == 0)
                {
                    CommonFunctions.ShowMessageboot(this, "MR No. should not be empty");
                    txtB2RequestNo.Focus();
                }
                else if (txtB2ReceiptNo.Text.Trim().Length == 0)
                {
                    CommonFunctions.ShowMessageboot(this, "Receipt No. should not be empty");
                    txtB2ReceiptNo.Focus();
                }
                else if (txtB2RequestNo.Text.Trim().Length > 0 && txtB2ReceiptNo.Text.Trim().Length > 0)
                {
                    stringServiceType = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (objDatasetResult != null && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count == 0)
                    {
                        objdatarow = objDatasetResult.Tables["t2"].NewRow();
                        objdatarow["be_id"] = stringbeid;

                        objdatarow["Receipt_ID"] = txtB2ReceiptNo.Text.Trim().ToUpper();
                        objdatarow["request_ID"] = txtB2RequestNo.Text.Trim().ToUpper();

                        objdatarow["MR_status"] = txtB2MRStatus.Text.Trim().ToUpper();

                        objdatarow["delmark"] = "N";
                        CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                        objDatasetResult.Tables["t2"].Rows.Add(objdatarow);
                        objDatasetResult.Tables["t2"].Rows[0].RowState.ToString();

                    }
                    objDatasetResult = objDatasetResult.GetChanges();
                    stringServiceType = "OperationServiceDML";
                    objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);
                    if (intErrorCount == 0)
                    {
                        ClearValues(2);
                        CommonFunctions.ShowMessageboot(this, "Payment Receipt deleted successfully");
                        LkBtnBack_Click(null, null);
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
                stringexp = null;
                objdatarow = null;
                stringbeid = null;
            }
        }
         
        protected void BtnUpdatePaymentType_Click(object sender, EventArgs e)//fix
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
            DataRow objdatarow = null;
            string stringLogMessage = "";
            string stringexp = "";
            string[] stringInputs = new string[8];
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringDMLIND = "";
            string stringRequestID1 = "";
            string stringformid01 = "";
            string stringOldPayTypeID = "";
            string stringNewPayTypeID = "";
            string stringformid05 = "";
            try
            {
                if (ValidateControls2())
                {
                    if (ddlB2PayTypeOld.SelectedValue != null && ddlB2NewPayType.SelectedValue != null)
                    {
                        stringServiceType = "DEFAULT";
                        stringexp = "";
                        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                        if (objDatasetResult != null && objDatasetResult.Tables["t4"] != null && objDatasetResult.Tables["t4"].Rows.Count == 0)
                        {
                            objdatarow = objDatasetResult.Tables["t4"].NewRow();
                            objdatarow["be_id"] = stringbeid;
                            objdatarow["Receipt_ID"] = txtB2ReceiptNo.Text.Trim().ToUpper();
                            objdatarow["Payee_Name"] = txtB2NewPayName.Text.Trim().ToUpper();
                            if (Session["G11EOSUser_Name"] != null)
                            { objdatarow["PymtCounter_ID"] = Session["G11EOSUser_Name"].ToString(); }
                            else { objdatarow["PymtCounter_ID"] = ""; }

                            if (ddlB2NewBank.SelectedItem!=null&& ddlB2NewBank.SelectedValue.ToString().Length > 0)
                            {
                                objdatarow["Bank_Name"] = ddlB2NewBank.SelectedItem.Value;
                            }
                            objdatarow["Cheque_no"] = txtB2NewCardNo.Text.Trim().ToUpper();
                            objdatarow["Long_Name"] = "";
                            objdatarow["Remarks"] = "";
                            objdatarow["delmark"] = "N";

                            stringLogMessage = " Receipt No " + txtB2ReceiptNo.Text.Trim().ToUpper() + " updated to the following values." + Environment.NewLine;
                            stringLogMessage += " Old Payee Name: " + txtB2PayName.Text.Trim().ToUpper() + " to New Payee Name: " + txtB2NewPayName.Text.Trim().ToUpper() + Environment.NewLine;
                            stringLogMessage += " Old Bank Name:  " + ddlB2OldBank.Text.Trim().ToUpper() + " to New Bank Name: " + ddlB2NewBank.Text.Trim().ToUpper() + Environment.NewLine;
                            stringLogMessage += " Old Cheque/Card No: " + txtB2OldCardNo.Text.Trim().ToUpper() + " to New Cheque/Card No: " + txtB2NewCardNo.Text.Trim().ToUpper() + Environment.NewLine;
                             
                            CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                            stringDMLIND = "U";
                            objDatasetResult.Tables["t4"].Rows.Add(objdatarow);

                            if (stringDMLIND == "U")
                            {
                                objDatasetResult.Tables["t4"].AcceptChanges();
                                objDatasetResult.Tables["t4"].Rows[0]["delmark"] = "N";
                            }
                            objDatasetResult.Tables["t4"].Rows[0].RowState.ToString();

                        }
                        objDatasetResult = objDatasetResult.GetChanges();
                        stringServiceType = "OperationServiceDML";
                        objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);
                        if (intErrorCount == 0)
                        {
                            stringRequestID1 = "";
                            stringformid01 = "FC3004R1V2";
                            objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                            objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = stringformid01;
                            if (Session["REQUESTID_PAYMENTEDIT"] != null)
                            {
                                stringRequestID1 = (string)Session["REQUESTID_PAYMENTEDIT"];
                            }

                            stringInputs[0] = stringRequestID1.Trim();
                            stringInputs[1] = "N";
                            objDatasetResult = CommonFunctions.UpdateMRRegistrationR1V1("UpdateMRRegistrationR1V1", stringInputs, stringformid, out interrorcount, out stringOutputResult); 
                            if (interrorcount == 0)
                            {
                                if (ddlB2PayTypeOld.SelectedValue.ToString() != ddlB2NewPayType.SelectedValue.ToString())
                                {
                                    stringOldPayTypeID = "";
                                    stringNewPayTypeID = "";

                                    stringOldPayTypeID = ddlB2PayTypeOld.SelectedValue.ToString();
                                    stringNewPayTypeID = ddlB2NewPayType.SelectedValue.ToString();


                                    stringServiceType = "DEFAULT";
                                    stringformid05 = "FC3008R1V1";
                                    stringexp = "";
                                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid05, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                                    if (objDatasetResult != null && objDatasetResult.Tables["t3"].Rows.Count == 0)
                                    {
                                        objdatarow = objDatasetResult.Tables["t3"].NewRow();

                                        objdatarow["be_id"] = stringbeid;
                                        objdatarow["Receipt_ID"] = txtB2ReceiptNo.Text.Trim().ToUpper();
                                        objdatarow["MR_Pymt_ID"] = txtB2MRPymtID.Text.Trim().ToUpper();
                                        if (ddlB2NewPayType.SelectedItem != null && ddlB2NewPayType.SelectedValue.ToString().Length > 0)
                                        {
                                            objdatarow["PayTyp_ID"] = ddlB2NewPayType.SelectedItem.Value;
                                        }
                                        if (ddlB2NewBank.SelectedItem != null && ddlB2NewBank.SelectedValue.ToString().Length > 0)
                                        {
                                            objdatarow["Bank_Name"] = ddlB2NewBank.SelectedItem.Value;
                                        }
                                        objdatarow["Cheque_no"] = txtB2NewCardNo.Text.Trim().ToUpper();
                                        objdatarow["delmark"] = "N";

                                        stringLogMessage = " Receipt No " + txtB2ReceiptNo.Text.Trim().ToUpper() + " updated to the following values." + Environment.NewLine;
                                        stringLogMessage += " Old Payment Type: " + ddlB2PayTypeOld.Text.Trim().ToUpper() + " to New Payment Type: " + ddlB2NewPayType.Text.Trim().ToUpper() + Environment.NewLine;
                                        stringLogMessage += " Old Bank Name:  " + ddlB2OldBank.Text.Trim().ToUpper() + " to New Bank Name: " + ddlB2NewBank.Text.Trim().ToUpper() + Environment.NewLine;
                                        stringLogMessage += " Old Cheque/Card No: " + txtB2OldCardNo.Text.Trim().ToUpper() + " to New Cheque/Card No: " + txtB2NewCardNo.Text.Trim().ToUpper() + Environment.NewLine;

                                        objdatarow["LOG_REMARKS"] = stringLogMessage;


                                        CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                                        objDatasetResult.Tables["t3"].Rows.Add(objdatarow);
                                        objDatasetResult.Tables["t3"].Rows[0].RowState.ToString();

                                    }
                                    objDatasetResult = objDatasetResult.GetChanges();
                                    stringServiceType = "OperationServiceDML";
                                    objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid05, out int intErrorCount1, out stringOutputResult);
                                    if (intErrorCount1 == 0)
                                    {
                                        ClearValues(2);
                                        CommonFunctions.ShowMessageboot(this, "Receipt updated successfully");
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
                stringServiceType = null;
                objdatarow = null;
                stringLogMessage = null;
                stringexp = null;
                stringbeid = null;
                stringDMLIND = null;
                stringRequestID1 = null;
                stringformid01 = null;
                stringOldPayTypeID = null;
                stringNewPayTypeID = null;
                stringformid05 = null;
            }
        }

        #endregion

        #region Link Button

        protected void LnkbtnMRReceipt_Click(object sender, EventArgs e)//?????
        {
            string stringRequestID = "";
            string stringScript = "";
            try
            {
                if (Request.QueryString["RequestID"] != null)
                {
                    stringRequestID = Request.QueryString["RequestID"].ToString();
                    stringScript = "FC3004R1V2.aspx?RequestID=" + stringRequestID;
                    if (Request.QueryString["LockFlag"] != null) { stringScript += "&LockFlag=TRUE"; }
                    Response.Redirect(stringScript, true);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringRequestID = null;
                stringScript = null;
            }
           
        }

        #endregion

        #region Dropdown

        protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)//fix
        {
            string stringPayTypID = "";
            string stringEnableBank = "";
            string stringEnableCheque = "";
            DataTable objDataTable = null;
            DataRow[] objDataRows = null;
            DataRow objDataRow = null;
            try
            {
                if (ddlB1PayType.SelectedIndex > 0)
                {
                    ddlB2NewBank.ClearSelection();
                    txtB2NewCardNo.Text = "";

                    stringPayTypID = ddlB1PayType.SelectedValue;
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

                                    if (stringEnableBank != null && stringEnableBank.Trim().ToUpper() == "Y") { ddlB1NewBank.Enabled = true; }
                                    else { ddlB1NewBank.Enabled = false; }

                                    txtB1NewCardNo.Text = "";
                                    if (stringEnableCheque != null && stringEnableCheque.Trim().ToUpper() == "Y") { txtB1NewCardNo.Enabled = true; }
                                    else { txtB1NewCardNo.Enabled = false; }

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
                stringEnableBank = null;
                stringEnableCheque = null;
                objDataTable = null;
                objDataRows = null;
                objDataRow = null;
            }
        }

        #endregion

        #endregion

        #region Methods/Functions

        private void LoadReceiptData(string stringReceiptID, bool BoolFirstBlock)//fix
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
            string stringTemp = "";
            string stringRequestID = "";
            string stringServiceType = "";
            string stringexp012 = "";
            string stringSupSessionID = "";
            DataRow objDataRow = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                stringServiceType = "List2R1V1";
                stringexp012 = "And mrpr.be_id= '" + stringbeid + "' And mrpr.receipt_id= '" + stringReceiptID + "' ";

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
                        if (DateTime.TryParse(objDataRow["CREATED_ON"].ToString(), out DateTime bankDate) && bankDate.Date == DateTime.Now.Date)
                        {
                            btnB1SoftDelete.Visible = true;
                        }
                        else
                        {
                            btnB1SoftDelete.Visible = false;
                        }
                        stringSupSessionID = objDataRow["SESSION_ID_1"].ToString();//reference_10
                        if (stringSupSessionID != null && stringSupSessionID.Trim().Length > 0)
                        {
                            btnB1UpdateReceiptInfo.Enabled = false;
                            btnUpdateReceiptInfo2.Enabled = false;
                            btnB1SoftDelete.Enabled = false;

                            CommonFunctions.ShowMessageboot(this, "You cannot modify the receipt after supervisor closed the counter");
                            if (pnlSupervisorUpdate.Visible) { txtB2RequestNo.Focus(); }
                            else { txtB1RequestNo.Focus(); }
                        }

                        if (Session["REQUESTID_PAYMENTEDIT"] != null)
                        {
                            stringRequestID = Session["REQUESTID_PAYMENTEDIT"].ToString();
                        }


                        if (BoolFirstBlock)
                        {
                            txtB1RequestNo.Text = stringRequestID;
                            txtB1ReceiptNo.Text = stringReceiptID;
                            txtB1OldPayName.Text = objDataRow["payee_name"].ToString();
                            txtB1NewPayName.Text = objDataRow["payee_name"].ToString();

                            stringTemp = objDataRow["bank_name"].ToString();
                            ddlB1OldBank.ClearSelection();
                            if (ddlB1OldBank.Items.FindByValue(stringTemp) != null)
                            { ddlB1OldBank.Items.FindByValue(stringTemp).Selected = true; }
                            txtB1OldCardNo.Text = objDataRow["cheque_no"].ToString();

                            stringTemp = objDataRow["paytyp_id"].ToString();
                            ddlB1PayType.ClearSelection();
                            if (ddlB1PayType.Items.FindByValue(stringTemp) != null)
                                ddlB1PayType.Items.FindByValue(stringTemp).Selected = true;
                            else ddlB1PayType.SelectedIndex = 0;

                            txtB1MRStatus.Text = objDataRow["mr_status"].ToString();

                            ddlB1NewBank.ClearSelection();
                            txtB1NewCardNo.Text = "";
                            txtB1NewPayName.Focus();
                        }
                        else
                        {

                            txtB2RequestNo.Text = stringRequestID;
                            txtB2ReceiptNo.Text = stringReceiptID;
                            txtB2PayName.Text = objDataRow["payee_name"].ToString();
                            txtB2NewPayName.Text = objDataRow["payee_name"].ToString();

                            stringTemp = objDataRow["bank_name"].ToString();
                            ddlB2OldBank.ClearSelection();
                            if (ddlB2OldBank.Items.FindByValue(stringTemp) != null)
                            { ddlB2OldBank.Items.FindByValue(stringTemp).Selected = true; }
                            txtB2OldCardNo.Text = objDataRow["cheque_no"].ToString();

                            stringTemp = objDataRow["paytyp_id"].ToString();
                            ddlB2PayTypeOld.ClearSelection();
                            if (ddlB2PayTypeOld.Items.FindByValue(stringTemp) != null)
                                ddlB2PayTypeOld.Items.FindByValue(stringTemp).Selected = true;
                            else ddlB2PayTypeOld.SelectedIndex = 0;

                            ddlB2NewPayType.ClearSelection();
                            ddlB2NewBank.ClearSelection();
                            txtB2NewCardNo.Text = "";

                            txtB2MRPymtID.Text = objDataRow["mr_pymt_id"].ToString();
                            txtB2MRStatus.Text = objDataRow["mr_status"].ToString();
                            ddlB2NewPayType.Focus();
                        }
                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "Receipt not found. Please verify the MR no. and receipt ID");
                        if (pnlSupervisorUpdate.Visible) { txtB2RequestNo.Focus(); }
                        else { txtB1RequestNo.Focus(); }
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
                stringTemp = null;
                stringRequestID = null;
                stringServiceType = null;
                stringexp012 = null;
                stringSupSessionID = null;
                objDataRow = null;
            }
        }

        private void LoadPaymentModes()//fix
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
                ddlB1PayType.Items.Clear();
                ddlB2PayTypeOld.Items.Clear();
                ddlB2NewPayType.Items.Clear();

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
                    ddlB1PayType.DataTextField = "short_name";
                    ddlB1PayType.DataValueField = "paytyp_id";
                    ddlB1PayType.DataSource = objdatatableLoadPaymentModes;
                    ddlB1PayType.DataBind();
                    ddlB1PayType.Items.Insert(0, new ListItem("", ""));

                    ddlB2PayTypeOld.DataTextField = "short_name";
                    ddlB2PayTypeOld.DataValueField = "paytyp_id";
                    ddlB2PayTypeOld.DataSource = objdatatableLoadPaymentModes;
                    ddlB2PayTypeOld.DataBind();
                    ddlB2PayTypeOld.Items.Insert(0, new ListItem("", ""));

                    ddlB2NewPayType.DataTextField = "short_name";
                    ddlB2NewPayType.DataValueField = "paytyp_id";
                    ddlB2NewPayType.DataSource = objdatatableLoadPaymentModes;
                    ddlB2NewPayType.DataBind();
                    ddlB2NewPayType.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlB1PayType.DataSource = null;
                    ddlB1PayType.DataBind();

                    ddlB2PayTypeOld.DataSource = null;
                    ddlB2PayTypeOld.DataBind();

                    ddlB2NewPayType.DataSource = null;
                    ddlB2NewPayType.DataBind();
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

        private void LoadBanks()//fix
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
            string stringServiceType = "";
            string stringLstRefGroupID = "";
            string stringcondition = "";
            try
            {
                ddlB1NewBank.Items.Clear();
                ddlB2NewBank.Items.Clear();
                ddlB2OldBank.Items.Clear();
                ddlB1OldBank.Items.Clear();
                stringServiceType = "List1R1V1";
                stringLstRefGroupID = "BANKS";
                stringcondition = "be_id= '" + stringbeid + "' and LSTGRP_ID = '" + stringLstRefGroupID.ToString() + "' AND  delmark='N' ";

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
                            if (objdatatableLoadBanks != null)
                            {
                                Session["SSNLOADBANKS"] = objdatatableLoadBanks;
                            }
                            
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadBanks != null && objdatatableLoadBanks.Rows.Count > 0)
                {
                    ddlB1NewBank.DataTextField = "short_name";
                    ddlB1NewBank.DataValueField = "lst_id";
                    ddlB1NewBank.DataSource = objdatatableLoadBanks;
                    ddlB1NewBank.DataBind();
                    ddlB1NewBank.Items.Insert(0, new ListItem("", ""));

                    ddlB2NewBank.DataTextField = "short_name";
                    ddlB2NewBank.DataValueField = "lst_id";
                    ddlB2NewBank.DataSource = objdatatableLoadBanks;
                    ddlB2NewBank.DataBind();
                    ddlB2NewBank.Items.Insert(0, new ListItem("", ""));

                    ddlB2OldBank.DataTextField = "short_name";
                    ddlB2OldBank.DataValueField = "lst_id";
                    ddlB2OldBank.DataSource = objdatatableLoadBanks;
                    ddlB2OldBank.DataBind();
                    ddlB2OldBank.Items.Insert(0, new ListItem("", ""));

                    ddlB1OldBank.DataTextField = "short_name";
                    ddlB1OldBank.DataValueField = "lst_id";
                    ddlB1OldBank.DataSource = objdatatableLoadBanks;
                    ddlB1OldBank.DataBind();
                    ddlB1OldBank.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlB1NewBank.DataSource = null;
                    ddlB1NewBank.DataBind();

                    ddlB2NewBank.DataSource = null;
                    ddlB2NewBank.DataBind();

                    ddlB2OldBank.DataSource = null;
                    ddlB2OldBank.DataBind();

                    ddlB1OldBank.DataSource = null;
                    ddlB1OldBank.DataBind();
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
                stringServiceType = null;
                stringLstRefGroupID = null;
                stringcondition = null;
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
                CommonFunctions.HandleException(objException);
            }
        }


        private bool ValidateControls1()//fix
        {
            bool boolStatus = true;

            string stringOverallMsg = "";
            string stringEnableBank = "";
            string stringEnableCheque = "";
            string stringAllowRounding = "";
            string stringPayTypID = "";
            DataTable objDataTable = null;
            DataRow[] objDataRows = null;
            DataRow objDataRow = null;
            try
            {
                boolStatus = true;
                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";
                if (ddlB1PayType.SelectedItem != null)
                {
                    stringPayTypID = ddlB1PayType.SelectedValue;
                }
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
                                stringAllowRounding = objDataRow["allow_rounding"].ToString();
                            }
                        }
                    }
                }

                if (txtB1NewPayName.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- New Payee Name" + "\\r\\n";
                    boolStatus = false;
                }

                if (ddlB1PayType.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Payment Type" + "\\r\\n";
                    boolStatus = false;
                }

                if (stringEnableBank != null && stringEnableBank.Trim().ToUpper() == "Y" && ddlB1NewBank.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Bank Name" + "\\r\\n";
                    boolStatus = false;
                }

                if (stringEnableCheque != null && stringEnableCheque.Trim().ToUpper() == "Y" && txtB1NewCardNo.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Cheque / Card No." + "\\r\\n";
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

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                boolStatus = true;

                stringOverallMsg = null;
                stringEnableBank = null;
                stringEnableCheque = null;
                stringAllowRounding = null;
                stringPayTypID = null;
                objDataTable = null;
                objDataRows = null;
                objDataRow = null;
            }
            return true;
        }

        private bool ValidateControls2()//fix
        {
            bool boolStatus = true;

            string stringOverallMsg = "";
            string stringEnableBank = "";
            string stringEnableCheque = "";
            string stringAllowRounding = "";
            string stringPayTypID = "";
            DataTable objDataTable = null;
            DataRow[] objDataRows = null;
            DataRow objDataRow = null;
            try
            {
                boolStatus = true;
                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";

                if (ddlB2NewPayType.SelectedItem != null)
                {
                    stringPayTypID = ddlB2NewPayType.SelectedValue;
                }
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
                                stringAllowRounding = objDataRow["allow_rounding"].ToString();
                            }
                        }
                    }
                }

                if (txtB2NewPayName.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- New Payee Name" + "\\r\\n";
                    boolStatus = false;
                }

                if (ddlB2NewPayType.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- New Payment Type" + "\\r\\n";
                    boolStatus = false;
                }

                if (stringEnableBank != null && stringEnableBank.Trim().ToUpper() == "Y" && ddlB2NewBank.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Bank Name" + "\\r\\n";
                    boolStatus = false;
                }

                if (stringEnableCheque != null && stringEnableCheque.Trim().ToUpper() == "Y" && txtB2NewCardNo.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Cheque / Card No." + "\\r\\n";
                    boolStatus = false;
                }
                if (stringEnableCheque != null && stringEnableCheque.Trim().ToUpper() == "Y" && txtB2NewCardNo.Text.Trim().Length < 4)
                {
                    stringOverallMsg += "- Cheque / Card No Is Invalid. " + "\\r\\n";
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

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                boolStatus = true;

                stringOverallMsg = null;
                stringEnableBank = null;
                stringEnableCheque = null;
                stringAllowRounding = null;
                stringPayTypID = null;
                objDataTable = null;
                objDataRows = null;
                objDataRow = null;
            }
            return true;
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
                stringComponent[0] = "FC3008R1V1";
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
                CommonFunctions.HandleException(objException);
            }
        }

        private void ResetVariables()//fix
        {
            try
            {
                Session["stringDMLIndicator"] = "I";
                Session["stringSortDirection"] = "ASC";
                Session["stringSortExpression"] = "";
                Session["stringFormID"] = "FC3008R1V1";
                Session["stringFormName"] = "SUPERVISOR UTILITIES";
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
           
        }


        private void ClearValues(int intIndex)//fix
        {
            try
            {
                if (intIndex == 1)
                {
                    txtB1RequestNo.Text = "";
                    txtB1ReceiptNo.Text = "";
                    txtB1OldPayName.Text = "";
                    txtB1NewPayName.Text = "";
                    ddlB1PayType.ClearSelection();
                    ddlB1OldBank.ClearSelection();
                    ddlB1NewBank.ClearSelection();
                    txtB1OldCardNo.Text = "";
                    txtB1NewCardNo.Text = "";
                    txtB1MRStatus.Text = "";
                    txtB1RequestNo.Focus();
                }
                else if (intIndex == 2)
                {
                    txtB2RequestNo.Text = "";
                    txtB2ReceiptNo.Text = "";
                    txtB2PayName.Text = "";
                    txtB2NewPayName.Text = "";
                    txtB2MRPymtID.Text = "";
                    ddlB2PayTypeOld.ClearSelection();
                    ddlB2NewPayType.ClearSelection();
                    ddlB2NewBank.ClearSelection();
                    ddlB2OldBank.ClearSelection();
                    txtB2NewCardNo.Text = "";
                    txtB2OldCardNo.Text = "";
                    txtB2MRStatus.Text = "";
                    txtB2RequestNo.Focus();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
           
        }

        #endregion
        protected void LkBtnBack_Click(object sender, EventArgs e)
        {
            string stringRequestID = "";
            try
            {
                if (Session["REQUESTID_PAYMENTEDIT"] != null)
                {
                    stringRequestID = Session["REQUESTID_PAYMENTEDIT"].ToString();
                    Session["REQUESTID_PAYMENT"] = stringRequestID;
                    if (Request.QueryString["LockFlag"] != null)
                    {
                        Response.Redirect("FC0003R1V1.aspx?LockFlag='TRUE'");
                    }
                    else
                    {
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
                stringRequestID = null;
            }

        }
    }
}