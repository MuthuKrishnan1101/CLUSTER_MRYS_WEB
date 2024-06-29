using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

namespace CLUSTER_MRTS
{
    public partial class FE0003R1V1 : System.Web.UI.Page
    {
        public DataSet objDatasetAppsVariables; 

        public int intpageIndextemperrordetail = 0;
        public int intrecFromtemperrordetail = 0;
        public int intrecTotemperrordetail = 0;
        string stringformId = "Orderhistorygridviewpagesize";
        public string stringformIdPagingtemperrordetail = "MRPaymentViewPopupPaging";

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {

            if (CommonFunctions.IsActive())
            {
                string stringformId = "";
                string stringOverallExpression = "";
                try
                {
                    intrecTotemperrordetail = CommonFunctions.GridViewPagesize(stringformIdPagingtemperrordetail);

                    if (!IsPostBack)
                    { 
                        VerifyAccessRights();
                        hdnfrom.Value = "0";
                        hdnpageIndex.Value = "0";
                        hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();

                        CommonFunctions.HeaderName(this, "FE0003R1V1");
                        ViewState["vsSortDirection"] = " ASC";
                        ViewState["vsSearchCondition"] = "";
                        ViewState["vsSortExpression"] = "";
                        ViewState["ERRORBATCCH_ID"] = "";
                        ViewState["particulartabcondition"] = "PENDING TRACING";
                        ViewState["bool"] = true;
                        LoadAdvancedSearchColumnNames();//for column name filters ddl
                        LoadAdvancedSearchConditions();//for condition ddl
                        LoadAdvancedSearchGrid();//for default 1st line
                        //string stringOverallExpression = "and mrreg.mr_status IN ('PENDING TRACING','PENDING DESPATCH','PENDING FORWARDING','PENDING ASSIGNED')";
                        stringOverallExpression = "and mrreg.mr_status IN ('PENDING TRACING')";
                        LoadRecord("LOADDEFAULT", stringOverallExpression, "priority_flag desc,CREATED_ON desc");
                    }
                    else
                    {
                        ViewState["bool"] = true;

                    }
                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }
                finally
                {
                    //stringformId = "";
                    //stringOverallExpression = "";
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
            imgbtnprint.Visible= false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FE0003R1V1";
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
        protected void Page_PreRender(object sender, EventArgs e)
        {
            string stringparticulartabcondition = "";
            try
            {
                if (ViewState["bool"] != null)
                {
                    bool boolsts = (bool)ViewState["bool"];

                    if (boolsts == false)
                    {
                        if (ViewState["particulartabcondition"] != null)
                        {
                            stringparticulartabcondition = ViewState["particulartabcondition"].ToString();
                        }
                        //if (stringparticulartabcondition.Length == 0)
                        //{
                        //    if (ViewState["vsSearchCondition"] != null)
                        //    {
                        //        stringparticulartabcondition = ViewState["vsSearchCondition"].ToString();
                        //    }
                        //}
                        PrepareSearchExpression(stringparticulartabcondition);


                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                //stringparticulartabcondition = "";
                // boolsts = true;
            }

        }
        #endregion
        #region auto gridbindings/linkbtn
        private void BindGrid()
        {
            DataTable objdatatableGridbing = null;
            try
            {
                if (ViewState["FE0003R1V1_idGrid"] != null)
                {
                    objdatatableGridbing = (DataTable)ViewState["FE0003R1V1_idGrid"];

                    gvUserHistory.DataSource = objdatatableGridbing;
                    gvUserHistory.DataBind();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objdatatableGridbing = null;
            }
        }

        #endregion

        #region for top condition list

        private void LoadAdvancedSearchColumnNames()
        {
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDataSet = null;
            string[] stringOutputResult = null;
            string stringServiceType = "RETRIVELOV";
            string stringFormID = "COMMONLOVLISTREFERENCES";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringFORMID = "";
            string stringexp = "";
            if (Request.QueryString["ID"] != null)
            {
                stringFORMID = Request.QueryString["ID"].ToString();
            }
            try
            {
                stringOutputResult = new string[3];
                stringexp = "And lst.lstgrp_id='" + stringFORMID + "'And lst.delmark='N'";

                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringFormID, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                    {
                        objDataSet.Tables[0].DefaultView.Sort = "priority asc,short_name asc";
                        objDataTable = objDataSet.Tables[0].DefaultView.ToTable();

                        if (ViewState["ADVSEARCHCOLUMNS"] != null) { ViewState.Remove("ADVSEARCHCOLUMNS"); }
                        ViewState.Add("ADVSEARCHCOLUMNS", objDataTable);
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
                intTotalRecord = 0;
                objDataSet = null;
                stringOutputResult = null;
                stringServiceType = "";
                stringFormID = "";
                stringOrderBy = "";
                stringexp = "";
            }
        }
        private void LoadAdvancedSearchConditions()
        {
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDataSet = null;
            string[] stringOutputResult = null;
            string stringServiceType = "RETRIVELOV";
            string stringFormID = "COMMONLOVLISTREFERENCES";
            string stringOrderBy = "";
            string stringexp = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            try
            {
                stringOutputResult = new string[3];
                stringexp = "And lst.lstgrp_id='GROUP0007'";
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringFormID, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                    {
                        objDataSet.Tables[0].DefaultView.Sort = "priority asc,short_name asc";
                        objDataTable = objDataSet.Tables[0].DefaultView.ToTable();

                        if (ViewState["ADVSEARCHCONDITIONS"] != null) { ViewState.Remove("ADVSEARCHCONDITIONS"); }
                        ViewState.Add("ADVSEARCHCONDITIONS", objDataTable);
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
                intTotalRecord = 0;
                objDataSet = null;
                stringOutputResult = null;
                stringServiceType = "";
                stringFormID = "";
                stringOrderBy = "";
                stringexp = "";
            }
        }
        private void LoadAdvancedSearchGrid()
        {
            DataTable objDataTable = null;
            DataRow objDataRowNew = null;
            try
            {
                if (Session["AdvancedSearchData"] != null) { Session.Remove("AdvancedSearchData"); }

                objDataTable = new DataTable();
                DataColumn objDataColumn = new DataColumn("SNo", typeof(long));
                objDataColumn.AutoIncrement = true;
                objDataColumn.AutoIncrementSeed = 1;
                objDataColumn.AutoIncrementStep = 1;
                objDataTable.Columns.Add(objDataColumn);

                objDataTable.Columns.Add("column_name");
                objDataTable.Columns.Add("condition");
                objDataTable.Columns.Add("search_value");

                objDataRowNew = objDataTable.NewRow();
                objDataTable.Rows.Add(objDataRowNew);
                Session["AdvancedSearchData"] = objDataTable;

                gvAdvancedSearch.DataSource = objDataTable;
                gvAdvancedSearch.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDataTable = null;
                objDataRowNew = null;
            }
        }

        private void FillExistingSearchValues()
        {
            string stringColumnName = "";
            string stringCondition = "";
            string stringSearchInput = "";
            DataTable objDataTable = null;
            string stringSNo = "";
            string stringExpression = "";
            DataRow[] objDataRowFiltered = null;
            DataRow objDataRow = null;
            try
            {
                if (Session["AdvancedSearchData"] != null)
                {
                    objDataTable = (DataTable)Session["AdvancedSearchData"];
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        for (int intCount = 0; intCount < gvAdvancedSearch.Rows.Count; intCount++)
                        {
                            stringColumnName = "";
                            stringCondition = "";
                            stringSearchInput = "";

                            DropDownList objDropDownListColumnName = (DropDownList)gvAdvancedSearch.Rows[intCount].FindControl("ddlColumnName");
                            DropDownList objDropDownListCondition = (DropDownList)gvAdvancedSearch.Rows[intCount].FindControl("ddlCondition");
                            TextBox objTextBoxSearchInput = (TextBox)gvAdvancedSearch.Rows[intCount].FindControl("txtSearchInput");

                            if (objDropDownListColumnName != null && objDropDownListColumnName.SelectedValue != null)
                            { stringColumnName = objDropDownListColumnName.SelectedValue.Trim(); }

                            if (objDropDownListCondition != null && objDropDownListCondition.SelectedValue != null)
                            { stringCondition = objDropDownListCondition.SelectedValue.Trim(); }

                            if (objTextBoxSearchInput != null)
                            { stringSearchInput = objTextBoxSearchInput.Text.Trim(); }

                            stringSNo = ((HiddenField)gvAdvancedSearch.Rows[intCount].FindControl("hfSNo")).Value;
                            if (stringSNo != null && stringSNo.Trim().Length > 0)
                            {
                                stringExpression = " SNo='" + stringSNo.Trim() + "'";
                                objDataRowFiltered = objDataTable.Select(stringExpression);
                                if (objDataRowFiltered != null && objDataRowFiltered.Length > 0)
                                {
                                    objDataRow = objDataRowFiltered[0];
                                    objDataRow["column_name"] = stringColumnName;
                                    objDataRow["condition"] = stringCondition;
                                    objDataRow["search_value"] = stringSearchInput;
                                }
                            }
                        }

                        Session["AdvancedSearchData"] = objDataTable;
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
                stringCondition = "";
                stringSearchInput = "";
                objDataTable = null;
                stringSNo = "";
                stringExpression = "";
                objDataRowFiltered = null;
                objDataRow = null;
            }
        }
        private void LoadAdvSearchColumnsDropdown(DropDownList objDropDownlistColumnName)
        {
            DataTable objDataTableColumns = null;
            try
            {
                if (ViewState["ADVSEARCHCOLUMNS"] != null)
                {
                    objDataTableColumns = (DataTable)ViewState["ADVSEARCHCOLUMNS"];
                    if (objDropDownlistColumnName != null)
                    {
                        objDropDownlistColumnName.DataTextField = "short_name";
                        objDropDownlistColumnName.DataValueField = "lst_id";
                        objDropDownlistColumnName.DataSource = objDataTableColumns;
                        objDropDownlistColumnName.DataBind();
                        objDropDownlistColumnName.Items.Insert(0, new ListItem("", ""));
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDataTableColumns = null;
            }
        }

        private void LoadAdvSearchConditionsDropdown(DropDownList objDropDownlistConditions)
        {
            DataTable objDataTableColumns = null;
            try
            {
                if (ViewState["ADVSEARCHCONDITIONS"] != null)
                {
                    objDataTableColumns = (DataTable)ViewState["ADVSEARCHCONDITIONS"];
                    if (objDropDownlistConditions != null)
                    {
                        objDropDownlistConditions.DataTextField = "short_name";
                        objDropDownlistConditions.DataValueField = "lst_id";
                        objDropDownlistConditions.DataSource = objDataTableColumns;
                        objDropDownlistConditions.DataBind();

                        objDropDownlistConditions.Items.Insert(0, new ListItem("", ""));

                        if (objDropDownlistConditions.Items.FindByValue("LIKE") != null)
                        {
                            objDropDownlistConditions.ClearSelection();
                            objDropDownlistConditions.Items.FindByValue("LIKE").Selected = true;
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
                objDataTableColumns = null;
            }
        }
        protected void gvAdvancedSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringSort = string.Empty;
            DataRowView objDataRowView = null;
            DataRow objDataRow = null;
            string stringTempSerialNo = "";
            DataTable objDataTable = null;
            DataRow[] objDataRowFiltered = null;
            DataRow objDataRowCurrent = null;
            string stringColumnName = "";
            string stringCondition = "";
            string stringSearchValue = "";
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null)
                {
                    return;
                }
                else
                {
                    objDataRowView = ((DataRowView)e.Row.DataItem);
                    objDataRow = objDataRowView.Row;

                    stringTempSerialNo = ((HiddenField)e.Row.FindControl("hfSNo")).Value;
                    DropDownList objDropDownlistColumnName = (DropDownList)e.Row.FindControl("ddlColumnName");
                    DropDownList objDropDownlistCondition = (DropDownList)e.Row.FindControl("ddlCondition");
                    TextBox objTextboxSearchInput = (TextBox)e.Row.FindControl("txtSearchInput");

                    if (objDropDownlistColumnName != null) { LoadAdvSearchColumnsDropdown(objDropDownlistColumnName); }
                    if (objDropDownlistCondition != null) { LoadAdvSearchConditionsDropdown(objDropDownlistCondition); }

                    if (Session["AdvancedSearchData"] != null)
                    {
                        objDataTable = (DataTable)Session["AdvancedSearchData"];
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            objDataRowFiltered = objDataTable.Select("SNo='" + stringTempSerialNo + "'");
                            if (objDataRowFiltered != null && objDataRowFiltered.Length > 0)
                            {
                                objDataRowCurrent = objDataRowFiltered[0];
                                stringColumnName = objDataRowCurrent["column_name"].ToString();
                                stringCondition = objDataRowCurrent["condition"].ToString();
                                stringSearchValue = objDataRowCurrent["search_value"].ToString();

                                if (stringColumnName != null && stringColumnName.Trim().Length > 0)
                                {
                                    if (objDropDownlistColumnName != null)
                                    {
                                        objDropDownlistColumnName.ClearSelection();
                                        if (objDropDownlistColumnName.Items.FindByValue(stringColumnName) != null)
                                        { objDropDownlistColumnName.Items.FindByValue(stringColumnName).Selected = true; }
                                    }
                                }

                                if (stringCondition != null && stringCondition.Trim().Length > 0)
                                {
                                    if (objDropDownlistCondition != null)
                                    {
                                        objDropDownlistCondition.ClearSelection();
                                        if (objDropDownlistCondition.Items.FindByValue(stringCondition) != null)
                                        { objDropDownlistCondition.Items.FindByValue(stringCondition).Selected = true; }
                                    }
                                }

                                if (stringSearchValue != null && stringSearchValue.Trim().Length > 0)
                                {
                                    if (objTextboxSearchInput != null)
                                    { objTextboxSearchInput.Text = stringSearchValue; }
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
                stringSort = string.Empty;
                objDataRowView = null;
                objDataRow = null;
                stringTempSerialNo = "";
                objDataTable = null;
                objDataRowFiltered = null;
                objDataRowCurrent = null;
                stringColumnName = "";
                stringCondition = "";
                stringSearchValue = "";
            }
        }

        private void ClearValues()
        {
            try
            {
                for (int intCount = 0; intCount < gvAdvancedSearch.Rows.Count; intCount++)
                {
                    ((TextBox)gvAdvancedSearch.Rows[intCount].FindControl("txtSearchInput")).Text = "";
                }

                if (gvAdvancedSearch.Rows.Count > 0)
                { ((TextBox)gvAdvancedSearch.Rows[0].FindControl("txtSearchInput")).Focus(); }
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
                ClearValues();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }
        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            DataTable objDataTable = null;
            DataRow objDataRowNew = null;
            try
            {
                FillExistingSearchValues();
                if (Session["AdvancedSearchData"] != null)
                {
                    objDataTable = (DataTable)Session["AdvancedSearchData"];
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        if (objDataTable.Rows.Count < 10)
                        {
                            objDataRowNew = objDataTable.NewRow();
                            objDataTable.Rows.Add(objDataRowNew);
                            Session["AdvancedSearchData"] = objDataTable;

                            gvAdvancedSearch.DataSource = objDataTable;
                            gvAdvancedSearch.DataBind();
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "You cannot add more than 10 levels to search");
                            lbtnSearch.Focus();
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
                objDataTable = null;
                objDataRowNew = null;
            }
        }

        protected void lnkbtnRemove_Click(object sender, EventArgs e)
        {
            DataTable objDataTable = null;
            try
            {
                FillExistingSearchValues();
                if (Session["AdvancedSearchData"] != null)
                {
                    objDataTable = (DataTable)Session["AdvancedSearchData"];
                    if (objDataTable != null && objDataTable.Rows.Count > 1)
                    {
                        objDataTable.Rows[objDataTable.Rows.Count - 1].Delete();
                        Session["AdvancedSearchData"] = objDataTable;

                        gvAdvancedSearch.DataSource = objDataTable;
                        gvAdvancedSearch.DataBind();
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDataTable = null;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            string stringparticulartabcondition = "";
            try
            {
                hdnfrom.Value = "0";
                hdnpageIndex.Value = "0";
                hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();

                if (ViewState["particulartabcondition"] != null)
                {
                    stringparticulartabcondition = ViewState["particulartabcondition"].ToString();
                }
                PrepareSearchExpression(stringparticulartabcondition);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringparticulartabcondition = "";
            }
        }

        protected void lnkbtnClear_Click(object sender, EventArgs e)
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

        #region method


        private void LoadRecord(string stringTYPE = null, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {
            DataSet objDataSet = null;
            int intRecordCount = 0;
            string stringMRStatus = "";
            string stringuserid = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                ViewState["vsSearchConditions"] = Condition;
                intRecordCount = 0;
                if (Session["G11EOSUserID"] != null)
                {
                    stringuserid = Session["G11EOSUserID"].ToString();
                }
                if (Condition.Contains("PENDING SUP VETTING"))
                { 
                    Condition = "AND mrreg.be_id = '" + stringbeid + "'  AND mrassvr.emp_no ='" + stringuserid + "'  and mrassvr.VERIFY_REF = 'VERIFIER' AND mrassvr.STATUS = 'IN-PROGRESS'" + "  ";

                }
                ViewState["vsSortExpression"] = SortExpression;
                objDataSet = GetRecords(out intRecordCount, Condition, SortExpression, RecordFrom, RecordTo);
                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                {
                    if (stringTYPE.Length > 0 && stringTYPE == "LOADDEFAULT")
                    {
                        ViewState["SelectedRecordsMANTANIN"] = objDataSet.Tables[0].Clone();
                    }
                    stringMRStatus = objDataSet.Tables[0].Rows[0]["MR_STATUS"].ToString();  
                    if (Condition.Contains("PENDING TRACING") && stringMRStatus == "PENDING TRACING")
                    {
                        btntriggertab2.Text = "Pending Tracing ( " + intRecordCount.ToString() + " )";
                    }
                    else if (Condition.Contains("PENDING DESPATCH") && stringMRStatus == "PENDING DESPATCH")
                    {
                        btntriggertab3.Text = "Pending Despatch ( " + intRecordCount.ToString() + " )";
                    }
                    else if (Condition.Contains("PENDING FORWARDING") && stringMRStatus == "PENDING FORWARDING")
                    {
                        btntriggertab4.Text = "Pending Forward ( " + intRecordCount.ToString() + " )";
                    }
                    else if (Condition.Contains("PENDING ASSIGNED") && stringMRStatus == "PENDING ASSIGNED")
                    {
                        btntriggertab5.Text = "Pending Request to be Assign ( " + intRecordCount.ToString() + " )";
                    }
                    else if (Condition.Contains("DRAFT_FLAG"))
                    {
                        btntriggertab6.Text = "Draft ( " + intRecordCount.ToString() + " )";
                    }
                    else if (Condition.Contains("PENDING SUP VETTING"))
                    {
                        btntriggertab7.Text = "Pending Report Verification ( " + intRecordCount.ToString() + " )";
                    }

                    ViewState["FE0003R1V1_idGrid"] = objDataSet.Tables[0];

                    if (stringMRStatus == "PENDING TRACING")
                    {
                        btntriggertab2.BackColor = ColorTranslator.FromHtml("#397279");
                        btntriggertab1.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = btntriggertab6.BackColor = Color.FromArgb(99, 191, 247);
                        pnlTab1.Visible = true;
                    }
                    else if (stringMRStatus == "PENDING DESPATCH")
                    {
                        btntriggertab3.BackColor = ColorTranslator.FromHtml("#397279");
                        btntriggertab2.BackColor = btntriggertab1.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = btntriggertab6.BackColor = Color.FromArgb(99, 191, 247);
                        pnlTab1.Visible = true;

                    }
                    else if (stringMRStatus == "PENDING FORWARDING")
                    {
                        btntriggertab4.BackColor = ColorTranslator.FromHtml("#397279");
                        btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab1.BackColor = btntriggertab5.BackColor = btntriggertab6.BackColor = Color.FromArgb(99, 191, 247);
                        pnlTab1.Visible = true;
                    }
                    else if (stringMRStatus == "PENDING ASSIGNED")
                    {
                        btntriggertab5.BackColor = ColorTranslator.FromHtml("#397279");
                        btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab1.BackColor = btntriggertab6.BackColor = Color.FromArgb(99, 191, 247);
                        pnlTab1.Visible = true;
                    }
                    else if (stringMRStatus == "DRAFT")
                    {
                        ViewState["Draft"] = stringMRStatus.ToString();
                        btntriggertab6.BackColor = Color.FromArgb(57, 114, 121);
                        btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab1.BackColor = btntriggertab5.BackColor = Color.FromArgb(99, 191, 247);
                        pnlTab1.Visible = true;
                    }


                }
                else
                {
                    btntriggertab2.Text = "Pending Tracing";
                    btntriggertab3.Text = "Pending Despatch";
                    btntriggertab4.Text = "Pending Forward";
                    btntriggertab5.Text = "Pending Request to be Assign";
                    btntriggertab6.Text = "Draft";
                    ViewState["FE0003R1V1_idGrid"] = null;
                    gvUserHistory.DataSource = objDataSet;
                    gvUserHistory.DataBind();
                }

                this.BindGrid();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                //objDataSet = null;
                intRecordCount = 0;
                stringMRStatus = "";
            }
        }

        public DataSet GetRecords(out int intRecordCount, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {

            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringFORMID = "FE0003R1V2";
            intRecordCount = 0;
            int inthdnpageIndex = 0;
            int intRecordFrom = 0;
            int intRecordTo = int.MaxValue;
            string stringuserID = "";
            string stringmessage = "";
            intRecordFrom = Convert.ToInt32(hdnfrom.Value.ToString());
            intRecordTo = Convert.ToInt32(hdnto.Value.ToString());
            try
            {

                objDatasetAppsVariables = (DataSet)Session["objDatasetlocaldeclaration"];
                objDatasetAppsVariables.Tables[0].Rows[0]["FORM_ID"] = stringFORMID;

                Condition += "and mrreg.mr_status not in  ('FORWARDED','COLLECTED','CANCELLED')";
                stringuserID = HttpContext.Current.Session["G11EOSUserID"].ToString();
                if (Condition.Contains("DRAFT_FLAG"))
                {
                    Condition += "AND mrreg.MR_USER_ID='" + HttpContext.Current.Session["G11EOSUserID"].ToString() + "' ";
                }
                inthdnpageIndex = Convert.ToInt32(hdnpageIndex.Value.ToString());
                ViewState["vsSearchCondition"] = Condition;
                ViewState["vsSortExpression"] = SortExpression;

                objDatasetResult = CommonFunctions.List33R1V1("List33R1V1", stringuserID, stringFORMID, Condition, SortExpression, intRecordFrom, intRecordTo, out intRecordCount, out interrorcount, out stringOutputResult);

                PopulatePager(intRecordCount, inthdnpageIndex);

                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[0] != null && objDatasetResult.Tables[0].Rows.Count > 0)
                    {
                        return (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[0].Rows.Count > 0) ? objDatasetResult : null;
                    }
                    else
                    {
                        stringmessage = "No Records Found ";
                        CommonFunctions.ShowMessageboot(this, stringmessage);
                        gvUserHistory.DataSource = objDatasetResult;
                        gvUserHistory.DataBind();
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
                stringOutputResult = null;
                stringFORMID = "";
                //intRecordFrom = 0;
                //intRecordTo = 0;
                stringuserID = "";
                stringmessage = "";
            }
            return null;
        }

        protected void LnkbtnSort_Click(object sender, EventArgs e)
        {
            DataSet objDataSetSort1 = null;
            objDataSetSort1 = new DataSet();
            string stringColumnName = "";
            string stringColumnNameDraft = "";
            try
            {
                if (sender != null)
                {
                    stringColumnName = ((LinkButton)sender).CommandArgument;
                    if (stringColumnName != null && stringColumnName.Trim().Length > 0)
                    {

                        ViewState["vsSortDirection"] = (ViewState["vsSortDirection"] != null && ViewState["vsSortDirection"].ToString().Trim() == "ASC") ? " DESC" : " ASC";
                        string stringsortexpression = ViewState["vsSortDirection"].ToString();

                        if (stringColumnName.ToUpper().Contains("."))
                        {
                            string[] stringValues = null; stringValues = stringColumnName.Split('.');
                            if (stringValues != null && stringValues.Length > 0)
                            {
                                stringColumnName = stringValues[1];
                            }
                        }
                        if (ViewState["Draft"] != null)
                        {
                            stringColumnNameDraft = ViewState["Draft"].ToString();
                            if (stringColumnNameDraft.ToUpper() == "DRAFT" && stringColumnName.ToString() == "MODIFIED_BY")
                            {
                                stringColumnName = "user_name ";
                                ViewState["vsSortExpression"] = stringColumnName + ViewState["vsSortDirection"].ToString();
                            }
                            else
                            {
                                ViewState["vsSortExpression"] = stringColumnName + ViewState["vsSortDirection"].ToString();
                            }
                        }
                        else
                        {
                            ViewState["vsSortExpression"] = stringColumnName + ViewState["vsSortDirection"].ToString();
                        }
                    
                        LoadRecord("", (string)ViewState["vsSearchConditions"], ViewState["vsSortExpression"].ToString(), 0);
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
                lblsysseqno.Text = stringOutputResult[3];
                txterrormsg.Text = stringOutputResult[2];
                Modelpopuperror.Show();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private string PrepareSearchExpression(string stringaddconditions)
        {
            string stringOverallExpression = "";
            string stringexp = null;
            string stringOperator = "";
            stringOperator = " AND ";
            string stringCondition = "";
            string stringColumnName = "";
            string stringDataType = ""; 
            string stringInput = "";
            DataTable objDataTableColumnsList = null;
            string stringTempExpression = "";
            DataRow[] objDataRowFiltered = null;
            try
            {

                if (ViewState["ADVSEARCHCOLUMNS"] != null) { objDataTableColumnsList = (DataTable)ViewState["ADVSEARCHCOLUMNS"]; }


                for (int intCount = 0; intCount < gvAdvancedSearch.Rows.Count; intCount++)
                {
                    stringCondition = "";
                    stringColumnName = "";
                    stringDataType = ""; 
                    stringInput = "";

                    TextBox objTextboxSearchInput = ((TextBox)gvAdvancedSearch.Rows[intCount].FindControl("txtSearchInput"));
                    DropDownList objDropDownListColumnName = ((DropDownList)gvAdvancedSearch.Rows[intCount].FindControl("ddlColumnName"));
                    DropDownList objDropDownListCondition = ((DropDownList)gvAdvancedSearch.Rows[intCount].FindControl("ddlCondition"));

                    if (objDropDownListCondition.SelectedItem != null)
                    { stringCondition = objDropDownListCondition.SelectedItem.Value; }


                    if (objDropDownListColumnName.SelectedItem != null)
                    { stringColumnName = objDropDownListColumnName.SelectedItem.Value; }


                    stringInput = objTextboxSearchInput.Text.Trim().Replace("'", "''");

                    if (stringCondition.Trim().Length > 0 && stringColumnName.Trim().Length > 0 && stringInput.Trim().Length > 0)
                    {
                        stringTempExpression = "lst_id='" + stringColumnName + "' and delmark='N'";

                        if (objDataTableColumnsList != null && objDataTableColumnsList.Rows.Count > 0)
                        {
                            objDataRowFiltered = objDataTableColumnsList.Select(stringTempExpression);
                            if (objDataRowFiltered != null && objDataRowFiltered.Length > 0)
                            {
                                stringDataType = objDataRowFiltered[0]["remarks"].ToString(); 
                            }
                        }

                        if (stringDataType != null && stringDataType.Trim().Length > 0)
                        { 
                            if (stringOverallExpression.Trim().Length > 0) { stringOverallExpression += stringOperator; }

                            if (stringDataType.Trim().ToUpper() == "STRING")
                            {
                                if (stringCondition.Trim().ToUpper() == "LIKE")
                                {
                                    stringOverallExpression += "( " + stringColumnName + " like '%" + stringInput.Trim() + "%' )";
                                }
                                else if (stringCondition.Trim().ToUpper() == "NOTLIKE")
                                {
                                    stringOverallExpression += "( " + stringColumnName + " not like '%" + stringInput.Trim() + "%' )";
                                }
                                else if (stringCondition.Trim().ToUpper() == "STARTSWITH")
                                {
                                    stringOverallExpression += "( " + stringColumnName + " like '" + stringInput.Trim() + "%' )";
                                }

                                else if (stringCondition.Trim().ToUpper() == "SOUNDSLIKE")
                                {
                                    stringOverallExpression += "(soundex(" + stringColumnName + ") = soundex('" + stringInput.Trim() + "'))";
                                }

                                else if (stringCondition.Trim().ToUpper() == "ENDSWITH")
                                {
                                    stringOverallExpression += "( " + stringColumnName + " like '%" + stringInput.Trim() + "' )";
                                }
                                else if (stringCondition.Trim().ToUpper() == "EQUALS")
                                {
                                    stringOverallExpression += "( " + stringColumnName + " = '" + stringInput.Trim() + "' )";
                                }
                                else if (stringCondition.Trim().ToUpper() == "NOTEQUALS")
                                {
                                    stringOverallExpression += "( " + stringColumnName + " <> '" + stringInput.Trim() + "' )";
                                }

                            }
                            else if (stringDataType.Trim().ToUpper() == "DATE")
                            {
                                DateTime objDateTimevalue = CommonFunctions.ConvertToDateTime(stringInput.Trim(), "dd-MM-yyyy");

                                if (stringCondition.Trim().ToUpper() == "EQUALS")
                                {
                                    stringOverallExpression += "( " + "CONVERT(varchar(12)," + stringColumnName + ",112)" + " = '" + objDateTimevalue.ToString("yyyyMMdd") + "' )";
                                }
                                else if (stringCondition.Trim().ToUpper() == "NOTEQUALS")
                                {
                                    stringOverallExpression += "( " + "CONVERT(varchar(12)," + stringColumnName + ",112)" + " <> '" + objDateTimevalue.ToString("yyyyMMdd") + "' )";
                                }
                            }
                            else if (stringDataType.Trim().ToUpper() == "INT" || stringDataType.Trim().ToUpper() == "FLOAT" || stringDataType.Trim().ToUpper() == "DECIMAL")
                            {
                                if (stringCondition.Trim().ToUpper() == "EQUALS")
                                {
                                    stringOverallExpression += "( " + stringColumnName + " = " + stringInput.Trim() + " )";
                                }
                                else if (stringCondition.Trim().ToUpper() == "NOTEQUALS")
                                {
                                    stringOverallExpression += "( " + stringColumnName + " <> " + stringInput.Trim() + " )";
                                }
                                else
                                {
                                    stringOverallExpression += "( " + stringColumnName + " = " + stringInput.Trim() + " )";
                                }
                            }
                        }
                    }
                }
                if (stringaddconditions.Length > 0)
                {
                    if (stringaddconditions.ToUpper() == "ALL")
                    {
                        ViewState["particulartabcondition"] = "ALL";
                        if (stringOverallExpression.Length > 0)
                        {
                            stringOverallExpression += "and mrreg.mr_status IN ('PENDING TRACING','PENDING DESPATCH','PENDING FORWARDING','PENDING ASSIGNED','PENDING SUP VETTING')";
                        }
                        else
                        {
                            stringOverallExpression += "mrreg.mr_status IN ('PENDING TRACING','PENDING DESPATCH','PENDING FORWARDING','PENDING ASSIGNED','PENDING SUP VETTING')";
                        }
                    }
                    else if (stringaddconditions.ToUpper() == "DRAFT")
                    {
                        ViewState["particulartabcondition"] = "ALL";
                        if (stringOverallExpression.Length > 0)
                        {
                            stringOverallExpression += "and mrreg.DRAFT_FLAG='YES'";
                        }
                        else
                        {
                            stringOverallExpression += "mrreg.DRAFT_FLAG='YES'";
                        }


                    }
                    else
                    {
                        ViewState["particulartabcondition"] = stringaddconditions;
                        if (stringOverallExpression.Length > 0)
                        {
                            stringOverallExpression += "and mrreg.mr_status='" + stringaddconditions + "'";
                        }
                        else
                        {
                            stringOverallExpression += "mrreg.mr_status='" + stringaddconditions + "'";
                        }

                    }
                }
                else
                {
                    ViewState["particulartabcondition"] = null;
                }
                if (stringOverallExpression != null && stringOverallExpression.Trim().Length > 0)
                {
                    SearchProfilesByExpression(" AND " + stringOverallExpression);
                }
                else
                {
                    SearchProfilesByExpression("");
                }

                return stringexp;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return null;
            }
            finally
            {
                stringOverallExpression = "";
                stringexp = null;
                stringOperator = ""; 
                objDataTableColumnsList = null;
                stringTempExpression = "";
                objDataRowFiltered = null;
            }
        }

        private void SearchProfilesByExpression(string stringOverallExpression)
        {
            try
            {
                LoadRecord("", stringOverallExpression, "priority_flag desc,CREATED_ON desc");
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        //Paging
        private void PopulatePager(int recordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5; 
                double dblPageCount = (double)((decimal)recordCount / CommonFunctions.GridViewPagesize("Orderhistorygridviewpagesize"));
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
                //if(pageIndex==0)
                //{
                //   pageIndex = 1;
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
                    if (currentPage < pageCount)
                    {
                        pages.Add(new ListItem("Last", pageCount.ToString()));
                    }
                    else
                    {

                    }
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
                    hdnpageIndex.Value = Session["PageIndex1"].ToString();
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
                    hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();
                }
                else
                {
                    intRecordTo = Convert.ToInt32(hdnto.Value.ToString());
                    inthdnpageIndex = Convert.ToInt32(hdnpageIndex.Value.ToString());
                    int intpaging = CommonFunctions.GridViewPagesize(stringformId);
                    int recFrom1 = (inthdnpageIndex * intpaging) - intpaging;

                    hdnfrom.Value = (recFrom1 + 1).ToString();
                    hdnto.Value = (recFrom1 + CommonFunctions.GridViewPagesize(stringformId)).ToString();

                }
                ViewState["bool"] = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        #endregion

        #region  //for grid tab triggerevent
        protected void btntriggertab1_Click(object sender, EventArgs e)
        {
            try
            {
                hdnfrom.Value = "0";
                hdnpageIndex.Value = "0";
                hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();
                btntriggertab1.BackColor = ColorTranslator.FromHtml("#397279");
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab7.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = btntriggertab6.BackColor = Color.FromArgb(99, 191, 247);

                PrepareSearchExpression("ALL");
                pnlTab1.Visible = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        protected void btntriggertab2_Click(object sender, EventArgs e)
        {
            try
            {
                hdnfrom.Value = "0";
                hdnpageIndex.Value = "0";
                hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();
                btntriggertab2.BackColor = Color.FromArgb(57, 114, 121);
                btntriggertab1.BackColor = btntriggertab3.BackColor = btntriggertab7.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = btntriggertab6.BackColor = Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("PENDING TRACING");
                pnlTab1.Visible = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }


        }

        protected void btntriggertab3_Click(object sender, EventArgs e)
        {
            try
            {
                hdnfrom.Value = "0";
                hdnpageIndex.Value = "0";
                hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();
                btntriggertab3.BackColor = Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab1.BackColor = btntriggertab7.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = btntriggertab6.BackColor = Color.FromArgb(99, 191, 247);

                PrepareSearchExpression("PENDING DESPATCH");
                pnlTab1.Visible = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }


        }

        protected void btntriggertab4_Click(object sender, EventArgs e)
        {
            try
            {
                hdnfrom.Value = "0";
                hdnpageIndex.Value = "0";
                hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();
                btntriggertab4.BackColor = Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab7.BackColor = btntriggertab1.BackColor = btntriggertab5.BackColor = btntriggertab6.BackColor = Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("PENDING FORWARDING");
                pnlTab1.Visible = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        protected void btntriggertab5_Click(object sender, EventArgs e)
        {
            try
            {
                hdnfrom.Value = "0";
                hdnpageIndex.Value = "0";
                hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();
                btntriggertab5.BackColor = Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab7.BackColor = btntriggertab4.BackColor = btntriggertab1.BackColor = btntriggertab6.BackColor = Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("PENDING ASSIGNED");
                pnlTab1.Visible = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        protected void btntriggertab6_Click(object sender, EventArgs e)
        {
            try
            {
                hdnfrom.Value = "0";
                hdnpageIndex.Value = "0";
                hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();
                btntriggertab6.BackColor = Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab7.BackColor = btntriggertab4.BackColor = btntriggertab1.BackColor = btntriggertab5.BackColor = Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("DRAFT");
                pnlTab1.Visible = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }
        protected void btntriggertab7_Click(object sender, EventArgs e)
        {
            try
            {
                hdnfrom.Value = "0";
                hdnpageIndex.Value = "0";
                hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();
                btntriggertab7.BackColor = Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab1.BackColor = btntriggertab5.BackColor = btntriggertab6.BackColor = Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("PENDING SUP VETTING");
                pnlTab1.Visible = true;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        #endregion

        #region  //for grid start
        protected void gvUserHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet objdataset = new DataSet();
            DataTable objdatatableselectedvalue = null;
            string stringreqID = "";
            DataRow[] objDataselectedrow = null;
            string stringMRSTATUS = "";
            string stringPRIORITY_FLAG = "";
            bool boolisReadOnly = true;
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRow objDataRow = ((DataRowView)e.Row.DataItem).Row;
                    var objLabel = (Label)e.Row.FindControl("lbluniqid");
                    var objLabelMRStatus = (Label)e.Row.FindControl("lblMRstatus");
                    var chkFlag = (CheckBox)e.Row.FindControl("chkPP");
                    var Icon01 = (Image)e.Row.FindControl("imggridflag01");
                    stringreqID = objLabel.Text.ToString();
                    chkFlag.Enabled = true;

                    //for checkbox auto selection during paging start
                    if (ViewState["SelectedRecordsMANTANIN"] != null)
                    {
                        objdatatableselectedvalue = (DataTable)ViewState["SelectedRecordsMANTANIN"];
                        if (objdatatableselectedvalue != null && objdatatableselectedvalue.Rows.Count > 0 && objdatatableselectedvalue.Select("REQUEST_ID='" + stringreqID + "'").Length > 0)
                        {
                            objDataselectedrow = objdatatableselectedvalue.Select("REQUEST_ID='" + stringreqID + "'");
                            if (objDataselectedrow != null && objDataselectedrow.Length > 0)
                            {
                                chkFlag.Checked = true;
                                chkFlag.Enabled = true;
                            }

                        }
                    }
                    //end

                    if (objDataRow != null)
                    {
                        stringMRSTATUS = objDataRow["MR_STATUS"].ToString().ToUpper();
                        if (stringMRSTATUS == "FORWARDED")
                        {
                            chkFlag.Enabled = false;
                        }

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


                    objDataRow = ((DataRowView)e.Row.DataItem).Row;
                    if (objDataRow != null)
                    {
                        LinkButton lnkbtnReceiptNo = (LinkButton)e.Row.FindControl("lblRequest_ID");

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
                objdataset = null; ;
                objdatatableselectedvalue = null;
                stringreqID = "";
                objDataselectedrow = null;
                stringMRSTATUS = "";
                stringPRIORITY_FLAG = "";
            }
        }

        protected void chkPP_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox objCheckBox = null;
            GridViewRow objGridViewRow = null;
            Label objlbluniqid = null;
            DataTable objdatatableDRIDdatatable = null;
            DataTable objdatatableselectedvalue = null;
            string stringreqID = "";
            DataRow[] objDataRow = null;
            DataRow objDataRowNew = null;
            try
            {
                objCheckBox = sender as CheckBox;
                objGridViewRow = (GridViewRow)objCheckBox.NamingContainer;
                objCheckBox = (CheckBox)objGridViewRow.FindControl("chkPP");
                objlbluniqid = (Label)objGridViewRow.FindControl("lbluniqid");
                stringreqID = objlbluniqid.Text.ToString();

                if (ViewState["FE0003R1V1_idGrid"] != null)
                {
                    objdatatableDRIDdatatable = (DataTable)ViewState["FE0003R1V1_idGrid"];
                }
                if (objCheckBox.Checked)
                {

                    if (objdatatableDRIDdatatable != null && objdatatableDRIDdatatable.Rows.Count > 0 && objdatatableDRIDdatatable.Select("REQUEST_ID='" + stringreqID + "'").Length > 0)
                    {
                        if (ViewState["SelectedRecordsMANTANIN"] != null)
                        {
                            objdatatableselectedvalue = (DataTable)ViewState["SelectedRecordsMANTANIN"];
                            if (objdatatableselectedvalue != null)
                            {
                                objDataRow = objdatatableDRIDdatatable.Select("REQUEST_ID='" + stringreqID + "'");

                                if (objDataRow != null && objDataRow.Length > 0)
                                {
                                    objDataRowNew = objdatatableselectedvalue.NewRow();
                                    objDataRowNew["CHK_FLAG"] = "Y";
                                    objDataRowNew["TRANS_STATUS"] = objDataRow[0]["MR_STATUS"].ToString();
                                    objDataRowNew["REQUEST_ID"] = objDataRow[0]["REQUEST_ID"].ToString();
                                    objdatatableselectedvalue.Rows.Add(objDataRowNew);
                                    ViewState["SelectedRecordsMANTANIN"] = objdatatableselectedvalue;
                                }
                            }
                        }
                    }
                }
                else
                {

                    if (ViewState["SelectedRecordsMANTANIN"] != null)
                    {
                        objdatatableselectedvalue = (DataTable)ViewState["SelectedRecordsMANTANIN"];
                        if (objdatatableselectedvalue != null && objdatatableselectedvalue.Rows.Count > 0 && objdatatableselectedvalue.Select("REQUEST_ID='" + stringreqID + "'").Length > 0)
                        {
                            objDataRow = objdatatableselectedvalue.Select("REQUEST_ID='" + stringreqID + "'");
                            if (objDataRow != null && objDataRow.Length > 0)
                            {
                                objDataRow[0].Delete();
                                objdatatableselectedvalue.AcceptChanges();
                                ViewState["SelectedRecordsMANTANIN"] = objdatatableselectedvalue;
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
                objCheckBox = null;
                objGridViewRow = null;
                objlbluniqid = null;
                objdatatableDRIDdatatable = null;
                objdatatableselectedvalue = null;
                stringreqID = "";
                objDataRow = null;
                objDataRowNew = null;
            }
        }


        #endregion //for grid end

        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            DataTable objdatatableselectedvalue = null;
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FE0003R1V2";
            DataSet objDatasetResult1 = null;
            ViewState["ERRORBATCCH_ID"] = "";
            DataTable dtCopy = null;
            string stringBATCHID = "";
            try
            {
                if (ViewState["SelectedRecordsMANTANIN"] != null)
                {
                    objdatatableselectedvalue = (DataTable)ViewState["SelectedRecordsMANTANIN"];
                    if (objdatatableselectedvalue != null && objdatatableselectedvalue.Rows.Count > 0 && objdatatableselectedvalue.Select("CHK_FLAG='Y'").Length > 0)
                    {
                        dtCopy = objdatatableselectedvalue.Copy();
                        objDatasetResult1 = new DataSet();
                        objDatasetResult1.Tables.Add(dtCopy);
                        objDatasetResult1.Tables[0].TableName = "t1";
                        objDatasetResult1.AcceptChanges();
                        if (objDatasetResult1 != null && objDatasetResult1.Tables[0] != null && objDatasetResult1.Tables[0].Rows.Count > 0)
                        {
                            stringBATCHID = DateTime.Now.ToString("HHmmssfffffff").ToUpper();
                            objDatasetResult = CommonFunctions.BatchWorkListR1V1("BatchWorkListR1V1", stringformid, stringBATCHID, objDatasetResult1, out interrorcount, out stringOutputResult);
                            if (interrorcount == 0)
                            {
                                BlockBillingSearch(stringBATCHID, "");
                            }
                            else
                            {
                                Errorpopup(stringOutputResult);
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
                objdatatableselectedvalue = null;
                objDatasetResult = null;
                interrorcount = 0;
                stringOutputResult = null;
                stringformid = "";
                objDatasetResult1 = null;
                dtCopy = null;
                stringBATCHID = "";
            }
        }


        private void BlockBillingSearch(string stringBatchID, string stringSource)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringOrderBy = "";
            string stringexp = "";
            DataTable objDataTable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            int intRecordFrom = 0;
            int intRecordTo = int.MaxValue;
            string stringformid01 = "";
            string stringServiceType = "";
            string stringcondition = "";
            try
            {
                intRecordFrom = intrecFromtemperrordetail;
                intRecordTo = intrecTotemperrordetail;
                stringformid01 = "FE0003R1V1";
                stringServiceType = "List2R1V1";
                if (stringBatchID.Length > 0)
                {
                    stringcondition = "And MSPD.be_id= '" + stringbeid + "'AND MSPD.BATCH_SEQ_NO= '" + stringBatchID + "'";
                }
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid01, stringexp, stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);

                PopulatePagerBBGeneration(intTotalRecord, intpageIndextemperrordetail);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables["t2"];
                    }
                    if (objDataTable != null && objDataTable.Rows.Count > 0)
                    {
                        if (stringSource.Length == 0)
                        {
                            ViewState["ERRORBATCCH_ID"] = stringBatchID;
                        }
                        gvlistBATCHPROLILE.DataSource = objDataTable;
                        gvlistBATCHPROLILE.DataBind();
                        pnlBatchRequest.Visible = true;
                        mpePdtBatchrequest.Show();
                    }
                    else
                    {
                        ViewState["ERRORBATCCH_ID"] = null;
                        gvlistBATCHPROLILE.DataSource = null;
                        gvlistBATCHPROLILE.DataBind();
                        pnlBatchRequest.Visible = true;
                        mpePdtBatchrequest.Show();
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
                stringOrderBy = "";
                stringexp = "";
                objDataTable = null;
                stringbeid = "";
                intRecordFrom = 0;
                intRecordTo = 0;
                stringformid01 = "";
                stringServiceType = "";
                stringcondition = "";
            }
        }

        private void PopulatePagerBBGeneration(int recordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdPagingtemperrordetail);
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
                rptPagertemperrordetail.DataSource = pages;
                rptPagertemperrordetail.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        protected void lnkPagegvInvoice_Click(object sender, EventArgs e)
        {
            string stringBatchID = "";
            int recFrom1 = 0;
            try
            {
                if (Session["PageIndex1"] != null)
                {
                    intpageIndextemperrordetail = Convert.ToInt32(Session["PageIndex1"].ToString());
                    Session["PageIndex"] = intpageIndextemperrordetail;
                }
                else
                {
                    intpageIndextemperrordetail = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndextemperrordetail != 0)
                    {
                        Session["PageIndex"] = intpageIndextemperrordetail;
                    }
                }

                if (intpageIndextemperrordetail == 1)
                {
                    intrecFromtemperrordetail = 0;
                    intrecTotemperrordetail = CommonFunctions.GridViewPagesize(stringformIdPagingtemperrordetail);
                }
                else
                {
                    recFrom1 = (intpageIndextemperrordetail * intrecTotemperrordetail) - intrecTotemperrordetail;
                    intrecFromtemperrordetail = recFrom1 + 1;
                    intrecTotemperrordetail = recFrom1 + CommonFunctions.GridViewPagesize(stringformIdPagingtemperrordetail);
                }

                if (ViewState["ERRORBATCCH_ID"] != null && ViewState["ERRORBATCCH_ID"].ToString().Length > 0)
                {
                    stringBatchID = ViewState["ERRORBATCCH_ID"].ToString();
                }
                BlockBillingSearch(stringBatchID, "PAGING");
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                //recFrom1 = 0;
                //stringBatchID = "";
            }

        }

        protected void btnokBATCHPROLILE_Click(object sender, EventArgs e)
        {
            string stringOverallExpression = "";
            try
            {
                stringOverallExpression = "and mrreg.mr_status IN ('PENDING TRACING')";
                LoadRecord("LOADDEFAULT", stringOverallExpression, "priority_flag desc,CREATED_ON desc");
                gvlistBATCHPROLILE.DataSource = null;
                gvlistBATCHPROLILE.DataBind();
                pnlBatchRequest.Visible = false;
                mpePdtBatchrequest.Hide();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringOverallExpression = "";
            }

        }
        protected void lnkbtnloadBATCHPROLILE_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string stringreqId = "";
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
                            stringreqId = stringValues[0];

                            Session["REQUEST_FromSummary"] = stringreqId;
                            Response.Redirect("FC0001R1V1.aspx?TO=Y");
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
                stringreqId = "";
                stringValues = null;
            }
        }

        protected void lnkbtnRequest_ID_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string stringRequestID = "";
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
                            stringRequestID = stringValues[0];
                            Session["REQUEST_FromSummary"] = stringRequestID.Trim();
                            Response.Redirect("FC0001R1V1.aspx?TO=Y");
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
                stringRequestID = "";
                stringValues = null;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FE0003R1V2";
            string stringOrderBy = "";
            byte[] byteArray;
            string stringServerPath = "";
            string stringsource = "";
            string stringFunctionName = "List1R1V1";
            string stringExportName = "Worklist";
            string stringexp1 = "";
            try
            {
                if (ViewState["vsSearchCondition"] != null)
                {
                    stringexp1 = ViewState["vsSearchCondition"].ToString();
                }
                else
                {
                    stringexp1 = "";
                }
                stringServerPath = CommonFunctions.Export(stringFunctionName, stringformid, stringexp1, stringOrderBy, stringExportName, out byteArray, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {
                    stringsource = Server.MapPath(@"\Export\");
                    CommonFunctions.OpenExportedFileR1V1(this, byteArray, stringExportName.ToString(), "EXCEL"); 
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
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                stringServerPath = "";
                stringsource = "";
                stringFunctionName = "";
                stringExportName = "";
                stringexp1 = "";
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            Label objlbluniqid = null;
            DataTable objdatatableDRIDdatatable = null;
            DataTable objdatatableselectedvalue = null;
            string stringreqID = "";
            DataRow[] objDataRow = null;
            DataRow objDataRowNew = null;
            try
            {
                if (sender != null)
                {
                    CheckBox objCheckBox = sender as CheckBox;
                    for (int intCount = 0; intCount < gvUserHistory.Rows.Count; intCount++)
                    {
                        ((CheckBox)gvUserHistory.Rows[intCount].FindControl("chkPP")).Checked = objCheckBox.Checked;

                        objCheckBox = sender as CheckBox;
                        objCheckBox = (CheckBox)gvUserHistory.Rows[intCount].FindControl("chkPP");
                        objlbluniqid = (Label)gvUserHistory.Rows[intCount].FindControl("lbluniqid");
                        if (objlbluniqid != null)
                        {
                            stringreqID = objlbluniqid.Text.ToString();
                        }

                        if (ViewState["FE0003R1V1_idGrid"] != null)
                        {
                            objdatatableDRIDdatatable = (DataTable)ViewState["FE0003R1V1_idGrid"];
                        }
                        if (objCheckBox.Checked)
                        {

                            if (objdatatableDRIDdatatable != null && objdatatableDRIDdatatable.Rows.Count > 0 && objdatatableDRIDdatatable.Select("REQUEST_ID='" + stringreqID + "'").Length > 0)
                            {
                                if (ViewState["SelectedRecordsMANTANIN"] != null)
                                {
                                    objdatatableselectedvalue = (DataTable)ViewState["SelectedRecordsMANTANIN"];
                                    if (objdatatableselectedvalue != null)
                                    {
                                        objDataRow = objdatatableDRIDdatatable.Select("REQUEST_ID='" + stringreqID + "'");

                                        if (objDataRow != null && objDataRow.Length > 0)
                                        {
                                            objDataRowNew = objdatatableselectedvalue.NewRow();
                                            objDataRowNew["CHK_FLAG"] = "Y";
                                            objDataRowNew["TRANS_STATUS"] = objDataRow[0]["MR_STATUS"].ToString();
                                            objDataRowNew["REQUEST_ID"] = objDataRow[0]["REQUEST_ID"].ToString();
                                            objdatatableselectedvalue.Rows.Add(objDataRowNew);
                                            ViewState["SelectedRecordsMANTANIN"] = objdatatableselectedvalue;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {

                            if (ViewState["SelectedRecordsMANTANIN"] != null)
                            {
                                objdatatableselectedvalue = (DataTable)ViewState["SelectedRecordsMANTANIN"];
                                if (objdatatableselectedvalue != null && objdatatableselectedvalue.Rows.Count > 0 && objdatatableselectedvalue.Select("REQUEST_ID='" + stringreqID + "'").Length > 0)
                                {
                                    objDataRow = objdatatableselectedvalue.Select("REQUEST_ID='" + stringreqID + "'");
                                    if (objDataRow != null && objDataRow.Length > 0)
                                    {
                                        objDataRow[0].Delete();
                                        objdatatableselectedvalue.AcceptChanges();
                                        ViewState["SelectedRecordsMANTANIN"] = objdatatableselectedvalue;
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
                objlbluniqid = null;
                objdatatableDRIDdatatable = null;
                objdatatableselectedvalue = null;
                stringreqID = "";
                objDataRow = null;
                objDataRowNew = null;
            }
        }
    }
}