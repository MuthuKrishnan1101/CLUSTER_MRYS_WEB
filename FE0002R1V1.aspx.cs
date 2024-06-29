using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FE0002R1V1 : System.Web.UI.Page
    {
        public int intpageIndexgvlist = 0;
        public int intrecFromgvlist = 0;
        public int intrecTogvlist = 0;
        public string stringformIdPaginggvlist = "FE002R1V1popupPaging";
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

                    intrecTogvlist = CommonFunctions.GridViewPagesize(stringformIdPaginggvlist);

                    if (!IsPostBack)
                    {
                        VerifyAccessRights();  
                        CommonFunctions.HeaderName(this, "FE0002R1V1");
                        LoadEnquiryStatus();
                        ClearValues();
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
            imgbtnprint.Enabled = false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FE0002R1V1";
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
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)//fix
        {
            bool boolstatus = true;
            try
            {
                if (!DoEmptyValidation())
                {
                    if (txtHRN.Text.Trim().Length > 0 && !DoNonCGHHrnValidation())
                    {
                        boolstatus = false;
                    }
                    if (boolstatus)
                    {
                        SearchRecords();
                        txtHRN.Focus();
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, "Please give at least one search criteria");
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                boolstatus = true;
            }
        }

        protected void imgBtnNew_Click(object sender, ImageClickEventArgs e)//fix
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


        private string PrepareSearchExpression()
        {
            string stringExpression = null;
            string stringtodate = "";
            string stringInput = "";
            string stringfromdate = "";
            string stringEncrypyValue = "";
            try
            {
                 
                if (ddlEnqStatus.SelectedValue.Length > 0 && ddlEnqStatus.SelectedValue.Trim() != "%")
                {
                    stringExpression += ddlEnqStatus.SelectedValue.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(mrenq.REFERENCE_1)  LIKE UPPER('%" + ddlEnqStatus.SelectedValue.Trim() + "%'))" : "";
                }
                if (txtRecTypeeID.Text.Length > 0 && txtRecTypeeID.Text.Trim() != "%")
                {
                    stringExpression += "And mrreg.rpttyp_id='" + txtRecTypeeID.Text.Trim().ToUpper() + "'";
                }
                if (txtFrmDate.Text.Trim().Length > 0)
                {
                    DateTime objDateFrom = CommonFunctions.ConvertToDateTime(txtFrmDate.Text.Trim(), "dd-MM-yyyy");
                    stringfromdate = objDateFrom.ToString("MM-dd-yyyy");
                     stringExpression += "AND CONVERT(date,mrenq.enq_date)  >= CONVERT(date,'" + stringfromdate + "') ";
                }
                if (txtToDate.Text.Trim().Length > 0)
                {
                    DateTime objDateTo = CommonFunctions.ConvertToDateTime(txtToDate.Text.Trim(), "dd-MM-yyyy");
                     stringtodate = objDateTo.ToString("MM-dd-yyyy");
                     stringExpression += "AND CONVERT(date,mrenq.enq_date)  <= CONVERT(date,'" + stringtodate + "') ";
                }
                stringInput = txtHRN.Text.Trim();
                stringEncrypyValue = "";
                if (stringInput.Length > 0)
                {
                    stringEncrypyValue = CommonFunctions.HRNtoEncrypyValue(stringInput);
                }
                if (stringEncrypyValue.Length > 0)
                {
                    stringExpression += "And mrpats.hrn_id='" + stringEncrypyValue.Trim() + "'";
                } 
                if (txtPatName.Text.Length > 0 && txtPatName.Text.Trim() != "%")
                { 
                    stringExpression += txtPatName.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER( inref.INDEX_VALUE)  LIKE UPPER('%" + txtPatName.Text.Trim().Replace("'", "''") + "%'))" : "";

                }



                ViewState["exportconditiondesig"] = stringExpression;
                return stringExpression;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return null;
            }
            finally
            {
                stringExpression = null; 
            }
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
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private void SearchRecords()
        {
            int intTotalRecord = 0;
            DataSet objDataSet = null;
            int interrorcount = 0;
            string[] stringOutputResult = null;
            string stringformid = "FE0003R1V1";  
            string stringOrderBy = "mrenq.created_on desc";
            string stringServiceType = "List1R1V1";
            string stringexp = "";
            int intRecordFrom = 0;
            int intRecordTo = int.MaxValue;
            try
            {
                if (ValidateControls() && ValidateBusinessLogic())
                {
                   
                    intRecordFrom = intrecFromgvlist;
                    intRecordTo = intrecTogvlist;

                    stringexp = PrepareSearchExpression();
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
                            pnlresultgrid.Visible = true;
                            lblTotalRecords.InnerText = objDataSet.Tables[0].Rows.Count.ToString();
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this,"No Records Found");
                            Session["SortTable"] = null;
                            gvList.DataSource = null;
                            gvList.DataBind();
                            pnlresultgrid.Visible = false;
                            lblTotalRecords.InnerText = "0";
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
                stringexp = "";
                //intRecordFrom = 0;
                //intRecordTo = 0;
            }
        }
         
        private void LoadEnquiryStatus()//fix
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
            DataTable objdatatableLoadEnquiry = null;
            string stringLstRefGroupID = "";
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                ddlEnqStatus.Items.Clear();

                stringLstRefGroupID = "MRENQUIRY_STATUS";
                stringcondition = "And lst.be_id= '" + stringbeid + "'  AND  lst.LSTGRP_ID='" + stringLstRefGroupID + "'AND lst.delmark='N' ";

                stringServiceType = "List1R1V1";
                if (Session["SSNLOADENQUIRY"] != null)
                {
                    objdatatableLoadEnquiry = (DataTable)Session["SSNLOADENQUIRY"];
                }
                if ((objdatatableLoadEnquiry == null) || (objdatatableLoadEnquiry != null && objdatatableLoadEnquiry.Rows.Count == 0))
                {
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdatatableLoadEnquiry = objDatasetResult.Tables["t1"];
                            Session["SSNLOADENQUIRY"] = objdatatableLoadEnquiry;
                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }

                if (objdatatableLoadEnquiry != null && objdatatableLoadEnquiry.Rows.Count > 0)
                {
                    ddlEnqStatus.DataTextField = "SHORT_NAME";
                    ddlEnqStatus.DataValueField = "LST_ID";
                    ddlEnqStatus.DataSource = objdatatableLoadEnquiry;
                    ddlEnqStatus.DataBind();
                    ddlEnqStatus.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    ddlEnqStatus.DataSource = null;
                    ddlEnqStatus.DataBind();
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
                stringbeid = "";
                objdatatableLoadEnquiry = null;
                stringLstRefGroupID = "";
                stringcondition = "";
                stringServiceType = "";
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

                if (txtFrmDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length == 0)
                {
                    stringOverallMsg += "- To Date" + "\\r\\n";
                    boolStatus = false;
                }

                if (txtToDate.Text.Trim().Length > 0 && txtFrmDate.Text.Trim().Length == 0)
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
                //boolStatus = true;
                //stringOverallMsg = "";
            }
        }

        private bool ValidateBusinessLogic()//fix
        {
            double doubleTotalDays = 0;
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
                else if (txtToDate.Text.Trim().Length > 0 && txtFrmDate.Text.Trim().Length > 0)
                {
                    doubleTotalDays = (CommonFunctions.ConvertToDateTime(txtFrmDate.Text.Trim(), "dd-MM-yyyy") - CommonFunctions.ConvertToDateTime(txtFrmDate.Text.Trim(), "dd-MM-yyyy")).TotalDays;

                    if (doubleTotalDays < 0)
                    {
                        stringOverallMsg += "From date must be less than to date.";
                        boolStatus = false;
                    }
                }

                if (!boolStatus)
                {
                    if (stringOverallMsg.Trim().Length > 0)
                    {
                        stringOverallMsg = stringOverallMsg.Trim() + " ";
                        stringOverallMsg = stringOverallMsg.Remove(stringOverallMsg.Length - 1, 1);
                        CommonFunctions.ShowMessageboot(this,stringOverallMsg);
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
                doubleTotalDays = 0;
                //boolStatus = true;
                //stringOverallMsg = "";
            }

        }

        private void ClearValues()//fix
        {
            try
            {
                txtHRN.Text = "";
                txtPatName.Text = "";
                txtFrmDate.Text = "";
                txtToDate.Text = "";
                txtRecTypeename.Text = "";
                txtRecTypeeID.Text = "";
                ddlEnqStatus.ClearSelection();
                Session["stringDMLIndicator"] = "I";
                txtHRN.Focus();
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
          
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
            finally
            {
                stringSub1 = "";
                stringSub2 = "";
                stringResult = "";
            }
            return stringHRN;
        }

        private bool DoEmptyValidation()//fix
        {
            try
            {

                if (txtHRN.Text.Trim().Length > 0) { return false; }
                if (txtPatName.Text.Trim().Length > 0) { return false; }
                if (txtFrmDate.Text.Trim().Length > 0) { return false; }
                if (txtToDate.Text.Trim().Length > 0) { return false; }
                if (txtRecTypeeID.Text.Trim().Length > 0) { return false; }
                if (ddlEnqStatus.Text.Trim().Length > 0) { return false; }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
            return true;
        }

         #region validate HRN

        public bool DoNonCGHHrnValidation()
        {
            bool boolMROHRN = false;
            bool boolStatus = true;
            string stringResult = "";
            string stringInput = "";
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
                CommonFunctions.HandleException(objException);
            }
            finally
            {
                //boolMROHRN = false;
                //boolStatus = true;
                //stringResult = "";
                //stringInput = "";
            }

            return false;
        }

        #endregion

        protected void lnkPagegvList_Click(object sender, EventArgs e)
        {
            int intrecFrom1 = 0;
            try
            {
                if (Session["intpageIndex1"] != null)
                {
                    intpageIndexgvlist = Convert.ToInt32(Session["intpageIndex1"].ToString());
                    Session["intpageIndex"] = intpageIndexgvlist;
                }
                else
                {
                    intpageIndexgvlist = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndexgvlist != 0)
                    {
                        Session["intpageIndex"] = intpageIndexgvlist;
                    }
                }

                if (intpageIndexgvlist == 1)
                {
                    intrecFromgvlist = 0;
                    intrecTogvlist = CommonFunctions.GridViewPagesize(stringformIdPaginggvlist);
                }
                else
                {
                    intrecFrom1 = (intpageIndexgvlist * intrecTogvlist) - intrecTogvlist;
                    intrecFromgvlist = intrecFrom1 + 1;
                    intrecTogvlist = intrecFrom1 + CommonFunctions.GridViewPagesize(stringformIdPaginggvlist);
                }
                SearchRecords();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                //intrecFrom1 = 0;
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

        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DoEmptyValidation())
                {
                    if (txtHRN.Text.Trim().Length > 0 && DoNonCGHHrnValidation())
                    { }
                    SearchRecords();
                    txtHRN.Focus();
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, "Please give at least one search criteria");
                }
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
                ClearValues();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
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
                //buttonId = "";
                //ToolTip = "";
                //stringToolTip = "";
                //ID = "";
                //Name = "";
                //stringValues = null;
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
                            if (hdnPopupDropdownValue != null && hdnPopupDropdownValue.Value.Length > 0)//&& hdnPopupDropdownValue.Value == "DOCTORINSTITUTION"
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
                //stringCmdArgument = "";
                //stringId = "";
                //stringNAME = "";
                //txtID = "";
                //txtnameID = "";
                //updatepnlID = "";
                //stringValues = null;
            }
        }
        protected void btnclosOrganisationpopup_Click(object sender, EventArgs e)
        {
            try
            {
                mdlpnlddlpopup.Hide();
                Panel3.Visible = false;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
          

        }

        private void PopulatePagerdropdownpopup(int recordCount, int currentPage, int intrecTo)
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
                                    else if (ddlID == "DEPTID")
                                    {
                                        lblpopupname.Text = "Department ID";
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
                //ddlID = "";
                //txtID = "";
                //txtNAmeID = "";
                //updatepnlID = "";
                //stringValues = null;
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
            int intRecordTo = int.MaxValue;
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
                        stringOrderBy = "mrrets.SHORT_NAME asc";

                        stringexp01 += "And mrrets.be_id= '" + stringbeid + "'  AND mrrets.delmark='N' AND mrrets.Reference_no_2 is not null";
                       
                    }
                    else if (hdnPopupDropdownValue.Value == "DEPTID")
                    {
                        stringformid = "FA0010R1V1";
                        stringOrderBy = "MRDEP.short_name asc";
                        stringexp01 += "And MRDEP.be_id= '" + stringbeid + "'And MRDEP.delmark= 'N'";

                    }
                    else if (hdnPopupDropdownValue.Value == "DOCTOR")
                    {
                        stringformid = "FA0011R1V1";
                        stringOrderBy = "mrd.short_name asc";
                        //stringexp01 += "And mrd.be_id= '" + stringbeid + "' And mrd.delmark= 'N'";
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
                //intRecordFrom = 0;
                //intRecordTo = int.MaxValue;
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
                    else if (hdnPopupDropdownValue.Value == "DEPTID")
                    {
                        stringColumn1 = "MRDEP.DEPT_ID";
                        stringColumn2 = "MRDEP.SHORT_NAME";
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
                        else if (hdnPopupDropdownValue.Value == "DEPTID")
                        {
                            stringID = objDataRow["DEPT_ID"].ToString();
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
                //stringSort = string.Empty;
                //objDRV = null;
                //objDataRow = null;
            }
        }

        protected void lnkPagedropdownpopup_Click(object sender, EventArgs e)
        {
            int intrecFromProcessHistory1 = 0;
            try
            {
                if (Session["intpageIndex1"] != null)
                {
                    intpageIndexdropdownpopup = Convert.ToInt32(Session["intpageIndex1"].ToString());
                    Session["intpageIndex"] = intpageIndexdropdownpopup;
                }
                else
                {
                    intpageIndexdropdownpopup = int.Parse((sender as LinkButton).CommandArgument);
                    if (intpageIndexdropdownpopup != 0)
                    {
                        Session["intpageIndex"] = intpageIndexdropdownpopup;
                    }
                }

                if (intpageIndexdropdownpopup == 1)
                {
                    intrecFromdropdownpopup = 0;
                }
                else
                {
                    intrecFromProcessHistory1 = (intpageIndexdropdownpopup * intrecTodropdownpopup) - intrecTodropdownpopup;
                    intrecFromdropdownpopup = intrecFromProcessHistory1 + 1;
                    intrecTodropdownpopup = intrecFromProcessHistory1 + CommonFunctions.GridViewPagesize(stringformIdddlpopup);

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
                //intrecFromProcessHistory1 = 0;
            }
        }
        private void ControlsDropdownValues(string stringType, bool boolEnable)//fix
        {
            try
            {
                if (stringType != null && stringType.Trim().Length > 0)
                {
                    switch (stringType.Trim().ToUpper())
                    {
                        case "RECTYPE":
                            {
                                imgbtntrigerRECTYPE.Enabled = boolEnable;
                                imgbtnClearRECTYPE.Enabled = boolEnable;
                                break;
                            }
                        case "HOSTINS":
                            {
                                // imgbtntrigerHOSTINS.Enabled = boolEnable;
                                //imgbtnClearHOSTINS.Enabled = boolEnable;
                                break;
                            }
                        case "DEPTID":
                            {
                                //imgbtntxtDeptIDtrigger.Enabled = boolEnable;
                                //imgbtnclrtxtDeptID.Enabled = boolEnable;
                                break;
                            }
                        case "DIVSIONCODE":
                            {
                                //imgbtntrigerDIVSIONCODE.Enabled = boolEnable;
                                //imgbtnClearDIVSIONCODE.Enabled = boolEnable;
                                break;
                            }
                        case "DOCTOR":
                            {
                                //imgbtntrigerDOCTOR.Enabled = boolEnable;
                                //imgbtnClearDOCTOR.Enabled = boolEnable;
                                break;
                            }

                        case "COSTCENTER":
                            {
                                //imgbtntrigerCOSTCENTER.Enabled = boolEnable;
                                //imgbtnClearCOSTCENTER.Enabled = boolEnable;
                                break;
                            }
                    }
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        #endregion

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