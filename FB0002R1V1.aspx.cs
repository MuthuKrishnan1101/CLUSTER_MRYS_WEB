using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FB0002R1V1 : System.Web.UI.Page
    {

        public int intpageIndex = 0;
        public int intrecFrom = 0;
        public int intrecTo = 0;
        public string stringformIdPaging = "MRPaymentViewPopupPaging";
        public DataSet objDatasetAppsVariables;
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
                        CommonFunctions.HeaderName(this, "FB0002R1V1");
                        ViewState["vsSortDirection"] = " ASC";
                        ViewState["vsSortExpression"] = "";
                        imgbtnClear_Click(null, null);
                        LoadPaidLocation(); 
                        pnlgrid.Visible = false;
                        lblTotalRecords.InnerText = "0";
                        gvUserHistory.DataSource = null;
                        gvUserHistory.DataBind();

                    }

                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }

            }

        }
        private void LoadPaidLocation()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0016R1V1";
            string stringOrderBy = "palon.short_name asc";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objdatatableLoadPaidAt = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                ddlPaidAt.Items.Clear();
                stringcondition = "And palon.be_id= '" + stringbeid + "'  AND palon.delmark='N' ";
                stringServiceType = "List1R1V1"; 

                if (Session["SSNLOADPAIDAT"] != null)
                {
                    objdatatableLoadPaidAt = (DataTable)Session["SSNLOADPAIDAT"];
                }
                if ((objdatatableLoadPaidAt == null) || (objdatatableLoadPaidAt != null && objdatatableLoadPaidAt.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadPaidAt = objDatasetResult.Tables["t1"];
                            Session["SSNLOADPAIDAT"] = objdatatableLoadPaidAt;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                if (objdatatableLoadPaidAt != null && objdatatableLoadPaidAt.Rows.Count > 0)
                { 
                    ddlPaidAt.DataTextField = "short_name";
                    ddlPaidAt.DataValueField = "loc_id";
                    ddlPaidAt.DataSource = objdatatableLoadPaidAt;
                    ddlPaidAt.DataBind();
                    ddlPaidAt.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlPaidAt.DataSource = null;
                    ddlPaidAt.DataBind();
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
                objdatatableLoadPaidAt = null;
                stringbeid = null;
                stringcondition = null;
                stringServiceType = null;
            }

        }
        private void LoadRecord(string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {
            DataSet objDataSet = null;
            int intRecordCount = 0;
            DataTable objDataTable = null;
            try
            {

                ViewState["vsSearchCondition"] = Condition;
                ViewState["vsSortExpression"] = SortExpression;
                
                objDataSet = GetRecords(out intRecordCount, Condition, SortExpression, RecordFrom, RecordTo);


                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables["t2"] != null && objDataSet.Tables["t2"].Rows.Count > 0)
                {
                    objDataTable = objDataSet.Tables["t2"]; 

                    pnlgrid.Visible = true;
                    lblTotalRecords.InnerText = intRecordCount.ToString();
                    gvUserHistory.DataSource = objDataTable;
                    gvUserHistory.DataBind();
                    Session["excelprofile"] = objDataTable;


                }
                else
                {
                    gvUserHistory.DataSource = objDataSet;
                    gvUserHistory.DataBind();
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDataSet = null;
                intRecordCount = 0;
                objDataTable = null;
            }
        }
        public DataSet GetRecords(out int intRecordCount, string Condition = null, string SortExpression = null, int? RecordFrom = null, int? RecordTo = null)
        {

            DataSet objDatasetResult = null;
            DataSet objDatasetAppsVariables01 = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC3001R1V1";
            intRecordCount = 0;
            string[] stringInputs = new string[2];
            int intRecordFrom = 0;
            int intRecordTo = 0;
            string stringmessage = "";
            try
            {
                stringInputs[0] = txtExtNo.Text.ToString();
                stringInputs[1] = txtChequeNo.Text.ToString();
                intRecordFrom = intrecFrom;
                intRecordTo = intrecTo;
                ViewState["vsSearchCondition"] = Condition;

                objDatasetAppsVariables01 = (DataSet)Session["objDatasetlocaldeclaration"];
                objDatasetAppsVariables01.Tables[0].Rows[0]["FORM_ID"] = stringformid;

                objDatasetResult = CommonFunctions.List32R1V1("List32R1V1", stringInputs, stringformid, Condition, SortExpression, intRecordFrom, intRecordTo, out intRecordCount, out interrorcount, out stringOutputResult);
                 
                PopulatePager(intRecordCount, intpageIndex);

                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"] != null && objDatasetResult.Tables["t2"].Rows.Count > 0)
                    {
                        return (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t2"].Rows.Count > 0) ? objDatasetResult : null;
                    }
                    else
                    {
                        pnlgrid.Visible = false; 
                        stringmessage = "No Records Found ";
                        CommonFunctions.ShowMessageboot(this, stringmessage);
                        lblTotalRecords.InnerText = intRecordCount.ToString();
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
                objDatasetAppsVariables01 = null;
                interrorcount = 0;
                stringOutputResult = null;
                stringformid = null;
                stringInputs = null;
                intRecordFrom = 0;
                intRecordTo = 0;
                stringmessage = null;
            }
            return null;
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
                stringComponent[0] = "FB0002R1V1";
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
                stringComponent[0] = "FC0003R1V1";
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
                stringstatus = null;
                stringOutputResult = null;
                stringComponent = null;
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
                        LoadRecord((string)ViewState["vsSearchCondition"], ViewState["vsSortExpression"].ToString(), 0);
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
                stringColumnName = null;
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

        private string PrepareSearchExpression()//fix
        {
            string stringExpression = null;
            string stringInput = "";
            string stringEncrypyValue = "";
            try
            {

                if (ddlPaidAt.SelectedItem != null && ddlPaidAt.SelectedValue.Length > 0)
                {
                    stringExpression += ddlPaidAt.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Payrcpt.Paid_At)  LIKE UPPER('%" + ddlPaidAt.SelectedValue.Trim() + "%'))" : "";
                }

                if (txtName.Text.Length > 0 && txtName.Text.Trim() != "%")
                {
                    stringExpression += txtName.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Payrcpt.Payee_Name)  LIKE UPPER('%" + txtName.Text.Trim() + "%'))" : "";
                }

                stringInput = txtHRN.Text.Trim();
                stringEncrypyValue ="";
                if (stringInput.Length > 0)
                {
                    stringEncrypyValue = CommonFunctions.HRNtoEncrypyValue(stringInput);
                }
                if (stringEncrypyValue.Length > 0)
                {
                    stringExpression += "And pat.hrn_id='" + stringEncrypyValue.Trim() + "'";
                } 
                if (txtChequeNo.Text.Length > 0 && txtChequeNo.Text.Trim() != "%")
                {
                    stringExpression += txtChequeNo.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(PayRcpt.Cheque_No)  LIKE UPPER('%" + txtChequeNo.Text.Trim() + "%'))" : "";
                }
                if (txtExtNo.Text.Length > 0 && txtExtNo.Text.Trim() != "%")
                {
                    stringExpression += txtExtNo.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(PayRcpt.External_Receipt_id)  LIKE UPPER('%" + txtExtNo.Text.Trim() + "%'))" : "";
                }
                if (txtRequestNo.Text.Length > 0 && txtRequestNo.Text.Trim() != "%")
                {

                    stringExpression += txtRequestNo.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Pay.Request_ID)  LIKE UPPER('%" + txtRequestNo.Text.Trim() + "%'))" : "";
                }
                if (txtReceipt.Text.Length > 0 && txtReceipt.Text.Trim() != "%")
                { 
                    stringExpression += txtReceipt.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(Payrcpt.Receipt_ID)  LIKE UPPER('%" + txtReceipt.Text.Trim() + "%'))" : "";
                }
                if (txtDateFrm.Text.Trim().Length > 0 && txtDateTo.Text.Trim().Length > 0)
                {
                    DateTime objDateTo = CommonFunctions.ConvertToDateTime(txtDateTo.Text.Trim(), "dd-MM-yyyy");

                    DateTime objDateFrom = CommonFunctions.ConvertToDateTime(txtDateFrm.Text.Trim(), "dd-MM-yyyy");
                    stringExpression += "and CONVERT(date,PayRcpt.Rcvd_Date)  between  CONVERT(date,'" + Convert.ToDateTime(objDateFrom).ToString("yyyy-MM-dd") + "') and CONVERT(date,'" + Convert.ToDateTime(objDateTo).ToString("yyyy-MM-dd") + "')";

                }


                return stringExpression;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return null;
            } 
        }

        private void ShowSpecialInfo()
        {
            string stringBoID = "";
            string stringHRNID = "";
            string stringSpecialInfo = "";
            try
            {
                if (Session["BusinessID"] != null)
                {
                    stringBoID = Session["BusinessID"].ToString();
                }
                if (txtHRN.Text.Trim().Length > 0)
                {
                    stringHRNID = txtHRN.Text.Trim().ToUpper();
                    stringSpecialInfo = CommonFunctions.GetSpecialInfo(stringBoID, stringHRNID, true);
                    if (stringSpecialInfo != null && stringSpecialInfo.Trim().Length > 0) { CommonFunctions.ShowMessageboot(this, stringSpecialInfo); }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                stringBoID = null;
                stringHRNID = null;
                stringSpecialInfo = null;
            }
        }
        private bool ValidateBusinessLogic()
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "";

                if (txtDateFrm.Text.Trim().Length > 0 && !CommonFunctions.ValidDateTime(txtDateFrm.Text.Trim()))
                {
                    stringOverallMsg += "\"Date From\" should be a valid date.";
                    boolStatus = false;
                }

                if (txtDateTo.Text.Trim().Length > 0 && !CommonFunctions.ValidDateTime(txtDateTo.Text.Trim()))
                {
                    stringOverallMsg += "\"Date To\" should be a valid date.";
                    boolStatus = false;
                }
                if (txtDateFrm.Text.Trim().Length > 0 && txtDateTo.Text.Trim().Length == 0  )
                {
                    stringOverallMsg += "\"Date To\" should not be empty";
                    boolStatus = false;
                }
                if (txtDateTo.Text.Trim().Length > 0 && txtDateFrm.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "\"Date From\" should not be empty";
                    boolStatus = false;
                }
                if (boolStatus)
                {
                    DateTime objDateTimeFrom = DateTime.Now;
                    DateTime objDateTimeTo = DateTime.Now;


                    if (txtDateFrm.Text.Trim().Length > 0 && txtDateTo.Text.Trim().Length > 0)
                    {
                        if (!CommonFunctions.IsValidDateTime(txtDateFrm.Text.Trim(), "dd/MM/yyyy HH:mm:ss", out objDateTimeFrom))
                        {
                            boolStatus = false;
                            CommonFunctions.ShowMessageboot(this, "\"Date From\" is a Invalid date.");
                            txtDateFrm.Focus();
                            return boolStatus;
                        }
                        else if (!CommonFunctions.IsValidDateTime(txtDateTo.Text.Trim(), "dd/MM/yyyy HH:mm:ss", out objDateTimeTo))
                        {
                            boolStatus = false;
                            CommonFunctions.ShowMessageboot(this, "\"Date To\" is a Invalid date.");
                            txtDateTo.Focus();
                            return boolStatus;
                        }
                        else
                        {
                            TimeSpan objTimeSpan = objDateTimeTo.Subtract(objDateTimeFrom);
                            if (objTimeSpan.TotalDays < 0)
                            {
                                boolStatus = false;
                                CommonFunctions.ShowMessageboot(this, "\"Date To\" should be greater than Request \"Date From\".");
                                return boolStatus;
                            }
                            else if (objTimeSpan.TotalDays > 90)
                            {
                                boolStatus = false;
                                CommonFunctions.ShowMessageboot(this, "Date range should be less than 90 days");
                                return boolStatus;
                            }
                        }

                    }

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
                //boolStatus = true;
                //stringOverallMsg = "";
            }

        }
        private bool CheckEmptyFields()
        {
            if (txtRequestNo.Text.Trim().Length > 0) { return false; }
            else if (txtHRN.Text.Trim().Length > 0) { return false; }
            else if (txtName.Text.Trim().Length > 0) { return false; }
            else if (txtReceipt.Text.Trim().Length > 0) { return false; }
            else if (ddlPaidAt.Text.Trim().Length > 0) { return false; }
            else if (txtDateFrm.Text.Trim().Length > 0) { return false; }
            else if (txtDateTo.Text.Trim().Length > 0) { return false; }
            else if (txtExtNo.Text.Trim().Length > 0) { return false; }
            else if (txtChequeNo.Text.Trim().Length > 0) { return false; }
            return true;
        }


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


                imgbtnSearch_Click(null, null);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        protected void lnkbtnReceiptNo_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringReciptid = "";
            string stringreqno = "";
            try
            {
                if (sender != null)
                {
                    stringCmdArgument = ((LinkButton)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length >= 2)
                        {
                            stringReciptid = stringValues[0];
                            stringreqno = stringValues[1];

                            if (stringReciptid.Length > 0 && stringreqno.Length > 0)
                            {
                                Session["REQUESTID_PAYMENT"] = stringreqno;
                                Response.Redirect("FC0003R1V1.aspx?ReceiptID=" + stringReciptid);
                            }
                            else
                            {
                                Session["REQUESTID_PAYMENT"] = null;
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
                stringValues = null;
                stringReciptid = null;
                stringreqno = null;
            }
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


                return false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                //boolMROHRN = false;
                //boolStatus = true;
                //stringInput = "";
                //stringResult = "";
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
                CommonFunctions.HandleException(objException);
            }

            return stringHRN;
        }
        #endregion


        protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtRequestNo.Text = "";
                txtReceipt.Text = "";
                txtReceipt.Text = "";
                txtHRN.Text = "";
                txtName.Text = "";
                txtDateFrm.Text = "";
                txtDateTo.Text = "";
                txtExtNo.Text = "";
                txtChequeNo.Text = "";
                ddlPaidAt.ClearSelection();

                Session["stringDMLIndicator"] = "I";
                txtRequestNo.Focus();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {

            bool boolstatus = true;
            gvUserHistory.DataSource = null;
            gvUserHistory.DataBind();
            PopulatePager(0, intpageIndex);
            pnlgrid.Visible = true;
            lblTotalRecords.InnerText = "0";
            Session["excelprofile"] = null; 
            try
            {

                if (!CheckEmptyFields())
                {
                    if (txtHRN.Text.Trim().Length > 0 && !DoNonCGHHrnValidation())
                    { boolstatus = false; }
                    
                    if (boolstatus)
                    {
                        if (ValidateBusinessLogic())
                        {
                            ShowSpecialInfo(); 
                            LoadRecord(PrepareSearchExpression(), "PayRcpt.Rcvd_Date asc");
                        }
                    }
                }
                else
                { CommonFunctions.ShowMessageboot(this, "Please give at least one search criteria"); }
                txtRequestNo.Focus();
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
                imgbtnSearch_Click(null, null);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtRequestNo.Text = "";
                txtReceipt.Text = "";
                txtReceipt.Text = "";
                txtHRN.Text = "";
                txtName.Text = "";
                txtDateFrm.Text = "";
                txtDateTo.Text = "";
                txtExtNo.Text = "";
                txtChequeNo.Text = "";
                ddlPaidAt.ClearSelection();

                Session["stringDMLIndicator"] = "I";
                txtRequestNo.Focus();

                pnlgrid.Visible = false;
                lblTotalRecords.InnerText = "0";
                gvUserHistory.DataSource = null;
                gvUserHistory.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void gvUserHistory_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    LinkButton lnkbtnReceiptNo = (LinkButton)e.Row.FindControl("lnkbtnReceiptNo");

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