using CLUSTER_MRTS.CommonFunction;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FA0038R1V1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CommonFunctions.IsActive())
            {
                string Stringorderid = "";
                try
                {
                    if (!IsPostBack)
                    {
                        //Session["FA0038R1V1_id"] = null;
                        Session["FA0038R1V1_ADD"] = null;
                        LoadBusinessEntity();
                        CommonFunctions.HeaderName(this, "FA0038R1V1");

                        if (Request.QueryString["Load"] != null && Request.QueryString["Load"].Trim().Length > 0)
                        {
                            Session["FA0038R1V1_id"] = null;
                        }
                        VerifyAccessRights();
                        if (Session["FA0038R1V1_id"] != null && Session["FA0038R1V1_id"].ToString().Trim().Length > 0 )
                        {
                            Stringorderid = Session["FA0038R1V1_id"].ToString();
                            Session["FA0038R1V1_id"] = Stringorderid;
                            LoadData(Stringorderid);
                        }
                        else
                        {
                           
                            clearvalues(); 
                            Session["FA0038R1V1_id"] = null;
                            pnlRemarksresultgrid.Visible = false;
                        }

                    }

                }
                catch (Exception objException)
                {
                    CommonFunctions.ShowMessageboot02(objException);
                }
                finally
                {
                    Stringorderid = null;
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

            imgbtnNew.Enabled = false; 
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FA0038R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        if (objDataRow["new"].ToString().ToUpper() == "ENABLED")
                        {
                            imgbtnNew.Enabled = true; 
                        }
                        if (objDataRow["edit"].ToString().ToUpper() == "ENABLED")
                        {
                            imgbtnNew.Enabled = true; 
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
                stringstatus = null;
                stringOutputResult = null;
                stringComponent = null;
                objDataRow = null;
            }
        }
        private void LoadData(string stringbedep)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0038R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            Session["FA0038R1V1_ADD"] = null; DataTable objdattable = null;
            string stringcondition = "";
            string stringServiceType = "List1R1V1";
            string stringfeeefective = ""; 
            DateTime objDateTimeFrom;
            string stringformattedEffectiveDate = "";
            try
            {
                if (Session["EFFECTIVEdATE"] != null)
                {
                    stringfeeefective = Session["EFFECTIVEdATE"].ToString();
                    if (stringfeeefective.Length > 0)
                    {
                        objDateTimeFrom = CommonFunctions.ConvertToDateTime(stringfeeefective.ToString(), "yyyy-MM-dd");
                        stringformattedEffectiveDate = CommonFunctions.ConvertDateTimetoString(objDateTimeFrom);
                    }
                }
                if (stringfeeefective.Length > 0)
                {
                    stringcondition = "And bogst.be_id= '" + stringbeid + "' And bogst.GST= '" + stringbedep.ToUpper() + "' And bogst.EFFECTIVE_DATE='" + stringformattedEffectiveDate + "'";

                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {

                        if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {
                            objdattable = objDatasetResult.Tables["t1"];

                            foreach (DataRow row in objdattable.Rows)
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

                            ViewState["3rdLevelPopupADDdataLOADDATA"] = objdattable;
                            Session["FA0038R1V1_ADD"] = objdattable;
                            Bindings(objdattable);
                            clearvalues();
                            Loaddatavalu(stringbeid, stringbedep, stringfeeefective);


                        }
                    }
                    else
                    {
                        Errorpopup(stringOutputResult);
                    }
                }
                else
                { 
                    Bindings(objdattable);
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
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringbeid = null;
                stringcondition = null;
                stringServiceType = null;
            }
        }
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
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        private void LoadBusinessEntity()
        {
            int interrorcount = 0;
            string[] stringOutpuResult = null;
            try
            {
                object[] objControls = new object[]
                   {
                    ddlBO,
                   };
                interrorcount = CommonFunctions.LoadBusinessEntity(objControls, out stringOutpuResult);
                if (interrorcount == 1)
                {
                    Errorpopup(stringOutpuResult);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                interrorcount = 0;
                stringOutpuResult = null;
            }
        }
        protected void lnkbtnusrid_Click(object sender, EventArgs e)
        {

        }

        protected void imgbtnAudit_Click(object sender, ImageClickEventArgs e)
        {
            DataRow[] objDataRow = null;
            try
            {
                if (Session["AuditLogFA0038R1V1"] != null)
                {
                    objDataRow = (DataRow[])Session["AuditLogFA0038R1V1"];
                    if (objDataRow != null)
                    {
                        CommonFunctions.AuditLog(this, objDataRow);
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objDataRow = null;
            }

        }

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            SaveData("DELETE");
        }

        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveData("INSERT");
        }
        private void ShowMessageandReloadPage(Page page, string stringMessageContent, string stringURL)
        {
            try
            {
                if (stringMessageContent.Trim().Length > 0)
                {
                    stringMessageContent = Server.HtmlEncode(stringMessageContent);
                    stringMessageContent = CommonFunctions.EncodeToJavaString(stringMessageContent.Trim());
                    ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "function", "<script language='JavaScript'>  bootbox.alert('" + stringMessageContent + "');window.location='" + stringURL + "';</script>", false);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private void SaveData(string stringTYPE)
        {
            DataSet objDatasetResult = null;
            string[] stringOutputResult = null;
            string stringformid = "FA0038R1V1";
            objDatasetResult = new DataSet();
            DataSet objDataSetToSave = null;
            DataSet objDatasetResult1 = null; 
            DataTable objProductTable = null; 
            DataTable objProductTableSAVEDRecords = null; 
            DataRow[] objDataselectedrow3rdlevel = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            int interrorcount = 0;
            int intTotalRecord = 0;
            bool boolrecexixts = true; 
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringServiceType = "";
            string stringexp = ""; 
            string stringServiceType1 = "";
            string stringEFFECTIVE_DATE = "";
            string stringGST = "";
            string stringDML_INDICATOR = "";
            DataRow[] objdatarowState = null;
            DataRow[] objdatarowState1 = null;
            DataSet objdataset5 = null;
            DataRow[] objdatarowState2 = null;
            try
            {
                stringServiceType = "DEFAULT";
                stringexp = "";
                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {
                    if (objDatasetResult != null && objDatasetResult.Tables["t1"] !=null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                    {
                        if (Session["FA0038R1V1_ADD"] != null)
                        {
                            objDataSetToSave = objDatasetResult.Clone();
  
                            if (boolrecexixts)
                            {
                                if (Session["FA0038R1V1_ADD"] != null)
                                {
                                    objProductTable = (DataTable)Session["FA0038R1V1_ADD"];
                                    if (objProductTable != null && objProductTable.Rows.Count > 0)
                                    { 
                                        //for (int intIndex = 0; intIndex < objProductTable.Rows.Count; intIndex++)
                                        //{
                                        //    stringEFFECTIVE_DATE = objProductTable.Rows[intIndex]["EFFECTIVE_DATE"].ToString();
                                        //    DateTime feeEffDate;
                                        //    if (DateTime.TryParse(stringEFFECTIVE_DATE, out feeEffDate))
                                        //    { 
                                        //    }
                                        //    stringGST = objProductTable.Rows[intIndex]["GST"].ToString();
                                        //    stringDML_INDICATOR = objProductTable.Rows[intIndex]["DML_INDICATOR"].ToString();
                                        //    if (ViewState["3rdLevelPopupADDdataLOADDATA"] != null)
                                        //    {
                                        //        objProductTableSAVEDRecords = (DataTable)ViewState["3rdLevelPopupADDdataLOADDATA"];
                                        //        if (objProductTableSAVEDRecords != null && objProductTableSAVEDRecords.Rows.Count > 0)
                                        //        {
                                        //            if (objProductTableSAVEDRecords.Rows.Count > 0 && objProductTableSAVEDRecords.Select("GST='" + stringGST.ToString() + "' AND EFFECTIVE_DATE = #" + feeEffDate.ToString("yyyy-MM-dd") + "# ").Length > 0)
                                        //            {
                                        //                if (objProductTable.Rows.Count > 0 && objProductTable.Select("GST='" + stringGST.ToString() + "' AND EFFECTIVE_DATE = #" + feeEffDate.ToString("yyyy-MM-dd") + "# and DML_INDICATOR='U' ").Length > 0)
                                        //                {
                                        //                    objDataselectedrow3rdlevel = objProductTable.Select("GST='" + stringGST.ToString() + "' AND EFFECTIVE_DATE = #" + feeEffDate.ToString("yyyy-MM-dd") + "# and DML_INDICATOR='U' ");
                                        //                    if (objDataselectedrow3rdlevel != null && objDataselectedrow3rdlevel.Length > 0)
                                        //                    {
                                        //                        objDataselectedrow3rdlevel[0]["DML_INDICATOR"] = "U";
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        objDataselectedrow3rdlevel[0]["DML_INDICATOR"] = "I";
                                        //                    }
                                        //                }
                                        //            }
                                        //            else
                                        //            {
                                        //                objDataselectedrow3rdlevel = objProductTable.Select("GST='" + stringGST.ToString() + "' AND EFFECTIVE_DATE = #" + feeEffDate.ToString("yyyy-MM-dd") + "#  ");
                                        //                if (objDataselectedrow3rdlevel != null && objDataselectedrow3rdlevel.Length > 0)
                                        //                {
                                        //                    objDataselectedrow3rdlevel[0]["DML_INDICATOR"] = "I";
                                        //                }
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            objDataselectedrow3rdlevel = objProductTable.Select("GST='" + stringGST.ToString() + "' AND EFFECTIVE_DATE = #" + feeEffDate.ToString("yyyy-MM-dd") + "#  ");
                                        //            if (objDataselectedrow3rdlevel != null && objDataselectedrow3rdlevel.Length > 0)
                                        //            {
                                        //                objDataselectedrow3rdlevel[0]["DML_INDICATOR"] = "I";
                                        //            }
                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        objDataselectedrow3rdlevel = objProductTable.Select("GST='" + stringGST.ToString() + "' AND EFFECTIVE_DATE = #" + feeEffDate.ToString("yyyy-MM-dd") + "#  ");
                                        //        if (objDataselectedrow3rdlevel != null && objDataselectedrow3rdlevel.Length > 0)
                                        //        {
                                        //            objDataselectedrow3rdlevel[0]["DML_INDICATOR"] = "I";
                                        //        }
                                        //    }

                                        //}
                                        objProductTable.AcceptChanges();

                                        objDataSetToSave.Tables["t1"].Merge(objProductTable);
                                        foreach (DataRow row in objDataSetToSave.Tables["t1"].Rows)
                                        {
                                            objDataSetToSave.Tables["t1"].Rows[0]["be_id"] = stringbeid.ToString().ToUpper();
                                        }
                                    }
                                }


                                objDataSetToSave.AcceptChanges();
                                for (int intIndex = 0; intIndex < objDataSetToSave.Tables.Count; intIndex++)
                                {
                                    if (objDataSetToSave.Tables[intIndex].Rows.Count > 0)
                                    {

                                       objdatarowState = objDataSetToSave.Tables[intIndex].Select("DML_INDICATOR = 'D'");
                                        if (objdatarowState != null && objdatarowState.Length > 0)
                                        {
                                            for (int intIndex1 = 0; intIndex1 < objdatarowState.Length; intIndex1++)
                                            {
                                                objdatarowState[intIndex1].Delete();
                                            }
                                        }

                                        objdatarowState1 = objDataSetToSave.Tables[intIndex].Select("DML_INDICATOR = 'U'");
                                        if (objdatarowState1 != null && objdatarowState1.Length > 0)
                                        {
                                            for (int intIndex2 = 0; intIndex2 < objdatarowState1.Length; intIndex2++)
                                            {
                                                objdatarowState1[intIndex2].SetModified();
                                            }
                                        }
                                        objdatarowState2 = objDataSetToSave.Tables[intIndex].Select("DML_INDICATOR = 'I'");
                                        if (objdatarowState2 != null && objdatarowState2.Length > 0)
                                        {
                                            for (int intIndex3 = 0; intIndex3 < objdatarowState2.Length; intIndex3++)
                                            {

                                                objdatarowState2[intIndex3].SetAdded();
                                            }
                                        }
                                    }
                                }
                                objdataset5 = objDataSetToSave.GetChanges();

                                stringServiceType1 = "OperationServiceDML";
                                objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType1, objDataSetToSave.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                                if (intErrorCount != 0)
                                {
                                    Errorpopup(stringOutputResult);
                                }

                                else
                                {
                                    if (Session["FA0038R1V1_ID"] == null)
                                    {
                                        string stringmessage = "Record Saved successfully";
                                        //Session["FA0038R1V1_ID"] = Session["gstvalue"].ToString();
                                        Session["FA0038R1V1_ID"] = null;
                                        //ShowMessageandReloadPage(this, stringmessage, "FA0024R1V1.aspx?Load=01");
                                        CommonFunctions.ShowMessageboot(this, stringmessage);

                                    }
                                    else
                                    {
                                        string stringmessage = "Record Updated successfully ";
                                        CommonFunctions.ShowMessageboot(this, stringmessage);
                                        Session["FA0038R1V1_ID"] = null;
                                        //ShowMessageandReloadPage(this, stringmessage, "FA0024R1V1.aspx?Load=01");
                                    }
                                }
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Please Configure GST");

                        }
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
            finally
            {
                 objDatasetResult = null;
                 stringOutputResult = null;
                 stringformid = null;
                 objDataSetToSave = null;
                 objDatasetResult1 = null;
                 objProductTable = null;
                 objProductTableSAVEDRecords = null;
                 objDataselectedrow3rdlevel = null;
                 stringbeid = null;
                 interrorcount = 0;
                 intTotalRecord = 0;
                 boolrecexixts = false;
                 stringOrderBy = "";
                 intFromRecord = 0;
                 intToRecord = int.MaxValue;
                 stringServiceType = null;
                 stringexp = null; 
                 stringServiceType1 = null;
                 stringEFFECTIVE_DATE = null;
                 stringGST = null;
                 stringDML_INDICATOR = null;
                 objdatarowState = null;
                 objdatarowState1 = null;
                 objdataset5 = null;
                 objdatarowState2 = null;
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable objOrderTable = null;
            DataRow[] objDataRow = null;
            string stringCmdArgument = ""; 
            string stringGST = "";
            string stringEFFECTIVE_DATE = "";
            string[] stringValues = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();

            DateTime? objDateTimeFrom = null;
            try
            {
                if (sender != null)
                {
                    stringCmdArgument = ((Button)sender).CommandArgument;
                    if (stringCmdArgument != null && stringCmdArgument.Trim().Length > 0)
                    {
                        stringValues = stringCmdArgument.Split(',');
                        if (stringValues != null && stringValues.Length > 0)
                        {
                            stringGST = stringValues[0];
                            stringEFFECTIVE_DATE = stringValues[1];

                            objDateTimeFrom = CommonFunctions.ConvertToDateTime(stringEFFECTIVE_DATE.ToString(), "yyyy-MM-dd");
                            stringEFFECTIVE_DATE = CommonFunctions.ConvertDateTimetoString(objDateTimeFrom);
                             
                            if (Session["FA0038R1V1_ADD"] != null)
                            {
                                objOrderTable = (DataTable)Session["FA0038R1V1_ADD"];
                                if (objOrderTable != null && objOrderTable.Rows.Count > 0)
                                { 

                                }
                            }
                            if (objOrderTable != null && objOrderTable.Rows.Count > 0)
                            {
                                if (stringGST.Length > 0 && objDateTimeFrom != null)
                                {
                                    if (objOrderTable != null && objOrderTable.Rows.Count > 0 )
                                    {
                                        objDataRow = objOrderTable.Select("be_id= '" + stringbeid + "' And GST='" + stringGST.ToString() + "' AND EFFECTIVE_DATE = #" + stringEFFECTIVE_DATE + "#  ");
                                    }
                                }
                                if (objDataRow != null && objDataRow.Length > 0)
                                {
                                    if (objDataRow[0]["DML_INDICATOR"].ToString() == "I")
                                    {
                                        objDataRow[0].Delete();
                                    }
                                    else
                                    {
                                        objDataRow[0]["DML_INDICATOR"] = "D";
                                    }
                                }

                                objOrderTable.AcceptChanges();
                                Session["FA0038R1V1_ADD"] = objOrderTable;
                                SaveData("");
                                

                                foreach (DataRow row in objOrderTable.Rows)
                                {
                                    if (objDataRow[0]["DML_INDICATOR"].ToString() == "D")
                                    {
                                        objDataRow[0].Delete();
                                    }
                                }
                                objOrderTable.AcceptChanges();

                                Session["FA0038R1V1_ADD"] = objOrderTable;
                                Bindings(objOrderTable);
                                clearvalues();
                            }

                        }
                    }
                } 
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
            finally
            {
                objOrderTable = null;
                objDataRow = null;
                stringCmdArgument = null;
                stringValues = null;
            }
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("FE0001R1V2?ID=FA0038R1V1");
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
                clearvalues();
            }
            catch (Exception objException)
            {

                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
        }

        private void clearvalues()
        {
            try
            {
               
                    object[] objControls = new object[]
                    {
                    txtrepname,
                    txtgst,
                    txtrepname,
                    chkDelMark,
                    ddlBO,
                    txttaxcode,
                    };
                    CommonFunctions.ClearASPControlValues(objControls);
                    if (ddlBO.SelectedItem != null)
                    {
                        ddlBO.SelectedIndex = 1;
                    }

                     txtgst.Enabled = true;
                     ddlBO.Enabled = false;
                     txtrepname.Enabled = true;
                Session["FA0038R1V1_id"] = null;
                Session["AuditLogFA0038R1V1"] = null;
            }
            catch (Exception objException)
            {

                CommonFunctions.ShowMessageboot(this, objException.Message);
            }
        }

        protected void lnkbtnUserID_Click(object sender, EventArgs e)
        {
            DataRow[] objdatarow = null;
            DataTable objDatatable = null;
            string stringCmdArgument = "";
            string[] stringValues = null;
            string stringbeid = "";
            string stringbedep = "";
            string stringGST = "";
            string stringfeeefective = "";
            Session["FA0038R1V1_id"] = null;
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
                            stringbeid = stringValues[0];
                            stringGST = stringValues[1];
                            stringfeeefective = stringValues[2];
                            Loaddatavalu( stringbeid, stringGST, stringfeeefective);

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
                objdatarow = null;
                objDatatable = null;
                stringCmdArgument = null;
                stringValues = null;
                stringbeid = null;
                stringbedep = null;
                stringGST = null;
            }
        }

        private void Loaddatavalu(string stringbeid,  string stringGST, string stringfeeefective)
        {
            DataRow[] objdatarow = null;
            DataTable objDatatable = null;
            string stringbedep = "";
            try
            {
                if (Session["FA0038R1V1_ADD"] != null)
                {
                    objDatatable = (DataTable)Session["FA0038R1V1_ADD"];
                    if (objDatatable != null && objDatatable.Rows.Count > 0)
                    {
                        ViewState["exportconditionFA0038R1V1"] = "And bogst.be_id= '" + stringbeid + "' And GST='" + stringGST.ToString() + "' AND EFFECTIVE_DATE = #" + stringfeeefective + "# ";

                        objdatarow = objDatatable.Select("be_id= '" + stringbeid + "' And GST='" + stringGST.ToString() + "' AND EFFECTIVE_DATE = #" + stringfeeefective + "#  ");
                        if (objdatarow != null && objdatarow.Length > 0)
                        {
                            Session["AuditLogFA0038R1V1"] = objdatarow;
                            ddlBO.ClearSelection();
                            stringbedep = objdatarow[0]["be_id"].ToString();
                            if (ddlBO.Items.FindByValue(stringbedep) != null)
                                ddlBO.Items.FindByValue(stringbedep).Selected = true;

                            txtgst.Text = objdatarow[0]["GST"].ToString();
                            Session["gstvalue"] = txtgst.Text.ToString();
                            txttaxcode.Text = objdatarow[0]["INT_TAX_CODE"].ToString();

                            if (objdatarow[0]["EFFECTIVE_DATE"] != null && objdatarow[0]["EFFECTIVE_DATE"].ToString().Trim().Length > 0)
                            { txtrepname.Text = Convert.ToDateTime(objdatarow[0]["EFFECTIVE_DATE"]).ToString("dd-MM-yyyy"); }

                            if (objdatarow[0]["TAX_ALLOWED"].ToString() == "Y")
                                chkDelMark.Checked = true;
                            ddlBO.Enabled = false;
                            txtgst.Enabled = false;
                            txtrepname.Enabled = false;

                            Session["FA0038R1V1_id"] = stringGST;


                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.HandleException(objException);
            }
        }

        protected void btnthirdleveladd_Click(object sender, EventArgs e)
        {
            string stringErrorMsg = "";
            string stringremarksID = "";
            DataTable objDatatableALL = null;
            DataTable objDatatable = null;
            string stringbeid = CommonFunctions.GETBussinessEntity().ToString();
            bool boolrecordexits = true;
            bool boolrecrdsave = false;
            DataRow[] objdatarow = null;
            DateTime? objDateTimeFrom= null ;
            string stringformattedEffectiveDate = "";
            try
            {
                if (CommonFunctions.ValidateASPControls(GetThisSCreenControls("TAB1"), out stringErrorMsg))
                {
                    if (txtrepname.Text.Trim().Length > 0)
                    {
                        objDateTimeFrom = CommonFunctions.ConvertToDateTime(txtrepname.Text.ToString(), "yyyy-MM-dd");
                        stringformattedEffectiveDate = CommonFunctions.ConvertDateTimetoString(objDateTimeFrom);
                    }

                    if (Session["FA0038R1V1_ADD"] != null)
                    {
                        objDatatable = (DataTable)Session["FA0038R1V1_ADD"];
                    }
                    else
                    {
                        LoadRecord();
                        if (Session["FA0038R1V1_ADD"] != null)
                        { objDatatable = (DataTable)Session["FA0038R1V1_ADD"]; }
                    }
                    if (objDatatable != null)
                    {
                        //    if (objDatatable != null &&  objDatatable.Rows.Count > 0)
                        //    {
                        if (Session["FA0038R1V1_id"] == null)
                        {

                            if (objDateTimeFrom != null)
                            {
                                objDatatableALL = LoadALLData();
                                if (objDatatableALL != null && objDatatableALL.Rows.Count > 0 && objDatatableALL.Select("be_id = '" + stringbeid + "' and GST = '" + txtgst.Text.Trim() + "' AND EFFECTIVE_DATE = #" + stringformattedEffectiveDate + "#").Length > 0)
                                {
                                    boolrecordexits = false;
                                }
                            }
                        }
                        if (boolrecordexits)
                        {
                            if (stringformattedEffectiveDate.Length > 0)
                            {
                                objDatatableALL = LoadALLData();
                                if (objDatatableALL != null && objDatatableALL.Rows.Count > 0 && objDatatableALL.Select("be_id = '" + stringbeid + "' and GST = '" + txtgst.Text.Trim() + "' AND EFFECTIVE_DATE = #" + stringformattedEffectiveDate.ToString() + "#").Length > 0)
                                {
                                    objdatarow = objDatatable.Select("be_id = '" + stringbeid + "' and GST = '" + txtgst.Text.Trim() + "' AND EFFECTIVE_DATE = #" + stringformattedEffectiveDate + "#");
                                }
                            }

                            if (objdatarow != null && objdatarow.Length > 0)
                            {
                                if (ddlBO.SelectedItem != null && ddlBO.SelectedValue.ToString().Length > 0)
                                {
                                    objdatarow[0]["be_id"] = ddlBO.SelectedValue.ToString().ToUpper();
                                }
                                objdatarow[0]["GST"] = txtgst.Text.Trim().ToUpper();
                                objdatarow[0]["INT_TAX_CODE"] = txttaxcode.Text.Trim();
                                if (txtrepname.Text.Trim().Length > 0)
                                { objdatarow[0]["EFFECTIVE_DATE"] = CommonFunctions.ConvertToDateTime(txtrepname.Text, "dd-MM-yyyy"); }

                                objdatarow[0]["TAX_ALLOWED"] = chkDelMark.Checked ? "Y" : "N";
                                objDatatable.AcceptChanges();
                                boolrecrdsave = true;
                            }
                            else
                            {
                                AddbtnInsert(stringremarksID, objDatatable, stringbeid);
                                boolrecrdsave = true;
                            }
                        }
                        else
                        {
                            CommonFunctions.ShowMessageboot(this, "Record already exist");
                        }
                        //}
                        //else
                        //{
                        //    AddbtnInsert(stringremarksID, objDatatable, stringbeid);
                        //    boolrecrdsave = true;
                        //}
                        if (boolrecrdsave)
                        {
                            SaveData("");
                        }
                        foreach (DataRow row in objDatatable.Rows)
                        {
                            row["DML_INDICATOR"] = "U";
                        }
                        objDatatable.AcceptChanges();
                        Session["FA0038R1V1_ADD"] = objDatatable;
                        objDatatable.AcceptChanges();
                        Bindings(objDatatable);
                        //clearvalues();
                    }
                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, stringErrorMsg);
                }

            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        private void AddbtnInsert(string stringremarksID, DataTable objDataTableAddAttachments, string stringbeid)
        {
            try
            {
                DataRow objdatarow = objDataTableAddAttachments.NewRow();

                if (ddlBO.SelectedItem != null && ddlBO.SelectedValue.ToString().Length > 0)
                {
                    objdatarow["be_id"] = ddlBO.SelectedValue.ToString().ToUpper();
                }
                objdatarow["GST"] = txtgst.Text.Trim().ToUpper();

                objdatarow["INT_TAX_CODE"] = txttaxcode.Text.Trim();

                if (txtrepname.Text.Trim().Length > 0)
                { objdatarow["EFFECTIVE_DATE"] = CommonFunctions.ConvertToDateTime(txtrepname.Text, "dd-MM-yyyy"); } 
                                  
                objdatarow["TAX_ALLOWED"] = chkDelMark.Checked ? "Y" : "N";
                 
                CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                objdatarow["DML_INDICATOR"] = "I";
                objDataTableAddAttachments.Rows.Add(objdatarow);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            } 
        }
        private void Bindings(DataTable objDataTableAddAttachments)
        {
            int intRecordCount = 0;
            try
            {
                if (objDataTableAddAttachments != null && objDataTableAddAttachments.Rows.Count > 0)
                {
                    DataRow[] objDatarow = objDataTableAddAttachments.Select("DML_INDICATOR<>'D'");
                    if (objDatarow.Length > 0)
                    {
                        objDataTableAddAttachments = objDataTableAddAttachments.Select("DML_INDICATOR<>'D'").CopyToDataTable<DataRow>();
                        intRecordCount = objDataTableAddAttachments.Rows.Count;
                    }
                    else
                    {
                        objDataTableAddAttachments = null;
                        intRecordCount = 0;
                    }

                    //PopulateRemarks(intRecordCount, recToRemarks);
                    gvNonMRList.DataSource = objDataTableAddAttachments;
                    gvNonMRList.DataBind();
                    pnlRemarksresultgrid.Visible = true;
                    TotalrecPatient.InnerText = intRecordCount.ToString();
                }
                else
                {
                    //PopulateRemarks(0, recToRemarks);
                   
                    gvNonMRList.DataSource = null;
                    gvNonMRList.DataBind();
                    pnlRemarksresultgrid.Visible = false;
                    TotalrecPatient.InnerText = "0";
                }


            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }

        private object[] GetThisSCreenControls(string stringType)
        {
            object[] objControls = null;
            try
            {
                if (stringType.ToUpper() == "TAB1")
                {
                    objControls = new object[] {
                        txtgst,
                        ddlBO,
                    };

                    return objControls;
                }
               
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

            return objControls;
        }
        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        private void LoadRecord()
        { 
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null; 
            string[] stringOutputResult = null;
            string stringformid = "FA0038R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringErrorMsg = ""; 
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            try
            {
                if (CommonFunctions.ValidateASPControls(GetThisSCreenControls("TAB1"), out stringErrorMsg))
                {
                    stringServiceType = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                        {
                            Session["FA0038R1V1_ADD"] = objDatasetResult.Tables["t1"];
                        }
                    }
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        protected void lnkbtnLevel3sort_Click(object sender, EventArgs e)
        { 
        }

        private DataTable LoadALLData()
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0038R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity(); 
            DataTable objdattable   = null;
            string stringcondition = "";
            string stringServiceType = "";
            try
            {
                stringcondition = "And bogst.be_id= '" + stringbeid + "' ";

                stringServiceType = "List1R1V1";

                objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringcondition, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                if (interrorcount == 0)
                {

                    if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                    {
                       objdattable = objDatasetResult.Tables["t1"];
                       return objdattable;
                    }
                }
                else
                {
                    Errorpopup(stringOutputResult);
                }
                return null;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
                return null;
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
                stringbeid = null;
                stringcondition = null;
                stringServiceType = null;
            }
        }
    }
}