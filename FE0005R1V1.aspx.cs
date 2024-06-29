using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FE0005R1V1 : System.Web.UI.Page
    {
        public int intpageIndexgvlist = 0;
        public int intrecFromgvlist = 0;
        public int intrecTogvlist = 0;
        public string stringformIdPaginggvlist = "FE005R1V1popupPaging"; 

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {

            if (CommonFunctions.IsActive())
            {
                string stringFORMID = "";
                string stringformId = "";
                try
                {
                    stringformId = "Orderhistorygridviewpagesize";
                    intrecTogvlist = CommonFunctions.GridViewPagesize(stringformId);


                    if (!IsPostBack)
                    {
                        VerifyAccessRights();
                        CommonFunctions.HeaderName(this, "FE0005R1V1");
                        ViewState["vsSearchCondition"] = null;
                        ViewState["vsSortDirection"] = " ASC";
                        ViewState["vsSortExpression"] = "";
                        Session["stringtransid"] = null;
                        Session["orderprocess"] = null;



                        Session[stringFORMID + "_idGrid"] = null;
                        LoadAdvancedSearchColumnNames();//for column name filters ddl
                        LoadAdvancedSearchConditions();//for condition ddl
                        LoadAdvancedSearchGrid();//for default 1st line
                        Session["bool"] = true;
                        PrepareSearchExpression();
                    }
                    else
                    {
                        Session["bool"] = true;
                    }
                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }
                finally
                {
                    stringFORMID = "";
                    stringformId = "";
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (Session["bool"] != null)
                {
                    bool boolsts = (bool)Session["bool"];

                    if (boolsts == false)
                    {
                        PrepareSearchExpression();
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
            imgbtnprint.Enabled = false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FE0005R1V1";
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
                stringexp = "And lst.lstgrp_id='" + stringFORMID + "'";

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
            try
            {
                PrepareSearchExpression();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
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

        protected void LnkbtnSort_Click(object sender, EventArgs e)
        {
            DataSet objDataSetSort1 = null;
            objDataSetSort1 = new DataSet();
            try
            {
                if (sender != null)
                {
                    string stringColumnName = ((LinkButton)sender).CommandArgument;
                    if (stringColumnName != null && stringColumnName.Trim().Length > 0)
                    {

                        ViewState["vsSortDirection"] = (ViewState["vsSortDirection"] != null && ViewState["vsSortDirection"].ToString().Trim() == "ASC") ? " DESC" : " ASC";
                        ViewState["vsSortExpression"] = stringColumnName + ViewState["vsSortDirection"].ToString();
                        SearchRecords((string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString());
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

        private string PrepareSearchExpression()
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

                if (stringOverallExpression != null && stringOverallExpression.Trim().Length > 0)
                {
                    SearchRecords(" AND " + stringOverallExpression, "mrenq.created_on desc");
                }
                else
                {
                    SearchRecords("", "mrenq.created_on desc");
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
        #endregion



        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)//fix
        {
            try
            {
                PrepareSearchExpression();
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

        private void SearchRecords(string stringexp, string stringOrderBy)
        {
            int intTotalRecord = 0;
            DataSet objDataSet = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FE0003R1V1"; 
            string stringServiceType = "List1R1V1";
            int intRecordFrom = 0;
            int intRecordTo = int.MaxValue;
            try
            {
                intRecordFrom = intrecFromgvlist;
                intRecordTo = intrecTogvlist;

                stringexp += "and mrenq.REFERENCE_1 = 'PENDING'";
                stringexp += "AND mrreg.mr_status not in ('FORWARDED','CANCELLED','COLLECTED')";

                ViewState["vsSearchCondition"] = stringexp;
                ViewState["vsSortExpression"] = stringOrderBy;
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);

                PopulatePagergvlist(intTotalRecord, intpageIndexgvlist);
                lblTotalRecords.InnerText = intTotalRecord.ToString();
                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["t1"] != null && objDataSet.Tables["t1"].Rows.Count > 0)
                    {
                        Session["SortTable"] = objDataSet.Tables["t1"];
                        gvList.DataSource = objDataSet.Tables["t1"];
                        gvList.DataBind();
                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "No Records Found");
                        Session["SortTable"] = null;
                        gvList.DataSource = null;
                        gvList.DataBind();
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
                intTotalRecord = 0;
                objDataSet = null;
                interrorcount = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                stringServiceType = "";
                //intRecordFrom = 0;
                //intRecordTo = 0;
            }
        }

        protected void lnkPagegvList_Click(object sender, EventArgs e)
        {
            string stringexp = "";
            string stringsortexp = "";
            try
            {
                if (Session["PageIndex1"] != null)
                {
                    intpageIndexgvlist = Convert.ToInt32(Session["PageIndex1"].ToString());
                    Session["PageIndex"] = intpageIndexgvlist;
                }
                else
                {
                    intpageIndexgvlist = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndexgvlist != 0)
                    {
                        Session["PageIndex"] = intpageIndexgvlist;
                    }
                }

                if (intpageIndexgvlist == 1)
                {
                    intrecFromgvlist = 0;
                    intrecTogvlist = CommonFunctions.GridViewPagesize(stringformIdPaginggvlist);
                }
                else
                {
                    int recFrom1 = (intpageIndexgvlist * intrecTogvlist) - intrecTogvlist;
                    intrecFromgvlist = recFrom1 + 1;
                    intrecTogvlist = recFrom1 + CommonFunctions.GridViewPagesize(stringformIdPaginggvlist);
                }
                ViewState["bool"] = false;
                if (ViewState["vsSearchCondition"] != null)
                {
                    stringexp = ViewState["vsSearchCondition"].ToString();
                }
                if (ViewState["vsSortExpression"] != null)
                {
                    stringsortexp = ViewState["vsSortExpression"].ToString();
                }
                SearchRecords(stringexp, stringsortexp);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private void PopulatePagergvlist(int recordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;
                int intpaging = CommonFunctions.GridViewPagesize(stringformIdPaginggvlist);
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
                rptPagergvInvoicegvList.DataSource = pages;
                rptPagergvInvoicegvList.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void lnkbtnREQUEST_IDGrid_Click(object sender, EventArgs e)
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

        protected void lbtnClear_Click(object sender, EventArgs e)
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool boolisReadOnly = true;
            DataRow objDataRow = null;
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null) { return; }

                objDataRow = ((DataRowView)e.Row.DataItem).Row;
                if (objDataRow != null)
                {
                    LinkButton lnkbtnReceiptNo = (LinkButton)e.Row.FindControl("lnkbtnREQUEST_IDGrid");

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
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                objDataRow = null;
            }
        }
         
    }
}