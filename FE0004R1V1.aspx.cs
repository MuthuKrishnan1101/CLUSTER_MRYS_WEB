using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

namespace CLUSTER_MRTS
{
    public partial class FE0004R1V1 : System.Web.UI.Page
    {
        public DataSet objDatasetAppsVariables; 
        public int intpageIndex = 0;
        public int intrecFrom = 0;
        public int intrecTo = 10;

        public int intpageIndextemperrordetail = 0;
        public int intrecFromtemperrordetail = 0;
        public int intrecTotemperrordetail = 0;

        public string stringformIdPagingtemperrordetail = "MRPaymentViewPopupPaging";

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {

            if (CommonFunctions.IsActive())
            {
                string stringformId = "";
                try
                {
                    stringformId = "Orderhistorygridviewpagesize";
                    intrecTo = CommonFunctions.GridViewPagesize(stringformId);
                    intrecTotemperrordetail = CommonFunctions.GridViewPagesize(stringformIdPagingtemperrordetail);

                    if (!IsPostBack)
                    {
                        VerifyAccessRights(); 
                        CommonFunctions.HeaderName(this, "FE0004R1V1");
                        ViewState["vsSortDirection"] = " ASC";
                        ViewState["vsSearchCondition"] = "";
                        ViewState["vsSortExpression"] = "";
                        ViewState["ERRORBATCCH_ID"] = "";
                        ViewState["bool"] = false;
                        LoadAdvancedSearchColumnNames();//for column name filters ddl
                        LoadAdvancedSearchConditions();//for condition ddl
                        LoadAdvancedSearchGrid();//for default 1st line
                        LoadRecord("LOADDEFAULT", " AND mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED')", "priority_flag desc,CREATED_ON desc");
                        ViewState["bool"] = true;
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
                    stringformId = "";
                }
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
                        if (stringparticulartabcondition.Length == 0)
                        {
                            if (ViewState["vsSearchCondition"] != null)
                            {
                                stringparticulartabcondition = ViewState["vsSearchCondition"].ToString();
                            }
                        }
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
                stringparticulartabcondition = "";
            }


        }
        private void VerifyAccessRights()
        {

            DataSet objDatasetResult = null;

            string stringstatus = "";
            string[] stringOutputResult = null;
            string[] stringComponent = null;
            DataRow objDataRow = null;
            imgbtnprint.Enabled = false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FE0004R1V1";
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
                intFromRecord = 0;
                intToRecord = 0;
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
                intFromRecord = 0;
                intToRecord = 0;
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
            DataTable objDataTable = null;
            string stringColumnName = "";
            string stringCondition = "";
            string stringSearchInput = "";
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
                objDataTable = null;
                stringColumnName = "";
                stringCondition = "";
                stringSearchInput = "";
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
                stringSort = string.Empty;
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
            int intRequestNotCompleted = 0;
            int intRecordOverduereq = 0;
            int intRecordOverduesoon = 0;
            int intRecordRequestwithpenreq = 0;
            int intRecordWaiverapp = 0;
            string stringMRStatus = "";
            string stringmessage = "";
            try
            {
                ViewState["vsSearchCondition"] = Condition;
                intRecordCount = 0;
                objDataSet = GetRecords(out intRecordCount, Condition, SortExpression, RecordFrom, RecordTo);
                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[1] != null && objDataSet.Tables[1].Rows.Count > 0)
                {
                    if (stringTYPE.Length > 0 && stringTYPE == "LOADDEFAULT")
                    {
                        ViewState["SelectedRecordsMANTANIN"] = objDataSet.Tables[1].Clone();
                    }
                    stringMRStatus = objDataSet.Tables[1].Rows[0]["MR_STATUS"].ToString();

                    intRequestNotCompleted = Convert.ToInt32(objDataSet.Tables[0].Rows[0]["REQUEST_NOT_COMPLETED"].ToString());
                    intRecordOverduereq = Convert.ToInt32(objDataSet.Tables[0].Rows[0]["OVERDUE_REQUEST"].ToString());
                    intRecordOverduesoon = Convert.ToInt32(objDataSet.Tables[0].Rows[0]["OVERDUE_SOON"].ToString());
                    intRecordRequestwithpenreq = Convert.ToInt32(objDataSet.Tables[0].Rows[0]["REQ_PENDING_ITEMS"].ToString());
                    intRecordWaiverapp = Convert.ToInt32(objDataSet.Tables[0].Rows[0]["PENDING_WAIVER_APPROVAL"].ToString());

                    btntriggertab1.Text = "Request Not Completed ( " + intRequestNotCompleted.ToString() + " )";
                    btntriggertab2.Text = "Overdue Request ( " + intRecordOverduereq.ToString() + " )";
                    btntriggertab3.Text = "Overdue soon ( " + intRecordOverduesoon.ToString() + " )";
                    btntriggertab4.Text = "Request With Pending Items ( " + intRecordRequestwithpenreq.ToString() + " )";
                    btntriggertab5.Text = "Pending Waiver Approval Request ( " + intRecordWaiverapp.ToString() + " )";

                    if (Condition.Contains(("'FORWARDED','CANCELLED','COLLECTED',")) || (stringTYPE.Length > 0 && stringTYPE == "LOADDEFAULT"))
                    {
                        btntriggertab1.Text = "Request Not Completed ( " + intRecordCount.ToString() + " )";
                    }
                    else if (Condition.Contains("< 0"))
                    {
                        btntriggertab2.Text = "Overdue Request ( " + intRecordCount.ToString() + " )";
                    }
                    else if (Condition.Contains("between 1 and 5"))
                    {
                        btntriggertab3.Text = "Overdue soon ( " + intRecordCount.ToString() + " )";
                    }
                    else if (Condition.Contains("PENDING_STATUS='PENDING'"))
                    {
                        btntriggertab4.Text = "Request With Pending Items ( " + intRecordCount.ToString() + " )";
                    }
                    else if (Condition.Contains("WAIVER_STATUS='YES' AND wa.WAIVER_APPROVED='PENDING'"))
                    {
                        btntriggertab5.Text = "Pending Waiver Approval Request ( " + intRecordCount.ToString() + " )";
                    }

                    ViewState["FE0003R1V1_idGrid"] = objDataSet.Tables[1];

                }
                else
                {
                    stringmessage = "No Records Found";
                    CommonFunctions.ShowMessageboot(this, stringmessage);
                    ViewState["FE0003R1V1_idGrid"] = null;
                    gvUserHistory.DataSource = null;
                    gvUserHistory.DataBind();
                    btntriggertab1.Text = "Request Not Completed";
                    btntriggertab2.Text = "Overdue Request";
                    btntriggertab3.Text = "Overdue soon";
                    btntriggertab4.Text = "Request With Pending Items";
                    btntriggertab5.Text = "Pending Waiver Approval Request";

                }

                this.BindGrid();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDataSet = null;
                intRecordCount = 0;
                stringMRStatus = "";
            }
        }

        public DataSet GetRecords(out int intRecordCount, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {

            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringFORMID = "";
            stringFORMID = "FE0004R1V1";
            intRecordCount = 0;
            string stringServiceType = "List1R1V1";
            int intRecordFrom = 0;
            int intRecordTo = int.MaxValue;

            try
            {
                intRecordFrom = intrecFrom;
                intRecordTo = intrecTo;
                ViewState["vsSearchCondition"] = Condition;
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringFORMID, Condition, SortExpression, intRecordFrom, intRecordTo, out intRecordCount, out interrorcount, out stringOutputResult);
                PopulatePager(intRecordCount, intpageIndex);

                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[1] != null && objDatasetResult.Tables[1].Rows.Count > 0)
                    {
                        return (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[1].Rows.Count > 0) ? objDatasetResult : null;
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
                intRecordFrom = 0;
                intRecordTo = 0;
            }
            return null;
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
                        ViewState["vsSortExpression"] = stringColumnName + ViewState["vsSortDirection"].ToString();
                        LoadRecord("", (string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString(), 0);
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

                objDataTableColumnsList = null;
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
                    ViewState["particulartabcondition"] = stringaddconditions;
                    if (stringaddconditions.ToUpper() == "REQUEST NOT COMPLETED")
                    {
                        if (stringOverallExpression.Length > 0)
                        {
                            stringOverallExpression += " and mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED')";
                        }
                        else
                        {
                            stringOverallExpression += "mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED')";
                        }
                    }
                    else if (stringaddconditions.ToUpper() == "OVERDUE REQUEST")
                    {
                        if (stringOverallExpression.Length > 0)
                        {
                            stringOverallExpression += " and mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED') and DATEDIFF(day, getdate(),mrreg.due_date ) < 0";
                        }
                        else
                        {
                            stringOverallExpression += "mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED') and DATEDIFF(day, getdate(),mrreg.due_date ) < 0";
                        }
                    }
                    else if (stringaddconditions.ToUpper() == "OVERDUE SOON")
                    {
                        if (stringOverallExpression.Length > 0)
                        {
                            stringOverallExpression += " and mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED') and DATEDIFF(day, getdate(),mrreg.due_date ) between 1 and 5";
                        }
                        else
                        {
                            stringOverallExpression += "mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED') and DATEDIFF(day, getdate(),mrreg.due_date ) between 1 and 5";
                        }
                    }
                    else if (stringaddconditions.ToUpper() == "REQUEST WITH PENDING ITEMS")
                    {
                        if (stringOverallExpression.Length > 0)
                        {
                            stringOverallExpression += " and mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED') and pen.PENDING_STATUS='PENDING'";
                        }
                        else
                        {
                            stringOverallExpression += "mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED') and pen.PENDING_STATUS='PENDING'";
                        }
                    }
                    else if (stringaddconditions.ToUpper() == "PENDING WAIVER APPROVAL REQUEST")
                    {
                        if (stringOverallExpression.Length > 0)
                        {
                            stringOverallExpression += " and mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED') and wa.WAIVER_STATUS='YES' AND wa.WAIVER_APPROVED='PENDING'";
                        }
                        else
                        {
                            stringOverallExpression += "mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED') and wa.WAIVER_STATUS='YES' AND wa.WAIVER_APPROVED='PENDING'";
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
                //int multiplier = 1000;
                //Calculate the Start and End Index of pages to be displayed.
                //double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal("10"));
                double dblPageCount = (double)((decimal)recordCount / CommonFunctions.GridViewPagesize("Orderhistorygridviewpagesize"));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                //int double_result = (int)((dblPageCount - (int)dblPageCount) * multiplier);
                //if(double_result>0&& pageCount!=0)
                //{
                //    pageCount = pageCount + 1;
                //}
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
                CommonFunctions.ShowMessageboot02(objException);
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
                    intrecTo = CommonFunctions.GridViewPagesize("Orderhistorygridviewpagesize");
                }
                else
                {
                    int intrecFrom1 = (intpageIndex * intrecTo) - intrecTo;
                    intrecFrom = intrecFrom1 + 1;
                    intrecTo = intrecFrom1 + CommonFunctions.GridViewPagesize("Orderhistorygridviewpagesize");
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
                btntriggertab1.BackColor = ColorTranslator.FromHtml("#397279");
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = Color.FromArgb(99, 191, 247);

                PrepareSearchExpression("REQUEST NOT COMPLETED");
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
                btntriggertab2.BackColor = Color.FromArgb(57, 114, 121);
                btntriggertab1.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("OVERDUE REQUEST");
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
                btntriggertab3.BackColor = Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab1.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = Color.FromArgb(99, 191, 247);

                PrepareSearchExpression("OVERDUE SOON");
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
                btntriggertab4.BackColor = Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab1.BackColor = btntriggertab5.BackColor = Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("REQUEST WITH PENDING ITEMS");

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
                btntriggertab5.BackColor = Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab1.BackColor = Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("PENDING WAIVER APPROVAL REQUEST");

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
            DataRow objDataRow = null;
            DataRow[] objDataselectedrow = null;
            string stringMRSTATUS = "";
            string stringPRIORITY_FLAG = "";
            string stringduedays = "";
            bool boolisReadOnly = true;
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    objDataRow = ((DataRowView)e.Row.DataItem).Row;
                    var objLabel = (Label)e.Row.FindControl("lbluniqid");
                    var objLabelMRStatus = (Label)e.Row.FindControl("lblMRstatus");
                    TextBox objtxtboxcolour = (TextBox)e.Row.FindControl("txtcolorbx");
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
                        stringduedays = objDataRow["due_days"].ToString().ToUpper();
                        if (stringduedays.Length > 0)
                        {
                            int intduedays = Convert.ToInt32(stringduedays.ToString());
                            if (intduedays < 0)
                            {
                                objtxtboxcolour.BackColor = Color.FromArgb(153, 51, 51);
                            }
                            else if (intduedays == 0 || intduedays == 1 || intduedays == 2 || intduedays == 3 || intduedays == 4 || intduedays == 5)
                            {
                                objtxtboxcolour.BackColor = Color.FromArgb(234, 58, 0);
                            }
                            else if (intduedays == 6 || intduedays == 7 || intduedays == 8)
                            {
                                objtxtboxcolour.BackColor = Color.FromArgb(255, 140, 0);
                            }
                            else if (intduedays == 9 || intduedays == 10 || intduedays == 11)
                            {
                                objtxtboxcolour.BackColor = Color.FromArgb(254, 197, 56);
                            }
                            else if (intduedays == 12 || intduedays == 13 || intduedays == 14 || intduedays > 14)
                            {
                                objtxtboxcolour.BackColor = Color.FromArgb(121, 196, 42);
                            }
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
                objdataset = null;
                objdatatableselectedvalue = null;
                stringreqID = "";
                objDataRow = null;
                objDataselectedrow = null;
                stringMRSTATUS = "";
                stringPRIORITY_FLAG = "";
                stringduedays = "";
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
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables[1] != null && objDatasetResult.Tables[1].Rows.Count > 0)
                    {
                        objDataTable = objDatasetResult.Tables[1];
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
            try
            {
                if (Session["intpageIndex1"] != null)
                {
                    intpageIndextemperrordetail = Convert.ToInt32(Session["intpageIndex1"].ToString());
                    Session["intpageIndex"] = intpageIndextemperrordetail;
                }
                else
                {
                    intpageIndextemperrordetail = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndextemperrordetail != 0)
                    {
                        Session["intpageIndex"] = intpageIndextemperrordetail;
                    }
                }

                if (intpageIndextemperrordetail == 1)
                {
                    intrecFromtemperrordetail = 0;
                    intrecTotemperrordetail = CommonFunctions.GridViewPagesize(stringformIdPagingtemperrordetail);
                }
                else
                {
                    int intrecFrom1 = (intpageIndextemperrordetail * intrecTotemperrordetail) - intrecTotemperrordetail;
                    intrecFromtemperrordetail = intrecFrom1 + 1;
                    intrecTotemperrordetail = intrecFrom1 + CommonFunctions.GridViewPagesize(stringformIdPagingtemperrordetail);
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

        }

        protected void btnokBATCHPROLILE_Click(object sender, EventArgs e)
        {
            try
            {
                LoadRecord("LOADDEFAULT", "", "priority_flag desc,CREATED_ON desc");
                gvlistBATCHPROLILE.DataSource = null;
                gvlistBATCHPROLILE.DataBind();
                pnlBatchRequest.Visible = false;
                mpePdtBatchrequest.Hide();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
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
            string stringformid = "FE0003R1V3";
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
                    // CommonFunctions.OpenExportedFileR1V1(stringServerPath, byteArray, stringExportName.ToString().Replace(".", "") + ".xlsx", stringsource);
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
    }
}