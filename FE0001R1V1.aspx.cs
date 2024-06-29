using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FE0001R1V1 : System.Web.UI.Page
    { 
        public DataSet objDatasetAppsVariables;
        public string stringformIdPaging = "MRTrackingPopupPaging";
        public int intpageIndexdropdownpopup = 0;
        public int intrecFromdropdownpopup = 0;
        public int intrecTodropdownpopup = 10;
        public string stringformIdddlpopup = "FC0001RropdownpopupPaging";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CommonFunctions.IsActive())
            {
                try
                { 
                    intrecTodropdownpopup = CommonFunctions.GridViewPagesize(stringformIdddlpopup);
                    if (!IsPostBack)
                    {
                        VerifyAccessRights();
                        hdnfrom.Value = "0";
                        hdnpageIndex.Value = "0";
                        hdnto.Value= CommonFunctions.GridViewPagesize(stringformIdPaging).ToString();
                        CommonFunctions.HeaderName(this, "FE0001R1V1"); 

                        ViewState["vsSortDirection"] = " ASC";
                        ViewState["vsSortExpression"] = "";
                        Session["SortTable"] = null;
                        lblTotalRecords.InnerText = "0";
                        LoadRequestorTypes();
                        LoadRequestTypes();
                        LoadMRStdTasks();
                        LoadDepartments();
                        loadreceivedfrom();
                        loaddeliverymode();
                        lbtnClear_Click(null, null);

                    }
                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
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
            //imgBtnNew.Enabled = false;
            // imgBtnSave.Enabled = false;
            //imgBtnDelete.Enabled = false;
            imgbtnprint.Enabled = false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FE0001R1V1";
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
                            imgbtnprint.Enabled = true;
                        }
                    }
                    else
                    {
                        Response.Redirect("PageAccessDenied.aspx", true);
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
        private void LoadMRStdTasks()//
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0032R1V1";
            string stringOrderBy = "CASE   WHEN mrsttaks.mrpt_id = 'Draft' THEN 1 else mrsttaks.REFERENCE_NO_1  end asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableLoadMRStdTasks = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringcondition = "";
            string stringServiceType = "";
            string stringID = "";
            try
            {
                ddlMRStatus.Items.Clear();
                stringcondition = "And mrsttaks.be_id= '" + stringbeid + "' AND mrsttaks.delmark='N'";
                stringServiceType = "List1R1V1";
                if (Session["SSNLOADMRSTDTASKS"] != null)
                {
                    objdatatableLoadMRStdTasks = (DataTable)Session["SSNLOADMRSTDTASKS"];
                }
                if ((objdatatableLoadMRStdTasks == null) || (objdatatableLoadMRStdTasks != null && objdatatableLoadMRStdTasks.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadMRStdTasks = objDatasetResult.Tables["t1"];
                            Session["SSNLOADMRSTDTASKS"] = objdatatableLoadMRStdTasks;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadMRStdTasks != null && objdatatableLoadMRStdTasks.Rows.Count > 0)
                {
                    foreach (DataRow objDataRow in objdatatableLoadMRStdTasks.Rows)
                    {
                        stringID = objDataRow["trans_status"].ToString();
                        if (ddlMRStatus.Items.FindByValue(stringID) == null)
                        { ddlMRStatus.Items.Add(new ListItem(objDataRow["trans_status"].ToString(), stringID)); }
                    }  
                    ddlMRStatus.Items.Insert(0, "");
                    ddlMRStatus.SelectedIndex = 0;
                }
                else
                {
                    ddlMRStatus.DataSource = null;
                    ddlMRStatus.DataBind();
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
                objdatatableLoadMRStdTasks = null;
                stringbeid = "";
                stringcondition = "";
                stringServiceType = "";
                stringID = "";
            }
        }

        private void LoadDepartments()//
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
                    ddlDepartmentOU.DataTextField = "SHORT_NAME";
                    ddlDepartmentOU.DataValueField = "DEPT_ID";
                    ddlDepartmentOU.DataSource = objdatatableLoadDepartmentOU;
                    ddlDepartmentOU.DataBind();
                    ddlDepartmentOU.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlDepartmentOU.DataSource = null;
                    ddlDepartmentOU.DataBind();
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
                intToRecord =0;
                stringbeid = "";
                objdatatableLoadDepartmentOU = null;
                stringcondition = "";
                stringServiceType = "";
            }
        }    
        private void loaddeliverymode()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0009R1V1";
            string stringOrderBy = "mrdelmos.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatabledeliverymode = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                ddldeliverymode.Items.Clear();
                stringcondition = "And mrdelmos.be_id= '" + stringbeid + "'  AND mrdelmos.delmark='N' ";
                stringServiceType = "List1R1V1";
                if (Session["SSNLOADDELIVERYMODE"] != null)
                {
                    objdatatabledeliverymode = (DataTable)Session["SSNLOADDELIVERYMODE"];
                }
                if ((objdatatabledeliverymode == null) || (objdatatabledeliverymode != null && objdatatabledeliverymode.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatabledeliverymode = objDatasetResult.Tables["t1"];
                            Session["SSNLOADDELIVERYMODE"] = objdatatabledeliverymode;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatabledeliverymode != null && objdatatabledeliverymode.Rows.Count > 0)
                {  
                    ddldeliverymode.DataTextField = "SHORT_NAME";
                    ddldeliverymode.DataValueField = "DELMOD_ID";
                    ddldeliverymode.DataSource = objdatatabledeliverymode;
                    ddldeliverymode.DataBind();
                    ddldeliverymode.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddldeliverymode.DataSource = null;
                    ddldeliverymode.DataBind();
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
                objdatatabledeliverymode = null;
                stringbeid = "";
                stringcondition = "";
                stringServiceType = "";
            }

        }

        private void loadreceivedfrom()//fix
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
            DataTable objdatatableLoadreceivedfrom = null;
            string stringLstRefGroupID = "";
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                ddlreceivedfrom.Items.Clear();

                stringLstRefGroupID = "RECFROM";
                stringcondition = "And lst.be_id= '" + stringbeid + "'  AND  lst.LSTGRP_ID='" + stringLstRefGroupID + "'AND lst.delmark='N' ";

                stringServiceType = "List1R1V1";
                if (Session["SSNLOADRECEIVEDFROM"] != null)
                {
                    objdatatableLoadreceivedfrom = (DataTable)Session["SSNLOADRECEIVEDFROM"];
                }
                if ((objdatatableLoadreceivedfrom == null) || (objdatatableLoadreceivedfrom != null && objdatatableLoadreceivedfrom.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadreceivedfrom = objDatasetResult.Tables["t1"];
                            Session["SSNLOADRECEIVEDFROM"] = objdatatableLoadreceivedfrom;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }

                if (objdatatableLoadreceivedfrom != null && objdatatableLoadreceivedfrom.Rows.Count > 0)
                {
                    ddlreceivedfrom.DataTextField = "short_name";
                    ddlreceivedfrom.DataValueField = "lst_id";
                    ddlreceivedfrom.DataSource = objdatatableLoadreceivedfrom;
                    ddlreceivedfrom.DataBind();
                    ddlreceivedfrom.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlreceivedfrom.DataSource = null;
                    ddlreceivedfrom.DataBind();
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
                stringformid = "FA0014R1V1";
                stringOrderBy = "lst.short_name asc";
                intFromRecord = 0;
                intToRecord = int.MaxValue;
                stringbeid = CommonFunctions.GETBussinessEntity();
                objdatatableLoadreceivedfrom = null;
                stringLstRefGroupID = "";
                stringcondition = "";
                stringServiceType = "";
            }
        }

        private void LoadRequestorTypes()//fised
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
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                objdatatableLoadRequestorTypes = null;
                stringbeid = "";
                stringexp = "";
                stringServiceType = "";
            }
        }

        private void LoadRequestTypes()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0026R1V1";
            string stringOrderBy = "mrreqtyp.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            DataTable objdatatablereqtype = null;
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                ddlReqType.Items.Clear();
                if (Session["SSNLOADREQUESTTYPES"] != null)
                {
                    objdatatablereqtype = (DataTable)Session["SSNLOADREQUESTTYPES"];
                }
                if ((objdatatablereqtype == null) || (objdatatablereqtype != null && objdatatablereqtype.Rows.Count == 0))
                {
                    stringcondition = "And mrreqtyp.be_id= '" + stringbeid + "' AND mrreqtyp.delmark='N'";
                    stringServiceType = "List1R1V1";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatablereqtype = objDatasetResult.Tables["t1"];
                            Session["SSNLOADREQUESTTYPES"] = objdatatablereqtype;
                        }

                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }

                if (objdatatablereqtype != null && objdatatablereqtype.Rows.Count > 0)
                {
                    ddlReqType.DataTextField = "short_name";
                    ddlReqType.DataValueField = "requesttyp_id";
                    ddlReqType.DataSource = objdatatablereqtype;
                    ddlReqType.DataBind();
                    ddlReqType.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlReqType.DataSource = null;
                    ddlReqType.DataBind();
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
                stringbeid = CommonFunctions.GETBussinessEntity();
                objdatatablereqtype = null;
                stringcondition = "";
                stringServiceType = "";
            }
        }

        protected void LnkbtnSort_Click(object sender, EventArgs e)
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
                       ViewState["vsSortExpression"] = "temtrac." + stringColumnName + ViewState["vsSortDirection"].ToString();
                    
                        LoadRecord((string)ViewState["exportconditiondesig"], ViewState["vsSortExpression"].ToString());
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
        private void Errorpopup(string[] stringOutputResult)
        {
            try
            {
                lblModalTile5.Text = "Error Message Summary";
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

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DoEmptyValidation())
                {
                    hdnfrom.Value = "0";
                    hdnpageIndex.Value = "0";
                    hdnto.Value = CommonFunctions.GridViewPagesize(stringformIdPaging).ToString();
                    ShowSpecialInfo();
                    SearchRecords();
                    SelectText(txtHRN);
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, "Please give at least one search criteria");
                }

                txtHRN.Focus();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private void SelectText(TextBox objTextbox)
        {
            try
            {
                if (objTextbox != null)
                {
                    ClientScript.RegisterStartupScript(typeof(String), "ClientScript", "<script language='JavaScript'>document.getElementById('" + objTextbox.ClientID + "').select();</script>");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
           
        }
        private string PrepareSearchExpression()
        {
            string stringExpression = "";
            string stringEncrypyValue = "";
            string stringInput = "";
            string stringfromdate = "";
            string stringtodate = "";
            string stringdelmark = "";
            try
            {
                stringInput = txtHRN.Text.Trim();
                if (stringInput.Length > 0)
                {
                    stringEncrypyValue = CommonFunctions.HRNtoEncrypyValue(stringInput);
                }  
                if (stringEncrypyValue.Length > 0 )
                {
                    stringExpression += "And pat.hrn_id='" + stringEncrypyValue.Trim() + "'";
                } 
                if (txtReqNo.Text.Length > 0 && txtReqNo.Text.Trim() != "%")
                {
                    stringExpression += txtReqNo.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER( Reg.request_id)  LIKE UPPER('%" + txtReqNo.Text.Trim() + "%'))" : "";
                }
                if (txtPatName.Text.Length > 0 && txtPatName.Text.Trim() != "%")
                {
                    stringExpression += txtPatName.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER( inref.INDEX_VALUE)  LIKE UPPER('%" + txtPatName.Text.Trim().Replace("'", "''") + "%'))" : "";
                }
                if (txtFrmDate.Text.Trim().Length > 0)
                {
                    DateTime objDateFrom = CommonFunctions.ConvertToDateTime(txtFrmDate.Text.Trim(), "dd-MM-yyyy");
                    stringfromdate = objDateFrom.ToString("MM-dd-yyyy");
                    stringExpression += "AND CONVERT(date,Reg.created_on)  >= CONVERT(date,'" + stringfromdate + "') ";
                }
                if (txtToDate.Text.Trim().Length > 0)
                {
                    DateTime objDateTo = CommonFunctions.ConvertToDateTime(txtToDate.Text.Trim(), "dd-MM-yyyy");
                    stringtodate = objDateTo.ToString("MM-dd-yyyy");
                    stringExpression += "AND CONVERT(date,Reg.created_on)  <= CONVERT(date,'" + stringtodate + "') ";
                }
                if (ddlMRStatus.SelectedItem != null && ddlMRStatus.SelectedValue.Length > 0 )
                {
                    stringExpression += ddlMRStatus.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Reg.MR_STATUS)  LIKE UPPER('%" + ddlMRStatus.SelectedValue.Trim() + "%'))" : "";
                }
                if (ddlDepartmentOU.SelectedItem != null && ddlDepartmentOU.SelectedValue.Length > 0)
                {
                    stringExpression += ddlDepartmentOU.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Reg.dept_id)  LIKE UPPER('%" + ddlDepartmentOU.SelectedValue.Trim() + "%'))" : "";
                }
                if (ddlReqType.SelectedItem != null && ddlReqType.SelectedValue.Length > 0)
                {
                    stringExpression += ddlReqType.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Reg.RequestTyp_ID)  LIKE UPPER('%" + ddlReqType.SelectedValue.Trim() + "%'))" : "";
                }
                if (ddlRequestorType.SelectedItem != null && ddlRequestorType.SelectedValue.Length > 0)
                {
                    stringExpression += ddlRequestorType.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Reg.ReqTyp_ID)  LIKE UPPER('%" + ddlRequestorType.SelectedValue.Trim() + "%'))" : "";
                } 
                if (ddlWApp.SelectedItem != null && ddlWApp.SelectedValue.Length > 0)
                {
                    stringExpression += ddlWApp.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(mrwa.WAIVER_STATUS)  LIKE UPPER('%" + ddlWApp.SelectedValue.Trim() + "%'))" : "";
                } 
                if (txtrequestor01ID.Text.Length > 0 && txtrequestor01ID.Text.Trim() != "%" && txtrequestor01name.Text.Length > 0)
                {
                    stringExpression += txtrequestor01ID.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER( Reg.RptReq_ID)  LIKE UPPER('%" + txtrequestor01ID.Text.Trim() + "%'))" : "";

                } 
                if (txtRecTypeeID.Text.Length > 0 && txtRecTypeeID.Text.Trim() != "%" && txtRecTypeename.Text.Length > 0)
                {
                    stringExpression += txtRecTypeeID.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER( Reg.rpttyp_id)  LIKE UPPER('%" + txtRecTypeeID.Text.Trim() + "%'))" : "";
                }
                if (ddldeliverymode.SelectedValue.Length > 0 && ddldeliverymode.SelectedValue.Trim() != "%")
                {
                    stringExpression += ddldeliverymode.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Reg.DelMod_ID)  LIKE UPPER('%" + ddldeliverymode.SelectedValue.Trim() + "%'))" : "";
                }
                if (ddlreceivedfrom.SelectedItem != null && ddlreceivedfrom.SelectedValue.Length > 0)
                {
                    stringExpression += ddlreceivedfrom.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Reg.Received_From)  LIKE UPPER('%" + ddlreceivedfrom.SelectedValue.Trim() + "%'))" : "";
                }
                if (ddlblockbiining.SelectedItem != null && ddlblockbiining.SelectedValue.Length > 0)
                {
                    stringExpression += ddlblockbiining.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Reg.Block_Billing)  LIKE UPPER('%" + ddlblockbiining.SelectedValue.Trim() + "%'))" : "";
                }
                if (ddlApplicationStatus.SelectedItem != null && ddlApplicationStatus.SelectedValue.Length > 0)
                {
                    stringdelmark = "";
                    if (ddlApplicationStatus.SelectedValue != null && ddlApplicationStatus.SelectedValue.ToString().ToUpper() == "ACTIVE")
                    {  stringdelmark = "N";}
                    else{stringdelmark = "Y";}
                    stringExpression += ddlApplicationStatus.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Reg.delmark)  LIKE UPPER('%" + stringdelmark + "%'))" : "";
                }

                return stringExpression;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return null;
            }
            finally
            {
                stringExpression = "";
                stringEncrypyValue = "";
                stringInput = "";
                stringfromdate = "";
                stringtodate = "";
                stringdelmark = "";
            }
        }

        private void SearchRecords()
        {
            string stringexp = "";
            int inthdnpageIndex = 0;
            bool boolstatus = true; 
            string stringOrderBy = "temtrac.created_on desc";
            try
            {
                if (txtHRN.Text.Trim().Length > 0 && !DoNonCGHHrnValidation())
                {
                    boolstatus = false;
                }
                if (boolstatus)
                {
                    if (ValidateControls() && ValidateBusinessLogic())
                    { 
                        stringexp = PrepareSearchExpression(); 
                        if (stringexp.ToString() == "")
                        {
                            CommonFunctions.ShowMessageboot(this, "Please give at least one search criteria");
                            lblTotalRecords.InnerText = "0";
                            Session["SortTable"] = null;
                            gvUserHistory.DataSource = null;
                            gvUserHistory.DataBind(); 
                            lblTotalRecords.InnerText = "0";
                            inthdnpageIndex = Convert.ToInt32(hdnpageIndex.Value.ToString());
                            PopulatePager(0, inthdnpageIndex);
                        }
                        else
                        {
                          LoadRecord(stringexp,stringOrderBy);
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
                stringexp = ""; 
            }
        }

        private void LoadRecord(string stringexp, string stringOrderBy)
        {
            int intTotalRecord = 0;
            int inthdnpageIndex = 0;
            DataSet objDataSet = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FE0001R1V3";
            string stringServiceType = "List1R1V1";
            int intRecordFrom = 0;
            int intRecordTo = int.MaxValue;
            DataTable objDataTable = null;

            intRecordFrom = Convert.ToInt32(hdnfrom.Value.ToString());
            intRecordTo = Convert.ToInt32(hdnto.Value.ToString()); 
            try
            {
                inthdnpageIndex = Convert.ToInt32(hdnpageIndex.Value.ToString());
                ViewState["exportconditiondesig"] = stringexp;
                ViewState["vsSortExpression"] = stringOrderBy;
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);
               
                 PopulatePager(intTotalRecord, inthdnpageIndex);
                lblTotalRecords.InnerText = intTotalRecord.ToString();
                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                    {
                        Session["SortTable"] = objDataSet.Tables[0];
                         
                        objDataSet.Tables[0].DefaultView.Sort = "priority_flag desc,CREATED_ON desc";
                        objDataTable = objDataSet.Tables[0].DefaultView.ToTable();

                        gvUserHistory.DataSource = objDataTable;
                        gvUserHistory.DataBind();
                        Modelpopuperrorsuccess.Hide();
                    }
                    else
                    {
                        lblTotalRecords.InnerText = "0";
                        Modelpopuperrorsuccess.Show();
                        Session["SortTable"] = null;
                        gvUserHistory.DataSource = null;
                        gvUserHistory.DataBind();
                    }
                }
                else
                {
                    lblTotalRecords.InnerText = "0";
                    PopulatePager(0, inthdnpageIndex);
                    Errorpopup(stringOutputResult);
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                intTotalRecord = 0;
                objDataSet = null;
                interrorcount = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                stringServiceType = "";
                intRecordFrom = 0;
                intRecordTo = 0;
                objDataTable = null;
            }
        }
        private void ShowSpecialInfo()
        {
            string stringBoID = "";
            bool boolCurrentDayOne = true;
            string stringHRNID = "";
            string stringSpecialInfo = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (txtHRN.Text.Trim().Length > 0)
                {
                    boolCurrentDayOne = true;
                    stringHRNID = txtHRN.Text.Trim().ToUpper();
                    stringSpecialInfo = CommonFunctions.GetSpecialInfo(stringBoID, stringHRNID, boolCurrentDayOne);
                    if (stringSpecialInfo != null && stringSpecialInfo.Trim().Length > 0) { CommonFunctions.ShowMessageboot(this, stringSpecialInfo); }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringBoID = ""; 
                stringHRNID = "";
                stringSpecialInfo = "";
            }
        }
        private bool ValidateControls() 
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";

                if (txtFrmDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length < 0)
                {
                    stringOverallMsg += "- To Date" + "\\r\\n";
                    boolStatus = false;
                }

                if (txtToDate.Text.Trim().Length < 0 && txtFrmDate.Text.Trim().Length > 0)
                {
                    stringOverallMsg += "- From Date" + "\\r\\n";
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
            finally
            {
                boolStatus = true;
                stringOverallMsg = "";
            }
        }

        private bool ValidateBusinessLogic()//
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "";

                if (txtFrmDate.Text.Trim().Length > 0 && !CommonFunctions.ValidDateTime(txtFrmDate.Text.Trim()))
                {
                    stringOverallMsg += "Date From should be a valid date.";
                    boolStatus = false;
                }
                else if (txtToDate.Text.Trim().Length > 0 && !CommonFunctions.ValidDateTime(txtToDate.Text.Trim()))
                {
                    stringOverallMsg += "Date To should be a valid date.";
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
            finally
            {
                boolStatus = true;
                stringOverallMsg = "";
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtReqNo.Text = "";
                txtHRN.Text = "";
                txtPatName.Text = "";
                txtFrmDate.Text = "";
                txtToDate.Text = "";
                ddlMRStatus.SelectedIndex = 0;
                ddlDepartmentOU.SelectedIndex = 0;
                ddlReqType.SelectedIndex = 0; 
                ddlRequestorType.SelectedIndex = 0;
                ddlWApp.ClearSelection();
                ddlblockbiining.ClearSelection();
                txtRecTypeeID.Text = "";
                txtRecTypeename.Text = "";
                txtrequestor01ID.Text = "";
                txtrequestor01name.Text = "";
                ddlreceivedfrom.ClearSelection();
                ddldeliverymode.ClearSelection();
                ddlApplicationStatus.SelectedIndex = 0;
                Session["stringDMLIndicator"] = "I";
                txtHRN.Focus();
                Session["FE0001transID"] = null;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        //for paging
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
                if (pageCount == 1 || pageCount == 0)
                {
                    ViewState["lastpagepagig"] = false;
                    pages.Clear();
                    //pages.Add(new ListItem("Last", pageCount.ToString()));
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
            int intRecordTo = 0;
            int inthdnpageIndex = 0; 
            try
            {
                if (Session["PageIndex1"] != null)
                {
                    hdnpageIndex.Value =Session["PageIndex1"].ToString();
                    Session["PageIndex"] = Convert.ToInt32(Session["PageIndex1"].ToString());
                }
                else
                {
                    hdnpageIndex.Value = int.Parse((sender as LinkButton).CommandArgument).ToString();

                    inthdnpageIndex = Convert.ToInt32(hdnpageIndex.Value.ToString());

                    if (inthdnpageIndex != 0)
                    {
                        Session["PageIndex"] = inthdnpageIndex;
                    }
                }
                inthdnpageIndex = Convert.ToInt32(hdnpageIndex.Value.ToString());

                if (inthdnpageIndex == 1)
                {
                    hdnfrom.Value = "0";
                    hdnto.Value = CommonFunctions.GridViewPagesize(stringformIdPaging).ToString(); 
                }
                else
                {
                    intRecordTo = Convert.ToInt32(hdnto.Value.ToString());
                    inthdnpageIndex = Convert.ToInt32(hdnpageIndex.Value.ToString());
                    int intpaging = CommonFunctions.GridViewPagesize(stringformIdPaging);
                    int recFrom1 = (inthdnpageIndex * intpaging) - intpaging;

                    hdnfrom.Value = (recFrom1 + 1).ToString();
                    hdnto.Value =( recFrom1 + CommonFunctions.GridViewPagesize(stringformIdPaging)).ToString();
                     
                }

                SearchRecords();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void gvUserHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet objdataset = new DataSet();
            DataRow objDataRow = null;
            string stringPRIORITY_FLAG = "";
            bool boolisReadOnly = true;
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    objDataRow = ((DataRowView)e.Row.DataItem).Row; 
                    var Icon01 = (Image)e.Row.FindControl("imggridflag01");
                   

                    if (objDataRow != null)
                    { 
                        stringPRIORITY_FLAG = objDataRow["PRIORITY_FLAG"].ToString().ToUpper();
                        if (stringPRIORITY_FLAG == "Y")
                        {
                            Icon01.Visible = true;
                        }
                        else if (stringPRIORITY_FLAG == "N")
                        {
                            Icon01.Visible = false;
                        }
                    }

                    GridViewRow objGridViewRow = e.Row;
                    if (objGridViewRow.DataItem == null) { return; }

                    objDataRow = ((DataRowView)e.Row.DataItem).Row;
                    if (objDataRow != null)
                    {
                        LinkButton lnkbtnReceiptNo = (LinkButton)e.Row.FindControl("lnkbtnUserID");

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
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objdataset = null ;
                objDataRow = null;
                stringPRIORITY_FLAG = "";
            }
        }

        protected void lnkbtnUserID_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string stringProductID = "";
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
                            stringProductID = stringValues[0];

                            if (stringProductID.Length > 0)
                            {
                                Session["REQUEST_FromSummary"] = stringProductID;
                                Response.Redirect("FC0001R1V1.aspx?TO=Y");
                            }
                            else
                            {
                                Session["REQUEST_FromSummary"] = null;
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
                stringCmdArgument = "";
                stringProductID = "";
                stringValues = null;
            }
        }

        private bool DoEmptyValidation()
        {
            try
            {
                if (txtReqNo.Text.Trim().Length > 0) { return false; }
                if (txtHRN.Text.Trim().Length > 0) { return false; }
                if (txtPatName.Text.Trim().Length > 0) { return false; }
                if (txtFrmDate.Text.Trim().Length > 0) { return false; }
                if (txtToDate.Text.Trim().Length > 0) { return false; }
                if (ddlDepartmentOU.Text.Trim().Length > 0) { return false; }
                if (ddlMRStatus.Text.Trim().Length > 0) { return false; }  
                if (ddlReqType.Text.Trim().Length > 0) { return false; }
                if (ddlRequestorType.Text.Trim().Length > 0) { return false; }
                if (ddlApplicationStatus.Text.Trim().Length > 0) { return false; }
                if (ddlWApp.Text.Trim().Length > 0)
                {
                    return false;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
           
            return false; 
        }
        #region validate HRN

        public bool DoNonCGHHrnValidation()
        {
            bool boolMROHRN = false;
            bool boolStatus = true;
            string stringInput = "";
            string stringResult = "";
            try
            {
                if (txtHRN.Text.Trim().Length > 0)
                {
                    stringInput = txtHRN.Text.Trim().ToUpper();
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
                                    txtHRN.Text = stringInput[0] + stringInput.Substring(2, stringInput.Length - 2) + stringInput[1];
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
                            stringResult = CommonFunctions.ValidateHRN(txtHRN.Text.Trim().ToUpper(), out string stringFormmatHrnID);
                            if (stringResult != "SUCCESS" && stringResult != "")
                            {
                                CommonFunctions.ShowMessageboot(this, "Invalid MRN");
                                return false;
                            }
                            else if (stringResult == "SUCCESS")
                            {
                                txtHRN.Text = ArrangeHRNNumber(stringFormmatHrnID);
                                return true;
                            }
                            else
                            {
                                txtHRN.Text = ArrangeHRNNumber(stringResult);
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
                        txtHRN.Focus();
                        // SelectText(txtHRN);
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
                boolMROHRN = false;
                boolStatus = true;
                stringInput = "";
                stringResult = "";
            }

            return false;
        }
        private string ArrangeHRNNumber(string stringHRN)
        {
            string stringSub1 = "";
            string stringSub2 = "";
            string stringResult = "";
            try
            {
                if (stringHRN.Trim().Length > 0)
                {
                    if (stringHRN.ToUpper().StartsWith("X") || stringHRN.ToUpper().StartsWith("Y"))
                    {
                        if (stringHRN.ToUpper().Length == 10 || stringHRN.ToUpper().Length == 12)
                        {
                            stringSub1 = stringHRN.Substring(0, 2);
                            stringSub2 = stringHRN.Substring(2, stringHRN.Length - 2);
                            stringResult = stringSub1.Trim()[0].ToString() + stringSub2.Trim() + stringSub1.Trim()[1];
                            return stringResult;
                        }
                    }
                    else if (stringHRN.Trim().Length == 10)
                    {
                        stringSub1 = stringHRN.Substring(0, 3);
                        stringSub2 = stringHRN.Substring(3, stringHRN.Length - 3);
                        stringResult = stringSub1.Trim()[0].ToString() + stringSub2.Trim() + stringSub1.Trim()[1];
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

        protected void btnStatus_Click(object sender, EventArgs e)
        {

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string stringProductID = "";
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
                            stringProductID = stringValues[0];

                            if (stringProductID.Length > 0)
                            {
                                Session["REQUESTID_ENQUIRY"] = stringProductID;
                                Response.Redirect("FC0005R1V1.aspx");
                            }
                            else
                            {
                                Session["REQUESTID_ENQUIRY"] = null;
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
                stringProductID = "";
                stringValues = null;
            }
        }

        protected void btnconfirmok_Click(object sender, EventArgs e)
        {
            Response.Redirect("FC0001R1V1.aspx?Load=01");
        }

        #region popup

        //txtboxclear Dynamic
        protected void imgbtnCleardropdowntxtboxvalue_Click(object sender, ImageClickEventArgs e)
        {
            string buttonId = "";
            string ToolTip = "";
            string stringToolTip = "";
            string ID = "";
            string Name = "";
            string[] stringValues = null;
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
                                Control foundControlupdpnl = upnl6.ContentTemplateContainer.FindControl(ID);
                                Control foundControlNAme = upnl6.ContentTemplateContainer.FindControl(Name);
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
            finally
            {
                buttonId = "";
                ToolTip = ""; 
            }

        }

        protected void lnkbtnddlpopupID_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string stringId = "";
            string stringNAME = "";
            string txtID = "";
            string txtnameID = "";
            string updatepnlID = "";
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
                            stringId = stringValues[0];
                            stringNAME = stringValues[1];
                            if (hdnPopupDropdownValue != null && hdnPopupDropdownValue.Value.Length > 0) 
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
                                            Control foundControltxtID = upnl6.ContentTemplateContainer.FindControl(txtID);
                                            Control foundControltxtNameID = upnl6.ContentTemplateContainer.FindControl(txtnameID);
                                            Control foundControlupdatepnlID = upnl6.ContentTemplateContainer.FindControl(updatepnlID);
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
                stringCmdArgument = "";
                stringId = "";
                stringNAME = ""; 
                stringValues = null;
            }

        }
        protected void btnclosOrganisationpopup_Click(object sender, EventArgs e)
        {
            try
            {
                mdlpnlddlpopup.Hide();
                Panel3.Visible = false;
                mpePdtPlt2.Show();

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
           
        }

        private void PopulatePagerdropdownpopup(int recordCount, int currentPage, int recto)
        {
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
                            string[] stringValues = ToolTip.Split('_');
                            if (stringValues != null && stringValues.Length == 4)
                            {
                                ddlID = stringValues[0];
                                txtID = stringValues[1];
                                txtNAmeID = stringValues[2];
                                updatepnlID = stringValues[3];
                                if (ddlID.Length > 0)
                                {
                                    if (ddlID == "RECTYPE")
                                    {
                                        lblpopupname.Text = "Report type";
                                    }
                                    else if (ddlID == "REQUESTOR01")
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
                buttonId = "";
                ToolTip = ""; 
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

                intRecordFrom = intrecFromdropdownpopup;
                intRecordTo = intrecTodropdownpopup;
                stringOutputResult = new string[3];
                if (hdnPopupDropdownValue != null && hdnPopupDropdownValue.Value.Length > 0)
                {
                    if (hdnPopupDropdownValue.Value == "RECTYPE")
                    {
                        stringformid = "FA0024R1V1";
                        stringOrderBy = "mrrets.short_name asc";

                        stringexp01 += "And mrrets.delmark= 'N'";
                        
                    }
                    else if (hdnPopupDropdownValue.Value == "REQUESTOR01")
                    {
                        stringformid = "FA0027R1V1";
                        stringOrderBy = "mrreq.short_name asc";
                        stringexp01 += "And mrreq.be_id= '" + stringbeid + "' ";
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

                PopulatePagerdropdownpopup(intrecordcount, intpageIndexdropdownpopup, intrecTodropdownpopup);

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
                stringOrderBy = "";
                stringformid = "";
                stringServiceType = "";
                stringbeid = "";
            }
        }

        protected void btnfindddlpopupRecord_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownSearchCndition();

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
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
                    if (hdnPopupDropdownValue.Value == "RECTYPE")
                    {
                        stringColumn1 = "mrrets.RPTTYP_ID";
                        stringColumn2 = "mrrets.short_name";
                    }
                    else if (hdnPopupDropdownValue.Value == "REQUESTOR01")
                    {
                        stringColumn1 = "mrreq.RPTREQ_ID";
                        stringColumn2 = "mrreq.SHORT_NAME";
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
            finally
            {
                stringExpression = "";
                stringColumn1 = "";
                stringColumn2 = "";
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
                if (objGridViewRow.DataItem == null) { return; }

                objDRV = ((DataRowView)objGridViewRow.DataItem);
                objDataRow = objDRV.Row;
                if (objDataRow != null)
                {
                    if (hdnPopupDropdownValue != null && hdnPopupDropdownValue.Value.Length > 0)
                    {
                        if (hdnPopupDropdownValue.Value == "RECTYPE")
                        {
                            stringID = objDataRow["RPTTYP_ID"].ToString();
                            stringdesc = objDataRow["short_name"].ToString();
                        }
                        else if (hdnPopupDropdownValue.Value == "REQUESTOR01")
                        {
                            stringID = objDataRow["RPTREQ_ID"].ToString();
                            stringdesc = objDataRow["SHORT_NAME"].ToString();
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
                stringID = "";
                stringdesc = "";
                stringSort = string.Empty;
                objDRV = null;
                objDataRow = null;
            }
        }

        protected void lnkPagedropdownpopup_Click(object sender, EventArgs e)
        {
            int recFromProcessHistory1 = 0;
            try
            {
                if (Session["PageIndex1"] != null)
                {
                    intpageIndexdropdownpopup = Convert.ToInt32(Session["PageIndex1"].ToString());
                    Session["PageIndex"] = intpageIndexdropdownpopup;
                }
                else
                {
                    intpageIndexdropdownpopup = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndexdropdownpopup != 0)
                    {
                        Session["PageIndex"] = intpageIndexdropdownpopup;
                    }
                }

                if (intpageIndexdropdownpopup == 1)
                {
                    intrecFromdropdownpopup = 0;
                }
                else
                {
                    recFromProcessHistory1 = (intpageIndexdropdownpopup * intrecTodropdownpopup) - intrecTodropdownpopup;
                    intrecFromdropdownpopup = recFromProcessHistory1 + 1;
                    intrecTodropdownpopup = recFromProcessHistory1 + CommonFunctions.GridViewPagesize(stringformIdddlpopup);

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
            finally
            {
                recFromProcessHistory1 = 0;
            }
        } 
        #endregion
    }
}