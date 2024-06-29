using CLUSTER_MRTS.CommonFunction;
using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FA0034R1V1 : System.Web.UI.Page
    {
        public DataSet objDatasetAppsVariables;
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (CommonFunctions.IsActive())
            {
                string Stringorderid = "";
                try
                {
                    if (!IsPostBack)
                    {
                        LoadBusinessEntity();
                        LoadSOURCEOFREFERENCE();
                        CommonFunctions.HeaderName(this, "FA0034R1V1");
                        if (Request.QueryString["Load"] != null && Request.QueryString["Load"].Trim().Length > 0)
                        {
                            Session["FA0034R1V1_id"] = null;
                        }
                        VerifyAccessRights();
                        if (Session["FA0034R1V1_id"] != null && Session["FA0034R1V1_id"].ToString().Trim().Length > 0 && Session["FA0034R1V1_idGRID"] != null)
                        {
                            Stringorderid = Session["FA0034R1V1_id"].ToString();
                            Session["FA0034R1V1_id"] = Stringorderid;
                            LoadData(Stringorderid);
                        }
                        else
                        {
                            InitializeValues(); 
                            Clearvalues();
                            Session["FA0034R1V1_id"] = null;
                        }

                    }

                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }
                finally
                {
                    Stringorderid = null;
                }
            }
        }
        private bool SECemailValidations()
        {
            try
            {
                if (IsValidEmailAddress(txtemail.Text.Trim()))
                {
                    return true;
                }
                return false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return false;
            }
        }
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
        private void Clearvalues()
        {
            try
            {

                object[] objControls = new object[]
                {
                txtID,
                txtSName,
                txtround,
                txthoscodesap,
                txthoscodesmr,
                txtwebsite,
                txtemail,
                txtfaxno,
                txttelephno, 
                chkAssignRequest,
                chkdocgenwithlogo,
                chknodification,
                chcksendtosmr,
                txtRemarks,             
                chkDelMark,
                ddlBO,
                ddlroundingtype,
                txtregno,
                txtinsaddress,
                };
                CommonFunctions.ClearASPControlValues(objControls);

                if (ddlBO.SelectedItem != null)
                {
                    ddlBO.SelectedIndex = 1;
                }
                txtID.Enabled = true;
                ddlBO.Enabled = true;
                txtID.CssClass = ddlBO.CssClass = "form-control ReadOnly";
                ViewState["exportcondition"] = null;
                Session["FA0034R1V1_id"] = null;
                Session["AuditLogFA0034R1V1"] = null;
                Session["Documentattach"] = null;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private void LoadSOURCEOFREFERENCE()//fix
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
            DataTable objDataTableRoundingtype = null;
            string stringLstRefGroupID = "";
            string stringServiceType = "";
            string stringcondition = "";
            try
            {
                ddlroundingtype.Items.Clear();

                stringLstRefGroupID = "INST_ROUNDING_TYPE";
                stringcondition = "And lst.be_id= '" + stringbeid + "'  AND  lst.LSTGRP_ID='" + stringLstRefGroupID + "'AND lst.delmark='N' ";

                stringServiceType = "List1R1V1";
                if (Session["SSNLOADROUNDINGTYPE"] != null)
                {
                    objDataTableRoundingtype = (DataTable)Session["SSNLOADROUNDINGTYPE"];
                }
                if ((objDataTableRoundingtype == null) || (objDataTableRoundingtype != null && objDataTableRoundingtype.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objDataTableRoundingtype = objDatasetResult.Tables["t1"];
                            Session["SSNLOADROUNDINGTYPE"] = objDataTableRoundingtype;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objDataTableRoundingtype != null && objDataTableRoundingtype.Rows.Count > 0)
                {
                    ddlroundingtype.DataTextField = "short_name";
                    ddlroundingtype.DataValueField = "lst_id";
                    ddlroundingtype.DataSource = objDataTableRoundingtype;
                    ddlroundingtype.DataBind();
                    ddlroundingtype.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlroundingtype.DataSource = null;
                    ddlroundingtype.DataBind();
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
                objDataTableRoundingtype = null;
                stringLstRefGroupID = null;
                stringServiceType = null;
                stringcondition = null;
            }
        }
        private void LoadBusinessEntity()
        {
            int interrorcount = 0;
            string[] stringOutpuResult = null;
            try
            {
                object[] objControls = new object[]
                   {
                    ddlBO,
                   };
                interrorcount = CommonFunctions.LoadBusinessEntity(objControls, out stringOutpuResult);
                if (interrorcount == 1)
                {
                    Errorpopup(stringOutpuResult);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                interrorcount = 0;
                stringOutpuResult = null;
            }
        }
        private void VerifyAccessRights()
        {

            DataSet objDatasetResult = null;

            string stringstatus = "";
            string[] stringOutputResult = null;
            string[] stringComponent = null;
            DataRow objDataRow = null;

            imgbtnNew.Enabled = false;
            imgbtnSave.Enabled = false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FA0034R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] !=null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow !=null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        if (objDataRow["new"].ToString().ToUpper() == "ENABLED")
                        {
                            imgbtnNew.Enabled = true;
                            imgbtnSave.Enabled = true;
                        }
                        if (objDataRow["edit"].ToString().ToUpper() == "ENABLED")
                        {
                            imgbtnNew.Enabled = true;
                            imgbtnSave.Enabled = true;
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
                stringstatus = null;
                stringOutputResult = null;
                stringComponent = null;
                objDataRow = null;
            }
        }

        private void InitializeValues()
        {
            try
            {
                ViewState["vsSortDirection"] = " ASC";
                ViewState["vsSortExpression"] = "";
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
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
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Clearvalues();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }
        private void SaveData(string stringTYPE)
        {
            DataRow objdatarow = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            DataSet objDatasetResult1 = null;
            string[] stringOutputResult = null;
            string stringformid = "FA0034R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringErrorMsg = "";
            bool boolrecexixts = true;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            string stringexp0123 = "";
            string stringServiceType1 = "";
            try
            {
                if (CommonFunctions.ValidateASPControls(GetThisSCreenControls("TAB1"), out stringErrorMsg))
                {
                    stringServiceType = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t1"] !=null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                        {
                            if (Session["FA0034R1V1_id"] == null)
                            {
                                stringexp0123 = " And INST.INS_ID= '" + txtID.Text.Trim().ToUpper() + "'";
                                stringServiceType1 = "List1R1V1";

                                objDatasetResult1 = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp0123, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                                if (interrorcount == 0)
                                {
                                    if (objDatasetResult1 != null && objDatasetResult1.Tables["t1"] !=null && objDatasetResult1.Tables["t1"].Rows.Count > 0)
                                    {
                                        CommonFunctions.ShowMessageboot(this, "ID already exist");
                                        boolrecexixts = false;
                                    }
                                }
                                else
                                {
                                    Errorpopup(stringOutputResult);
                                    boolrecexixts = false;
                                }
                            }
                            if (boolrecexixts)
                            {
                                objdatarow = objDatasetResult.Tables["t1"].NewRow();
                                objdatarow["BE_ID"] = Session["BusinessID"].ToString();//BOID

                                objdatarow["INS_ID"] = txtID.Text.Trim().ToUpper();

                                objdatarow["INS_NAME"] = txtSName.Text.Trim();//ShortName

                                objdatarow["INS_ADDRESS"] = txtinsaddress.Text.Trim();//GroupName
                                objdatarow["INS_TELEPHONE"] = txttelephno.Text.Trim();//Telephonenumber
                                objdatarow["INS_FACSIMILE"] = txtfaxno.Text.Trim();//Telephonenumber
                                objdatarow["INS_EMAIL"] = txtemail.Text.Trim();//Telephonenumber
                                objdatarow["INS_WEBSITE"] = txtwebsite.Text.Trim();//Telephonenumber
                                objdatarow["REG_NO"] = txtregno.Text.Trim();//Telephonenumber
                                if (ddlroundingtype.SelectedItem != null && ddlroundingtype.SelectedValue.ToString().Length > 0)
                                {
                                    objdatarow["ROUND_TYPE"] = ddlroundingtype.SelectedValue.ToString();
                                }
                                objdatarow["ROUND_TO_NEAR"] = txtround.Text.Trim();//GroupName
                                objdatarow["SMR_INS_CODE"] = txthoscodesmr.Text.Trim();//GroupName
                                objdatarow["SAP_INS_CODE"] = txthoscodesap.Text.Trim();//GroupName
                                objdatarow["SMTP"] = txtRemarks.Text.Trim();//GroupName

                                                       
                                objdatarow["INC_GST"] = chkDelMark.Checked ? "Y" : "N";
                                objdatarow["NTF_SEN_IN_BAT"] = chknodification.Checked ? "Y" : "N"; 
                                objdatarow["ASSIGN_BASE_ON_MRN"] = chkAssignRequest.Checked ? "Y" : "N";
                                objdatarow["DOC_GEN_LOGO_FLAG"] = chkdocgenwithlogo.Checked ? "N" : "Y";
                                objdatarow["SEND_TO_SMR"] = chcksendtosmr.Checked ? "Y" : "N";

                                CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                                objDatasetResult.Tables["t1"].Rows.Add(objdatarow);

                                if (Session["FA0034R1V1_id"] != null && stringTYPE == "DELETE")
                                {
                                    objDatasetResult.AcceptChanges();
                                    objDatasetResult.Tables["t1"].Rows[0].Delete();
                                }
                                else if (Session["FA0034R1V1_id"] != null && stringTYPE == "INSERT")
                                {
                                    objDatasetResult.AcceptChanges();
                                    objDatasetResult.Tables["t1"].Rows[0]["INC_GST"] = chkDelMark.Checked ? "Y" : "N";
                                }
                                objDatasetResult.Tables["t1"].Rows[0].RowState.ToString();

                                objDatasetResult = objDatasetResult.GetChanges();
                                stringServiceType = "OperationServiceDML";
                                objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                                if (intErrorCount == 0)
                                {
                                    if (Session["FA0034R1V1_id"] != null && stringTYPE == "DELETE")
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Record deleted successfully");
                                    }
                                    else if (Session["FA0034R1V1_id"] != null && stringTYPE == "INSERT")
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Record Updated Successfully");
                                    }
                                    else
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Record Saved Successfully");
                                    }

                                    Clearvalues();
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
        }
        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveData("INSERT");
            
        }

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            SaveData("DELETE");
        }

        protected void imgbtnAudit_Click(object sender, ImageClickEventArgs e)
        {
            DataRow[] objDataRow = null;
            try
            {
                if (Session["AuditLogFA0034R1V1"] != null)
                {
                    objDataRow = (DataRow[])Session["AuditLogFA0034R1V1"];
                    if (objDataRow != null)
                    {
                        CommonFunctions.AuditLog(this, objDataRow);
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDataRow = null;
            }
        }

        private void LoadData(string stringInstID)
        {
            DataRow[] objdatarow = null;
            DataSet objDataSet = null; 
            string stringBoID = "";
            string stringbedep = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            } 

            try
            {
                if (Session["FA0034R1V1_idGRID"] != null)
                {
                    objDataSet = (DataSet)Session["FA0034R1V1_idGRID"];
                    if (objDataSet != null && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                    {
                        ViewState["exportcondition"] = "And INS_ID = '" + stringInstID.ToString() + "'";
                        objdatarow = objDataSet.Tables[0].Select("INS_ID = '" + stringInstID.ToString() + "'");
                        if (objdatarow != null && objdatarow.Length > 0)
                        {
                            Session["AuditLogFA0034R1V1"] = objdatarow;
                            ddlBO.ClearSelection();
                            stringbedep = objdatarow[0]["BE_ID"].ToString();
                            if (ddlBO.Items.FindByValue(stringbedep) != null)
                                ddlBO.Items.FindByValue(stringbedep).Selected = true;
                            ddlroundingtype.ClearSelection();
                            stringbedep = objdatarow[0]["ROUND_TYPE"].ToString();
                            if (ddlroundingtype.Items.FindByValue(stringbedep) != null)
                                ddlroundingtype.Items.FindByValue(stringbedep).Selected = true;

                            txtID.Text = objdatarow[0]["INS_ID"].ToString();
                            txtSName.Text = objdatarow[0]["INS_NAME"].ToString();
                         
                            txtinsaddress.Text = objdatarow[0]["INS_ADDRESS"].ToString();
                            txttelephno.Text = objdatarow[0]["INS_TELEPHONE"].ToString();
                            txtfaxno.Text = objdatarow[0]["INS_FACSIMILE"].ToString();
                            txtemail.Text = objdatarow[0]["INS_EMAIL"].ToString();
                            txtwebsite.Text = objdatarow[0]["INS_WEBSITE"].ToString();
                            txtround.Text = objdatarow[0]["ROUND_TO_NEAR"].ToString();
                            txthoscodesmr.Text = objdatarow[0]["SMR_INS_CODE"].ToString();
                            txthoscodesap.Text = objdatarow[0]["SAP_INS_CODE"].ToString();
                            txtRemarks.Text = objdatarow[0]["SMTP"].ToString();
                            txtRemarks.Text = objdatarow[0]["SMTP"].ToString();
                            txtregno.Text = objdatarow[0]["REG_NO"].ToString();

                            if (objdatarow[0]["INC_GST"].ToString() == "Y")
                            {
                                chkDelMark.Checked = true;
                            }
                            if (objdatarow[0]["NTF_SEN_IN_BAT"].ToString() == "Y")
                            {
                                chknodification.Checked = true;
                            } 
                            if (objdatarow[0]["ASSIGN_BASE_ON_MRN"].ToString() == "Y")
                            {
                                chkAssignRequest.Checked = true;
                            }
                            if (objdatarow[0]["DOC_GEN_LOGO_FLAG"].ToString() == "N")
                            {
                                chkdocgenwithlogo.Checked = true;
                            }
                            if (objdatarow[0]["SEND_TO_SMR"].ToString() == "Y")
                            {
                                chcksendtosmr.Checked = true;
                            }
                                
                            ddlBO.Enabled = false;
                            txtID.Enabled = false;

                            txtID.CssClass = ddlBO.CssClass = "form-control ReadOnly";

                            //if (stringInstID != null && stringInstID.Trim().Length > 0)
                            //{
                            //    string stringformid1 = "FC0001R1V1";
                            //    string stringServiceType = "List18R1V1";

                            //    string stringExpression = " and dach.be_id='" + stringBoID + "' and dach.trans_id='" + Session["FA0034R1V1_id"].ToString() + "' and dach.form_id='FA0034R1V1' ";

                            //    objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid1, stringExpression, "", 0, int.MaxValue, out intTotalRecord, out interrorcount, out stringOutputResult);

                            //    if (objDataSet != null && objDataSet.Tables["t1"].Rows.Count > 0)
                            //    {
                            //        if (interrorcount == 0)
                            //        {
                            //            string stringdocname = objDataSet.Tables["t1"].Rows[0]["DOC_NAME"].ToString();
                            //            string stringdoctype = objDataSet.Tables["t1"].Rows[0]["DOC_TYPE"].ToString();
                            //            string stringformid = objDataSet.Tables["t1"].Rows[0]["FORM_ID"].ToString();
                            //            string stringtransid1 = objDataSet.Tables["t1"].Rows[0]["TRANS_ID"].ToString();
                            //            string stringdocnamedoctype = stringformid + @"\" + stringtransid1 + @"\" + stringdocname;

                            //            if (stringdocname.Length > 0 && stringdoctype.Length > 0)
                            //            {
                            //                objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                            //                objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FA0034R1V1";

                            //                using (GSFileTransferService.FileTransferServiceClient objFileTransfer1 = new GSFileTransferService.FileTransferServiceClient())
                            //                {
                            //                    objFileTransfer1.DownloadFileFromServer(ref objDatasetAppsVariables, ref stringdocnamedoctype, out longlength, out stringFilepath, out stringOutputResult, out objbyteArray);
                            //                    if (objFileTransfer1 != null)
                            //                        objFileTransfer1.Close();
                            //                }

                            //                if (objbyteArray != null)
                            //                {
                            //                    string base64String = Convert.ToBase64String(objbyteArray, 0, objbyteArray.Length);
                            //                    imgInstPic.ImageUrl = "data:image/png;base64," + base64String;
                            //                }
                            //                else
                            //                {
                            //                    imgInstPic.Visible = false;

                            //                }
                            //            }
                            //            else
                            //            {
                            //                imgInstPic.Visible = false;

                            //            }
                            //        }
                            //        else
                            //        {
                            //            Errorpopup(stringOutputResult);
                            //        }
                            //    }
                            //    else
                            //    {
                            //        imgInstPic.Visible = false;
                            //    }

                            //}
                            //else { imgInstPic.Visible = false; }
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
                objDataSet = null;
                stringBoID = null;
                stringbedep = null;
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
                        txtID,
                        txtSName,
                        txthoscodesmr,
                        txthoscodesap,

                    };

                    return objControls;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

            return objControls;
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("FE0001R1V2?ID=FA0034R1V1");
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void txtemail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsValidEmailAddress(txtemail.Text.Trim()))
                {

                }
                else
                {
                    string stringErrorMsg = "Please Enter the Valid Email";
                    CommonFunctions.ShowMessageboot(this, stringErrorMsg);
                }
                txtemail.Focus();

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private bool ValidateControls()
        {
            bool boolStatus = true;
            string stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";


            if (txtID.Text.Trim().Length == 0)
            {
                stringOverallMsg += "- Institution ID" + "\\r\\n";
                boolStatus = false;
            }

            if (txtSName.Text.Trim().Length == 0)
            {
                stringOverallMsg += "- Institution Name" + "\\r\\n";
                boolStatus = false;
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

            return true;
        }

        //protected void btnphotoupload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ValidateControls())
        //        {
        //            UploadFile();
        //        }
        //    }
        //    catch (Exception objException)
        //    {
        //        CommonFunctions.ShowMessageboot02(objException);
        //    }

        //}

        //private void UploadFile()
        //{
        //    string stringformid = "FC0001R1V1";
        //    int interrorcount = 0;
        //    int intTotalRecord = 0;
        //    string stringOrderBy = "";
        //    int intFromRecord = 0;
        //    int intToRecord = int.MaxValue;
        //    int intFileSize, intMaxFileSize;
        //    string stringExtention = "";
        //    DataRow objdatarow = null;
        //    string[] stringExtentionAllowed;
        //    bool boolAllowedExtention = false;
        //    DataSet objDatasetResult = null;
        //    int intErrorCount = 0;
        //    string[] stringOutputResult = null;
        //    byte[] objbyteArray = null;
        //    string[] stringOutResult = new string[3];
        //    DataSet objDataSet = null;
        //    Session["Documentattach"] = null;
        //    string stringBoID = "";
        //    if (Session["BusinessID"] != null)
        //    {
        //        stringBoID = Session["BusinessID"].ToString();
        //    }
        //    try
        //    {
        //        if (FileUpload1.PostedFile != null && FileUpload1.HasFile)
        //        {
        //            double doubleSizeInMB = (double)FileUpload1.PostedFile.ContentLength / (1024.0 * 1024.0);
        //            if (FileUpload1.PostedFile.FileName.Trim().Length > 0)
        //            {
        //                stringExtention = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
        //                stringExtentionAllowed = ConfigurationManager.AppSettings["InstImgExtentionAllowed"].ToString().Split('|');
        //                foreach (string stringFilter in stringExtentionAllowed)
        //                {
        //                    if (stringExtention == stringFilter)
        //                    {
        //                        boolAllowedExtention = true;
        //                    }
        //                }

        //                if (boolAllowedExtention)
        //                {
        //                    intMaxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["InstImgSize"].ToString());
        //                    intFileSize = FileUpload1.FileBytes.Length / (1024 * 1024);
        //                    if (intFileSize < intMaxFileSize)
        //                    {
        //                        string stringtransid = Session["FA0034R1V1_id"].ToString();
        //                        string stringFileName = FileUpload1.FileName.Trim();
        //                        string stringPath = Server.MapPath(@"~/Images/InstitutionImages/") + stringFileName;
        //                        FileUpload1.PostedFile.SaveAs(stringPath);
        //                        string stringFilePath = Server.MapPath(@"~/Images/InstitutionImages/") + stringFileName;
        //                        objbyteArray = File.ReadAllBytes(stringFilePath);

        //                        objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
        //                        objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FC0001R1V1";

        //                        string stringServiceType = "List18R1V1";
        //                        string stringExpression = " and dach.be_id='" + stringBoID + "' and dach.trans_id='" + Session["FA0034R1V1_id"].ToString() + "' and dach.form_id='FA0034R1V1' ";

        //                        objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

        //                        if (interrorcount == 0)
        //                        {
        //                            if (objDataSet != null && objDataSet.Tables["t1"].Rows.Count > 0)
        //                            {
        //                                Session["Documentattach"] = "UPDATE";
        //                            }
        //                            objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
        //                            objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FB0002R1V1";
        //                            using (GSFileTransferService.FileTransferServiceClient objFileTransfer1 = new GSFileTransferService.FileTransferServiceClient())
        //                            {
        //                                objFileTransfer1.UploadFileR1V1(objbyteArray, stringFileName, stringtransid, objDatasetAppsVariables, out intErrorCount, out stringOutResult);
        //                                if (objFileTransfer1 != null)
        //                                    objFileTransfer1.Close();
        //                            }

        //                            if (intErrorCount == 0)
        //                            {
        //                                string stringServiceType01 = "DEFAULT";
        //                                string stringexp = "";
        //                                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType01, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutResult);

        //                                if (objDatasetResult != null && objDatasetResult.Tables["t1"].Rows.Count == 0)
        //                                {
        //                                    objdatarow = objDatasetResult.Tables["t1"].NewRow();
        //                                    objdatarow["BE_ID"] = ConfigurationManager.AppSettings["BusinessEntity"].ToString();
        //                                    objdatarow["ATTACH_ID"] = System.Guid.NewGuid().ToString().ToUpper();
        //                                    objdatarow["DOC_NAME"] = stringFileName;
        //                                    objdatarow["DOC_TYPE"] = stringExtention;
        //                                    objdatarow["DOC_SIZE"] = intFileSize;
        //                                    objdatarow["trans_id"] = Session["FA0034R1V1_id"].ToString();
        //                                    objdatarow["form_id"] = "FA0034R1V1";
        //                                    objdatarow["DELMARK"] = "N";
        //                                    CommonFunctions.AssignAuditLogDetails(ref objdatarow);
        //                                    objDatasetResult.Tables["t1"].Rows.Add(objdatarow);

        //                                    if (Session["Documentattach"] != null)
        //                                    {
        //                                        objDatasetResult.Tables["t1"].Rows[0]["DOC_NAME"] = stringFileName;
        //                                        objDatasetResult.Tables["t1"].Rows[0]["DOC_TYPE"] = stringExtention;
        //                                        objDatasetResult.Tables["t1"].Rows[0]["DOC_SIZE"] = intFileSize;
        //                                    }

        //                                }
        //                                if (objDatasetResult != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
        //                                {
        //                                    objDatasetResult = objDatasetResult.GetChanges();
        //                                    string stringServiceType1 = "OperationServiceDML";
        //                                    string stringformid1 = "FC0001R1V1";
        //                                    objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType1, objDatasetResult.GetChanges(), stringformid1, out intErrorCount, out string[] stringOutputResult1);
        //                                    if (intErrorCount == 0)
        //                                    {
        //                                        CommonFunctions.ShowMessageboot(this, "File Uploaded Successfully");
        //                                        Clearvalues();
        //                                    }
        //                                    else
        //                                    {
        //                                        Errorpopup(stringOutResult);
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Errorpopup(stringOutResult);
        //                            }

        //                        }
        //                        else
        //                        {
        //                            Errorpopup(stringOutResult);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        CommonFunctions.ShowMessageboot(this, "Upto " + intMaxFileSize + " MB is allowed to attach.");
        //                    }
        //                }
        //                else
        //                {
        //                    CommonFunctions.ShowMessageboot(this, "This file extention is not allowed.");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            string stringOverallMsg = "Please verify the following things are correct:- " + "\\r\\n";
        //            stringOverallMsg += "- File is not in open mode." + "\\r\\n";
        //            CommonFunctions.ShowMessageboot(this, stringOverallMsg);
        //        }
        //    }
        //    catch (Exception objException)
        //    {
        //        CommonFunctions.ShowMessageboot02(objException);
        //    }
        //}
    }
}