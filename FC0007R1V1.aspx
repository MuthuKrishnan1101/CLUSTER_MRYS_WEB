<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FC0007R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FC0007R1V1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Bootstrap 5.2.0 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css?v=1.0" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Customize Bootstrap -->
    <link rel="stylesheet" href="CSS/bootstrap.css" type="text/css" />

    <!-- gridviewstyle -->
    <link rel="stylesheet" href="CSS/gridviewstyle.css" />

     <link rel="stylesheet" href="CSS/Registration.css" />

    <script src="Scripts/Validation.js" type="text/javascript"></script>
  
    <!-- Three Dot Button Css start -->
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
                <div class="ToolBarcard">
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:HiddenField ID="hdfmramount" runat="server" />
                        <asp:HiddenField ID="hdfddlBlockBill" runat="server" />
                        <asp:HiddenField ID="hdfddlWApp" runat="server" />
                        <asp:HiddenField ID="hdfddlWApproved" runat="server" />
                        <asp:HiddenField ID="hdfmrreporttypeamount" runat="server" />
                        <asp:HiddenField ID="hdfRecallcurreentStatus" runat="server" />
                        <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">
                            <asp:ImageButton ID="imgbtnNew" runat="server" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                                ToolTip="Add New" CssClass="MenuImageButton" OnClick="imgbtnNew_Click" />

                            <asp:ImageButton ID="imgbtnprint" runat="server" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                                ToolTip="Show all records" CssClass="MenuImageButton" />

                            <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                                ToolTip="Save" CssClass="MenuImageButton" OnClick="imgbtnSave_Click" />

                           

                            <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                                ToolTip="Delete" CssClass="MenuImageButton" OnClick="imgbtnDelete_Click" />

                            <asp:ImageButton ID="imgbtnAudit" runat="server" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                                ToolTip="Export" CssClass="MenuImageButton" OnClick="imgbtnAudit_Click" />

                            <asp:ImageButton ID="imgbtnexport" runat="server" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                                ToolTip="Audit Log" CssClass="MenuImageButton" Enabled="false" />

                           <%-- <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                                ToolTip="Information" CssClass="MenuImageButton" Enabled="false" />--%>

                            <div class="dropdown" style="float: right;">
                                <button class="dropbtn">
                                    <img src="Images/menu.png" width="35" height="35" /></button>
                                <div class="dropdown-content">

                                    <asp:LinkButton ID="lnkbtnpending" runat="server" CssClass="threedot" Text="Pending Items" OnClick="lnkbtnpending_Click"></asp:LinkButton>

                                    <asp:LinkButton ID="lnkbtnAppoint" runat="server" CssClass="threedot" Text="Appointment" OnClick="lnkbtnAppoint_Click"></asp:LinkButton>

                                    <asp:LinkButton ID="lnkbtnEnquiry" runat="server" CssClass="threedot" Text="Enquiry" OnClick="lnkbtnEnquiry_Click"></asp:LinkButton>

                                    <asp:LinkButton ID="lnkbtnpayment" runat="server" CssClass="threedot" Text="Payment" OnClick="lnkbtnpayment_Click"></asp:LinkButton>

                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="threedot" Text="Cancellation" OnClick="lnkbtnCancel_Click"></asp:LinkButton>


                                    <asp:LinkButton ID="lnkbtnViewMedical" runat="server" CssClass="threedot" Text="View Medical Report" OnClick="lnkbtnViewMedical_Click"></asp:LinkButton>

                                    <asp:LinkButton ID="lnkbtnrecalhistory" runat="server" CssClass="threedot" Text="Recall & DelayReason History" OnClick="lnkbtnrecalhistory_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
           </div>
              <div class="card">
            <!-- Process Button  Code Start -->
            <asp:Panel ID="pnlprocess" runat="server">
                <div class="box ProcessBox box-solid">
                    <div class="Responsive">
                        <div class="box-body">
                            <div class="row">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div class="dropdown">
                                                <asp:Button runat="server" Enabled="false" Text="MR Created" CssClass="btnprocess1 dropdown-toggle"></asp:Button>
                                            </div>
                                        </td>
                                        <asp:Panel ID="pnlPendingTracing" runat="server">
                                            <td>
                                                <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />
                                                <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                            </td>
                                            <td>

                                                <div class="dropdown">
                                                    <asp:Button runat="server" ID="btnPendingTracing" Text="Pending Tracing" CssClass="btnprocess dropdown-toggle" data-toggle="dropdown"></asp:Button>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnProcessCompletedTraccing" ToolTip="Pending Tracing" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnInsertDelayReason" runat="server" ToolTip="Pending Tracing" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                    </ul>
                                                    <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonTracing" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonTracing" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                </div>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPendingDespatch" runat="server">
                                            <td>
                                                <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />
                                                <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                            </td>
                                            <td>
                                                <div class="dropdown">
                                                    <asp:Button runat="server" ID="btnPendingDespatch" Text="Pending Despatch" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                    <ul class="dropdown-menu">

                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnProcessCompletedPendingDespatch" ToolTip="Pending Despatch" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnInsertDelayReason2" runat="server" ToolTip="Pending Despatch" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnRecallRequest2" runat="server" ToolTip="Pending Despatch" Text="Recall Request" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>
                                                    </ul>
                                                    <asp:ImageButton ID="imgbtnoverduewithoutdelayreasoDespatch" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonDespatch" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                </div>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPendingAssigned" runat="server">
                                            <td>
                                                <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />
                                                <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                            </td>
                                            <td>
                                                <div class="dropdown">

                                                    <asp:Button runat="server" ID="btnPendingAssigned" Text="Pending Assigned" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnProcessCompletedPendingAssigned" runat="server" ToolTip="Pending Assigned" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnInsertDelayReason3" runat="server" ToolTip="Pending Assigned" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnRecallRequest3" runat="server" ToolTip="Pending Assigned" Text="Recall Request" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>
                                                    </ul>
                                                    <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonPendingAssigned" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonPendingAssigned" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                </div>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPendingReport" runat="server">
                                            <td>
                                                <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />
                                                <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                            </td>
                                            <td>
                                                <div class="dropdown">

                                                    <asp:Button runat="server" ID="btnPendingReport" Text="Pending Report" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnProcessCompletedPendingReport" runat="server" ToolTip="Pending Report" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnInsertDelayReason4" runat="server" Text="Insert Delay Reason" ToolTip="Pending Report" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnRecallRequest4" runat="server" Text="Recall Request" ToolTip="Pending Report" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>
                                                    </ul>
                                                    <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonPendingReport" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonReport" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                </div>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPendingReleasetoHIMS" runat="server">
                                            <td>
                                                <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />
                                                <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                            </td>
                                            <td>
                                                <div class="dropdown">

                                                    <asp:Button runat="server" ID="btnPendingReleasetoHIMS" Text="Pending Release to HIMS" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnProcessCompletedPendingReleasetoHIMS" ToolTip="Pending Release to HIMS" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnInsertDelayReason5" runat="server" ToolTip="Pending Release to HIMS" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnRecallRequest5" runat="server" ToolTip="Pending Release to HIMS" Text="Recall Request" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>
                                                    </ul>
                                                    <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonReleasetoHIMS" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages1" Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonReleasetoHIMS" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages1" Enabled="false" />
                                                </div>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlSupervisorVetting" runat="server">
                                            <td>
                                                <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />
                                                <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                            </td>
                                            <td>
                                                <div class="dropdown">
                                                    <asp:Button runat="server" ID="btnPendingSupVetting" Text="Pending Supervisor Vetting" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnProcessCompletedPendingSupVetting" ToolTip="Pending Sup Vetting" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnInsertDelayReason6" runat="server" ToolTip="Pending Sup Vetting" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnRecallRequest6" runat="server" ToolTip="Pending Sup Vetting" Text="Recall Request" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>
                                                    </ul>
                                                    <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonSupVetting" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages1" Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonSupVetting" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages1" Enabled="false" />
                                                </div>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlforwarding" runat="server">
                                            <td>
                                                <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />
                                                <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                            </td>
                                            <td>


                                                <div class="dropdown">

                                                    <asp:Button runat="server" ID="btnPendingforwarding" Text="Pending forwarding" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                    <ul class="dropdown-menu pull-right">
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnProcessCompletedPendingforwarding" ToolTip="Pending forwarding" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnInsertDelayReason7" runat="server" ToolTip="Pending forwarding" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnRecallRequest7" runat="server" ToolTip="Pending forwarding" Text="Recall Request" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>
                                                    </ul>
                                                    <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonforwarding" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonforwarding" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                </div>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPendingCollectInPerson" runat="server">
                                            <td>
                                                <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />
                                                <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                            </td>
                                            <td>

                                                <div class="dropdown">

                                                    <asp:Button runat="server" ID="btnPendingCollectInPerson" Text="Pending Collect In Person" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                    <ul class="dropdown-menu pull-right">
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnProcessCompletedPendingCollectInPerson" ToolTip="Pending Collect In Person" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                        <li><a href="#">
                                                            <asp:LinkButton ID="lnkbtnInsertDelayReason8" runat="server" ToolTip="Pending Collect In Person" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                    </ul>
                                                    <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonCollectInPerson" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonCollectInPerson" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages" Enabled="false" />
                                                </div>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlForwarded" runat="server" Visible="false">
                                            <td>
                                                <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />
                                                <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                            </td>
                                            <td>
                                                <div class="dropdown">
                                                    <asp:Button runat="server" ID="btnForwarded" Enabled="false" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                </div>
                                            </td>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlprocessRequiredFields" runat="server" Visible="false">
                                            <td>
                                                <span hidden>
                                                    <asp:TextBox ID="txtProcessType" runat="server"></asp:TextBox></span>
                                                <span hidden>
                                                    <asp:TextBox ID="txtBypassPenItems" runat="server"></asp:TextBox></span>

                                                <span hidden>
                                                    <asp:TextBox ID="txtBlockBill" runat="server"></asp:TextBox></span>
                                                <span hidden>
                                                    <asp:TextBox ID="txtWApproved" runat="server"></asp:TextBox></span>
                                                <span hidden>
                                                    <asp:TextBox ID="txtWApp" runat="server"></asp:TextBox></span>

                                            </td>
                                        </asp:Panel>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <!-- Process Button  Code  End -->

            <table width="100%" style="margin-top: -12px">
                <tr>
                    <td>
                        <div class="box Box box-solid">
                            <br />
                            <asp:UpdatePanel ID="updPnlPreRequest" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                                    <asp:LinkButton ID="LkBtnBack" runat="server"  CssClass="LnkbtnPatient" Text="Back to MR Registration" OnClick="LkBtnBack_Click"></asp:LinkButton>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">MR Number </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtMRNumberHEADER" ReadOnly="True" runat="server" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1"></div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Request Number </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtRequestNo" ReadOnly="True" runat="server" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1"></div>
                                                    <div class="form-inline">
                                                        <div class="col-sm-1">
                                                            <div class="form-group">
                                                                <i class="fa fa-flag" style="color: red"></i>
                                                                <span>
                                                                    <asp:CheckBox runat="server" ID="chkpriorityflag" ToolTip="Priority Flag" class="flagchk" Enabled="false" CssClass="ReadOnly form-control" BorderStyle="none" /></span>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">MR Status</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtMRStatus" runat="server" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1"></div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Writing and Verifying Status </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWritingandVerifyingStatus" runat="server" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>

            <div class="box Box box-solid" style="margin-top: -12px">
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2 control-label">MRN </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtHRN" runat="server" ReadOnly="true" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1"></div>
                                <label for="inputEmail3" class="col-sm-2 control-label">Patient Name </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPatName" ReadOnly="true" ToolTip="Patient Name" runat="server" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2 control-label">Report Type </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtRptType" ReadOnly="true" runat="server" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1"></div>
                                <label for="inputEmail3" class="col-sm-2 control-label">Received Date </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtRecDate" ReadOnly="true" runat="server" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2 control-label">Requestor </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtReq" ReadOnly="true" runat="server" ToolTip="Requestor" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2 control-label">Remarks Templates </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlRemarks" AutoPostBack="true" ToolTip="Remarks Templates" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-1"></div>
                                <label for="inputEmail3" class="col-sm-2 control-label">Target Audience </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlTarget" AutoPostBack="true" ToolTip="Target Audience" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2 control-label">Description </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" ToolTip="Description" CssClass=" MultiLine_Textbox  form-control" style="height: 100px;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1"></div>
                                <label for="inputEmail3" class="col-sm-2 control-label">Remark Date </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtRemarkdte" runat="server" ToolTip="Remark Date" CssClass=" form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
              
                <table width="98%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnadd" runat="server" CssClass="btn btngreen" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px; margin-top: -20px" Text="ADD" OnClick="btnadd_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="pnlresultgrid" runat="server">
                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                        <div class="box-header with-border TotalRecord">
                            <div class="box-title">
                                <label class="text-right" style="color: white">Total Record : <span id="lblTotalRecords" runat="server"></span></label>
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <div class="form">
                                <asp:HiddenField ID="hdnVisiblity" runat="server" />
                                <div class="table-responsive table--no-card m-b-30" id="divid">
                                    <asp:GridView ID="gvList" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                        AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                                        CellPadding="2"
                                        ForeColor="#333333"
                                        HorizontalAlign="Center"
                                        PageSize="10"
                                        CssClass="table table-borderless table-striped table-responsive">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsno" runat="server" Text="<%#Bind('REFERENCE_1')%>"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="2%" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnRemarkID" runat="server" Text="<%#Bind('SHORT_NAME')%>" CommandArgument='<%#Eval("REFERENCE_1")+","+ Eval("Request_ID")+","+ Eval("REGRMK_ID")%>' OnClick="lnkbtnRemarkID_Click" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnEnq_Date1" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Remarks Templates" Font-Names="Arial"  ForeColor="White"
                                                                    CommandArgument="REGRMK_ID"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemStyle Width="2%" HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                           
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblremarks" runat="server" Text="<%#Bind('remarks')%>" TextMode="MultiLine" Style="resize: none; height: 50px" ReadOnly="true" Width="100%" CssClass="MultiLine_Textbox"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:Label ID="lnkbtnname" runat="server" Text="Description" Width="350px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemStyle Width="2%" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Target Audience">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReportPendingItemID" runat="server" CssClass="styleCellsLeft" Text='<%# Eval("TARG_AUD") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="styleCellsHeaderLOVWnd text-left" />
                                                <ItemStyle HorizontalAlign="left" Width="1%" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Remark Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarkDate" runat="server" CssClass="styleCellsLeft" Text='<%#Bind("REMARKS_DATE", "{0:dd-MM-yyyy}")%>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="styleCellsHeaderLOVWnd text-left" />
                                                <ItemStyle HorizontalAlign="left" Width="2%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDelete_Click" CommandArgument='<%#Eval("REFERENCE_1")+","+ Eval("Request_ID")+","+ Eval("REGRMK_ID")%>' />
                                                    <controlstyle cssclass="btn btnred"></controlstyle>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                        <tr>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemStyle Width="2%" HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <RowStyle CssClass="GridviewRowStyle" />
                                        <HeaderStyle CssClass="GridHeaderTextforScroll-UnText"></HeaderStyle>
                                        <RowStyle CssClass="GridviewAlternatingRowStyle" />
                                        <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-sm-12" style="text-align: center">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPage_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />
 </div>

 </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btnerror" runat="server" />
                                        <cc1:ModalPopupExtender ID="Modelpopuperror" runat="server" BackgroundCssClass="modal-background"
                                            DynamicControlID="btnerror" PopupControlID="pnlpopuperror" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                            TargetControlID="btnerror" />
                                        <asp:Panel ID="pnlpopuperror" runat="server" CssClass="ErrorPopup"
                                            EnableTheming="True" Style="text-align: center; resize: none;" Width="447px">
                                            <center>
                                                <table style="width: 98%; text-align: left">
                                                    <tr>
                                                        <td style="width: 18%"></td>
                                                        <td style="width: 70%; text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #000080;">
                                                            <asp:Label ID="lblModalTile5" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">
                                                                        <asp:Label ID="lblErrorTypeheader" runat="server" Font-Names="Tahoma" Font-Size="9pt" Text="Error Type:"></asp:Label>

                                                                    </td>
                                                                    <td style="width: 2%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">:
                                                                    </td>
                                                                    <td style="width: 70%; font-family: tahoma; font-size: 9pt;">
                                                                        <asp:Label ID="lblErrorType" runat="server" Font-Names="Tahoma" Font-Size="9pt"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:Panel ID="PanelValidateFromTo" runat="server">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">
                                                                            <asp:Label ID="lblErrorCodeheader" runat="server" Font-Names="Tahoma" Font-Size="9pt" Text="Error Code:"></asp:Label>

                                                                        </td>
                                                                        <td style="width: 2%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">:
                                                                        </td>
                                                                        <td style="width: 70%; font-family: tahoma; font-size: 9pt;">
                                                                            <asp:Label ID="lblErrorCode" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">
                                                                            <asp:Label ID="lblsysseqnoheader" runat="server" Font-Names="Tahoma" Font-Size="9pt" Text="System Sequence No:"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 2%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">:
                                                                        </td>
                                                                        <td style="width: 70%; font-family: tahoma; font-size: 9pt;">
                                                                            <asp:Label ID="lblsysseqno" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">Error Message:</td>
                                                                        <td style="width: 2%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: #000080; font-size: 9pt;">:
                                                                        </td>
                                                                        <td style="width: 70%; font-family: tahoma; resize: none; font-size: 9pt;">
                                                                            <asp:TextBox ID="txterrormsg" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                                BorderWidth="1px" Font-Names="Tahoma" Font-Size="9pt" Height="100px" MaxLength="500"
                                                                                TextMode="MultiLine" Width="99%">Message Content Sample....</asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="text-align: center">
                                                            <asp:Button ID="btnErrorpopupclose" runat="server" align="right" CssClass="SubmitButtonStyle"
                                                                Text="Close" Width="100px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>

                <!-- Update Process Popup start -->
                <asp:Panel runat="server" ID="pnlupdateremarksandprocess">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelModal6success" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btnerrorsuccess" runat="server" />
                                        <cc1:ModalPopupExtender ID="Modelpopuperrorsuccess" runat="server" BackgroundCssClass="modal-background"
                                            DynamicControlID="btnerrorsuccess" PopupControlID="pnlpopuperrorsuccess" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                            TargetControlID="btnerrorsuccess" />
                                        <asp:Panel ID="pnlpopuperrorsuccess" runat="server" BackColor="#e3f6fd"
                                            EnableTheming="True" Style="text-align: center; resize: none; align-content: flex-start; align-items: flex-start; border: 5px solid #2aa7ed; border-radius: 20px; padding: 10px;" Width="447px">
                                            <center>
                                                <table style="width: 98%; text-align: center" bgcolor="#f4eaff">
                                                    <tr>
                                                        <td style="width: 12%"></td>
                                                        <td align="center">
                                                            <asp:Label Style="text-align: center" ID="Label1" runat="server" Text="Update Process" ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 96px">
                                                            <table style="width: 100%">
                                                            </table>
                                                            <asp:Panel ID="Panel2" runat="server" Height="67px">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <label>Remarks:&nbsp;&nbsp;</label></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtProcessCompletedRemarks" MaxLength="50" runat="server" CssClass="form-control" Visible="true"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 5%"></td>
                                                                        <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt">Are you sure want to update the process?</td>
                                                                        <td style="width: 12%"></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="text-align: center">
                                                            <asp:Button ID="btnConfirmprocessStatus" runat="server" align="right" CssClass="btn btngreen"
                                                                Text="Yes" OnClick="btnConfirmprocessStatus_Click" />
                                                            <asp:Button ID="btnConfirmprocessClose" runat="server" align="right" CssClass="btn btnred"
                                                                Text="NO" OnClick="btnConfirmprocessClose_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <!-- Update Process Popup End -->

                <!-- Recall Request Popup start -->
                <asp:Panel runat="server" ID="pnlupdateRECALL">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelRecall" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="lnkbtnbtnrecall" runat="server" />
                                        <cc1:ModalPopupExtender ID="ModelpopupRecall" runat="server" BackgroundCssClass="modal-background"
                                            DynamicControlID="lnkbtnbtnrecall" PopupControlID="pnlrecall" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                            TargetControlID="lnkbtnbtnrecall" />
                                        <asp:Panel ID="pnlrecall" runat="server" BackColor="#e3f6fd"
                                            EnableTheming="True" Style="text-align: center; resize: none; align-content: flex-start; align-items: flex-start; border: 5px solid #2aa7ed; border-radius: 20px; padding: 10px;" Width="447px">
                                            <center>
                                                <table style="width: 98%; text-align: center" bgcolor="#f4eaff">
                                                    <tr>
                                                        <td style="width: 12%"></td>
                                                        <td align="center">
                                                            <asp:Label Style="text-align: center" ID="Label3" runat="server" Text="Recall Request" ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 96px">
                                                            <table style="width: 100%">
                                                            </table>
                                                            <asp:Panel ID="Panel5" runat="server" Height="67px">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <label>Recall Reason:&nbsp;&nbsp;</label></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtRecallRemarks" MaxLength="50" runat="server" TextMode="MultiLine" CssClass="MultiLine_Textbox form-control" Visible="true"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 5%"></td>
                                                                        <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt">Are you sure want to Recall the process?</td>
                                                                        <td style="width: 12%"></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="text-align: center">
                                                            <asp:Button ID="btnrecallOK" runat="server" align="right" CssClass="btn btngreen"
                                                                Text="Yes" OnClick="btnrecallOK_Click" />
                                                            <asp:Button ID="btnrecallcancel" runat="server" align="right" CssClass="btn btnred"
                                                                Text="NO" OnClick="btnrecallcancel_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <!-- Recall Request Popup End -->

                <!-- Delay Reason Popup start -->
                <asp:Panel runat="server" ID="pnlupdateDelayReason">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelDelayReason" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="lnkbtnbtnDelayReason" runat="server" />
                                        <cc1:ModalPopupExtender ID="ModelpopupDelayReason" runat="server" BackgroundCssClass="modal-background"
                                            DynamicControlID="lnkbtnbtnDelayReason" PopupControlID="pnlDelayReason" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                            TargetControlID="lnkbtnbtnDelayReason" />
                                        <asp:Panel ID="pnlDelayReason" runat="server" BackColor="#e3f6fd" BorderStyle="Outset"
                                            EnableTheming="True" Style="text-align: center; resize: none; align-content: flex-start; align-items: flex-start; border: 5px solid #2aa7ed; border-radius: 20px; padding: 10px;" Width="447px">
                                            <center>
                                                <table style="width: 98%; text-align: center" bgcolor="#f4eaff">
                                                    <tr>
                                                        <td style="width: 12%"></td>
                                                        <td align="center">
                                                            <asp:Label Style="text-align: center" ID="Label4" runat="server" Text="Insert Delay Reason" ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 96px">
                                                            <table style="width: 100%">
                                                            </table>
                                                            <asp:Panel ID="Panel6" runat="server" Height="67px">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <label>Delay Reason:&nbsp;&nbsp;</label></td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlDelayReason" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 5%"></td>
                                                                        <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt">Are you sure want to Insert Delay Reason?</td>
                                                                        <td style="width: 12%"></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="text-align: center">
                                                            <asp:Button ID="btnDelayReasonOK" runat="server" align="right" CssClass="btn btngreen"
                                                                Text="Yes" OnClick="btnDelayReasonOK_Click" />
                                                            <asp:Button ID="btnDelayReasoncancel" runat="server" align="right" CssClass="btn btnred"
                                                                Text="NO" OnClick="btnDelayReasoncancel_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <!-- Delay Reason Popup End -->

           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
