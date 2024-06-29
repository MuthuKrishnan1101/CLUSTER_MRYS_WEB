using CLUSTER_MRTS.CommonFunction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLUSTER_MRTS
{
    public partial class FC0002R1V1 : System.Web.UI.Page
    { 
        public int intrecFrom = 0;
        public int intrecTo = 0;
        public int intpageIndex = 0;
        public string stringformIdPaging = "RegistrationBiodataPopupPaging";

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (CommonFunctions.IsActive())
            {
                intrecTo = CommonFunctions.GridViewPagesize(stringformIdPaging);
                string Stringorderid = "";
                try
                {
                    if (!IsPostBack)
                    {
                        CommonFunctions.HeaderName(this, "FC0002R1V1");
                        Session["stringSortDirection"] = "ASC";

                        if (Request.QueryString["Load"] != null && Request.QueryString["Load"].Trim().Length > 0)
                        {
                            Session["FC0002R1V1_id"] = null;
                        }
                        VerifyAccessRights();
                        if (Session["FC0002R1V1_id"] != null && Session["FC0002R1V1_id"].ToString().Trim().Length > 0 && Session["FC0002R1V1_idGRID"] != null)
                        {
                            Stringorderid = Session["FC0002R1V1_id"].ToString();
                            Session["FC0002R1V1_id"] = Stringorderid;
                            LoadData(Stringorderid);
                        }
                        else
                        {
                            InitializeValues(); 
                            Clearvalues();
                            Session["FC0002R1V1_id"] = null;
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
        private void Clearvalues()
        {
            try
            {

                object[] objControls = new object[]
                {
                   txtHRN,
                   txtpatientID,
                   txtPatName,
                   txtSpecialInformation,
                   txtFromDate,
                   txtToDate,
                   chkDelMark,
                };
                CommonFunctions.ClearASPControlValues(objControls);

                txtHRN.Enabled = true;
                txtHRN.CssClass = "form-control ReadOnly";
                ViewState["exportconditionSPL"] = null;
                Session["FC0002R1V1_id"] = null;
                Session["AuditLogSPL"] = null;
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
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FC0002R1V1";
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

                stringstatus = null;
                stringOutputResult = null;
                stringComponent = null;
                objDataRow = null;
            }
        }
        private void InitializeValues()
        {
            try
            {
                ViewState["vsSortDirection"] = " ASC";
                ViewState["vsSortExpression"] = "";
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
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
        protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Clearvalues();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }

        }
        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            DataRow objdatarow = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            DataSet objDatasetResult1 = null;
            string[] stringOutputResult = null;
            string stringformid = "FC0002R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringdepid1 = "";
            string stringErrorMsg = "";
            bool boolStatus = false;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            string stringexp0123 = "";
            string stringServiceType1 = "";
            string stringexp012 = "";
            string stringdelmark = "";
            try
            {
                if (CommonFunctions.ValidateASPControls(GetThisSCreenControls("TAB1"), out stringErrorMsg))
                {
                    stringServiceType = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);

                    if (objDatasetResult != null && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                    {
                        if (Session["FC0002R1V1_id"] == null)
                        {
                            stringexp0123 = "And spinf.be_id= '" + stringbeid + "' And spinf.PATIENT_ID= '" + txtpatientID.Text.Trim() + "'";
                            stringServiceType1 = "List1R1V1";

                            objDatasetResult1 = CommonFunctions.SelectionServiceClient(stringServiceType1, stringformid, stringexp0123, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                            if (objDatasetResult1 != null && objDatasetResult1.Tables["t1"] != null && objDatasetResult1.Tables["t1"].Rows.Count == 0)
                            {
                                objdatarow = objDatasetResult.Tables["t1"].NewRow();

                                objdatarow["be_id"] = stringbeid;
                                objdatarow["PATIENT_ID"] = txtpatientID.Text.Trim().ToUpper();
                                objdatarow["Short_Name"] = txtPatName.Text.Trim().ToUpper();
                                objdatarow["SPECIAL_INFO"] = txtSpecialInformation.Text.Trim().ToUpper();
                                objdatarow["DELMARK"] = chkDelMark.Checked ? "Y" : "N";
                                if (txtFromDate.Text.Trim().Length > 0)
                                { objdatarow["reference_date_1"] = CommonFunctions.ConvertToDateTime(txtFromDate.Text.Trim(), "dd-MM-yyyy"); }
                                else
                                {
                                    objdatarow["reference_date_1"] = DateTime.Now;
                                }
                                if (txtToDate.Text.Trim().Length > 0) { objdatarow["reference_date_2"] = CommonFunctions.ConvertToDateTime(txtToDate.Text.Trim(), "dd-MM-yyyy"); }

                                CommonFunctions.AssignAuditLogDetails(ref objdatarow);

                                objDatasetResult.Tables["t1"].Rows.Add(objdatarow);
                                boolStatus = true;
                            }
                            else { CommonFunctions.ShowMessageboot(this, "ID already exist"); }
                        }
                        else
                        {

                            if (Session["FC0002R1V1_id"] != null)
                                stringdepid1 = Session["FC0002R1V1_id"].ToString();

                            stringexp012 = "And spinf.be_id= '" + stringbeid + "' And spinf.PATIENT_ID= '" + stringdepid1 + "'";
                            stringServiceType = "List1R1V1";

                            objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                            if (interrorcount == 0)
                            {
                                if (objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                                { 
                                    objDatasetResult.Tables["t1"].Rows[0]["Short_Name"] = txtPatName.Text.Trim().ToUpper();
                                    objDatasetResult.Tables["t1"].Rows[0]["SPECIAL_INFO"] = txtSpecialInformation.Text.Trim().ToUpper();
                                    stringdelmark= chkDelMark.Checked ? "Y" : "N";
                                    objDatasetResult.Tables["t1"].Rows[0]["delmark"] = stringdelmark;

                                    if (txtFromDate.Text.Trim().Length > 0)
                                    { objDatasetResult.Tables["t1"].Rows[0]["reference_date_1"] = CommonFunctions.ConvertToDateTime(txtFromDate.Text.Trim(), "dd-MM-yyyy"); }
                                    else
                                    {
                                        objDatasetResult.Tables["t1"].Rows[0]["reference_date_1"] = DateTime.Now;
                                    }
                                    if (txtToDate.Text.Trim().Length > 0)
                                    { objDatasetResult.Tables["t1"].Rows[0]["reference_date_2"] = CommonFunctions.ConvertToDateTime(txtToDate.Text.Trim(), "dd-MM-yyyy"); }
                                    else
                                    {
                                        objDatasetResult.Tables["t1"].Rows[0]["reference_date_2"] = DBNull.Value;
                                    }

                                    if (Session["stringComputerName"] != null)
                                        objDatasetResult.Tables["t1"].Rows[0]["MODIFIED_AT"] = Session["stringComputerName"].ToString();
                                    if (Session["G11EOSUser_Name"] != null)
                                        objDatasetResult.Tables["t1"].Rows[0]["MODIFIED_BY"] = Session["G11EOSUser_Name"].ToString();
                                    objDatasetResult.Tables["t1"].Rows[0]["MODIFIED_ON"] = DateTime.Now;

                                    objDatasetResult.Tables["t1"].Rows[0].RowState.ToString();
                                    boolStatus = true;
                                }
                            }
                            else
                            {
                                Errorpopup(stringOutputResult);
                            }
                        }
                        if (boolStatus)
                        {
                            objDatasetResult = objDatasetResult.GetChanges();
                            stringServiceType = "OperationServiceDML";
                            objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                            if (intErrorCount == 0)
                            {
                                if (Session["FC0002R1V1_id"] != null)
                                {
                                    CommonFunctions.ShowMessageboot(this, "Record Updated Successfully");
                                }
                                else
                                {
                                    CommonFunctions.ShowMessageboot(this, "Record Saved Successfully");
                                }

                                Session["FC0002R1V1_id"] = txtHRN.Text.Trim().ToUpper();

                                Clearvalues();
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
                    CommonFunctions.ShowMessageboot(this, stringErrorMsg);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
            finally
            {
                objdatarow = null;
                stringdepid1 = null;
                interrorcount = 0;
                intTotalRecord = 0;
                objDatasetResult = null;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringServiceType = null;
                stringexp = null;
                stringexp0123 = null;
                stringServiceType1 = null;
                stringexp012 = null;
                stringdelmark = null;
            }
        }

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FC0002R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            objDatasetResult = new DataSet();
            string stringexp = "";
            string stringServiceType = "";
            try
            {
                if (Session["FC0002R1V1_id"] != null)
                {
                    stringexp = " And spinf.be_id = '" + stringbeid + "' And spinf.PATIENT_ID = '" + Session["FC0002R1V1_id"].ToString() + "'";

                    stringServiceType = "List1R1V1";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                        {

                            foreach (DataRow objDataRow in objDatasetResult.Tables["t1"].Rows)
                            {
                                objDataRow.Delete();
                            }
                            objDatasetResult.Tables[0].Rows[0].RowState.ToString();
                            objDatasetResult = objDatasetResult.GetChanges();
                        }
                        stringServiceType = "OperationServiceDML";
                        objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                        if (intErrorCount == 0)
                        {
                            CommonFunctions.ShowMessageboot(this, "Record deleted successfully");
                            Session["FC0002R1V1_id"] = null;
                            Clearvalues();
                        }
                        else
                        {
                            Errorpopup(stringOutputResult);
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
                objDatasetResult = null;
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                stringbeid = null;
                stringexp = null;
                stringServiceType = null;
            }
        }

        protected void imgbtnAudit_Click(object sender, ImageClickEventArgs e)
        {
            DataRow[] objDataRow = null;
            try
            {
                if (Session["AuditLogSPL"] != null)
                {
                    objDataRow = (DataRow[])Session["AuditLogSPL"];
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

        private void LoadData(string stringbedep)
        {
            DataRow[] objdatarow = null;
            DataSet objDataSet = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                if (Session["FC0002R1V1_idGRID"] != null)
                {
                    objDataSet = (DataSet)Session["FC0002R1V1_idGRID"];
                    if (objDataSet != null && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                    {
                        ViewState["exportconditionSPL"] = "And be_id= '" + stringbeid + "' And PATIENT_ID = '" + stringbedep.ToString() + "'";
                        objdatarow = objDataSet.Tables[0].Select("be_id= '" + stringbeid + "' and PATIENT_ID = '" + stringbedep.ToString() + "'");
                        if (objdatarow != null && objdatarow.Length > 0)
                        {
                            Session["AuditLogSPL"] = objdatarow;

                            txtHRN.Text = objdatarow[0]["HRN_ID"].ToString();
                            txtPatName.Text = objdatarow[0]["SHORT_NAME"].ToString();
                            txtSpecialInformation.Text = objdatarow[0]["SPECIAL_INFO"].ToString();

                            if (objdatarow[0]["reference_date_1"] != null && objdatarow[0]["reference_date_1"].ToString().Trim().Length > 0)
                            { txtFromDate.Text = Convert.ToDateTime(objdatarow[0]["reference_date_1"]).ToString("dd-MM-yyyy"); }
                            else
                            {
                                txtFromDate.Text = "";
                            }

                            if (objdatarow[0]["reference_date_2"] != null && objdatarow[0]["reference_date_2"].ToString().Trim().Length > 0)
                            { txtToDate.Text = Convert.ToDateTime(objdatarow[0]["reference_date_2"]).ToString("dd-MM-yyyy"); }
                            else
                            {
                                txtToDate.Text = "";
                            }
                            if (objdatarow[0]["DELMARK"].ToString() == "Y")
                                chkDelMark.Checked = true;
                            txtHRN.Enabled = false;
                            txtHRN.CssClass = "form-control ReadOnly";
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
                objdatarow = null;
                objDataSet = null;
                stringbeid = null;

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
                        txtHRN,
                        txtPatName,
                        txtSpecialInformation,
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

        protected void LkBtnBios_Click(object sender, EventArgs e)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0017R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            DataTable objDataTablePat = null; 
            string stringBoID = CommonFunctions.GETBussinessEntity();
            string stringInput = "";
            string stringEncrypyValue = "";
            string stringServiceType = "";
            string stringexp012 = "";
            DataRow objDataRowPatient = null;
            try
            {
                object[] objControls = new object[]
                     {
                txtHRN,
                     };
              

                if (txtHRN.Text.Trim().Length == 0)
                {
                    Biodatapopupclearvalues();
                    mpePdtPlt2.Show();
                    PopulatePager(0, intpageIndex);
                }
                else
                {
                    stringInput = txtHRN.Text.Trim().ToUpper();
                    stringEncrypyValue = "";
                    if (stringInput.Length > 0)
                    {
                        stringEncrypyValue = CommonFunctions.HRNtoEncrypyValue(stringInput);
                    }
                    if (stringEncrypyValue.Length > 0 && DoNonCGHHrnValidation(objControls))
                    { 
                        stringServiceType = "List1R1V1"; 
                        stringexp012 = "And mrpats.be_id= '" + stringBoID.ToString() + "' And mrpats.hrn_id= '" + stringEncrypyValue + "'  And mrpats.delmark= 'N'";

                        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                        if (interrorcount == 0)
                        {
                            if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                            {
                                objDataTablePat = objDatasetResult.Tables["t1"];
                            }
                            if (objDataTablePat != null && objDataTablePat.Rows.Count > 0)
                            {
                                objDataRowPatient = objDataTablePat.Rows[0];
                                if (objDataRowPatient != null)
                                {
                                    txtPatName.Text = objDataRowPatient["short_name"].ToString();
                                    txtpatientID.Text = objDataRowPatient["patient_id"].ToString();
                                    if (txtPatName.Text.Trim().Length > 0)
                                    {
                                        txtSpecialInformation.Focus();
                                    }
                                    else { txtHRN.Focus(); }

                                }
                            }
                            else
                            {
                                CommonFunctions.ShowMessageboot(this, "Patient Not Found");
                                txtHRN.Focus();
                            }
                        }
                        else
                        {
                            Errorpopup(stringOutputResult);
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
                interrorcount = 0;
                intTotalRecord = 0;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;
                objDataTablePat = null;
                stringBoID = null;
                stringInput = null;
                stringEncrypyValue = null;
                stringServiceType = null;
                stringexp012 = null;
                objDataRowPatient = null;
            }
        }
        #region validate HRN
        public bool DoNonCGHHrnValidation(object[] objControls)
        {
            bool boolMROHRN = false;
            bool boolStatus = true;
            string stringInput = "";
            string stringResult = "";
            long longTemp = 0;
            try
            {
                boolMROHRN = false;
                boolStatus = true;
                if (objControls[0] is TextBox)
                {
                    var objControl = (TextBox)objControls[0];

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
                                    longTemp = 0;
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
                            // SelectText(txtHRN);
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
                stringInput = null;
                stringResult = null;
                longTemp = 0;
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
                stringSub1 = null;
                stringSub2 = null;
                stringResult = null;
            }
            return stringHRN;
        }
        #endregion

        #region biodata popup
        //for new
        protected void btnbiodatapopupnew_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Biodatapopupclearvalues();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        //for search
        protected void btnbiodatapopupsearch_Click(object sender, ImageClickEventArgs e)
        {
            DataSet objDatasetResult = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            string[] stringOutputResult = null;
            string stringformid = "FA0017R1V1";
            string stringOrderBy = "";
            DataTable objDataTable = null;
            string stringexp012 = "";
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringEncrypyValue = "";
            bool boolstatus = true;
            string stringServiceType = "";
            string stringInput = "";
            int intRecordFrom = 0;
            int intRecordTo = 0;
            try
            {
                if (txthrnbiodata.Text.Length > 0 || txthrnNamebiodata.Text.Length > 0)
                {
                    object[] objControls = new object[]
                  {
                txthrnbiodata,
                  };
                    if (txthrnbiodata.Text.Trim().Length > 0 && !DoNonCGHHrnValidation(objControls))
                    {
                        boolstatus = false;
                    }
                    if (boolstatus)
                    {
                        stringServiceType = "List1R1V1";
                        stringInput = txthrnbiodata.Text.Trim();
                        if (stringInput.Length > 0)
                        {
                            stringEncrypyValue = CommonFunctions.HRNtoEncrypyValue(stringInput);
                        }
                        if (txthrnNamebiodata.Text.Trim().Length > 0 && stringEncrypyValue.Length > 0)//fix
                        {
                            stringexp012 = "And mrpats.be_id= '" + stringbeid + "' And mrpats.hrn_id= '" + stringEncrypyValue.Trim().ToUpper() + "' AND(UPPER(inref.INDEX_VALUE)  LIKE UPPER('%" + txthrnNamebiodata.Text.Trim().Replace("'", "''") + "%'))   And mrpats.delmark= 'N'";

                        }
                        else if (stringEncrypyValue.Length > 0)//fix
                        {
                            stringexp012 = "And mrpats.be_id= '" + stringbeid + "' And mrpats.hrn_id= '" + stringEncrypyValue.Trim().ToUpper() + "'  And mrpats.delmark= 'N'";
                        }
                        else if (txthrnNamebiodata.Text.Trim().Length > 0)//fix
                        {

                            stringexp012 = "And mrpats.be_id= '" + stringbeid + "'  AND(UPPER(inref.INDEX_VALUE)  LIKE UPPER('%" + txthrnNamebiodata.Text.Trim().Replace("'", "''") + "%'))  And mrpats.delmark= 'N'";

                        }

                        intRecordFrom = intrecFrom;
                        intRecordTo = intrecTo;

                        objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp012, stringOrderBy, intRecordFrom, intRecordTo, out intTotalRecord, out interrorcount, out stringOutputResult);
                        if (interrorcount == 0)
                        {
                            PopulatePager(intTotalRecord, intpageIndex);

                            if (objDatasetResult != null && objDatasetResult.Tables.Count > 0 && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count > 0)
                            {
                                objDataTable = objDatasetResult.Tables["t1"];
                            }
                            if (objDataTable != null && objDataTable.Rows.Count > 0)
                            {
                                gvlistbiodatapopup.DataSource = objDataTable;
                                gvlistbiodatapopup.DataBind();
                            }
                            else
                            {
                                gvlistbiodatapopup.DataSource = null;
                                gvlistbiodatapopup.DataBind();
                                CommonFunctions.ShowMessageboot(this, "No Records Found");
                                txthrnbiodata.Focus();
                            }
                        }
                        else
                        {
                            PopulatePager(0, intpageIndex);

                            CommonFunctions.ShowMessageboot(this, stringOutputResult[0]);
                        }
                    }
                    mpePdtPlt2.Show();
                    txthrnbiodata.Focus();

                }
                else
                {
                    CommonFunctions.ShowMessageboot(this, "Please Fill at least One Criteria");
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
                objDataTable = null;
                stringexp012 = null;
                stringbeid = null;
                stringEncrypyValue = null;
                boolstatus = true;
                stringServiceType = null;
                stringInput = null;
                intRecordFrom = 0;
                intRecordTo = 0;
            }
        }
        //for clearvalues
        private void Biodatapopupclearvalues()
        {
            try
            {
                txthrnbiodata.Text = "";
                txthrnNamebiodata.Text = "";
                gvlistbiodatapopup.DataSource = null;
                gvlistbiodatapopup.DataBind();
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        //for paging
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

                btnbiodatapopupsearch_Click(null, null);
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

        //for popup linkbutton click in grid
        protected void lnkbtnpopupHRN_Click(object sender, EventArgs e)
        {
            string stringCmdArgument = "";
            string stringHRNId = "";
            string stringHRNname = "";
            string stringpatientID = "";
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
                            stringHRNId = stringValues[0];
                            stringHRNname = stringValues[1];
                            stringpatientID = stringValues[2];

                            txtHRN.Text = stringHRNId;
                            txtpatientID.Text = stringpatientID;
                            mpePdtPlt2.Hide();
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
                stringHRNId = null;
                stringHRNname = null;
                stringpatientID = null;
                stringValues = null;
            }
        }


        #endregion

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("FE0001R1V2?ID=FC0002R1V1");
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        protected void txtHRN_TextChanged(object sender, EventArgs e)
        { 
            try
            {
                if (txtHRN.Text.Trim().Length > 5)
                { 
                    LkBtnBios_Click(LkBtnBios, null);
                }
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }

       
    }
}