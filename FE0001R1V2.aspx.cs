using CLUSTER_MRTS.CommonFunction;
using G10CertificateValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FE0001R1V2 : System.Web.UI.Page
    { 
        private string GroupId = "GROUP0007"; 
        public int intpageIndex = 0;
        public int intrecFrom = 0;
        public int intrecTo = 10;
        private int dynamicColumnCounter = 0;

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {

            if (CommonFunctions.IsActive())
            {
                string stringFORMID = "";
                string stringformId = "";
                DataTable objuserRole = null;
                try
                {
                   

                    stringformId = "Orderhistorygridviewpagesize";
                    intrecTo = CommonFunctions.GridViewPagesize(stringformId);


                    if (!IsPostBack)
                    {
                        lblTotalRecords.InnerText = "0";
                        gvUserHistory.DataSource = null;
                        gvUserHistory.DataBind();
                        ViewState["vsSearchCondition"] = null; 
                        ViewState["vsSortDirection"] = " ASC";
                        ViewState["vsSortDirectionHRN"] = " DESC";
                        ViewState["vsSortExpression"] = "";
                        ViewState["DOCTORFA0010R1V1"] = "FALSE";
                        Session["stringtransid"] = null;
                        Session["orderprocess"] = null;
                        Session["intpageIndex"] = null;
                        Session["intrecFrom"] = 0; 
                        Session["intrecTo"] = intrecTo;

                        
                        if (Request.QueryString["ID"] != null)
                        { 
                            stringFORMID = Request.QueryString["ID"].ToString();
                            if(stringFORMID == "FA0017R1V1")
                            {
                                Session["bool"] = true;
                            }
                            else
                            {
                                Session["bool"] = false;
                            }
                            CommonFunctions.HeaderName(this, stringFORMID);

                            if (stringFORMID == "FA0010R1V1")
                            {
                                if (Session["UserRolestable"] != null)
                                {
                                    objuserRole = (DataTable)Session["UserRolestable"];

                                    if (objuserRole != null && objuserRole.Rows.Count > 0)
                                    {
                                        if (objuserRole.Select("Group_ID= 'DEPARTMENT SECRETARY'").Length > 0)
                                        {
                                            ViewState["DOCTORFA0010R1V1"] = "TRUE";
                                        } 
                                    } 
                                }
                            }
                        }
                        else
                        { 
                            CommonFunctions.HeaderName(this, "FE0001R1V2");
                        }
                         
                        Session[stringFORMID + "_idGrid"] = null;
                        LoadDynamicGridBindings(stringFORMID); //for grid bindings
                        LoadAdvancedSearchColumnNames();//for column name filters ddl
                        LoadAdvancedSearchConditions();//for condition ddl
                        LoadAdvancedSearchGrid();//for default 1st line 
                    }
                    else
                    {
                        Session["bool"] = true; 
                        this.BindGrid();
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
                    objuserRole = null;
                }
            }
        } 
        protected void Page_PreRender(object sender, EventArgs e)
        { 
            string stringdeptID = "";
            try
            {
                if (Session["bool"] != null)
                {
                   bool boolsts = (bool)Session["bool"];

                    if (boolsts == false)
                    {
                        if (ViewState["DOCTORFA0010R1V1"] != null && ViewState["DOCTORFA0010R1V1"].ToString().Length > 0 && ViewState["DOCTORFA0010R1V1"].ToString() == "TRUE")
                        {
                            if (Session["DEPARTMENTID"] != null)
                            {
                                stringdeptID = Session["DEPARTMENTID"].ToString();
                            }
                            else if (Session["DEPARTMENTIDMULTIPLE"] != null)
                            {
                                stringdeptID = Session["DEPARTMENTIDMULTIPLE"].ToString();
                            }
                            if (stringdeptID.Length > 0)
                            {
                                PrepareSearchExpression(stringdeptID);
                            }
                            else
                            {
                                PrepareSearchExpression("");
                            }
                        }
                        else
                        {
                            PrepareSearchExpression("");
                        }

                        this.BindGrid();
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            } 
        }
        #endregion
        #region auto gridbindings/linkbtn
        private void BindGrid()
        {
            string stringFORMID = "";
            DataSet objdataset = null;
            try
            {
               
                if (Request.QueryString["ID"] != null)
                {
                    stringFORMID = Request.QueryString["ID"].ToString();
                }                
                if (Session[stringFORMID + "_idGrid"] != null)
                {
                    objdataset = (DataSet)Session[stringFORMID + "_idGrid"];

                    gvUserHistory.DataSource = objdataset.Tables[0];
                    gvUserHistory.DataBind();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringFORMID = "";
                objdataset = null;
            }
        }
        protected void ViewDetails(object sender, EventArgs e)
        {
            string stringFORMID = "";
            string stringFA0033R1V1taskid = "";
            string id = "";
            string name = "";
            string stringCmdArgument = "";
            string stringeffective = "";
            string stringpatientID = "";
            string stringbeid = "";
            string[] stringValues = null;
            string str = "";
            try
            {
                if (Request.QueryString["ID"] != null)
                {
                    stringFORMID = Request.QueryString["ID"].ToString();
                }
                LinkButton lnkView = (sender as LinkButton);
                GridViewRow row = (lnkView.NamingContainer as GridViewRow);
                id = lnkView.CommandArgument;
                name = row.Cells[0].Text;

                if (stringFORMID == "FA0017R1V1" || stringFORMID == "FC0002R1V1")
                {
                    if (sender != null)
                    {
                        stringCmdArgument = ((LinkButton)sender).CommandArgument;
                        if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                        {
                            stringValues = stringCmdArgument.Split(',');
                            if (stringValues != null && stringValues.Length > 0)
                            {
                                stringpatientID = stringValues[0];
                                stringbeid = stringValues[1];

                                name = stringpatientID;
                            }
                        }
                    }
                } 

                if (stringFORMID == "FA0033R1V1")
                {
                    stringFA0033R1V1taskid = row.Cells[1].Text;
                    Session["stringFA0033R1V1taskid"] = stringFA0033R1V1taskid;
                }
                else if(stringFORMID == "FA0038R1V1")
                {
                    name = row.Cells[0].Text;
                    stringeffective= row.Cells[2].Text;
                    if (stringeffective != null && stringeffective.Length > 0)
                    {
                        Session["EFFECTIVEdATE"] = stringeffective;
                    }
                    else
                    {
                        Session["EFFECTIVEdATE"] = null;
                    }
                    
                }
                else
                {
                    Session["stringFA0033R1V1taskid"] = null;
                }
                if (name.Length > 0)
                {
                    if (name.Contains("&"))
                    {
                         name = WebUtility.HtmlDecode(name);
                    } 
                    Session[stringFORMID + "_id"] = name;

                    str = Session[stringFORMID + "_id"].ToString();
                    Response.Redirect(stringFORMID);
                }
                else
                {
                    Session[stringFORMID + "_id"] = null;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringFORMID = "";
                stringFA0033R1V1taskid = ""; 
            }
        }
        protected void gvUserHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringFORMID = "";
            DataRowView objDRV = null;
            DataRow objDataRow = null;
            string stringSort = string.Empty;
            string stringpatient_id = "";
            string stringbe_id = "";
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnkView = new LinkButton();
                    lnkView.ID = "lnkView";
                    lnkView.Text = e.Row.Cells[0].Text.ToString();
                    lnkView.Click += ViewDetails;
                    lnkView.CommandArgument = e.Row.Cells[0].Text.ToString();
                    e.Row.Cells[0].Controls.Add(lnkView);
                     
                }

                if (Request.QueryString["ID"] != null)
                {
                    stringFORMID = Request.QueryString["ID"].ToString();
                }
                if (stringFORMID == "FA0017R1V1" || stringFORMID == "FC0002R1V1")
                {

                    GridViewRow objGridViewRow = e.Row;
                    if (objGridViewRow.DataItem == null) { return; }

                    objDRV = ((DataRowView)objGridViewRow.DataItem);
                    objDataRow = objDRV.Row;
                    if (objDataRow != null)
                    {
                        stringpatient_id = objDataRow["patient_id"].ToString();
                        stringbe_id = objDataRow["be_id"].ToString();

                        LinkButton objButtonName = (LinkButton)e.Row.FindControl("lnkView");
                        objButtonName.CommandArgument = stringpatient_id + "," + stringbe_id;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringFORMID = "";
                objDRV = null;
                objDataRow = null;
                stringSort = string.Empty;
                stringpatient_id = "";
                stringbe_id = "";
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
            try
            {
                if (Request.QueryString["ID"] != null)
                {
                    stringFORMID = Request.QueryString["ID"].ToString();
                }
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
                stringexp = "And lst.lstgrp_id='" + GroupId + "'";
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
            DataRowView objDataRowView = null;
            string stringSort = string.Empty;
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
                objDataRowView = null;
                stringSort = string.Empty;
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
                    ((DropDownList)gvAdvancedSearch.Rows[intCount].FindControl("ddlColumnName")).ClearSelection();
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
            string stringFORMID = "";
            try
            {
               
                if (Request.QueryString["ID"] != null)
                {
                    stringFORMID = Request.QueryString["ID"].ToString();
                    Response.Redirect(stringFORMID+ "?Load=01");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringFORMID = "";
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
            string stringdeptID = "";
            try
            {
                if (ViewState["DOCTORFA0010R1V1"] != null && ViewState["DOCTORFA0010R1V1"].ToString().Length > 0 && ViewState["DOCTORFA0010R1V1"].ToString() == "TRUE")
                {
                    if (Session["DEPARTMENTID"] != null)
                    {
                        stringdeptID = Session["DEPARTMENTID"].ToString();
                    }
                    else if (Session["DEPARTMENTIDMULTIPLE"] != null)
                    {
                        stringdeptID = Session["DEPARTMENTIDMULTIPLE"].ToString();
                    }
                    if (stringdeptID.Length > 0)
                    {
                        PrepareSearchExpression(stringdeptID);
                    }
                    else
                    {
                        PrepareSearchExpression("");
                    }
                }
                else
                {
                    PrepareSearchExpression("");
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringdeptID = "";
            }
            this.BindGrid();
                
            
        }

        protected void lnkbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearValues();
                Session["bool"] = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        #endregion

        #region method
        

        private void LoadRecord(string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {
            bool booldeptID = false;
            string stringalise = "";
            DataSet objDataSet = null;
            string stringFORMID = "";
            if (Request.QueryString["ID"] != null)
            {
                stringFORMID = Request.QueryString["ID"].ToString();
            }
            DataTable objuserRole = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            int intRecordCount = 0;
            DataTable objdattable = null;
            string stringasc = "";
            try
            {
                stringalise = AlisenameCondition(stringalise, stringFORMID, "be_id");
                if ((ViewState["vsSearchCondition"] == null || (ViewState["vsSearchCondition"] != null && ViewState["vsSearchCondition"].ToString().Length == 0)) && stringFORMID == "FA0010R1V1")
                {
                    if (Session["UserRolestable"] != null)
                    {
                        objuserRole = (DataTable)Session["UserRolestable"];

                        if (objuserRole != null && objuserRole.Rows.Count > 0)
                        {
                            if (objuserRole.Select("Group_ID= 'DEPARTMENT SECRETARY'").Length > 0)
                            {
                                booldeptID = true;
                            }
                        }
                    }
                    if (!booldeptID)
                    {
                        Condition += "And " + stringalise + "." + "be_id= '" + stringbeid + "' ";
                    }
                }
                else if ((ViewState["vsSearchCondition"] == null || (ViewState["vsSearchCondition"] != null && ViewState["vsSearchCondition"].ToString().Length == 0)) && stringFORMID != "FA0011R1V1" && stringFORMID != "FA0036R1V1")
                {
                    Condition += "And " + stringalise + "." + "be_id= '" + stringbeid + "' ";
                }
                else if (stringFORMID != "FA0011R1V1" && stringFORMID != "FA0036R1V1" && !Condition.ToUpper().Contains("BE_ID"))
                {
                    Condition += "And " + stringalise + "." + "be_id= '" + stringbeid + "' ";
                }
               
                intRecordCount = 0;
                objDataSet = GetRecords(out intRecordCount, Condition, SortExpression, RecordFrom, RecordTo);
                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                {
                    objdattable = null;
                    objdattable = objDataSet.Tables[0];
                    lblTotalRecords.InnerText = intRecordCount.ToString();
                    Session[stringFORMID + "_idGrid"] = objDataSet;
                    foreach (DataRow row in objDataSet.Tables[0].Rows)
                    {
                        if (row.Table.Columns.Contains("ORDER_ID"))
                        {
                            if (row["ORDER_ID"].ToString() == "9999")
                            {
                                row["ORDER_ID"] = DBNull.Value;
                            }
                        }

                    }
                    if (stringFORMID.ToString() == "FA0017R1V1")
                    {
                        ViewState["vsSortDirectionHRN"] = (ViewState["vsSortDirectionHRN"] != null && ViewState["vsSortDirectionHRN"].ToString().Trim() == "ASC") ? " DESC" : " ASC";
                        stringasc = ViewState["vsSortDirectionHRN"].ToString();
                        objdattable.DefaultView.Sort = "HRN_ID "+ ViewState["vsSortDirectionHRN"].ToString();
                        objdattable = objdattable.DefaultView.ToTable();
                    }

                    gvUserHistory.DataSource = objdattable;
                    gvUserHistory.DataBind();
                }
                else
                {
                    Session[stringFORMID + "_idGrid"] = null;
                    gvUserHistory.DataSource = objDataSet;
                    gvUserHistory.DataBind();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private static string AlisenameCondition(string stringalise, string stringFORMID,string stringcolumnnamereports)
        {
            try
            {
                if (stringFORMID == "FA0001R1V1")
                {
                    stringalise = "mrcanres";
                }
                else if (stringFORMID == "FA0002R1V1")
                {
                    stringalise = "caseatyp";
                }
                else if (stringFORMID == "FA0003R1V1")
                {
                    stringalise = "mrclis";
                }
                else if (stringFORMID == "FA0004R1V1")
                {
                    stringalise = "mrcoty";
                }
                else if (stringFORMID == "FA0005R1V1")
                {
                    stringalise = "costcen";
                }
                else if (stringFORMID == "FA0006R1V1")
                {
                    stringalise = "mrcurry";
                }
                else if (stringFORMID == "FA0007R1V1")
                {
                    stringalise = "mrcus";
                }
                else if (stringFORMID == "FA0008R1V1")
                {
                    stringalise = "mrdelas";
                }
                else if (stringFORMID == "FA0009R1V1")
                {
                    stringalise = "mrdelmos";
                }
                else if (stringFORMID == "FA0010R1V1")
                {
                    stringalise = "MRDEP";
                }
                else if (stringFORMID == "FA0011R1V1")
                {
                    stringalise = "mrd";
                }
                else if (stringFORMID == "FA0012R1V1")
                {
                    stringalise = "doctmapp";
                }
                else if (stringFORMID == "FA0013R1V1")
                {
                    stringalise = "REFG";
                }
                else if (stringFORMID == "FA0014R1V1")
                {
                    stringalise = "lst";
                }
                else if (stringFORMID == "FA0015R1V1")
                {
                    stringalise = "mrhoss";
                }
                else if (stringFORMID == "FA0016R1V1")
                {
                    stringalise = "palon";
                }
                else if (stringFORMID == "FA0017R1V1")
                {
                    stringalise = "mrpats";
                }
                else if (stringFORMID == "FA0018R1V1")
                {
                    stringalise = "mrpaytys";
                }
                else if (stringFORMID == "FA0019R1V1")
                {
                    stringalise = "mrpycous";
                }
                else if (stringFORMID == "FA0020R1V1")
                {
                    stringalise = "peits";
                }
                else if (stringFORMID == "FA0021R1V1")
                {
                    stringalise = "mrpurs";
                }
                else if (stringFORMID == "FA0022R1V1")
                {
                    stringalise = "mrreqcay";
                }
                else if (stringFORMID == "FA0023R1V1")
                {
                    stringalise = "mreeks";
                }
                else if (stringFORMID == "FA0024R1V1")
                { 
                    if (stringcolumnnamereports == "REPORT_PURPOSE_NAME")
                    {
                        stringalise = "purp";
                    }
                    else
                    {
                        stringalise = "mrrets";
                    }

                }
                else if (stringFORMID == "FA0025R1V1")
                {
                    stringalise = "mrrefors";
                }
                else if (stringFORMID == "FA0026R1V1")
                {
                    stringalise = "mrreqtyp";
                }
                else if (stringFORMID == "FA0027R1V1")
                {
                    stringalise = "mrreq";
                }
                else if (stringFORMID == "FA0028R1V1")
                {
                    stringalise = "mrreqts";
                }
                else if (stringFORMID == "FA0029R1V1")
                {
                    stringalise = "MRRETYS";
                }
                else if (stringFORMID == "FA0030R1V1")
                {
                    stringalise = "tyvit";
                }
                else if (stringFORMID == "FA0031R1V1")
                {
                    stringalise = "mrstprocs";
                }
                else if (stringFORMID == "FA0032R1V1")
                {
                    stringalise = "mrsttaks";
                }
                else if (stringFORMID == "FA0033R1V1")
                {
                    stringalise = "mrstprts";
                }
                else if (stringFORMID == "FA0034R1V1")
                {
                    stringalise = "INST";
                }
                else if (stringFORMID == "FA0035R1V1")
                {
                    stringalise = "depcat";
                }
                else if (stringFORMID == "FA0036R1V1")
                {
                    stringalise = "spec";
                }
                else if (stringFORMID == "FA0037R1V1")
                {
                    stringalise = "hosda";
                }
                else if (stringFORMID == "FC0002R1V1")
                {
                    stringalise = "spinf";
                }
                else if (stringFORMID == "FR0001R1V1")
                {
                    stringalise = "LTRRPT";
                }
                else if (stringFORMID == "FA0038R1V1")
                {
                    stringalise = "bogst";
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            
            return stringalise;
        }

        public DataSet GetRecords(out int intRecordCount, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {

            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringFORMID = "";
            if (Request.QueryString["ID"] != null)
            {
                stringFORMID = Request.QueryString["ID"].ToString();
            }
            intRecordCount = 0;
            string stringServiceType = "List1R1V1";
            int intRecordFrom = 0;
            int intRecordTo = 0;
            string stringmessage = "";
            try
            {
                SortExpression = SortingExp(SortExpression, stringFORMID);
                intRecordFrom = 0;
                intRecordTo = 0;
                if (Session["intrecFrom"] != null)
                {
                    intRecordFrom = (int)Session["intrecFrom"];
                }
                if (Session["intrecTo"] != null)
                {
                    intRecordTo = (int)Session["intrecTo"];
                }
                ViewState["vsSearchCondition"] = Condition;
                Session["exportcondition"] = Condition;
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringFORMID, Condition, SortExpression, intRecordFrom, intRecordTo, out intRecordCount, out interrorcount, out stringOutputResult);
                if (Session["intpageIndex"] != null)
                {
                    int stringpaging;
                    if (int.TryParse(Session["intpageIndex"].ToString(), out stringpaging))
                    {
                        PopulatePager(intRecordCount, stringpaging);
                    }

                }
                else
                {
                    PopulatePager(intRecordCount, intpageIndex);
                }


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
                        lblTotalRecords.InnerText = intRecordCount.ToString();
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
                stringServiceType = "List1R1V1";
                intRecordFrom = 0;
                intRecordTo = 0;
                stringmessage = "";
            }
            return null;
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
        #region validate HRN
        public bool DoNonCGHHrnValidation(TextBox objTextboxSearchInput)
        {
            bool boolMROHRN = false;
            bool boolStatus = true;
            string stringResult = "";
            string stringInput = "";
            try
            {
                if (objTextboxSearchInput is TextBox)
                {
                    var objControl = (TextBox)objTextboxSearchInput;

                    if (objControl.Text.Trim().Length > 0)
                    {
                        stringInput = objControl.Text.Trim().ToUpper();
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
                                        objControl.Text = stringInput[0] + stringInput.Substring(2, stringInput.Length - 2) + stringInput[1];
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
                                stringResult = CommonFunctions.ValidateHRN(objControl.Text.Trim().ToUpper(), out string stringFormmatHrnID);
                                if (stringResult != "SUCCESS" && stringResult != "")
                                {
                                    CommonFunctions.ShowMessageboot(this, "Invalid MRN");
                                    return false;
                                }
                                else if (stringResult == "SUCCESS")
                                {
                                    objControl.Text = ArrangeHRNNumber(stringFormmatHrnID);
                                    return true;
                                }
                                else
                                {
                                    objControl.Text = ArrangeHRNNumber(stringResult);
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
                            objControl.Focus(); 
                            return false;
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
                boolMROHRN = false;
                boolStatus = true;
                stringResult = "";
                stringInput = "";
            }

            return false;
        }
        private string ArrangeHRNNumber(string stringHRN)//fix
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
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringSub1 = "";
                stringSub2 = "";
                stringResult = "";
            }
            return stringHRN;
        }
        #endregion
        private string PrepareSearchExpression(string strigDeptID)
        {
            string stringOverallExpression = "";
            string stringexp = null;
            string stringOperator = "";
            stringOperator = " AND ";
            bool boolstatus = true;
            string stringCondition = "";
            string stringColumnName = "";
            string stringDataType = ""; 
            string stringInput = "";
            string stringTempExpression = "";
            DataRow[] objDataRowFiltered = null;
            string stringEncrypyValue = "";
            string stringFORMID = "";
            bool booldepID = false;
            DataTable objuserRole = null;
            string stringDEPARTMENTID = "";
            string DEPARTMENTIDMULTIPLE = "";
            string stringinputvalue = ""; 
            try
            {
                DataTable objDataTableColumnsList = null;
                if (ViewState["ADVSEARCHCOLUMNS"] != null) { objDataTableColumnsList = (DataTable)ViewState["ADVSEARCHCOLUMNS"]; }


                for (int intCount = 0; intCount < gvAdvancedSearch.Rows.Count; intCount++)
                {
                    stringCondition = "";
                    stringColumnName = "";
                    stringDataType = ""; 
                    stringInput = "";

                    TextBox objTextboxSearchInput1 = ((TextBox)gvAdvancedSearch.Rows[intCount].FindControl("txtSearchInput"));
                    DropDownList objDropDownListColumnName = ((DropDownList)gvAdvancedSearch.Rows[intCount].FindControl("ddlColumnName"));
                    DropDownList objDropDownListCondition = ((DropDownList)gvAdvancedSearch.Rows[intCount].FindControl("ddlCondition"));
                    stringInput = objTextboxSearchInput1.Text.ToString().Trim().Replace("'", "''");
                    ViewState["textboxinputvalue"] = stringInput;
                    ViewState["DropdownValues"] = objDropDownListColumnName.SelectedValue.ToString();
                    if (objDropDownListCondition.SelectedItem != null)
                    { stringCondition = objDropDownListCondition.SelectedItem.Value; }


                    if (objDropDownListColumnName.SelectedItem != null)
                    { stringColumnName = objDropDownListColumnName.SelectedItem.Value; }
                     

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

                            if (stringDataType.Trim().ToUpper() == "STRING")//hrn
                            {
                                if (stringCondition.Trim().ToUpper() == "LIKE")
                                {
                                    if(stringColumnName.Contains("HRN_ID_MASKED"))
                                    { 
                                        if (stringInput.Trim().Length > 0 && !DoNonCGHHrnValidation(objTextboxSearchInput1))
                                        {
                                            boolstatus = false;
                                        }
                                        if (boolstatus)
                                        { 
                                            stringEncrypyValue = CommonFunctions.HRNtoEncrypyValue(stringInput);

                                            stringOverallExpression += "( " + "mrpats.HRN_ID" + " ='" + stringEncrypyValue.Trim() + "' )";
                                        }                         
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
                            else if (stringDataType.Trim().ToUpper() == "DATE" || stringDataType.Trim().ToUpper() == "DATETIME")
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

                if (stringOverallExpression != null && stringOverallExpression.Trim().Length > 0 && boolstatus)
                {
                    SearchProfilesByExpression(" AND " + stringOverallExpression);
                }
                else if(boolstatus)
                {
                    booldepID = false;
                    if (Request.QueryString["ID"] != null)
                    {
                        stringFORMID = Request.QueryString["ID"].ToString();

                        if (stringFORMID == "FA0010R1V1")
                        {
                            if (Session["UserRolestable"] != null)
                            {
                                objuserRole = (DataTable)Session["UserRolestable"];

                                if (objuserRole != null && objuserRole.Rows.Count > 0)
                                {
                                    if (objuserRole.Select("Group_ID= 'DEPARTMENT SECRETARY'").Length > 0)
                                    {
                                        booldepID = true;
                                    }
                                }
                            }
                        }
                       
                        if(booldepID)
                        {
                            stringDEPARTMENTID = "";
                            DEPARTMENTIDMULTIPLE = "";
                            if (Session["DEPARTMENTID"] != null)
                            {
                                stringDEPARTMENTID = Session["DEPARTMENTID"].ToString();
                            }
                            else if (Session["DEPARTMENTIDMULTIPLE"] != null)
                            {
                                DEPARTMENTIDMULTIPLE = Session["DEPARTMENTIDMULTIPLE"].ToString();
                            }
                            if(stringDEPARTMENTID.Length >0 )
                            {
                                SearchProfilesByExpression("AND mrdep.DEPT_ID='" + stringDEPARTMENTID.ToString().ToUpper() + "' ");
                            }
                            else if (DEPARTMENTIDMULTIPLE.Length > 0)
                            { 
                                SearchProfilesByExpression("AND mrdep.DEPT_ID IN (" + DEPARTMENTIDMULTIPLE + ")  ");
                            } 


                        }
                        else
                        {
                            if (stringFORMID == "FA0017R1V1")
                            {
                                if (ViewState["textboxinputvalue"] != null)
                                {
                                    stringinputvalue = ViewState["textboxinputvalue"].ToString();
                                    if (stringinputvalue != null && stringinputvalue.Trim().Length > 0)
                                    {
                                        SearchProfilesByExpression("");
                                    }
                                    else
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Please give at least one search criteria");
                                        Session[stringFORMID + "_idGrid"] = null;
                                        gvUserHistory.DataSource = null;
                                        gvUserHistory.DataBind();
                                        lblTotalRecords.InnerText = "0";
                                        PopulatePager(0, intpageIndex);

                                    }
                                }
                            }
                            else
                            {
                                SearchProfilesByExpression("");
                            }
                             
                        }
                    }
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
                stringCondition = "";
                stringColumnName = "";
                stringDataType = ""; 
                stringInput = "";
                stringTempExpression = "";
                objDataRowFiltered = null;
                stringEncrypyValue = "";
                stringFORMID = "";
                booldepID = false;
                objuserRole = null;
                stringDEPARTMENTID = "";
                DEPARTMENTIDMULTIPLE = "";
                stringinputvalue = "";
            }
        }

        private void SearchProfilesByExpression(string stringOverallExpression)
        {
            try
            {
                
                if((ViewState["DropdownValues"] != null && ViewState["DropdownValues"].ToString().Length > 0) && ViewState["textboxinputvalue"]!=null&& ViewState["textboxinputvalue"].ToString().Length == 0)
                {
                    CommonFunctions.ShowMessageboot(this, "Please give at least one search criteria");
                }
                else
                {
                    LoadRecord(stringOverallExpression, "");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        //Paging
        private void PopulatePager(int recordCount, int currentPage)
        {
            ViewState["lastpagepagig"] = true;
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
                if (pageCount == 1 || pageCount == 0)
                {
                    ViewState["lastpagepagig"] = false;
                    pages.Clear();
                   // pages.Add(new ListItem("Last", pageCount.ToString()));
                }

                rptPager.DataSource = pages;
                rptPager.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        protected void rptPager_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkPage = (LinkButton)e.Item.FindControl("lnkPage");

                if (lnkPage != null)
                {
                    if (ViewState["lastpagepagig"] != null)
                    {
                        bool boolpaging = (bool)ViewState["lastpagepagig"];
                        if (!boolpaging)
                        {
                            lnkPage.Enabled = false;
                        }
                    }
                }
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
                    Session["intrecFrom"] = 0;
                    intrecTo = CommonFunctions.GridViewPagesize("Orderhistorygridviewpagesize");
                    Session["intrecTo"] = intrecTo;
                }
                else
                {
                    int intrecFrom1 = (intpageIndex * intrecTo) - intrecTo;
                    intrecFrom = intrecFrom1 + 1;
                    Session["intrecFrom"] = intrecFrom;
                    intrecTo = intrecFrom1 + CommonFunctions.GridViewPagesize("Orderhistorygridviewpagesize");
                    Session["intrecTo"] = intrecTo;
                }
                Session["bool"] = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        #endregion

        #region DynamicBindings
        private void LoadDynamicGridBindings(String StringScreenFormID)
        {
            int interrorcount = 0; 
            string[] stringOutputResult = null; 
            DataSet[] objDatasetResult = null; 
            string stringststus = "";
            string stringuserid = "";
            DataSet objDatasetLocalization = new DataSet();
            try
            { 
                stringOutputResult = new string[3];
                if (Session["G11EOSUser_Name"] != null)
                {
                    stringuserid = Session["G11EOSUser_Name"].ToString();
                }
                clsCertificateValidation.EnableTrustedHosts();
                using (GSXMLASBusinessServices.Service1Client objservice1client1 = new GSXMLASBusinessServices.Service1Client(CommonFunctions.CreateServiceBasicHttpBinding(), CommonFunctions.objEndpointAddressBusinessServices))
                {
                    objDatasetResult = objservice1client1.GetUIRelatedTablesR1V1("ENG", stringuserid, CommonFunctions.GetG10XMLServerServiceSettings01(StringScreenFormID), out stringststus, out stringOutputResult);
                    if (objservice1client1 != null)
                        objservice1client1.Close();

                }
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult[1].Tables.Count > 0)
                    {
                        objDatasetLocalization.Merge(objDatasetResult[1]);
                        foreach (DataRow objDataRow in objDatasetLocalization.Tables[0].Select("", "list_view_position ASC").CopyToDataTable<DataRow>().Rows)
                        {
                            if (objDataRow["Visibility"].ToString() == "Y")
                            {
                                if (objDataRow["list_view_position"].ToString() == "C001")
                                {
                                    BoundField bfield = new BoundField();
                                    bfield.HeaderText = objDataRow["display_name"].ToString();
                                    bfield.DataField = objDataRow["column_name"].ToString(); 
                                    bfield.SortExpression = objDataRow["column_name"].ToString() ; 
                                    if (objDataRow["data_type"].ToString().ToUpper() == "DATE")
                                    {
                                        bfield.DataFormatString = "{0:dd-MM-yyyy}";
                                    }
                                    dynamicColumnCounter++;

                                    gvUserHistory.Columns.Add(bfield);
                                      
                                }
                                else
                                {

                                    BoundField bfield = new BoundField();
                                    bfield.HeaderText = objDataRow["display_name"].ToString();
                                    bfield.DataField = objDataRow["column_name"].ToString(); 
                                    bfield.SortExpression = objDataRow["column_name"].ToString() ;

                                    if (objDataRow["data_type"].ToString().ToUpper() == "DATE")
                                    {
                                        bfield.DataFormatString = "{0:dd-MM-yyyy}";
                                    }

                                    dynamicColumnCounter++; 
                                    gvUserHistory.Columns.Add(bfield);

                                }


                                if (objDataRow["column_width"].ToString() == "1"
                                    && (objDataRow["column_name"].ToString() == "Select" || objDataRow["column_name"].ToString() == "visibility" || objDataRow["column_name"].ToString() == "SELECT" || objDataRow["column_name"].ToString() == "TERM_FLAG" || objDataRow["column_name"].ToString() == "REFERENCE_4"))
                                {
                                    gvUserHistory.HeaderStyle.Width = 50;  
                                }
                                else
                                {
                                    var var = objDataRow["column_width"].ToString();
                                    gvUserHistory.HeaderStyle.Width = 50;
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
                stringOutputResult = null;
                objDatasetResult = null;
                stringststus = "";
                stringuserid = "";
                objDatasetLocalization = null;
                interrorcount = 0;
            }
        }


        protected void gvUserHistory_Sorting(object sender, GridViewSortEventArgs e)
        {  
            DataSet objDataSetSort1 = null;
            objDataSetSort1 = new DataSet();
            string stringalise = ""; 
            string stringFORMID = "";
            if (Request.QueryString["ID"] != null)
            {
                stringFORMID = Request.QueryString["ID"].ToString();
            }
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringColumnName = "";
            string stringcolumnnamereports = "";
            try
            {
                if (sender != null)
                {
                    stringColumnName = e.SortExpression;
                    ViewState["ColumnNames"] = stringColumnName;
                    if (stringColumnName != null && stringColumnName.Trim().Length > 0)
                    {
                        
                        if(stringColumnName!=null&& stringColumnName.ToString()== "LST_GROUP_NAME")
                        {
                            stringalise ="lstref";
                            ViewState["vsSortDirection"] = (ViewState["vsSortDirection"] != null && ViewState["vsSortDirection"].ToString().Trim() == "ASC") ? " DESC" : " ASC";
                            ViewState["vsSortExpression"] = stringalise + "." + "short_name " + ViewState["vsSortDirection"].ToString();
                            LoadRecord((string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString(), 0);

                        }
                        else
                        {
                            stringalise = AlisenameCondition(stringalise, stringFORMID, stringcolumnnamereports);
                            if (stringFORMID == "FA0017R1V1")
                            {
                                LoadRecord((string)ViewState["vsSearchCondition"], "", intrecFrom); 
                            }
                            else
                            {
                                if(stringColumnName== "REPORT_PURPOSE_NAME")
                                {
                                    stringcolumnnamereports = "short_name";
                                    ViewState["vsSortDirection"] = (ViewState["vsSortDirection"] != null && ViewState["vsSortDirection"].ToString().Trim() == "ASC") ? " DESC" : " ASC";
                                    ViewState["vsSortExpression"] = stringalise + "." + stringcolumnnamereports + ViewState["vsSortDirection"].ToString();
                                    LoadRecord((string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString(), intrecFrom);

                                }
                                else
                                {
                                    ViewState["vsSortDirection"] = (ViewState["vsSortDirection"] != null && ViewState["vsSortDirection"].ToString().Trim() == "ASC") ? " DESC" : " ASC";
                                    ViewState["vsSortExpression"] = stringalise + "." + stringColumnName + ViewState["vsSortDirection"].ToString();
                                    LoadRecord((string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString(), intrecFrom);

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
                objDataSetSort1 = null;
                stringalise = "";
                stringFORMID = "";
                stringbeid = "";
                stringColumnName = "";
                stringcolumnnamereports = "";
            }
        }

        #endregion

        protected void imgbtnprint_Click(object sender, ImageClickEventArgs e)
        {
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = Request.QueryString["ID"].ToString();
            string stringOrderBy = "";
            byte[] byteArray;
            string stringServerPath = "";
            string stringFunctionName = "List1R1V1";
            string stringExportName = "";
            string stringexp1 = "";
            try
            {
                HtmlGenericControl home = (HtmlGenericControl)Page.Master.FindControl("hdrPageTitle");
                stringExportName= home.InnerText.ToString();

                if (Session["exportcondition"] != null)
                {
                    stringexp1 = Session["exportcondition"].ToString();
                }
                else
                {
                    stringexp1 = "";
                }
                stringServerPath = CommonFunctions.Export(stringFunctionName, stringformid, stringexp1, stringOrderBy, stringExportName, out byteArray, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                { 
                    CommonFunctions.OpenExportedFileR1V1(this, byteArray, stringExportName.ToString().Replace(".", ""), "EXCEL");
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
        }
        private static string SortingExp(string SortExpression, string stringFORMID)
        {
            try
            {
                if (SortExpression.Length == 0)
                {
                    if (stringFORMID.ToString() == "FA0001R1V1")
                    {
                        SortExpression = "mrcanres.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0005R1V1")
                    {
                        SortExpression = "costcen.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0006R1V1")
                    {
                        SortExpression = "mrcurry.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0008R1V1")
                    {
                        SortExpression = "mrdelas.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0009R1V1")
                    {
                        SortExpression = "mrdelmos.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0016R1V1")
                    {
                        SortExpression = "palon.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0018R1V1")
                    {
                        SortExpression = "mrpaytys.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0020R1V1")
                    {
                        SortExpression = "peits.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0021R1V1")
                    {
                        SortExpression = "mrpurs.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0023R1V1")
                    {
                        SortExpression = "mreeks.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0025R1V1")
                    {
                        SortExpression = "mrrefors.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0026R1V1")
                    {
                        SortExpression = "mrreqtyp.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0027R1V1")
                    {
                        SortExpression = "mrreq.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0028R1V1")
                    {
                        SortExpression = "mrreqts.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0029R1V1")
                    {
                        SortExpression = "MRRETYS.ORDER_ID ASC";
                    }
                    else if (stringFORMID.ToString() == "FA0030R1V1")
                    {
                        SortExpression = "tyvit.ORDER_ID ASC";
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

            return SortExpression;
        }

    }
}