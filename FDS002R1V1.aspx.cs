using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CLUSTER_MRTS
{
    public partial class FDS002R1V1 : System.Web.UI.Page
    { 
        string stringformId = "Orderhistorygridviewpagesize";
        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {

            if (CommonFunctions.IsActive())
            {
                
                string stringOverallExpression = "";
                try
                {
                    if (!IsPostBack)
                    {
                        VerifyAccessRights(); 
                        hdnfrom.Value = "0";
                        hdnpageIndex.Value = "0";
                        hdnto.Value = CommonFunctions.GridViewPagesize(stringformId).ToString();
                        CommonFunctions.HeaderName(this, "FDS002R1V1");
                        ViewState["vsSortDirection"] = " ASC";
                        ViewState["vsSortExpression"] = "";
                        ViewState["bool"] = true;
                        ViewState["particulartabcondition"] = "PENDING ASSIGNED";
                        LoadAdvancedSearchColumnNames();//for column name filters ddl
                        LoadAdvancedSearchConditions();//for condition ddl
                        LoadAdvancedSearchGrid();//for default 1st line
                        stringOverallExpression = "and mrreg.mr_status IN ('PENDING ASSIGNED')";
                        LoadRecord("PENDING ASSIGNED", "LOADDEFAULT", stringOverallExpression, "");
                    }
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
                //objDataSet = null;
                stringOutputResult = null;
                stringServiceType = "";
                stringFormID = "";
                stringOrderBy = "";
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
            }
        }

        private void FillExistingSearchValues()
        {
            DataTable objDataTable = null;
            string stringColumnName = "";
            string stringCondition = "";
            string stringSearchInput = "";
            string stringExpression = "";
            string stringSNo = "";
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
                stringExpression = "";
                stringSNo = "";
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
            string stringTempSerialNo = "";
            DataRowView objDataRowView = null;
            DataRow objDataRow = null;
            DataRow[] objDataRowFiltered = null;
            DataTable objDataTable = null;
            DataRow objDataRowCurrent = null;
            string stringColumnName = "";
            string stringCondition = "";
            string stringSearchValue = "";
            try
            {
                GridViewRow objGridViewRow = e.Row;
                string stringSort = string.Empty;
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
                stringTempSerialNo = "";
                objDataRowView = null;
                objDataRow = null;
                objDataRowFiltered = null;
                objDataTable = null;
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
            ClearValues();
        }
        #endregion

        #region method


        private void LoadRecord(string stringaddconditions =null, string stringTYPE = null, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {
            DataSet objDataSet = null;
            string stringuserid = "";
            string stringDEPARTMENTID = "";
            string DEPARTMENTIDMULTIPLE = "";
            int intRecordCount = 0;

            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                if(Session["G11EOSUserID"] != null)
                {
                    stringuserid = Session["G11EOSUserID"].ToString();
                }
                if (Session["DEPARTMENTID"] != null)
                {
                    stringDEPARTMENTID = Session["DEPARTMENTID"].ToString();
                }
                else if (Session["DEPARTMENTIDMULTIPLE"] != null)
                {
                    DEPARTMENTIDMULTIPLE = Session["DEPARTMENTIDMULTIPLE"].ToString();
                }
                ViewState["vsSearchCondition"] = Condition;
                intRecordCount = 0;
                if (stringaddconditions == "PENDING SUP VETTING")
                {
                    Condition = "AND mrreg.be_id = '" + stringbeid + "' AND mrreg.mr_status = 'PENDING REPORT' AND mrassvr.emp_no ='" + stringuserid + "'  and mrassvr.VERIFY_REF = 'VERIFIER' AND mrassvr.STATUS = 'IN-PROGRESS'" + "AND U.USER_ID = '" + stringuserid + "' and d.SECRETARY_ADID='" + stringuserid + "'";

                }
                else
                {
                    //if (stringDEPARTMENTID.Length > 0)
                    //{
                    //    Condition += "AND U.USER_ID='" + stringuserid + "' AND mrreg.DEPT_ID='" + stringDEPARTMENTID + "' and d.SECRETARY_ADID='" + stringuserid + "'" ;
                    //}
                    //else if (DEPARTMENTIDMULTIPLE.Length > 0)
                    //{
                    //    Condition += "AND U.USER_ID='" + stringuserid + "' AND mrreg.DEPT_ID IN (" + DEPARTMENTIDMULTIPLE + ") " + " and d.SECRETARY_ADID='" + stringuserid + "'";
                    //}
                    //else
                    //{
                    //    Condition += "AND U.USER_ID='" + stringuserid + "'  and d.SECRETARY_ADID='" + stringuserid + "'";
                    //}
                    Condition += "AND U.USER_ID='" + stringuserid + "'";
                }
              
                objDataSet = GetRecords(out intRecordCount, Condition, SortExpression, RecordFrom, RecordTo);
                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                {
                    string stringMRStatus = objDataSet.Tables[0].Rows[0]["MR_STATUS"].ToString();

                    if (stringTYPE.Length > 0 && stringTYPE == "LOADDEFAULT")
                    {
                        ViewState["SelectedRecordsMANTANIN"] = objDataSet.Tables[0].Clone();
                    }
                    if(stringaddconditions == "ALL")
                    {
                        btntriggertab1.Text = "ALL ( " + intRecordCount.ToString() + " )";
                    }
                    else if (stringaddconditions == "PENDING ASSIGNED")
                    {
                        btntriggertab2.Text = "Pending Assign ( " + intRecordCount.ToString() + " )";
                    }
                    else if (stringaddconditions == "PENDING REPORT")
                    {
                        btntriggertab3.Text = "Writing Report Tracking ( " + intRecordCount.ToString() + " )";
                    }
                    else if (stringaddconditions == "PENDING SUP VETTING")
                    {
                        btntriggertab4.Text = "Pending Report Verification ( " + intRecordCount.ToString() + " )";
                    }
                    else if (stringaddconditions == "PENDING RELEASE TO HIMS")
                    {
                        btntriggertab5.Text = "Pending Release ( " + intRecordCount.ToString() + " )";
                    }
                    ViewState["FE0003R1V1_idGrid"] = objDataSet.Tables[0];

                    if (stringMRStatus == "PENDING ASSIGNED")
                    {
                        btntriggertab2.BackColor = ColorTranslator.FromHtml("#397279");
                        btntriggertab1.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor =   Color.FromArgb(99, 191, 247);
                    }
                    else if (stringMRStatus == "PENDING REPORT")
                    {
                        if(stringaddconditions == "PENDING SUP VETTING")
                        {
                            btntriggertab4.BackColor = ColorTranslator.FromHtml("#397279");
                            btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab1.BackColor = btntriggertab5.BackColor = Color.FromArgb(99, 191, 247);

                        }
                        else
                        {
                            btntriggertab3.BackColor = ColorTranslator.FromHtml("#397279");
                            btntriggertab2.BackColor = btntriggertab1.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = Color.FromArgb(99, 191, 247);

                        }
                    }
                    //else if (stringMRStatus == "PENDING SUP VETTING")
                    //{
                    //    btntriggertab4.BackColor = ColorTranslator.FromHtml("#397279");
                    //    btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab1.BackColor = btntriggertab5.BackColor =   Color.FromArgb(99, 191, 247);
                    //}
                    else if (stringMRStatus == "PENDING RELEASE TO HIMS")
                    {
                        btntriggertab5.BackColor = ColorTranslator.FromHtml("#397279");
                        btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab1.BackColor =   Color.FromArgb(99, 191, 247);
                    } 
                }
                else
                {
                    btntriggertab1.Text = "ALL";
                    btntriggertab2.Text = "Pending Assign";
                    btntriggertab3.Text = "Writing Report Tracking";
                    btntriggertab4.Text = "Pending Report Verification";
                    btntriggertab5.Text = "Pending Release";

                    ViewState["FE0003R1V1_idGrid"] = null; 
                    gvUserHistory.DataSource = null;
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
                objDataSet = null;
                stringuserid = "";
                stringDEPARTMENTID = "";
                DEPARTMENTIDMULTIPLE = "";
                intRecordCount = 0;
            }
        }

        public DataSet GetRecords(out int intRecordCount, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {

            DataSet objDatasetResult = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringFORMID = "";
            stringFORMID = "FDS001R1V1";
            intRecordCount = 0;
            int inthdnpageIndex = 0;
            string stringuserID = "";
            int intRecordFrom = 0;
            int intRecordTo = 0;
            string stringmessage = "";
            intRecordFrom = Convert.ToInt32(hdnfrom.Value.ToString());
            intRecordTo = Convert.ToInt32(hdnto.Value.ToString());
            SortExpression = "dep.short_name asc,mrreg.CREATED_ON desc";
            try
            {
                ViewState["vsSearchCondition"] = Condition;
                stringuserID = HttpContext.Current.Session["G11EOSUserID"].ToString(); 
                objDatasetResult = CommonFunctions.List33R1V1("List33R1V1", stringuserID, stringFORMID, Condition, SortExpression, intRecordFrom, intRecordTo, out intRecordCount, out interrorcount, out stringOutputResult);

                inthdnpageIndex = Convert.ToInt32(hdnpageIndex.Value.ToString());
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
                        gvUserHistory.DataSource = null;
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
                intRecordFrom = 0;
                intRecordTo = 0;
                stringmessage = "";
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
                        LoadRecord("","", (string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString(), 0);
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

        private string PrepareSearchExpression(string stringaddconditions)
        {
            string stringOverallExpression = "";
            string stringexp = null;
            string stringOperator = "";
            stringOperator = " AND ";
            string stringCondition = "";
            string stringColumnName = "";
            string stringDataType = "";
            string stringAlliseName = "";
            string stringInput = "";
            string stringTempExpression = "";
            DataTable objDataTableColumnsList = null;
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
                            DataRow[] objDataRowFiltered = objDataTableColumnsList.Select(stringTempExpression);
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
                            stringOverallExpression += "and mrreg.mr_status IN ('PENDING ASSIGNED','PENDING REPORT','PENDING SUP VETTING','PENDING RELEASE TO HIMS')";
                        }
                        else
                        {
                            stringOverallExpression += " mrreg.mr_status IN ('PENDING ASSIGNED','PENDING REPORT','PENDING SUP VETTING','PENDING RELEASE TO HIMS')";
                        }
                    }
                    else
                    {
                        ViewState["particulartabcondition"] = stringaddconditions;
                        if (stringOverallExpression.Length > 0)
                        {
                            stringOverallExpression += " and mrreg.mr_status='" + stringaddconditions + "'";
                        }
                        else
                        {
                            stringOverallExpression += " mrreg.mr_status='" + stringaddconditions + "'";
                        }
                        
                    }
                }
                else
                {
                    ViewState["particulartabcondition"] = null;
                }
                if (stringOverallExpression != null && stringOverallExpression.Trim().Length > 0)
                {
                    SearchProfilesByExpression(stringaddconditions," AND " + stringOverallExpression);
                }
                else
                {
                    string stringOverallExpression01 = "and mrreg.mr_status IN ('PENDING ASSIGNED','PENDING REPORT','PENDING SUP VETTING','PENDING RELEASE TO HIMS')";

                    SearchProfilesByExpression(stringaddconditions, stringOverallExpression01);
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
                stringAlliseName = "";
                stringInput = "";
                stringTempExpression = "";
                objDataTableColumnsList = null;
            }
        }

        private void SearchProfilesByExpression(string stringaddconditions, string stringOverallExpression)
        {
            try
            {
                LoadRecord(stringaddconditions,"", stringOverallExpression, "mrreg.MODIFIED_ON desc");
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
                btntriggertab1.BackColor = System.Drawing.ColorTranslator.FromHtml("#397279");
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = System.Drawing.Color.FromArgb(99, 191, 247);

                PrepareSearchExpression("ALL");

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
                btntriggertab2.BackColor = System.Drawing.Color.FromArgb(57, 114, 121);
                btntriggertab1.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = System.Drawing.Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("PENDING ASSIGNED");
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
                btntriggertab3.BackColor = System.Drawing.Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab1.BackColor = btntriggertab4.BackColor = btntriggertab5.BackColor = System.Drawing.Color.FromArgb(99, 191, 247);

                PrepareSearchExpression("PENDING REPORT");
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
                btntriggertab4.BackColor = System.Drawing.Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab1.BackColor = btntriggertab5.BackColor = System.Drawing.Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("PENDING SUP VETTING");
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
                btntriggertab5.BackColor = System.Drawing.Color.FromArgb(57, 114, 121);
                btntriggertab2.BackColor = btntriggertab3.BackColor = btntriggertab4.BackColor = btntriggertab1.BackColor = System.Drawing.Color.FromArgb(99, 191, 247);
                PrepareSearchExpression("PENDING RELEASE TO HIMS");
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
         

        }


        #endregion
          
 
        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        { 
        }

        protected void lnkbtnRequest_ID_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringReqNo = "";
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
                            stringReqNo = stringValues[0];
                            Session["REQUESTID_FDS001R1V1"] = stringReqNo;
                            Response.Redirect("FDS001R1V1.aspx");
                        }
                        else
                        {
                            Session["REQUESTID_FDS001R1V1"] = null;
                        }
                    }
                    else
                    {
                        Session["REQUESTID_FDS001R1V1"] = null;
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
                stringValues = null;
                stringReqNo = "";
            }
        }
        private void VerifyAccessRights()
        {

            DataSet objDatasetResult = null;

            string stringstatus = "";
            string[] stringOutputResult = null;
            string[] stringComponent = null;
            DataRow objDataRow = null; 
            ViewState["boolaccess"] = false; 
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FDS002R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {  

                    }
                    else
                    {
                        Response.Redirect("PageAccessDenied.aspx", true);
                    }

                }

                stringComponent = new string[1];
                stringComponent[0] = "FDS001R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        ViewState["boolaccess"] = true;
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
                //        ViewState["boolaccess"] = true;
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
                //        ViewState["boolaccess"] = true;
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
                //        ViewState["boolaccess"] = true;
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
        protected void gvUserHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet objdataset = new DataSet();
            DataTable objdatatableselectedvalue = null;
            string stringreqID = "";
            DataRow[] objDataselectedrow = null;
            DataRow objDataRow = null;
            string stringMRSTATUS = "";
            string stringPRIORITY_FLAG = "";
            bool boolisReadOnly = true;
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    objDataRow = ((DataRowView)e.Row.DataItem).Row;
                    var objLabel = (Label)e.Row.FindControl("lbluniqid");
                    var objLabelMRStatus = (Label)e.Row.FindControl("lblRsts");
                    var Icon01 = (System.Web.UI.WebControls.Image)e.Row.FindControl("imggridflag02");
                    stringreqID = objLabel.Text.ToString();

                    //for checkbox auto selection during paging start
                    if (ViewState["SelectedRecordsMANTANIN"] != null)
                    {
                        objdatatableselectedvalue = (DataTable)ViewState["SelectedRecordsMANTANIN"];
                        if (objdatatableselectedvalue != null && objdatatableselectedvalue.Rows.Count > 0 && objdatatableselectedvalue.Select("REQUEST_ID='" + stringreqID + "'").Length > 0)
                        {
                            objDataselectedrow = objdatatableselectedvalue.Select("REQUEST_ID='" + stringreqID + "'");
                            if (objDataselectedrow != null && objDataselectedrow.Length > 0)
                            {
                            //    chkFlag.Checked = true;
                            //    chkFlag.Enabled = true;
                            }

                        }
                    }
                    //end

                    if (objDataRow != null)
                    {
                        stringMRSTATUS = objDataRow["MR_STATUS"].ToString().ToUpper();
                        if (stringMRSTATUS == "FORWARDED")
                        {
                            //chkFlag.Enabled = false;
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
                        LinkButton lnkbtnReceiptNo = (LinkButton)e.Row.FindControl("lnkbtnRequest_ID");

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
                objDataselectedrow = null;
                objDataRow = null;
                stringMRSTATUS = "";
                stringPRIORITY_FLAG = "";
            }
        }
    }
}