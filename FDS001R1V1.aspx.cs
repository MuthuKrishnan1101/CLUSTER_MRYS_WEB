using CLUSTER_MRTS.CommonFunction;
using G10CertificateValidation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FDS001R1V1 : System.Web.UI.Page
    {
        public DataSet objDatasetAppsVariables;

        public int intintrecFromdocselection = 0;
        public int intrecFrom = 0;
        public int intrecFromProcessHistory = 0;

        public int intrecTo = 0;
        public int intrecTodocselection = 0;
        public int intrecToProcessHistory = 0;

        public int intpageIndexdocselection = 0;
        public int intpageIndex = 0;
        public int intpageIndexProcessHistory = 0;


        public string stringformIdPagingDocsel = "FC0004R1V1DOCSELECTIONgridviewpagesize";
        public string stringformIdProcessHistory = "RegistrationProcessHistoryPopupPaging";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (CommonFunctions.IsActive())
            {
                string stringRequestID = "";
                string stringTYPE = "";
                intrecTodocselection = CommonFunctions.GridViewPagesize(stringformIdPagingDocsel);
                intrecToProcessHistory = CommonFunctions.GridViewPagesize(stringformIdProcessHistory);
                try
                {
                    if (!IsPostBack)
                    {
                        VerifyAccessRights();
                        CommonFunctions.HeaderName(this, "FDS001R1V1");
                        if (Request.QueryString["Load"] != null && Request.QueryString["Load"].Trim().Length > 0)
                        {
                        }
                        hdfuniqid.Value = "";
                        Session["REQUESTDATA"] = null;
                        ViewState["DOCTORCHANGES"] = null;
                        txtsno.Text = "";
                        ClearHeaderValues();
                        ClearValues(1);
                        ClearValues(2);
                        ClearValues(3);
                        ClearValues(4);
                        LoadDepartmentOU();
                        LoadSOURCEOFREFERENCE();
                        LoadRequestCategory();
                        ddlApplicationStatus.Enabled = true;
                        btnConfirm.Enabled = true;

                        Session["stringDMLIndicator"] = "I";
                        if (Session["REQUESTID_FDS001R1V1"] != null)
                        {
                            stringRequestID = Session["REQUESTID_FDS001R1V1"].ToString();

                            LoadAllDATA(stringRequestID, out stringTYPE);
                            LoadData(stringRequestID, stringTYPE);
                            Loadattachment(stringRequestID, stringTYPE);
                            LoadProcessHistory(stringRequestID, stringTYPE);
                            LoadDocterandVerifiers(stringRequestID, "LOAD");//1
                            LoadRemarks(stringRequestID, stringTYPE, true);
                            if (Session["REQUESTID_COMPLETED"] != null && Session["REQUESTID_COMPLETED"].ToString() == "COMPLETED")
                            {
                                TABActive(3); 
                                btnConfirm_Click(null, null);
                            }

                        }
                        else
                        {
                            Session["REQUESTID_COMPLETED"] = null; 
                        }

                        updtpnldocterselection.Visible = false;

                        btnInfoRequest_Click(null, null);
                    }
                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }
                finally
                {
                    stringRequestID = "";
                    stringTYPE = "";
                }
            }
        }
        private void VerifyAccessRights()
        { 
            DataSet objDatasetResult = null; 
            string stringstatus = "";
            string[] stringOutputResult = null;
            string[] stringComponent = null;
            DataRow objDataRow = null; 
            imgbtnSaveAsDraft.Enabled = false; 
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FDS001R1V1";
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
                            imgbtnSaveAsDraft.Enabled = true; 
                        } 
                    }
                    else
                    {
                        Response.Redirect("PageAccessDenied.aspx", true);
                    }

                }

                //stringComponent = new string[1];
                //stringComponent[0] = "FDS001R1V2";
                //objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                //if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                //{
                //    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                //    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                //    {
                //        if (objDataRow["new"].ToString().ToUpper() == "ENABLED")
                //        {
                //            imgbtnNew.Enabled = true;
                //        }
                //        if (objDataRow["edit"].ToString().ToUpper() == "ENABLED")
                //        {
                //            imgbtnNew.Enabled = true;
                //            imgbtnSaveAsDraft.Enabled = true;
                //        } 
                //    }
                //    else
                //    {
                //        Response.Redirect("PageAccessDenied.aspx", true);
                //    }

                //}

                //stringComponent = new string[1];
                //stringComponent[0] = "FDS001R1V3";
                //objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                //if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                //{
                //    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                //    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                //    {
                //        if (objDataRow["new"].ToString().ToUpper() == "ENABLED")
                //        {
                //            imgbtnNew.Enabled = true;
                //        }
                //        if (objDataRow["edit"].ToString().ToUpper() == "ENABLED")
                //        {
                //            imgbtnNew.Enabled = true;
                //            imgbtnSaveAsDraft.Enabled = true;
                //        } 
                //    }
                //    else
                //    {
                //        Response.Redirect("PageAccessDenied.aspx", true);
                //    }

                //}

                //stringComponent = new string[1];
                //stringComponent[0] = "FDS001R1V4";
                //objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                //if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                //{
                //    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                //    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                //    {
                //        if (objDataRow["new"].ToString().ToUpper() == "ENABLED")
                //        {
                //            imgbtnNew.Enabled = true;
                //        }
                //        if (objDataRow["edit"].ToString().ToUpper() == "ENABLED")
                //        {
                //            imgbtnNew.Enabled = true;
                //            imgbtnSaveAsDraft.Enabled = true;
                //        } 
                //    }
                //    else
                //    {
                //        Response.Redirect("PageAccessDenied.aspx", true);
                //    }

                //}

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
        private void LoadAllDATA(string striongREQID, out string stringTYPE)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "";
            string stringOrderBy = "";
            int intFromRecord = 0;
            stringTYPE = "";
            string stringServiceType = "";
            int intToRecord = int.MaxValue;
            string stringcondition = "";
            string stringTransType = "";
            DataTable objdadatable = null;
            DataTable objdatatable = null;
            try
            {
                stringcondition = striongREQID.ToString();
                if (striongREQID.Contains("DRAFT"))
                {
                    stringformid = "FC0001R1V5";
                    stringServiceType = "List1R1V1";
                }
                else
                {
                    stringformid = "FC0001R1V2";
                    stringServiceType = "List1R1V1";
                }
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0)
                    {
                        stringTYPE = "LOAD";
                        //0.POP0132a0_get_mr_registration_search--- LoadData

                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[0] != null && objDatasetResult.Tables[0].Rows.Count > 0)
                        {
                            objdadatable = objDatasetResult.Tables[0];
                            if (!objdadatable.Columns.Contains("DML_INDICATOR"))
                            {
                                objdadatable.Columns.Add("DML_INDICATOR");
                            }
                            Session["LoadDataFC0001"] = objdadatable;
                        }
                        else
                        {
                            Session["LoadDataFC0001"] = null;
                        }
                        //1.POP0128a0_get_mr_registration_req_search---LoadNonMRDetail
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[1] != null && objDatasetResult.Tables[1].Rows.Count > 0)
                        {
                            stringTransType = "ADDITIONAL MR";
                            if (objDatasetResult.Tables[1].Select("trans_type= '" + stringTransType.ToString() + "'").Length > 0)
                            {
                                objdatatable = objDatasetResult.Tables[1].Select("trans_type= '" + stringTransType.ToString() + "'").CopyToDataTable();
                                if (!objdatatable.Columns.Contains("DML_INDICATOR"))
                                {
                                    objdatatable.Columns.Add("DML_INDICATOR");
                                }
                                Session["LoadNonMRDetailFC0001"] = objdatatable;
                            }
                        }
                        else
                        {
                            Session["LoadNonMRDetailFC0001"] = null;
                        }
                        //2.POP0130a0_get_mr_regist_remarks_search---LoadEnquiry
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[2] != null && objDatasetResult.Tables[2].Rows.Count > 0)
                        {
                            objdadatable = objDatasetResult.Tables[2];
                            if (!objdadatable.Columns.Contains("DML_INDICATOR"))
                            {
                                objdadatable.Columns.Add("DML_INDICATOR");
                            }
                            Session["ADD_REMARKS"] = objdadatable;
                        }
                        else
                        {
                            Session["ADD_REMARKS"] = null;
                        }
                        //3.POP0129a0_get_mr_pending_items_search            ---- LoadNonMRDetail
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[3] != null && objDatasetResult.Tables[3].Rows.Count > 0)
                        {
                            objdadatable = objDatasetResult.Tables[3];
                            if (!objdadatable.Columns.Contains("DML_INDICATOR"))
                            {
                                objdadatable.Columns.Add("DML_INDICATOR");
                            }
                            Session["LoadPendingItemsFC0001"] = objdadatable;
                        }
                        else
                        {
                            Session["LoadPendingItemsFC0001"] = null;
                        }
                        //4.POP0178a0_document_attachments_search            ---Loadattachment
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[4] != null && objDatasetResult.Tables[4].Rows.Count > 0)
                        {
                            objdadatable = objDatasetResult.Tables[4];
                            if (!objdadatable.Columns.Contains("DML_INDICATOR"))
                            {
                                objdadatable.Columns.Add("DML_INDICATOR");
                            }
                            Session["LoadattachmentFC0001"] = objdadatable;
                        }
                        else
                        {
                            Session["LoadattachmentFC0001"] = null;
                        }
                        //5.POP0220a0_GET_MR_ASSIGN_DOC_VERIFIER_SEARCH            -----LoadDocterandVerifiers
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[5] != null && objDatasetResult.Tables[5].Rows.Count > 0)
                        {
                            objdadatable = objDatasetResult.Tables[5];
                            if (!objdadatable.Columns.Contains("DML_INDICATOR"))
                            {
                                objdadatable.Columns.Add("DML_INDICATOR");
                            }
                            Session["LoadDocterandVerifiersFC0001"] = objdadatable;
                        }
                        else
                        {
                            Session["LoadDocterandVerifiersFC0001"] = null;
                        }
                        //6.POP0155a0_get_MR_Payment_Receipts_search_2          ---LoadPaymentReceiptsGrid
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[6] != null && objDatasetResult.Tables[6].Rows.Count > 0)
                        {
                            objdadatable = objDatasetResult.Tables[6];
                            if (!objdadatable.Columns.Contains("DML_INDICATOR"))
                            {
                                objdadatable.Columns.Add("DML_INDICATOR");
                            }
                            Session["LoadPaymentReceiptsGridFC0001"] = objdadatable;
                        }
                        else
                        {
                            Session["LoadPaymentReceiptsGridFC0001"] = null;
                        }
                        //7.POP0151a0_get_mr_enquiryhistory_search---LoadEnquiry
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[7] != null && objDatasetResult.Tables[7].Rows.Count > 0)
                        {
                            objdadatable = objDatasetResult.Tables[7];
                            if (!objdadatable.Columns.Contains("DML_INDICATOR"))
                            {
                                objdadatable.Columns.Add("DML_INDICATOR");
                            }
                            Session["LoadEnquiryFC0001"] = objdadatable;
                        }
                        else
                        {
                            Session["LoadEnquiryFC0001"] = null;
                        }
                        //8.POP0133a0_get_mr_process_details_search---LoadProcessHistory
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[8] != null && objDatasetResult.Tables[8].Rows.Count > 0)
                        {
                            objdadatable = objDatasetResult.Tables[8];
                            if (!objdadatable.Columns.Contains("DML_INDICATOR"))
                            {
                                objdadatable.Columns.Add("DML_INDICATOR");
                            }
                            Session["LoadProcessHistoryFC0001"] = objdadatable;
                        }
                        else
                        {
                            Session["LoadProcessHistoryFC0001"] = null;
                        }

                    }
                    else
                    {
                        stringTYPE = "NONLOAD";
                    }

                }
                else
                {
                    stringTYPE = "NONLOAD";
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
                stringServiceType = "";
                intToRecord = 0;
                stringcondition = "";
                stringTransType = "";
                objdadatable = null;
                objdatatable = null;
            }
        }
        private void ClearHeaderValues()
        {
            try
            {
                txtMRNumberHEADER.Text = "";
                txtRequestNo.Text = "";
                txtMRStatus.Text = "";
                txtWritingandVerifyingStatus.Text = "";
                chkpriorityflag.Checked = false;
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
                switch (intIndex)
                {
                    case 0:  // PATIENTS BLOCK
                        {

                            txtHRN.Text = "";
                            txtName.Text = "";
                            txtcasevisitno.Text = "";
                            txtAppDate.Text = "";
                            txtAccidentDate.Text = "";
                            txtrelatedMRref.Text = "";
                            txtReassDate.Text = "";
                            txtConInforDoctor.Text = "";
                            txtpurpose.Text = "";
                            txtreportformat.Text = "";
                            txtreporttypeID.Text = "";
                            txtreporttype.Text = "";

                            rbtEMR.Checked = false;
                            rbt1EMR.Checked = true;
                            rbtnboth.Checked = false;
                            ddlReference.ClearSelection();
                            ddlReference.Enabled = false;


                            break;

                        }
                    case 1:  // attachments
                        {
                            txtRemark.Text = "";
                            ddlCategory.ClearSelection();

                            Session["ADD_ATTACHMENTS"] = null;
                            gvAttachments.DataSource = null;
                            gvAttachments.DataBind();
                            break;
                        }
                    case 2: // REQUEST DETAIL BLOCK
                        {
                            ddlApplicationStatus.ClearSelection();

                            pnlRerouteRequest.Visible = false;
                            pnlRejectRequest.Visible = false;
                            pnlAssignDoctorandverifier.Visible = false;

                            txtcurrentloc.Text = "";
                            ddlDepartmentOU.ClearSelection();
                            txtRemarks.Text = "";

                            txtLocation.Text = "";
                            txtRejectReason.Text = "";

                            txtAssignDoctor.Text = "";
                            gvassigndoctor.DataSource = null;
                            gvassigndoctor.DataBind();

                            txtAssignVerifier.Text = "";
                            gvassignverifier.DataSource = null;
                            gvassignverifier.DataBind();

                            break;
                        }
                    case 3: // Process HIS
                        {
                            gvprocesshistorygrid.DataSource = null;
                            gvprocesshistorygrid.DataBind();
                            pnlProcessHistory.Visible = false;
                            btnProcessHistory.Visible = false;
                            btnProcessHistory.ToolTip = "Don't have any Process History";
                            lblpnlprocesshistory.InnerText = "0";

                            break;
                        }

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        #region Load DropDownValues



        private void LoadSOURCEOFREFERENCE()
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
            DataTable objdatatableLoadSOURCEOFREFERENCE = null;
            string stringLstRefGroupID = "";
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                ddlReference.Items.Clear();

                stringLstRefGroupID = "SOURCE_OF_REFERENCE";
                stringcondition = "And lst.be_id= '" + stringbeid + "'  AND  lst.LSTGRP_ID='" + stringLstRefGroupID + "'AND lst.delmark='N' ";

                stringServiceType = "List1R1V1";
                if (Session["SSNLOADSOURCEOFREFERENCE"] != null)
                {
                    objdatatableLoadSOURCEOFREFERENCE = (DataTable)Session["SSNLOADSOURCEOFREFERENCE"];
                }
                if ((objdatatableLoadSOURCEOFREFERENCE == null) || (objdatatableLoadSOURCEOFREFERENCE != null && objdatatableLoadSOURCEOFREFERENCE.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadSOURCEOFREFERENCE = objDatasetResult.Tables["t1"];
                            Session["SSNLOADSOURCEOFREFERENCE"] = objdatatableLoadSOURCEOFREFERENCE;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }

                if (objdatatableLoadSOURCEOFREFERENCE != null && objdatatableLoadSOURCEOFREFERENCE.Rows.Count > 0)
                {
                    ddlReference.DataTextField = "short_name";
                    ddlReference.DataValueField = "lst_id";
                    ddlReference.DataSource = objdatatableLoadSOURCEOFREFERENCE;
                    ddlReference.DataBind();
                    ddlReference.Items.Insert(0, new ListItem("", ""));
                    ddlReference.SelectedIndex = 1;
                }
                else
                {
                    ddlReference.DataSource = null;
                    ddlReference.DataBind();
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
                objdatatableLoadSOURCEOFREFERENCE = null;
                stringLstRefGroupID = "";
                stringcondition = "";
                stringServiceType = "";
            }
        }
        private void LoadDepartmentselect(bool boolReroutined)//fix
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
            DataTable objdatatableLoadApplicationStatus = null;
            string stringLstRefGroupID = "";
            string stringcondition = "";
            string stringServiceType = "";
            DataRow[] objdatarow = null;
            try
            {
                ddlApplicationStatus.Items.Clear();

                stringLstRefGroupID = "DEPARTMENT_SELECT_ACTION";
                stringcondition = "And lst.be_id= '" + stringbeid + "'  AND  lst.LSTGRP_ID='" + stringLstRefGroupID + "'AND lst.delmark='N' ";

                stringServiceType = "List1R1V1";
                if (Session["SSNLOADLOADAPPLICATIONSTATUS"] != null)
                {
                    objdatatableLoadApplicationStatus = (DataTable)Session["SSNLOADLOADAPPLICATIONSTATUS"];
                }
                if ((objdatatableLoadApplicationStatus == null) || (objdatatableLoadApplicationStatus != null && objdatatableLoadApplicationStatus.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadApplicationStatus = objDatasetResult.Tables["t1"];
                            Session["SSNLOADLOADAPPLICATIONSTATUS"] = objdatatableLoadApplicationStatus;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }

                if (objdatatableLoadApplicationStatus != null && objdatatableLoadApplicationStatus.Rows.Count > 0)
                {
                    if (!boolReroutined)
                    {
                        objdatarow = objdatatableLoadApplicationStatus.Select("lst_id='REROUTE_REQUEST'");
                        if (objdatarow != null && objdatarow.Length > 0)
                        {
                            objdatarow[0].Delete();
                            objdatatableLoadApplicationStatus.AcceptChanges();
                        }
                    }
                    ddlApplicationStatus.DataTextField = "short_name";
                    ddlApplicationStatus.DataValueField = "lst_id";
                    ddlApplicationStatus.DataSource = objdatatableLoadApplicationStatus;
                    ddlApplicationStatus.DataBind();
                    ddlApplicationStatus.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlApplicationStatus.DataSource = null;
                    ddlApplicationStatus.DataBind();
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
                objdatatableLoadApplicationStatus = null;
                stringLstRefGroupID = "";
                stringcondition = "";
                stringServiceType = "";
                objdatarow = null;
            }
        }
        private void LoadRequestCategory()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0035R1V1";
            string stringOrderBy = "depcat.ORDER_ID asc,depcat.short_name asc";
            int intFromRecord = 0;
            int intToRecord = 5000;
            DataTable objdatatableCategory = null;
            string stringCategory = "";
            string stringDeleteMark = "";
            string stringexp012 = "";
            string stringServiceType = "";
            DataRow[] datarow = null;
            try
            {
                ddlCategory.Items.Clear();
                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
                Session["stringCategory"] = null;
                stringDeleteMark = "N";
                stringexp012 = "And depcat.delmark= '" + stringDeleteMark.ToString() + "'";
                stringServiceType = "List1R1V1";
                if (Session["SSNLOADCATEGORY"] != null)
                {
                    objdatatableCategory = (DataTable)Session["SSNLOADCATEGORY"];
                }
                if ((objdatatableCategory == null) || (objdatatableCategory != null && objdatatableCategory.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableCategory = objDatasetResult.Tables["t1"];
                            Session["SSNLOADCATEGORY"] = objdatatableCategory;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableCategory != null && objdatatableCategory.Rows.Count > 0)
                {
                    datarow = objdatatableCategory.Select("ENABLE_DEP_USER= 'Y'");
                    if (datarow != null && datarow.Length > 0)
                    {
                        objdatatableCategory = objdatatableCategory.Select("ENABLE_DEP_USER= 'Y'").CopyToDataTable();
                    }
                    ddlCategory.DataTextField = "short_name";
                    ddlCategory.DataValueField = "id";
                    ddlCategory.DataSource = objdatatableCategory;
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, new ListItem("", ""));

                    for (int i = 0; i < objdatatableCategory.Rows.Count; i++)
                    {
                        if (i == objdatatableCategory.Rows.Count - 1)
                        {
                            stringCategory += "'" + objdatatableCategory.Rows[i]["id"].ToString() + "'";
                        }
                        else
                            stringCategory += "'" + objdatatableCategory.Rows[i]["id"].ToString() + "'" + ",";
                    }
                    Session["stringCategory"] = stringCategory;
                    ddlCategory.SelectedIndex = 1;
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
                objdatatableCategory = null;
                stringCategory = "";
                stringDeleteMark = "";
                stringexp012 = "";
                stringServiceType = "";
                datarow = null;
            }
        }
        #endregion
        #region Load DATA

        private void LoadData(string stringRequestID, string stringTYPE)//fix
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
            string stringHRNID = "";
            string stringReference = "";
            try
            {
                if (stringRequestID != null && stringRequestID.Trim().Length > 0)
                {
                    if (stringTYPE == "NONLOAD" || (Session["LoadDataFC0001"] == null && stringTYPE.Length > 0 && stringTYPE != "Load"))
                    {
                        stringServiceType1 = "List5R1V1";
                        stringexp012 = "And mrreg.be_id= '" + stringbeid + "' And mrreg.request_id= '" + stringRequestID.ToString() + "'";

                        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                        if (interrorcount == 0)
                        {
                            if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t5"] != null && objDatasetResult.Tables["t5"].Rows.Count > 0)
                            {
                                objDataTable = objDatasetResult.Tables["t5"];
                            }

                        }
                        else
                        {
                            Errorpopup(stringOutputResult);
                        }
                    }
                    else
                    {
                        if (HttpContext.Current.Session["LoadDataFC0001"] != null)
                        {
                            objDataTable = (DataTable)HttpContext.Current.Session["LoadDataFC0001"];
                        }
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        Session["REQUESTDATA"] = objDataTable;
                        TABActive(0);
                        pnlProcessHistory.Visible = false;

                        objDataRow = objDataTable.Rows[0];
                        Session["AuditLogFC0001R1V1"] = objDataRow;

                        //load header values

                        txtMRNumberHEADER.Text = objDataRow["MR_ID"].ToString();
                        txtRequestNo.Text = objDataRow["request_id"].ToString();
                        if (objDataRow["PRIORITY_FLAG"].ToString() == "Y")
                        {
                            chkpriorityflag.Checked = true;
                        }
                        else
                        {
                            chkpriorityflag.Checked = false;
                        }
                        stringStatus = objDataRow["mr_status"].ToString();

                        if (stringStatus == "PENDING FORWARDING")
                        {
                            //lnkbtnaddattachments.Visible = true;
                            //lnkbtnaddattachmentsclear.Visible = true;
                        }
                        else
                        {
                            //lnkbtnaddattachmentsclear.Visible = false;
                            //lnkbtnaddattachments.Visible = false;
                        }
                        txtMRStatus.Text = stringStatus;

                        if (stringStatus == "PENDING DESPATCH")
                        {
                            imgbtnSaveAsDraft.Enabled = false;
                            lnkbtnaddattachments.Enabled = false;
                            lnkbtnaddattachmentsclear.Enabled = false;
                            ddlApplicationStatus.Enabled = false;
                            btnConfirm.Enabled = false;
                        }
                        else
                        {
                            imgbtnSaveAsDraft.Enabled = true;
                            lnkbtnaddattachments.Enabled = true;
                            lnkbtnaddattachmentsclear.Enabled = true;
                            ddlApplicationStatus.Enabled = true;
                            btnConfirm.Enabled = true;
                        }
                        txtWritingandVerifyingStatus.Text = objDataRow["VERIFY_STATUS"].ToString(); ;

                        //Load level 01
                        stringHRNID = objDataRow["hrn_id"].ToString();
                        txtHRN.Text = stringHRNID.Trim().ToUpper();
                        txtName.Text = objDataRow["PATIENT_SHORT_NAME"].ToString();
                        txtpurpose.Text = objDataRow["REPORT_PURPOSE_NAME"].ToString();
                        txtreportformat.Text = objDataRow["REPORT_FORMAT_NAME"].ToString();
                        txtreporttypeID.Text = objDataRow["rpttyp_id"].ToString();
                        txtreporttype.Text = objDataRow["REPORT_TYPE_SHORT_NAME"].ToString(); 

                        if (objDataRow["TCU_DATE"] != null && objDataRow["TCU_DATE"].ToString().Trim().Length > 0)
                        { txtAppDate.Text = Convert.ToDateTime(objDataRow["TCU_DATE"]).ToString("dd-MM-yyyy"); }

                        if (objDataRow["accident_date"] != null && objDataRow["accident_date"].ToString().Trim().Length > 0)
                        { txtAccidentDate.Text = Convert.ToDateTime(objDataRow["accident_date"]).ToString("dd-MM-yyyy"); }

                        txtrelatedMRref.Text = objDataRow["Related_MR_Ref"].ToString();

                        if (objDataRow["reasses_date"] != null && objDataRow["reasses_date"].ToString().Trim().Length > 0)
                        { txtReassDate.Text = Convert.ToDateTime(objDataRow["reasses_date"]).ToString("dd-MM-yyyy"); }

                        txtConInforDoctor.Text = objDataRow["Con_Info_Doc"].ToString();

                        if (objDataRow["EMR"].ToString().ToUpper() == "Y")
                        {
                            rbtEMR.Checked = true;
                            updpnlEMR.Update();
                            LoadDepartmentselect(true);
                        }
                        else if (objDataRow["EMR"].ToString().ToUpper() == "N")
                        {
                            rbt1EMR.Checked = true;
                            updpnlEMR.Update();
                            LoadDepartmentselect(false);
                        }
                        else if (objDataRow["EMR"].ToString().ToUpper() == "BOTH")
                        {
                            rbtnboth.Checked = true;
                            updpnlEMR.Update();
                            LoadDepartmentselect(false);
                        }

                        stringReference = objDataRow["SOURCE_REF"].ToString();
                        if (ddlReference.Items.FindByValue(stringReference) != null)
                        { ddlReference.ClearSelection(); ddlReference.Items.FindByValue(stringReference).Selected = true; }

                        txtcurrentloc.Text = objDataRow["DEP_SHORT_NAME"].ToString();
                        txtLocation.Text = objDataRow["DEP_SHORT_NAME"].ToString();
                        txtRemarks.Text = objDataRow["Remarks"].ToString();
                    }
                    else
                    {
                        //pnlCreateRequestas.Visible = true;
                        //pnlprofilesummary.Visible = false;
                        Session["REQUESTDATA"] = null;
                        ClearHeaderValues();
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
                stringServiceType1 = "";
                stringexp012 = "";
                objDataRow = null;
                stringStatus = "";
                stringHRNID = "";
                stringReference = "";
            }
        }
        #endregion
        #region Errorpopup
        private void Errorpopup(string[] stringOutputResult)
        {
            try
            {
                lblErrorType.Text = stringOutputResult[1];
                lblErrorCode.Text = stringOutputResult[0];
                
                txterrormsg.Text = stringOutputResult[2];
                Modelpopuperror.Show();
                pnlerrorpopup.Visible = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        #endregion
        private void PopulateProcessHistory(int recordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdProcessHistory);
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
                rptPagerProcessHistory.DataSource = pages;
                rptPagerProcessHistory.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void LkBtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("FDS002R1V1.aspx", true);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }
        #region top icons
        protected void imgbtnprint_Click(object sender, ImageClickEventArgs e)
        {
        } 

        protected void imgbtnAudit_Click(object sender, ImageClickEventArgs e)
        {
        }
        protected void imgbtnSaveAsDraft_Click(object sender, ImageClickEventArgs e)
        {
            bool boolvalidation = true;
            string stringapptype = "";
            try
            {
                if (txtMRStatus.Text == "PENDING DESPATCH")
                {
                    CommonFunctions.ShowMessageboot(this, "You cannot Save the Request with the status as- PENDING DESPATCH");
                }
                else
                {
                    if (ddlApplicationStatus.SelectedItem != null && ddlApplicationStatus.SelectedValue.Length > 0)
                    {
                        stringapptype = ddlApplicationStatus.SelectedValue.ToString().ToUpper();
                    }
                    if (stringapptype == "REROUTE_REQUEST")
                    {
                        if (txtRemarks.Text.Length == 0)
                        {
                            CommonFunctions.ShowMessageboot(this, "Remarks field should not be empty");
                            boolvalidation = false;
                        }
                    }
                    if(boolvalidation)
                    {
                        SaveData("DRAFT");
                    }
                       
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        private void SaveData(string stringTYPE)
        {
            string stringapptype = "";
            bool boolstatus = false;
            DataTable objdatatable = null;
            string stringNewReqID = "";
            string stringMessage = "";
            try
            {
                if (ddlApplicationStatus.SelectedItem != null && ddlApplicationStatus.SelectedValue.Length > 0)
                {
                    stringapptype = ddlApplicationStatus.SelectedValue.ToString().ToUpper();
                }
                stringNewReqID = txtRequestNo.Text.Trim().ToString();
                if (SaveAttachments(stringNewReqID, stringTYPE))
                {
                    boolstatus = true;
                    if (stringapptype == "REJECT_REQUEST")
                    {
                        if ((stringapptype == "REJECT_REQUEST") && (txtRejectReason.Text.Trim().Length > 0))
                        {
                            if (txtMRStatus.Text.ToUpper() == "PENDING ASSIGNED")
                            {
                                if (UpdateProcessStatusInREVERSEOrder("REJECT_REQUEST"))
                                {
                                    boolstatus = true;
                                }
                                else
                                {
                                    boolstatus = false;
                                }
                            }
                            else
                            {
                                CommonFunctions.ShowMessageboot(this, "You can reject the Request with the status as- PENDING ASSIGNED");
                                boolstatus = false;
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Reject Reason should not be empty");
                            boolstatus = false;
                        }
                    }
                    else if (stringapptype == "REROUTE_REQUEST")
                    {
                        if ((stringapptype == "REROUTE_REQUEST") && (txtcurrentloc.Text.Trim().Length > 0 && ddlDepartmentOU.SelectedItem != null && ddlDepartmentOU.SelectedItem.Value.Length > 0))
                        {
                            if (Session["REQUESTDATA"] != null)
                            {
                                objdatatable = (DataTable)Session["REQUESTDATA"];

                                if (objdatatable != null && objdatatable.Rows.Count > 0)
                                {
                                    if (SaveDeptID(stringNewReqID))
                                    {
                                        boolstatus = true;
                                        if (txtRemarks.Text.Length > 0)
                                        {
                                            if (SaveRemarks(stringNewReqID))
                                            {
                                                boolstatus = true;
                                            }
                                            else
                                            {
                                                boolstatus = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        boolstatus = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Please Select Department OU");
                            boolstatus = false;
                        }
                    }
                    else if (stringapptype == "ASSIGN_DOCTOR_VERIFIER")
                    {
                        if (SaveAssignDocteorandVerifier(stringNewReqID, stringTYPE))
                        {
                            boolstatus = true;
                        }
                        else
                        {
                            boolstatus = false;
                        }
                    }
                }
                if (boolstatus)
                {
                     
                    if (boolstatus)
                    {
                        Session["REQUESTID_COMPLETED"] = null;
                        Session["REQUESTID_FDS001R1V1"] = stringNewReqID;
                        if(ViewState["DOCTORCHANGES"] == null)
                        {
                            stringMessage = "Record Saved Successfully";
                            ShowMessageandReloadPage(this, stringMessage, "FDS001R1V1.aspx");
                        }
                        else if (ViewState["DOCTORCHANGES"].ToString() == "ADDED" )
                        { 
                            bool boolUpdatestatusDOCTOR = true;
                            if (txtMRStatus.Text.ToUpper() == "PENDING REPORT" )
                            {
                                objdatatable = LoadDocterandVerifiersCOnfirm(txtRequestNo.Text.Trim()); 
                                if (objdatatable != null && objdatatable.Rows.Count > 0 && objdatatable.Select("VERIFY_REF ='VERIFIER'").Length > 0)
                                {
                                    int intoverallDOCTORpending = 0; 
                                    int intoverallDOCTORREJECTED = 0;
                                    int intoverallcerifier = 0;
                                    if (objdatatable.Select("VERIFY_REF = 'VERIFIER'").Length > 0)
                                    {
                                        intoverallcerifier = Convert.ToInt32(objdatatable.Select("VERIFY_REF ='DOCTOR'").Length.ToString());
                                    }

                                    if (objdatatable.Select("VERIFY_REF = 'VERIFIER' and STATUS = 'REJECTED'").Length > 0)
                                    {
                                        intoverallDOCTORREJECTED = Convert.ToInt32(objdatatable.Select("VERIFY_REF ='DOCTOR' and STATUS = 'REJECTED'").Length.ToString());
                                    } 
                                    if (objdatatable.Select("VERIFY_REF = 'VERIFIER' and (STATUS = 'IN-PROGRESS' or STATUS = 'PENDING')").Length > 0)
                                    {
                                        intoverallDOCTORpending = Convert.ToInt32(objdatatable.Select("VERIFY_REF ='VERIFIER' and (STATUS = 'IN-PROGRESS' or STATUS = 'PENDING')").Length.ToString());
                                    }

                                    if (intoverallcerifier == intoverallDOCTORREJECTED)
                                    {
                                        boolUpdatestatusDOCTOR = false;
                                    }
                                    else if (intoverallDOCTORpending > 0)
                                    {
                                        boolUpdatestatusDOCTOR = false;
                                    } 
                                }
                                 
                            }
                            if (boolUpdatestatusDOCTOR == true)
                            { 
                                string script = @"<script type='text/javascript'>btnCancelClicked();</script>";  
                                ClientScript.RegisterStartupScript(this.GetType(), "btnCancelClicked", script); 
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
                stringapptype = "";
                // boolstatus = false;
                objdatatable = null;
                stringNewReqID = "";
                stringMessage = "";
            }
        }
        private void ShowMessageandReloadPage(Page page, string stringMessageContent, string stringURL)
        {
            try
            {
                if (stringMessageContent.Trim().Length > 0)
                {
                    stringMessageContent = Server.HtmlEncode(stringMessageContent);
                    stringMessageContent = CommonFunctions.EncodeToJavaString(stringMessageContent.Trim());
                    ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "function", "<script language='JavaScript'>  bootbox.alert('" + stringMessageContent + "');window.location='" + stringURL + "';</script>", false);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private bool SaveDeptID(string stringNewReqID)
        {
            string[] stringresult = null;
            string[] stringOutputResult = null;
            string stringformid = "FDS001R1V1";
            string stringServiceType = "";

            int interrorcount = 0;
            int intTotalRecord = 0;
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataRow objdatarow = null;
            DataSet objDatasetResult = null;
            string stringServiceType1 = "DEFAULT";
            string stringexp = "";
            try
            {
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                    {
                        objdatarow = objDatasetResult.Tables["t1"].NewRow();
                        objdatarow["be_id"] = CommonFunctions.GETBussinessEntity();
                        objdatarow["Request_ID"] = stringNewReqID;
                        if (ddlDepartmentOU.SelectedItem != null && ddlDepartmentOU.SelectedItem.Value.Length > 0)
                        {
                            objdatarow["dept_id"] = ddlDepartmentOU.SelectedItem.Value;
                        }
                        objDatasetResult.Tables["t1"].Rows.Add(objdatarow);


                        if (objDatasetResult != null && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objDatasetResult.AcceptChanges();
                            objDatasetResult.Tables["t1"].Rows[0].SetModified();
                            objDatasetResult = objDatasetResult.GetChanges();
                            stringServiceType = "OperationServiceDML";
                            objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                            if (intErrorCount == 0)
                            {
                                CommonFunctions.ShowMessageboot(this, "Transaction Saved Successfully");
                                return true;
                            }
                            else
                            {
                                Errorpopup(stringresult);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    Errorpopup(stringresult);
                    return false;
                }
                return false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return false;
            }
            finally
            {
                stringresult = null;
                stringOutputResult = null;
                stringformid = "";
                stringServiceType = "";

                interrorcount = 0;
                intTotalRecord = 0;
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = int.MaxValue;
                objdatarow = null;
                objDatasetResult = null;
                stringServiceType1 = "";
                stringexp = "";
            }
        }
        private bool SaveRemarks(string stringNewReqID)
        {
            string[] stringresult = null;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringServiceType = "";

            int interrorcount = 0;
            int intTotalRecord = 0;
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataRow objdatarow = null;
            DataSet objDatasetResult = null;
            string stringServiceType1 = "DEFAULT";
            string stringexp = "";
            string stringtransid = "";
            try
            {
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables["t9"] != null && objDatasetResult.Tables["t9"].Rows.Count == 0)
                    {
                        if (txtRemarks.Text.Length > 0)
                        {
                            objdatarow = objDatasetResult.Tables["t9"].NewRow();
                            objdatarow["be_id"] = CommonFunctions.GETBussinessEntity();
                            objdatarow["Request_ID"] = stringNewReqID;

                            objdatarow["SHORT_NAME"] = txtRemarks.Text.Trim();
                            objdatarow["Remarks"] = txtRemarks.Text.Trim();
                            objdatarow["LONG_NAME"] = txtRemarks.Text.Trim();
                            objdatarow["REMARKS_DATE"] = CommonFunctions.ConvertToDateTime(DateTime.Now.ToString(), "dd-MM-yyyy");
                            objdatarow["TARG_AUD"] = "ALL";

                            objdatarow["REGRMK_ID"] = "DEPARTMENT REROUTE";
                            stringtransid = DateTime.Now.ToString("HHmmssfff");
                            objdatarow["reference_1"] = stringtransid;
                            CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                            objDatasetResult.Tables["t9"].Rows.Add(objdatarow);

                            if (objDatasetResult != null && objDatasetResult.Tables["t9"] != null && objDatasetResult.Tables["t9"].Rows.Count > 0)
                            {
                                objDatasetResult = objDatasetResult.GetChanges();
                                stringServiceType = "OperationServiceDML";
                                objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                                if (intErrorCount == 0)
                                {
                                    return true;
                                    //CommonFunctions.ShowMessageboot(this, "Transaction Saved Successfully");
                                }
                                else
                                {
                                    Errorpopup(stringresult);
                                }
                            }
                        }
                    }
                    return false;
                }
                else
                {
                    Errorpopup(stringresult);
                    return false;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return false;
            }
            finally
            {
                stringresult = null;
                stringOutputResult = null;
                stringformid = "";
                stringServiceType = "";

                interrorcount = 0;
                intTotalRecord = 0;
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                objdatarow = null;
                objDatasetResult = null;
                stringServiceType1 = "";
                stringexp = "";
                stringtransid = "";
            }
        }

        private bool BulkCOlumnUpdate(DataSet objDataSetNEWDatatsble, DataSet objDataSetDBtable, out DataSet objDatasetResult)
        {
            bool boolStatus = true;
            DataRow objdatarow = null;
            objDatasetResult = null;
            string stringSAVEDColumnNAME = "";
            string stringNEWColumnValue = "";
            try
            {
                if (objDataSetNEWDatatsble != null && objDataSetNEWDatatsble.Tables[0] != null && objDataSetNEWDatatsble.Tables[0].Rows.Count > 0 && objDataSetDBtable != null && objDataSetDBtable.Tables[0] != null)
                {
                    objdatarow = objDataSetDBtable.Tables["t1"].NewRow();
                    objDataSetDBtable.Tables["t1"].Rows.Add(objdatarow);
                    objDataSetDBtable.AcceptChanges();
                    for (int intIndex1 = 0; intIndex1 < objDataSetNEWDatatsble.Tables[0].Rows.Count; intIndex1++)
                    {
                        for (int intIndex = 0; intIndex < objDataSetDBtable.Tables[0].Columns.Count; intIndex++)
                        {
                            stringSAVEDColumnNAME = objDataSetDBtable.Tables[0].Columns[intIndex].ToString().ToUpper();
                            stringNEWColumnValue = objDataSetNEWDatatsble.Tables[0].Rows[0][stringSAVEDColumnNAME].ToString();
                            if (stringSAVEDColumnNAME != "CREATED_AT" || stringSAVEDColumnNAME != "CREATED_BY" || stringSAVEDColumnNAME != "CREATED_ON" || stringSAVEDColumnNAME != "SYS_STAMP")
                            {
                                if (stringSAVEDColumnNAME == "MODIFIED_BY")
                                {
                                    objDataSetDBtable.Tables[0].Rows[0][stringSAVEDColumnNAME] = Session["G11EOSUser_Name"].ToString();
                                }
                                if (stringSAVEDColumnNAME == "MODIFIED_ON")
                                {
                                    objDataSetDBtable.Tables[0].Rows[0][stringSAVEDColumnNAME] = DateTime.Now;
                                }
                                if (stringSAVEDColumnNAME == "MODIFIED_AT")
                                {
                                    objDataSetDBtable.Tables[0].Rows[0][stringSAVEDColumnNAME] = Session["stringComputerName"].ToString();
                                }
                                else
                                {
                                    objDataSetDBtable.Tables[0].Rows[0][stringSAVEDColumnNAME] = objDataSetNEWDatatsble.Tables[0].Rows[0][stringSAVEDColumnNAME];
                                }
                            }
                        }
                    }
                    objDataSetDBtable.Tables[0].AcceptChanges();
                    objDatasetResult = objDataSetDBtable;
                }
                return boolStatus;

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return boolStatus;
            }
            finally
            {
                objdatarow = null;
                stringSAVEDColumnNAME = "";
                stringNEWColumnValue = "";
            }
        }
        #endregion

        #region Pannel control
        protected void btnInfoRequest_Click(object sender, EventArgs e)
        {
            pnlmenu1.Visible = true;
            pnlmenu2.Visible = false;
            pnlmenu3.Visible = false;
            pnlProcessHistory.Visible = false;
        }

        protected void btnAttachment_Click(object sender, EventArgs e)
        {
            pnlmenu1.Visible = false;
            pnlmenu2.Visible = true;
            pnlmenu3.Visible = false;
            pnlProcessHistory.Visible = false;
        }

        protected void btnSelectAction_Click(object sender, EventArgs e)
        {
            pnlmenu1.Visible = false;
            pnlmenu2.Visible = false;
            pnlmenu3.Visible = true;
            pnlProcessHistory.Visible = false;
        }

        protected void btnProcessHistory_Click(object sender, EventArgs e)
        {
            pnlmenu1.Visible = false;
            pnlmenu2.Visible = false;
            pnlmenu3.Visible = false;
            pnlProcessHistory.Visible = true;
        }

        private void TABActive(int intIndex)//fix
        {
            try
            {
                switch (intIndex)
                {

                    case 1:  // Patients
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showpanel('li_Patient');", true);
                            btnInfoRequest_Click(null, null);
                            txtHRN.Attributes.Add("onfocus", "this.select()");
                            break;
                        }
                    case 2: // Attachment
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showpanel1('li_Requestor');", true);
                            btnAttachment_Click(null, null);
                            txtRemark.Attributes.Add("onfocus", "this.select()");
                            break;
                        }
                    case 3: // selection
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showpanel2('li_Request');", true);
                            btnSelectAction_Click(null, null);
                            ddlApplicationStatus.Attributes.Add("onfocus", "this.select()");
                            break;
                        }
                    case 4: // ProcessHistory
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showpanel8('li_ProcessHistory');", true);
                            btnProcessHistory_Click(null, null);

                            break;
                        }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        #endregion

        #region process his
        protected void gvprocesshistorygrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringSort = string.Empty;
            string stringReference1 = "";
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
                    if (objDataRow["Trans_Status"].ToString() == "PENDING")
                    { e.Row.FindControl("lblCOMPLETED_DATE").Visible = false; }

                    stringReference1 = objDataRow["reference_1"].ToString();
                    if (stringReference1 != null && stringReference1.Trim().Length > 0)
                    { ((Label)e.Row.FindControl("lblTRANS_STATUS")).Text = stringReference1; }


                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringSort = string.Empty;
                stringReference1 = "";
                objDRV = null;
                objDataRow = null;
            }
        }

        protected void lnkPageProcessHistory_Click(object sender, EventArgs e)
        {
            int intrecFromProcessHistory1 = 0;
            string stringRequestID = "";
            try
            {
                if (Session["intpageIndex1"] != null)
                {
                    intpageIndexProcessHistory = Convert.ToInt32(Session["intpageIndex1"].ToString());
                    Session["intpageIndex"] = intpageIndexProcessHistory;
                }
                else
                {
                    intpageIndexProcessHistory = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndexProcessHistory != 0)
                    {
                        Session["intpageIndex"] = intpageIndexProcessHistory;
                    }
                }

                if (intpageIndexProcessHistory == 1)
                {
                    intrecFromProcessHistory = 0;
                    intrecToProcessHistory = CommonFunctions.GridViewPagesize(stringformIdProcessHistory);
                }
                else
                {
                    intrecFromProcessHistory1 = (intpageIndexProcessHistory * intrecToProcessHistory) - intrecToProcessHistory;
                    intrecFromProcessHistory = intrecFromProcessHistory1 + 1;
                    intrecToProcessHistory = intrecFromProcessHistory1 + CommonFunctions.GridViewPagesize(stringformIdProcessHistory);
                }
                if (Session["REQUEST_FromSummary"] != null && Session["REQUEST_FromSummary"].ToString().Trim().Length > 0)
                {
                    stringRequestID = Session["REQUEST_FromSummary"].ToString();
                    LoadProcessHistory(stringRequestID, "NONLOAD");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                intrecFromProcessHistory1 = 0;
                stringRequestID = "";
            }
        }

        private void LoadProcessHistory(string stringRequestID, string stringTYPE)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0002R1V1";
            string stringOrderBy = "mrpdt.SEQ_ID asc";
            int intFromRecord = intrecFrom;
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
                if ((Session["LoadProcessHistoryFC0001"] == null) || (stringTYPE.Length > 0 && stringTYPE != "LOAD"))
                {
                    stringServiceType = "List2R1V1";
                    stringExpression = "And mrpdt.be_id= '" + stringBoID + "' And mrpdt.request_id= '" + stringRequestID + "' ";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    PopulateProcessHistory(intTotalRecord, intpageIndex);
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
                        pnlProcessHistory.Visible = false;
                    }
                }
                else
                {
                    if (HttpContext.Current.Session["LoadProcessHistoryFC0001"] != null)
                    {
                        objDataTable = (DataTable)HttpContext.Current.Session["LoadProcessHistoryFC0001"];
                    }
                }
                if (stringRequestID.Trim().Length > 0)
                {
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        gvprocesshistorygrid.DataSource = objDataTable;
                        gvprocesshistorygrid.DataBind();
                        pnlProcessHistory.Visible = true;
                        btnProcessHistory.Visible = true;
                        lblpnlprocesshistory.InnerText = objDataTable.Rows.Count.ToString();
                    }
                    else
                    {
                        gvprocesshistorygrid.DataSource = null;
                        gvprocesshistorygrid.DataBind();
                        pnlProcessHistory.Visible = false;
                        btnProcessHistory.Visible = false;
                        btnProcessHistory.ToolTip = "Don't have any Process History";
                        lblpnlprocesshistory.InnerText = "0";
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
                stringServiceType = "";
                objDataTable = null;
                stringExpression = "";
                stringBoID = "";
            }
        }
        #endregion

        #region Attachments

        protected void gvAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringRequestID = "";
            string stringSort = string.Empty;
            DataRowView objdatarowattachments = null;
            DataRow objDataRow = null;
            string stringReference1 = "";
            string stringreqID = "";
            try
            {
                GridViewRow objGridViewRow = e.Row;
                stringSort = string.Empty;
                if (objGridViewRow.DataItem == null) { return; }

                objdatarowattachments = ((DataRowView)objGridViewRow.DataItem);
                objDataRow = objdatarowattachments.Row;
                if (objDataRow != null)
                {
                    Button objbtndelete = e.Row.FindControl("btnDeleteAddattachments") as Button;
                    if (objDataRow["CREATED_BY"].ToString() == Session["G11EOSUser_Name"].ToString())
                    {
                        objbtndelete.Enabled = true;
                    }
                    else
                    {
                        objbtndelete.Enabled = false;
                    }
                }
                if (objDataRow != null)
                {
                    if (Session["REQUESTID_FDS001R1V1"] != null)
                    {
                        stringRequestID = Session["REQUESTID_FDS001R1V1"].ToString();
                    }
                    stringReference1 = objDataRow["DOC_NAME"].ToString();
                    if (stringRequestID.Length > 0)
                    {
                        stringreqID = stringRequestID.ToString().Replace('/', '_');
                        if (stringreqID.Length > 0)
                        {
                            stringReference1 = stringreqID + "_" + stringReference1;
                        }
                    }
                    if (stringReference1 != null && stringReference1.Trim().Length > 0)
                    { ((LinkButton)e.Row.FindControl("lnkbtnattachmentid")).Text = stringReference1; }
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringRequestID = "";
                stringSort = string.Empty;
                objdatarowattachments = null;
                objDataRow = null;
                stringReference1 = "";
                stringreqID = "";
            }
        }
        protected void lnkbtnaddattachments_Click(object sender, EventArgs e)
        {
            DataTable objDataTableAddAttachments = new DataTable();
            string stringBoID = "";
            string stringCategory = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            bool boolAllowedExtention = false;
            bool boolCHECKRecordExist = true;
            string stringExtention = "";
            string stringFileName = "";
            string stringpasscode = "";
            string[] stringExtentionAllowed;
            byte[] objbyteArray = null;
            int intFileSize, intMaxFileSize;
            int intErrorCount = 0;
            DataRow[] objdatarow = null;
            string[] stringOutputResult = null;
            string stringLogFileDateFormat, stringuserID, stringATTACHID = "";

            string stringSMRId = "";
            bool boolexist = true; 
            DataRow[] objdatarowSMR = null;
            string stringPath = "";
            string stringFileNameID = "";
            DataRow objDataRow = null;
            string stringHRN = "";
            try
            {
                if (txtHRN.Text.Trim().Length > 0)
                {
                    if (ddlCategory.SelectedItem != null && ddlCategory.SelectedValue.Length > 0)
                    {
                        if (ddlCategory.SelectedItem != null)
                        {
                            stringCategory = ddlCategory.SelectedItem.Value;
                        }
                        if (Session["ADD_ATTACHMENTS"] != null)
                        { objDataTableAddAttachments = (DataTable)Session["ADD_ATTACHMENTS"]; }
                        if (objDataTableAddAttachments != null && objDataTableAddAttachments.Rows.Count > 0)
                        {
                            if (stringCategory == "SUPPORTING DOCUMENT" || stringCategory == "COMPLETED MEDICAL REPORTS")
                            {
                                objdatarowSMR = objDataTableAddAttachments.Select("SMR_ID <>'" + stringSMRId + "'");
                                if (objdatarowSMR.Length > 0)
                                {
                                    boolexist = false;
                                }
                            }
                        }
                        if (boolexist)
                        {
                            stringFileName = FileUpload1.FileName.Trim();
                            if(stringFileName.Contains("'"))
                            {
                                stringFileName = stringFileName.ToString().Replace("'", "");
                            }
                            if (FileUpload1.PostedFile != null && FileUpload1.HasFile)
                            {
                                double doubleSizeInMB = (double)FileUpload1.PostedFile.ContentLength / (1024.0 * 1024.0);
                                if (FileUpload1.PostedFile.FileName.Trim().Length > 0)
                                {
                                    if (Session["ADD_ATTACHMENTS"] != null)
                                    { objDataTableAddAttachments = (DataTable)Session["ADD_ATTACHMENTS"]; }
                                    else
                                    {
                                        Loadattachment("123", "UNLOAD");
                                        if (Session["ADD_ATTACHMENTS"] != null)
                                        { objDataTableAddAttachments = (DataTable)Session["ADD_ATTACHMENTS"]; }
                                    }

                                    if (objDataTableAddAttachments != null)
                                    { 
                                        if (objDataTableAddAttachments != null && objDataTableAddAttachments.Rows.Count > 0)
                                        {
                                            objdatarow = objDataTableAddAttachments.Select("DOC_NAME = '" + stringFileName.ToString() + "'");
                                            if (objdatarow != null && objdatarow.Length > 0)
                                            {
                                                boolCHECKRecordExist = false;
                                            }
                                        }
                                        if (boolCHECKRecordExist)
                                        {
                                            stringExtention = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                                            stringExtentionAllowed = ConfigurationManager.AppSettings["AttachmentsExtentionAllowedSMR"].ToString().Split('|');
                                            foreach (string stringFilter in stringExtentionAllowed)
                                            {
                                                if (stringExtention == stringFilter)
                                                {
                                                    boolAllowedExtention = true;
                                                }
                                            }
                                            if (boolAllowedExtention)
                                            {
                                                intMaxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["AttachmentsImgSize"].ToString());
                                                intFileSize = FileUpload1.FileBytes.Length / (1024 * 1024);
                                                if (intFileSize < intMaxFileSize)
                                                {
                                                    stringATTACHID = DateTime.Now.ToString("HHmmssff").ToUpper();
                                                    stringuserID = HttpContext.Current.Session["G11EOSUser_Name"] != null ? HttpContext.Current.Session["G11EOSUser_Name"].ToString() : "GUEST";

                                                    stringLogFileDateFormat = ConfigurationManager.AppSettings["LogFileDateFormat"].ToString();

                                                    stringPath = ConfigurationManager.AppSettings["AttachmentPathUploadPath"].ToString() + @"\" + DateTime.Now.ToString(stringLogFileDateFormat) + @"\" + stringuserID + @"\";
                                                    if (!System.IO.Directory.Exists(stringPath))
                                                        System.IO.Directory.CreateDirectory(stringPath);
                                                    stringPath += stringFileName;
                                                    FileUpload1.PostedFile.SaveAs(stringPath);
                                                    objbyteArray = File.ReadAllBytes(stringPath);

                                                    if (objDataTableAddAttachments != null)
                                                    {
                                                        objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                                                        objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FC0001R1V1_COMPLETEREPORT";
                                                        stringFileNameID = stringFileName ;
                                                        clsCertificateValidation.EnableTrustedHosts();
                                                        using (GSFileTransferService.FileTransferServiceClient objFileTransfer1 = new GSFileTransferService.FileTransferServiceClient())
                                                        {
                                                            objFileTransfer1.UploadFileR1V2(objbyteArray, stringFileNameID, stringATTACHID, objDatasetAppsVariables, out intErrorCount, out stringOutputResult);
                                                            if (objFileTransfer1 != null)
                                                                objFileTransfer1.Close();
                                                        }
                                                        if (intErrorCount != 0)
                                                        {
                                                            Errorpopup(stringOutputResult);
                                                        }
                                                        else
                                                        {
                                                            objDataRow = objDataTableAddAttachments.NewRow();

                                                            stringHRN = txtHRN.Text.Trim();
                                                            stringpasscode = stringHRN.Substring(stringHRN.Length - 4);

                                                            objDataRow["BE_ID"] = stringBoID;
                                                            objDataRow["ATTACH_ID"] = stringATTACHID.ToString().ToUpper();
                                                            objDataRow["DOC_NAME"] = stringFileNameID;
                                                            objDataRow["DOC_SIZE"] = intFileSize;
                                                            objDataRow["DOC_TYPE"] = stringExtention;
                                                            if (ddlCategory.SelectedItem != null)
                                                            {
                                                                objDataRow["CATEGORY"] = ddlCategory.SelectedItem.Value.ToString();
                                                            }
                                                            objDataRow["FORM_ID"] = "FC0001R1V1_COMPLETEREPORT";
                                                            objDataRow["TRANS_ID"] = txtRequestNo.Text.Trim().ToString();
                                                            objDataRow["DELMARK"] = "N";
                                                            objDataRow["REMARKS"] = txtRemark.Text.Trim();

                                                            if (ddlCategory.SelectedItem != null && ddlCategory.SelectedItem.Value == "COMPLETED MEDICAL REPORTS")
                                                            {
                                                                objDataRow["REPORT_NAME"] = "MRTS – Medical Report";
                                                            }
                                                            else if (ddlCategory.SelectedItem != null && ddlCategory.SelectedItem.Value == "SUPPORTING DOCUMENT")
                                                            {
                                                                objDataRow["REPORT_NAME"] = "MRTS – MR Request";
                                                                objDataRow["INCLUDED_IN_REPORT"] = "Y";
                                                            }
                                                            else
                                                            {
                                                                objDataRow["INCLUDED_IN_REPORT"] = "N";
                                                            }
                                                            objDataRow["DML_INDICATOR"] = "I";
                                                            CommonFunctions.AssignAuditLogDetails(ref objDataRow);

                                                            objDataTableAddAttachments.Rows.Add(objDataRow);
                                                            Session["ADD_ATTACHMENTS"] = objDataTableAddAttachments;
                                                        }
                                                    }
                                                    TABActive(2);
                                                    lbltotalrecAttachments.InnerText = objDataTableAddAttachments.Rows.Count.ToString();
                                                    gvAttachments.DataSource = objDataTableAddAttachments;
                                                    gvAttachments.DataBind();
                                                }
                                                else
                                                {
                                                    CommonFunctions.ShowMessageboot(this, "File Size is too large");
                                                }
                                            }
                                            else
                                            {
                                                CommonFunctions.ShowMessageboot(this, "File Extention is not valid");
                                            }
                                        }
                                        else
                                        {
                                            CommonFunctions.ShowMessageboot(this, "File already exist");
                                        }

                                    }
                                    else
                                    {
                                        lbltotalrecAttachments.InnerText = "0";
                                        gvAttachments.DataSource = null;
                                        gvAttachments.DataBind();
                                    }

                                    ddlCategory.SelectedIndex = 0;
                                    txtRemark.Text = "";
                                    ddlCategory.Focus();

                                    TABActive(2);
                                }
                            }
                            else
                            {
                                TABActive(2);
                                CommonFunctions.ShowMessageboot(this, "Please select a File for Attachment");
                            }
                        }
                        else
                        {
                            TABActive(4);
                            CommonFunctions.ShowMessageboot(this, "Complete Medical Report ,Already Forward to SMR");
                        }
                    }
                    else
                    {
                        TABActive(4);
                        CommonFunctions.ShowMessageboot(this, "Please Choose a Category");
                    }
                }
                else
                {
                    TABActive(1);
                    CommonFunctions.ShowMessageboot(this, "Please fill patient details");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDataTableAddAttachments = new DataTable();
                stringBoID = "";
                stringCategory = "";
                stringExtention = "";
                stringFileName = "";
                stringpasscode = "";
                objbyteArray = null;
                intErrorCount = 0;
                objdatarow = null;
                stringOutputResult = null;
                stringLogFileDateFormat = "";
                stringuserID = "";
                stringATTACHID = "";

                stringSMRId = ""; 
                objdatarowSMR = null;
                stringPath = "";
                stringFileNameID = "";
                objDataRow = null;
                stringHRN = "";
            }

        }
        protected void lnkbtnattachmentid_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string[] stringOutputResult = null;
                DataSet objDatasetAppsVariables = (DataSet)HttpContext.Current.Session["objDatasetlocaldeclaration"];
                objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = "FINT0003R1V1";
                string stringCmdArgument = "";
                long longlength;
                string stringFilepath = "";
                string[] stringValues = null;
                byte[] objbyteArray = null;
                string stringRequestID = "";
                string stringreqID = "";
                string stringBE_ID, stringFORM_ID, stringTRANS_ID, stringDOC_NAME, stringDOC_TYPE, stringATTACH_ID, StringFileName = string.Empty;
                try
                {
                    LinkButton objLinkButton = (LinkButton)sender;

                    if (objLinkButton != null)
                    {
                        StringFileName = objLinkButton.Text;
                        stringCmdArgument = ((LinkButton)sender).CommandArgument;
                        if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                        {
                            stringValues = stringCmdArgument.Split(',');
                            if (stringValues != null && stringValues.Length > 0)
                            {
                                stringBE_ID = stringValues[0];
                                stringFORM_ID = stringValues[1];
                                stringTRANS_ID = stringValues[2];
                                stringDOC_NAME = stringValues[3];
                                stringDOC_TYPE = stringValues[4];
                                stringATTACH_ID = stringValues[5];
                                StringFileName = stringBE_ID + @"\" + stringFORM_ID + @"\" + stringATTACH_ID + @"\" + stringDOC_NAME;
                            }
                        }
                        clsCertificateValidation.EnableTrustedHosts();

                        using (GSFileTransferService.FileTransferServiceClient objFileTransfer1 = new GSFileTransferService.FileTransferServiceClient())
                        {
                            objFileTransfer1.DownloadFileFromServer(ref objDatasetAppsVariables, ref StringFileName, out longlength, out stringFilepath, out stringOutputResult, out objbyteArray);
                            if (objFileTransfer1 != null)
                                objFileTransfer1.Close();
                        }

                        if (stringFilepath != null && stringFilepath.Length > 0)
                        {
                            if (Session["REQUESTID_FDS001R1V1"] != null)
                            {
                                stringRequestID = Session["REQUESTID_FDS001R1V1"].ToString();
                                stringreqID = stringRequestID.ToString().Replace('/', '_');
                                StringFileName = stringreqID + "_" + StringFileName;
                            }
                            CommonFunctions.OpenExportedFileR1V1(this, objbyteArray, StringFileName.ToString(), "ATTACHMENTS");
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
                    stringOutputResult = null;
                    stringCmdArgument = "";
                    stringFilepath = "";
                    stringValues = null;
                    stringRequestID = "";
                    stringreqID = "";
                    stringBE_ID = "";
                    stringFORM_ID = "";
                    stringTRANS_ID = "";
                    stringDOC_NAME = "";
                    stringDOC_TYPE = "";
                    stringATTACH_ID = "";
                    StringFileName = string.Empty;

                }
            }
        }
        protected void btnDeleteAddattachments_Click(object sender, EventArgs e)
        {
            DataTable objDataTableCopy = null;
            string stringCmdArgument = "";
            string stringCondition = "";
            string[] stringValues = null;
            DataRow[] objDataRow = null;
            DataTable objDataTableATTACHMENTs = null;
            DataRow[] objDatarow = null;
            DataRow[] objDatarowdel = null;
            string stringBE_ID, stringFORM_ID, stringTRANS_ID, stringDOC_NAME, stringDOC_TYPE, stringATTACH_ID, StringFileName = string.Empty;
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
                            stringBE_ID = stringValues[0];
                            stringFORM_ID = stringValues[1];
                            stringTRANS_ID = stringValues[2];
                            stringDOC_NAME = stringValues[3];
                            stringDOC_TYPE = stringValues[4];
                            stringATTACH_ID = stringValues[5];

                            if (stringATTACH_ID != null && stringDOC_NAME.Trim().Length > 0)
                            {
                                if (Session["ADD_ATTACHMENTS"] != null)
                                {
                                    objDataTableATTACHMENTs = (DataTable)Session["ADD_ATTACHMENTS"];
                                    if (objDataTableATTACHMENTs != null && objDataTableATTACHMENTs.Rows.Count > 0)
                                    {//new
                                        stringCondition = "ATTACH_ID='" + stringATTACH_ID + "'and TRANS_ID='" + stringTRANS_ID + "'";
                                        objDataRow = objDataTableATTACHMENTs.Select(stringCondition);
                                        if (objDataRow != null && objDataRow.Length > 0)
                                        {
                                            if (objDataRow[0]["DML_INDICATOR"].ToString() == "I" || (objDataRow[0]["DML_INDICATOR"].ToString() == "U" && objDataRow[0]["rec_no"].ToString().Length == 0))
                                            {
                                                objDataRow[0].Delete();
                                            }
                                            else
                                            {
                                                objDataRow[0]["DML_INDICATOR"] = "D";
                                            }
                                        }

                                        Session["ADD_ATTACHMENTS"] = objDataTableATTACHMENTs;

                                        objDatarow = objDataTableATTACHMENTs.Select("DML_INDICATOR<>'D'");
                                        if (objDatarow.Length > 0)
                                        {
                                            objDataTableCopy = objDataTableATTACHMENTs.Select("DML_INDICATOR<>'D'").CopyToDataTable<DataRow>();
                                        }
                                        else
                                        {
                                            objDataTableCopy = null;
                                        }
                                        if (objDataTableCopy != null && objDataTableCopy.Rows.Count > 0)
                                        {
                                            lbltotalrecAttachments.InnerText = objDataTableCopy.Rows.Count.ToString();
                                        }
                                        else
                                        {
                                            lbltotalrecAttachments.InnerText = "0";
                                        }
                                        gvAttachments.DataSource = objDataTableCopy;
                                        gvAttachments.DataBind();
                                        objDatarowdel = objDataTableATTACHMENTs.Select("DML_INDICATOR='D'");
                                        if (objDatarowdel.Length > 0)
                                        {
                                            SaveAttachments(txtRequestNo.Text.Trim(), "");
                                            Loadattachment(txtRequestNo.Text.Trim(), "NONLOAD");
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
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDataTableCopy = null;
                stringCmdArgument = "";
                stringCondition = "";
                stringValues = null;
                objDataRow = null;
                objDataTableATTACHMENTs = null;
                objDatarow = null;
                objDatarowdel = null;
                stringBE_ID = "";
                stringFORM_ID = string.Empty;
                stringTRANS_ID = string.Empty;
                stringDOC_NAME = string.Empty;
                stringDOC_TYPE = string.Empty;
                stringATTACH_ID = string.Empty;
                StringFileName = string.Empty;

            }
        }
        protected void lnkbtnaddattachmentsclear_Click(object sender, EventArgs e)
        {
            try
            {
                lbltotalrecAttachments.InnerText = "0";
                Session["ADD_ATTACHMENTS"] = null;
                gvAttachments.DataSource = null;
                gvAttachments.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private void Loadattachment(string stringRequestID, string stringTYPE)//fix
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
            string stringServiceType = "";
            string stringExpression = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (Session["stringCategory"] != null)
                {
                    stringCategory = Session["stringCategory"].ToString();
                }
                if ((Session["LoadattachmentFC0001"] == null) || (stringTYPE.Length > 0 && stringTYPE != "LOAD"))
                {
                    stringServiceType = "List18R1V1";

                    if (stringCategory.Length > 0)
                    {
                        stringExpression = " and dach.be_id='" + stringBoID + "' and dach.trans_id='" + stringRequestID + "' and CATEGORY in(" + stringCategory + ")";
                    }
                    else
                    {
                        stringExpression = " and dach.be_id='" + stringBoID + "' and dach.trans_id='" + stringRequestID + "' ";
                    }

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t18"] != null && objDatasetResult.Tables["t18"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t18"];

                            foreach (DataRow row in objDataTable.Rows)
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
                        Session["ADD_ATTACHMENTS"] = objDatasetResult.Tables["t18"];

                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                        Session["ADD_ATTACHMENTS"] = null;
                    }
                }
                else
                {
                    if (HttpContext.Current.Session["LoadattachmentFC0001"] != null)
                    {
                        objDataTable = (DataTable)HttpContext.Current.Session["LoadattachmentFC0001"];

                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            if (stringCategory.Length > 0)
                            {
                                stringExpression = "be_id='" + stringBoID + "' and trans_id='" + stringRequestID + "' and CATEGORY in(" + stringCategory + ")";
                            }
                            else
                            {
                                stringExpression = "be_id='" + stringBoID + "' and trans_id='" + stringRequestID + "' ";
                            }
                            if (objDataTable.Select(stringExpression).Length > 0)
                            {
                                objDataTable = objDataTable.Select(stringExpression).CopyToDataTable();
                            }
                            else
                            {
                                objDataTable = null;
                            }
                        }

                    }
                }

                if (objDataTable != null && objDataTable.Rows.Count > 0)
                {
                    lbltotalrecAttachments.InnerText = objDataTable.Rows.Count.ToString();
                    Session["ADD_ATTACHMENTS"] = objDataTable;
                    gvAttachments.DataSource = objDataTable;
                    gvAttachments.DataBind();
                }
                else
                {
                    lbltotalrecAttachments.InnerText = "0";
                    //Session["ADD_ATTACHMENTS"] = null;
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
                stringBoID = "";
                stringCategory = "";
                stringServiceType = "";
                stringExpression = "";
            }
        }
        private bool SaveAttachments(string stringRequestID, string stringTYPE)
        {
            DataTable objDataTableAddReports = null;
            bool boolStatus = true;
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
            bool boolContinue = true;
            int intErrorCount = 0;
            string stringDMLIndicator = "";
            string stringbeid = "";
            DataTable objdatatable = null;
            DataTable dtCopy = null;
            DataSet objDatasetResult2 = new DataSet();
            string stringcustemerid3 = "";
            string stringServiceType1 = "";
            string stringformid1 = "";
            try
            {
                if (Session["ADD_ATTACHMENTS"] != null)
                { objDataTableAddReports = (DataTable)Session["ADD_ATTACHMENTS"]; }


                if (objDataTableAddReports != null && objDataTableAddReports.Rows.Count > 0)
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t18"] != null && objDatasetResult.Tables["t18"].Rows.Count == 0)
                        {
                            for (int intIndex = 0; intIndex < objDataTableAddReports.Rows.Count; intIndex++)
                            {
                                objDataRow = objDatasetResult.Tables["t18"].NewRow();

                                stringDMLIndicator = objDataTableAddReports.Rows[intIndex]["DML_INDICATOR"].ToString();

                                if (stringDMLIndicator != null && stringDMLIndicator.Trim().Length > 0)
                                {
                                    if (boolContinue)
                                    {
                                        stringbeid = CommonFunctions.GETBussinessEntity();
                                        objDataRow["BE_ID"] = stringbeid;
                                        objDataRow["ATTACH_ID"] = objDataTableAddReports.Rows[intIndex]["ATTACH_ID"].ToString();
                                        objDataRow["DOC_NAME"] = objDataTableAddReports.Rows[intIndex]["DOC_NAME"].ToString();
                                        objDataRow["DOC_SIZE"] = objDataTableAddReports.Rows[intIndex]["DOC_SIZE"].ToString();
                                        objDataRow["DOC_TYPE"] = objDataTableAddReports.Rows[intIndex]["DOC_TYPE"].ToString();
                                        objDataRow["CATEGORY"] = objDataTableAddReports.Rows[intIndex]["CATEGORY"].ToString();
                                        objDataRow["FORM_ID"] = objDataTableAddReports.Rows[intIndex]["FORM_ID"].ToString();
                                        if (stringRequestID != null && stringRequestID.Trim().Length > 0 && stringRequestID.Trim().ToUpper() != "NULL") { objDataRow["TRANS_ID"] = stringRequestID.Trim().ToUpper(); }
                                        else { objDataRow["TRANS_ID"] = txtRequestNo.Text.Trim().ToUpper(); }
                                        objDataRow["DELMARK"] = objDataTableAddReports.Rows[intIndex]["DELMARK"].ToString();
                                        objDataRow["REMARKS"] = objDataTableAddReports.Rows[intIndex]["REMARKS"].ToString();

                                        objDataRow["REPORT_NAME"] = objDataTableAddReports.Rows[intIndex]["REPORT_NAME"].ToString();
                                        objDataRow["INCLUDED_IN_REPORT"] = objDataTableAddReports.Rows[intIndex]["INCLUDED_IN_REPORT"].ToString();

                                        objDataRow["REFERENCE_5"] = stringDMLIndicator;


                                        CommonFunctions.AssignAuditLogDetails(ref objDataRow);

                                        objDatasetResult.Tables["t18"].Rows.Add(objDataRow);

                                        objDatasetResult.Tables["t18"].Rows[0].RowState.ToString();
                                    }
                                }


                            }
                            if (objDatasetResult.Tables["t18"].Rows.Count > 0)
                            {
                                objdatatable = objDatasetResult.Tables["t18"];
                                if (objDatasetResult.Tables["t18"].Select("REFERENCE_5 <> 'U'").Length > 0)
                                {
                                    objdatatable = objDatasetResult.Tables["t18"].Select("REFERENCE_5 <> 'U'").CopyToDataTable();

                                    dtCopy = objdatatable.Copy();
                                    objDatasetResult2.Tables.Add(dtCopy);
                                    objDatasetResult2.Tables[0].TableName = "t18";
                                    objDatasetResult2.AcceptChanges();

                                    for (int intIndex = 0; intIndex < objDatasetResult2.Tables["t18"].Rows.Count; intIndex++)
                                    {
                                        stringcustemerid3 = objDatasetResult2.Tables["t18"].Rows[intIndex]["REFERENCE_5"].ToString();

                                        if (stringcustemerid3 == "D")
                                        {
                                            objDatasetResult2.Tables["t18"].Rows[intIndex].Delete();
                                        }
                                        else if (stringcustemerid3 == "I")
                                        {
                                            objDatasetResult2.Tables["t18"].Rows[intIndex].SetAdded();
                                        }
                                        else if (stringcustemerid3 == "U")
                                        {
                                            objDatasetResult2.Tables["t18"].Rows[intIndex].SetModified();
                                        }
                                    }

                                    objDatasetResult = objDatasetResult2.GetChanges();
                                    stringServiceType1 = "OperationServiceDML";
                                    stringformid1 = "FC0001R1V1";
                                    objDatasetResult1 = CommonFunctions.DataManipulationR1V1(stringServiceType1, objDatasetResult2.GetChanges(), stringformid1, out intErrorCount, out string[] stringOutputResult1);
                                    if (intErrorCount == 0)
                                    {
                                        objDatasetResult2 = null;
                                        boolStatus = true;
                                    }
                                    else
                                    {
                                        Errorpopup(stringOutputResult1);
                                        boolStatus = false;

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                        return false;
                    }
                }

                return boolStatus;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDataTableAddReports = null;
                boolStatus = true;
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
                stringDMLIndicator = "";
                stringbeid = "";
                objdatatable = null;
                dtCopy = null;
                objDatasetResult2 = null;
                stringcustemerid3 = "";
                stringServiceType1 = "";
                stringformid1 = "";
            }

            return false;
        }
        #endregion

        #region popup DOCTER SELECTION
        protected void btndoctorselectionpopupnew_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Docterpopupclearvalues();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        //for clearvalues
        private void Docterpopupclearvalues()
        {
            try
            {
                ddlDepartmentOUDoctersel.ClearSelection();
                txtdocempnumdocselection.Text = "";
                txtMCRNumberdocselection.Text = "";
                if (rbtndoctor.Checked == true)
                {
                    rbtndoctor.Checked = true;
                }
                else if (rbtndepsec.Checked == true)
                {
                    rbtndepsec.Checked = true;
                }
                else if (rbtnhims.Checked == true)
                {
                    rbtnhims.Checked = true;
                }
                else { rbtndoctor.Checked = true; }
                txtSecretaryname.Text = "";
                txtDoctorNamedocselection.Text = "";
                gvlistdoctorselectionpopup.DataSource = null;
                gvlistdoctorselectionpopup.DataBind();
                PopulatePagerdoctorselection(0, intpageIndexdocselection);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        protected void btndoctorselectionsearch_Click(object sender, ImageClickEventArgs e)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "";
            string stringServiceType = "List15R1V1";
            string stringOrderBy = "";
            DataTable objDataTable = null;
            string stringexp012 = "";
            string stringDepartmentOUDocterselection = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            int intRecordFrom = 0;
            int intRecordTo = 0;
            try
            {
                if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                {
                    stringformid = "FC0001R1V4";
                }
                else if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                {
                    stringformid = "FC0001R1V1";
                }
                if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                {
                    stringexp012 += " delmark = 'N'";
                }
                else if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                {
                    stringexp012 += "AND mrd.delmark = 'N'";
                }

                if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                {
                    if (rbtndoctor.Checked == true)
                    {
                        stringexp012 += "And user_type= 'DOCTORS' ";
                    }
                    else if (rbtndepsec.Checked == true)
                    {
                        stringexp012 += "And user_type IN ('DEPARTMENT SECRETARY','DEPARTMENT USERS')  "; 
                    }
                    else if (rbtnhims.Checked == true)
                    {
                        stringexp012 += "And user_type IN ('HIMS USERS','HIMS SUPERVISOR','HOD','NCCS HIMS TEAM LEAD','SGH HIMS TEAM LEAD')  ";
                    }
                }
                if (ddlDepartmentOUDoctersel.SelectedItem != null && ddlDepartmentOUDoctersel.SelectedItem.Value.Length > 0)
                {
                    stringDepartmentOUDocterselection = ddlDepartmentOUDoctersel.SelectedItem.Value;
                    if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                    {
                        stringexp012 += stringDepartmentOUDocterselection.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(DEPT_CODE)  LIKE UPPER('%" + stringDepartmentOUDocterselection.Trim() + "%'))" : "";
                    }
                    else if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                    {
                        stringexp012 += stringDepartmentOUDocterselection.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(mrd.DEPT_CODE)  LIKE UPPER('%" + stringDepartmentOUDocterselection.Trim() + "%'))" : "";
                    }
                }

                if (txtdocempnumdocselection.Text.Length > 0 && txtdocempnumdocselection.Text.Trim() != "%")
                {
                    if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                    {
                        stringexp012 += txtdocempnumdocselection.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(EMP_NO)  LIKE UPPER('%" + txtdocempnumdocselection.Text.Trim().ToUpper() + "%'))" : "";
                    }
                    else if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                    {
                        stringexp012 += txtdocempnumdocselection.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(mrd.EMP_NO)  LIKE UPPER('%" + txtdocempnumdocselection.Text.Trim().ToUpper() + "%'))" : "";
                    }

                }
                if (txtMCRNumberdocselection.Text.Length > 0 && txtMCRNumberdocselection.Text.Trim() != "%")
                {
                    if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                    {
                        stringexp012 += txtMCRNumberdocselection.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(MCR_NO)  LIKE UPPER('%" + txtMCRNumberdocselection.Text.Trim().ToUpper() + "%'))" : "";
                    }
                    else if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                    {
                        stringexp012 += txtMCRNumberdocselection.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(mrd.MCR_NO)  LIKE UPPER('%" + txtMCRNumberdocselection.Text.Trim().ToUpper() + "%'))" : "";
                    }

                }
                if (txtSecretaryname.Text.Length > 0 && txtSecretaryname.Text.Trim() != "%")
                {
                    if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                    {
                        stringexp012 += txtSecretaryname.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(short_name)  LIKE UPPER('%" + txtSecretaryname.Text.Trim().ToUpper() + "%'))" : "";
                    }

                }
                if (txtDoctorNamedocselection.Text.Length > 0 && txtDoctorNamedocselection.Text.Trim() != "%")
                {
                    if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                    {
                        stringexp012 += txtDoctorNamedocselection.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(short_name)  LIKE UPPER('%" + txtDoctorNamedocselection.Text.Trim().ToUpper() + "%'))" : "";
                    }
                    else if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                    {
                        stringexp012 += txtDoctorNamedocselection.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(mrd.short_name)  LIKE UPPER('%" + txtDoctorNamedocselection.Text.Trim().ToUpper() + "%'))" : "";
                    }
                }


                intRecordFrom = intintrecFromdocselection;
                intRecordTo = intrecTodocselection;

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);

                PopulatePagerdoctorselection(intTotalRecord, intpageIndexdocselection);
                if (interrorcount == 0)
                {

                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t15"] != null && objDatasetResult.Tables["t15"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t15"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        gvlistdoctorselectionpopup.DataSource = objDataTable;
                        gvlistdoctorselectionpopup.DataBind();
                        updtpnldocterselection.Visible = true;

                        LoadDepartmentOU();
                        mdlpopupdoctorselection.Show();
                    }
                    else
                    {
                        gvlistdoctorselectionpopup.DataSource = null;
                        gvlistdoctorselectionpopup.DataBind();
                        CommonFunctions.ShowMessageboot(this, "No Records Found");
                        mdlpopupdoctorselection.Show();
                        LoadDepartmentOU();
                        updtpnldocterselection.Visible = true;
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
                stringformid = "";
                stringServiceType = "";
                stringOrderBy = "";
                objDataTable = null;
                stringexp012 = "";
                stringDepartmentOUDocterselection = "";
                stringbeid = "";
                intRecordFrom = 0;
                intRecordTo = 0;
            }
        }

        //for popup linkbutton click in grid
        protected void lnkbtnpopupDOCID_Click(object sender, EventArgs e)
        {
            string stringdocttype = "";
            string stringMCRno = "";
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringEMPNo = "";
            string stringdocname = "";
            string stringDepname = "";
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
                            stringEMPNo = stringValues[0];
                            stringdocname = stringValues[1];
                            if (stringValues.Length >= 3)
                            {
                                stringMCRno = stringValues[2];
                            }
                            stringDepname = stringValues[3];


                            if (ViewState["DocterSelectionPopup"] != null)
                            {
                                stringdocttype = ViewState["DocterSelectionPopup"].ToString();
                            }

                            if (stringdocttype == "DOCTOR")
                            {
                                AssignDoctertoGrid(stringdocttype, stringEMPNo, stringdocname, stringMCRno, stringDepname);
                            }
                            else if (stringdocttype == "VERIFIER")
                            {
                                AssignDoctertoGrid(stringdocttype, stringEMPNo, stringdocname, stringMCRno, stringDepname);
                            }
                            ViewState["DOCTORCHANGES"] = "ADDED";
                            mdlpopupdoctorselection.Hide();
                            updtpnldocterselection.Visible = false;
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
                stringdocttype = "";
                stringMCRno = "";
                stringCmdArgument = "";
                stringValues = null;
                stringEMPNo = "";
                stringdocname = "";
                stringDepname = "";
            }
        }

        public DataRow[] LoadReportType(string stringRPTTYP_ID)//mrpr
        {
            DataSet objDataSet = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0024R1V2";
            string stringOrderBy = "";
            string stringexp = " AND mrpr.be_ID='" + CommonFunctions.GETBussinessEntity().ToString() + "' AND mrpr.RPTTYP_ID='" + stringRPTTYP_ID.ToString() + "'";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableHOD = null;
            string stringServiceType = "";
            string stringdate = "";
            DataRow[] objdatarow = null;
            try
            {
                stringServiceType = "List1R1V1";
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    //2nd level 1st tab
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["table1"] != null && objDataSet.Tables["table1"].Rows.Count > 0)
                    {
                        objdatatableHOD = objDataSet.Tables["table1"];
                        DateTime objDateTimeFrom = FindNearestDate(objdatatableHOD);
                        stringdate = CommonFunctions.ConvertDateTimetoString(objDateTimeFrom);

                    }
                    //2nd level 22n tab
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["table2"] != null && objDataSet.Tables["table2"].Rows.Count > 0)
                    {
                        objdatatableHOD = objDataSet.Tables["table2"];
                        if (objdatatableHOD != null && objdatatableHOD.Rows.Count > 0)
                        {
                            if (objdatatableHOD.Select("FEE_EFF_DATE = #" + stringdate + "#").Length > 0)
                            {
                                objdatarow = objdatatableHOD.Select("FEE_EFF_DATE = #" + stringdate + "#");
                                return objdatarow;
                            }
                        }

                    }

                }
                else
                {
                    Errorpopup(stringOutputResult);

                }
                return null;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot(this, objException.Message);
                return null;
            }
            finally
            {
                objDataSet = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                stringexp = "";
                intFromRecord = 0;
                intToRecord = 0;
                objdatatableHOD = null;
                stringServiceType = "";
            }

        }
        private DateTime FindNearestDate(DataTable dataTable)
        {
            DateTime today = DateTime.Today;
            DateTime nearestDate = DateTime.MaxValue;
            TimeSpan nearestDifference = TimeSpan.MaxValue;

            foreach (DataRow row in dataTable.Rows)
            {
                // Assuming the column is named "effective_date"
                DateTime date = Convert.ToDateTime(row["FEE_EFF_DATE"]);

                // Filter out dates greater than today
                if (date <= today)
                {
                    TimeSpan difference = today - date;

                    if (difference < nearestDifference)
                    {
                        nearestDifference = difference;
                        nearestDate = date;
                    }
                }
            }
            return nearestDate;
        }
        protected void LkBtnDoc1_Click(object sender, EventArgs e)
        {
            bool boolvalidation = true;
            DataTable objDataTable = null;
            DataTable objdatatablerpttype = null;
            DataRow[] objdatarowdoctorcount = null;
            DataRow[] objdatarow01 = null;
            string stringnoofdoctors = "";
            int intnoofdoctors = 0;
            try
            {
                objdatarow01 = LoadReportType(txtreporttypeID.Text.ToString());
                if (objdatarow01 != null && objdatarow01.Length > 0)
                {
                    stringnoofdoctors = objdatarow01[0]["NO_DOCTORS"].ToString();
                    if (stringnoofdoctors.Length > 0)
                    {
                        intnoofdoctors = Convert.ToInt32(stringnoofdoctors.ToString());
                        if (intnoofdoctors == 0)
                        {
                            boolvalidation = false;
                        }
                    }
                    else
                    {
                        boolvalidation = false;
                    }
                }
                else
                {
                    boolvalidation = false;
                }
                if (ViewState["DOCTERLISTFC0001"] != null)
                {
                    objDataTable = (DataTable)ViewState["DOCTERLISTFC0001"];

                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        objdatarowdoctorcount = objDataTable.Select("VERIFY_REF = 'DOCTOR' and (status ='IN-PROGRESS' or status='PENDING')");
                        if ((intnoofdoctors == 0) || (objdatarowdoctorcount != null && objdatarowdoctorcount.Length >= intnoofdoctors))
                        {
                            boolvalidation = false;
                        }

                    }
                }
                if (boolvalidation)
                {
                    ViewState["DocterSelectionPopup"] = "DOCTOR";
                    updtpnldocterselection.Visible = true;
                    LoadDepartmentOU();
                    Docterpopupclearvalues();
                    mdlpopupdoctorselection.Show();
                    lbldoctorselectionTilte.Text = "Search Doctor";

                    pnlverselection.Visible = false;
                    pnlDepartmentOU.Visible = true;
                    pnlMCRNumber.Visible = true;
                    pnlDocempno.Visible = true;
                    pnlDoctorname.Visible = true;
                    pnlSecretaryname.Visible = false;
                }
                else
                {
                    CommonFunctions.ShowMessagebootwithsingquotes(this, "Doctor can't be assign . Please check the Report Type Configuration");
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void LkBtnDoc2_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["DocterSelectionPopup"] = "VERIFIER";
                updtpnldocterselection.Visible = true;
                Docterpopupclearvalues();
                LoadDepartmentOU();
                mdlpopupdoctorselection.Show();
                lbldoctorselectionTilte.Text = "Search Verifier";


                if (rbtndoctor.Checked == true)
                {
                    pnlverselection.Visible = true;
                    pnlDepartmentOU.Visible = true;
                    pnlMCRNumber.Visible = true;
                    pnlDocempno.Visible = true;
                    pnlDoctorname.Visible = true;
                    pnlSecretaryname.Visible = false;

                }
                else if (rbtndepsec.Checked == true)
                {
                    pnlDepartmentOU.Visible = true;
                    pnlMCRNumber.Visible = false;
                    pnlDocempno.Visible = false;
                    pnlDoctorname.Visible = false;
                    pnlSecretaryname.Visible = true;
                }
                else if (rbtnhims.Checked == true)
                {
                    pnlDepartmentOU.Visible = false;
                    pnlMCRNumber.Visible = false;
                    pnlDocempno.Visible = true;
                    pnlDoctorname.Visible = true;
                    pnlSecretaryname.Visible = false;
                }
                //rbtndoctor.Checked = true;


            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private DataTable GetDocterList(string stringEXP, string stringtype)
        {
            DataSet objDataSet = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V3";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            try
            {

                stringServiceType = "List2R1V1";
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringEXP, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {

                    if (objDataSet != null && objDataSet.Tables["t2"] != null)
                    {
                        return objDataSet.Tables["t2"];
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                }


            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot(this, objException.Message);

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
            }

            return null;
        }
        private void AssignDoctertoGrid(string stringdocttype, string stringEMPNo, string stringdocname, string stringMCRno, string stringDepname)
        {
            DataRow objDataRow = null;
            DataTable objOrderTable = null;

            DataTable objDataTableCopy = null;
            string stringTMSID = string.Empty;
            string stringTMSOID = string.Empty;
            bool boolrecordexist = false;
            try
            {
                if (ViewState["DOCTERLISTFC0001"] == null)
                {
                    objOrderTable = GetDocterList("and mrasdoc.EMP_NO='DEFAULT'", "DEFAULT");
                    ViewState["DOCTERLISTFC0001"] = objOrderTable;
                }
                else if (ViewState["DOCTERLISTFC0001"] != null)
                {
                    objOrderTable = (DataTable)ViewState["DOCTERLISTFC0001"];
                }
                if (objOrderTable != null)
                {
                    //If Edit Button Clicked then update value

                    if (objOrderTable != null && objOrderTable.Rows.Count > 0)
                    {
                        if (CheckCongIDExistOrNot(stringdocttype, stringEMPNo.Trim().ToString()))
                        {
                            boolrecordexist = true;

                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "ID Already Exist");
                        }
                    }
                    else
                    {
                        boolrecordexist = true;
                    }


                    if (boolrecordexist)
                    {
                        objDataRow = objOrderTable.NewRow();
                        if (objDataRow != null)
                        {
                            objDataRow["BE_ID"] = CommonFunctions.GETBussinessEntity().ToString();
                            objDataRow["EMP_NO"] = stringEMPNo.ToString();
                            objDataRow["NAME"] = stringdocname.ToString();
                            objDataRow["MCR_NO"] = stringMCRno;
                            objDataRow["REJ_REASON"] = "-";
                            objDataRow["REJ_TIME_STAMP"] = "-";
                            objDataRow["REMARKS"] = "-";
                            objDataRow["DEPT_DESC"] = stringDepname;
                            objDataRow["VERIFY_REF"] = stringdocttype;
                            objDataRow["STATUS"] = "PENDING";
                            objDataRow["UNIQUE_ID"] = System.Guid.NewGuid().ToString();
                            objDataRow["DML_INDICATOR"] = "I";
                            objDataRow["reference_2"] = DateTime.Now.ToString();
                            CommonFunctions.AssignAuditLogDetails(ref objDataRow);

                            objOrderTable.Rows.Add(objDataRow);
                        }
                    }

                    ViewState["DOCTERLISTFC0001"] = objOrderTable;
                }
                if (objOrderTable != null && objOrderTable.Rows.Count > 0)
                {
                    objDataTableCopy = objOrderTable.Select("DML_INDICATOR='I' or DML_INDICATOR='U'").CopyToDataTable<DataRow>();

                    BindOrderData(objDataTableCopy);
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
            finally
            {
                objDataRow = null;
                objOrderTable = null;

                objDataTableCopy = null;
                stringTMSID = string.Empty;
                stringTMSOID = string.Empty;
            }
        }

        private static DataTable SetSequenceNUM(DataTable objOrderTable, string stringdocttype)
        {
            DataTable objOrderTablependingcomp = null;

            DataTable objOrderTablenew = null;
            try
            {
                if (objOrderTable != null && objOrderTable.Rows.Count > 0)
                {
                    //objOrderTable.DefaultView.Sort = "COMPLETED_DATE desc,MODIFIED_ON asc";
                    //objOrderTable = objOrderTable.DefaultView.ToTable();

                    objOrderTable.DefaultView.Sort = "reference_2 asc ";
                    objOrderTable = objOrderTable.DefaultView.ToTable();


                    if (objOrderTable.Select("VERIFY_REF = '" + stringdocttype + "' and ( status='COMPLETED'  or status='IN-PROGRESS' or status='PENDING')").Length > 0)
                    {
                        objOrderTablependingcomp = objOrderTable.Select("VERIFY_REF = '" + stringdocttype + "' and (status='COMPLETED'  or status='IN-PROGRESS' or status='PENDING')").CopyToDataTable();
                        if (objOrderTablependingcomp != null && objOrderTablependingcomp.Rows.Count > 0)
                        {
                            for (int intCount = 1; intCount < objOrderTablependingcomp.Rows.Count + 1; intCount++)
                            {
                                objOrderTablependingcomp.Rows[intCount - 1]["SEQ_NO"] = intCount;
                            }
                        }
                    }
                    if (objOrderTable.Select("VERIFY_REF = '" + stringdocttype + "' and status='REJECTED' ").Length > 0)
                    {
                        objOrderTablenew = objOrderTable.Select("VERIFY_REF = '" + stringdocttype + "' and  status='REJECTED'").CopyToDataTable();
                        if (objOrderTablenew != null && objOrderTablenew.Rows.Count > 0)
                        {
                            for (int intCount = 1; intCount < objOrderTablenew.Rows.Count + 1; intCount++)
                            {
                                objOrderTablenew.Rows[intCount - 1]["SEQ_NO"] = 0;
                            }
                        }
                    }
                    objOrderTable = MergeTables(objOrderTable, objOrderTablependingcomp, objOrderTablenew);


                    objOrderTable.DefaultView.Sort = "reference_2 asc ";
                    objOrderTable = objOrderTable.DefaultView.ToTable();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objOrderTablependingcomp = null;
                objOrderTablenew = null;
            }

            return objOrderTable;
        }
        public static DataTable MergeTables(DataTable table1, DataTable table2, DataTable table3)
        {
            DataTable mergedTable = null;
            try
            {
                if (table1 != null)
                {
                    // Clone the structure of table1
                    mergedTable = table1.Clone();

                    if (table2 != null && table2.Rows.Count > 0)
                    {
                        // Merge the data from table2 into mergedTable
                        mergedTable.Merge(table2);
                    }
                    if (table3 != null && table3.Rows.Count > 0)
                    {
                        // Merge the data from table3 into mergedTable
                        mergedTable.Merge(table3);
                    }
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            return mergedTable;
        }
        private void BindOrderData(DataTable table)//fixed
        {
            DataView objDataView = null;
            DataTable objDataTableCloneshort = null;
            DataTable objDataTableDOCTOR = null;
            DataTable objDataTableVERIFIER = null;
            DataTable objOrderTable = null;
            try
            {
                if (table != null && table.Rows.Count > 0)
                {
                    objDataView = table.DefaultView;
                    objDataView.Sort = "reference_2 asc";
                    objDataTableCloneshort = objDataView.ToTable();

                    if (objDataTableCloneshort.Select("VERIFY_REF = 'DOCTOR'").Length > 0)
                    {
                        objDataTableDOCTOR = objDataTableCloneshort.Select("VERIFY_REF = 'DOCTOR'").CopyToDataTable();
                        if (objDataTableDOCTOR != null && objDataTableDOCTOR.Rows.Count > 0)
                        {
                            objDataTableDOCTOR = SetSequenceNUM(objDataTableDOCTOR, "DOCTOR");
                            objDataTableDOCTOR.AcceptChanges();
                            gvassigndoctor.DataSource = objDataTableDOCTOR;
                            gvassigndoctor.DataBind();

                            //for (int intIndexdoc = 0; intIndexdoc < gvassigndoctor.Rows.Count; intIndexdoc++)
                            //{
                            //    stringSno = ((Label)(gvassigndoctor.Rows[intIndexdoc].FindControl("lblseqno"))).Text;
                            //    intIndexdoc01++;
                            //    break;


                            //    int columnIndexToHide = 5;

                            //    if (columnIndexToHide >= 0 && columnIndexToHide < gvassigndoctor.Columns.Count)
                            //    {
                            //        ((TemplateField)gvassigndoctor.Columns[columnIndexToHide]).Visible = false;
                            //    }
                            //}
                        }
                        else
                        {
                            gvassigndoctor.DataSource = null;
                            gvassigndoctor.DataBind();
                        }
                    }

                    if (objDataTableCloneshort.Select("VERIFY_REF = 'VERIFIER'").Length > 0)
                    {
                        objDataTableVERIFIER = objDataTableCloneshort.Select("VERIFY_REF = 'VERIFIER'").CopyToDataTable();
                        if (objDataTableVERIFIER != null && objDataTableVERIFIER.Rows.Count > 0)
                        {
                            objDataTableVERIFIER = SetSequenceNUM(objDataTableVERIFIER, "VERIFIER");
                            gvassignverifier.DataSource = objDataTableVERIFIER;
                            gvassignverifier.DataBind();
                        }
                        else
                        {
                            gvassignverifier.DataSource = null;
                            gvassignverifier.DataBind();
                        }
                    }
                    else
                    {
                        gvassignverifier.DataSource = null;
                        gvassignverifier.DataBind();
                    }
                    objDataTableCloneshort.AcceptChanges();
                }
                else
                {
                    gvassigndoctor.DataSource = null;
                    gvassigndoctor.DataBind();

                    gvassignverifier.DataSource = null;
                    gvassignverifier.DataBind();
                }
                objOrderTable = MergeTables(objDataTableDOCTOR, objDataTableDOCTOR, objDataTableVERIFIER);
                ViewState["DOCTERLISTFC0001"] = objOrderTable;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
            finally
            {
                objDataView = null;
                objDataTableCloneshort = null;
                objDataTableDOCTOR = null;
                objDataTableVERIFIER = null;
                objOrderTable = null;
            }
        }
        private bool CheckCongIDExistOrNot(string stringType, string stringValue)
        {
            DataTable objDataTable = new DataTable();
            try
            {
                if (stringValue.Length > 0)
                {
                    if (stringType == "DOCTOR")
                    {
                        if (ViewState["DOCTERLISTFC0001"] != null)
                        {
                            objDataTable = (DataTable)ViewState["DOCTERLISTFC0001"];

                            if (objDataTable != null && objDataTable.Rows.Count > 0)
                            {
                                if (objDataTable.Select("EMP_NO = '" + stringValue + "' and VERIFY_REF = 'DOCTOR' and (status ='IN-PROGRESS' or status='PENDING' or status='COMPLETED')").Length > 0)
                                    return false;
                                else
                                    return true;

                            }
                        }

                    }
                    else
                    {
                        if (ViewState["DOCTERLISTFC0001"] != null)
                        {
                            objDataTable = (DataTable)ViewState["DOCTERLISTFC0001"];

                            if (objDataTable != null && objDataTable.Rows.Count > 0)
                            {
                                if (objDataTable.Select("EMP_NO = '" + stringValue + "' and VERIFY_REF = 'VERIFIER' and (status ='IN-PROGRESS' or status='PENDING' or status='COMPLETED')").Length > 0)
                                    return false;
                                else
                                    return true;
                            }
                        }

                    }
                }
                else
                    return false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
            finally
            {
                objDataTable = null;
            }
            return false;
        }

        protected void btnDeletedocter_Click(object sender, EventArgs e)
        {
            DataTable objOrderTable = null;
            DataRow[] objDataRow = null;
            DataTable objDataTableCopy = null;
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringuniqID = "";
            string stringdocttype = "";
            DataRow[] objDatarow = null;
            try
            {
                //link button id
                if (sender != null)
                {
                    stringCmdArgument = ((Button)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length > 0)
                        {
                            stringuniqID = stringValues[0];
                            stringdocttype = stringValues[1];

                            if (ViewState["DOCTERLISTFC0001"] != null)
                            {
                                objOrderTable = (DataTable)ViewState["DOCTERLISTFC0001"];
                            }

                            if (objOrderTable != null && objOrderTable.Rows.Count > 0)
                            {
                                objDataRow = objOrderTable.Select("UNIQUE_ID='" + stringuniqID + "' ");
                                if (objDataRow != null && objDataRow.Length > 0)
                                {
                                    if (objDataRow[0]["DML_INDICATOR"].ToString() == "I" || (objDataRow[0]["DML_INDICATOR"].ToString() == "U" && objDataRow[0]["rec_no"].ToString().Length == 0))
                                    {
                                        objDataRow[0].Delete();
                                    }
                                    else
                                    {
                                        objDataRow[0]["DML_INDICATOR"] = "D";
                                    }
                                }
                                ViewState["DOCTERLISTFC0001"] = objOrderTable;

                                objDatarow = objOrderTable.Select("DML_INDICATOR<>'D'");
                                if (objDatarow != null && objDatarow.Length > 0)
                                {
                                    objDataTableCopy = objOrderTable.Select("DML_INDICATOR<>'D'").CopyToDataTable<DataRow>();
                                }
                                else
                                {
                                    objDataTableCopy = null;
                                }
                                BindOrderData(objDataTableCopy);

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
                objDataTableCopy = null;
                stringCmdArgument = "";
                stringValues = null;
                stringuniqID = "";
                stringdocttype = "";
                objDatarow = null;
            }
        }
        protected void rbtndoctor_CheckedChanged(object sender, EventArgs e)
        {
            pnlDepartmentOU.Visible = true;
            pnlMCRNumber.Visible = true;
            pnlDocempno.Visible = true;
            pnlDoctorname.Visible = true;
            pnlSecretaryname.Visible = false;


            ddlDepartmentOUDoctersel.Items.Clear();
            Clearpopvalues();
            LoadDepartmentOU();


            updtpnldocterselection.Visible = true;
            mdlpopupdoctorselection.Show();

            gvlistdoctorselectionpopup.DataSource = null;
            gvlistdoctorselectionpopup.DataBind();
            PopulatePagerdoctorselection(0, intpageIndexdocselection);
        }

        protected void rbtndepsec_CheckedChanged(object sender, EventArgs e)
        {
            pnlDepartmentOU.Visible = true;
            pnlMCRNumber.Visible = false;
            pnlDocempno.Visible = false;
            pnlDoctorname.Visible = false;
            pnlSecretaryname.Visible = true;

            ddlDepartmentOUDoctersel.Items.Clear();
            Clearpopvalues();
            LoadDepartmentOU();

            updtpnldocterselection.Visible = true;
            mdlpopupdoctorselection.Show();

            gvlistdoctorselectionpopup.DataSource = null;
            gvlistdoctorselectionpopup.DataBind();
            PopulatePagerdoctorselection(0, intpageIndexdocselection);
        }

        protected void rbtnhims_CheckedChanged(object sender, EventArgs e)
        {
            pnlDepartmentOU.Visible = false;
            pnlMCRNumber.Visible = false;
            pnlDocempno.Visible = true;
            pnlDoctorname.Visible = true;
            pnlSecretaryname.Visible = false;
            lbldoctorEmployeeNo.Text = "User ID";
            lblDoctorName.Text = "User Name";

            updtpnldocterselection.Visible = true;
            mdlpopupdoctorselection.Show();

            gvlistdoctorselectionpopup.DataSource = null;
            gvlistdoctorselectionpopup.DataBind();
            PopulatePagerdoctorselection(0, intpageIndexdocselection);
        }


        protected void gvlistdoctorselectionpopup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView objdatarowattachments = null;
            DataRow objDataRow = null;
            int columnIndexToHide = 0;
            int columnIndexToHide6 = 0;
            int columnIndexToHide3 = 0;
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Label lblEMP_NOr = (Label)e.Row.FindControl("lblEMP_NOr");
                    Label lblDescription = (Label)e.Row.FindControl("lblDescription");
                    if (lblEMP_NOr != null)
                    {
                        if (rbtndepsec.Checked == true)
                        {
                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                            {
                                lblEMP_NOr.Text = "Verifier ID";
                            }

                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                            {
                                lblEMP_NOr.Text = "Secretary ADID";
                            }

                        }
                        else if (rbtndoctor.Checked == true)
                        {
                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                            {
                                lblEMP_NOr.Text = "Verifier ID";
                            }

                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                            {
                                lblEMP_NOr.Text = "Doctor Employee Number";
                            }
                        }
                        else if (rbtnhims.Checked == true)
                        {
                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                            {
                                lblEMP_NOr.Text = "Verifier ID";
                            }

                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                            {
                                lblEMP_NOr.Text = "User ID";
                            }
                        }
                    }
                    if (lblDescription != null)
                    {

                        if (rbtndepsec.Checked == true)
                        {
                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                            {
                                lblDescription.Text = "Verifier Name";
                            }

                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                            {
                                lblDescription.Text = "Secretary Name";
                            }
                        }
                        else if (rbtndoctor.Checked == true)
                        {
                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                            {
                                lblDescription.Text = "Verifier Name";
                            }

                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                            {
                                lblDescription.Text = "Doctor Name";
                            }
                        }
                        else if (rbtnhims.Checked == true)
                        {
                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "VERIFIER")
                            {
                                lblDescription.Text = "Verifier Name";
                            }

                            if (ViewState["DocterSelectionPopup"] != null && ViewState["DocterSelectionPopup"].ToString() == "DOCTOR")
                            {
                                lblDescription.Text = "User Name";
                            }
                        }
                    }
                }

                GridViewRow objGridViewRow = e.Row;
                string stringSort = string.Empty;
                if (objGridViewRow.DataItem == null) { return; }

                objdatarowattachments = ((DataRowView)objGridViewRow.DataItem);
                objDataRow = objdatarowattachments.Row;
                if (objDataRow != null)
                {
                    if (rbtndepsec.Checked == true)
                    {
                        columnIndexToHide = 2;

                        if (columnIndexToHide >= 0 && columnIndexToHide < gvlistdoctorselectionpopup.Columns.Count)
                        {
                            ((TemplateField)gvlistdoctorselectionpopup.Columns[columnIndexToHide]).Visible = false;
                        }
                        columnIndexToHide6 = 3;

                        if (columnIndexToHide6 >= 0 && columnIndexToHide6 < gvlistdoctorselectionpopup.Columns.Count)
                        {
                            ((TemplateField)gvlistdoctorselectionpopup.Columns[columnIndexToHide6]).Visible = true;
                        }
                    }
                    else if (rbtndoctor.Checked == true)
                    {
                        columnIndexToHide = 2;

                        if (columnIndexToHide >= 0 && columnIndexToHide < gvlistdoctorselectionpopup.Columns.Count)
                        {
                            ((TemplateField)gvlistdoctorselectionpopup.Columns[columnIndexToHide]).Visible = true;
                        }

                        columnIndexToHide6 = 3;

                        if (columnIndexToHide6 >= 0 && columnIndexToHide6 < gvlistdoctorselectionpopup.Columns.Count)
                        {
                            ((TemplateField)gvlistdoctorselectionpopup.Columns[columnIndexToHide6]).Visible = true;
                        }
                    }
                    else if (rbtnhims.Checked == true)
                    {
                        columnIndexToHide = 2;

                        if (columnIndexToHide >= 0 && columnIndexToHide < gvlistdoctorselectionpopup.Columns.Count)
                        {
                            ((TemplateField)gvlistdoctorselectionpopup.Columns[columnIndexToHide]).Visible = false;
                        }

                        columnIndexToHide3 = 3;

                        if (columnIndexToHide3 >= 0 && columnIndexToHide3 < gvlistdoctorselectionpopup.Columns.Count)
                        {
                            ((TemplateField)gvlistdoctorselectionpopup.Columns[columnIndexToHide3]).Visible = false;
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
                objdatarowattachments = null;
                objDataRow = null;
                columnIndexToHide = 0;
                columnIndexToHide6 = 0;
                columnIndexToHide3 = 0;
            }
        }
        private void LoadDepartmentOU()//fixed
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0010R1V1";
            string stringOrderBy = "short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue; string stringbeid = CommonFunctions.GETBussinessEntity();
            DataTable objdatatableLoadDepartmentOU = null;
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                ddlDepartmentOU.Items.Clear();
                ddlDepartmentOUDoctersel.Items.Clear();
                ddlDepartmentOU.DataSource = null;
                ddlDepartmentOU.DataBind();
                ddlDepartmentOUDoctersel.DataSource = null;
                ddlDepartmentOUDoctersel.DataBind();
                stringcondition = " And delmark= 'N'";

                stringServiceType = "List1R1V1";
                if (Session["SSNLOADDEPARTMENTOU"] != null)
                {
                    objdatatableLoadDepartmentOU = (DataTable)Session["SSNLOADDEPARTMENTOU"];
                }
                if ((objdatatableLoadDepartmentOU == null) || (objdatatableLoadDepartmentOU != null && objdatatableLoadDepartmentOU.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadDepartmentOU = objDatasetResult.Tables["t1"];
                            Session["SSNLOADDEPARTMENTOU"] = objdatatableLoadDepartmentOU;
                        }

                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadDepartmentOU != null && objdatatableLoadDepartmentOU.Rows.Count > 0)
                {
                    ddlDepartmentOU.DataTextField = "short_name";
                    ddlDepartmentOU.DataValueField = "DEPT_ID";
                    ddlDepartmentOU.DataSource = objdatatableLoadDepartmentOU;
                    ddlDepartmentOU.DataBind();
                    ddlDepartmentOU.Items.Insert(0, new ListItem("", ""));

                    ddlDepartmentOUDoctersel.DataTextField = "short_name";
                    ddlDepartmentOUDoctersel.DataValueField = "DEPT_ID";
                    ddlDepartmentOUDoctersel.DataSource = objdatatableLoadDepartmentOU;
                    ddlDepartmentOUDoctersel.DataBind();
                    ddlDepartmentOUDoctersel.Items.Insert(0, new ListItem("", ""));
                    //Clearpopvalues();
                }
                else
                {
                    ddlDepartmentOU.DataSource = null;
                    ddlDepartmentOU.DataBind();

                    ddlDepartmentOUDoctersel.DataSource = null;
                    ddlDepartmentOUDoctersel.DataBind();
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
                stringbeid = "";
                objdatatableLoadDepartmentOU = null;
                stringcondition = "";
                stringServiceType = "";
            }
        }

        private void Clearpopvalues()
        {
            try
            {
                txtSecretaryname.Text = "";
                txtdocempnumdocselection.Text = "";
                ddlDepartmentOUDoctersel.ClearSelection();
                txtMCRNumberdocselection.Text = "";
                txtDoctorNamedocselection.Text = "";
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void ddlDepartmentOU_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stringApplicationStatus = "";
            try
            {
                if (ddlApplicationStatus.SelectedValue != null && ddlApplicationStatus.SelectedItem.Value != "")
                {
                    stringApplicationStatus = ddlApplicationStatus.SelectedValue.ToString();
                }

                if (stringApplicationStatus.Length > 0 && stringApplicationStatus != "BATCHREQUESTBYPATIENT" && txtHRN.Text.Trim().Length == 0)
                {
                    CommonFunctions.ShowMessageboot(this, "Please select a patient first, before proceed to Department OU selection!");
                    TABActive(0);
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringApplicationStatus = "";
            }
        }

        private bool SaveAssignDocteorandVerifier(string stringNewReqID, string stringTYPE)//fix
        {
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V3";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatable = null;
            DataTable objDOCTERLISTable = null;
            DataRow objDataRow = null;
            DataView objDataView = null;
            bool booldoctor = false;
            bool boolsuccesss = false;
            bool boolUpdatestatusdoctor = false;
            bool boolUpdatestatusverifier = false;
            string stringServiceType1 = "";
            string stringexp = "";
            string stringtype = "";
            string stringDML_INDICATOR = "";
            string stringServiceType2 = "";
            string stringformid1 = "";
            string stringMessage = "";
            string stringRequestID = "";
            try
            {
                if (ViewState["DOCTERLISTFC0001"] != null)
                {
                    stringServiceType1 = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count == 0)
                        {
                            objDOCTERLISTable = (DataTable)ViewState["DOCTERLISTFC0001"];
                            objDataView = objDOCTERLISTable.DefaultView;
                            objDataView.Sort = "VERIFY_REF asc";
                            objDOCTERLISTable = objDataView.ToTable();

                            if (objDOCTERLISTable != null && objDOCTERLISTable.Rows.Count > 0)
                            {
                                for (int intIndex = 0; intIndex < objDOCTERLISTable.Rows.Count; intIndex++)
                                {
                                    objDataRow = objDatasetResult.Tables["t2"].NewRow();

                                    objDataRow["BE_ID"] = objDOCTERLISTable.Rows[intIndex]["BE_ID"].ToString();
                                    objDataRow["REQUEST_ID"] = stringNewReqID;
                                    //objDataRow["DOC_SEQ_ID"] = System.Guid.NewGuid().ToString().ToUpper();
                                    objDataRow["DEPT_ID"] = objDOCTERLISTable.Rows[intIndex]["DEPT_ID"].ToString();
                                    objDataRow["EMP_NO"] = objDOCTERLISTable.Rows[intIndex]["EMP_NO"].ToString();
                                    objDataRow["NAME"] = objDOCTERLISTable.Rows[intIndex]["NAME"].ToString();
                                    objDataRow["MCR_NO"] = objDOCTERLISTable.Rows[intIndex]["MCR_NO"].ToString();
                                    objDataRow["REJ_REASON"] = objDOCTERLISTable.Rows[intIndex]["REJ_REASON"].ToString();
                                    objDataRow["REJ_TIME_STAMP"] = objDOCTERLISTable.Rows[intIndex]["REJ_TIME_STAMP"].ToString();
                                    objDataRow["REMARKS"] = objDOCTERLISTable.Rows[intIndex]["REMARKS"].ToString();
                                    objDataRow["STATUS"] = objDOCTERLISTable.Rows[intIndex]["STATUS"].ToString();
                                    stringtype = objDOCTERLISTable.Rows[intIndex]["VERIFY_REF"].ToString();
                                    objDataRow["VERIFY_REF"] = stringtype;
                                    objDataRow["UNIQUE_ID"] = objDOCTERLISTable.Rows[intIndex]["UNIQUE_ID"].ToString();
                                    objDataRow["reference_2"] = objDOCTERLISTable.Rows[intIndex]["reference_2"].ToString();
                                    objDataRow["COMPLETED_DATE"] = DateTime.Now;
                                    objDataRow["DML_INDICATOR"] = objDOCTERLISTable.Rows[intIndex]["DML_INDICATOR"].ToString();

                                    objDataRow["CREATED_AT"] = objDOCTERLISTable.Rows[intIndex]["CREATED_AT"].ToString();
                                    objDataRow["CREATED_BY"] = objDOCTERLISTable.Rows[intIndex]["CREATED_BY"].ToString();
                                    objDataRow["CREATED_ON"] = objDOCTERLISTable.Rows[intIndex]["CREATED_ON"];
                                    objDataRow["MODIFIED_AT"] = objDOCTERLISTable.Rows[intIndex]["MODIFIED_AT"].ToString();
                                    objDataRow["MODIFIED_BY"] = objDOCTERLISTable.Rows[intIndex]["MODIFIED_BY"].ToString();
                                    objDataRow["MODIFIED_ON"] = objDOCTERLISTable.Rows[intIndex]["MODIFIED_ON"];
                                    objDataRow["SEQ_NO"] = objDOCTERLISTable.Rows[intIndex]["SEQ_NO"];
                                    string stringDMLIndicator = objDOCTERLISTable.Rows[intIndex]["DML_INDICATOR"].ToString();
                                    if (stringDMLIndicator.ToString() == "I")
                                    {
                                        objDataRow["DOC_SEQ_ID"] = System.Guid.NewGuid().ToString().ToUpper();
                                    }
                                    else if (stringDMLIndicator.ToString() == "U")
                                    {
                                        objDataRow["DOC_SEQ_ID"] = objDOCTERLISTable.Rows[intIndex]["DOC_SEQ_ID"].ToString();
                                    }
                                    if (stringtype == "DOCTOR")
                                    {
                                        booldoctor = true;
                                    }
                                    else
                                    {
                                        //boolverifier = true;
                                    }
                                    //if (stringtype == "DOCTOR")
                                    //{
                                    //    booldoctor = true;
                                    //    for (int intIndexdoc = intIndexdoc01; intIndexdoc < gvassigndoctor.Rows.Count; intIndexdoc++)
                                    //    {
                                    //        stringSno = ((Label)(gvassigndoctor.Rows[intIndexdoc].FindControl("lblseqno"))).Text;
                                    //        intIndexdoc01++;
                                    //        break;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    boolverifier = true;
                                    //    for (int intIndexver = intIndexver01; intIndexver < gvassignverifier.Rows.Count; intIndexver++)
                                    //    {
                                    //        stringSno = ((Label)(gvassignverifier.Rows[intIndexver].FindControl("lblseqnoverifier"))).Text;
                                    //        intIndexver01++;
                                    //        break;
                                    //    }
                                    //}
                                    //objDataRow["SEQ_NO"] = Convert.ToInt32(stringSno);


                                    objDatasetResult.Tables["t2"].Rows.Add(objDataRow);
                                }
                            }

                            if (objDatasetResult != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                            {
                                objDatasetResult.Tables["t2"].AcceptChanges();
                                for (int intIndex = 0; intIndex < objDatasetResult.Tables["t2"].Rows.Count; intIndex++)
                                {
                                    stringDML_INDICATOR = objDatasetResult.Tables["t2"].Rows[intIndex]["DML_INDICATOR"].ToString();

                                    if (stringDML_INDICATOR == "D")
                                    {
                                        objDatasetResult.Tables["t2"].Rows[intIndex].Delete();
                                    }
                                    else if (stringDML_INDICATOR == "I")
                                    {
                                        objDatasetResult.Tables["t2"].Rows[intIndex].SetAdded();
                                    }
                                    else if (stringDML_INDICATOR == "U")
                                    {
                                        objDatasetResult.Tables["t2"].Rows[intIndex].SetModified();
                                    }

                                }

                                objDatasetResult = objDatasetResult.GetChanges();
                                stringServiceType2 = "OperationServiceDML";
                                stringformid1 = "FC0001R1V3";
                                objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType2, objDatasetResult.GetChanges(), stringformid1, out int intErrorCount, out string[] stringOutputResult1);
                                if (intErrorCount == 0)
                                {
                                    
                                    if ((txtMRStatus.Text.ToUpper() == "PENDING ASSIGNED" && booldoctor == true))
                                    {
                                        if (UpdateProcessStatus(txtMRStatus.Text.Trim().ToUpper()))
                                        {
                                            boolsuccesss = true;
                                        }
                                        else
                                        {
                                            boolsuccesss = false;
                                        }
                                    }
                                    if ((txtMRStatus.Text.ToUpper() == "PENDING REPORT" && stringtype.ToUpper() == "DOCTOR") || (txtMRStatus.Text.ToUpper() == "PENDING REPORT" && stringtype.ToUpper() == "VERIFIER"))
                                    {
                                        objdatatable = LoadDocterandVerifiersCOnfirm(txtRequestNo.Text.Trim());
                                        if (objdatatable != null && objdatatable.Rows.Count > 0 && objdatatable.Select("VERIFY_REF ='VERIFIER'").Length > 0)
                                        {
                                            int intoverallveridierCompleted = 0;
                                            if (objdatatable.Select("VERIFY_REF = 'VERIFIER' and (STATUS = 'IN-PROGRESS' or STATUS = 'PENDING')").Length > 0)
                                            {
                                                intoverallveridierCompleted = Convert.ToInt32(objdatatable.Select("VERIFY_REF ='VERIFIER'  and (STATUS = 'IN-PROGRESS' or STATUS = 'PENDING')").Length.ToString());
                                            }
                                            if (intoverallveridierCompleted == 0)
                                            {
                                                boolUpdatestatusverifier = true;
                                            }
                                        }
                                        else
                                        {
                                            boolUpdatestatusverifier = true;
                                        }

                                        if (objdatatable != null && objdatatable.Rows.Count > 0 && objdatatable.Select("VERIFY_REF ='DOCTOR'").Length > 0)
                                        {
                                            int intoverallDOCTORCompleted = 0;
                                            if (objdatatable.Select("VERIFY_REF = 'DOCTOR' and (STATUS = 'IN-PROGRESS' or STATUS = 'PENDING')").Length > 0)
                                            {
                                                intoverallDOCTORCompleted = Convert.ToInt32(objdatatable.Select("VERIFY_REF ='DOCTOR' and (STATUS = 'IN-PROGRESS' or STATUS = 'PENDING')").Length.ToString());
                                            }
                                            if (intoverallDOCTORCompleted == 0)
                                            {
                                                boolUpdatestatusdoctor = true;
                                            }

                                        }
                                        else
                                        {
                                            boolUpdatestatusdoctor = true;
                                        } 
                                    }
                                    if (boolUpdatestatusdoctor == true && boolUpdatestatusverifier == true)
                                    {
                                        if (UpdateProcessStatus(txtMRStatus.Text.Trim().ToUpper()))
                                        {
                                            boolsuccesss = true;
                                        }
                                        else
                                        {
                                            boolsuccesss = false;
                                        }
                                    }
                                    if ((txtMRStatus.Text.ToUpper() == "PENDING RELEASE TO HIMS") && Session["ADD_ATTACHMENTS"] != null)
                                    {
                                        ViewState["DOCTORCHANGES"] = "HIMS";
                                        CHECKPendingRelizeToHIMS(); 
                                    }
                                    if (boolsuccesss)
                                    {
                                        stringRequestID = txtRequestNo.Text.Trim().ToUpper();
                                        Session["REQUESTID_FDS001R1V1"] = stringRequestID;
                                        Session["REQUESTID_COMPLETED"] = "COMPLETED";
                                        stringMessage = "Record Completed Successfully";
                                        ShowMessageandReloadPage(this, stringMessage, "FDS001R1V1.aspx");
                                    }
                                    return true;
                                }
                                else
                                {
                                    Errorpopup(stringOutputResult1);
                                    return false;

                                }
                            }
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
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
                interrorcount = 0;
                intTotalRecord = 0;
                objDatasetResult = null;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                objDOCTERLISTable = null;
                objDataRow = null;
                objDataView = null;
                stringServiceType1 = "";
                stringexp = "";
                stringtype = "";
                stringDML_INDICATOR = "";
                stringServiceType2 = "";
                stringformid1 = "";
                stringMessage = "";
                stringRequestID = "";
            }
        }
        protected void btnupdateHIMS_Click(object sender, EventArgs e)
        {
            bool boolsuccesss = false;
            string stringRequestID = "";
            try
            {
                if (UpdateProcessStatus(txtMRStatus.Text.Trim().ToUpper()))
                {
                    boolsuccesss = true;
                }
                else
                {
                    boolsuccesss = false;
                }
                if (boolsuccesss)
                {
                    stringRequestID = txtRequestNo.Text.Trim().ToUpper();
                    Session["REQUESTID_FDS001R1V1"] = stringRequestID;
                    Session["REQUESTID_COMPLETED"] = "COMPLETED";
                    string stringMessage = "Record Updated Successfully";
                    ShowMessageandReloadPage(this, stringMessage, "FDS001R1V1.aspx");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        protected void btncancelHIMS_Click(object sender, EventArgs e)
        {
            ModelpopuperrorHIMS.Hide();
            pnlHIMS.Visible = false;
        }
        protected void CHECKPendingRelizeToHIMS()
        {
            DataTable objDataTableAddReports = null;
            DataTable objDataTableCompleteMedicalReport = null;
            DataRow[] objdatarowCompleteMedicalReport = null;
            string stringprocessname = "";
            bool boolnotaccatched = false; 
            bool boolsuccesss = false;
            string stringRequestID = "";
            try
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
                            boolnotaccatched = true;
                        }
                    }
                }
                if (!boolnotaccatched )
                {
                    lblupdateprocesscontent.Text = "There no Medical Report being attached. Do you want to proceed ?";
                    ViewState["NEXTPROCESSNAME"] = stringprocessname;
                    txtProcessCompletedRemarks.Text = "";
                    ModelpopuperrorHIMS.Show();
                    UpdatePanelModalHIMS.Visible = true;
                }
                else if (boolnotaccatched)
                {
                    if (UpdateProcessStatus(txtMRStatus.Text.Trim().ToUpper()))
                    {
                        boolsuccesss = true;
                    }
                    else
                    {
                        boolsuccesss = false;
                    }
                    if (boolsuccesss)
                    {
                        stringRequestID = txtRequestNo.Text.Trim().ToUpper();
                        Session["REQUESTID_FDS001R1V1"] = stringRequestID;
                        Session["REQUESTID_COMPLETED"] = "COMPLETED";
                        string stringMessage = "Record Updated Successfully";
                        ShowMessageandReloadPage(this, stringMessage, "FDS001R1V1.aspx");
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
                stringprocessname = "";
            }
        }

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
     
        private void LoadDocterandVerifiers(string stringRequestID, string stringTYPE)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V3";
            string stringOrderBy = "mrasdoc.COMPLETED_DATE desc,mrasdoc.MODIFIED_ON asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
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
                            objDataTable = objDatasetResult.Tables["t2"];
                            if (objDataTable != null && objDataTable.Rows.Count > 0)
                            {
                                foreach (DataRow row in objDataTable.Rows)
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


                            if (ddlApplicationStatus.Items.FindByValue("ASSIGN_DOCTOR_VERIFIER") != null)
                            {
                                ddlApplicationStatus.ClearSelection();
                                ddlApplicationStatus.Items.FindByValue("ASSIGN_DOCTOR_VERIFIER").Selected = true;
                            }
                            if (txtMRStatus.Text == "PENDING ASSIGNED")
                            {
                                ddlApplicationStatus.Enabled = true;
                                btnConfirm.Enabled = true;
                            }
                            else
                            {
                                ddlApplicationStatus.Enabled = false;
                                btnConfirm.Enabled = false;
                            }
                            btnConfirm_Click(null, null);

                        }
                        ViewState["DOCTERLISTFC0001"] = objDataTable;

                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                        ViewState["DOCTERLISTFC0001"] = null;
                    }
                }

                if (objDataTable != null && objDataTable.Rows.Count > 0)
                {
                    BindOrderData(objDataTable);
                }
                else
                {
                    ViewState["DOCTERLISTFC0001"] = null;
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
                stringBoID = "";
                stringServiceType = "";
                stringExpression = "";
            }
        }

        private DataTable LoadDocterandVerifiersCOnfirm(string stringRequestID)//fix
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
                            return objDatasetResult.Tables["t2"];
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                return null;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return null;
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
                intToRecord = int.MaxValue;
                stringBoID = "";
                stringServiceType = "";
                stringExpression = "";
            }
        }


        #endregion

        //for Docter selection
        private void PopulatePagerdoctorselection(int recordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdPagingDocsel);
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
                Repeater5.DataSource = pages;
                Repeater5.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        protected void lnkPagedoctorselectionPopup_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["intpageIndex1"] != null)
                {
                    intpageIndexdocselection = Convert.ToInt32(Session["intpageIndex1"].ToString());
                    Session["intpageIndex"] = intpageIndexdocselection;
                }
                else
                {
                    intpageIndexdocselection = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndexdocselection != 0)
                    {
                        Session["intpageIndex"] = intpageIndexdocselection;
                    }
                }

                if (intpageIndexdocselection == 1)
                {
                    intintrecFromdocselection = 0;
                    intrecTodocselection = CommonFunctions.GridViewPagesize(stringformIdPagingDocsel);
                }
                else
                {
                    int intrecFrom1 = (intpageIndexdocselection * intrecTodocselection) - intrecTodocselection;
                    intintrecFromdocselection = intrecFrom1 + 1;
                    intrecTodocselection = intrecFrom1 + CommonFunctions.GridViewPagesize(stringformIdPagingDocsel);
                }

                btndoctorselectionsearch_Click(null, null);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string stringapptype = "";
            try
            {
                if (txtHRN.Text.Trim().Length > 0)
                {
                    if (ddlApplicationStatus.SelectedItem != null && ddlApplicationStatus.SelectedValue.Length > 0)
                    {
                        stringapptype = ddlApplicationStatus.SelectedValue.ToString().ToUpper();

                        if (stringapptype == "ASSIGN_DOCTOR_VERIFIER")
                        {
                            pnlRerouteRequest.Visible = false;
                            pnlRejectRequest.Visible = false;
                            pnlAssignDoctorandverifier.Visible = true;
                        }
                        else if (stringapptype == "REROUTE_REQUEST")
                        {
                            pnlRerouteRequest.Visible = true;
                            pnlRejectRequest.Visible = false;
                            pnlAssignDoctorandverifier.Visible = false;
                        }
                        else if (stringapptype == "REJECT_REQUEST")
                        {
                            pnlRerouteRequest.Visible = false;
                            pnlRejectRequest.Visible = true;
                            pnlAssignDoctorandverifier.Visible = false;
                        }
                    }
                }
                else
                {
                    TABActive(1);
                    CommonFunctions.ShowMessageboot(this, "Please fill patient details");
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }



        protected void btnCompletedocter_Click(object sender, EventArgs e)
        {
            DataTable objOrderTable = null;
            DataRow[] objDataRow = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V3";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataRow objDataRowsave = null;
            string stringtype = "";
            string stringCmdArgument = "";
            string stringuniqID = "";
            string stringreqID = "";
            string stringdocttype = "";
            DataTable objdatatable = null;
            string[] stringValues = null;
            string stringServiceType1 = "";
            string stringexp = "";
            string stringServiceType2 = "";
            string stringformid1 = "";
            string stringDML_INDICATOR = "";
            string stringRequestID = "";
            string stringMessage = "";
            bool boolverification = true;
            DataRow[] objDataRowverification = null;
            try
            {
                //link button id
                if (sender != null)
                {
                    stringCmdArgument = ((Button)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length > 0)
                        {
                            stringuniqID = stringValues[0];
                            stringdocttype = stringValues[1];
                            stringreqID = stringValues[2];

                            if (ViewState["DOCTERLISTFC0001"] != null)
                            {
                                objOrderTable = (DataTable)ViewState["DOCTERLISTFC0001"];
                            }

                            if (objOrderTable != null && objOrderTable.Rows.Count > 0)
                            {
                                if (stringdocttype.ToUpper() == "VERIFIER")
                                {
                                    objDataRowverification = objOrderTable.Select("VERIFY_REF = 'DOCTOR' and status='IN-PROGRESS' ");
                                    if (objDataRowverification != null && objDataRowverification.Length > 0)
                                    {
                                        boolverification = false;
                                    }
                                }
                                if (boolverification)
                                {
                                    objDataRow = objOrderTable.Select("UNIQUE_ID='" + stringuniqID + "' ");
                                    if (objDataRow != null && objDataRow.Length > 0)
                                    {
                                        stringServiceType1 = "DEFAULT";
                                        stringexp = "";
                                        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                                        if (interrorcount == 0)
                                        {
                                            if (objDatasetResult != null && objDatasetResult.Tables["t2"].Rows.Count == 0)
                                            {
                                                objDataRowsave = objDatasetResult.Tables["t2"].NewRow();

                                                objDataRowsave["BE_ID"] = objDataRow[0]["BE_ID"].ToString();
                                                objDataRowsave["REQUEST_ID"] = objDataRow[0]["REQUEST_ID"].ToString();
                                                objDataRowsave["DEPT_ID"] = objDataRow[0]["DEPT_ID"].ToString();
                                                objDataRowsave["EMP_NO"] = objDataRow[0]["EMP_NO"].ToString();
                                                objDataRowsave["NAME"] = objDataRow[0]["NAME"].ToString();
                                                objDataRowsave["MCR_NO"] = objDataRow[0]["MCR_NO"].ToString();
                                                objDataRowsave["REJ_REASON"] = objDataRow[0]["REJ_REASON"].ToString();
                                                objDataRowsave["REJ_TIME_STAMP"] = objDataRow[0]["REJ_TIME_STAMP"].ToString();
                                                objDataRowsave["REMARKS"] = objDataRow[0]["REMARKS"].ToString();
                                                objDataRowsave["STATUS"] = "COMPLETED";
                                                objDataRowsave["DOC_SEQ_ID"] = objDataRow[0]["DOC_SEQ_ID"].ToString();
                                                stringtype = objDataRow[0]["VERIFY_REF"].ToString();
                                                objDataRowsave["VERIFY_REF"] = stringtype;
                                                objDataRowsave["UNIQUE_ID"] = objDataRow[0]["UNIQUE_ID"].ToString();
                                                objDataRowsave["COMPLETED_DATE"] = DateTime.Now;
                                                objDataRowsave["SEQ_NO"] = objDataRow[0]["SEQ_NO"].ToString();
                                                objDataRowsave["reference_2"] = objDataRow[0]["reference_2"].ToString();
                                                objDataRowsave["DML_INDICATOR"] = "U";
                                                if (HttpContext.Current.Session["stringComputerName"] != null)
                                                    objDataRowsave["MODIFIED_AT"] = HttpContext.Current.Session["stringComputerName"].ToString();
                                                if (HttpContext.Current.Session["G11EOSUser_Name"] != null)
                                                    objDataRowsave["MODIFIED_BY"] = HttpContext.Current.Session["G11EOSUser_Name"].ToString();
                                                objDataRowsave["MODIFIED_ON"] = DateTime.Now;


                                                objDatasetResult.Tables["t2"].Rows.Add(objDataRowsave);

                                                if (objDatasetResult != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                                                {
                                                    objDatasetResult.Tables["t2"].AcceptChanges();
                                                    for (int intIndex = 0; intIndex < objDatasetResult.Tables["t2"].Rows.Count; intIndex++)
                                                    {
                                                        stringDML_INDICATOR = objDatasetResult.Tables["t2"].Rows[intIndex]["DML_INDICATOR"].ToString();

                                                        if (stringDML_INDICATOR == "U")
                                                        {
                                                            objDatasetResult.Tables["t2"].Rows[intIndex].SetModified();
                                                        }

                                                    }

                                                    objDatasetResult = objDatasetResult.GetChanges();
                                                    stringServiceType2 = "OperationServiceDML";
                                                    stringformid1 = "FC0001R1V3";
                                                    objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType2, objDatasetResult.GetChanges(), stringformid1, out int intErrorCount, out string[] stringOutputResult1);
                                                    if (intErrorCount == 0)
                                                    {
                                                        bool boolsuccesss = true;
                                                        bool boolUpdatestatusDOCTOR = false;
                                                        bool boolUpdatestatusverifierR = false;

                                                        if (boolsuccesss)
                                                        {
                                                            ViewState["DOCTORCHANGES"] = "ADDED";
                                                            stringRequestID = txtRequestNo.Text.Trim().ToUpper();
                                                            Session["REQUESTID_FDS001R1V1"] = stringRequestID;
                                                            Session["REQUESTID_COMPLETED"] = "COMPLETED";
                                                            stringMessage = "Record Completed Successfully";
                                                            ShowMessageandReloadPage(this, stringMessage, "FDS001R1V1.aspx");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Session["REQUESTID_COMPLETED"] = null;
                                                        Errorpopup(stringOutputResult1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    CommonFunctions.ShowMessageboot(this, "Please complete the doctor to update the verification status");
                                }
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
                interrorcount = 0;
                intTotalRecord = 0;
                objDatasetResult = null;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = int.MaxValue;
                objDataRowsave = null;
                stringtype = "";
                stringCmdArgument = "";
                stringuniqID = "";
                stringreqID = "";
                stringdocttype = "";
                objdatatable = null;
                stringValues = null;
                stringServiceType1 = "";
                stringexp = "";
                stringServiceType2 = "";
                stringformid1 = "";
                stringDML_INDICATOR = "";
                stringRequestID = "";
                stringMessage = "";
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


                if (stringTransSattus != null && stringTransSattus.Trim().Length > 0)
                {
                    stringInputs[0] = txtRequestNo.Text.Trim().ToUpper();
                    stringInputs[1] = "";
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
                stringformid = "";
            }
            return boolStatus;
        }
        private bool UpdateProcessStatusInREVERSEOrder(string stringTransSattus)//fix
        {
            bool boolStatus = false;
            DataRow objdatarow = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FDS001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            try
            {
                stringServiceType = "DEFAULT";
                stringexp = "";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count == 0)
                    {
                        objdatarow = objDatasetResult.Tables["t2"].NewRow();
                        objdatarow["be_id"] = stringbeid;
                        objdatarow["remarks_id"] = System.Guid.NewGuid().ToString().ToUpper();
                        objdatarow["Request_ID"] = txtRequestNo.Text.Trim();
                        objdatarow["remarks_desc"] = txtRejectReason.Text.Trim();
                        objdatarow["Status"] = txtMRStatus.Text.Trim();
                        if (stringTransSattus == "DOCTOR")
                        {
                            objdatarow["Trans_type"] = "DOCTOR";
                        }
                        else
                        {
                            objdatarow["Trans_type"] = "";
                        }
                        CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                        objDatasetResult.Tables["t2"].Rows.Add(objdatarow);
                        objDatasetResult.Tables["t2"].Rows[0].RowState.ToString();

                        objDatasetResult = objDatasetResult.GetChanges();
                        stringServiceType = "OperationServiceDML";
                        objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                        if (intErrorCount == 0)
                        {
                            return true;
                            //CommonFunctions.ShowMessageboot(this, "Recall Completed Successfully");
                            //   string stringRequestID = txtReqNo.Text.Trim().ToUpper();
                            // LoadData(stringRequestID, "NONLOAD", "");
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
                stringbeid = "";
                stringServiceType = "";
                stringexp = "";
            }
            return boolStatus;
        }
        protected void btnRejectdocter_Click(object sender, EventArgs e)
        {
            txtsno.Text = "";
            string stringCmdArgument = "";
            string stringuniqID = "";
            string stringdocttype = "";
            string stringseqno = "";
            string[] stringValues = null;
            try
            {
                stringCmdArgument = ((Button)sender).CommandArgument;
                if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                {
                    stringValues = stringCmdArgument.Split(',');
                    if (stringValues != null && stringValues.Length > 0)
                    {
                        stringuniqID = stringValues[0];
                        stringdocttype = stringValues[1];
                        stringseqno = stringValues[2];
                        txtProcessCompletedRemarks.Text = "";
                        pnlupdateremarksandprocess.Visible = true;
                        UpdatePanelModal6success.Visible = true;
                        Modelpopuperrorsuccess.Show();
                        if (stringdocttype == "VERIFIER")
                        {
                            lblreject.Text = "Verifier Reject";
                        }
                        else
                        {
                            lblreject.Text = "Doctor Reject";
                        }
                        txtsno.Text = stringseqno;
                        hdfuniqid.Value = stringuniqID;
                    }
                    else
                    {
                        hdfuniqid.Value = "";
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
                stringuniqID = "";
                stringdocttype = "";
                stringseqno = "";
                stringValues = null;
            }
        }
        protected void btnConfirmprocessStatus_Click(object sender, EventArgs e)
        {
            DataTable objOrderTable = null;
            DataRow[] objDataRow = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V3";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataRow objDataRowsave = null;
            string stringServiceType1 = "";
            string stringtype = "";
            string stringexp = "";
            string stringDML_INDICATOR = "";
            string stringRequestID = "";
            int intoverallDOCTORCompleted = 0;
            int intoverallDOCTORREJECTED = 0;
            string stringServiceType2 = "";
            string stringformid1 = "";
            string stringRequestID1 = "";
            string stringMessage = "";
            try
            {
                pnlupdateremarksandprocess.Visible = false;
                UpdatePanelModal6success.Visible = false;
                Modelpopuperrorsuccess.Hide();
                //link button id
                if (hdfuniqid != null && hdfuniqid.Value.Length > 0)
                {
                    if (ViewState["DOCTERLISTFC0001"] != null)
                    {
                        objOrderTable = (DataTable)ViewState["DOCTERLISTFC0001"];
                    }

                    if (objOrderTable != null && objOrderTable.Rows.Count > 0)
                    {
                        objDataRow = objOrderTable.Select("UNIQUE_ID='" + hdfuniqid.Value.ToString() + "' ");
                        if (objDataRow != null && objDataRow.Length > 0)
                        {
                            stringServiceType1 = "DEFAULT";
                            stringexp = "";
                            objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                            if (interrorcount == 0)
                            {
                                if (objDatasetResult != null && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count == 0)
                                {
                                    objDataRowsave = objDatasetResult.Tables["t2"].NewRow();

                                    objDataRowsave["BE_ID"] = objDataRow[0]["BE_ID"].ToString();
                                    objDataRowsave["REQUEST_ID"] = objDataRow[0]["REQUEST_ID"].ToString();
                                    objDataRowsave["DEPT_ID"] = objDataRow[0]["DEPT_ID"].ToString();
                                    objDataRowsave["EMP_NO"] = objDataRow[0]["EMP_NO"].ToString();
                                    objDataRowsave["NAME"] = objDataRow[0]["NAME"].ToString();
                                    objDataRowsave["MCR_NO"] = objDataRow[0]["MCR_NO"].ToString();
                                    objDataRowsave["REJ_REASON"] = txtProcessCompletedRemarks.Text.Trim();
                                    objDataRowsave["REJ_TIME_STAMP"] = DateTime.Now.ToString("dd-MM-yyyy");
                                    objDataRowsave["REMARKS"] = objDataRow[0]["REMARKS"].ToString();
                                    objDataRowsave["STATUS"] = "REJECTED";
                                    stringtype = objDataRow[0]["VERIFY_REF"].ToString();
                                    objDataRowsave["VERIFY_REF"] = stringtype;
                                    objDataRowsave["UNIQUE_ID"] = objDataRow[0]["UNIQUE_ID"].ToString();
                                    objDataRowsave["reference_2"] = objDataRow[0]["reference_2"].ToString();
                                    objDataRowsave["COMPLETED_DATE"] = DateTime.Now;
                                    objDataRowsave["DOC_SEQ_ID"] = objDataRow[0]["DOC_SEQ_ID"].ToString();
                                    objDataRowsave["SEQ_NO"] = objDataRow[0]["SEQ_NO"].ToString();
                                    objDataRowsave["DML_INDICATOR"] = "U";

                                    if (HttpContext.Current.Session["stringComputerName"] != null)
                                        objDataRowsave["MODIFIED_AT"] = HttpContext.Current.Session["stringComputerName"].ToString();
                                    if (HttpContext.Current.Session["G11EOSUser_Name"] != null)
                                        objDataRowsave["MODIFIED_BY"] = HttpContext.Current.Session["G11EOSUser_Name"].ToString();
                                    objDataRowsave["MODIFIED_ON"] = DateTime.Now;

                                    objDatasetResult.Tables["t2"].Rows.Add(objDataRowsave);

                                    if (objDatasetResult != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                                    {
                                        objDatasetResult.Tables["t2"].AcceptChanges();
                                        for (int intIndex = 0; intIndex < objDatasetResult.Tables["t2"].Rows.Count; intIndex++)
                                        {
                                            stringDML_INDICATOR = objDatasetResult.Tables["t2"].Rows[intIndex]["DML_INDICATOR"].ToString();

                                            if (stringDML_INDICATOR == "U")
                                            {
                                                objDatasetResult.Tables["t2"].Rows[intIndex].SetModified();
                                            }

                                        }

                                        objDatasetResult = objDatasetResult.GetChanges();
                                        stringServiceType2 = "OperationServiceDML";
                                        stringformid1 = "FC0001R1V3";
                                        objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType2, objDatasetResult.GetChanges(), stringformid1, out int intErrorCount, out string[] stringOutputResult1);
                                        if (intErrorCount == 0)
                                        {
                                            bool boolsuccesss = true;
                                            ////reject 
                                            bool boolUpdatestatusDOCTOR = false;
                                            DataTable objdatatable = null;
                                            if (Session["REQUESTID_FDS001R1V1"] != null)
                                            {
                                                stringRequestID = Session["REQUESTID_FDS001R1V1"].ToString();

                                            }
                                            objdatatable = LoadDocterandVerifiersCOnfirm(stringRequestID);

                                            if (objdatatable != null && objdatatable.Rows.Count > 0 && objdatatable.Select("VERIFY_REF ='DOCTOR'").Length > 0)
                                            {
                                                intoverallDOCTORCompleted = 0;
                                                if (objdatatable.Select("VERIFY_REF = 'DOCTOR' and STATUS = 'COMPLETED'").Length > 0)
                                                {
                                                    intoverallDOCTORCompleted = Convert.ToInt32(objdatatable.Select("VERIFY_REF ='DOCTOR' and STATUS = 'COMPLETED'").Length.ToString());
                                                }
                                                intoverallDOCTORREJECTED = 0;
                                                if (objdatatable.Select("VERIFY_REF = 'DOCTOR' and STATUS = 'REJECTED'").Length > 0)
                                                {
                                                    intoverallDOCTORREJECTED = Convert.ToInt32(objdatatable.Select("VERIFY_REF ='DOCTOR'  and STATUS = 'REJECTED'").Length.ToString());
                                                }
                                                if (intoverallDOCTORCompleted > 0)
                                                {
                                                    boolUpdatestatusDOCTOR = false;
                                                }
                                                else if (intoverallDOCTORREJECTED > 0)
                                                {
                                                    boolUpdatestatusDOCTOR = true;
                                                }
                                            }
                                            else
                                            {
                                                boolUpdatestatusDOCTOR = true;

                                            }
                                            if (boolUpdatestatusDOCTOR)
                                            {
                                                if (txtMRStatus.Text.ToUpper() == "PENDING REPORT" && stringtype.ToUpper() == "DOCTOR")
                                                {
                                                    if (UpdateProcessStatusInREVERSEOrder("DOCTOR"))
                                                    {
                                                        boolsuccesss = true;
                                                    }
                                                    else
                                                    {
                                                        boolsuccesss = false;
                                                    }
                                                }
                                            }
                                            if (boolsuccesss)
                                            {
                                                ViewState["DOCTORCHANGES"] = "ADDED";
                                                stringRequestID1 = txtRequestNo.Text.Trim().ToUpper();
                                                Session["REQUESTID_FDS001R1V1"] = stringRequestID1;
                                                Session["REQUESTID_COMPLETED"] = "COMPLETED";
                                                stringMessage = "";
                                                if (stringtype != null && stringtype.ToString() == "DOCTOR")
                                                {
                                                    stringMessage = "Doctor Rejected Successfully";
                                                }
                                                else if (stringtype != null && stringtype.ToString() == "VERIFIER")
                                                {
                                                    stringMessage = "Verifier Rejected Successfully";
                                                }
                                                ShowMessageandReloadPage(this, stringMessage, "FDS001R1V1.aspx");
                                            }

                                        }
                                        else
                                        {
                                            Session["REQUESTID_COMPLETED"] = null;
                                            Errorpopup(stringOutputResult1);

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
                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
            finally
            {
                objOrderTable = null;
                objDataRow = null;
                interrorcount = 0;
                intTotalRecord = 0;
                objDatasetResult = null;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                objDataRowsave = null;
                stringServiceType1 = "";
                stringtype = "";
                stringexp = "";
                stringDML_INDICATOR = "";
                stringRequestID = "";
                intoverallDOCTORCompleted = 0;
                intoverallDOCTORREJECTED = 0;
                stringServiceType2 = "";
                stringformid1 = "";
                stringRequestID1 = "";
                stringMessage = "";
            }
        }
        protected void btnConfirmprocessClose_Click(object sender, EventArgs e)
        {
            try
            {

                Modelpopuperrorsuccess.Hide();
                pnlupdateremarksandprocess.Visible = false;
                UpdatePanelModal6success.Visible = false;
                hdfuniqid.Value = "";
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        protected void gvassigndoctor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView objdatarowattachments = null;
            DataRow objDataRow = null;
            try
            {
                GridViewRow objGridViewRow = e.Row;
                string stringSort = string.Empty;
                if (objGridViewRow.DataItem == null) { return; }

                objdatarowattachments = ((DataRowView)objGridViewRow.DataItem);
                objDataRow = objdatarowattachments.Row;
                if (objDataRow != null)
                {
                    Button objbtnComplete = e.Row.FindControl("btnCompletedocter") as Button;
                    Button objbtnReject = e.Row.FindControl("btnRejectdocter") as Button;
                    if (objDataRow["REC_NO"].ToString().Length > 0)
                    {
                        objbtnComplete.Visible = true;
                        objbtnReject.Visible = true;
                        Button objbtndelete = e.Row.FindControl("btnDeletedocter") as Button;
                        objbtndelete.Visible = false;

                    }
                    else
                    {
                        objbtnComplete.Visible = false;
                        objbtnReject.Visible = false;
                    }


                    if (txtMRStatus.Text.Trim() == "PENDING REPORT")
                    {
                        objbtnComplete.Enabled = true;
                    }
                    else
                    {
                        objbtnComplete.Enabled = false;
                    }

                    if ((txtMRStatus.Text.Trim() == "PENDING TRACING" || txtMRStatus.Text.Trim() == "PENDING DESPATCH" || txtMRStatus.Text.Trim() == "PENDING ASSIGNED" || txtMRStatus.Text.Trim() == "PENDING REPORT") && ((objDataRow["STATUS"].ToString() == "IN-PROGRESS") || (objDataRow["STATUS"].ToString() == "PENDING" && objDataRow["SEQ_NO"].ToString() == "1")))
                    {
                        objbtnReject.Enabled = true;
                    }
                    else
                    {
                        objbtnReject.Enabled = false;
                    }
                    if (objDataRow["STATUS"].ToString() == "COMPLETED" || objDataRow["STATUS"].ToString() == "REJECTED")
                    {
                        objbtnComplete.Enabled = false;
                        objbtnReject.Enabled = false;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objdatarowattachments = null;
                objDataRow = null;
            }
        }

        protected void gvassignverifier_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView objdatarowattachments = null;
            DataRow objDataRow = null;
            try
            {
                GridViewRow objGridViewRow = e.Row;
                string stringSort = string.Empty;
                if (objGridViewRow.DataItem == null) { return; }

                objdatarowattachments = ((DataRowView)objGridViewRow.DataItem);
                objDataRow = objdatarowattachments.Row;
                if (objDataRow != null)
                {
                    Button objbtnComplete = e.Row.FindControl("btnCompletedocterverifier") as Button;
                    Button objbtnReject = e.Row.FindControl("btnRejectdocterverifier") as Button;
                    if (objDataRow["REC_NO"].ToString().Length > 0)
                    {
                        objbtnComplete.Visible = true;
                        objbtnReject.Visible = true;
                        Button objbtndelete = e.Row.FindControl("btnDeletedocterverifier") as Button;
                        objbtndelete.Visible = false;

                    }
                    else
                    {
                        objbtnComplete.Visible = false;
                        objbtnReject.Visible = false;
                    }


                    if (txtMRStatus.Text.Trim().ToUpper() == "FORWARDED" || txtMRStatus.Text.Trim().ToUpper() == "COLLECTED" || txtMRStatus.Text.Trim().ToUpper() == "CANCELLED")
                    {
                        objbtnComplete.Enabled = false;
                    }
                    else
                    {
                        objbtnComplete.Enabled = true;
                    }

                    if ((txtMRStatus.Text.Trim() == "PENDING TRACING" || txtMRStatus.Text.Trim() == "PENDING DESPATCH" || txtMRStatus.Text.Trim() == "PENDING ASSIGNED" || txtMRStatus.Text.Trim() == "PENDING REPORT") && ((objDataRow["STATUS"].ToString() == "IN-PROGRESS") || (objDataRow["STATUS"].ToString() == "PENDING" && objDataRow["SEQ_NO"].ToString() == "1")))
                    {
                        objbtnReject.Enabled = true;
                    }
                    else
                    {
                        objbtnReject.Enabled = false;
                    }
                    if (objDataRow["STATUS"].ToString() == "COMPLETED" || objDataRow["STATUS"].ToString() == "REJECTED")
                    {
                        objbtnComplete.Enabled = false;
                        objbtnReject.Enabled = false;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objdatarowattachments = null;
                objDataRow = null;
            }
        }

        protected void ddlApplicationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlRerouteRequest.Visible = false;
            pnlRejectRequest.Visible = false;
            pnlAssignDoctorandverifier.Visible = false;
        }


        private void LoadRemarks(string stringRequestID, string stringTYPE, bool boolVAlidation)
        {
            DataSet objDatasetResultREm = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformidREm = "FC0001R1V1";
            string stringOrderByREm = "mrregrmk.CREATED_ON desc";


            //int intFromRecord = intrecFromEnquiry;
            //int intToRecord = intrecToEnquiry;
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTableREm = null;
            string stringBoID = "";
            string stringServiceTypeREm = "";
            string stringExpressionREm = "";
            string stringroleid = "";
            string stringExp = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if ((Session["ADD_REMARKS"] == null) || (stringTYPE.Length > 0 && stringTYPE != "LOAD"))
                {
                    stringServiceTypeREm = "List9R1V1";
                    stringExpressionREm = "And mrregrmk.be_id= '" + stringBoID + "' And mrregrmk.Request_ID= '" + stringRequestID + "' ";

                    objDatasetResultREm = CommonFunctions.SelectionServiceClient(stringServiceTypeREm, stringformidREm, stringExpressionREm, stringOrderByREm, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    //PopulateEnquiry(intTotalRecord, intpageIndex);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResultREm != null && objDatasetResultREm.Tables.Count > 0 && objDatasetResultREm.Tables["t9"] != null && objDatasetResultREm.Tables["t9"].Rows.Count > 0)
                        {
                            objDataTableREm = objDatasetResultREm.Tables["t9"];
                        }

                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                        pnlRemarksresultgrid.Visible = false;
                    }
                }
                else
                {
                    if (HttpContext.Current.Session["ADD_REMARKS"] != null)
                    {
                        objDataTableREm = (DataTable)HttpContext.Current.Session["ADD_REMARKS"];
                    }
                }
                if (boolVAlidation)
                {
                    stringroleid = "";
                    stringExp = "";
                    if (objDataTableREm != null && objDataTableREm.Rows.Count > 0)
                    { 
                        if (Session["UserRole_ForRemarks"] != null)
                        {
                            stringroleid = Session["UserRole_ForRemarks"].ToString();
                        }
                        if (stringroleid.Length > 0 && (stringroleid.Contains("DEPARTMENT SECRETARY")))
                        {
                            stringExp = "(TARG_AUD= 'ALL') OR (TARG_AUD= 'DEPARTMENT SECRETARY') OR (TARG_AUD= 'HOD') OR (TARG_AUD= 'HIMS SUPERVISOR') OR (TARG_AUD= 'HIMS USERS')  ";
                        }
                        else if (stringroleid.Length > 0 && (stringroleid.Contains("DOCTORS")))
                        {
                            stringExp = "(TARG_AUD= 'ALL') OR (TARG_AUD= 'DOCTORS') OR (TARG_AUD= 'HIMS SUPERVISOR') OR (TARG_AUD= 'HIMS USERS') OR (TARG_AUD= 'HOD')  ";
                        }
                        if (stringExp.Length > 0)
                        {
                            if (objDataTableREm.Select(stringExp).Length > 0)
                            {
                                objDataTableREm = objDataTableREm.Select(stringExp).CopyToDataTable();
                            }
                            else
                            {
                                objDataTableREm = null;
                            }
                        }
                    }
                }

                if (objDataTableREm != null && objDataTableREm.Rows.Count > 0)
                {
                    objDataTableREm.DefaultView.Sort = "REMARKS_DATE desc";
                    objDataTableREm = objDataTableREm.DefaultView.ToTable();

                    gvList.DataSource = objDataTableREm;
                    gvList.DataBind();
                    pnlRemarksresultgrid.Visible = true;
                    lblpnlremRecord.InnerText = objDataTableREm.Rows.Count.ToString();

                    //PopulateRemarks(objDataTableREm.Rows.Count, intpageIndex);
                }
                else
                {
                    //PopulateRemarks(0, intpageIndex);
                    gvList.DataSource = null;
                    gvList.DataBind();
                    pnlRemarksresultgrid.Visible = false;
                    lblpnlremRecord.InnerText = "0";
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDatasetResultREm = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformidREm = "";
                stringOrderByREm = "";
                objDataTableREm = null;
                stringBoID = "";
                stringServiceTypeREm = "";
                stringExpressionREm = "";
                stringroleid = "";
                stringExp = "";
            }
        }
        protected void btnCancelTrigger_Click(object sender, EventArgs e)
        {
            bool boolsuccesss = true;
            if (UpdateProcessStatus(txtMRStatus.Text.Trim().ToUpper()))
            {
                boolsuccesss = true;
            }
            else
            {
                boolsuccesss = false;
            } 
        }

    
    }
}