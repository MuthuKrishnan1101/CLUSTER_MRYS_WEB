using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FC0008R1V1 : System.Web.UI.Page
    {
        public DataSet objDatasetAppsVariables;
        public int intpageIndex = 0;
        public int intrecFrom = 0;
        public int intrecTo = 0;

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)//fix
        {
            string stringRequestID = "";
            try
            {
              
                if (CommonFunctions.IsActive())
                {
                    if (Session["REQUESTID_PENDINGITEMS"] != null)
                    {
                        stringRequestID = Session["REQUESTID_PENDINGITEMS"].ToString();
                        Session["REQUESTID_PENDINGITEMS"] = stringRequestID;
                    }
                   
                    string stringformId = "FC0008R1V1gridviewpagesize";
                    intrecTo = CommonFunctions.GridViewPagesize(stringformId);

                    if (!IsPostBack)
                    {
                        txtCloseDate.ReadOnly = true;
                        if (txtCloseDate.ReadOnly == true)
                        {
                            txtCloseDate.CssClass = "form-control ReadOnly";
                        }
                        CommonFunctions.HeaderName(this, "FC0008R1V1");
                        VerifyAccessRights();
                        hdfmramount.Value = "";
                        hdfddlBlockBill.Value = "";
                        hdfddlWApp.Value = "";
                        hdfddlWApproved.Value = "";
                        hdfRecallcurreentStatus.Value = "";
                        ResetVariables();
                        ClearValues();
                        LoadPendingItems();
                        LoadStatus();
                        LoadRegRequestInfo(stringRequestID);

                        LoadAllDATA(stringRequestID);
                        Session["bool"] = true;

                        if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim().Length > 0)
                        {
                            LoadDatavalues(Request.QueryString["ID"].ToString());
                        }
                    }
                    else
                    {
                        Session["bool"] = false;
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
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (Session["bool"] != null)
                {
                    bool boolsts = (bool)Session["bool"];
                    string stringRequestID1 = "";
                    if (Session["REQUESTID_PENDINGITEMS"] != null)
                    { stringRequestID1 = Session["REQUESTID_PENDINGITEMS"].ToString(); }

                    if (boolsts == true)
                    {
                        LoadGrid(stringRequestID1);
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
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
                stringComponent[0] = "FC0008R1V1";
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
        #endregion

        #region Image Button


        protected void imgBtnNew_Click(object sender, ImageClickEventArgs e)//fix
        {
            ClearValues();
        }

        protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)//fix
        {
            SvePendingItems();

        }

        private void SvePendingItems()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            string stringpenid = "";
            string stringDMLIND = "";
            DataRow objdatarow = null;
            bool boolRecExist = false;
            string stringexp = "";
            string stringStatus = "";
            try
            {
                if (ValidateControls() && ValidateBusinessLogic())
                {
                    if (ddlPenItems.SelectedItem != null)
                    {

                        stringpenid = ddlPenItems.SelectedValue.ToString();
                    }
                    if (Session["stringDMLIndicator"].ToString() == "I")
                    { boolRecExist = IsRecordExist(txtRequestNo.Text.Trim(), stringpenid); }

                    if (!boolRecExist)
                    {
                        stringServiceType = "DEFAULT";
                        stringexp = "";
                        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                        if (objDatasetResult != null && objDatasetResult.Tables["t13"].Rows.Count == 0)
                        {
                            objdatarow = objDatasetResult.Tables["t13"].NewRow();
                            objdatarow["be_id"] = CommonFunctions.GETBussinessEntity().ToString();

                            objdatarow["PenSeq_ID"] = hidFldPenSeq_ID.Value;
                            if (ddlPenItems.SelectedItem != null && ddlPenItems.SelectedValue.ToString().Length > 0)
                            {
                                objdatarow["Pen_ID"] = ddlPenItems.SelectedValue.ToString().ToUpper();
                            }
                            objdatarow["Request_ID"] = txtRequestNo.Text.Trim();
                            if (ddlStatus.SelectedItem != null && ddlStatus.SelectedValue.ToString().Length > 0)
                            {
                                stringStatus = ddlStatus.SelectedValue.ToString();
                                objdatarow["Pending_Status"] = stringStatus;
                            }

                            if (txtCloseDate.Text.Trim().Length > 0)
                            { objdatarow["Close_Date"] = CommonFunctions.ConvertToDateTime(txtCloseDate.Text.Trim().ToString(), "dd-MM-yyyy"); }
                            if (stringStatus != null && stringStatus.Trim().ToUpper() == "CLOSED")
                            { objdatarow["Close_Date"] = DateTime.Now; }
                            if (txtStartDate.Text.Trim().Length > 0)
                            { objdatarow["reference_date_1"] = CommonFunctions.ConvertToDateTime(txtCloseDate.Text.Trim().ToString(), "dd-MM-yyyy"); }
                            objdatarow["Due_days"] = Convert.ToInt32(txtDueDays.Text.Trim());
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

                            objDatasetResult.Tables["t13"].Rows.Add(objdatarow);
                            if (Session["stringDMLIndicator"] != null)
                            {
                                stringDMLIND = (string)Session["stringDMLIndicator"];
                            }
                            if (stringDMLIND == "U")
                            {
                                objDatasetResult.Tables["t13"].AcceptChanges();
                                objDatasetResult.Tables["t13"].Rows[0]["delmark"] = "N";
                            }


                            objDatasetResult.Tables["t13"].Rows[0].RowState.ToString();

                            objDatasetResult = objDatasetResult.GetChanges();
                            stringServiceType = "OperationServiceDML";
                            objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);
                            if (intErrorCount == 0)
                            {
                                Session["bool"] = true;
                                Session["REQUESTID_PENDINGITEMS"] = txtRequestNo.Text.Trim();
                                if (Session["stringDMLIndicator"].ToString() == "I")
                                {
                                    CommonFunctions.ShowMessageboot(this, "Record saved successfully");
                                }
                                else
                                {
                                    CommonFunctions.ShowMessageboot(this, "Record updated successfully");
                                }

                                ClearValues();
                            }
                            else
                            {
                                Errorpopup(stringOutputResult);
                            }

                        }

                    }
                    else { CommonFunctions.ShowMessageboot(this, "Item already exist"); }
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
                stringpenid = null;
                stringDMLIND = null;
                objdatarow = null;
                stringexp = null;
                stringStatus = null;
            }
        }

        private bool IsRecordExist(string stringreqID, string stringpenID)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "";
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
                stringServiceType = "List12R1V1";
                if (stringpenID.Length > 0)
                {
                    stringExpression = "and mrpend.be_id='" + stringBoID + "' and mrpend.request_id='" + stringreqID + "' and mrpend.pen_id='" + stringpenID + "'";

                }
                else
                {
                    stringExpression = "and mrpend.be_id='" + stringBoID + "' and mrpend.request_id='" + stringreqID + "' ";

                }
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t12"] != null && objDatasetResult.Tables["t12"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t12"];
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = null;
                objDataTable = null;
                stringExpression = null;
                stringBoID = null;
            }
            return false;
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

         

        protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)//fix
        {
            ClearValues();
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
            string stringServiceType = "";
            DataTable objDataTable = null;
            string stringExpression = "";
            string stringBoID = "";
            DataRow objDataRow = null;
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (hidFldPenSeq_ID.Value.Trim().Length > 0)
                {
                    stringServiceType = "List12R1V1";

                    stringExpression = "and mrpend.be_id='" + stringBoID + "' and mrpend.penseq_id='" + hidFldPenSeq_ID + "' ";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t12"] != null && objDatasetResult.Tables["t12"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t12"];
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
                stringServiceType = null;
                objDataTable = null;
                stringExpression = null;
                stringBoID = null;
                objDataRow = null;
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

        #region DropDown List

        protected void ddlPenItems_SelectedIndexChanged(object sender, EventArgs e)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0020R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            DataTable objDataTable = null;
            string stringID = "";
            string stringcondition = "";
            string stringServiceType = "";
            DataRow objDataRow = null;
            try
            {
                if (ddlPenItems.SelectedIndex >= 0)
                {
                    stringID = ddlPenItems.SelectedValue.ToString();
                    stringcondition = "And peits.be_id= '" + stringbeid + "' And peits.pen_id= '" + stringID.ToString() + "'";

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
                            txtDueDays.Text = objDataRow["due_days"].ToString();
                            if (ddlStatus.Items.FindByText("PENDING") != null)
                            {
                                ddlStatus.ClearSelection(); 
                                ddlStatus.Items.FindByText("PENDING").Selected = true;
                                txtCloseDate.Text = "";

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
                    txtDueDays.Text = "0";
                    ddlStatus.SelectedIndex = 0;
                    txtCloseDate.Text = "";
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
                stringID = null;
                stringcondition = null;
                stringServiceType = null;
                objDataRow = null;
            }
        }

        #endregion

        #region Gridview
         
        protected void GvList_Sorting(object sender, GridViewSortEventArgs e)//fix
        {

            string stringBoID = "";
            DataTable SortTable = null;
            try
            {
                stringBoID = "";
                if (Session["BusinessID"] != null)
                {
                    stringBoID = Session["BusinessID"].ToString();
                }
                Session["stringSortExpression"] = e.SortExpression;
                if (Session["stringSortDirection"].ToString() == "ASC") { Session["stringSortDirection"] = "DESC"; }
                else { Session["stringSortDirection"] = "ASC"; }
                SortTable = (DataTable)Session["SortTable"];
                if (SortTable != null)
                {
                    Session["SortTable"] = CommonFunctions.SortData(SortTable, e.SortExpression + " " + Session["stringSortDirection"].ToString(), stringBoID);
                    gvList.DataSource = (DataTable)Session["SortTable"];
                    gvList.DataBind();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringBoID = null;
                SortTable = null;
            }
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
            string stringReportTypID = "";
            string stringStatus = "";
            string stringMRamt = ""; 
            string stringUserID = "";
            string stringUserDesc = "";
            ViewState["DEPTOU"] = null;
            string stringcancellstatus = "";
            try
            {
                if (stringRequestNo != null && stringRequestNo.Trim().Length > 0)
                {
                    stringServiceType1 = "List5R1V1";
                    stringexp012 = " And mrreg.be_id= '" + stringbeid + "'  And mrreg.request_id= '" + stringRequestNo.ToString() + "'";

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
                                LoadProcesstabData(objDataRow);
                                ReportRelatedValues(objDataRow);

                                ViewState["DEPTOU"] = objDataRow["DEPT_ID"].ToString();
                                if (objDataRow["receive_date"] != null && objDataRow["receive_date"].ToString().Trim().Length > 0)
                                { txtRecDate.Text = Convert.ToDateTime(objDataRow["receive_date"]).ToString("dd-MM-yyyy"); }

                                if (objDataRow["due_date"] != null && objDataRow["due_date"].ToString().Trim().Length > 0)
                                { txtDueDate.Text = Convert.ToDateTime(objDataRow["due_date"]).ToString("dd-MM-yyyy"); }


                                stringReportTypID = objDataRow["rpttyp_id"].ToString();
                                txtRptType.ToolTip = stringReportTypID;
                                txtRptType.Text = objDataRow["report_type_short_name"].ToString();
                                if (objDataRow["PRIORITY_FLAG"].ToString() == "Y")
                                {
                                    chkpriorityflag.Checked = true;
                                }
                                else
                                {
                                    chkpriorityflag.Checked = false;
                                }
                                txtMRNumberHEADER.Text = objDataRow["MR_ID"].ToString();
                                stringStatus = objDataRow["mr_status"].ToString();
                                txtMRStatus.Text = stringStatus;
                                stringMRamt = objDataRow["MR_AMOUNT"].ToString();
                                
                                hdfmramount.Value = stringMRamt;
                                hdfddlBlockBill.Value = objDataRow["Block_Billing"].ToString();
                                hdfddlWApp.Value = objDataRow["WAIVER_STATUS_1"].ToString();
                                hdfddlWApproved.Value = objDataRow["WAIVER_APPROVED"].ToString();
                                txtHRN.Text = objDataRow["hrn_id"].ToString();
                                txtPatName.Text = objDataRow["patient_short_name"].ToString();
                                if (objDataRow["start_date"] != null && objDataRow["start_date"].ToString().Trim().Length > 0)
                                { txtStartDate.Text = Convert.ToDateTime(objDataRow["start_date"]).ToString("dd-MM-yyyy"); } 

                                txtReqContact.Text = objDataRow["requested_by"].ToString();
                                stringUserID = objDataRow["created_by"].ToString();
                                stringUserDesc = objDataRow["created_by"].ToString();
                                if (stringUserDesc != null && stringUserDesc.Trim().Length > 0) { txtCreateBy.Text = stringUserDesc; }
                                txtCreateBy.Text = objDataRow["created_by"].ToString();
                                txtCreateBy.ToolTip = stringUserID;
                                string  stringDelMode = objDataRow["delmod_id"].ToString();
                                txtDelToID.Text = stringDelMode;
                                stringcancellstatus = objDataRow["sup_status"].ToString();
                                if (stringStatus.Trim().ToUpper() == "FORWARDED" || stringStatus.Trim().ToUpper() == "COLLECTED" || stringStatus.Trim().ToUpper() == "CANCELLED" || stringcancellstatus.ToUpper() == "PENDING")
                                { ControlsEnabledByProStatus("CANCELLED"); }
                                else { } 
                            }
                        }
                        else
                        {
                            txtRequestNo.Text = txtMRNumberHEADER.Text = txtMRStatus.Text  = "";
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                objDataTable = null;
                stringbeid = null;
                stringServiceType1 = null;
                stringexp012 = null;
                objDataRow = null;
                stringReportTypID = null;
                stringStatus = null;
                stringMRamt = null; 
                stringUserID = null;
                stringUserDesc = null;
                stringcancellstatus = null;
            }
        }

        
        private void LoadDatavalues(string stringSequenceID) 
        {
            string stringTemp = "";
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            DataTable objDataTable = null;
            string stringExpression = "";
            string stringBoID = "";
            DataRow objDataRow = null;
            string stringformid01 = "";
            string stringID = "";
            string stringcondition = "";
            string stringServiceType1 = "";
            if (Session["BusinessID"] != null)
            {
                stringBoID = Session["BusinessID"].ToString();
            }
            try
            {
                if (stringSequenceID != null && stringSequenceID.Trim().Length > 0)
                {
                    stringServiceType = "List12R1V1";

                    stringExpression = "and mrpend.be_id='" + stringBoID + "' and mrpend.penseq_id='" + stringSequenceID + "' ";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t12"] != null && objDatasetResult.Tables["t12"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t12"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {

                            objDataRow = objDataTable.Rows[0];
                            hidFldPenSeq_ID.Value = stringSequenceID;
                            ddlPenItems.ClearSelection();
                            stringTemp = objDataRow["pen_id"].ToString();
                            if (stringTemp != null && stringTemp.Trim().Length > 0)
                            {
                                if (ddlPenItems.Items.FindByValue(stringTemp) != null)
                                {
                                    ddlPenItems.Items.FindByValue(stringTemp).Selected = true;
                                }
                                else
                                {
                                    stringTemp = objDataRow["pen_id"].ToString();
                                    stringformid01 = "FA0020R1V1";
                                    if (ddlPenItems.SelectedItem != null)
                                    {
                                        stringID = ddlPenItems.SelectedValue.ToString();
                                    }
                                    
                                    stringcondition = "and peits.be_id='" + stringBoID + "' And peits.delmark= 'N' ";

                                    stringServiceType1 = "List1R1V1";

                                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid01, stringcondition, "peits.ORDER_ID asc,peits.short_name asc", intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                                    if (interrorcount == 0)
                                    {
                                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                                        {
                                            objDataTable = objDatasetResult.Tables["t1"];
                                        }
                                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                                        {
                                            ddlPenItems.DataTextField = "short_name";
                                            ddlPenItems.DataValueField = "pen_id";
                                            ddlPenItems.DataSource = objDataTable;
                                            ddlPenItems.DataBind();
                                            ddlPenItems.Items.Insert(0, new ListItem("", ""));

                                            foreach (DataRow objDataRowUpdate in objDataTable.Select("pen_id ='" + stringTemp + "'"))
                                            {
                                                stringTemp = objDataRowUpdate["short_name"].ToString();
                                            }
                                        }

                                        ddlPenItems.SelectedItem.Text = stringTemp;
                                    }
                                    else
                                    {
                                        Errorpopup(stringOutputResult);
                                    }

                                }
                            }

                            txtDueDays.Text = objDataRow["due_days"].ToString();
                            if (objDataRow["close_date"] != null && objDataRow["close_date"].ToString().Trim().Length > 0)
                            { txtDueDate.Text = Convert.ToDateTime(objDataRow["close_date"]).ToString("dd-MM-yyyy"); }

                            if (objDataRow["Close_Date"] != null && objDataRow["Close_Date"].ToString().Trim().Length > 0)
                            { txtCloseDate.Text = Convert.ToDateTime(objDataRow["Close_Date"]).ToString("dd-MM-yyyy"); }

                            ddlStatus.ClearSelection();
                            stringTemp = objDataRow["pending_status"].ToString();
                            if (stringTemp != null && stringTemp.Trim().Length > 0)
                            {
                                if (ddlStatus.Items.FindByValue(stringTemp) != null)
                                { ddlStatus.Items.FindByValue(stringTemp).Selected = true;
                                }
                            }
                            if(stringTemp == "PENDING")
                            {
                                txtCloseDate.Text = "";
                            }

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
                stringTemp = null;
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
                objDataRow = null;
                stringformid01 = null;
                stringID = null;
                stringcondition = null;
                stringServiceType1 = null;
            }

        }

        private void LoadGrid(string stringRequestID)//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "mrpend.modified_on desc";
            int intFromRecord = intrecFrom;
            int intToRecord = intrecTo;
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

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringExpression, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    PopulatePager(intTotalRecord, intpageIndex);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t12"] != null && objDatasetResult.Tables["t12"].Rows.Count > 0)
                        {
                            objDataTable = objDatasetResult.Tables["t12"];
                        }
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            Session["SortTable"] = objDataTable;
                            Session["PendingItemsList"] = objDataTable;
                            gvList.DataSource = objDataTable;
                            gvList.DataBind();
                            pnlresultgrid.Visible = true;
                            lblTotalRecords.InnerText = objDataTable.Rows.Count.ToString();
                        }
                        else
                        {
                            lblTotalRecords.InnerText = "0";
                            gvList.DataSource = null;
                            gvList.DataBind();
                            pnlresultgrid.Visible = false;
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
                intToRecord = int.MaxValue;
                stringServiceType = null;
                objDataTable = null;
                stringExpression = null;
                stringBoID = null;
            }

        }

        private void LoadPendingItems()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0020R1V1";
            string stringOrderBy = "peits.ORDER_ID asc,peits.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                ddlPenItems.Items.Clear();
                stringcondition = "And peits.be_id= '" + stringbeid + "'  And peits.delmark= 'N'";

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
                        ddlPenItems.DataTextField = "short_name";
                        ddlPenItems.DataValueField = "pen_id";
                        ddlPenItems.DataSource = objDataTable;
                        ddlPenItems.DataBind();
                        ddlPenItems.Items.Insert(0, new ListItem("", ""));
                        ddlPenItems.SelectedIndex = 1;
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
                stringcondition = null;
                stringServiceType = null;
            }

        }

        private void LoadStatus()//fix
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0014R1V1";
            string stringOrderBy = "lst.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                ddlStatus.Items.Clear();

                stringcondition = "And lst.be_id= '" + stringbeid + "'  AND  lst.LSTGRP_ID like '%PENDING-ITEM-STATUS%' AND lst.delmark='N' ";


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
                        ddlStatus.DataTextField = "short_name";
                        ddlStatus.DataValueField = "lst_id";
                        ddlStatus.DataSource = objDataTable;
                        ddlStatus.DataBind();
                        ddlStatus.Items.Insert(0, new ListItem("", ""));
                        ddlStatus.SelectedIndex = 1;
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
                stringcondition = null;
                stringServiceType = null;
            }
        }

        private void ControlsEnabledByProStatus(string stringStatus)//fix
        {
            switch (stringStatus)
            {
                case "CANCELLED":
                    {
                        imgbtnSave.Enabled = false;
                        //imgbtnNew.Enabled = false;
                        //imgbtnprint.Enabled = false;
                        //imgbtnSearch.Enabled = false;
                        imgbtnexport.Enabled = false;
                        lnkbtnAdd.Enabled = false; 
                        //ImageButton1.Enabled = false; 

                        ddlPenItems.Enabled = false; 
                        ddlPenItems.CssClass = "form-control ReadOnly";
                        txtDueDays.ReadOnly = true;
                        if (txtDueDays.ReadOnly == true)
                        {
                            txtDueDays.CssClass = "form-control ReadOnly";
                        }
                        ddlStatus.Enabled = false; 
                        ddlStatus.CssClass = "form-control ReadOnly"; 
                        txtStartDate_CalendarExtender.Enabled = false;
                        txtCloseDate_CalendarExtender.Enabled = false;
                        txtCloseDate.ReadOnly = true;
                        if (txtCloseDate.ReadOnly == true)
                        {
                            txtCloseDate.CssClass = "form-control ReadOnly";
                        }
                        txtStartDate.ReadOnly = true;
                        if (txtStartDate.ReadOnly == true)
                        {
                            txtStartDate.CssClass = "form-control ReadOnly";
                        }

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
                    stringScript += "','anycontent','width=300,height=310,left=16,top=16,status,location=no,diintrecTories=no,";
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
                Session["stringFormID"] = "FC0004R1V1";
                Session["stringFormName"] = "MR PENDING ITEMS";
            }
            catch (Exception objException) { CommonFunctions.HandleException(objException); }
            
        }

        private void ClearValues()//fix
        {
            try
            {
                ddlStatus.ClearSelection();
                ddlPenItems.ClearSelection();
                txtDueDays.Text = "";
                txtCloseDate.Text = "";
                //txtCloseDate.Text = DateTime.Now.ToString("dd-MM-yyyy"); 
                Session["stringDMLIndicator"] = "I";
                txtRequestNo.Focus();
            }
            catch (Exception objException) { CommonFunctions.HandleException(objException); }
           
        } 
        private bool ValidateControls()//fix
        {
            string stringOverallMsg = "";
            bool boolStatus = true;
            try
            {
                boolStatus = true;

                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";
                if (ddlPenItems.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Pending Items" + "\\r\\n";
                    boolStatus = false;
                }

                if (txtDueDays.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- DueDays" + "\\r\\n";
                    boolStatus = false;
                }

                if (ddlStatus.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Pending Status" + "\\r\\n";
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
                stringOverallMsg = null; 
            }
            
            return boolStatus;
        }

        private bool ValidateBusinessLogic()//fix
        {
            bool boolStatus = true;

            try
            {
                if (txtCloseDate.Text.Trim().Length > 0 && !CommonFunctions.ValidDateTime(txtCloseDate.Text.Trim()))
                {
                    CommonFunctions.ShowMessageboot(this, "Close date Should be a valid date");
                    txtCloseDate.Focus();
                    return false;
                }
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
                CommonFunctions.HandleException(objException);
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
                    intrecTo = CommonFunctions.GridViewPagesize("FC0008R1V1gridviewpagesize");
                }
                else
                {
                    int intrecFrom1 = (intpageIndex * intrecTo) - intrecTo;
                    intrecFrom = intrecFrom1 + 1;
                    intrecTo = intrecFrom1 + CommonFunctions.GridViewPagesize("FC0008R1V1gridviewpagesize");
                }
                //search(intrecFrom, intrecTo);
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
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

        protected void lblpendingshortname_Click(object sender, EventArgs e)
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

                            if (stringReciptid.Length > 0)
                            {
                                LoadDatavalues(stringReciptid);
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
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = int.MaxValue;
                objDataTable = null;
                stringServiceType = null;
                stringexp012 = null;
                stringbeid = null;
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
            string stringprocessname = "";
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            string stringexp = "";
            string stringRequestID = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
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
                stringServiceType = null;
                stringexp = null;
                stringRequestID = null;
                stringbeid = null;
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
            bool boolAssignDoctor = true;
            bool boolpendingreport = true;
            bool boolpaymentpendingItems = true;
            bool boolEnquirypendingItems = true;
            DataRow[] objdatarow = null;
            string stringMRamt = "";
            string stringbalanceamt = "";
            int intbalanceamt = 0; 
            DataTable objdatatable = null;
            string stringRequestID = "";
            bool boolpendingverifier = true;
            bool boomdeptou = true;
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
                    if ((stringTransSattus.ToUpper() == "PENDING DESPATCH" && txtBypassPenItems.Text.Trim().ToString().ToUpper() == "N") || stringTransSattus.ToUpper() == "PENDING FORWARDING")
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
                        if (CheckVerifierComplete(txtRequestNo.Text.Trim()))
                        {
                            boolpendingverifier = false;
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
                stringMRamt = null;
                stringbalanceamt = null;
                intbalanceamt = 0; 
                objdatatable = null;
                stringRequestID = null;
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
            string stringProcessID = "";
            string stringduedte = "";
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
                stringDeliverBy = null;
                stringProcessID = null;
                stringduedte = null;
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
        private DataTable GetRequestDetails(string stringReqID) 
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
        #endregion

        #endregion

        #endregion

        protected void btncloseprocess_Click(object sender, EventArgs e)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";  
            DataRow objdatarow = null;
            string stringCmdArgument = "";
            string stringPenSeq_ID = "";
            string stringPen_ID = "";
            string stringRequest_ID = "";
            string stringStatus = "";
            string stringDue_days = "";
            string stringexp = "";
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
                            stringPenSeq_ID = stringValues[0];
                            stringPen_ID = stringValues[1];
                            stringRequest_ID = stringValues[2];
                            stringStatus = stringValues[3];
                            stringDue_days = stringValues[4];
                            if (stringPenSeq_ID.Length > 0 && stringPen_ID.Length > 0 && stringRequest_ID.Length > 0)
                            { 
                                stringServiceType = "DEFAULT";
                                stringexp = "";
                                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                                if (objDatasetResult != null && objDatasetResult.Tables["t13"].Rows.Count == 0)
                                {
                                    objdatarow = objDatasetResult.Tables["t13"].NewRow();
                                    objdatarow["be_id"] = CommonFunctions.GETBussinessEntity().ToString();
                                    objdatarow["PenSeq_ID"] = stringPenSeq_ID;
                                    objdatarow["Pen_ID"] = stringPen_ID;
                                    objdatarow["Request_ID"] = stringRequest_ID;
                                    objdatarow["Pending_Status"] = "CLOSED";
                                    objdatarow["Close_Date"] = CommonFunctions.ConvertToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), "dd-MM-yyyy");
                                    objdatarow["reference_date_1"] = CommonFunctions.ConvertToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), "dd-MM-yyyy");
                                    objdatarow["Due_days"] = stringDue_days;
                                    objdatarow["delmark"] = "N";
                                    if (Session["stringComputerName"] != null)
                                        objdatarow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                    if (Session["stringUserID"] != null)
                                        objdatarow["MODIFIED_BY"] = Session["stringUserID"].ToString();
                                    objdatarow["MODIFIED_ON"] = DateTime.Now;

                                    objDatasetResult.Tables["t13"].Rows.Add(objdatarow);
                                    objDatasetResult.Tables["t13"].AcceptChanges();
                                    objDatasetResult.Tables["t13"].Rows[0]["delmark"] = "N";
                                    objDatasetResult.Tables["t13"].Rows[0].RowState.ToString();

                                    objDatasetResult = objDatasetResult.GetChanges();
                                    stringServiceType = "OperationServiceDML";
                                    objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);
                                    if (intErrorCount == 0)
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Record updated successfully");
                                        ClearValues();
                                        LoadGrid(stringRequest_ID);
                                    }
                                    else
                                    {
                                        Errorpopup(stringOutputResult);
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
                stringCmdArgument = null;
                stringPenSeq_ID = null;
                stringPen_ID = null;
                stringRequest_ID = null;
                stringStatus = null;
                stringDue_days = null;
                stringexp = null;
                stringValues = null;
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringSort = string.Empty;
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
                     if (objDataRow["Pending_Status"].ToString() == "PENDING")
                    { ((Label)e.Row.FindControl("lblClosedte")).Text = ""; }

                    Button objbtnComplete = e.Row.FindControl("btnclose") as Button;  
                    if (txtMRStatus.Text.Trim().ToUpper() == "CANCELLED")
                    {
                        objbtnComplete.Enabled = false;
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

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stringsts = "";
            try
            {
                if (ddlStatus.SelectedItem != null && ddlStatus.SelectedValue.ToString().Length > 0)
                {
                    stringsts = ddlStatus.SelectedValue.ToString();
                    if (stringsts == "CLOSED")
                    {
                        txtCloseDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    }
                    else if (stringsts == "PENDING")
                    {
                        txtCloseDate.Text = "";
                    }
                }
                else
                {
                    txtCloseDate.Text = "";
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringsts = null;
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
                        Session["defauledviewstatus"] = null;
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
                        stringnotreadyforaccessment = objdatarow01[0]["WIC_FLAG"].ToString(); 
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
        private void ControlByStatus(string stringStatus, string stringDeliveryMode) 
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
                        stringInputs[3] = "";
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
                        //stringInputs[4] = "";
                        //stringInputs[5] = "";
                    }
                    else if (stringFORMID == "DOP700002R1V1")
                    {
                        stringInputs = new string[4];
                        stringInputs[0] = CommonFunctions.GETBussinessEntity();
                        stringInputs[1] = stringRequestID01;
                        stringInputs[2] = "";
                        stringInputs[3] = "";
                        //stringInputs[4] = "";
                        //stringInputs[5] = "";
                    }
                    else if (stringFORMID == "DOP700015R1V1")
                    {
                        stringInputs = new string[4];
                        stringInputs[0] = CommonFunctions.GETBussinessEntity();
                        stringInputs[1] = stringRequestID01;
                        stringInputs[2] = "";
                        stringInputs[3] = "";
                        //stringInputs[4] = "";
                        //stringInputs[5] = "";
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

                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
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