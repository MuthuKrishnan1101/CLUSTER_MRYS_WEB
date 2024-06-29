using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FD0001R1V1 : System.Web.UI.Page
    { 
        public int intpageIndex = 0;
        public int intrecFrom = 0;
        public int intrecTo = 10;
        public string stringformIdPaging = "DRAFTPopupPaging";

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {

            if (CommonFunctions.IsActive())
            {
                try
                {  
                    intrecTo = CommonFunctions.GridViewPagesize(stringformIdPaging);

                    if (!IsPostBack)
                    {
                        VerifyAccessRights(); 
                        CommonFunctions.HeaderName(this, "FD0001R1V1");
                        Session["SSNALLPRODUCTTABLEDRAFT"] = null;  
                        Session["ssnSelectedProductFromAllProduct"] = null;

                        ViewState["vsSortDirection"] = " ASC";
                        ViewState["vsSortExpression"] = "";
                        ViewState["bool"] = false;
                        LoadAdvancedSearchColumnNames();//for column name filters ddl
                        LoadAdvancedSearchConditions();//for condition ddl
                        LoadAdvancedSearchGrid();//for default 1st line 
                        LoadUserProfiles();
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
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        { 
            try
            {
                if (ViewState["bool"] != null)
                {
                    bool boolsts = (bool)ViewState["bool"];

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
            if (Request.QueryString["ID"] != null)
            {
                stringFORMID = Request.QueryString["ID"].ToString();
            }
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringexp = "";
            try
            {
                stringOutputResult = new string[3];
                stringexp = "And lst.be_id= '" + stringbeid + "' And lst.lstgrp_id='" + stringFORMID + "' And lst.delmark= 'N'";

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
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTable = null;
            string stringexp = "";
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
            }
        }
        private void LoadAdvancedSearchGrid()
        {
            DataTable objDataTable = null;
            DataColumn objDataColumn = null;
            DataRow objDataRowNew = null;
            try
            {
                if (Session["AdvancedSearchData"] != null) { Session.Remove("AdvancedSearchData"); }

                objDataTable = new DataTable();
                objDataColumn = new DataColumn("SNo", typeof(long));
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
                objDataColumn = null;
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
                            //lbtnSearch.Focus();
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
                ViewState["bool"] = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        protected void lnkbtnClear_Click(object sender, EventArgs e)
        {
            ClearValues();
        }
        #endregion

        #region method


        private void SearchRecords(string stringexp)
        {

            int intTotalRecord = 0;
            DataSet objDataSet = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FE0001R1V3";
            string stringOrderBy = "temtrac.created_on desc";
            string stringServiceType = "List1R1V1"; 
            bool boolstatus = true;
            int intRecordFrom = 0;
            int intRecordTo = 0;
            DataTable objDataTable = null;
            try
            {
                if (boolstatus)
                {
                    intRecordFrom = intrecFrom;
                    intRecordTo = intrecTo;
                    stringexp += "And Reg.DRAFT_FLAG='YES'";
                    ViewState["vsSortExpression"] = stringexp;
                   objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);

                    PopulatePager(intTotalRecord, intpageIndex);
                    lblTotalRecords.InnerText = intTotalRecord.ToString();
                    if (interrorcount == 0)
                    {
                        if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                        { 
                            Session["SSNALLPRODUCTTABLEDRAFT"] = objDataSet.Tables[0];  
                            Session["ssnProductTableDRAFT"] = objDataSet.Tables[0].Clone();

                            objDataSet.Tables[0].DefaultView.Sort = "priority_flag desc,CREATED_ON desc";
                            objDataTable = objDataSet.Tables[0].DefaultView.ToTable();

                            gvUserHistory.DataSource = objDataTable;
                            gvUserHistory.DataBind();
                            Modelpopuperrorsuccess.Hide();
                        }
                        else
                        {
                            Session["SSNALLPRODUCTTABLEDRAFT"] = null;  
                            Session["ssnProductTableDRAFT"] = null;

                            lblTotalRecords.InnerText = "0";
                            Modelpopuperrorsuccess.Show(); 
                            gvUserHistory.DataSource = null;
                            gvUserHistory.DataBind();
                        }
                    }
                    else
                    {
                        lblTotalRecords.InnerText = "0";
                        PopulatePager(0, intpageIndex);
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
                intTotalRecord = 0;
                objDataSet = null;
                interrorcount = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intRecordFrom = 0;
                intRecordTo = 0;
                objDataTable = null;
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
                        ViewState["vsSortExpression"] = stringColumnName + ViewState["vsSortDirection"].ToString();
                        SearchRecords(ViewState["vsSortExpression"].ToString());
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
            DataTable objDataTableColumnsList = null;
            string stringCondition = "";
            string stringColumnName = "";
            string stringDataType = "";
            string stringAlliseName = "";
            string stringInput = "";
            string stringTempExpression = "";
            DataRow[] objDataRowFiltered = null;
            string stringEncrypyValue = "";
            try
            {

                if (ViewState["ADVSEARCHCOLUMNS"] != null) { objDataTableColumnsList = (DataTable)ViewState["ADVSEARCHCOLUMNS"]; }


                for (int intCount = 0; intCount < gvAdvancedSearch.Rows.Count; intCount++)
                {
                    stringCondition = "";
                    stringColumnName = "";
                    stringDataType = "";
                    stringAlliseName = "";
                    stringInput = "";

                    TextBox objTextboxSearchInput = ((TextBox)gvAdvancedSearch.Rows[intCount].FindControl("txtSearchInput"));
                    DropDownList objDropDownListColumnName = ((DropDownList)gvAdvancedSearch.Rows[intCount].FindControl("ddlColumnName"));
                    DropDownList objDropDownListCondition = ((DropDownList)gvAdvancedSearch.Rows[intCount].FindControl("ddlCondition"));

                    if (objDropDownListCondition.SelectedItem != null)
                    { stringCondition = objDropDownListCondition.SelectedItem.Value; }


                    if (objDropDownListColumnName.SelectedItem != null)
                    { stringColumnName = objDropDownListColumnName.SelectedItem.Value; }


                    stringInput = objTextboxSearchInput.Text.Trim();

                    if (stringCondition.Trim().Length > 0 && stringColumnName.Trim().Length > 0 && stringInput.Trim().Length > 0)
                    {
                        stringTempExpression = "lst_id='" + stringColumnName + "' and delmark='N'";

                        if (objDataTableColumnsList != null && objDataTableColumnsList.Rows.Count > 0)
                        {
                            objDataRowFiltered = objDataTableColumnsList.Select(stringTempExpression);
                            if (objDataRowFiltered != null && objDataRowFiltered.Length > 0)
                            {
                                stringDataType = objDataRowFiltered[0]["remarks"].ToString();
                                //stringAlliseName = objDataRowFiltered[0]["reference_2"].ToString();
                            }
                        }

                        if (stringDataType != null && stringDataType.Trim().Length > 0)
                        {
                            if (!stringColumnName.Contains("."))
                            { stringColumnName = stringAlliseName + "." + stringColumnName; }


                            if (stringOverallExpression.Trim().Length > 0) { stringOverallExpression += stringOperator; }

                            if (stringDataType.Trim().ToUpper() == "STRING")
                            {
                                if (stringCondition.Trim().ToUpper() == "LIKE")
                                {
                                    if(stringColumnName.ToString().ToUpper()== "PAT.HRN_ID")
                                    {
                                        stringEncrypyValue = CommonFunctions.HRNtoEncrypyValue(stringInput);
                                        stringOverallExpression += "( " + stringColumnName + " like '%" + stringEncrypyValue.Trim() + "%' )";
                                    }
                                    else
                                    {
                                        stringOverallExpression += "( " + stringColumnName + " like '%" + stringInput.Trim() + "%' )";
                                    }
                                    
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
                stringCondition = "";
                stringColumnName = "";
                stringDataType = "";
                stringAlliseName = "";
                stringInput = "";
                stringTempExpression = "";
                objDataRowFiltered = null;
                stringEncrypyValue = "";
            }
        }

        private void SearchProfilesByExpression(string stringOverallExpression)
        {
            try
            {
                SearchRecords(stringOverallExpression);
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
                    intrecTo = CommonFunctions.GridViewPagesize(stringformIdPaging);
                }
                else
                {
                    int intrecFrom1 = (intpageIndex * intrecTo) - intrecTo;
                    intrecFrom = intrecFrom1 + 1;
                    intrecTo = intrecFrom1 + CommonFunctions.GridViewPagesize(stringformIdPaging);
                }
                SearchRecords(ViewState["vsSortExpression"].ToString());
                ViewState["bool"] = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void gvUserHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet objdataset = new DataSet();
            string stringASSIGNED_BY = null; 
            LinkButton objlnkbtnreqid= null;
            DataRow[] objSelectedRow = null; 
            DataTable objGETPACKINGUOM = null;
            DropDownList objDropDownList = null; string stringREQUEST_ID = "";
            string stringPRIORITY_FLAG = "";
            string stringSort = string.Empty;
            string stringBE_ID = "";
            DataTable objDT = new DataTable();
            string stringreq_id = "";
            bool boolisReadOnly = true;
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridViewRow objGridViewRow = e.Row;
                    stringSort = string.Empty;
                    if (objGridViewRow.DataItem == null) { return; }

                    DataRow objDataRow = ((DataRowView)e.Row.DataItem).Row;
                    var Icon01 = (Image)e.Row.FindControl("imggridflag01");


                    objlnkbtnreqid = (LinkButton)e.Row.FindControl("lnkbtnRequestID");
                    if (objDataRow != null)
                    {
                        stringPRIORITY_FLAG = objDataRow["PRIORITY_FLAG"].ToString().ToUpper();
                        stringBE_ID = objDataRow["BE_ID"].ToString().ToUpper();
                          stringREQUEST_ID = objDataRow["REQUEST_ID"].ToString().ToUpper();
                        stringASSIGNED_BY = objDataRow["ACTION_BY"].ToString();
                        if (stringPRIORITY_FLAG == "Y")
                        {
                            Icon01.Visible = true;
                        }
                        else if (stringPRIORITY_FLAG == "N")
                        {
                            Icon01.Visible = false;
                        }
                        if (objGridViewRow.FindControl("hfIDsLeft") != null)
                        {
                            ((HiddenField)objGridViewRow.FindControl("hfIDsLeft")).Value = stringBE_ID + "~" + stringREQUEST_ID ;
                        }
                    }

                    objDataRow = ((DataRowView)e.Row.DataItem).Row;
                    if (objDataRow != null)
                    {
                        LinkButton lnkbtnReceiptNo = (LinkButton)e.Row.FindControl("lnkbtnRequestID");

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
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    objDropDownList = (DropDownList)e.Row.FindControl("ddlassigndby");

                    if (Session["Assigned_BY"] != null)
                    {
                        objGETPACKINGUOM = (DataTable)Session["Assigned_BY"];
                        objDropDownList = (DropDownList)e.Row.FindControl("ddlassigndby");
                        objDropDownList.DataTextField = "user_name";
                        objDropDownList.DataValueField = "user_id";

                        objDropDownList.DataSource = objGETPACKINGUOM;
                        objDropDownList.DataBind();
                        objDropDownList.Items.Insert(0, new ListItem("", ""));

                        if (objGETPACKINGUOM.Select("user_id='" + stringASSIGNED_BY + "'").Length > 0)
                        {

                        }
                        else
                        {
                            objDropDownList.Items.Insert(0, new ListItem(stringASSIGNED_BY, stringASSIGNED_BY));
                        }

                        if (objDropDownList.Items.FindByValue(stringASSIGNED_BY) != null)
                        {
                            objDropDownList.ClearSelection();
                            objDropDownList.Items.FindByValue(stringASSIGNED_BY).Selected = true;
                        }
                    }
                    else
                    {
                        objDropDownList.ClearSelection();
                    }

                    objDropDownList.Enabled = false;
                    objDropDownList.ClearSelection();


                    if (Session["ssnSelectedProductFromAllProduct"] != null)
                    {
                       
                        objDT = Session["ssnSelectedProductFromAllProduct"] as DataTable;

                        if (objDT != null && objDT.Rows.Count > 0)
                        {
                            DropDownList objddlassigned = (DropDownList)e.Row.FindControl("ddlassigndby"); 
                            var chkFlag = (CheckBox)e.Row.FindControl("chkSelectLeft");

                            objSelectedRow = objDT.Select("chkFlag='Y'");

                            if (objSelectedRow != null && objSelectedRow.Length > 0)
                            {
                                foreach (DataRow row in objSelectedRow)
                                {
                                    stringreq_id = objlnkbtnreqid.Text.ToString();
                                    if (stringREQUEST_ID == row["BEI_ID"].ToString())
                                    {
                                        chkFlag.Checked = true;

                                        objDropDownList.ClearSelection();
                                        if (objddlassigned.Items.FindByValue(stringASSIGNED_BY) != null)
                                        {
                                            objDropDownList.Items.FindByValue(stringASSIGNED_BY).Selected = true; 
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
                objdataset = new DataSet();
                stringASSIGNED_BY = null;
                objlnkbtnreqid = null;
                objSelectedRow = null;
                objGETPACKINGUOM = null;
                objDropDownList = null; 
                stringREQUEST_ID = "";
                stringPRIORITY_FLAG = "";
                stringSort = string.Empty;
                stringBE_ID = "";
                objDT = null;
                stringreq_id = "";
            }
        }

        protected void lnkbtnUserID_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringProductID = "";
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
                stringValues = null;
                stringProductID = "";
            }
        }


        #endregion

        protected void chkSelectAllLeft_CheckedChanged(object sender, EventArgs e)
        {
        
            CheckBox objCheckBox = null; 
            GridViewRow objGridViewRow = null;
            string stringRequestID = null;
            DataTable objAllProductTable = null;
            DropDownList ddlassigndby = null;
            DataRow[] row = null;
            DataRow[] objDataRow = null;
            DataTable objDT = new DataTable();
            DataTable objDT2 = new DataTable();
            try
                {
                    if (Session["SSNALLPRODUCTTABLEDRAFT"] != null)
                    {
                        objAllProductTable = (DataTable)Session["SSNALLPRODUCTTABLEDRAFT"];
                    }
                     

                    objCheckBox = sender as CheckBox;
                    objGridViewRow = (GridViewRow)objCheckBox.NamingContainer;
                    stringRequestID = ((LinkButton)objGridViewRow.FindControl("lnkbtnRequestID")).Text; 
                    objCheckBox = (CheckBox)objGridViewRow.FindControl("chkSelectLeft");
                    ddlassigndby = (DropDownList)objGridViewRow.FindControl("ddlassigndby"); 

                    if (objAllProductTable != null && objAllProductTable.Rows.Count > 0)
                    {
                        objDataRow = objAllProductTable.Select("Request_ID='" + stringRequestID + "' ");

                        if (objDataRow != null && objDataRow.Length > 0)
                        {
                            if (objCheckBox.Checked)
                            {
                                if (objAllProductTable != null)
                                {
                                    if (objDataRow != null && objDataRow.Length > 0)
                                    {
                                        //if (objAllProductTable != null && objAllProductTable.Rows.Count > 0 && objAllProductTable.Select("Request_ID = '" + stringproductID + "'").Length > 0)
                                        //{
                                        //    objCheckBox.Checked = false;
                                        //    objCheckBox.Enabled = false;
                                        //    ddlassigndby.Enabled = false;
                                        //}
                                        //else
                                        //{
                                            objCheckBox.Checked = true;
                                            objCheckBox.Enabled = true;
                                            ddlassigndby.Enabled = true;
                                            objDataRow[0]["chkFlag"] = "Y"; 
                                            objDataRow[0]["DML_INDICATOR"] = "I";
                                            objAllProductTable.AcceptChanges();
                                            Session["SSNALLPRODUCTTABLEDRAFT"] = objAllProductTable;
                                        //}
                                    }
                                }
                            }
                            else
                            {
                                objDataRow[0]["chkFlag"] = "N"; 
                                objDataRow[0]["DML_INDICATOR"] = ""; 
                                objDataRow[0]["ACTION_BY"] = ""; 
                                objAllProductTable.AcceptChanges();
                                Session["SSNALLPRODUCTTABLEDRAFT"] = objAllProductTable;
                                ddlassigndby.ClearSelection();
                                ddlassigndby.Enabled = false;
                            }
                        }
                    }
                    if (Session["ssnSelectedProductFromAllProduct"] == null)
                    {
                        objDT = objAllProductTable.Select("chkFlag = 'Y' ").Length > 0 ? objAllProductTable.Select("chkFlag = 'Y' ").CopyToDataTable() : null;
                        Session["ssnSelectedProductFromAllProduct"] = objDT;
                    }
                    else
                    {
                        objDT = Session["ssnSelectedProductFromAllProduct"] as DataTable;
                        List<string> listDT = new List<string>();
                        List<string> listDT2 = new List<string>();
                        if (objAllProductTable.Select("chkFlag = 'Y' or chkFlag = 'N'").Length > 0)
                        {
                            objDT2 = objAllProductTable.Select("chkFlag = 'Y' or chkFlag = 'N'").CopyToDataTable();
                            for (int i = 0; i < objDT.Rows.Count; i++)
                                listDT.Add(objDT.Rows[i]["Request_ID"].ToString());

                            for (int i = 0; i < objDT2.Rows.Count; i++)
                                listDT2.Add(objDT2.Rows[i]["Request_ID"].ToString());

                            for (int i = 0; i < listDT2.Count; i++)
                            {
                                if (!listDT.Contains(listDT2[i]))
                                {
                                    objDT.ImportRow(objDT2.Rows[i]);
                                }
                                else
                                {
                                    row = objDT.Select("Request_ID = '" + listDT2[i] + "'");
                                    if (row.Length > 0)
                                    {
                                        row[0]["chkFlag"] = objDT2.Rows[i]["chkFlag"].ToString();
                                        row[0]["ACTION_BY"] = objDT2.Rows[i]["ACTION_BY"].ToString(); 
                                        objDT.AcceptChanges();
                                    }
                                }
                            }
                        }
                        else
                        {
                            objDT = null;
                        }
                        Session["ssnSelectedProductFromAllProduct"] = objDT;
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
                    objAllProductTable = null;
                }
        

        }
        private bool ValidateCheckedRecords()
        {

            bool boolSelected = false;
            try
            {
                boolSelected = false;
                for (int intCount = 0; intCount < gvUserHistory.Rows.Count; intCount++)
                {
                    CheckBox objCheckbox = (CheckBox)gvUserHistory.Rows[intCount].FindControl("chkSelectLeft");
                    if (objCheckbox != null && objCheckbox.Checked)
                    {
                        boolSelected = true;
                        break;
                    }
                }

                
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                //boolSelected = false;
            }
            return boolSelected;
        }
        protected void imgbtnDelete_Click(object sender, EventArgs e)
        { 
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FC0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            DataRow objdatarow = null;
            int intSuccessCount = 0;
            string stringServiceType1 = "";
            string stringexp = "";
            string stringBE_ID = "";
            string stringREQUEST_ID = "";
            string stringTooltip = "";
            string[] stringIDs = null;
            DataRow[] objdatarowState = null;
            try
            {
                if (ValidateCheckedRecords())
                {
                    stringServiceType1 = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t1"] !=null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                        {
                            for (int intCount = 0; intCount < gvUserHistory.Rows.Count; intCount++)
                            {
                                CheckBox objCheckBox = (CheckBox)gvUserHistory.Rows[intCount].FindControl("chkSelectLeft");
                                HiddenField objHiddenField = (HiddenField)gvUserHistory.Rows[intCount].FindControl("hfIDsLeft");

                                if (objCheckBox != null && objCheckBox.Checked)
                                {
                                    stringBE_ID = "";
                                    stringREQUEST_ID = "";
                                    stringTooltip = objHiddenField.Value;

                                    if (stringTooltip != null && stringTooltip.Trim().Length > 0)
                                    {
                                        stringIDs = stringTooltip.Split('~');
                                        if (stringIDs != null && stringIDs.Length > 0)
                                        {
                                            stringBE_ID = stringIDs[0];
                                            stringREQUEST_ID = stringIDs[1];

                                            if (stringREQUEST_ID.Length > 0)
                                            {

                                                if (objDatasetResult != null )
                                                {
                                                    objdatarow = objDatasetResult.Tables["t1"].NewRow();
                                                    objdatarow["BE_ID"] = stringBE_ID;
                                                    objdatarow["Request_ID"] = stringREQUEST_ID;

                                                    CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                                                    objDatasetResult.Tables["t1"].Rows.Add(objdatarow);

                                                    objDatasetResult.AcceptChanges();
                                                      
                                                    intSuccessCount++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            objDatasetResult.AcceptChanges();
                            if (intSuccessCount > 0 && objDatasetResult != null && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                            {
                                objdatarowState = objDatasetResult.Tables["t1"].Select("CREATED_BY = '"+ HttpContext.Current.Session["G11EOSUser_Name"].ToString() +"'");
                                if (objdatarowState != null && objdatarowState.Length > 0)
                                {
                                    for (int intIndex1 = 0; intIndex1 < objdatarowState.Length; intIndex1++)
                                    {
                                        objdatarowState[intIndex1].Delete();
                                    }
                                }
                                objDatasetResult = objDatasetResult.GetChanges();
                                stringServiceType = "MRISService_DataManipulationR1V1";
                                objDatasetResult = CommonFunctions.DataManipulationExcelR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                                if (intErrorCount == 0)
                                {
                                    CommonFunctions.ShowMessageboot(this, "Record Deleted Successfully");
                                    SearchRecords("");
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
                    CommonFunctions.ShowMessageboot(this, "Please select at least one Record");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
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
                stringServiceType = "";
                objdatarow = null;
                intSuccessCount = 0;
                stringServiceType1 = "";
                stringexp = "";
                stringBE_ID = "";
                stringREQUEST_ID = "";
                stringTooltip = "";
                stringIDs = null;
                objdatarowState = null;
            }
        }
         
        protected void btnconfirmok_Click(object sender, EventArgs e)
        {
            Response.Redirect("FC0001R1V1.aspx?Load=01");
        }
        private void LoadUserProfiles()//fix -ok
        {
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringServiceType = "SERVER_SERVICE_LIST";
            string stringConfigId = "UserSummaryR1V3";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringOrderBy = "";
            DataTable objdatatableLoadActionby = null;
            Session["Assigned_BY"] = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringexp012 = "";
            try
            { 
                stringOutputResult = new string[3];
                stringexp012 = "and usbuents.reference_1= '" + stringbeid + "' and ua.group_id  in ('HIMS USERS','SGH HIMS TEAM LEAD','HIMS SUPERVISOR','NCCS HIMS TEAM LEAD', 'HOD')";

                if (Session["SSNLOADACTIONBYR1V4FD0004"] != null)
                {
                    objdatatableLoadActionby = (DataTable)Session["SSNLOADACTIONBYR1V4FD0004"];
                }
                if ((objdatatableLoadActionby == null) || (objdatatableLoadActionby != null && objdatatableLoadActionby.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringConfigId, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadActionby = objDatasetResult.Tables["t1"];
                            Session["SSNLOADACTIONBYR1V4FD0004"] = objdatatableLoadActionby;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadActionby != null && objdatatableLoadActionby.Rows.Count > 0)
                {
                    Session["Assigned_BY"] = objdatatableLoadActionby; 
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
                objDatasetResult = null;
                stringOutputResult = null;
                stringConfigId = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
            }


        }

        protected void ddlassigndby_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlassigndby = null;
            GridViewRow row = null;
            CheckBox chkRow = null;
            string stringRequestID = "";
            string stringHRN_ID = ""; 
            DataRow[] objDataRow = null;
            DataTable objAllProductTable = null;
            DataTable objGETPACKINGUOM = null; 
            DataTable objDTSelectedPrd = new DataTable();
            DataRow[] objDataRow2 = null;
            DataTable dt = new DataTable();
            try
            {
                ddlassigndby = sender as DropDownList;
                row = (GridViewRow)ddlassigndby.NamingContainer;
                stringRequestID = ((LinkButton)row.FindControl("lnkbtnRequestID")).Text;
                stringHRN_ID = ((Label)row.FindControl("lblHRN_ID")).Text;
                chkRow = (CheckBox)row.FindControl("chkSelectLeft"); 
                if (Session["Assigned_BY"] != null)
                {
                    objGETPACKINGUOM = (DataTable)Session["Assigned_BY"];
                }
                if (Session["SSNALLPRODUCTTABLEDRAFT"] != null)
                {
                    objAllProductTable = (DataTable)Session["SSNALLPRODUCTTABLEDRAFT"];
                } 
                if (Session["ssnSelectedProductFromAllProduct"] != null)
                {
                    objDTSelectedPrd = (DataTable)Session["ssnSelectedProductFromAllProduct"];
                }

                if (objDTSelectedPrd != null && objDTSelectedPrd.Rows.Count > 0)
                {
                    objDataRow = objDTSelectedPrd.Select("Request_ID='" + stringRequestID + "'");
                    objDataRow2 = objAllProductTable.Select("Request_ID='" + stringRequestID + "' ");

                    if (objDataRow != null && objDataRow.Length > 0 && objDataRow2 != null && objDataRow2.Length > 0)
                    {
                        if (ddlassigndby.SelectedValue.Length > 0)
                        {
                            objDataRow[0]["ACTION_BY"] = ddlassigndby.SelectedValue;
                            objDataRow2[0]["ACTION_BY"] = ddlassigndby.SelectedValue;
                            objDataRow[0]["DML_INDICATOR"] = "I";
                            objDataRow2[0]["DML_INDICATOR"] = "I";
                             
                            objDTSelectedPrd.AcceptChanges();
                            objAllProductTable.AcceptChanges();
                            Session["SSNALLPRODUCTTABLEDRAFT"] = objAllProductTable;
                            dt = objDTSelectedPrd.Select("chkFlag = 'Y' ").CopyToDataTable();
                            Session["ssnSelectedProductFromAllProduct"] = dt;


                        }
                        else
                        {
                            objDataRow[0]["ACTION_BY"] = "";
                            objDataRow2[0]["ACTION_BY"] = ""; 
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
                ddlassigndby = null;
                row = null;
                chkRow = null;
                stringRequestID = null;
                objDataRow = null;
                objAllProductTable = null;
                objGETPACKINGUOM = null;
                objDTSelectedPrd = null;
                objDataRow2 = null;
                dt = null;
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
            imgbtnDelete.Enabled = false; 
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FD0001R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        if (objDataRow["new"].ToString().ToUpper() == "ENABLED")
                        {
                            imgbtnNew.Enabled = true;
                            imgbtnDelete.Enabled = true; 
                        }
                        if (objDataRow["edit"].ToString().ToUpper() == "ENABLED")
                        {
                            imgbtnNew.Enabled = true;
                            imgbtnDelete.Enabled = true;
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

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FE0001R1V3";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            objDatasetResult = new DataSet();
            DataRow objDataRow = null;
            DataSet objDataSet = null;
            DataTable objProductTable = null;
            DataTable objProductTablecopy = null;
            string stringServiceType = "";
            string stringexp = "";
            DataRow[] objdatarowState = null;
            DataRow[] objdatarowState1 = null;
            DataRow[] objdatarowState2 = null;
            DataSet objdataset5 = null;
            try
            {
                stringServiceType = "DEFAULT";
                stringexp = "";
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount != 0 && stringOutputResult != null)
                {
                    Errorpopup(stringOutputResult);
                }
                else
                {
                    if (Session["SSNALLPRODUCTTABLEDRAFT"] != null)
                    {
                        if (Session["SSNALLPRODUCTTABLEDRAFT"] != null)
                        {
                            objProductTablecopy = (DataTable)Session["SSNALLPRODUCTTABLEDRAFT"];
                            if (objProductTablecopy.Select("chkFlag = 'Y' ").Length > 0)
                            {
                                objProductTable = objProductTablecopy.Select("chkFlag = 'Y' ").CopyToDataTable();
                            }
                        }
                        if (objProductTable != null && objProductTable.Rows.Count > 0)
                        {
                            for (int intIndex = 0; intIndex < objProductTable.Rows.Count; intIndex++)
                            {
                                objDataRow = objDataSet.Tables["t1"].NewRow();

                                objDataRow["BE_ID"] = Session["BusinessID"].ToString();
                                objDataRow["REQUEST_ID"] = objProductTable.Rows[intIndex]["REQUEST_ID"].ToString();
                                objDataRow["ACTION_BY"] = objProductTable.Rows[intIndex]["ACTION_BY"];
                                objDataRow["REMARKS"] = objProductTable.Rows[intIndex]["REMARKS"];

                                objDataRow["DML_INDICATOR"] = "U";
                                if (Session["stringComputerName"] != null)
                                    objDataRow["CREATED_AT"] = Session["stringComputerName"].ToString();
                                if (Session["G11EOSUser_ID"] != null)
                                    objDataRow["CREATED_BY"] = Session["G11EOSUser_ID"].ToString();
                                objDataRow["CREATED_ON"] = DateTime.Now;
                                if (Session["stringComputerName"] != null)
                                    objDataRow["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                if (Session["G11EOSUser_ID"] != null)
                                    objDataRow["MODIFIED_BY"] = Session["G11EOSUser_ID"].ToString();
                                objDataRow["MODIFIED_ON"] = DateTime.Now;

                                objDataSet.Tables["t1"].Rows.Add(objDataRow);
                            }
                            objDataSet.Tables["t1"].Merge(objDataSet.Tables["t1"]);
                        }


                        objDataSet.AcceptChanges();
                        for (int intIndex = 0; intIndex < objDataSet.Tables.Count; intIndex++)
                        {
                            objdatarowState = objDataSet.Tables[intIndex].Select("DML_INDICATOR = 'D'");
                            if (objdatarowState != null && objdatarowState.Length > 0)
                            {
                                for (int intIndex1 = 0; intIndex1 < objdatarowState.Length; intIndex1++)
                                {
                                    objdatarowState[intIndex1].Delete();
                                }
                            }

                            objdatarowState1 = objDataSet.Tables[intIndex].Select("DML_INDICATOR = 'U'");
                            if (objdatarowState1 != null && objdatarowState1.Length > 0)
                            {
                                for (int intIndex2 = 0; intIndex2 < objdatarowState1.Length; intIndex2++)
                                {
                                    objdatarowState1[intIndex2].SetModified();
                                }
                            }
                            objdatarowState2 = objDataSet.Tables[intIndex].Select("DML_INDICATOR = 'I'");
                            if (objdatarowState2 != null && objdatarowState2.Length > 0)
                            {
                                for (int intIndex3 = 0; intIndex3 < objdatarowState2.Length; intIndex3++)
                                {

                                    objdatarowState2[intIndex3].SetAdded();
                                }
                            }
                        }
                        objdataset5 = objDataSet.GetChanges();


                        stringServiceType = "OperationServiceDML";
                        objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDataSet.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                        if (intErrorCount != 0)
                        {
                            CommonFunctions.ShowMessageboot(this, (stringOutputResult != null && stringOutputResult.Length > 2) ? stringOutputResult[1] : null);
                        }
                        else
                        {
                            Session["SSNALLPRODUCTTABLEDRAFT"] = null;
                            Session["ssnSelectedProductFromAllProduct"] = null;
                            SearchRecords("");
                            string stringmessage = "Record Saved Successfully ";
                            CommonFunctions.ShowMessageboot(this, stringmessage);
                        }
                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "Select at least One Row");
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringOutputResult = null;
                objDataSet = null;
                objDataRow = null;
                objProductTable = null;
                stringServiceType = "";
                stringexp = "";
                objdatarowState = null;
                objdatarowState1 = null;
                objdatarowState2 = null;
                objdataset5 = null;
            }
        }
    }
}