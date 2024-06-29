using CLUSTER_MRTS.CommonFunction;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;

namespace CLUSTER_MRTS
{
    public partial class FA0036R1V1 : System.Web.UI.Page
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
                        LoadBusinessEntity();
                        CommonFunctions.HeaderName(this, "FA0036R1V1");

                        if (Request.QueryString["Load"] != null && Request.QueryString["Load"].Trim().Length > 0)
                        {
                            Session["FA0036R1V1_id"] = null;
                        }
                        VerifyAccessRights();
                        if (Session["FA0036R1V1_id"] != null && Session["FA0036R1V1_id"].ToString().Trim().Length > 0 && Session["FA0036R1V1_idGRID"] != null)
                        {
                            Stringorderid = Session["FA0036R1V1_id"].ToString();
                            Session["FA0036R1V1_id"] = Stringorderid;
                            LoadData(Stringorderid);
                        }
                        else
                        {
                            InitializeValues(); 
                            Clearvalues();
                            Session["FA0036R1V1_id"] = null;
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
                    txtspecialtyid,
                    txtspecdesc,

                    chkDelMark,
                    ddlinstitution,
                    
                }; 
                CommonFunctions.ClearASPControlValues(objControls);
                if (ddlinstitution.SelectedItem != null)
                {
                    ddlinstitution.SelectedIndex = 1;
                }
                txtorderid.Text = "";
                txtspecialtyid.Enabled = true;
                ddlinstitution.Enabled = false;
                txtspecialtyid.CssClass = ddlinstitution.CssClass = "form-control ReadOnly";
                ViewState["exportconditionFA0036R1V1"] = null;
                Session["FA0036R1V1_id"] = null;
                Session["AuditLogFA0036R1V1"] = null;
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
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
        private void VerifyAccessRights()
        {

            DataSet objDatasetResult = null;

            string stringstatus = "";
            string[] stringOutputResult = null;
            string[] stringComponent = null;
            DataRow objDataRow = null;

            imgbtnNew.Enabled = false;
            imgbtnSave.Enabled = false;
            try
            {

                stringComponent = new string[1];
                stringComponent[0] = "FA0036R1V1";
                objDatasetResult = CommonFunctions.verifyaccessrights(out stringstatus, out stringOutputResult, stringComponent);

                if (objDatasetResult != null && objDatasetResult.Tables["SystemAccess"] != null && objDatasetResult.Tables["SystemAccess"].Rows.Count > 0)
                {
                    objDataRow = objDatasetResult.Tables["SystemAccess"].Rows[0];
                    if (objDataRow != null && objDataRow["access_status"].ToString().ToUpper() == "ENABLED")
                    {
                        if (objDataRow["new"].ToString().ToUpper() == "ENABLED")
                        {
                            imgbtnNew.Enabled = true;
                            imgbtnSave.Enabled = true;
                        }
                        if (objDataRow["edit"].ToString().ToUpper() == "ENABLED")
                        {
                            imgbtnNew.Enabled = true;
                            imgbtnSave.Enabled = true;
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
                    ddlinstitution,
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
        private void LoadData(string stringbedep)
        {
            DataRow[] objdatarow = null;
            DataSet objDataSet = null;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            try
            {
                if (Session["FA0036R1V1_idGRID"] != null)
                {
                    objDataSet = (DataSet)Session["FA0036R1V1_idGRID"];
                    if (objDataSet != null && objDataSet.Tables[0] != null && objDataSet.Tables[0].Rows.Count > 0)
                    {
                        ViewState["exportconditionFA0036R1V1"] = "And spec.be_id= '" + stringbeid + "' And ID = '" + stringbedep.ToString() + "'";
                        objdatarow = objDataSet.Tables[0].Select("be_id= '" + stringbeid + "' and ID = '" + stringbedep.ToString() + "'");
                        if (objdatarow != null && objdatarow.Length > 0)
                        {
                            Session["AuditLogFA0036R1V1"] = objdatarow;
                            ddlinstitution.ClearSelection();
                            stringbedep = objdatarow[0]["be_id"].ToString();
                            if (ddlinstitution.Items.FindByValue(stringbedep) != null)
                                ddlinstitution.Items.FindByValue(stringbedep).Selected = true;

                            txtspecialtyid.Text = objdatarow[0]["ID"].ToString();
                            txtspecdesc.Text = objdatarow[0]["SHORT_NAME"].ToString();
                            //txtRemarks.Text = objdatarow[0]["remarks"].ToString();
                            //txtGroupName.Text = objdatarow[0]["group_name"].ToString();
                            txtorderid.Text = objdatarow[0]["ORDER_ID"].ToString();
                            if (txtorderid.Text == "9999")
                            {
                                txtorderid.Text = "";
                            }
                            if (objdatarow[0]["delmark"].ToString() == "Y")
                                chkDelMark.Checked = true;
                            ddlinstitution.Enabled = false;
                            txtspecialtyid.Enabled = false;

                            txtspecialtyid.CssClass = ddlinstitution.CssClass = "form-control ReadOnly";
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

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("FE0001R1V2?ID=FA0036R1V1");
            }
            catch (Exception objException)
            {
                CommonFunctions.ShowMessageboot02(objException);
            }
        }
        
        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveData("INSERT");
        }
        private object[] GetThisSCreenControls(string stringType)
        {
            object[] objControls = null;
            try
            {
                if (stringType.ToUpper() == "TAB1")
                {
                    objControls = new object[] {
                        txtspecialtyid,
                        txtspecdesc,
                        ddlinstitution,
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
        private void SaveData(string stringTYPE)
        {
            DataRow objdatarow = null;
            int interrorcount = 0;
            int intTotalRecord = 0;
            DataSet objDatasetResult = null;
            DataSet objDatasetResult1 = null;
            string[] stringOutputResult = null;
            string stringformid = "FA0036R1V1";
            string stringOrderBy = "";
            int intFromRecord = 0;
            int intToRecord = int.MaxValue;
            string stringErrorMsg = "";
            bool boolrecexixts = true;
            string stringbeid = CommonFunctions.GETBussinessEntity();
            string stringServiceType = "";
            string stringexp = "";
            string stringexp0123 = "";
            string stringServiceType1 = "";
            int intorderid = 0;
            try
            {
                if (CommonFunctions.ValidateASPControls(GetThisSCreenControls("TAB1"), out stringErrorMsg))
                {
                    stringServiceType = "DEFAULT";
                    stringexp = "";
                    objDatasetResult = CommonFunctions.SelectionServiceClient(stringServiceType, stringformid, stringexp, stringOrderBy, intFromRecord, intToRecord, out intTotalRecord, out interrorcount, out stringOutputResult);
                    if (interrorcount == 0)
                    {
                        if (objDatasetResult != null && objDatasetResult.Tables["t1"] != null && objDatasetResult.Tables["t1"].Rows.Count == 0)
                        {
                            if (Session["FA0036R1V1_id"] == null)
                            {
                                stringexp0123 = " And spec.ID= '" + txtspecialtyid.Text.Trim().ToUpper() + "'";
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
                                objdatarow = objDatasetResult.Tables["t1"].NewRow();
                                if (ddlinstitution.SelectedItem != null && ddlinstitution.SelectedValue.ToString().Length > 0)
                                {
                                    objdatarow["be_id"] = ddlinstitution.SelectedValue.ToString().ToUpper();
                                } 
                                objdatarow["ID"] = txtspecialtyid.Text.Trim().ToUpper(); 

                                objdatarow["SHORT_NAME"] = txtspecdesc.Text.Trim().ToUpper();
                                intorderid = 9999;
                                if (txtorderid.Text.Trim().Length > 0)
                                {
                                    int.TryParse(txtorderid.Text.Trim(), out intorderid);
                                }
                                objdatarow["ORDER_ID"] = intorderid;
                                //objdatarow["group_name"] = txtGroupName.Text.Trim();

                                //objdatarow["remarks"] = txtRemarks.Text.Trim();//Remarks                           
                                objdatarow["delmark"] = chkDelMark.Checked ? "Y" : "N";

                                CommonFunctions.AssignAuditLogDetails(ref objdatarow);
                                objDatasetResult.Tables["t1"].Rows.Add(objdatarow);

                                if (Session["FA0036R1V1_id"] != null && stringTYPE == "DELETE")
                                {
                                    objDatasetResult.AcceptChanges();
                                    objDatasetResult.Tables["t1"].Rows[0].Delete();
                                }
                                else if (Session["FA0036R1V1_id"] != null && stringTYPE == "INSERT")
                                {
                                    objDatasetResult.AcceptChanges();
                                    objDatasetResult.Tables["t1"].Rows[0]["delmark"] = chkDelMark.Checked ? "Y" : "N";
                                }
                                objDatasetResult.Tables["t1"].Rows[0].RowState.ToString();

                                objDatasetResult = objDatasetResult.GetChanges();
                                stringServiceType = "OperationServiceDML";
                                objDatasetResult = CommonFunctions.DataManipulationR1V1(stringServiceType, objDatasetResult.GetChanges(), stringformid, out int intErrorCount, out stringOutputResult);

                                if (intErrorCount == 0)
                                {
                                    if (Session["FA0036R1V1_id"] != null && stringTYPE == "DELETE")
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Record deleted successfully");
                                    }
                                    else if (Session["FA0036R1V1_id"] != null && stringTYPE == "INSERT")
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Record Updated Successfully");
                                    }
                                    else
                                    {
                                        CommonFunctions.ShowMessageboot(this, "Record Saved Successfully");
                                    }

                                  //  Clearvalues();
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
                interrorcount = 0;
                intTotalRecord = 0;
                objDatasetResult = null;
                stringOutputResult = null;
                stringformid = null;
                stringOrderBy = null;
                intFromRecord = 0;
                intToRecord = 0;

            }
        }

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            SaveData("DELETE");
        }

        protected void imgbtnprint_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgbtnAudit_Click(object sender, ImageClickEventArgs e)
        {

            DataRow[] objDataRow = null;
            try
            {
                if (Session["AuditLogFA0036R1V1"] != null)
                {
                    objDataRow = (DataRow[])Session["AuditLogFA0036R1V1"];
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
    }
}