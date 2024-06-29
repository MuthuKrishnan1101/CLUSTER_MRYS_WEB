using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FDU0001R1V1 : System.Web.UI.Page
    {
        public int intrecFrom = 0;
        public int intrecTo = 0; 
        public int intpageIndex = 0; 
        public string stringformIdPaging = "FDU0001R1V1PopupPaging";

        public int intrecFromRight = 0;
        public int intrecToRight = 0;
        public int intpageIndexRight = 0; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CommonFunctions.IsActive())
            {
                string stringGroupID = "";
                string stringBOID = "";
                try
                {
                    intrecTo = CommonFunctions.GridViewPagesize(stringformIdPaging);
                    intrecToRight = CommonFunctions.GridViewPagesize(stringformIdPaging);
                     
                    if (!IsPostBack)
                    {
                        VerifyAccessRights();
                        CommonFunctions.HeaderName(this, "FDU0001R1V1");
                        lblTotalRecordsLeft.InnerText ="0";
                        gvAvailableStaffs.DataSource = null;
                        gvAvailableStaffs.DataBind();

                        lblTotalRecordsright.InnerText = "0";
                        gvAddedStaffs.DataSource = null;
                        gvAddedStaffs.DataBind();

                        txtGroupName.Text = "";
                        txtHRSequence.Text = "";

                        lblTotalRecordsright.InnerText = "0";
                        gvAddedStaffs.DataSource = null;
                        gvAddedStaffs.DataBind();
                        txtGroupName.Enabled = true;

                        if (Session["GROUP_IDFDU0001"] != null && Session["GROUP_IDFDU0001"].ToString().Trim().Length > 0)
                        {
                            txtGroupName.Enabled = false;
                            stringBOID = CommonFunctions.GETBussinessEntity();
                            if(Session["GROUP_IDFDU0001"] != null)
                            {
                                stringGroupID = Session["GROUP_IDFDU0001"].ToString();
                                if(stringGroupID.Length > 0)
                                {
                                    LoadGroupStaffs(stringBOID, stringGroupID);
                                }
                            }                          
                        }
                        else
                        { 
                        } 
                    }
                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }
                finally
                {
                    stringGroupID = "";
                    stringBOID = "";
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
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FDU0001R1V1";
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
                stringstatus = "";
                stringOutputResult = null;
                stringComponent = null;
            }
        }
        private void LoadGroupStaffs(string stringBOID, string stringGroupID)
        {
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FDU0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataSet objDataSet = null;
            DataTable objDataTable = null;
            string stringexp = "";
            string stringServiceType = "";
            string stringHRNseq = "";
            DataRow objDataRowTemp = null;
            string stringGroupName = "";
            string stringHRNID = "";
            string stringHRNID01 = "";
            try
            {

                if (stringBOID != null && stringBOID.Length > 0)
                    stringexp += "and AOF.be_id='" + stringBOID + "'";

                if (stringGroupID != null && stringGroupID.Length > 0)
                    stringexp += "and AOF.GRP_ID='" + stringGroupID + "'";
                Session["export2"] = stringexp;

                stringServiceType = "List1R1V1";
                objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                if (interrorcount == 0)
                {
                    if (objDataSet != null && objDataSet.Tables["t1"] != null && objDataSet.Tables["t1"].Rows.Count > 0)
                    {
                        objDataRowTemp = objDataSet.Tables["t1"].Rows[0]; 
                        stringGroupName = objDataRowTemp["GRP_NAME"].ToString();
                        stringHRNID = objDataRowTemp["start_value"].ToString();
                        stringHRNID01 = objDataRowTemp["end_value"].ToString();
                        stringHRNseq = objDataRowTemp["HR_SEQUENCE"].ToString();

                        txtHRSequence.Text = stringHRNseq;
                        hfGroupID.Value = stringGroupID;
                        txtGroupName.Text = stringGroupName; 
                         
                    }
                    if (objDataSet != null && objDataSet.Tables["table1"] != null && objDataSet.Tables["table1"].Rows.Count > 0)
                    {
                        Session["gvAddedStaffs"] = objDataSet;
                        objDataSet.Tables["table1"].DefaultView.Sort = "rec_no asc";
                        objDataTable = objDataSet.Tables["table1"].DefaultView.ToTable();

                        ViewState["AddedProfiles"] = objDataTable;
                        gvAddedStaffs.DataSource = objDataTable;
                        gvAddedStaffs.DataBind();

                        lblTotalRecordsright.InnerText = "0";
                        if (objDataTable != null && objDataTable.Rows.Count > 0)
                        {
                            lblTotalRecordsright.InnerText = objDataTable.Rows.Count.ToString();
                        }
                        if (objDataTable.Columns.Contains("DML_INDICATOR") == false)
                            objDataTable.Columns.Add("DML_INDICATOR");
                        if (objDataTable != null)
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


                        objDataTable.AcceptChanges();
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
                objDataSet = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = "";
                stringOrderBy = "";
                intFromRecord = 0;
                intToRecord = 0;
                stringexp = "";
                stringServiceType = "";
                stringHRNseq = "";
                objDataRowTemp = null;
                stringGroupName = "";
                stringHRNID = "";
                stringHRNID01 = "";
            }
        }
        protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
        {

        }


        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgbtnAudit_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void LnkbtnSort_Click(object sender, EventArgs e)
        {

        }
        #region grids
        protected void gvAvailableStaffs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringSort = string.Empty;
            string stringuser_id = "";
            string stringuser_name = "";
            string stringbe_id = "";
            DataRow objDataRow = null;
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null) { return; }

                objDataRow = ((DataRowView)e.Row.DataItem).Row;
                stringuser_id = objDataRow["user_id"].ToString();
                stringuser_name = objDataRow["user_name"].ToString();
                stringbe_id = objDataRow["be_id"].ToString();


                if (objGridViewRow.FindControl("hfIDsLeft") != null)
                {
                    ((HiddenField)objGridViewRow.FindControl("hfIDsLeft")).Value = stringuser_id + "~" + stringuser_name + "~" + stringbe_id;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringSort = string.Empty;
                stringuser_id = "";
                stringuser_name = "";
                stringbe_id = "";
                objDataRow = null;
            }
        }
        protected void gvAddedStaffs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string stringSort = string.Empty;
            string stringuser_id = "";
            string stringuser_name = "";
            string stringbe_id = "";
            DataRow objDataRow = null;
            try
            {
                GridViewRow objGridViewRow = e.Row;
                if (objGridViewRow.DataItem == null) { return; }

                objDataRow = ((DataRowView)e.Row.DataItem).Row;
                stringuser_id = objDataRow["user_id"].ToString();
                stringuser_name = objDataRow["user_name"].ToString();
                stringbe_id = objDataRow["be_id"].ToString();

                if (objGridViewRow.FindControl("hfIDsRight") != null)
                {
                    ((HiddenField)objGridViewRow.FindControl("hfIDsRight")).Value = stringuser_id + "~" + stringuser_name + "~" + stringbe_id;
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }
        protected void chkSelectAllLeft_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    CheckBox objCheckBox = sender as CheckBox;
                    for (int intCount = 0; intCount < gvAvailableStaffs.Rows.Count; intCount++)
                    {
                        ((CheckBox)gvAvailableStaffs.Rows[intCount].FindControl("chkSelectLeft")).Checked = objCheckBox.Checked;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }

        }

        protected void chkSelectAllRight_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    CheckBox objCheckBox = sender as CheckBox;
                    for (int intCount = 0; intCount < gvAddedStaffs.Rows.Count; intCount++)
                    {
                        ((CheckBox)gvAddedStaffs.Rows[intCount].FindControl("chkSelectRight")).Checked = objCheckBox.Checked;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }

        }

        #endregion

        #region search
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDataSet = null;
            string[] stringOutputResult = null;
            string stringServiceType = "SERVER_SERVICE_LIST";
            string stringConfigId = "UserSummaryR1V2"; 
            string stringOrderBy = "";
            DataTable objDataTable = null;
            int intRecordFrom = 0;
            int intRecordTo = 0;
            try
            {
                if (EmptyValidation())
                {
                    intRecordFrom = intrecFrom;
                    intRecordTo = intrecTo;
                    stringOutputResult = new string[3]; 
                    objDataSet = CommonFunctions.SelectionServiceClient(stringServiceType, stringConfigId, PrepareSearchExpression(), stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);
                    PopulatePager(intTotalRecord, intpageIndex);
                    lblTotalRecordsLeft.InnerText = intTotalRecord.ToString();
                    if (interrorcount == 0)
                    {
                        if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                        { 
                               Session["gvAvailableStaffs01"] = objDataSet;
                            objDataSet.Tables[0].DefaultView.Sort = "user_name asc";
                            objDataTable = objDataSet.Tables[0].DefaultView.ToTable();
                            if (objDataTable.Columns.Contains("DML_INDICATOR") == false)
                                objDataTable.Columns.Add("DML_INDICATOR");


                            if (ViewState["AddedProfilesData"] != null) { } else { ViewState.Add("AddedProfilesData", objDataTable); }
                            if (ViewState["AddedProfiles"] != null) { } else { ViewState.Add("AddedProfiles", objDataTable.Clone()); }
                            gvAvailableStaffs.DataSource = objDataTable;
                            gvAvailableStaffs.DataBind();
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "No Records Found");
                            gvAvailableStaffs.DataSource = null;
                            gvAvailableStaffs.DataBind(); 
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
                objDataSet = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;  
                stringOrderBy = "";
                objDataTable = null;
                intRecordFrom = 0;
                intRecordTo = 0;
            }

        }
        //for paging for bio data
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
                Repeater2.DataSource = pages;
                Repeater2.DataBind();
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

                btnsearch_Click(null, null);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private string PrepareSearchExpression()
        {
            string stringExpression = null;
            try
            { 
                
                if (txtuserID.Text.Length > 0 && txtuserID.Text.Trim() != "%")
                {
                    stringExpression += txtuserID.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(USR.user_id)  LIKE UPPER('%" + txtuserID.Text.Trim() + "%'))" : "";
                }

                if (txtuserNAme.Text.Length > 0 && txtuserNAme.Text.Trim() != "%")
                {
                    stringExpression += txtuserNAme.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(USR.user_name)  LIKE UPPER('%" + txtuserNAme.Text.Trim() + "%'))" : "";
                } 
                stringExpression += "AND USR.delmark = 'N'";
                 
                return stringExpression;
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
                return "";
            }
            finally
            {
                stringExpression = null;
            }
        }
        private bool EmptyValidation()
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "You must enter the value for at least one field to search. " + "\\r\\n";

                if (txtuserID.Text.Trim().Length == 0 && txtuserNAme.Text.Trim().Length == 0)
                {
                    boolStatus = false;
                }

                if (!boolStatus)
                {
                    if (stringOverallMsg.Trim().Length > 0)
                    {
                        stringOverallMsg = stringOverallMsg.Trim();
                        CommonFunctions.ShowMessage(stringOverallMsg);
                        return false;
                    }
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                //boolStatus = true;
                //stringOverallMsg = "";
            }
            return true;
        }
        #endregion

        #region add/remove
        private bool ValidateCheckedRecords()
        {
            bool boolSelected = false;
            try
            {
                boolSelected = false;
                for (int intCount = 0; intCount < gvAvailableStaffs.Rows.Count; intCount++)
                {
                    CheckBox objCheckbox = (CheckBox)gvAvailableStaffs.Rows[intCount].FindControl("chkSelectLeft");
                    if (objCheckbox != null && objCheckbox.Checked)
                    {
                        boolSelected = true;
                        break;
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                 //boolSelected = false;
            }

            return boolSelected;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string stringstringuser_id = "";
            string stringstringuser_name = "";
            string stringbe_id = "";
            string[] stringIDs = null;
            string stringTooltip = "";
            DataTable objDataTableAdded = null;
            DataRow[] objDataRowfiltered = null;
            DataRow objDataRowNew = null;
            string stringExpression = "";
            try
            {
                if (ValidateCheckedRecords())
                {
                    for (int intCount = 0; intCount < gvAvailableStaffs.Rows.Count; intCount++)
                    {
                        CheckBox objCheckBox = (CheckBox)gvAvailableStaffs.Rows[intCount].FindControl("chkSelectLeft");
                        HiddenField objHiddenField = (HiddenField)gvAvailableStaffs.Rows[intCount].FindControl("hfIDsLeft");

                        if (objCheckBox != null && objCheckBox.Checked)
                        {
                            stringstringuser_id = "";
                            stringstringuser_name = ""; 
                            stringbe_id = ""; 
                            stringTooltip = objHiddenField.Value;

                            if (stringTooltip != null && stringTooltip.Trim().Length > 0)
                            {
                                stringIDs = stringTooltip.Split('~');
                                if (stringIDs != null && stringIDs.Length > 0)
                                {
                                    stringstringuser_id = stringIDs[0];
                                    stringstringuser_name = stringIDs[1];
                                    stringbe_id = stringIDs[2]; 

                                    if (ViewState["AddedProfiles"] != null)
                                    {
                                        objDataTableAdded = (DataTable)ViewState["AddedProfiles"];
                                        if (objDataTableAdded != null)
                                        {
                                            stringExpression = "user_id='" + stringstringuser_id + "'";
                                            objDataRowfiltered = objDataTableAdded.Select(stringExpression);
                                            if (objDataRowfiltered != null && objDataRowfiltered.Length > 0)
                                            { }
                                            else
                                            {
                                                objDataRowNew = objDataTableAdded.NewRow();
                                                objDataRowNew["user_id"] = stringstringuser_id;
                                                objDataRowNew["user_name"] = stringstringuser_name; 
                                                objDataRowNew["be_id"] = stringbe_id; 
                                                objDataRowNew["DML_INDICATOR"] = "I"; 

                                                objDataTableAdded.Rows.Add(objDataRowNew);

                                                ViewState["AddedProfiles"] = objDataTableAdded;
                                                gvAddedStaffs.DataSource = objDataTableAdded;
                                                gvAddedStaffs.DataBind();

                                                lblTotalRecordsright.InnerText = "0";
                                                if (objDataTableAdded != null && objDataTableAdded.Rows.Count > 0)
                                                {
                                                    lblTotalRecordsright.InnerText = objDataTableAdded.Rows.Count.ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this,"Please select at least one staff to add");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                stringstringuser_id = "";
                stringstringuser_name = "";
                stringbe_id = "";
                stringIDs = null;
                stringTooltip = "";
                objDataTableAdded = null;
                objDataRowfiltered = null;
                objDataRowNew = null;
                stringExpression = "";
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int intRemovedCount = 0;
            string stringstringuser_id = "";
            string stringstringuser_name = "";
            string stringbe_id = "";
            string stringExpression = "";
            string stringTooltip = "";
            string[] stringIDs = null;
            DataTable objDataTableAdded = null;
            DataRow[] objDataRowFiltered = null;
            DataTable objDataTableAddedProfile = null;
            DataRow[] objDatarow = null;
            try
            {

                for (int intCount = 0; intCount < gvAddedStaffs.Rows.Count; intCount++)
                {
                    CheckBox objCheckBox = (CheckBox)gvAddedStaffs.Rows[intCount].FindControl("chkSelectRight");
                    HiddenField objHiddenField = (HiddenField)gvAddedStaffs.Rows[intCount].FindControl("hfIDsRight");

                    if (objCheckBox != null && objCheckBox.Checked)
                    {
                        stringstringuser_id = "";
                        stringstringuser_name = "";
                        stringbe_id = "";

                        stringTooltip = objHiddenField.Value;

                        if (stringTooltip != null && stringTooltip.Trim().Length > 0)
                        {
                            stringIDs = stringTooltip.Split('~');
                            if (stringIDs != null && stringIDs.Length > 0)
                            {
                                stringstringuser_id = stringIDs[0];
                                stringstringuser_name = stringIDs[1];
                                stringbe_id = stringIDs[2];

                                if (ViewState["AddedProfiles"] != null)
                                {
                                    objDataTableAdded = (DataTable)ViewState["AddedProfiles"];
                                    if (objDataTableAdded != null)
                                    {
                                        stringExpression = "user_id='" + stringstringuser_id + "'";
                                        objDataRowFiltered = objDataTableAdded.Select(stringExpression);
                                        if (objDataRowFiltered != null && objDataRowFiltered.Length > 0)
                                        {
                                            if (objDataRowFiltered != null && objDataRowFiltered.Length > 0)
                                            {
                                                if (objDataRowFiltered[0]["DML_INDICATOR"].ToString() == "I" || (objDataRowFiltered[0]["DML_INDICATOR"].ToString() == "U" && objDataRowFiltered[0]["rec_no"].ToString().Length == 0))
                                                {
                                                    objDataTableAdded.Select(stringExpression)[0].Delete();
                                                }
                                                else
                                                {
                                                    objDataRowFiltered[0]["DML_INDICATOR"] = "D";
                                                }
                                            }
                                            objDataTableAdded.AcceptChanges();
                                            ViewState["AddedProfiles"] = objDataTableAdded;
                                            intRemovedCount++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (intRemovedCount > 0)
                {
                    objDataTableAddedProfile = (DataTable)ViewState["AddedProfiles"];
                    if (objDataTableAddedProfile != null)
                    {
                       objDatarow = objDataTableAddedProfile.Select("DML_INDICATOR<>'D'");
                    }
                     
                    if (objDatarow !=null && objDatarow.Length > 0)
                    {
                        objDataTableAddedProfile = objDataTableAddedProfile.Select("DML_INDICATOR<>'D'").CopyToDataTable<DataRow>();
                    } 
                    gvAddedStaffs.DataSource = objDataTableAddedProfile;
                    gvAddedStaffs.DataBind();

                    lblTotalRecordsright.InnerText = "0";
                    if (objDataTableAddedProfile != null && objDataTableAddedProfile.Rows.Count > 0)
                    {
                        lblTotalRecordsright.InnerText = objDataTableAddedProfile.Rows.Count.ToString();
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                intRemovedCount = 0;
                stringstringuser_id = "";
                stringstringuser_name = "";
                stringbe_id = "";
                stringExpression = "";
                stringTooltip = "";
                stringIDs = null;
                objDataTableAdded = null;
                objDataRowFiltered = null;
                objDataTableAddedProfile = null;
                objDatarow = null;
            }
        }
        #endregion
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
                CommonFunctions.HandleException(objException);
            }
        }

        #region save

        private bool ValidateControls()
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";
                if (txtGroupName.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- Group Name" + "\\r\\n";
                    boolStatus = false;
                }
                if (txtHRSequence.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- HR Sequence" + "\\r\\n";
                    boolStatus = false;
                }

                if (!boolStatus)
                {
                    if (stringOverallMsg.Trim().Length > 0)
                    {
                        stringOverallMsg = stringOverallMsg.Trim();
                        CommonFunctions.ShowMessage(stringOverallMsg);
                        return false;
                    }
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                //boolStatus = true;
                //stringOverallMsg = "";
            }
            return true;
        }
        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            string stringDMLindicator = "";
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            DataSet objDatasetResult1 = null;
            string[] stringOutputResult = null;
            string stringformid = "FDU0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue; 
            int intSuccessCount = 0;
            int intSuccesCountDetails = 0;
            bool boolStatus = true;
            bool boolrecexixts = true;
            string stringBOID = CommonFunctions.GETBussinessEntity();
            string stringGroupName = "";
            string stringServiceType = "";
            string stringexp = "";
            string stringGroupID = "";
            string stringHRseqno = "";
            string stringexp0123 = "";
            string stringServiceType1 = "";
            DataTable objDataTableAddedStaffs = null;
            string stringUserID = "";
            string stringUSerName = "";
            string stringRemarks = "";
            try
            {
                stringGroupName = txtGroupName.Text.Trim().Replace("  ", " ").ToUpper();
                stringServiceType = "DEFAULT";
                stringexp = "";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                if (ValidateControls())
                {
                    if (txtHRSequence.Text.Trim().Length > 0 && txtHRSequence.Text.Contains("-"))
                    {
                        stringGroupID = ""; 
                        stringHRseqno = txtHRSequence.Text.Trim().Replace("  ", " ").ToUpper();

                        if (hfGroupID.Value.Trim().Length == 0)
                        {
                            if (Session["FA0001R1V1_id"] == null)
                            {
                                stringexp0123 = "And AOF.be_id= '" + stringBOID + "' And AOF.grp_name= '" + stringGroupName.Trim().ToUpper() + "'";
                                stringServiceType1 = "List1R1V1";

                                objDatasetResult1 = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp0123, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                                if (interrorcount == 0)
                                {
                                    if (objDatasetResult1 != null && objDatasetResult1.Tables["t1"] != null && objDatasetResult1.Tables["t1"].Rows.Count > 0)
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
                                stringDMLindicator = "I";
                                stringGroupID = DateTime.Now.ToString("HHmmssffffff").ToString();
                                if (stringGroupID != null && stringGroupID.Trim().Length > 0)
                                {
                                    hfGroupID.Value = stringGroupID.Trim();
                                    if (SaveGroup(ref objDatasetResult, stringBOID, stringHRseqno, hfGroupID.Value, stringGroupName, stringDMLindicator))
                                    {
                                        boolStatus = true;
                                        intSuccessCount++;
                                    }
                                    else
                                    {
                                        boolStatus = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            boolStatus = false;
                            btnOk_Click(null, null);
                        }

                        if (boolStatus)
                        {
                            if (ViewState["AddedProfiles"] != null)
                            {
                                if (stringDMLindicator == "I")
                                {
                                    objDataTableAddedStaffs = (DataTable)ViewState["AddedProfiles"];
                                    if (objDataTableAddedStaffs != null && objDataTableAddedStaffs.Rows.Count > 0)
                                    {
                                        foreach (DataRow objDataRow in objDataTableAddedStaffs.Rows)
                                        {
                                            stringUserID = objDataRow["USER_ID"].ToString();
                                            stringUSerName = objDataRow["USER_NAME"].ToString();
                                            stringRemarks = "";

                                            if (!SaveGroupDetails(ref objDatasetResult, stringBOID, stringGroupID, stringGroupName, stringUserID, stringUSerName, stringRemarks,"I"))
                                            {
                                                boolStatus = false;
                                            }
                                            else
                                            {
                                                intSuccesCountDetails++;
                                            }
                                        }
                                    }
                                }
                            }

                            if (boolStatus && (intSuccessCount > 0 || intSuccesCountDetails > 0))
                            {
                                objDatasetResult = objDatasetResult.GetChanges();
                                stringServiceType = "OperationServiceDML";
                                objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                                if (intErrorCount == 0)
                                {
                                    CommonFunctions.ShowMessage("Group Created Successfully.");

                                    if (stringGroupID.Length > 0)
                                    {
                                        Session["GROUP_IDFDU0001"] = stringGroupID;
                                        Response.Redirect("FDU0001R1V1.aspx?TO=Y");
                                    }
                                    else
                                    {
                                        Session["GROUP_IDFDU0001"] = null;
                                    }
                                }
                                else
                                {
                                    Session["GROUP_IDFDU0001"] = null;
                                    Errorpopup(stringOutputResult);
                                }
                            }
                        }
                    }
                    else
                    {
                        CommonFunctions.ShowMessageboot(this, "Please enter valid HR Sequence");
                    }
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
                stringGroupName = "";
                stringServiceType = "";
                stringexp = "";
                stringGroupID = "";
                stringHRseqno = "";
                stringexp0123 = "";
                stringServiceType1 = "";
                objDataTableAddedStaffs = null;
                stringUserID = "";
                stringUSerName = "";
                stringRemarks = "";
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            string stringDMLindicator1 = "";
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FDU0001R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            int intSuccessCount = 0;
            int intSuccesCountDetails = 0;
            bool boolStatus = true;
            string stringBOID = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            string stringGroupID = "";
            string stringGroupName = "";
            string stringHRseqno = "";
            DataTable objDataTableAddedStaffs = null;
            string stringUserID = "";
            string stringUSerName = "";
            string stringDML_INDICATOR = "";
            string stringRemarks = "";
            DataRow[] objdatarowState = null;
            DataRow[] objdatarowState1 = null;
            DataRow[] objdatarowState2 = null;
            try
            {
                if (txtHRSequence.Text.Trim().Length > 0 && txtHRSequence.Text.Contains("-"))
                {
                    stringServiceType = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                    stringGroupID = hfGroupID.Value.Trim();
                    stringGroupName = txtGroupName.Text.Trim();
                    stringHRseqno = txtHRSequence.Text.Trim().Replace("  ", " ").ToUpper();

                    stringDMLindicator1 = "U";

                    if (SaveGroup(ref objDatasetResult, stringBOID, stringHRseqno, stringGroupID, stringGroupName, "U"))
                    {
                        boolStatus = true;
                        intSuccessCount++;
                    }
                    else
                    {
                        boolStatus = false;
                    }

                    if (boolStatus)
                    {
                        if (ViewState["AddedProfiles"] != null)
                        {
                            if (stringDMLindicator1 == "U")
                            {

                                objDataTableAddedStaffs = (DataTable)ViewState["AddedProfiles"];

                                if (objDataTableAddedStaffs != null && objDataTableAddedStaffs.Rows.Count > 0)
                                {
                                    foreach (DataRow objDataRow in objDataTableAddedStaffs.Rows)
                                    {

                                        stringUserID = objDataRow["USER_ID"].ToString();
                                        stringUSerName = objDataRow["USER_NAME"].ToString();
                                        stringDML_INDICATOR = objDataRow["DML_INDICATOR"].ToString();
                                        stringRemarks = "";

                                        if (!SaveGroupDetails(ref objDatasetResult, stringBOID, stringGroupID, stringGroupName, stringUserID, stringUSerName, stringRemarks, stringDML_INDICATOR))
                                        {
                                            boolStatus = false;
                                        }
                                        else
                                        {

                                            intSuccesCountDetails++;
                                            boolStatus = true;
                                        }

                                    }
                                }
                            }

                        }

                        if (boolStatus && (intSuccessCount > 0 || intSuccesCountDetails > 0))
                        {
                            objDatasetResult.AcceptChanges();
                            if (objDatasetResult != null)
                            {
                                for (int intIndex = 0; intIndex < objDatasetResult.Tables.Count; intIndex++)
                                {
                                    objdatarowState = objDatasetResult.Tables[intIndex].Select("DML_INDICATOR = 'D'");
                                    if (objdatarowState != null && objdatarowState.Length > 0)
                                    {
                                        for (int intIndex1 = 0; intIndex1 < objdatarowState.Length; intIndex1++)
                                        {
                                            objdatarowState[intIndex1].Delete();
                                        }
                                    }

                                    objdatarowState1 = objDatasetResult.Tables[intIndex].Select("DML_INDICATOR = 'U'");
                                    if (objdatarowState1 != null && objdatarowState1.Length > 0)
                                    {
                                        for (int intIndex2 = 0; intIndex2 < objdatarowState1.Length; intIndex2++)
                                        {
                                            objdatarowState1[intIndex2].SetModified();
                                        }
                                    }
                                    objdatarowState2 = objDatasetResult.Tables[intIndex].Select("DML_INDICATOR = 'I'");
                                    if (objdatarowState2 != null && objdatarowState2.Length > 0)
                                    {
                                        for (int intIndex3 = 0; intIndex3 < objdatarowState2.Length; intIndex3++)
                                        {

                                            objdatarowState2[intIndex3].SetAdded();
                                        }
                                    }
                                }
                            }
                           
                            objDatasetResult = objDatasetResult.GetChanges();
                            stringServiceType = "OperationServiceDML";
                            objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                            if (intErrorCount == 0)
                            {
                                if (stringGroupID.Length > 0)
                                {
                                    Session["GROUP_IDFDU0001"] = stringGroupID;
                                    Response.Redirect("FDU0001R1V1.aspx?TO=Y");
                                }
                                else
                                {
                                    Session["GROUP_IDFDU0001"] = null;
                                }

                                CommonFunctions.ShowMessageboot(this, "Group Updated Successfully");
                            }
                            else
                            {
                                Session["GROUP_IDFDU0001"] = null;
                                Errorpopup(stringOutputResult);
                            }
                        }
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, "Please enter valid HR Sequence");
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
                stringBOID = "";
                stringServiceType = "";
                stringexp = "";
                stringGroupID = "";
                stringGroupName = "";
                stringHRseqno = "";
                objDataTableAddedStaffs = null;
                stringUserID = "";
                stringUSerName = "";
                stringDML_INDICATOR = "";
                stringRemarks = "";
                objdatarowState = null;
                objdatarowState1 = null;
                objdatarowState2 = null;
            }
        }
        private bool SaveGroup(ref DataSet objDatasetResult, string stringBOID, string stringHRseqno, string stringGroupID, string stringGroupName, string stringDMLIndicator)
        {
            DataRow objdatarow = null;
            string[] stringValues = null;
            string stringHRNId = "";
            string stringHRNId01 = "";
            string stringID = "";
            try
            {
                if (objDatasetResult != null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                {
                    objdatarow = objDatasetResult.Tables["t1"].NewRow();
                    objdatarow["BE_ID"] = stringBOID;
                    objdatarow["GRP_ID"] = stringGroupID.Trim();
                    objdatarow["GRP_NAME"] = stringGroupName.Trim();

                    if (stringHRseqno.Length > 0 && stringHRseqno.Contains("-"))
                    {
                        stringValues = stringHRseqno.Split('-');
                        if (stringValues != null && stringValues.Length > 1)
                        {
                            stringHRNId = stringValues[0];
                            stringHRNId01 = stringValues[1];

                            objdatarow["START_VALUE"] = stringHRNId;
                            objdatarow["END_VALUE"] = stringHRNId01;
                        }
                    }
                     
                    stringID = DateTime.Now.ToString("HHmmssffffff").ToString();
                    objdatarow["reference_9"] = stringID;
                    Session["UNIQID"] = stringID;

                    CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                    objDatasetResult.Tables["t1"].Rows.Add(objdatarow);

                    if (stringDMLIndicator == "U")
                    {
                        objDatasetResult.Tables["t1"].AcceptChanges();
                        objDatasetResult.Tables["t1"].Rows[0]["reference_9"] = stringID;
                        objDatasetResult.Tables["t1"].Rows[0]["DML_INDICATOR"] = "U";
                        Session["UNIQID"] = stringID;
                    }
                    objDatasetResult.Tables["t1"].Rows[0].RowState.ToString();
                    return true;
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
                objdatarow = null;
                stringValues = null;
                stringHRNId = "";
                stringHRNId01 = "";
                stringID = "";
            }
        }

        private bool SaveGroupDetails(ref DataSet objDatasetResult, string stringBOID, string stringGroupID, string stringGroupName, string stringUserID, string stringUSerName, string stringRemarks,string stringDML_INDICATOR)
        {
            string stringID2 = "";
            DataRow objdatarow = null;
            try
            {

                if (objDatasetResult != null)
                {
                    objdatarow = objDatasetResult.Tables["t2"].NewRow();
                    objdatarow["BE_ID"] = stringBOID;
                    objdatarow["GRP_ID"] = stringGroupID.Trim();
                    objdatarow["USER_ID"] = stringUserID.Trim();
                    objdatarow["USER_NAME"] = stringUSerName.Trim();
                    objdatarow["Remarks"] = stringRemarks;
                    objdatarow["DML_INDICATOR"] = stringDML_INDICATOR;
                    if (Session["UNIQID"] != null)
                    {
                        stringID2 = Session["UNIQID"].ToString();
                    }
                    objdatarow["reference_9"] = stringID2.Trim();
                    CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                    objDatasetResult.Tables["t2"].Rows.Add(objdatarow);


                    return true;
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
                stringID2 = "";
                objdatarow = null;
            }
        }


        #endregion

    }
}