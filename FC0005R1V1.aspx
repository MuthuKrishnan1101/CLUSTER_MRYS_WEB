<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FC0005R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FC0005R1V1" %>

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
    <!-- Three Dot Button Css End -->

    <!-- Process Button Css Code Start -->
   
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
                            <asp:ImageButton ID="imgbtnNew" runat="server" CssClass="MenuImageButton"  ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                                 ToolTip="Add New" OnClick="imgbtnNew_Click" />

                            <asp:ImageButton ID="imgbtnprint" runat="server" CssClass="MenuImageButton"  ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                                ToolTip="Show all records" />

                            <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton"  ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                                 ToolTip="Save" OnClick="imgbtnSave_Click" />

                            

                            <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="MenuImageButton"  ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                                 ToolTip="Delete" />

                            <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton"  ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                                 ToolTip="Export" />

                            <asp:ImageButton ID="imgbtnexport" runat="server" CssClass="MenuImageButton"  ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                                 ToolTip="Audit Log" Enabled="false" />

                            <asp:ImageButton ID="ImageButton1" runat="server" CssClass="MenuImageButton"  ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                                 ToolTip="Information" Enabled="false" />

                             <div class="dropdown" style="float: right;">
                            <button class="dropbtn">
                                <img src="Images/menu.png" width="35" height="35" /></button>
                            <div class="dropdown-content">
                               
                                    <asp:LinkButton ID="lnkbtnpending" runat="server" CssClass="threedot" Text="Pending Items"   OnClick="lnkbtnpending_Click"></asp:LinkButton> 
                               
                                    <asp:LinkButton ID="lnkbtnAppoint" runat="server" CssClass="threedot" Text="Appointment" OnClick="lnkbtnAppoint_Click"></asp:LinkButton>  
                                   
                                    <asp:LinkButton ID="lnkbtnpayment" runat="server" CssClass="threedot" Text="Payment"  OnClick="lnkbtnpayment_Click"></asp:LinkButton>  
                               
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="threedot" Text="Cancellation"  OnClick="lnkbtnCancel_Click"></asp:LinkButton>  
                                
                                    <asp:LinkButton ID="lnkbtnRemark" runat="server" CssClass="threedot" Text="Remark"  OnClick="lnkbtnRemark_Click"></asp:LinkButton>  
                               
                                    <asp:LinkButton ID="lnkbtnViewMedical" runat="server" CssClass="threedot" Text="View Medical Report"  OnClick="lnkbtnViewMedical_Click"></asp:LinkButton>    
                                
                                    <asp:LinkButton ID="lnkbtnrecalhistory" runat="server" CssClass="threedot" Text="Recall & DelayReason History"  OnClick="lnkbtnrecalhistory_Click"></asp:LinkButton>  
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
                                                <asp:Button runat="server" Enabled="false" Text="MR Created" CssClass="btnprocess1 dropdown-toggle" ></asp:Button>
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
                                                        ToolTip="New Record" CssClass="Overdueimages"  Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonTracing" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages"  Enabled="false"/>
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
                                                        ToolTip="New Record" CssClass="Overdueimages"  Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonDespatch" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages"  Enabled="false"/>
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
                                                        ToolTip="New Record" CssClass="Overdueimages"  Enabled="false"/>
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonPendingAssigned" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages"  Enabled="false" />
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
                                                        ToolTip="New Record" CssClass="Overdueimages"  Enabled="false"/>
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
                                                        ToolTip="New Record" CssClass="Overdueimages1"  Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonReleasetoHIMS" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages1"  Enabled="false"  />
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
                                                        ToolTip="New Record" CssClass="Overdueimages1"  Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonSupVetting" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages1"   Enabled="false"/>
                                                </div>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlforwarding" runat="server">
                                            <td>
                                                <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />
                                               <%-- <i class="fa fa-long-arrow-right symbol"></i>--%>
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
                                                        ToolTip="New Record" CssClass="Overdueimages"  Enabled="false" />
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonforwarding" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages"  Enabled="false" />
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
                                                        ToolTip="New Record" CssClass="Overdueimages"   Enabled="false"/>
                                                    <asp:ImageButton ID="imgbtnoverduewithdelayreasonCollectInPerson" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                        ToolTip="New Record" CssClass="Overdueimages"  Enabled="false" />
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
                                                    <div class="dropdownlbl">
                                                             <asp:Label ID="lblForwardStatus" runat="server" />
                                                    </div>
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
                        <div class="box box-solid Box">
                            <br />
                            <asp:UpdatePanel ID="updPnlPreRequest" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="100%">

                                        <tr>
                                            <td>
                                                <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                                    <asp:LinkButton ID="LkBtnBack" runat="server"  CssClass="LnkbtnPatient"  Text="Back to MR Registration" OnClick="LkBtnBack_Click"></asp:LinkButton>
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
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Request Number</label>
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
                                                    <label for="inputEmail3" class="col-sm-2 control-label">MR Status </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtMRStatus" runat="server" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                     <div class="col-sm-1"></div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Writing and Verifying Status</label>
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

            <div class="box box-solid Box" style="margin-top: -12px">
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
                                        <asp:TextBox ID="txtPatName" runat="server" ReadOnly="true" CssClass="ReadOnly form-control"></asp:TextBox>
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
                                        <asp:TextBox ID="txtRptType" runat="server" ReadOnly="true" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-1"></div>
                                <label for="inputEmail3" class="col-sm-2 control-label">Received Date </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtRecDate" runat="server" ReadOnly="true" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2 control-label">Due Date </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtDueDate" runat="server" ReadOnly="true" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-1"></div>
                                <label for="inputEmail3" class="col-sm-2 control-label">Requestor Contact </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtReqContact" runat="server" ReadOnly="true" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2 control-label">MR Created By </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtCreateBy" runat="server" ReadOnly="true" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-1"></div>
                                <label for="inputEmail3" class="col-sm-2 control-label">Contact Person </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtReqName" runat="server" ReadOnly="true" CssClass="ReadOnly form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2 control-label">Caller's Name </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtCName" runat="server" CssClass=" form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-1"></div>
                                <label for="inputEmail3" class="col-sm-2 control-label">Caller's Enquiry </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtCEnq" runat="server" TextMode="MultiLine" Style="resize: none" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2 control-label">Staff's Name </label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSName" runat="server" CssClass=" form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-1"></div>
                                <label for="inputEmail3" class="col-sm-2 control-label">Staff Response</label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSRes" runat="server" TextMode="MultiLine" Style="resize: none;" CssClass=" form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-2 control-label">Enquiry Status</label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlEnqStatus" AutoPostBack="true" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-sm-1"></div>
                                <label for="inputEmail3" class="col-sm-2 control-label">Remarks</label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Style="resize: none;" CssClass=" form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
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
                                        CssClass="table table-borderless table-striped">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnEnq_Date" runat="server" Text='<%#Bind("Enq_Date","{0:dd-MM-yyy HH:mm}")%>' CommandArgument='<%#Bind("mr_enq_id")%>' OnClick="lnkbtnEnq_Date_Click">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnEnq_Date1" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Enquiry Date" Font-Names="Arial"  ForeColor="White"
                                                                    CommandArgument="Enq_Date"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemStyle Width="1%" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>

                                                    <asp:Label ID="lblCallers_Name" runat="server" Text="<%#Bind('Callers_Name')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnCallers_Name" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Caller's Name" Font-Names="Arial"  ForeColor="White"
                                                                    CommandArgument="Callers_Name"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemStyle Width="5%" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCallers_Enquiry" runat="server" Text="<%#Bind('Callers_Enquiry')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnCallers_Enquiry" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Enquiry" Font-Names="Arial"  ForeColor="White"
                                                                    CommandArgument="Callers_Enquiry"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemStyle Width="6%" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>

                                                    <asp:Label ID="lblStaffs_Name" runat="server" Text="<%#Bind('Staffs_Name')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd"">
                                                                <asp:LinkButton ID="lnkbtnStaffs_Name" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Staff's Name" Font-Names="Arial"  ForeColor="White"
                                                                    CommandArgument="Staffs_Name"></asp:LinkButton>
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

                                                    <asp:Label ID="lblStaffs_Response" runat="server" Text="<%#Bind('Staffs_Response')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnStaffs_Response" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Response" Font-Names="Arial"  ForeColor="White"
                                                                    CommandArgument="BE_ID"></asp:LinkButton>
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

                                                    <asp:Label ID="lblreference_1" runat="server" Text="<%#Bind('reference_1')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnreference_1" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Status" Font-Names="Arial"  ForeColor="White"
                                                                    CommandArgument="reference_1"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemStyle Width="2%" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                        <RowStyle CssClass="GridviewRowStyle" />
                                        <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
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

            <asp:Panel runat="server" ID="pnlreportpopup" Visible="false">
                <asp:LinkButton ID="lnkbtnReportdetail" runat="server" />
                                    <cc1:ModalPopupExtender ID="mdlrbt" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="lnkbtnReportdetail" PopupControlID="pnlreports" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="lnkbtnReportdetail" />

                <asp:Panel ID="pnlreports" runat="server" Width="950">
                    <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                        <div class="modal-dialog modal-lg panelBiodata" style="width: 70%;">
                            <div class="modal-content PopupModelContent">
                                <div class="text-center" style="background-color: #304863;">
                                    <h4 class="modal-title" style="font-weight: bold; font-size: 16px; color: white"></h4>
                                </div>
                                <div class="modal-body" style="border-radius: 20px;">
                                    <div class="nav-tabs-custom">
                                        <div class="box box-primary box-solid">
                                            <div class="box-header Popboxheader" style="">
                                                <ul class="nav nav-tabs">
                                                    <li class="" style="background-color: #e3f6fd; border-radius: 5px;"><a href="#tabPrd_1" data-toggle="tab"
                                                        style="font-weight: 500; font-size: 15px; color: black; bordproductr: rgba(221, 221, 221, 0.5490196078431373);">
                                                        <asp:Label runat="server" ID="lblprint" Text="Report"></asp:Label>
                                                    </a></li>
                                                    <div style="text-align: right">
                                                        <asp:ImageButton ID="ImageButton5" runat="server" Height="38px" ImageUrl="Images/PopColse.png"
                                                            Width="38px" ToolTip="Close" OnClick="btnclose_Click" />
                                                    </div>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="accordion-group">
                                        <div class="accordion-heading">
                                            <span class="accordion-toggle" data-toggle="collapse" data-parent="toggle" href="#collapseTwo">
                                                <i class="fa fa-minus"></i>&nbsp;Remarks for Reminder Letter
                                            </span>
                                        </div>
                                        <div id="collapseTwo" class="accordion-body collapse in">
                                            <div class="accordion-inner">
                                                <br />
                                                        <div class="box-body">
                                                 <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label"> 1st Reminder Printed On </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtFirstRemPrintedOn" runat="server" Enabled="false" CssClass="form-control ReadOnly"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                      <label for="inputEmail3" class="col-sm-2 control-label"> 2nd Reminder Printed On </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSecRemPrintedOn" runat="server" Enabled="false" CssClass="form-control ReadOnly"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                              <br />
                                                              <br />
                                                           </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="accordion-group">
                                        <div class="accordion-heading">
                                            <span class="accordion-toggle" data-toggle="collapse" data-parent="toggle" href="#collapseThree">
                                                <i class="fa fa-minus"></i>&nbsp;Letter Report
                                            </span>
                                        </div>
                                        <div id="collapseThree" class="accordion-body collapse in">
                                            <div class="accordion-inner">
                                                <asp:Panel ID="Panel4" Width="100%" runat="server" Style="margin-left: 20px;">
                                                    <div class="form-horizontal">
                                                        <div class="box-body">
                                                            <div class="form-group">
                                                                   <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnacknoeledge" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                         <label for="inputEmail3" class="control-label">Acknowledgement Form </label>
                                                                    </div>
                                                                </div>
                                                               
                                                                 <div class="col-sm-3"></div>
                                                                 <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtncancellation" Enabled="false" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Cancellation Request</label>
                                                                    </div>
                                                                </div>

                                                               
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnconsent" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Consent Form</label>
                                                                    </div>
                                                                </div>

                                                                 <div class="col-sm-3"></div>
                                                                  <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtncoverletter" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Cover Letter  </label>
                                                                    </div>
                                                                </div>
                                                                
                                                            </div>

                                                            <div class="form-group">

                                                                 <asp:Panel ID="pnldefaultclinicreview" runat="server">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtndefaultclinicreview"  Enabled="false"  runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Default for Clinic Review</label>
                                                                    </div>
                                                                </div>
                                                                    </asp:Panel>
                                                               
                                                                 <div class="col-sm-3"></div>
                                                                 <asp:Panel ID="pnldefaultclinicreviewfinal" runat="server">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtndefaultclinicfinal"  Enabled="false"  runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Default for Clinic Review-Final</label>
                                                                    </div>
                                                                </div>
                                                                    </asp:Panel>
                                                              

                                                            </div>
                                                            <div class="form-group">

                                                                <asp:Panel ID="pnldefaultmrassessment" runat="server">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnmrassesment"  Enabled="false" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                         <label for="inputEmail3" class="control-label">Default for MR Assessment </label>
                                                                    </div>
                                                                </div>
                                                                   </asp:Panel>
                                                                 <div class="col-sm-3"></div>

                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnEnvelopletter" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                         <label for="inputEmail3" class="control-label">Envelope Letter </label>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                 <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnhospitalization" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                         <label for="inputEmail3" class="control-label">Hospitalisation Letter  </label>
                                                                    </div>
                                                                </div>
                                                                 <div class="col-sm-3"></div>
                                                                 <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnmedicalreport" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                       <label for="inputEmail3" class="control-label">Medical Report Processing Form </label>
                                                                    </div>
                                                                </div>
                                                                  
                                                               
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnnorecord" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">No Record Letter</label>
                                                                    </div>
                                                                </div>
                                                                 <div class="col-sm-3"></div>
                                                                 <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnnotreportassenment" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Not Ready for Assessment</label>
                                                                    </div>
                                                                </div>
                                                               
                                                            </div>
                                                            <div class="form-group">

                                                                 <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnoutstanding" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Outstanding Status</label>
                                                                    </div>
                                                                </div>
                                                               
                                                                 <div class="col-sm-3"></div>
                                                                 <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnpaymentAcknowlege" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Payment Acknowledgement </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                         <asp:RadioButton ID="rbtnpendingitemfirst" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                       <label for="inputEmail3" class="control-label">Pending Item First-Reminder</label>
                                                                    </div>
                                                                </div>
                                                                 <div class="col-sm-3"></div>
                                                                 <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnpendingitemfinal" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                         <label for="inputEmail3" class="control-label">Pending Item Final-Reminder</label>
                                                                    </div>
                                                                </div>
                                                             
                                                                
                                                            </div>
                                                            <div class="form-group">

                                                                 <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnsplistreport" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                         <label for="inputEmail3" class="control-label">Specialist Report  </label>
                                                                    </div>
                                                                </div>
                                                                 <div class="col-sm-3"></div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnsimplemedicalreort" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Simple Medical Report Template  </label>
                                                                    </div>
                                                                </div>
                                                              
                                                            </div>

                                                            <div class="form-group">
                                                                 <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnpartialrefundletter" Enabled="false" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Partial Refund Letter  </label>
                                                                    </div>
                                                                </div>
                                                                 <div class="col-sm-3"></div>
                                                                  <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnrefundletter" Enabled="false" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Refund Letter  </label>
                                                                    </div>
                                                                </div>
                                                               
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnnodletter" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                         <label for="inputEmail3" class="control-label">NOD Letter  </label>
                                                                    </div>
                                                                </div>
                                                                 <div class="col-sm-3"></div>
                                                                 
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnstandardnodletter" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Standard NOD Letter </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            
                                                            
                                                            <div class="form-group">
                                                                 <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnwaiverform" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Waiver Form </label>
                                                                    </div>
                                                                </div>
                                                                 <div class="col-sm-3"></div>
                                                                  <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:RadioButton ID="rbtnworkcompensation" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /> &nbsp;&nbsp;
                                                                        <label for="inputEmail3" class="control-label">Workmen Compensation Status</label>
                                                                    </div>
                                                                </div>
                                                              
                                                               
                                                            </div>
                                                          
                                                            <br />

                                                            <div class="form-group">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>&nbsp;&nbsp;&nbsp;</td>
                                                                      <td>
                                                                           <label>Address to print out :</label>
                                                                      </td>
                                                                        <td>
                                                                             <asp:RadioButton ID="rbtnpadientadd" runat="server" CssClass="form-control" BorderStyle="none" GroupName="Reportpatient" Width="38" />
                                                                        </td>
                                                                        <td>
                                                                           <label>Patient Address</label>
                                                                        </td>
                                                                          <td>&nbsp;&nbsp;</td>
                                                                          <td>&nbsp;&nbsp;</td>
                                                                          <td>&nbsp;&nbsp;</td>
                                                                        <td>
                                                                            <asp:RadioButton ID="rbtnrequestor" runat="server" CssClass="form-control" BorderStyle="none" GroupName="Reportpatient" Width="38" />
                                                                            
                                                                        </td>
                                                                        <td>
                                                                            <label>Requestor Address </label>
                                                                        </td>
                                                                          <td>&nbsp;&nbsp;</td>
                                                                          <td>&nbsp;&nbsp;</td>
                                                                          <td>&nbsp;&nbsp;&nbsp;</td>
                                                                        <td>&nbsp;&nbsp;&nbsp;</td>
                                                                        <td>
                                                                           <asp:RadioButton ID="rbtnrequestormail" runat="server" CssClass="form-control" BorderStyle="none" GroupName="Reportpatient" Width="38" />
                                                                        </td>
                                                                        <td>
                                                                             <label>Requestor Mailing Address </label>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            
                                                            <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                                                <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-primary" Text="Generate" OnClientClick="showLoader()" OnClick="btnGenerate_Click" />
                                                                <asp:Button ID="btnclose" runat="server" CssClass="btn btnred" Text="Close" OnClick="btnclose_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <!-- Report  Popup End -->


          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
