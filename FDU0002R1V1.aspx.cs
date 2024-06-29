using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FDU0002R1V1 : System.Web.UI.Page
    {
        public string stringformIdPaginggvlist = "FE009R1V1popupPaging";
        public int intpageIndexgvlist = 0;
        public int intrecFromgvlist = 0;
        public int intrecTogvlist = 0;
        public string stringformIdddlpopup = "FDU0002searchPaging";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CommonFunctions.IsActive())
            {
                try
                {
                    intrecTogvlist = CommonFunctions.GridViewPagesize(stringformIdddlpopup);
                    if (!IsPostBack)
                    { 
                        CommonFunctions.HeaderName(this, "FDU0002R1V1");
                        SearchRecords();
                        VerifyAccessRights();
                    }

                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }
            }

        }

        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)//fix
        { 
            try
            {
                if (!DoEmptyValidation())
                {
                    SearchRecords();
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
            // imgBtnPrint.Enabled = false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FDU0002R1V1";
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
            try
            {

                if (txtgroupname.Text.Trim().ToString().Length > 0 && txtgroupname.Text.Trim().ToString() != "%")
                {
                    stringExpression += txtgroupname.Text.ToString().Replace("%", "").Length > 0 ? "AND (UPPER(AOF.grp_name)  LIKE UPPER('%" + txtgroupname.Text.Trim().ToString() + "%'))" : "";
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
                stringExpression = "";
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
            string stringformid = "FDU0001R1V1";
            string stringOrderBy = "AOF.created_on desc";
            string stringServiceType = "List1R1V1";
            string stringexp = "";
            int intRecordFrom = 0;
            int intRecordTo = 0;
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
                            CommonFunctions.ShowMessageboot(this, "No Records Found");
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
                intRecordFrom = 0;
                intRecordTo = 0;
            }
        }



        //private void LoadReportType()
        //{
        //    DataSet objDatasetResult = null;
        //    int interrorcount = 0;
        //    int intTotalRecord = 0;
        //    string[] stringOutputResult = null;
        //    string stringformid = "FA0024R1V1";
        //    string stringOrderBy = "";
        //    int intFromRecord = 0;
        //    int intToRecord = int.MaxValue;
        //    DataTable objDataTable = null;
        //    string stringbeid = CommonFunctions.GETBussinessEntity();
        //    try
        //    {
        //        ddlactby.Items.Clear();

        //        string stringcondition = "And mrrets.be_id= '" + stringbeid + "'  AND mrrets.delmark='N' AND mrrets.Reference_no_2 is not null";
        //        string stringServiceType = "List1R1V1";

        //        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
        //        if (interrorcount == 0)
        //        {
        //            if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
        //            {
        //                objDataTable = objDatasetResult.Tables["t1"];
        //            }

        //            if (objDataTable != null && objDataTable.Rows.Count > 0)
        //            {
        //                objDataTable.DefaultView.Sort = "REFERENCE_NO_2 asc,SHORT_NAME asc";
        //                objDataTable = objDataTable.DefaultView.ToTable();

        //                ddlactby.DataTextField = "short_name";
        //                ddlactby.DataValueField = "rpttyp_id";
        //                ddlactby.DataSource = objDataTable;
        //                ddlactby.DataBind();
        //                ddlactby.Items.Insert(0, new ListItem("", ""));
        //            }


        //        }
        //        else
        //        {
        //            Errorpopup(stringOutputResult);
        //        }


        //    }
        //    catch (Exception objException)
        //    {
        //        CommonFunctions.HandleException(objException);
        //    }

        //}

        private bool ValidateControls()
        {
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "You must enter the value for the following fields:- " + "\\r\\n";



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
            bool boolStatus = true;
            string stringOverallMsg = "";
            try
            {
                boolStatus = true;
                stringOverallMsg = "";



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

        private void ClearValues()//fix
        {
            try
            {
                txtgroupname.Text = "";

                Session["stringDMLIndicator"] = "I";
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
                if (txtgroupname.Text.Trim().Length > 0) { return false; }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }

            return true;
        }

        #region validate HRN



        #endregion

        protected void lnkPagegvList_Click(object sender, EventArgs e)
        {
            int recFrom1 = 0;
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
                    recFrom1 = (intpageIndexgvlist * intrecTogvlist) - intrecTogvlist;
                    intrecFromgvlist = recFrom1 + 1;
                    intrecTogvlist = recFrom1 + CommonFunctions.GridViewPagesize(stringformIdPaginggvlist);
                }
                SearchRecords();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                recFrom1 = 0;
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
            string[] stringValues = null;
            string stringGroupID = "";
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
                            stringGroupID = stringValues[0];

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
                stringGroupID = "";
            }
        }
      

        protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
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

        protected void btnthirdleveladd_Click(object sender, EventArgs e)
        {
            try
            {
                Modelpopuperrorsuccess.Show();
            }
            catch (Exception objException)
            {

                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        protected void btnConfirmprocessClose_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmprocessStatus_Click(object sender, EventArgs e)
        {

        }
    }
}