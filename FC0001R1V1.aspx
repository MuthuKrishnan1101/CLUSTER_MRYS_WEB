<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FC0001R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FC0001R1V1"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Bootstrap 5.2.0 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css?v=1.0" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Customize Bootstrap -->
    <link rel="stylesheet" href="CSS/bootstrap.css" type="text/css" />

    <!-- Popup -->
    <link href="CSS/design.css" rel="stylesheet" type="text/css" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Ionicons -->
    <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css" />

    <!-- gridviewstyle -->
    <link rel="stylesheet" href="CSS/gridviewstyle.css" />
    <link rel="stylesheet" href="CSS/Registration.css" />

    <!-- javascript -->
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="Scripts/Validation.js" type="text/javascript"></script>

    

    <%-- <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>--%>
    <%-- <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script src="Scripts/Validation.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="ToolBarcard">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:PlaceHolder runat="server" ID="ScriptPlaceholder"></asp:PlaceHolder>

                            <asp:FileUpload ID="FileUpload2" runat="server" Style="display: none;" />

                            <asp:HiddenField ID="hidFldPenSeq_ID" runat="server" />
                            <asp:HiddenField ID="hdnClickEvent" runat="server" />
                            <asp:HiddenField ID="hdnPopupDropdownValue" runat="server" />
                            <asp:HiddenField ID="hdnPopuptxtboxValue" runat="server" />
                            <asp:HiddenField ID="hdnPopuptxtboxnameValue" runat="server" />
                            <asp:HiddenField ID="hdnPopupupdatepnlValue" runat="server" />
                            <asp:HiddenField ID="hdfuniqid" runat="server" />
                            <asp:HiddenField ID="hdfmramount" runat="server" />
                            <asp:HiddenField ID="hdfmrreporttypeamount" runat="server" />
                            <asp:HiddenField ID="hdfRecallcurreentStatus" runat="server" />
                            <asp:HiddenField ID="hdfEditrequestDetail" runat="server" />
                            <asp:HiddenField ID="hdfCANRefundamt" runat="server" />
                            <asp:HiddenField ID="hdfappoinmentdate" runat="server" />
                            <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">

                                <span hidden>
                                    <asp:ImageButton ID="imgbtnNew" runat="server" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                                        ToolTip="Add New" CssClass="MenuImageButton" OnClick="imgbtnNew_Click" />
                                    <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                                        ToolTip="Show all records" CssClass="MenuImageButton" Enabled="false" />
                                    <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                                        ToolTip="Delete" CssClass="MenuImageButton" Enabled="false" />
                                </span>

                                <asp:ImageButton ID="imgbtnSaveAsDraft" runat="server" ImageUrl="Images/MenuTemplatesImg/DRAFT_MENU.png"
                                    ToolTip="Draft" CssClass="MenuImageButton" OnClick="imgbtnSaveAsDraft_Click" />

                                <asp:ImageButton ID="imgbtnCONFIRM_MENU" runat="server" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                                    ToolTip="Confirm Save" CssClass="MenuImageButton" OnClick="imgbtnCONFIRM_MENU_Click" />


                                <asp:ImageButton ID="imgBtnPrint" runat="server" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                                    ToolTip="Export" CssClass="MenuImageButton" OnClick="imgBtnPrint_Click" />
                                 

                                <asp:ImageButton ID="imgbtnInstTransfer" runat="server" ImageUrl="Images/MenuTemplatesImg/TRANSFER.png"
                                    ToolTip="Transfer" CssClass="MenuImageButton" OnClick="imgbtnInstTransfer_Click" />


                                <asp:ImageButton ID="imgbtnAUDIT" runat="server" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                                    ToolTip="Audit Log" CssClass="MenuImageButton" OnClick="imgbtnAUDIT_Click" />
                                 

                                <div class="dropdown" style="float: right; text-align: left">
                                    <asp:ImageButton ID="btnthreedot" runat="server" ImageUrl="Images/menu.png"
                                        CssClass="dropbtn" />
                                    <div class="dropdown-content">
                                         

                                        <asp:LinkButton ID="lnkbtnAppoint" runat="server" CssClass="threedot" Text="Appointment" OnClick="lnkbtnAppoint_Click"></asp:LinkButton>

                                         

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
                    <div class="box box-solid ProcessBox">
                        <div class="Responsive">
                            <div class="box-body">
                                <div class="row">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td>
                                                <div class="dropdown">
                                                    <asp:Button runat="server" Enabled="false" Text="1" CssClass="btnprocess1 dropdown-toggle"></asp:Button>
                                                    <div class="dropdownlbl">
                                                        <label>MR
                                                            <br />
                                                            Created</label>
                                                    </div>
                                            </td>
                                            <asp:Panel ID="pnlPendingTracing" runat="server">
                                                <td>
                                                    <asp:Button Text="" class="middleButton1" runat="server" />
                                                    <%-- <img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />--%>
                                                    <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                                </td>
                                                <td>

                                                    <div class="dropdown">
                                                        <asp:Button runat="server" ID="btnPendingTracing" Text="2" CssClass="btnprocess dropdown-toggle" data-toggle="dropdown"></asp:Button>
                                                        <ul class="dropdown-menu">
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnProcessCompletedTraccing" ToolTip="Pending Tracing" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnInsertDelayReason" runat="server" ToolTip="Pending Tracing" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                        </ul>
                                                        <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonTracing" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <asp:ImageButton ID="imgbtnoverduewithdelayreasonTracing" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <div class="dropdownlbl">
                                                            <label>Pending
                                                                <br />
                                                                Tracing</label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlPendingDespatch" runat="server">
                                                <td>
                                                    <asp:Button Text="" class="middleButton" runat="server" />
                                                    <%--<img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />--%>
                                                    <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                                </td>
                                                <td>
                                                    <div class="dropdown">
                                                        <asp:Button runat="server" ID="btnPendingDespatch" Text="3" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                        <ul class="dropdown-menu">

                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnProcessCompletedPendingDespatch" ToolTip="Pending Despatch" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnInsertDelayReason2" runat="server" ToolTip="Pending Despatch" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnRecallRequest2" runat="server" ToolTip="Pending Despatch" Text="Recall Request" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>
                                                        </ul>
                                                        <asp:ImageButton ID="imgbtnoverduewithoutdelayreasoDespatch" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <asp:ImageButton ID="imgbtnoverduewithdelayreasonDespatch" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <div class="dropdownlbl">
                                                            <label>Pending
                                                                <br />
                                                                Despatch</label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlPendingAssigned" runat="server">
                                                <td>
                                                    <asp:Button Text="" class="middleButton" runat="server" />
                                                    <%--<img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />--%>
                                                    <%-- <i class="fa fa-long-arrow-right symbol"></i>--%>
                                                </td>
                                                <td>
                                                    <div class="dropdown">

                                                        <asp:Button runat="server" ID="btnPendingAssigned" Text="4" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                        <ul class="dropdown-menu">
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnProcessCompletedPendingAssigned" runat="server" ToolTip="Pending Assigned" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnInsertDelayReason3" runat="server" ToolTip="Pending Assigned" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnRecallRequest3" runat="server" ToolTip="Pending Assigned" Text="Recall Request" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>
                                                        </ul>
                                                        <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonPendingAssigned" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <asp:ImageButton ID="imgbtnoverduewithdelayreasonPendingAssigned" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <div class="dropdownlbl">
                                                            <label>Pending
                                                                <br />
                                                                Assigned</label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlPendingReport" runat="server">
                                                <td>
                                                    <asp:Button Text="" class="middleButton" runat="server" />
                                                    <%--<img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />--%>
                                                    <%-- <i class="fa fa-long-arrow-right symbol"></i>--%>
                                                </td>
                                                <td>
                                                    <div class="dropdown">

                                                        <asp:Button runat="server" ID="btnPendingReport" Text="5" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                        <ul class="dropdown-menu">
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnProcessCompletedPendingReport" runat="server" ToolTip="Pending Report" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnInsertDelayReason4" runat="server" Text="Insert Delay Reason" ToolTip="Pending Report" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnRecallRequest4" runat="server" Text="Recall Request" ToolTip="Pending Report" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>
                                                        </ul>
                                                        <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonPendingReport" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <asp:ImageButton ID="imgbtnoverduewithdelayreasonReport" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <div class="dropdownlbl">
                                                            <label>Pending
                                                                <br />
                                                                Report</label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlPendingReleasetoHIMS" runat="server">
                                                <td>
                                                    <asp:Button Text="" class="middleButton" runat="server" />
                                                    <%--<img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />--%>
                                                    <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                                </td>
                                                <td>
                                                    <div class="dropdown">

                                                        <asp:Button runat="server" ID="btnPendingReleasetoHIMS" Text="6" CssClass="btnprocess2 dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                        <ul class="dropdown-menu">
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnProcessCompletedPendingReleasetoHIMS" ToolTip="Pending Release to HIMS" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnInsertDelayReason5" runat="server" ToolTip="Pending Release to HIMS" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                            <%--<li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnRecallRequest5" runat="server" ToolTip="Pending Release to HIMS" Text="Recall Request" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>--%>
                                                        </ul>
                                                        <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonReleasetoHIMS" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages1" Enabled="false" />
                                                        <asp:ImageButton ID="imgbtnoverduewithdelayreasonReleasetoHIMS" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages1" Enabled="false" />
                                                        <div class="dropdownlblPending">
                                                            <label>Pending
                                                                <br />
                                                                Release  to
                                                                <br />
                                                                HIMS </label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlSupervisorVetting" runat="server">
                                                <td>
                                                    <asp:Button Text="" class="middleButton" runat="server" />
                                                    <%--<img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />--%>
                                                    <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                                </td>
                                                <td>
                                                    <div class="dropdown">
                                                        <asp:Button runat="server" ID="btnPendingSupVetting" Text="7" CssClass="btnprocess2 dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                        <ul class="dropdown-menu">
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnProcessCompletedPendingSupVetting" ToolTip="Pending Sup Vetting" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnInsertDelayReason6" runat="server" ToolTip="Pending Sup Vetting" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                            <%--<li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnRecallRequest6" runat="server" ToolTip="Pending Sup Vetting" Text="Recall Request" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>--%>
                                                        </ul>
                                                        <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonSupVetting" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages1" Enabled="false" />
                                                        <asp:ImageButton ID="imgbtnoverduewithdelayreasonSupVetting" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages1" Enabled="false" />
                                                        <div class="dropdownlblPending">
                                                            <label>Pending
                                                                <br />
                                                                Supervisor
                                                                <br />
                                                                Vetting</label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlforwarding" runat="server">
                                                <td>
                                                    <asp:Button Text="" class="middleButton" runat="server" />
                                                    <%--<img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />--%>
                                                    <%-- <i class="fa fa-long-arrow-right symbol"></i>--%>
                                                </td>
                                                <td>


                                                    <div class="dropdown">

                                                        <asp:Button runat="server" ID="btnPendingforwarding" Text="8" CssClass="btnprocess dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                        <ul class="dropdown-menu pull-right">
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnProcessCompletedPendingforwarding" ToolTip="Pending forwarding" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnInsertDelayReason7" runat="server" ToolTip="Pending forwarding" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                           <%-- <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnRecallRequest7" runat="server" ToolTip="Pending forwarding" Text="Recall Request" OnClick="lnkbtnRecallRequest_Click"></asp:LinkButton></a>--%>
                                                        </ul>
                                                        <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonforwarding" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <asp:ImageButton ID="imgbtnoverduewithdelayreasonforwarding" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <div class="dropdownlbl">
                                                            <label>Pending
                                                                <br />
                                                                Forwarding</label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlPendingCollectInPerson" runat="server">
                                                <td>
                                                    <asp:Button Text="" class="middleButton" runat="server" />
                                                    <%--<img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />--%>
                                                    <%--<i class="fa fa-long-arrow-right symbol"></i>--%>
                                                </td>
                                                <td>

                                                    <div class="dropdown">
                                                        <asp:Button runat="server" ID="btnPendingCollectInPerson" Text="9" CssClass="btnprocess2 dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                        <ul class="dropdown-menu pull-right">
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnProcessCompletedPendingCollectInPerson" ToolTip="Pending Collect In Person" runat="server" Text="Process Completed" OnClick="lnkbtnProcessCompletedTraccing_Click"></asp:LinkButton></a>
                                                            <li><a href="#">
                                                                <asp:LinkButton ID="lnkbtnInsertDelayReason8" runat="server" ToolTip="Pending Collect In Person" Text="Insert Delay Reason" OnClick="lnkbtnInsertDelayReason_Click"></asp:LinkButton></a>
                                                        </ul>
                                                        <asp:ImageButton ID="imgbtnoverduewithoutdelayreasonCollectInPerson" runat="server" ImageUrl="Images/stopwatch (2).png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <asp:ImageButton ID="imgbtnoverduewithdelayreasonCollectInPerson" runat="server" ImageUrl="Images/deadline.png" Visible="false"
                                                            ToolTip="No delay reason" CssClass="Overdueimages" Enabled="false" />
                                                        <div class="dropdownlblPending">
                                                            <label>Pending
                                                                <br />
                                                                Collect
                                                                <br />
                                                                In Person</label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlForwarded" runat="server" Visible="false">
                                                <td>
                                                    <asp:Button Text="" class="middleButton" runat="server" />
                                                    <%--<img src="Images/right-arrow.png" class="symbol" alt="Alternate Text" />--%>
                                                    <%-- <i class="fa fa-long-arrow-right symbol"></i>--%>
                                                </td>
                                                <td>
                                                    <div class="dropdown">
                                                        <asp:Button runat="server" ID="btnForwarded" Enabled="false" CssClass="btnprocess3 dropdown-toggle" type="button" data-toggle="dropdown"></asp:Button>
                                                        <div class="dropdownlbl">
                                                            <asp:Label ID="lblForwardStatus" runat="server" />
                                                        </div>
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

                <!-- Panel Creatus  Code Start -->
                <asp:Panel runat="server" ID="pnlCreateRequestas" Style="margin-top: -10px;">
                    <div class="box box-solid CreatusBox">
                        <div class="form-group form-inline">
                            <label for="inputEmail3" class="col-sm-2 control-label">Create Request As </label>

                            <div class="col-sm-3 " style="width: max-content;">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlApplicationStatus"  runat="server" CssClass="form-control" Width="260">
                                        <asp:ListItem Value="INDIVIDUALREQUEST">Individual Request</asp:ListItem>
                                        <asp:ListItem Value="COPYREQUEST">Copy Request</asp:ListItem>
                                        <asp:ListItem Value="BATCHREQUESTBYPATIENT">Batch Request By Patient</asp:ListItem>
                                        <asp:ListItem Value="BATCHREQUESTBYREQUESTDETAIL">Batch Request By Request Details</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:Button runat="server" Text="Confirm" ID="btnConfirm" OnClick="btnConfirm_Click" Visible="true" CssClass="  btn btngreen " Style="border-radius: 10px" />
                            </div>

                            <label for="inputEmail3" class="col-sm-2 control-label">Bypass Pending Items </label>
                            <div class="col-sm-1">
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="chkBypassPendingItems" class="chk" CssClass="form-contl" BorderStyle="none" />
                                </div>
                            </div>
                            <div class="col-sm-2"></div>
                            <div class="form-inline">
                                <div class="Flag">
                                    <div class="form-group">
                                        <div class="Flag">
                                            <i class="fa fa-flag" style="color: red"></i>
                                            <span>
                                                <asp:CheckBox runat="server" ID="chkpriorityflagINITIAL" ToolTip="Priority Flag" class="flagchk" CssClass="form-control ReadOnly" BorderStyle="none" /></span>
                                            <span hidden>
                                                <asp:CheckBox runat="server" ID="chkCOPYQUERY" /></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel runat="server" ID="pnlRreRegReqCondition" Visible="false">

                            <div class="form-group">

                                <div class="form-group form-inline">
                                    <asp:Label for="inputEmail3" ID="lblactionby" runat="server" class="col-sm-2 control-label" Text="Action By"></asp:Label>

                                    <div class="col-sm-3 " style="width: max-content;">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlactionby" runat="server" Visible="true" CssClass="form-control" Width="260">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>

                        </asp:Panel>

                    </div>
                </asp:Panel>
                <!-- Panel Creatus  Code End -->

                <!-- Panel MRNumber  Code Start -->
                <asp:Panel runat="server" ID="pnlprofilesummary" Style="margin-top: -12px">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div class="box box-solid MRNumberBox">
                                    <br />
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="updPnlPreRequest" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <div class="form-group">
                                                                        <label for="inputEmail3" class="col-sm-2 control-label">Request Number  </label>

                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <asp:TextBox ID="txtReqNo" runat="server" ReadOnly="True" CssClass="form-control ReadOnly"></asp:TextBox>

                                                                            </div>
                                                                        </div>

                                                                        <label for="inputEmail3" class="col-sm-2 control-label">MR Number  </label>
                                                                        <div class="col-sm-3">
                                                                            <div class="form-group">
                                                                                <asp:TextBox ID="txtMRNumberHEADER" ReadOnly="True" runat="server" CssClass="form-control ReadOnly"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-1"></div>
                                                                        <div class="form-inline">
                                                                            <div class="col-sm-1">
                                                                                <div class="form-group">
                                                                                    <i class="fa fa-flag" style="color: red"></i>
                                                                                    <span>
                                                                                        <asp:CheckBox runat="server" ID="chkpriorityflag" Enabled="false" ToolTip="Priority Flag" class="flagchk" CssClass="form-control ReadOnly" BorderStyle="none" /></span>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">MRN </label>

                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtHRNHEADER" runat="server" ReadOnly="True" CssClass="form-control ReadOnly"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Patient Name </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtPatientNameHEADER" runat="server" BackColor="#F5F5F5" ReadOnly="True" CssClass="form-control ReadOnly"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtMRStatus" runat="server" ReadOnly="True" CssClass="form-control ReadOnly"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Writing and Verifying Status </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWritingandVerifyingStatus" runat="server" ReadOnly="True" CssClass="form-control ReadOnly"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtRequestorHEADER" runat="server" ReadOnly="True" CssClass="form-control ReadOnly"></asp:TextBox>

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

                                                            <asp:TextBox ID="txtRequestorTypeHEADER" runat="server" ReadOnly="True" CssClass="form-control ReadOnly"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-7 Sidegrid">
                                                        <div class="form-group">
                                                            <div class="box box-primary box-solid" style="border-radius: 19px; border: 1px dashed #ad9ed6; margin-top: -100px; width: 100%; height: auto;">
                                                                <div class="box-header with-border" style="border-radius: 60px; left: 1px; background-color: #bae4fc; height: 38px;">
                                                                    <div style="text-align: center">
                                                                        <label style="color: white; font-weight: bold; font-size: 16px;" id="lblblockbilling" runat="server"><span id="Span4" runat="server"></span></label>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="box-body" style="margin-top: -25px">
                                                                    <div class="form">
                                                                        <asp:HiddenField ID="HiddenField10" runat="server" />
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="gvReceipt" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                                AutoGenerateColumns="False"
                                                                                CellPadding="2"
                                                                                ForeColor="#333333"
                                                                                HorizontalAlign="Center"
                                                                                PageSize="10"
                                                                                CssClass=" table-striped">
                                                                                <Columns>


                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>

                                                                                            <asp:Label ID="lblRECEIPT_ID" runat="server" Text="<%#Bind('RECEIPT_ID')%>"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderTemplate>
                                                                                            <table width="100%" border="0">
                                                                                                <tr>
                                                                                                    <td align="center" class="gridtable" style="background-color: #b287f5">
                                                                                                       <asp:Label ID="lnkRECEIPT_ID" CssClass="NonBBName" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                            Text="Receipt ID" Font-Names="Arial"
                                                                                                            ></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </HeaderTemplate>
                                                                                        <ItemStyle Width="2%" HorizontalAlign="center" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>

                                                                                            <asp:Label ID="lblPYMENT_STATUS" runat="server" Text="<%#Bind('PAYMENT_STATUS')%>"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderTemplate>
                                                                                            <table width="100%">
                                                                                                <tr>
                                                                                                    <td align="center" class="gridtable" style="background-color: #b287f5 /*#c4befa*/">
                                                                                                        <asp:Label  ID="lnkbtnprocesssts6" CssClass="NonBBName" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                            Text="Payment Status" Font-Names="Arial"
                                                                                                          ></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </HeaderTemplate>
                                                                                        <ItemStyle Width="2%" HorizontalAlign="center" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblRcvd_Date" runat="server" Text='<%#Bind("Rcvd_Date", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderTemplate>
                                                                                            <table width="100%">
                                                                                                <tr>
                                                                                                    <td align="center" class="gridtable" style="background-color: #b287f5 /*#c4befa*/">
                                                                                                       <asp:Label ID="lnkbtnprocesssts6014" CssClass="NonBBName" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                            Text="Payment Date" Font-Names="Arial" ></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </HeaderTemplate>
                                                                                        <ItemStyle Width="2%" HorizontalAlign="center" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle CssClass="GridHeaderTextforScroll-UnTextNon" />
                                                                                <RowStyle CssClass="GridviewRowStyleNon" />
                                                                                <AlternatingRowStyle CssClass="GridviewAlternatingRowStyleNon alternativeFontColor" />
                                                                            </asp:GridView>
                                                                        </div>

                                                                    </div>
                                                                    <div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <!-- Panel MRNumber  Code End -->

                <!-- Progress Bar Code Start -->
                <div class="box box-solid ProgressBox" style="">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <%--<span hidden>
                                    <asp:Button ID="btnpatientprofile" Visible="True" runat="server" OnClick="btnpatientprofile_Click" />
                                    <asp:Button ID="btnrequestordetails" Visible="True" runat="server" OnClick="btnrequestordetails_Click" />
                                    <asp:Button ID="btnrequestdetails" Visible="True" runat="server" OnClick="btnrequestdetails_Click" />
                                    <asp:Button ID="btnattachment" Visible="True" runat="server" OnClick="btnattachment_Click" />
                                    <asp:Button ID="btnwaiver" Visible="True" runat="server" OnClick="btnwaiver_Click" />
                                    <asp:Button ID="btnAssignDocter" Visible="True" runat="server" OnClick="btnAssignDocter_Click" />
                                    <asp:Button ID="btnenquiry" Visible="True" runat="server" OnClick="btnenquiry_Click" />
                                    <asp:Button ID="btnpendingitems" Visible="false" runat="server" OnClick="btnpendingitems_Click" />
                                    <asp:Button ID="btnProcessHistory" Visible="false" runat="server" OnClick="btnProcessHistory_Click" />
                                </span>--%>
                                <asp:Panel ID="pnlAfterRequest" runat="server" Visible="false">
                                    <div id="divprogress" class="divprogress li">
                                        <hr class="line" />
                                        <li onclick="showpanel()" class="" id="li_Patient" title="Patient"><small>
                                            <asp:Button ID="btnpatientprofile" CssClass="aspButton" Visible="True" Text="Patient" runat="server" OnClick="btnpatientprofile_Click" />
                                            </small></li>
                                        <li onclick="showpanel1()" class="" id="li_Requestor" title="Requestor"><small> 
                                            <asp:Button ID="btnrequestordetails" CssClass="aspButton" Visible="True" runat="server" Text="Requestor" OnClick="btnrequestordetails_Click" />
                                                                                                                              </small></li>
                                        <li onclick="showpanel2()" class="" id="li_Request" title="Request"><small>
                                            <asp:Button ID="btnrequestdetails" CssClass="aspButton"  Visible="True" runat="server" Text="Request" OnClick="btnrequestdetails_Click" />
                                                                                                                        </small></li>
                                        <li onclick="showpanel3()" class="" id="li_Attachment"  title="Attachments"><small>
                                            <asp:Button ID="btnattachment" CssClass="aspButton" Visible="True" runat="server" Text="Attachments" OnClick="btnattachment_Click" />
                                                                                                                   </small></li>
                                        <li onclick="showpanel4()" class="" id="li_Waiver_Info" title="Waiver"><small>
                                            <asp:Button ID="btnwaiver" Visible="True" CssClass="aspButton" runat="server" Text="Waiver" OnClick="btnwaiver_Click" />
                                                                                                               </small></li>
                                        <li onclick="showpanel5()" class="" id="li_AssignDocter" title="Assignment"><small>
                                            <asp:Button ID="btnAssignDocter" Visible="True" CssClass="aspButton" runat="server" Text="Assignment" OnClick="btnAssignDocter_Click" />
                                                                                                                    </small></li>
                                        <li onclick="showpanel6()" class="" id="li_Enquiry" title="Remarks / Enquiry"><small>
                                            <asp:Button ID="btnenquiry" Visible="True" CssClass="aspButton" runat="server" Text="Remarks / Enquiry" OnClick="btnenquiry_Click" />
                                                                                                                      </small></li>
                                        <li onclick="showpanel7()" class="" id="li_Pendingitems" title="Pending Items"><small>
                                            <asp:Button ID="btnpendingitems" Visible="false" CssClass="aspButton" runat="server" Text="Pending Items" OnClick="btnpendingitems_Click" />
                                                                                                                       </small></li>
                                        <li onclick="showpanel8()" class="" id="li_ProcessHistory" title="Process History"><small>
                                            <asp:Button ID="btnProcessHistory" Visible="false" CssClass="aspButton" runat="server" Text="Process History" OnClick="btnProcessHistory_Click" />
                                                                                                                           </small></li>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlBeforeRequest" runat="server" Visible="false">
                                    <div id="divprogress1" class="divprogress1 li">
                                        <hr class="line1" />
                                        <li onclick="showpanel(false)" class="" id="li_Patient1" title="Patient"><small>
                                            <asp:Button ID="btnpatientprofile2" Visible="True"  CssClass="aspButton" runat="server" Text="Patient" OnClick="btnpatientprofile_Click" />
                                                                                                            </small></li>
                                        <li onclick="showpanel1(false)" class="" id="li_Requestor1" title="Requestor"><small>
                                            <asp:Button ID="btnrequestordetails2" Visible="True" CssClass="aspButton" runat="server" Text="Requestor" OnClick="btnrequestordetails_Click" />
                                                                                                                 </small></li>
                                        <li onclick="showpanel2(false)" class="" id="li_Request1" title="Request"><small>
                                            <asp:Button ID="btnrequestdetails2" Visible="True" CssClass="aspButton" runat="server" Text="Request" OnClick="btnrequestdetails_Click" />
                                                                                                             </small></li>
                                        <li onclick="showpanel3(false)" class="" id="li_Attachment1" title="Attachments"><small>
                                            <asp:Button ID="btnattachment2" Visible="True" CssClass="aspButton" runat="server" Text="Attachments" OnClick="btnattachment_Click" />
                                                                                                                    </small></li>
                                        <li onclick="showpanel4(false)" class="" id="li_Waiver_Info1" title="Waiver"><small>
                                            <asp:Button ID="btnwaiver2" Visible="True" CssClass="aspButton" runat="server" Text="Waiver" OnClick="btnwaiver_Click" />
                                                                                                                </small></li>
                                        <li onclick="showpanel5(false)" class="" id="li_AssignDocter1" title="Assignment"><small>
                                            <asp:Button ID="btnAssignDocter2" Visible="True" CssClass="aspButton" runat="server" Text="Assignment" OnClick="btnAssignDocter_Click" />
                                                                                                                     </small></li>

                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <div class="container-fluid">
                        <div class="box-body">
                            <div class="row">
                                <br />
                                <div class="tab-content">
                                    <%--TAB 01-Patient Start--%>
                                    <asp:Panel ID="pnlmenu1" runat="server">
                                        <div>
                                            <div>
                                                <div class="row">
                                                    <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                                        <asp:LinkButton ID="LkBtnBiodata" runat="server" CssClass="LnkbtnPatient" OnClick="btnHRNsearch_Click"> Bio Data </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LkBtnnewpatient" runat="server" CssClass="LnkbtnPatient" OnClick="LkBtnnewpatient_Click">New Patient</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                     <asp:LinkButton ID="LkBtnEdit" runat="server" CssClass="LnkbtnPatient" OnClick="LkBtnEdit_Click">Edit Patient</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </div>
                                                </div>
                                                <br />
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">

                                                                <label for="inputEmail3" class="col-sm-2 control-label">MRN </label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtHRN" MaxLength="50" Wrap="False" runat="server" CssClass="form-control" onkeypress="return HRNenter1_click(event);"></asp:TextBox>

                                                                        <span hidden>
                                                                            <asp:TextBox ID="txtpatientID" MaxLength="50" Wrap="False" runat="server" CssClass="form-control" onkeypress="return HRNenter1_click(event);"></asp:TextBox></span>
                                                                        <span hidden>
                                                                            <asp:TextBox ID="txtpatientIDEncripted" MaxLength="50" Wrap="False" runat="server" CssClass="form-control" onkeypress="return HRNenter1_click(event);"></asp:TextBox></span>
                                                                        <span hidden>
                                                                            <asp:Button ID="btnHRNsearch" runat="server" OnClick="btnHRNsearch_Click" /></span>
                                                                    </div>
                                                                </div>

                                                                <label for="inputEmail3" class="col-sm-2 control-label">Patient Name </label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtName" runat="server" MaxLength="50" ReadOnly="True" CssClass="form-control ReadOnly"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">
                                                                <label for="inputEmail3" class="col-sm-2 control-label">DOB </label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtDOB" runat="server" MaxLength="50" BackColor="" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <label for="inputEmail3" class="col-sm-2 control-label">Address </label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Style="height: 80px" ReadOnly="True" MaxLength="512" CssClass="MultiLine_Textbox ReadOnly form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">
                                                                <label for="inputEmail3" class="col-sm-2 control-label">Gender </label>

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtGender" runat="server" MaxLength="50" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <label for="inputEmail3" class="col-sm-2 control-label">Postal Code </label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtPostCode" runat="server" MaxLength="50" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">
                                                                <label for="inputEmail3" class="col-sm-2 control-label">Phone </label>

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="50" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <label for="inputEmail3" class="col-sm-2 control-label">Email </label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">


                                                                <label for="inputEmail3" class="col-sm-2 control-label">Deceased Status </label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:CheckBox ID="chkDeath" runat="server" CssClass="form-control ReadOnly" Width="40px" Enabled="False" />
                                                                    </div>
                                                                </div>
                                                                <asp:Label for="inputEmail3" ID="lblDDate" runat="server" class="col-sm-2 control-label" Text="Deceased Date"></asp:Label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtDDate" runat="server" ReadOnly="True" CssClass="form-control ReadOnly" MaxLength="10"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">
                                                                <label for="inputEmail3" class="col-sm-2 control-label">EMR </label>
                                                                <asp:UpdatePanel ID="updpnlEMR" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="col-sm-1">
                                                                            <div class="form-group">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:RadioButton ID="rbtEMR" OnCheckedChanged="rbtEMR_CheckedChanged" runat="server" CssClass="Space" AutoPostBack="true" Height="20px" Width="100%" GroupName="rdgrpSelection1" />
                                                                                        </td>
                                                                                        <td>&nbsp;&nbsp;</td>
                                                                                        <td>
                                                                                            <label style="position: relative; top: 2px;">Yes</label></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                            <div class="form-group">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:RadioButton ID="rbt1EMR" OnCheckedChanged="rbt1EMR_CheckedChanged" runat="server" CssClass="Space" AutoPostBack="true" Height="20px" Width="100%" GroupName="rdgrpSelection1" />
                                                                                        </td>
                                                                                        <td>&nbsp;&nbsp;</td>
                                                                                        <td>
                                                                                            <label style="position: relative; top: 2px;">No</label></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                            <div class="form-group">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:RadioButton ID="rbtnboth" OnCheckedChanged="rbtnboth_CheckedChanged" runat="server" CssClass="Space" AutoPostBack="true" Height="20px" Width="100%" GroupName="rdgrpSelection1" />
                                                                                        </td>
                                                                                        <td>&nbsp;&nbsp;</td>
                                                                                        <td>
                                                                                            <label style="position: relative; top: 2px;">
                                                                                            Both </labe>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">
                                                                <label for="inputEmail3" class="col-sm-2 control-label">Source of Reference </label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:DropDownList ID="ddlReference" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Panel ID="pnlBATCHREQBYPATIENT" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-left: -15px">
                                                            <asp:LinkButton ID="LinkbtnaddBatchProfile" runat="server" OnClick="LinkbtnaddBatchProfile_Click" CssClass="btn btns-search btngreen" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px"><i class="fa fa-plus" aria-hidden="true"></i> ADD</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #aa97dd; width: 99%">
                                                        <div class="box-header with-border TotalRecord">
                                                            <div class="box-title">
                                                                <label class="text-right" style="color: white">Total Records : <span id="TotalrecPatient" runat="server"></span></label>
                                                            </div>
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="form">
                                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                                                <div class="table-responsive table--no-card m-b-30" id="grid2">
                                                                    <asp:GridView ID="gvlistBatchpatientprofile" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                        AutoGenerateColumns="False"
                                                                        CellPadding="2"
                                                                        ForeColor="#333333"
                                                                        HorizontalAlign="Center"
                                                                        PageSize="10"
                                                                        CssClass="table table-borderless table-striped">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHRN_ID" runat="server" Text="<%#Bind('HRN_ID')%>"></asp:Label>
                                                                                    <%--<asp:LinkButton ID="lnkbtnUserID" runat="server" Text="<%#Bind('HRN_ID')%>" CommandArgument='<%#Eval("HRN_ID")+","+ Eval("SHORT_NAME")%>'></asp:LinkButton>--%>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnMRN" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="MRN" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="HRN_ID"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblordno" runat="server" Text="<%#Bind('EMR')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnEMR" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="EMR" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="ORDI_ID"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblName" runat="server" Text="<%#Bind('SHORT_NAME')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">

                                                                                                <asp:Label ID="lnkbtnname" runat="server" Text="Name"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDOB" runat="server" Text='<%#Bind("DOB", "{0:dd-MM-yyyy }")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lnkbtdob" runat="server" Text="DOB"></asp:Label>

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAddress" runat="server" Text="<%#Bind('Address')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lnkbtnAddress" runat="server" Text="Address"></asp:Label>

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGender" runat="server" Text="<%#Bind('Gender')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lnkbtnGender" runat="server" Text="Gender"></asp:Label>

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPostal" runat="server" Text="<%#Bind('post_code')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lnkbtnPostal" runat="server" Text="Postal"></asp:Label>

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPhone" runat="server" Text="<%#Bind('ph_no1')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lnkbtnPhone" runat="server" Text="Phone"></asp:Label>

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEmail" runat="server" Text="<%#Bind('Email')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lnkbtnEmail" runat="server" Text="Email"></asp:Label>

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnEditAddpatintinGRID" runat="server" Text="Edit" CssClass="btnEdit" OnClick="btnEditAddpatintinGRID_Click" CommandArgument='<%#Eval("HRN_ID")+","+Eval("SHORT_NAME")+","+ Eval("patient_id")%>' />
                                                                                    <controlstyle cssclass="btn btnred"></controlstyle>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #8572b7; border-collapse: collapse;">
                                                                                        <tr>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnDeleteAddpatintinGRID" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDeleteAddpatintinGRID_Click" CommandArgument='<%#Eval("HRN_ID")+","+Eval("SHORT_NAME")+","+ Eval("patient_id")%>' />
                                                                                    <controlstyle cssclass="btn btnred"></controlstyle>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #8572b7; border-collapse: collapse;">
                                                                                        <tr>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                        <RowStyle CssClass="GridviewRowStyle" />
                                                                        <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                    </asp:GridView>
                                                                </div>
                                                                <div class="col-lg-12">
                                                                    <div class="row form-group">
                                                                        <div class="col-sm-12" style="text-align: center">
                                                                            <asp:Repeater ID="Repeater3" runat="server">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <ul class="list-unstyled list-inline pull-right" style="border-radius: 40px;">
                                                <li>
                                                    <button type="button" class="btn btngreen next-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel1()">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></button>
                                                </li>
                                            </ul>
                                    </asp:Panel>
                                    <%--TAB 01-Patient END--%>

                                    <%--TAB 02-Requestor Start--%>
                                    <asp:Panel ID="pnlmenu2" runat="server">
                                        <div>

                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                                    <asp:LinkButton ID="lnkbtnNewReq" runat="server" OnClick="lnkbtnNewReq_Click" CssClass="LnkbtnPatient">New Requestor</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                     <asp:LinkButton ID="lnkbtnselfReq" OnClick="lnkbtnselfReq_Click" runat="server" CssClass="LnkbtnPatient">Self Requestor</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                     <asp:LinkButton ID="lnkbtnEditReq" OnClick="lnkbtnEditReq_Click" runat="server" CssClass="LnkbtnPatient">Edit Requestor</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                     <asp:LinkButton ID="lnkbtnOthers" OnClick="lnkbtnOthers_Click" runat="server" CssClass="LnkbtnPatient">Others</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                     <asp:LinkButton ID="lnkbtnReqlist" OnClick="lnkbtnReqlist_Click" runat="server" CssClass="LnkbtnPatient">Refresh Requestor List</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </div>
                                            </div>
                                            <br />
                                            <table style="width: 100%">

                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Requestor </label>

                                                            <div class="col-sm-4">
                                                                <div class="form-group">

                                                                    <asp:UpdatePanel ID="updpnltxtReqID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtReqname" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtReqID" CssClass=" form-control"></asp:TextBox>
                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">

                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="REQUESTOR_txtReqID_txtReqname_updpnltxtReqID" runat="server" ID="imgbtntrigerREQUESTOR" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="REQUESTOR_txtReqID_txtReqname" runat="server" ID="imgbtnClearREQUESTOR" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-2 control-label">Requestor Ref No </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtRegReferenceNo" runat="server" CssClass="form-control" BackColor="#e2f7fe"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">If Others (Please Specify) </label>

                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtReqOthers" runat="server" CssClass="optional form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Requestor Type </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">

                                                                    <asp:UpdatePanel ID="updpnltxtRequestorTypeID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtRequestorTypename" Enabled="false" CssClass=" form-control " onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtRequestorTypeID" Enabled="false" CssClass="ReadOnly form-control"></asp:TextBox>
                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">

                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="REQUESTORTYPE_txtRequestorTypeID_txtRequestorTypename_updpnltxtRequestorTypeID" runat="server" ID="imgbtntrigerPopup" Enabled="false" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="REQUESTORTYPE_txtRequestorTypeID_txtRequestorTypename" runat="server" ID="imgbtnCleardropdowntxtboxvalue" Enabled="false" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Requestor Name </label>

                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtReqBy" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <%-- <asp:TextBox ID="txtRequestorName" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Relationship with Patient </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="updpnltxtReqRelationID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtReqRelationname" CssClass=" optional form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtReqRelationID" CssClass="optional form-control"></asp:TextBox>
                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">

                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="RELATIONSHIPWITHPATIENT_txtReqRelationID_txtReqRelationname_updpnltxtReqRelationID" runat="server" ID="imgbtntxtReqRelationIDtrigger" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="RELATIONSHIPWITHPATIENT_txtReqRelationID_txtReqRelationname" runat="server" ID="imgbtnclrtxtReqRelationID" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Requestor Address </label>

                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtReqAdd" runat="server" TextMode="MultiLine" Style="resize: none" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-2 control-label">Mailing Address </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtMailAdd" runat="server" TextMode="MultiLine" Style="resize: none;" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Recipient Email </label>

                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtReqEmail" runat="server"  CssClass="optional form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-2 control-label">Requestor Phone </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtReqPhNo" runat="server"  CssClass="optional form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Internal Staff Request</label>

                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:CheckBox runat="server" ID="chkinternalstaff" AutoPostBack="true" OnCheckedChanged="chkinternalstaff_CheckedChanged" class="chk" CssClass="form-control wrap" Width="40px" />
                                                                    <asp:Label ID="chkinternalstaff1" Text="" OnCheckedChanged="chkinternalstaff_CheckedChanged" CssClass="wrap" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-2 control-label">Block Billing </label>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlBlockBill" runat="server" CssClass="form-control ReadOnly" Enabled="False" OnSelectedIndexChanged="ddlBlockBill_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>YES</asp:ListItem>
                                                                    <asp:ListItem>NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Cost Centre </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">

                                                                    <asp:UpdatePanel ID="updpnltxtcostcenterID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtcostcentername" CssClass="form-control  wrap"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtcostcenterID" Enabled="false" CssClass="form-control ReadOnly"></asp:TextBox>
                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">

                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="COSTCENTER_txtcostcenterID_txtcostcentername_updpnltxtcostcenterID" Enabled="false" runat="server" ID="imgbtntrigerCOSTCENTER" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="COSTCENTER_txtcostcenterID_txtcostcentername" runat="server" ID="imgbtnClearCOSTCENTER" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-2 control-label">Contact Preference </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">

                                                                    <asp:DropDownList ID="ddlContactPreference" runat="server" CssClass="optional form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Mobile No (For SMS) </label>

                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtReqMobileNo" runat="server" MaxLength="8" CssClass="form-control optional" onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <ul class="list-unstyled list-inline pull-right">
                                                <li>
                                                    <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button></li>
                                                <li>
                                                    <button type="button" class="btn btngreen next-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel2()">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></button>
                                                </li>
                                            </ul>
                                        </div>
                                    </asp:Panel>
                                    <%--TAB 02-Requestor END--%>

                                    <%--TAB 03-Request Start--%>
                                    <asp:Panel ID="pnlmenu3" runat="server">
                                        <div>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Created Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtCreateDate" runat="server" CssClass="ReadOnly form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender12" TargetControlID="txtCreateDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Hospital Attendance Date</label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtDOHA" runat="server" CssClass="optional form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtDOHA_CalendarExtender" TargetControlID="txtDOHA" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Request Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtReqDate" runat="server" CssClass="form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtReqDate_CalendarExtender" TargetControlID="txtReqDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Type of Visit </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="updpnltxttypeofvisitID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txttypeofvisitname" CssClass="optional form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txttypeofvisitID" CssClass="form-control "></asp:TextBox>

                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">

                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="TYPEOFVISIT_txttypeofvisitID_txttypeofvisitName_updpnltxttypeofvisitID" runat="server" ID="imgbtntrigerTYPEOFVISIT" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="TYPEOFVISIT_txttypeofvisitID_txttypeofvisitName" runat="server" ID="imgbtnClearTYPEOFVISIT" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>


                                                                </div>
                                                            </div>

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Received Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtRecDate" runat="server" CssClass="form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtRecDate_CalendarExtender" TargetControlID="txtRecDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Case/Visit Number </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtcasevisitno" runat="server" CssClass="optional form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Due Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtDueDate" runat="server" Enabled="false" CssClass="ReadOnly form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtDueDate_CalendarExtender" TargetControlID="txtDueDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Priority </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">

                                                                    <asp:UpdatePanel ID="updpnltxtPriorityID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtPriorityname" CssClass="optional form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtPriorityID" CssClass="optional form-control "></asp:TextBox>

                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">

                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="PRIORITY_txtPriorityID_txtPriorityname_updpnltxtPriorityID" runat="server" ID="imgbtntrigerPRIORITY" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="PRIORITY_txtPriorityID_txtPriorityname" runat="server" ID="imgbtnClearPRIORITY" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">


                                                             <label for="inputEmail3" class="col-sm-2 control-label">Process Type </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">

                                                                    <asp:UpdatePanel ID="updpnltxtProcessTypeID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtProcessTypename" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtProcessTypeID" CssClass="form-control"></asp:TextBox>

                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="PROCESSTYPE_txtProcessTypeID_txtProcessTypename_updpnltxtProcessTypeID" runat="server" ID="imgbtntrigerPROCESSTYPE" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="PROCESSTYPE_txtProcessTypeID_txtProcessTypename" runat="server" ID="imgbtnClearPROCESSTYPE" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>


                                                                </div>
                                                            </div>


                                                            
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Report Type </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="updpnltxtRptTypeID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtRptTypename" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtRptTypeID" CssClass="form-control"></asp:TextBox>

                                                                            </span>
                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="REPORTTYPE_txtRptTypeID_txtRptTypename_updpnltxtRptTypeID" runat="server" ID="imgbtntrigerREPORTTYPE" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="REPORTTYPE_txtRptTypeID_txtRptTypename" runat="server" ID="imgbtnClearREPORTTYPE" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                           <label for="inputEmail3" class="col-sm-2 control-label">Request Type </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="updpnltxtReqTypeID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtReqTypename" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtReqTypeID" CssClass="form-control"></asp:TextBox>

                                                                            </span>
                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="REQUESTTYPE_txtReqTypeID_txtReqTypename_updpnltxtReqTypeID" runat="server" ID="imgbtntrigerREQUESTTYPE" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="REQUESTTYPE_txtReqTypeID_txtReqTypename" runat="server" ID="imgbtnClearREQUESTTYPE" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>


                                                                </div>
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-2 control-label">Purpose </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="updpnltxtPurposeID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtPurposename" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtPurposeID" CssClass="form-control"></asp:TextBox>

                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="PURPOSE_txtPurposeID_txtPurposename_updpnltxtPurposeID" runat="server" ID="imgbtntrigerPURPOSE" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="PURPOSE_txtPurposeID_txtPurposename" runat="server" ID="imgbtnClearPURPOSE" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>

                                                            <span hidden>
                                                                <label for="inputEmail3" class="col-sm-2 control-label">Record Type </label>
                                                            </span>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="updpnlRecModReqTypeID" runat="server" UpdateMode="Conditional" Visible="false">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtRecModReqTypename" CssClass="optional form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtRecModReqTypeID" CssClass="optional form-control "></asp:TextBox>

                                                                            </span>
                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">

                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="RECMODRECORDTYPE_txtRecModReqTypeID_txtRecModReqTypename_updpnlRecModReqTypeID" runat="server" ID="imgbtntrigerRECMODRECORDTYPE" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="RECMODRECORDTYPE_txtRecModReqTypeID_txtRecModReqTypename" runat="server" ID="imgbtnClearRECMODRECORDTYPE" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">

                                                             <label for="inputEmail3" class="col-sm-2 control-label">Received From </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="updpnltxtRecFrmID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtRecFrmname" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtRecFrmID" CssClass="form-control"></asp:TextBox>

                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="RECEIVEDFROM_txtRecFrmID_txtRecFrmname_updpnltxtRecFrmID" runat="server" ID="imgbtntrigerRECEIVEDFROM" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="RECEIVEDFROM_txtRecFrmID_txtRecFrmname" runat="server" ID="imgbtnClearRECEIVEDFROM" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-2 control-label">Report Format </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="updpnltxtRptFormatID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtRptFormatname" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtRptFormatID" CssClass="form-control"></asp:TextBox>

                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="REPORTFORMAT_txtRptFormatID_txtRptFormatname_updpnltxtRptFormatID" runat="server" ID="imgbtntrigerREPORTFORMAT" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="REPORTFORMAT_txtRptFormatID_txtRptFormatname" runat="server" ID="imgbtnClearREPORTFORMAT" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <span hidden>
                                                                <label for="inputEmail3" class="col-sm-2 control-label">Request Category </label>
                                                            </span>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">

                                                                    <asp:UpdatePanel ID="updpnltxtRecTypeID" runat="server" UpdateMode="Conditional" Visible="false">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtRecTypename" CssClass=" form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtRecTypeID" CssClass="form-control"></asp:TextBox>

                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="REQUESTCATEGORY_txtRecTypeID_txtRecTypename_updpnltxtRecTypeID" runat="server" ID="imgbtntrigerREQUESTCATEGORY" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="REQUESTCATEGORY_txtRecTypeID_txtRecTypename" runat="server" ID="imgbtnClearREQUESTCATEGORY" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Deliver Mode </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="updpnltxtDelToID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtDelToname" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtDelToID" CssClass="form-control "></asp:TextBox>

                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">

                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="DELIVERBY_txtDelToID_txtDelToname_updpnltxtDelToID" runat="server" ID="imgbtntrigerDELIVERBY" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="DELIVERBY_txtDelToID_txtDelToname" runat="server" ID="imgbtnClearDELIVERBY" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">No of Copies </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtCopies" runat="server" CssClass="form-control" Text="1" onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Private Medical Insurance </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlPMI" runat="server" CssClass="optional form-control">
                                                                        <asp:ListItem Value="YES" Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Conditioning Information for Doctor</label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtConInforDoctor" runat="server" TextMode="MultiLine" Style="resize: none" CssClass="optional form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Name of the Institution (For Loan In/Out only) </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtLoanFromInst" runat="server" CssClass="optional form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">CAT A </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlCAT" runat="server" CssClass="optional form-control">
                                                                        <asp:ListItem Value="YES" Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Appointment Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtAppDate" runat="server" CssClass="optional form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtAppDate_CalendarExtender" TargetControlID="txtAppDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Accident Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtAccidentDate" runat="server" CssClass="optional form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtAccidentDate_CalendarExtender" TargetControlID="txtAccidentDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                           <label for="inputEmail3" class="col-sm-2 control-label">Re-Despatch Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtReassDate" runat="server" CssClass="optional form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtReassDate_CalendarExtender" TargetControlID="txtReassDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-2 control-label">Assessment Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtAssessmentDate" runat="server" CssClass="optional form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtAssessmentDate_CalendarExtender" TargetControlID="txtAssessmentDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                             <label for="inputEmail3" class="col-sm-2 control-label">Related MR Ref</label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtrelatedMRref" runat="server" CssClass="optional form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <%--  <tr>
                                                <td>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-2 control-label">Additional Process Type</label>
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlAdditionalProcessType" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <label for="inputEmail3" class="col-sm-2 control-label">Additional Process start Date</label>
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtDocReviewDate" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender13" TargetControlID="txtDocReviewDate" runat="server"
                                                                    Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="form-group">
                                                       
                                                        <label for="inputEmail3" class="col-sm-2 control-label">Auto Request Record</label>
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <asp:CheckBox runat="server" ID="chkAutoRequest" class="chk" CssClass="form-control" BorderStyle="none" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>--%>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label"><%--Copy Request Ref No.--%></label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <span hidden>
                                                                        <asp:TextBox ID="txtReqRefNo" runat="server" CssClass="form-control"></asp:TextBox></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label" visible="False"><%--Reference No.--%></label>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <span hidden>
                                                                        <asp:TextBox ID="txtReqRef" runat="server" CssClass="form-control" Visible="False"> </asp:TextBox>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label"></label>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <span hidden>
                                                                        <asp:TextBox ID="txtReqStartDate" runat="server" CssClass="form-control" autocomplete="off" Enabled="False" Visible="False"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="txtReqStartDate_CalendarExtender" TargetControlID="txtReqStartDate" runat="server"
                                                                            Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <asp:Label class="col-sm-2 control-label" Visible="False" ID="lblrecby" runat="server" Text="Receive By"></asp:Label>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlRecBy" Visible="False" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlcopyexixtrecord" runat="server" Visible="false">
                                                    <div class="box-body">
                                                        <div class="form">
                                                            <div class="table-responsive table--no-card m-b-30">
                                                                <asp:GridView ID="gvcopyexixtrecord" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                    AutoGenerateColumns="False"
                                                                    CellPadding="2"
                                                                    ForeColor="#333333"
                                                                    HorizontalAlign="Center"
                                                                    PageSize="10"
                                                                    CssClass="table table-borderless table-striped">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMRNOcopy" runat="server" Text="<%#Bind('MR_ID')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnMRNOcopy" runat="server" Text="MR Number"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkbtncopyreqLinkexistrec" runat="server" Text="<%#Bind('Request_ID')%>" CommandArgument='<%#Eval("hrn_id")+","+ Eval("Request_ID")+","+ Eval("MR_STATUS")+","+ Eval("MR_ID")%>' OnClick="lnkbtncopyreqLinkexistrec_Click"></asp:LinkButton>

                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnRequesnocopy" runat="server" Text="Request Number"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcreateDatecopy" runat="server" Text='<%#Bind("Create_Date", "{0:dd-MM-yyyy  }")%>'></asp:Label>

                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnCreate_Datecopy" runat="server" Text="Date Created"></asp:Label>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                    <RowStyle CssClass="GridviewRowStyle" />
                                                                    <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                            </table>
                                            <asp:Panel ID="pnlBATCHREQBYreqderail" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-left: -15px">
                                                        <asp:LinkButton ID="btnaddpatientgridbatchreq" OnClick="btnaddpatientgridbatchreq_Click" runat="server" CssClass="btn btns-search btngreen" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px; width: 90px"><i class="fa fa-plus" aria-hidden="true"></i> Add </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed; width: 99%">
                                                    <div class="box-header with-border TotalRecord">
                                                        <div class="box-title">
                                                            <label class="text-right" style="color: white">Total Records : <span id="TotalrecRequest" runat="server"></span></label>
                                                        </div>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="form">
                                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                                            <div class="table-responsive table--no-card m-b-30" id="grid1">
                                                                <asp:GridView ID="gvlistBatchRequestprofile" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                    AutoGenerateColumns="False"
                                                                    CellPadding="2"
                                                                    ForeColor="#333333"
                                                                    HorizontalAlign="Center"
                                                                    PageSize="10"
                                                                    CssClass="table table-borderless table-striped">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcreateDate" runat="server" Text='<%#Bind("Create_Date", "{0:dd-MM-yyyy  }")%>'></asp:Label>

                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnCreate_Date" runat="server" Text="Create Date"></asp:Label>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblReceived_From1" runat="server" Text="<%#Bind('Received_From')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnReceived_From" runat="server" Text="Received From"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRequesttyp" runat="server" Text="<%#Bind('RequestTyp_ID')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnRequesttype" runat="server" Text="Request Type"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblProcesstyp" runat="server" Text="<%#Bind('MRP_ID')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnProcesstype" runat="server" Text="Process Type"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDelMod_ID" runat="server" Text="<%#Bind('DelMod_ID')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnDelMod_IDp14e" runat="server" Text="Deliver By"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRequestDate" runat="server" Text='<%#Bind("Request_Date", "{0:dd-MM-yyyy  }")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnRequestDateDpe" runat="server" Text="Request Date"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDueDate" runat="server" Text='<%#Bind("Due_Date", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkDueDpe" runat="server" Text="Due Date"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblReceive_Date" runat="server" Text='<%#Bind("Receive_Date", "{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnReceive_Date" runat="server" Text="Received Date"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblReport" runat="server" Text="<%#Bind('RptFmt_ID')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnReport" runat="server" Text="Report Format"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <%--<asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRecord" runat="server" Text="<%#Bind('MODREQ_TYPE')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnRecord" runat="server" Text="Record Type"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>--%>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRptTyp_ID" runat="server" Text="<%#Bind('RptTyp_ID')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnRptTyp_ID" runat="server" Text="Report Type"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPurpose" runat="server" Text="<%#Bind('RptPur_ID')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnPurpose" runat="server" Text="Purpose"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPriority" runat="server" Text="<%#Bind('Priority')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnPriority" runat="server" Text="Priority"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPMI" runat="server" Text="<%#Bind('PMI')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnPMI" runat="server" Text="PMI"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCAT_A" runat="server" Text="<%#Bind('CAT_A')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnCAT_A" runat="server" Text="CAT A"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCon_Info_Doc" runat="server" Text="<%#Bind('Con_Info_Doc')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lnkbtnCon_Info_Doc" runat="server" Text="Con_Info_Doc"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnEditAddRequestGRID" runat="server" Text="Edit" CssClass="btnEdit" OnClick="btnEditAddRequestGRID_Click" CommandArgument='<%#Eval("RptTyp_ID")+","+ Eval("MRP_ID")%>' />
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
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnDeleteAddRequestGRID" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDeleteAddRequestGRID_Click" CommandArgument='<%#Eval("RptTyp_ID")+","+ Eval("MRP_ID")%>' />
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
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                    <RowStyle CssClass="GridviewRowStyle" />
                                                                    <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                </asp:GridView>
                                                            </div>
                                                            <div class="col-lg-12">
                                                                <div class="row form-group">
                                                                    <div class="col-sm-12" style="text-align: center">
                                                                        <asp:Repeater ID="Repeater4" runat="server">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                    CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="color: red; font-size: 20px;">Additional Non-MR</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Report Type </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="updpnltxtAddRptTypeID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtAddRptTypename" CssClass="optional form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtAddRptTypeID" CssClass="form-control"></asp:TextBox>

                                                                            </span>

                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">

                                                                                <span class="downarrrow">
                                                                                    <asp:ImageButton AlternateText="ADDREPORTTYPE_txtAddRptTypeID_txtAddRptTypename_updpnltxtAddRptTypeID" runat="server" ID="imgbtntrigerADDREPORTTYPE" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="ADDREPORTTYPE_txtAddRptTypeID_txtAddRptTypename" runat="server" ID="imgbtnClearADDREPORTTYPE" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">No of Copies </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtAddNoCopy" runat="server" CssClass="optional form-control" Text="1" onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div class="form-group">

                                                            <label for="inputEmail3" class="col-sm-2 control-label">Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtAddDate" runat="server" CssClass="optional form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtAddDate_CalendarExtender" TargetControlID="txtAddDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-2 control-label"></label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:LinkButton ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" CssClass="btn btns-search btngreen" Style="font-size: 13px; width: 100px; line-height: 37px; padding: 0px 15px; border-radius: 15px; width: 90px"><i class="fa fa-plus" aria-hidden="true"></i> Add </asp:LinkButton>
                                                                    <%--<i class="fa fa-plus" aria-hidden="true"  ></i>--%>
                                                                    <%-- <asp:Button runat="server" ID="BtnAdd" CssClass="form-control btngreen" Style="width: 100px;" Text="Add" OnClick="BtnAdd_Click" />--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed; width: 99%">
                                                <div class="box-header with-border TotalRecord">
                                                    <div class="box-title">
                                                        <label class="text-right" style="color: white">Total Records : <span id="lblTotalRecords" runat="server"></span></label>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="form">
                                                        <asp:HiddenField ID="hdnVisiblity" runat="server" />
                                                        <div class="table-responsive table--no-card m-b-30" id="divid">
                                                            <asp:GridView ID="gvNonMRList" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                AutoGenerateColumns="False" OnRowDataBound="gvNonMRList_RowDataBound"
                                                                CellPadding="2"
                                                                ForeColor="#333333"
                                                                HorizontalAlign="Center"
                                                                PageSize="10"
                                                                CssClass="table table-borderless table-striped">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="UNIQUE_ID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbluniqid" runat="server" Visible="false" Text='<%#Eval("Index") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" CssClass="text-left" Width="70px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkbtnUserID" runat="server" Text="<%#Bind('SHORT_NAME')%>" OnClick="lnkbtnUserID_Click" CommandArgument='<%#Eval("Index")+","+ Eval("SHORT_NAME")%>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                <tr>
                                                                                    <td class="gridtabletd">
                                                                                        <asp:LinkButton ID="lnkbtnusrid" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                            Text="Report Type" Font-Names="Arial" ForeColor="White"
                                                                                            CommandArgument="Short_Name"></asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblordno" runat="server" Text="<%#Bind('reference_1')%>"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                <tr>
                                                                                    <td class="gridtabletd">
                                                                                        <asp:LinkButton ID="lnkbtnprocesssts7" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                            Text="Qty" Font-Names="Arial" ForeColor="White"
                                                                                            CommandArgument="reference_1"></asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTransDate" runat="server" Text='<%#Bind("reference_date_1", "{0:dd-MM-yyyy }")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                <tr>
                                                                                    <td class="gridtabletd">
                                                                                        <asp:LinkButton ID="lnkbtnHeadershortName9" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                            Text="Date Added" Font-Names="Arial" ForeColor="White"
                                                                                            CommandArgument="reference_date_1"></asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnDeletelevel3" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDeletelevel3_Click" CommandArgument='<%#Eval("Index")+","+ Eval("SHORT_NAME")%>' />
                                                                            <controlstyle cssclass="btn btnred"></controlstyle>
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                <tr>
                                                                                </tr>
                                                                                <tr>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                <RowStyle CssClass="GridviewRowStyle" />
                                                                <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                            </asp:GridView>
                                                        </div>
                                                        <div class="col-lg-12">
                                                            <div class="row form-group">
                                                                <div class="col-sm-12" style="text-align: center">
                                                                    <asp:Repeater ID="rptPager" runat="server">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <ul class="list-unstyled list-inline pull-right">
                                                <li>
                                                    <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel1()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button></li>
                                                <li>
                                                    <button type="button" class="btn btngreen next-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel3()">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></button>
                                                </li>
                                            </ul>
                                        </div>
                                    </asp:Panel>
                                    <%--TAB 03-Request END--%>

                                    <%--TAB 04-Attachment Start--%>
                                    <asp:UpdatePanel ID="updpnlattachments" runat="server">
                                        <ContentTemplate>
                                            <div>

                                                <table id="myTable" style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">

                                                                <label for="inputEmail3" class="col-sm-2 control-label">Select File for Attachment </label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:FileUpload ID="FileUpload1" runat="server"  Enabled="false"/>
                                                                        <label style="color: red;">Allowed File Extensions : .doc,.xlsx,.docx,.pptx,.pdf,.jpg,.jpeg,.PNG</label>
                                                                        <label style="color: red;">Maximum File Size: 2MB </label>

                                                                    </div>
                                                                </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">
                                                                <label for="inputEmail3" class="col-sm-2 control-label">Category </label>

                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="optional form-control">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">
                                                                <label for="inputEmail3" class="col-sm-2 control-label">Add Remark </label>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Style="height: 90px; resize: none" CssClass="optional form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                </table>
                                                <div class="row">
                                                    <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-left: -15px">
                                                        <asp:UpdatePanel ID="UpnlUpload" runat="server">
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lnkbtnaddattachments" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lnkbtnaddattachments" CausesValidation="False" Enabled="false" runat="server" OnClick="lnkbtnaddattachments_Click" CssClass="btn btns-search btngreen" OnClientClick="validateCategory()" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px; width: 90px"><i class="fa fa-plus" aria-hidden="true"></i> Add</asp:LinkButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed; width: 99%">
                                                    <div class="box-header with-border TotalRecord">
                                                        <div class="box-title">
                                                            <label class="text-right" style="color: white">Total Records : <span id="lbltotalrecAttachments" runat="server"></span></label>
                                                        </div>
                                                    </div>

                                                    <div class="box-body">
                                                        <div class="form">
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            <div class="table-responsive table--no-card m-b-30" id="divid1">
                                                                <asp:GridView ID="gvAttachments" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                    AutoGenerateColumns="False"
                                                                    CellPadding="2"
                                                                    ForeColor="#333333"
                                                                    HorizontalAlign="Center"
                                                                    PageSize="10"
                                                                    CssClass="table table-borderless table-striped" OnRowDataBound="gvAttachments_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Literal ID="ltrID" runat="server" Text='<%# Eval("ATTACH_ID") %>' Visible="false"></asp:Literal>
                                                                                <asp:LinkButton ID="lnkbtnattachmentid" runat="server" Text="" CommandArgument='<%#Eval("BE_ID")+","+ Eval("FORM_ID")+","+ Eval("TRANS_ID")+","+ Eval("DOC_NAME")+","+ Eval("DOC_TYPE")+","+ Eval("ATTACH_ID")%>' OnClick="lnkbtnattachmentid_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblFilename" runat="server" Text="File Name"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAttachedDateatt" runat="server" Text='<%#Bind("Created_On", "{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblAttachedon" runat="server" Text="Attached On"></asp:Label>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAttachedbyatt" runat="server" Text='<%#Bind("Created_By")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblAttachedBy" runat="server" Text="Attached By"></asp:Label>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblremarksatt" runat="server" Text='<%#Bind("REMARKS")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblRemarks01" runat="server" Text="Remarks"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCategoryatt" runat="server" Text='<%#Bind("CATEGORY")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblCategory01" runat="server" Text="Category"></asp:Label>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnDeleteAddattachments" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDeleteAddattachments_Click" CommandArgument='<%#Eval("BE_ID")+","+ Eval("FORM_ID")+","+ Eval("TRANS_ID")+","+ Eval("DOC_NAME")+","+ Eval("DOC_TYPE")+","+ Eval("ATTACH_ID")%>' />
                                                                                <controlstyle cssclass="btn btnred"></controlstyle>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                    <tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                    <RowStyle CssClass="GridviewRowStyle" />
                                                                    <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                </asp:GridView>
                                                            </div>
                                                            <div class="col-lg-12">
                                                                <div class="row form-group">
                                                                    <div class="col-sm-12" style="text-align: center">
                                                                        <asp:Repeater ID="Repeater1" runat="server">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                    CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <ul class="list-unstyled list-inline pull-right">
                                                    <li>
                                                        <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel2()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button></li>
                                                    <li>
                                                        <button type="button" class="btn btngreen next-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel4()">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></button>
                                                </ul>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <%--TAB 04-Attachment END--%>

                                    <%--TAB 05-Waiver Applications Start--%>
                                    <asp:Panel ID="pnlmenu5" runat="server">
                                        <div>

                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Waiver Applications </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlWApp" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWApp_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Yes" Value="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="NO"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Waiver Remark </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtWaiverrmk" runat="server" CssClass="optional form-control" TextMode="MultiLine" Style="resize: none; height: 80px"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Financial Assistance Status </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlFinancialdte" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFinancialdte_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Financial Assistance Validity from Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtFinAssformdte" runat="server" CssClass="form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtFinAssformdte_CalendarExtender" TargetControlID="txtFinAssformdte" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Financial Assistance Validity to Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtFinAsstodte" runat="server" CssClass="form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtFinAsstodte_CalendarExtender" TargetControlID="txtFinAsstodte" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Doctor Waiver </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddldoctorwaiver" runat="server" CssClass="form-control " AutoPostBack="true" OnSelectedIndexChanged="ddldoctorwaiver_SelectedIndexChanged">
                                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="Pending" Value="PENDING"></asp:ListItem>
                                                                        <asp:ListItem Text="Approved" Value="APPROVED"></asp:ListItem>
                                                                        <asp:ListItem Text="Declined" Value="DECLINED"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Doctor Action Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtDoctorActionDate" runat="server" CssClass="form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtDoctorActionDate_CalendarExtender" TargetControlID="txtDoctorActionDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Doctor Decline Reason </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlDoctorDeclineReason" runat="server" CssClass="form-control ReadOnly">
                                                                        <asp:ListItem></asp:ListItem>
                                                                        <asp:ListItem>Self</asp:ListItem>
                                                                        <asp:ListItem>AIA</asp:ListItem>
                                                                        <asp:ListItem>Others</asp:ListItem>
                                                                        <asp:ListItem>Internal staff</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Hospital Waiver </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlhospitalrwaiver" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlhospitalrwaiver_SelectedIndexChanged">
                                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="Pending" Value="PENDING"></asp:ListItem>
                                                                        <asp:ListItem Text="Approved" Value="APPROVED"></asp:ListItem>
                                                                        <asp:ListItem Text="Declined" Value="DECLINED"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-2 control-label">Hospital Action Date </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtHospitalActionDate" runat="server" CssClass="form-control" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtHospitalActionDate_CalendarExtender" TargetControlID="txtHospitalActionDate" runat="server"
                                                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Hospital Decline Reason </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlHospitalDeclineReason" runat="server" CssClass="form-control ReadOnly">
                                                                        <asp:ListItem></asp:ListItem>
                                                                        <asp:ListItem>Self</asp:ListItem>
                                                                        <asp:ListItem>AIA</asp:ListItem>
                                                                        <asp:ListItem>Others</asp:ListItem>
                                                                        <asp:ListItem>Internal staff</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Waiver Approved </label>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlWApproved" runat="server" CssClass="form-control ReadOnly">
                                                                        <asp:ListItem></asp:ListItem>
                                                                        <asp:ListItem Value="PENDING" Text="Pending"></asp:ListItem>
                                                                        <asp:ListItem Value="REJECTED" Text="REJECTED"></asp:ListItem>
                                                                        <asp:ListItem Value="HALFWAIVER" Text="Half-Waiver"></asp:ListItem>
                                                                        <asp:ListItem Value="FULLWAIVER" Text="Full-Waiver"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <ul class="list-unstyled list-inline pull-right">
                                                <li>
                                                    <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel3()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button></li>
                                                <li>
                                                    <button type="button" class="btn btngreen" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel5()">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></button></li>
                                            </ul>
                                        </div>
                                    </asp:Panel>
                                    <%--TAB 05-Waiver Applications END--%>

                                    <%--TAB 06-Assign doc & verifier Start--%>
                                    <asp:Panel ID="pnlmenu6" runat="server" Visible="false">
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <div class="form-group">

                                                    <label for="inputEmail3" class="col-sm-2 control-label">Assign To Department </label>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <asp:UpdatePanel ID="updpnlDepartmentOUID" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtDepartmentOUname" CssClass="optional form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                    <span hidden>
                                                                        <asp:TextBox runat="server" ID="txtDepartmentOUID" CssClass="optional form-control "></asp:TextBox>

                                                                    </span>

                                                                    <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">

                                                                        <span class="downarrrow">
                                                                            <asp:ImageButton AlternateText="DEPARTMENTOU_txtDepartmentOUID_txtDepartmentOUname_updpnlDepartmentOUID" runat="server" ID="imgbtntrigerDEPARTMENTOU" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                        </span>

                                                                        <span class="CrossMark">
                                                                            <asp:ImageButton AlternateText="DEPARTMENTOU_txtDepartmentOUID_txtDepartmentOUname" runat="server" ID="imgbtnClearDEPARTMENTOU" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                        </span>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Panel ID="Panel9" runat="server">
                                                        <div class="box-body">
                                                            <div class="form">
                                                                <asp:HiddenField ID="HiddenField14" runat="server" />
                                                                <div class="table-responsive table--no-card m-b-30">
                                                                    <asp:GridView ID="gv2ndtabqstlevel" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                        AutoGenerateColumns="False" OnRowDataBound="gv2ndtabqstlevel_RowDataBound"
                                                                        border-radius="40px"
                                                                        CellPadding="2"
                                                                        ForeColor="#333333"
                                                                        HorizontalAlign="Center"
                                                                        PageSize="10"
                                                                        CssClass="table table-borderless table-striped">
                                                                        <%-- <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                            <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />--%>
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsno1st" runat="server" Text="<%#Bind('REFERENCE_1')%>"></asp:Label>
                                                                                </ItemTemplate>

                                                                                <ItemStyle Width="8%" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbluniqid1st" runat="server" Text="<%#Bind('REFERENCE_2')%>"></asp:Label>
                                                                                </ItemTemplate>

                                                                                <ItemStyle Width="8%" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>




                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRMR_PAYMENT_AMT" runat="server" Text='<%# Bind("SECRETARY_DOMAIN") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkMR_PAYMENT_AMT" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Secretary AD Domain" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="SECRETARY_DOMAIN" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRCSECReetaryadid" runat="server" Text='<%# Bind("SECRETARY_ADID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkCANCELLATION_CHARGES" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Secretary ADID" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="SECRETARY_ADID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsecretaryname" runat="server" Text='<%# Bind("SECRETARY_NAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkCANCELLATION_CHARGES" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Secretary Name" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="SECRETARY_NAME" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsecredaryemail" runat="server" Text='<%# Bind("SECRETARY_EMAIL") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkCANCELLATION_CHARGES" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Secretary Email" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="SECRETARY_EMAIL" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsecretarypho" runat="server" Text='<%# Bind("SECRETARY_PH_NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkCANCELLATION_CHARGES" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Secretary Contact No" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="SECRETARY_PH_NO" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblunfromdate" runat="server" Text='<%# Bind("UNAVAILABLE_FROM_DATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkCANCELLATION_CHARGES" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Unavailable From Date" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="UNAVAILABLE_FROM_DATE" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbluntodate" runat="server" Text='<%# Bind("UNAVAILABLE_TO_DATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkCANCELLATION_CHARGES" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Unavailable To Date" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="UNAVAILABLE_TO_DATE" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>



                                                                        </Columns>
                                                                        <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                        <RowStyle CssClass="GridviewRowStyle" />
                                                                        <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                    </asp:GridView>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group">
                                                    <laLkBtnDoc1_Clickbel for="inputEmail3" class="col-sm-2 control-label" style="">Assign Doctor </laLkBtnDoc1_Clickbel>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtAssignDoctor" runat="server" ToolTip="HRN" CssClass="optional form-control" ReadOnly="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    &nbsp;
                                             <asp:LinkButton ID="LkBtnBios" runat="server" CausesValidation="False" CssClass="LnkbtnPatient"
                                                 OnClick="LkBtnDoc1_Click">Select</asp:LinkButton>
                                                </div>

                                                <div class="form-group">
                                                    <asp:Panel ID="pnlAssignDoctor" runat="server">
                                                        <div class="box-body">
                                                            <div class="form">
                                                                <asp:HiddenField ID="HiddenField7" runat="server" />
                                                                <div class="table-responsive table--no-card m-b-30">
                                                                    <asp:GridView ID="gvassigndoctor" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                        AutoGenerateColumns="False" OnRowDataBound="gvassigndoctor_RowDataBound"
                                                                        border-radius="40px"
                                                                        CellPadding="2"
                                                                        ForeColor="#333333"
                                                                        HorizontalAlign="Center"
                                                                        PageSize="10"
                                                                        CssClass="table table-borderless table-striped">
                                                                        <%-- <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                            <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />--%>
                                                                        <Columns>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkbtnpopupDOCID" runat="server" Text="<%#Bind('EMP_NO')%>" CommandArgument='<%#Eval("EMP_NO")+","+ Eval("UNIQUE_ID")%>'></asp:LinkButton>

                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblid11fx" runat="server" Text="Doctor Employee Number"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmcr_no" runat="server" Text="<%#Bind('DEPT_DESC')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblmcrx" runat="server" Text="Department Name"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblShort_Name" runat="server" Text="<%#Bind('NAME')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblDescriptionx" runat="server" Text="Doctor Name"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmcr" runat="server" Text="<%#Bind('MCR_NO')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblmcrx" runat="server" Text="MCR Number"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblseqno" runat="server" Text="<%#Bind('SEQ_NO')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblseqnox" runat="server" Text="Sequence Number"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrejrea" runat="server" Text="<%#Bind('REJ_REASON')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblrejreax" runat="server" Text="Reject Reason"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrejreatime" runat="server" Text="<%#Bind('REJ_TIME_STAMP')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblrejreatimxe" runat="server" Text="Reject TimeStamp"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblremarks" runat="server" Text="<%#Bind('REMARKS')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblremarksx" runat="server" Text="Remarks"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsts" runat="server" Text="<%#Bind('STATUS')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblsts01x" runat="server" Text="Status"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnDeletedocter" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDeletedocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")%>' />
                                                                                    <controlstyle cssclass="btn btnred"></controlstyle>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnCompletedocter" runat="server" Text="Complete" CssClass="btnselect" OnClick="btnCompletedocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")+","+ Eval("DOC_SEQ_ID")%>' Visible="false" />
                                                                                    <controlstyle cssclass="btn btngreen"></controlstyle>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnRejectdocter" runat="server" Text="Reject" CssClass="btnDelete" OnClick="btnRejectdocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")%>' Visible="false" />
                                                                                    <controlstyle cssclass="btn btngreen"></controlstyle>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                        <RowStyle CssClass="GridviewRowStyle" />
                                                                        <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                    </asp:GridView>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Assign Verifier </label>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtAssignVerifier" runat="server" ToolTip="HRN" CssClass="optional form-control" ReadOnly="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    &nbsp;
                                             <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="LnkbtnPatient"
                                                 OnClick="LkBtnDoc2_Click">Select</asp:LinkButton>
                                                </div>

                                                <div class="form-group">

                                                    <asp:Panel ID="pnlAssignVerfier" runat="server">
                                                        <div class="box-body">
                                                            <div class="form">
                                                                <asp:HiddenField ID="HiddenField8" runat="server" />
                                                                <div class="table-responsive table--no-card m-b-30">
                                                                    <asp:GridView ID="gvassignverifier" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                        AutoGenerateColumns="False" OnRowDataBound="gvassignverifier_RowDataBound"
                                                                        border-radius="40px"
                                                                        CellPadding="2"
                                                                        ForeColor="#333333"
                                                                        HorizontalAlign="Center"
                                                                        PageSize="10"
                                                                        CssClass="table table-borderless table-striped">
                                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                        <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                                        <Columns>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkbtnpopupDOCIDverifier" runat="server" Text="<%#Bind('EMP_NO')%>" CommandArgument='<%#Eval("EMP_NO")+","+ Eval("UNIQUE_ID")%>'></asp:LinkButton>

                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblid11fverifierz" runat="server" Text="Verifier ID"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblShort_Nameverifier" runat="server" Text="<%#Bind('NAME')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblDescriptionverifierz" runat="server" Text="Verifier Name"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmcr_noverifier" runat="server" Text="<%#Bind('DEPT_DESC')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblmcrverifierz" runat="server" Text="Verifier Department"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>


                                                                            <%--<asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmcrverifier" runat="server" Text="<%#Bind('MCR_NO')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblmcrverifierz" runat="server" Text="MCR Number"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>--%>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblseqnoverifier" runat="server" Text="<%#Bind('SEQ_NO')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblseqnoverifierz" runat="server" Text="Sequence Number"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrejreaverifier" runat="server" Text="<%#Bind('REJ_REASON')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblrejreaverifierz" runat="server" Text="Reject Reason"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrejreatimeverifier" runat="server" Text="<%#Bind('REJ_TIME_STAMP')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblrejreatimeverifierz" runat="server" Text="Reject TimeStamp"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblremarksverifier" runat="server" Text="<%#Bind('REMARKS')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblremarksverifierz" runat="server" Text="Remarks"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstsverifier" runat="server" Text="<%#Bind('STATUS')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblsts01verifierx" runat="server" Text="Status"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnDeletedocterverifier" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDeletedocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")%>' />
                                                                                    <controlstyle cssclass="btn btnred"></controlstyle>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                                        <tr>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnCompletedocterverifier" runat="server" Text="Complete" CssClass="btnselect" OnClick="btnCompletedocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")%>' Visible="false" />
                                                                                    <controlstyle cssclass="btn btngreen"></controlstyle>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnRejectdocterverifier" runat="server" Text="Reject" CssClass="btnDelete" OnClick="btnRejectdocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")%>' Visible="false" />
                                                                                    <controlstyle cssclass="btn btngreen"></controlstyle>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                        <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                        <RowStyle CssClass="GridviewRowStyle" />
                                                                        <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                    </asp:GridView>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                                <ul class="list-unstyled list-inline pull-right">
                                                    <li>
                                                        <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel4()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button>
                                                        <asp:LinkButton Text="" ID="lnkbtnnextassignation" Style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" class="btn btngreen next-step" OnClick="lnkbtnnextattachments_Click" runat="server">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></asp:LinkButton>

                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <%--TAB 06-Assign doc & verifier END--%>

                                    <%--TAB 07-Enquiry History Start--%>
                                    <asp:Panel ID="pnlEnquiry" Visible="true" runat="server">
                                        <div class="box-body">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div class="accordion-group">
                                                        <div class="accordion-heading">
                                                            <span class="accordion-toggle" data-toggle="collapse" data-parent="toggle" href="#collapseOne">
                                                                <i class="fa fa-minus"></i>&nbsp; Remarks
                                                            </span>
                                                        </div>
                                                        <div id="collapseOne" class="accordion-body collapse in">


                                                            <div class="accordion-inner">
                                                                <asp:Panel ID="Panel1" Width="90%" runat="server" Style="margin-left: 20px;">
                                                                    <div class="form-horizontal">
                                                                        <div class="box-body">
                                                                            <div class="form-group">
                                                                                <div class="form-group">
                                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Remarks Templates </label>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="form-group">
                                                                                            <asp:DropDownList ID="ddlRemarks" AutoPostBack="true" ToolTip="Remarks Templates" runat="server" OnSelectedIndexChanged="ddlRemarks_SelectedIndexChanged" CssClass="form-control">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>

                                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Description</label>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="form-group">
                                                                                            <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" ToolTip="Description" CssClass=" MultiLine_Textbox  form-control optional" Style="height: 80px;"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="form-group">
                                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Target Audience </label>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="form-group">
                                                                                            <asp:DropDownList ID="ddlTarget" AutoPostBack="true" ToolTip="Target Audience" runat="server" CssClass="form-control">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>

                                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Remarks Date </label>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="form-group">
                                                                                            <asp:TextBox ID="txtRemarkdte" runat="server" Enabled="false" ToolTip="Remarks Date" CssClass=" form-control optional" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtRemarkdte" runat="server"
                                                                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-left: -15px">
                                                                                <asp:LinkButton ID="lnkbtnaddremarks" runat="server" CssClass="btn btngreen" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px; width: 90px" OnClick="lnkbtnaddremarks_Click"><i class="fa fa-plus" aria-hidden="true"  ></i> Add </asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkbtnremarksclear" runat="server" CssClass="btn btns-save btnred" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; width: 90px; border-radius: 15px" OnClick="lnkbtnremarksclear_Click"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear</asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlRemarksresultgrid" runat="server" Visible="true">
                                                                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                                                                        <div class="box-header with-border TotalRecord">
                                                                            <div class="box-title">
                                                                                <label class="text-right" style="color: white">Remarks Total Record : <span id="lblpnlremRecord" runat="server"></span></label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="box-body">
                                                                            <div class="form">
                                                                                <asp:HiddenField ID="HiddenField13" runat="server" />
                                                                                <div class="table-responsive table--no-card m-b-30">
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
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                  <asp:Label ID="lnkbtnRemarkID" runat="server" CssClass="styleCellsLeft" Text='<%# Eval("SHORT_NAME") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                <HeaderTemplate>
                                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                        <tr>
                                                                                                            <td class="gridtabletd">
                                                                                                                <asp:LinkButton ID="lnkbtnEnq_Date1" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                                    Text="Remarks Templates" Font-Names="Arial" ForeColor="White"
                                                                                                                    CommandArgument="REGRMK_ID"></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                               <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                                            </asp:TemplateField> 
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="lblremarks" runat="server" Text="<%#Bind('remarks')%>" TextMode="MultiLine" Style="resize: none; height: 50px;" Width="100%" ReadOnly="true" CssClass="MultiLine_Textbox optional"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                                <HeaderTemplate>
                                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                        <tr>
                                                                                                            <td class="gridtabletd">
                                                                                                                <asp:Label ID="lnkbtnname" runat="server" Text="Description"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                
                                                                                               <ItemStyle HorizontalAlign="Left" Width="40%" />
                                                                                            </asp:TemplateField> 
                                                                                            <asp:TemplateField HeaderText="Target Audience">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblReportPendingItemID" runat="server" CssClass="styleCellsLeft" Text='<%# Eval("TARG_AUD") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="styleCellsHeaderLOVWnd text-left" />
                                                                                                <ItemStyle HorizontalAlign="left" />
                                                                                            </asp:TemplateField> 
                                                                                            <asp:TemplateField HeaderText="Remark Date">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblRemarkDate" runat="server" CssClass="styleCellsLeft" Text='<%#Bind("REMARKS_DATE","{0:dd-MM-yyy HH:mm}")%>' ></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="styleCellsHeaderLOVWnd text-left" />
                                                                                                <ItemStyle HorizontalAlign="left" />
                                                                                            </asp:TemplateField>  
                                                                                            <asp:TemplateField HeaderText="Created User">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblModifiedusr" runat="server" CssClass="styleCellsLeft" Text='<%# Eval("MODIFIED_BY") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="styleCellsHeaderLOVWnd text-left" />
                                                                                                <ItemStyle HorizontalAlign="left" />
                                                                                            </asp:TemplateField> 
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDelete_Click" CommandArgument='<%#Eval("REMARK_ID")+","+ Eval("Request_ID")+","+ Eval("REGRMK_ID")%>' />
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
                                                                                                <ItemStyle HorizontalAlign="Left" />
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
                                                                                    <asp:Repeater ID="rptPagerRemarks" runat="server">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                                Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPageRemarks_Click"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="accordion-group">
                                                        <div class="accordion-heading">
                                                            <span class="accordion-toggle" data-toggle="collapse" data-parent="toggle" href="#collapseOneTwo">
                                                                <i class="fa fa-minus"></i>&nbsp; Enquiry
                                                            </span>
                                                        </div>
                                                        <div id="collapseOneTwo" class="accordion-body collapse in">
                                                            <div class="accordion-inner">
                                                                <asp:Panel ID="Panel4" Width="90%" runat="server" Style="margin-left: 20px;">
                                                                    <div class="form-horizontal">
                                                                        <div class="box-body">
                                                                            <div class="form-group">
                                                                                <div class="form-group">
                                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Caller's Name</label>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="form-group">
                                                                                            <asp:TextBox ID="txtCName" runat="server" CssClass=" form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>

                                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Caller's Enquiry </label>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="form-group">
                                                                                            <asp:TextBox ID="txtCEnq" runat="server" TextMode="MultiLine" Style="resize: none; height: 80px" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="form-group">
                                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Staff's Name </label>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="form-group">
                                                                                            <asp:TextBox ID="txtSName" runat="server" CssClass=" form-control optional"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>

                                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Staff Response </label>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="form-group">
                                                                                            <asp:TextBox ID="txtSRes" runat="server" TextMode="MultiLine" Style="resize: none; height: 80px" CssClass=" form-control optional"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="form-group">
                                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Enquiry Status  </label>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="form-group">
                                                                                            <asp:DropDownList ID="ddlEnqStatus"   runat="server" CssClass="form-control optional">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>

                                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Remarks </label>
                                                                                    <div class="col-sm-4">
                                                                                        <div class="form-group">

                                                                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Style="resize: none; height: 80px" CssClass=" form-control optional"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-left: -15px">
                                                                                <asp:LinkButton ID="lnkbtnenquiryadd" runat="server" CssClass="btn btngreen" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px; width: 90px" OnClick="lnkbtnenquiryadd_Click"><i class="fa fa-plus" aria-hidden="true"  ></i> Add </asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkbtnenquiryclear" runat="server" CssClass="btn btns-save btnred" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px; width: 90px" OnClick="lnkbtnenquiryclear_Click"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear</asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlEnqresultgrid" runat="server" Visible="true">
                                                                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                                                                        <div class="box-header with-border TotalRecord">
                                                                            <div class="box-title">
                                                                                <label class="text-right" style="color: white">Enquiry Total Record : <span id="lblpnlEnquiryRecord" runat="server"></span></label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="box-body">
                                                                            <div class="form">
                                                                                <asp:HiddenField ID="HiddenField5" runat="server" />
                                                                                <div class="table-responsive table--no-card m-b-30">
                                                                                    <asp:GridView ID="gvListEnquiry" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                                        AutoGenerateColumns="False"
                                                                                        CellPadding="2"
                                                                                        ForeColor="#333333"
                                                                                        HorizontalAlign="Center" OnRowDataBound="gvListEnquiry_RowDataBound"
                                                                                        PageSize="10"
                                                                                        CssClass="table table-borderless table-striped">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkbtnEnq_Date" runat="server" Text='<%#Bind("Enq_Date","{0:dd-MM-yyy}")%>' CommandArgument='<%#Bind("mr_enq_id")%>' OnClick="lnkbtnEnq_Date_Click">
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <HeaderTemplate>
                                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                        <tr>
                                                                                                            <td class="gridtabletd">
                                                                                                                <asp:LinkButton ID="lnkbtnEnq_Date1" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                                    Text="Enquiry Date" Font-Names="Arial" ForeColor="White"
                                                                                                                    CommandArgument="Enq_Date"></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
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
                                                                                                                    Text="Caller's Name" Font-Names="Arial" ForeColor="White"
                                                                                                                    CommandArgument="Callers_Name"></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="lblCallers_Enquiry" runat="server" Text="<%#Bind('Callers_Enquiry')%>" TextMode="MultiLine" Style="resize: none; background:transparent; height: 50px" ReadOnly="true" Width="100%" CssClass="MultiLine_Textbox"></asp:TextBox>
                                                                                                </ItemTemplate>

                                                                                                <HeaderTemplate>
                                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                        <tr>
                                                                                                            <td class="gridtabletd">
                                                                                                                <asp:LinkButton ID="lnkbtnCallers_Enquiry" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                                    Text="Caller's Enquiry" Font-Names="Arial" ForeColor="White"
                                                                                                                    CommandArgument="Callers_Enquiry"></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>

                                                                                                    <asp:Label ID="lblStaffs_Name" runat="server" Text="<%#Bind('Staffs_Name')%>"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderTemplate>
                                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                        <tr>
                                                                                                            <td class="gridtabletd">
                                                                                                                <asp:LinkButton ID="lnkbtnStaffs_Name" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                                    Text="Staff's Name" Font-Names="Arial" ForeColor="White"
                                                                                                                    CommandArgument="Staffs_Name"></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="lblStaffs_Response" runat="server" Text="<%#Bind('Staffs_Response')%>" TextMode="MultiLine" Style="resize: none; background:transparent; height: 50px" ReadOnly="true" Width="100%" CssClass="MultiLine_Textbox"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                                <HeaderTemplate>
                                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                        <tr>
                                                                                                            <td class="gridtabletd">
                                                                                                                <asp:LinkButton ID="lnkbtnStaffs_Response" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                                    Text="Staff Response" Font-Names="Arial" ForeColor="White"
                                                                                                                    CommandArgument="BE_ID"></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="lblreference_1" runat="server" Text="<%#Bind('reference_1')%>" TextMode="MultiLine" Style="resize: none; background:transparent; height: 50px" Width="100%" ReadOnly="true" CssClass="MultiLine_Textbox"></asp:TextBox>
                                                                                                </ItemTemplate>

                                                                                                <HeaderTemplate>
                                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                        <tr>
                                                                                                            <td class="gridtabletd">
                                                                                                                <asp:LinkButton ID="lnkbtnreference_1" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                                    Text="Enquiry Status" Font-Names="Arial" ForeColor="White"
                                                                                                                    CommandArgument="reference_1"></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="btnDeleteenq" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDeleteenq_Click" CommandArgument='<%#Bind("mr_enq_id")%>' />
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
                                                                                                <ItemStyle HorizontalAlign="Left" />
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
                                                                                    <asp:Repeater ID="rptPagerEnquiry" runat="server">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                                Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPageEnquiry_Click"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>


                                        <ul class="list-unstyled list-inline pull-right">
                                            <li>
                                                <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel5()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button></li>
                                            <li>
                                                <button type="button" class="btn btngreen next-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel7()">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></button>
                                    </asp:Panel>
                                    <%--TAB 07-Enquiry History END--%>

                                    <%--TAB 08-PendingItems History Start--%>
                                    <asp:Panel ID="pnlPendingItems" Visible="false" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <div class="form-group">

                                                        <label for="inputEmail3" class="col-sm-2 control-label">Pending Items </label>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlPenItems" AutoPostBack="true" runat="server" CssClass="form-control optional" OnSelectedIndexChanged="ddlPenItems_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-2 control-label">Due Days </label>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtDueDays" runat="server" CssClass=" form-control optional" onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="form-group">

                                                        <label for="inputEmail3" class="col-sm-2 control-label">Pending Status </label>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlStatus" AutoPostBack="true" runat="server" CssClass="form-control optional" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                                    <asp:ListItem Value="INDIVIDUALREQUEST">Individual Request</asp:ListItem>
                                                                    <asp:ListItem Value="BATCHREQUESTBYPATIENT">Batch Request By Patient</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-2 control-label"></label>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="form-group">

                                                        <label for="inputEmail3" class="col-sm-2 control-label">Start Date </label>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtStartDate" runat="server" CssClass=" form-control optional" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" TargetControlID="txtStartDate" runat="server"
                                                                    Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                            </div>
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-2 control-label">Close Date </label>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtCloseDate" runat="server" CssClass=" form-control"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtCloseDate_CalendarExtender" TargetControlID="txtCloseDate" runat="server"
                                                                    Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </td>
                                            </tr>
                                            <tr>

                                                <td>
                                                    <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-left: -15px">
                                                        <asp:LinkButton ID="lnkbtnpendingItemsadd" runat="server" CssClass="btn btngreen" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px; width: 90px" OnClick="lnkbtnpendingItemsadd_Click"><i class="fa fa-plus" aria-hidden="true"  ></i> Add </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnClear" runat="server" CssClass="btn btns-save btnred" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px; width: 90px" OnClick="lbtnClear_Click"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear</asp:LinkButton>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                                                        <div class="box-header with-border TotalRecord">
                                                            <div class="box-title">
                                                                <label class="text-right" style="color: white">Total Record : <span id="lblpnlPendingItemsRercord" runat="server"></span></label>
                                                            </div>
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="form">
                                                                <asp:HiddenField ID="HiddenField6" runat="server" />
                                                                <div class="table-responsive">
                                                                    <asp:GridView ID="gvListPendingItems" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                        AutoGenerateColumns="False" OnRowDataBound="gvListPendingItems_RowDataBound"
                                                                        CellPadding="2"
                                                                        ForeColor="#333333"
                                                                        HorizontalAlign="Center"
                                                                        PageSize="10"
                                                                        CssClass="table table-borderless table-striped">
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPenSeq_ID" runat="server" Text="<%#Bind('PenSeq_ID')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPen_ID" runat="server" Text="<%#Bind('Pen_ID')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblpendingshortname" runat="server" Text="<%#Bind('pending_short_name')%>" CommandArgument='<%#Eval("PenSeq_ID")%>' OnClick="lblpendingshortname_Click">
                                           
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnpendingshortname" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Pending Items" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="pending_short_name"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblDueDays" runat="server" Text="<%#Bind('Due_Days')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnDueDays" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Due Days" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="Due_Days"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPendingStatus" runat="server" Text="<%#Bind('Pending_Status')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnPending_Status" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Pending Status" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="Pending_Status"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblstartdate" runat="server" Text='<%#Bind("start_Date", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnStartdate" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Start Date" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="start_Date"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblClosedte" runat="server" Text='<%#Bind("Close_Date", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Close Date" Font-Names="Arial" ForeColor="White"
                                                                                                    CommandArgument="Close_Date"></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnclose" runat="server" Text="Close" CssClass="btnDelete" OnClick="btnclosependingitms_Click" CommandArgument='<%#Eval("PenSeq_ID")+","+ Eval("Pen_ID")+","+ Eval("Request_ID") +","+ Eval("Pending_Status")+","+ Eval("Due_days")+","+ Eval("Request_ID")%>' />
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
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btndeletependingitms_Click" CommandArgument='<%#Eval("PenSeq_ID")+","+ Eval("Pen_ID")+","+ Eval("Request_ID") +","+ Eval("Pending_Status")+","+ Eval("Due_days")+","+ Eval("Request_ID")%>' />
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
                                                                                <ItemStyle HorizontalAlign="Left" />
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
                                                                    <asp:Repeater ID="rptPagerPendingItems" runat="server">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkPagePendingItems" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPagePendingItems_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>

                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <ul class="list-unstyled list-inline pull-right">
                                                        <li>
                                                            <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel6()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button></li>
                                                        <li>
                                                            <button type="button" class="btn btngreen next-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel8()">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></button>
                                                    </ul>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%--TAB 08-PendingItems History END--%>

                                    <%--TAB 09-Process History Start--%>
                                    <asp:Panel ID="pnlProcessHistory" Visible="false" runat="server">
                                        <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed;">
                                            <div class="box-header with-border TotalRecord">
                                                <div class="box-title">
                                                    <label class="text-right" style="color: white">Total Record : <span id="lblpnlprocesshistory" runat="server"></span></label>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <div class="form">
                                                    <asp:HiddenField ID="HiddenField11" runat="server" />
                                                    <div class="table-responsive table--no-card m-b-30">
                                                        <asp:GridView ID="gvprocesshistorygrid" runat="server" GridLines="Vertical" RowStyle-Wrap="true" OnRowDataBound="gvprocesshistorygrid_RowDataBound"
                                                            AutoGenerateColumns="False"
                                                            CellPadding="2"
                                                            ForeColor="#333333"
                                                            HorizontalAlign="Center"
                                                            PageSize="10"
                                                            CssClass="table table-borderless table-striped">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPenSeq_ID" runat="server" Text="<%#Bind('SEQ_ID')%>"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:LinkButton ID="lnkbtnpendingshortname" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                        Text="Seq No" Font-Names="Arial" ForeColor="White"
                                                                                        CommandArgument="SEQ_ID"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpendingshortname" runat="server" Text="<%#Bind('TASK_SHORT_NAME')%>" CommandArgument='<%#Eval("SEQ_ID")%>'>
                                           
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:LinkButton ID="lnkbtnpendingshortname" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                        Text="Task Name" Font-Names="Arial" ForeColor="White"
                                                                                        CommandArgument="TASK_SHORT_NAME"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblDueDays" runat="server" Text="<%#Bind('SUB_TASK')%>"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:LinkButton ID="lnkbtnDueDays" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                        Text="Sub Task" Font-Names="Arial" ForeColor="White"
                                                                                        CommandArgument="SUB_TASK"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblClosedte" runat="server" Text='<%#Bind("START_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                        Text="Task Start Date" Font-Names="Arial" ForeColor="White"
                                                                                        CommandArgument="START_DATE"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblDaysAllowed" runat="server" Text="<%#Bind('DUE_DAYS')%>"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:LinkButton ID="lnkbtnDueDays" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                        Text="Days Allowed" Font-Names="Arial" ForeColor="White"
                                                                                        CommandArgument="DUE_DAYS"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEND_DATE" runat="server" Text='<%#Bind("END_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                        Text="Task Due Date" Font-Names="Arial" ForeColor="White"
                                                                                        CommandArgument="END_DATE"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCOMPLETED_DATE" runat="server" Text='<%#Bind("COMPLETED_DATE", "{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                        Text="Completed Date" Font-Names="Arial" ForeColor="White"
                                                                                        CommandArgument="COMPLETED_DATE"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTRANS_STATUS" runat="server" Text="<%#Bind('TRANS_STATUS')%>"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                        Text="Pending Status" Font-Names="Arial" ForeColor="White"
                                                                                        CommandArgument="TRANS_STATUS"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMODIFIED_BY" runat="server" Text="<%#Bind('MODIFIED_BY')%>"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:LinkButton ID="lnkbtnMODIFIED_BY" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                        Text="Modified User" Font-Names="Arial" ForeColor="White"
                                                                                        CommandArgument="MODIFIED_BY"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRemarks" runat="server" Text="<%#Bind('Remarks')%>"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:LinkButton ID="lnkbtnRemarks" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                        Text="Remarks" Font-Names="Arial" ForeColor="White"
                                                                                        CommandArgument="Remarks"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
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
                                                        <asp:Repeater ID="rptPagerProcessHistory" runat="server">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkPageProcessHistory" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                    Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPageProcessHistory_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <ul class="list-unstyled list-inline pull-right">
                                            <li>
                                                <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px; width: 90px" onclick="showpanel7()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button></li>

                                        </ul>
                                    </asp:Panel>
                                    <%--TAB 09-Process History END--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Progress Bar Code End -->
            </div>

            <!-- Update Process Popup start -->
            <asp:Panel runat="server" ID="pnlupdateremarksandprocess">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelModal6success" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="btnerrorsuccess" runat="server" />
                                    <cc1:ModalPopupExtender ID="Modelpopuperrorsuccess" runat="server" BackgroundCssClass="modal-background "
                                        DynamicControlID="btnerrorsuccess" PopupControlID="pnlpopuperrorsuccess" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="btnerrorsuccess" />
                                    <asp:Panel ID="pnlpopuperrorsuccess" runat="server" BackColor="#e3f6fd"
                                        EnableTheming="True" Style="text-align: center; resize: none; align-content: flex-start; align-items: flex-start; border: 5px solid #2aa7ed; border-radius: 20px; padding: 10px; " Width="447px">
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
                                                                        <label class="RegPopuptxt">Remarks:&nbsp;&nbsp;</label></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtProcessCompletedRemarks" runat="server" CssClass="form-control" Visible="true"></asp:TextBox></td>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 5%"></td>
                                                                        <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt;">
                                                                            <asp:Label Text="Are you sure want to update the process?" ID="lblupdateprocesscontent" runat="server" />
                                                                        </td>
                                                                    </tr>
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
                                                            Text="YES" OnClick="btnConfirmprocessStatus_Click" />
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
                                            <table style="width: 100%; text-align: center" bgcolor="#f4eaff">
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
                                                                        <label class="RegPopuptxt">Recall Reason:&nbsp;&nbsp;</label></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRecallRemarks" MaxLength="50" runat="server" TextMode="MultiLine" CssClass="MultiLine_Textbox form-control" Visible="true"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%"></td>
                                                                    <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt;">Are you sure want to Recall the process?</td>
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
                                                    <td>&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td colspan="3" style="text-align: center">
                                                        <asp:Button ID="btnrecallOK" runat="server" align="right" CssClass="btn btngreen"
                                                            Text="YES" OnClick="btnrecallOK_Click" />
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
                                            <table style="width: 100%; text-align: center" bgcolor="#f4eaff">
                                                <tr>
                                                    <td style="width: 12%"></td>
                                                    <td align="center">
                                                        <asp:Label Style="text-align: center" ID="Label4" runat="server" Text="Insert Delay Reason" ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 96px">

                                                        <asp:Panel ID="Panel6" runat="server" Height="67px">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <label class="RegPopuptxt">Delay Reason:&nbsp;&nbsp;</label></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlDelayReason" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%"></td>
                                                                    <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt;">Are you sure want to Insert Delay Reason?</td>
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
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align: center">
                                                        <asp:Button ID="btnDelayReasonOK" runat="server" align="right" CssClass="btn btngreen"
                                                            Text="YES" OnClick="btnDelayReasonOK_Click" />
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


            <!-- Bio-Data Popup start -->
            <asp:Panel runat="server" ID="pnlbiodatta" Visible="false">

                   <asp:LinkButton ID="lnkbtnProducts2" runat="server" />
                                    <cc1:ModalPopupExtender ID="mpePnlbiodattfc0001" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="lnkbtnProducts2" PopupControlID="pnlPdtPlt22" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="lnkbtnProducts2" />
                 
                <asp:Panel ID="pnlPdtPlt22" runat="server" Width="950">
                    <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">

                        <div class="modal-dialog modal-lg panelBiodata" style="width: 50%;">
                            <div class="modal-content PopupModelContent">
                                <div class="text-center" style="background-color: #304863;">
                                    <h4 class="modal-title" style="font-weight: bold; font-size: 16px; color: white"></h4>
                                </div>
                                <div class="modal-body" style="border-radius: 20px;">
                                    <div class="nav-tabs-custom">
                                        <div class="box box-primary box-solid">
                                            <div class="box-header Popboxheader" style="" >
                                                <ul class="nav nav-tabs" style="">
                                                    <li class="" style="background-color: #e3f6fd; border-radius: 5px;"><a href="#tabPrd_1" data-toggle="tab"
                                                        style="font-weight: 500; font-size: 15px; color: black">
                                                        <asp:Label runat="server" ID="lbl2ndLvlTabTilte" Text="Bio-Data"></asp:Label>
                                                    </a></li>
                                                    <div style="text-align: right">
                                                        <asp:ImageButton ID="ImageButton2" runat="server" Height="38px" ImageUrl="Images/PopColse.png"
                                                            Width="38px" ToolTip="Close" />
                                                    </div>
                                                </ul>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                        <%-- <asp:ImageButton ID="btnbiodatapopupnew" runat="server" Height="28px" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                                            Width="28px" ToolTip="New Record" OnClick="btnbiodatapopupnew_Click" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                        <asp:ImageButton ID="btnbiodatapopupsearch" runat="server" Height="28px" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                                            Width="28px" ToolTip="Search Record" OnClick="btnbiodatapopupsearch_Click" />
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                        </div>
                                        <label for="inputEmail3" class="col-sm-2 control-label">MRN  </label>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <asp:TextBox ID="txthrnbiodata" runat="server" CssClass="form-control" Style="text-transform: uppercase;" ReadOnly="false" onkeypress="return Biodataenter1_click(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                        </div>
                                        <label for="inputEmail3" class="col-sm-2 control-label">Name </label>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <asp:TextBox ID="txthrnNamebiodata" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return Biodataenter1_click(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="form">
                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                        <div class="table-responsive table--no-card m-b-30" id="popuppanel">
                                            <asp:GridView ID="gvlistbiodatapopup" runat="server" GridLines="Vertical" RowStyle-Wrap="true" AutoGenerateColumns="false"
                                                CssClass="table table-borderless table-striped table-earning">

                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                <Columns>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnpopupHRN" runat="server" Text="<%#Bind('hrn_id')%>" CommandArgument='<%#Eval("hrn_id")+","+ Eval("short_name")+","+ Eval("patient_id")%>' OnClick="lnkbtnpopupHRN_Click"></asp:LinkButton>

                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                <tr>
                                                                    <td class="gridtabletd" cssclass="GridHeaderText" width="50px">
                                                                        <asp:Label ID="lblid11f" runat="server" Text="MRN" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblordno" runat="server" Text="<%#Bind('short_name')%>"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                <tr>
                                                                    <td class="gridtabletd" cssclass="GridHeaderText" width="50px">
                                                                        <asp:Label ID="lblstrgeresf" runat="server" Text="Name" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                <RowStyle CssClass="GridviewRowStyle" />
                                                <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer" style="text-align: center;">
                                    <asp:Repeater ID="Repeater2" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPage_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="modal-footer" style="text-align: center;">
                                    <asp:Button runat="server" ID="btnCancelCongWH_Name" Text="Cancel" CssClass="btn btnred" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </asp:Panel>
            <!-- Bio-Data Popup End -->

            <!-- Copy Request Popup start -->
            <asp:Panel runat="server" ID="pnlcopyrequest" Visible="false">

                   <asp:LinkButton ID="lnkbtncopyrequest" runat="server" />
                                    <cc1:ModalPopupExtender ID="mpePdtcopyrequest" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="lnkbtncopyrequest" PopupControlID="pnlPdtcopyrequest" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="lnkbtncopyrequest" />
                   <div class="table-responsive table--no-card m-b-30">
                    <asp:Panel ID="pnlPdtcopyrequest" runat="server">
                        <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                            <div class="modal-dialog modal-lg" style="width: 40%;">
                                <div class="modal-content PopupModelContent" >
                                    <div class="text-center" style="background-color: #304863;">
                                        <h4 class="modal-title" style="font-weight: bold; font-size: 16px; color: white"></h4>
                                    </div>
                                    <div class="modal-body" style="border-radius: 20px;">
                                        <div class="nav-tabs-custom">
                                            <div class="box box-primary box-solid">
                                                <div class="box-header Popboxheader">
                                                    <ul class="nav nav-tabs">
                                                        <li class="" style="background-color: #e3f6fd; border-radius: 5px;"><a href="#tabPrd_1" data-toggle="tab"
                                                            style="font-weight: 500; font-size: 15px; color: black">
                                                            <asp:Label runat="server" ID="lbl2ndLvlTabTiltecopyrequest" Text="Copy Request"></asp:Label>
                                                        </a></li>
                                                        <div style="text-align: right">
                                                            <asp:ImageButton ID="ImageButton3" runat="server" Height="38px" ImageUrl="Images/PopColse.png"
                                                                Width="38px" ToolTip="Close" />
                                                        </div>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                            <%--<asp:ImageButton ID="btnbiodatapopupnewcopyrequest" runat="server" Height="28px" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                                                Width="28px" ToolTip="New Record" OnClick="btnbiodatapopupnewcopyrequest_Click" />&nbsp;&nbsp;&nbsp;&nbsp;--%>

                                            <asp:ImageButton ID="btnbiodatapopupsearchcopyrequets" runat="server" Height="28px" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                                                Width="28px" ToolTip="Search Record" OnClick="btnbiodatapopupsearchcopyrequets_Click" />
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                            </div>
                                            <label for="inputEmail3" class="col-sm-2 control-label">
                                                MRN 
                                            </label>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txthrncopyrequest" runat="server" CssClass="form-control" Style="text-transform: uppercase;" ReadOnly="false" onkeypress="return Copyreqenter1_click(event);"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                            </div>
                                            <label for="inputEmail3" class="col-sm-2 control-label">Request Number  </label>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtreqnumbercopyrequest" runat="server" CssClass="form-control" Style="text-transform: uppercase;" ReadOnly="false" onkeypress="return Copyreqenter1_click(event);"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-2">
                                            </div>
                                            <label for="inputEmail3" class="col-sm-4 control-label">Link to Existing Request  </label>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ID="chklnkExistingReq" CssClass="bootformcontrolchk" BorderStyle="none" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-body">
                                        <div class="form">
                                            <asp:HiddenField ID="HiddenField1copyrequest" runat="server" />
                                            <div class="table-responsive table--no-card m-b-30">
                                                <asp:GridView ID="gvlistcopyreqpopup" runat="server" GridLines="Vertical" RowStyle-Wrap="true" AutoGenerateColumns="false"
                                                    CssClass="table table-borderless table-striped table-earning">
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                    <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                    <Columns>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnpopupMRNcopyreq" runat="server" Text="<%#Bind('Request_ID')%>" CommandArgument='<%#Eval("hrn_id")+","+ Eval("Request_ID")+","+ Eval("MR_STATUS")%>' OnClick="lnkbtnpopupMRNcopyreq_Click"></asp:LinkButton>

                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd" cssclass="GridHeaderText" width="50px">
                                                                            <asp:Label ID="lblstrgeresf" runat="server" Text="Request Number" ForeColor="White"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblreqno" runat="server" Text="<%#Bind('hrn_id')%>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd" cssclass="GridHeaderText" width="50px">
                                                                            <asp:Label ID="lblstrgeresf" runat="server" Text="MRN" ForeColor="White"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblREQUESTOR_SHORT_NAME" runat="server" Text="<%#Bind('REQUESTOR_SHORT_NAME')%>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd" cssclass="GridHeaderText" width="50px">
                                                                            <asp:Label ID="lblREQUESTOR_SHORT_NAME0" runat="server" Text="Requestor" ForeColor="White"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                    <RowStyle CssClass="GridviewRowStyle" />
                                                    <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer" style="text-align: center;">
                                        <asp:Repeater ID="Repeatercopyreq" runat="server">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPagecopyreq" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                    Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPagecopyreq_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="modal-footer" style="text-align: center;">
                                        <asp:Button runat="server" ID="btnCancelcopyreq" Text="Cancel" CssClass="btn btnred" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                </div>

            </asp:Panel>
            <!-- Copy Request Popup End -->


            <!-- Doctor Search Popup start -->
            <asp:UpdatePanel ID="updtpnldocterselection" runat="server" Visible="false">
                <ContentTemplate>

                    <div>
                        <tr>
                            <td>

                                 <asp:LinkButton ID="lnkbtndoctorselection" runat="server" />
                                    <cc1:ModalPopupExtender ID="mdlpopupdoctorselection" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="lnkbtndoctorselection" PopupControlID="pnldoctorselection" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="lnkbtndoctorselection" /> 
                                <div class="table-responsive table--no-card m-b-30">
                                    <asp:Panel ID="pnldoctorselection" runat="server">
                                        <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                                            <div class="modal-dialog modal-lg" style="width: 50%;">
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
                                                                            style="font-weight: 500; font-size: 15px; color: black">
                                                                            <asp:Label runat="server" ID="lbldoctorselectionTilte"></asp:Label>
                                                                        </a></li>
                                                                        <div style="text-align: right">
                                                                            <asp:ImageButton ID="ImageButton4" runat="server" Height="38px" ImageUrl="Images/PopColse.png"
                                                                                Width="38px" ToolTip="Close" />
                                                                        </div>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-horizontal">
                                                        <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                                            <%--<asp:ImageButton ID="btndoctorselectionpopupnew" runat="server" Height="28px" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                                                                Width="28px" ToolTip="New Record" OnClick="btndoctorselectionpopupnew_Click" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                            <asp:ImageButton ID="btndoctorselectionsearch" runat="server" Height="28px" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                                                                Width="28px" ToolTip="Search Record" OnClick="btndoctorselectionsearch_Click" />
                                                        </div>
                                                        <asp:Panel ID="pnlverselection" runat="server">
                                                            <div class="form-group">
                                                                <div class="col-sm-2">
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:RadioButton ID="rbtndoctor" OnCheckedChanged="rbtndoctor_CheckedChanged" AutoPostBack="true" runat="server" GroupName="VERSELECTION" />&nbsp; Doctor
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:RadioButton ID="rbtndepsec" runat="server" OnCheckedChanged="rbtndepsec_CheckedChanged" AutoPostBack="true" GroupName="VERSELECTION" />&nbsp; Department Secretary
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:RadioButton ID="rbtnhims" runat="server" OnCheckedChanged="rbtnhims_CheckedChanged" AutoPostBack="true" GroupName="VERSELECTION" />&nbsp; HIMS
                                                                </div>
                                                            </div>
                                                            <br />
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlDepartmentOU" runat="server">
                                                            <div class="form-group">
                                                                <div class="col-sm-2">
                                                                </div>
                                                                <label for="inputEmail3" class="col-sm-3 control-label">Department OU</label>
                                                                <div class="col-sm-5">
                                                                    <div class="form-group">
                                                                        <asp:DropDownList ID="ddlDepartmentOUDoctersel" runat="server" CssClass="form-control" AutoPostBack="false">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlMCRNumber" runat="server">
                                                            <div class="form-group">
                                                                <div class="col-sm-2">
                                                                </div>
                                                                <label for="inputEmail3" class="col-sm-3 control-label">MCR Number</label>
                                                                <div class="col-sm-5">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtMCRNumberdocselection" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return DOClistenter1_click(event);"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlDocempno" runat="server">
                                                            <div class="form-group">
                                                                <div class="col-sm-2">
                                                                </div>
                                                                <asp:Label Text="Doctor Employee Number" ID="lbldoctorEmployeeNo" runat="server" class="col-sm-3 control-label" />
                                                                <div class="col-sm-5">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtdocempnumdocselection" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return DOClistenter1_click(event);"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlSecretaryname" runat="server">
                                                            <div class="form-group">
                                                                <div class="col-sm-2">
                                                                </div>
                                                                <asp:Label Text="Secretary Name" ID="lblSecretaryName" runat="server" class="col-sm-3 control-label" />
                                                                <div class="col-sm-5">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtSecretaryname" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return DOClistenter1_click(event);"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlDoctorname" runat="server">
                                                            <div class="form-group">
                                                                <div class="col-sm-2">
                                                                </div>

                                                                <asp:Label Text="Doctor Name" ID="lblDoctorName" runat="server" class="col-sm-3 control-label" />
                                                                <div class="col-sm-5">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtDoctorNamedocselection" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return DOClistenter1_click(event);"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                        <div class="box-body">
                                                            <div class="form">
                                                                <asp:HiddenField ID="HiddenField9" runat="server" />
                                                                <div class="table-responsive table--no-card m-b-30">
                                                                    <asp:GridView ID="gvlistdoctorselectionpopup" runat="server" GridLines="Vertical" RowStyle-Wrap="true" AutoGenerateColumns="false"
                                                                        CssClass="table table-borderless table-striped table-earning" OnRowDataBound="gvlistdoctorselectionpopup_RowDataBound">

                                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                        <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                                        <Columns>


                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblEMP_NO" runat="server" Text="<%#Bind('EMP_NO')%>" CommandArgument='<%#Eval("EMP_NO")+","+ Eval("Short_Name") +","+ Eval("MCR_NO")+","+ Eval("DEPT_DESC")%>' OnClick="lnkbtnpopupDOCID_Click"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #F8F8F8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblEMP_NOr" runat="server" ForeColor="White" Text="Doctor Employee Number."></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblShort_Name" runat="server" Text="<%#Bind('Short_Name')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #F8F8F8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblDescription" runat="server" ForeColor="White" Text="Doctor Name"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmcrno" runat="server" Text="<%#Bind('MCR_NO')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #F8F8F8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblmcrnoon" runat="server" ForeColor="White" Text="MCR No"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmcr_no" runat="server" Text="<%#Bind('DEPT_DESC')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #F8F8F8; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:Label ID="lblmcr" runat="server" ForeColor="White" Text="Department Name"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>



                                                                        </Columns>
                                                                        <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                        <RowStyle CssClass="GridviewRowStyle" />
                                                                        <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="modal-footer" style="text-align: center;">
                                                            <asp:Repeater ID="Repeater5" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkPagedoctorselectionPopup" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                        Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPagedoctorselectionPopup_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                        <div class="modal-footer" style="text-align: center;">
                                                            <asp:Button runat="server" ID="Button1" OnClientClick="showTabs('btnpatientprofile')" Text="Cancel" CssClass="btn btnred" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- Doctor Search Popup End -->

            <!-- Report Popup start -->
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
                                                <div class="form-horizontal">
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
                                                              <div class="form-group">
                                                                  <label for="inputEmail3" class="col-sm-2 control-label"> No of Month(s) </label>
                                                                   <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtnomonths" runat="server" Enabled="true"  MaxLength="5"  CssClass="form-control  optional" onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                                   <label for="inputEmail3" class="col-sm-4 control-label"> (Applicable only Not ready for assessment) </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            
                                                        </div>
                                                    </div>
                                                                  </div>
                                                                </div>
                                                    </div>
                                                              <br />
                                                           
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
                                                <asp:Panel ID="pnlGeneralDetails" Width="100%" runat="server" Style="margin-left: 20px;">
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



            <!--For PAYMENT Popup Starts -->
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="false">
                    <ContentTemplate>
                        <asp:LinkButton ID="lnkbtnsupcounter" runat="server" />
                        <cc1:ModalPopupExtender ID="modelpopupRedirectPaymentscreen" runat="server" BackgroundCssClass="modal-background"
                            DynamicControlID="lnkbtnsupcounter" PopupControlID="Panelsupcounter" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                            TargetControlID="lnkbtnsupcounter" />
                        <asp:Panel ID="Panelsupcounter" runat="server" CssClass="modalPopup" align="center" EnableTheming="True" Style="text-align: center" Width="400px">

                            <table width="100%">
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: red; padding-left: 30px; text-align: left">
                                        <asp:Label ID="lblsupcounter" runat="server" Visible="true" Text="Record Created Successfully. New Request No. is "></asp:Label>

                                        <asp:Label ID="lblreqID" runat="server" ForeColor="Blue" Visible="true"></asp:Label>

                                        <asp:Label Style="text-align: center" ID="Label5" runat="server" Visible="true" Text=". Do you want to generate payment now?"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="line-height: 11px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 50px">
                                        <asp:Button ID="btnOKRedirectPaymentscreen" runat="server" CssClass="btn btngreen" Visible="true" OnClick="btnOKRedirectPaymentscreen_Click"
                                            Text="Yes" Width="100px" />
                                        &nbsp;<asp:Button ID="btnRedirectPaymentscreen" runat="server" CssClass="btn btnred" Visible="true" OnClick="btnRedirectPaymentscreen_Click"
                                            Text="No" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1"></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                    </td>
                                </tr>

                            </table>


                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!--For PAYMENT Popup  End -->

            <!-- BatchRequest Popup start -->
            <asp:Panel runat="server" ID="pnlBatchRequest" Visible="false">
                 <asp:LinkButton ID="lnkbtnBatchrequest" runat="server" />
                                    <cc1:ModalPopupExtender ID="mpePdtBatchrequest" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="lnkbtnBatchrequest" PopupControlID="pnlPdtBatchrequest" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="lnkbtnBatchrequest" />
                 <asp:Panel ID="pnlPdtBatchrequest" runat="server" Width="950">
                    <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                        <div class="modal-dialog modal-lg panelBiodata" style="width: 40%;">
                            <div class="modal-content PopupModelContent">
                                <div class="text-center" style="background-color: #304863;">
                                    <h4 class="modal-title" style="font-weight: bold; font-size: 16px; color: white"></h4>
                                </div>


                                <div class="form-horizontal">
                                    <h3 style="text-align: center; color: black; font-size: 20px; font-weight: 500">Your Batch Requests Are Created Successfully</h3>
                                </div>
                                <div class="box-body">
                                    <div class="form">
                                        <asp:HiddenField ID="HiddenField12" runat="server" />
                                        <div class="table-responsive table--no-card m-b-30">
                                            <asp:GridView ID="gvlistBATCHPROLILE" runat="server" GridLines="Vertical" RowStyle-Wrap="true" AutoGenerateColumns="false"
                                                CssClass="table table-borderless table-striped table-earning">

                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                <Columns>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnloadBATCHPROLILE" runat="server" Text="<%#Bind('hrn_id')%>" CommandArgument='<%#Eval("REQUEST_ID")%>' OnClick="lnkbtnloadBATCHPROLILE_Click"></asp:LinkButton>

                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                <tr>
                                                                    <td class="gridtabletd" cssclass="GridHeaderText" width="50px">
                                                                        <asp:Label ID="Label6" runat="server" Text="MRN" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text="<%#Bind('REQUEST_ID')%>"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse">
                                                                <tr>
                                                                    <td class="gridtabletd" cssclass="GridHeaderText" width="50px">
                                                                        <asp:Label ID="Label8" runat="server" Text="Request No" ForeColor="White"></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                <RowStyle CssClass="GridviewRowStyle" />
                                                <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer" style="text-align: center;">
                                    <asp:Repeater ID="Repeater6" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton3" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPage_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="modal-footer" style="text-align: center;">
                                    <asp:Button runat="server" ID="btnokBATCHPROLILE" Text="Ok" OnClick="btnokBATCHPROLILE_Click" CssClass="btn btngreen" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </asp:Panel>
            <!-- BatchRequest Popup End -->

            <!-- Update doctor Popup start -->
            <asp:Panel runat="server" ID="pnlupdateremarksDoctor" Visible="false">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelModalDoctor" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="btnerrorsuccessDoctor" runat="server" />
                                    <cc1:ModalPopupExtender ID="ModelpopuperrorDoctor" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="btnerrorsuccessDoctor" PopupControlID="pnlpopuperrorDoctor" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="btnerrorsuccessDoctor" />
                                    <asp:Panel ID="pnlpopuperrorDoctor" runat="server" BackColor="#e3f6fd"
                                        EnableTheming="True" Style="text-align: center; resize: none; align-content: flex-start; align-items: flex-start; border: 5px solid #2aa7ed; border-radius: 20px; padding: 10px;" Width="447px">
                                        <center>
                                            <table style="width: 100%; text-align: center" bgcolor="#f4eaff">
                                                <tr>
                                                    <td style="width: 12%"></td>
                                                    <td align="center">
                                                        <asp:Label Style="text-align: center" ID="lblreject" runat="server" Text="" ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 96px">
                                                        <table style="width: 100%">
                                                        </table>
                                                        <asp:Panel ID="Panel7" runat="server" Height="67px">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <label class="RegPopuptxt">Reject Reason:&nbsp;&nbsp;</label></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtProcessCompletedDoctor" MaxLength="50" runat="server" CssClass="form-control" Visible="true"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%"></td>
                                                                    <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt;">Are you sure want to update the process?</td>
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
                                                        <asp:Button ID="btnConfirmprocessStatusDoc" runat="server" align="right" CssClass="btn btngreen"
                                                            Text="Yes" OnClick="btnConfirmprocessStatusDoc_Click" />
                                                        <asp:Button ID="btnConfirmprocessCloseDoc" runat="server" align="right" CssClass="btn btnred"
                                                            Text="NO" OnClick="btnConfirmprocessCloseDoc_Click" />
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
            <!-- Update doctor Popup End -->

            <!--DDL Popup start -->
            <asp:Panel runat="server" ID="Panel3" Visible="false">
                 <asp:LinkButton ID="lnkbtnddlpopup" runat="server" />
                                    <cc1:ModalPopupExtender ID="mdlpnlddlpopup" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="lnkbtnddlpopup" PopupControlID="pnlddlpopup" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="lnkbtnddlpopup" />
                  <div class="table-responsive table--no-card m-b-30">
                    <asp:Panel ID="pnlddlpopup" runat="server">
                        <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                            <div class="modal-dialog modal-lg" style="width: 70%;">
                                <div class="modal-content PopupModelContent" style="">
                                    <div class="text-center" style="background-color: #304863;">
                                        <h4 class="modal-title" style="font-weight: bold; font-size: 16px; color: white"></h4>
                                    </div>
                                    <div class="modal-body" style="border-radius: 20px;">
                                        <div class="nav-tabs-custom">
                                            <div class="box box-primary box-solid" style="border: 2px solid #2aa7ed; background-color: #f9feff">
                                                <div class="box-header Popboxheader" style="">
                                                    <ul class="nav nav-tabs">
                                                        <li class="" style="background-color: #e3f6fd; border-radius: 5px;"><a href="#tabPrd_1" data-toggle="tab" style="font-weight: 500; font-size: 15px; bordproductr: rgba(221, 221, 221, 0.5490196078431373);">
                                                            <asp:Label runat="server" ID="lblpopupname" ForeColor="Black" Text="User Selection"></asp:Label>
                                                        </a></li>
                                                        <div style="text-align: right">
                                                            <asp:ImageButton ID="ImageButton6" runat="server" Height="38px" ImageUrl="Images/PopColse.png"
                                                                Width="38px" ToolTip="Close" OnClick="btnclosOrganisationpopup_Click" />
                                                        </div>
                                                    </ul>
                                                </div>
                                                <div class="box-body">
                                                    <div class="tab-content">
                                                        <div class="tab-pane active" id="tabPrd_12">
                                                            <div class="form-horizontal">
                                                                <div class="row">
                                                                    <div class="col-xs-12 col-sm-12 col-md-12">

                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:Panel runat="server" ID="pnlCongWH" Visible="true">
                                                                                    <asp:UpdatePanel runat="server" ID="upnl44">
                                                                                        <ContentTemplate>
                                                                                            <table border="1" style="width: 100%; height: 30px;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <div>
                                                                                                            <label for="inputEmail3" class="col-sm-2 control-label" style="margin-top: 10px">Search : </label>
                                                                                                            <div class="col-sm-3" style="margin-top: 10px;">
                                                                                                                <div class="form-group">
                                                                                                                    <asp:TextBox ID="txtddlpopupvalue" runat="server" CssClass="form-control" ReadOnly="false" Width="200px" onkeypress="return Popuplistenter1_click(event);"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>

                                                                                                            <div class="col-sm-3" style="margin-left: 70px; margin-top: 10px;">
                                                                                                                <div class="form-group">
                                                                                                                    <asp:Button runat="server" ID="btnfindddlpopupRecord" CssClass="btn btnblue" Width="130px" OnClick="btnfindddlpopupRecord_Click" Text="Find" />
                                                                                                                </div>
                                                                                                            </div>

                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>

                                                                                            <asp:GridView ID="gvlistPopUppurpose" runat="server" GridLines="Vertical" RowStyle-Wrap="true" AutoGenerateColumns="false"
                                                                                                CssClass="table table-borderless table-striped table-earning" OnRowDataBound="gvlistPopUppurpose_RowDataBound">

                                                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                                                <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                                                                <Columns>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkbtnddlpopupID" runat="server" Text="" OnClick="lnkbtnddlpopupID_Click"></asp:LinkButton>

                                                                                                        </ItemTemplate>
                                                                                                        <HeaderTemplate>
                                                                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #F8F8F8; border-collapse: collapse;">
                                                                                                                <tr>
                                                                                                                    <td class="gridtabletd" width="50px">
                                                                                                                        <asp:Label ID="lblid11f" runat="server" ForeColor="white" Text="ID"></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>

                                                                                                            </table>
                                                                                                        </HeaderTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lnkbtnddlpopupdesc" runat="server" Text=""></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderTemplate>
                                                                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #F8F8F8; border-collapse: collapse;">
                                                                                                                <tr>
                                                                                                                    <td class="gridtabletd" width="50px">
                                                                                                                        <asp:Label ID="lblid11f" runat="server" ForeColor="white" Text="Description"></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>

                                                                                                            </table>
                                                                                                        </HeaderTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                                    </asp:TemplateField>

                                                                                                </Columns>
                                                                                                <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                                                <RowStyle CssClass="GridviewRowStyle" />
                                                                                                <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                                            </asp:GridView>

                                                                                            <div class="modal-footer" style="text-align: center;">
                                                                                                <asp:Repeater ID="Repeater8" runat="server">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnkPagedropdownpopup" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                                            CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPagedropdownpopup_Click"></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>

                                                                                            </div>
                                                                                            <div class="modal-footer" style="text-align: center;">
                                                                                                <asp:Button runat="server" ID="Button2" OnClick="btnclosOrganisationpopup_Click" Text="Cancel" CssClass="btn btnred" />
                                                                                            </div>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </asp:Panel>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>

                                                                </div>
                                                            </div>

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
                </div>
            </asp:Panel>
            <!-- DDL doctor Popup End -->

            <!-- Transfer Popup Start -->
            <asp:Panel runat="server" ID="pnlupdateTransfer" Visible="false">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelTransfer" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkbtnbtnTransfer" runat="server" />
                                    <cc1:ModalPopupExtender ID="ModelpopupTransfer" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="lnkbtnbtnTransfer" PopupControlID="pnlTransfer" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="lnkbtnbtnTransfer" />
                                    <asp:Panel ID="pnlTransfer" runat="server" BackColor="#e3f6fd" BorderStyle="Outset"
                                        EnableTheming="True" Style="text-align: center; resize: none; align-content: flex-start; align-items: flex-start; border: 5px solid #2aa7ed; border-radius: 20px; padding: 10px;" Width="447px">
                                        <center>
                                            <table style="width: 100%; text-align: center" bgcolor="#f4eaff">
                                                <tr>
                                                    <td style="width: 12%"></td>
                                                    <td align="center">
                                                        <asp:Label Style="text-align: center" ID="Label2" runat="server" Text="Transfer To" ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 96px">

                                                        <asp:Panel ID="Panel8" runat="server" Height="67px">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <label class="RegPopuptxt">Institution:&nbsp;&nbsp;</label></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddltransferInst" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%"></td>
                                                                    <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt;">Are you sure you want to transfer this request? </td>
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
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align: center">
                                                        <asp:Button ID="btnTransferOK" runat="server" align="right" CssClass="btn btngreen"
                                                            Text="Yes" OnClick="btnTransferOK_Click" />
                                                        <asp:Button ID="btnTransferCancel" runat="server" align="right" CssClass="btn btnred"
                                                            Text="NO" OnClick="btnTransferCancel_Click" />
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
            <!-- Transfer End Start -->


            <asp:Panel runat="server" ID="Panel10" Visible="false">

                 <asp:LinkButton ID="lnkbtnProducts21" runat="server" />
                                    <cc1:ModalPopupExtender ID="mpePdtPlsmremaisend" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="lnkbtnProducts21" PopupControlID="pnlPdtPlt23" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="lnkbtnProducts21" />
                <asp:Panel ID="pnlPdtPlt23" runat="server" Width="950">
                    <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                        <div class="modal-dialog modal-lg panelBiodata" style="width: 50%;">
                            <div class="modal-content PopupModelContent">
                                <div class="text-center" style="background-color: #304863;">
                                    <h4 class="modal-title" style="font-weight: bold; font-size: 16px; color: white"></h4>
                                </div>
                                <div class="modal-body" style="border-radius: 20px;">
                                    <div class="nav-tabs-custom">
                                        <div class="box box-primary box-solid">
                                            <div class="box-header Popboxheader" style="">
                                                <ul class="nav nav-tabs" style="">
                                                    <li class="" style="background-color: #e3f6fd; border-radius: 5px;"><a href="#tabPrd_1" data-toggle="tab"
                                                        style="font-weight: 500; font-size: 15px; color: black">
                                                        <asp:Label runat="server" ID="Label10" Text="Email to Requestor"></asp:Label>
                                                    </a></li>
                                                    <div style="text-align: right">
                                                        <asp:ImageButton ID="ImageButton7" runat="server" Height="38px" ImageUrl="Images/PopColse.png"
                                                            Width="38px" ToolTip="Close" />
                                                    </div>
                                                </ul>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                     <div class="form-group">
                                        <div class="col-sm-2">
                                        </div>
                                        <label for="inputEmail3" class="col-sm-3 control-label">Name [MRN] </label>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtpatnamemrn" runat="server" CssClass="form-control" Style="text-transform: uppercase;" ReadOnly="true" onkeypress="return Biodataenter1_click(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                        </div>
                                        <label for="inputEmail3" class="col-sm-3 control-label">Recipient Email  </label>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtsmremailRequestor" runat="server" CssClass="form-control" Style="text-transform: uppercase;" ReadOnly="false" onkeypress="return Biodataenter1_click(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                        </div>
                                        <label for="inputEmail3" class="col-sm-3 control-label">CC </label>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtsmremailCC" runat="server" CssClass="form-control optional" ReadOnly="false" onkeypress="return Biodataenter1_click(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                        </div>
                                        <label for="inputEmail3" class="col-sm-3 control-label">BCC </label>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtsmremailBCC" runat="server" CssClass="form-control optional" ReadOnly="false" onkeypress="return Biodataenter1_click(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                        </div>
                                        <label for="inputEmail3" class="col-sm-3 control-label">Subject </label>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtsmremailSubject" TextMode="MultiLine" runat="server" CssClass="form-control optional MultiLine_Textbox" ReadOnly="false" onkeypress="return Biodataenter1_click(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="pnlattachmentsmr">
                                    <center>
                                        <div class="Emailbutton">
                                            <table width="100%">
                                                <tr>
                                                    <td width="5%">
                                                        <img src="Images/PDF.png" width="40" height="40" alt="Alternate Text" />
                                                    </td>
                                                    <td width="40%">
                                                        <asp:LinkButton ID="lnkbtnsmremailfilename" runat="server" OnClick="lnkbtnsmremailfilename_Click" />
                                                    </td>
                                                    <td width="1%">
                                                        <asp:ImageButton ID="btncancelsmrfile" runat="server" Height="19" ImageUrl="Images/cross.png"
                                                            Width="19" ToolTip="Close" OnClick="btncancelsmrfile_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </center>
                                </asp:Panel>
                                <br />
                                <asp:Panel runat="server">
                                    <div class="EmailTemplate">
                                        <asp:TextBox runat="server" TextMode="MultiLine" Text="" ID="txtsmremailCOntent" CssClass="form-control MultiLine_Textbox" Style="height: 220px" />
                                    </div>
                                </asp:Panel>
                                <br />
                                <div class="modal-footer" style="text-align: center;">
                                    <asp:Button Text="Send" CssClass="btn btngreen" runat="server" ID="btnsentCOMPLETESMRDOC" OnClick="btnsentCOMPLETESMRDOC_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </asp:Panel>
          <%--  for smr email send popup--%>


            <div>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="Paneldocpreview" Visible="true">
                             <asp:LinkButton ID="LinkButton2" runat="server" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtenderpreview" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="LinkButton2" PopupControlID="pnlsmrpreview" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="LinkButton2" />
                            <asp:Panel ID="pnlsmrpreview" runat="server" Width="950">
                                <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                                    <div class="modal-dialog modal-lg panelBiodata" style="width: 70%;">
                                        <div class="modal-content PopupModelContent" style="height: 1000px !important">
                                            <div class="text-center" style="background-color: #304863;">
                                                <h4 class="modal-title" style="font-weight: bold; font-size: 16px; color: white"></h4>
                                            </div>
                                            <div class="modal-body" style="border-radius: 20px;">
                                                <div class="nav-tabs-custom">
                                                    <div class="box box-primary box-solid">
                                                        <div class="box-header Popboxheader" style="">
                                                            <ul class="nav nav-tabs" style="text-align: center;">
                                                                <asp:Label ID="Label9" runat="server" Text="Complete Medical Report" Style="color: black; top: 18px; position: relative; font-size: 18px; font-weight: 500"></asp:Label>
                                                                <div style="text-align: right; position: relative; top: -30px">
                                                                    <asp:ImageButton ID="ImageButton8" runat="server" Height="38px" ImageUrl="Images/PopColse.png"
                                                                        Width="38px" ToolTip="Close" OnClick="ImageButton8_Click" />
                                                                </div>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="heightincreasepreview">
                                                <asp:UpdatePanel ID="UpdatePanel58" runat="server">
                                                    <ContentTemplate>
                                                        <div style="height: 790px;">
                                                            <iframe id="iframepreview" style="width: 100%; height: 100%" runat="server"  frameborder="0"></iframe>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            </div>

                                            

                                            <%-- <div class="modal-footer" style="text-align: center;">
                                    <asp:Button runat="server" ID="Button3" Text="Close" CssClass="btn btnred Backcolor"   />
                                </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                        </asp:Panel>

                    </td>
                </tr>
            </div>


            <!-- Error Popup start -->
            <div>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlerrorpopup" Visible="false">
                            <asp:UpdatePanel ID="UpdatePanelModal6" runat="server">
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
                        </asp:Panel>
                    </td>
                </tr>
            </div>
            <!-- Error Popup End -->

        </ContentTemplate>
    </asp:UpdatePanel>
    <script>

        /*window.onload = showpanel*/
        /*showpanel(false)*/
        const root = document.querySelector(":root"); //grabbing the root element
        root.style.setProperty("--tab1", '#2aa7ed');
        root.style.setProperty("--tab1FontColor", 'white');

        function showpanel(boolFlag = true) { // This code for showtabs time

            

            const root = document.querySelector(":root"); //grabbing the root element


            root.style.setProperty("--tab1", '#2aa7ed');
            root.style.setProperty("--tab2", '#DFDFDF');
            root.style.setProperty("--tab3", '#DFDFDF');
            root.style.setProperty("--tab4", '#DFDFDF');
            root.style.setProperty("--tab5", '#DFDFDF');
            root.style.setProperty("--tab6", '#DFDFDF');
            root.style.setProperty("--tab7", '#DFDFDF');
            root.style.setProperty("--tab8", '#DFDFDF');
            root.style.setProperty("--tab9", '#DFDFDF');

            root.style.setProperty("--tab1FontColor", 'white');
            root.style.setProperty("--tab2FontColor", '#706F6F');
            root.style.setProperty("--tab3FontColor", '#706F6F');
            root.style.setProperty("--tab4FontColor", '#706F6F');
            root.style.setProperty("--tab5FontColor", '#706F6F');
            root.style.setProperty("--tab6FontColor", '#706F6F');
            root.style.setProperty("--tab7FontColor", '#706F6F');
            root.style.setProperty("--tab8FontColor", '#706F6F');
            root.style.setProperty("--tab9FontColor", '#706F6F');

            if (document.getElementById("<%=btnpatientprofile.ClientID%>")) {
                document.getElementById("<%=btnpatientprofile.ClientID%>").click()
            }
            if (document.getElementById("<%=btnpatientprofile2.ClientID%>")) {
                document.getElementById("<%=btnpatientprofile2.ClientID%>").click();
            }

        }
        function showpanel1(boolFlag = true) {

            
            const root = document.querySelector(":root"); //grabbing the root element

            root.style.setProperty("--tab1", '#DFDFDF');
            root.style.setProperty("--tab2", '#2aa7ed');
            root.style.setProperty("--tab3", '#DFDFDF');
            root.style.setProperty("--tab4", '#DFDFDF');
            root.style.setProperty("--tab5", '#DFDFDF');
            root.style.setProperty("--tab6", '#DFDFDF');
            root.style.setProperty("--tab7", '#DFDFDF');
            root.style.setProperty("--tab8", '#DFDFDF');
            root.style.setProperty("--tab9", '#DFDFDF');

            root.style.setProperty("--tab1FontColor", '#706F6F');
            root.style.setProperty("--tab2FontColor", 'white');
            root.style.setProperty("--tab3FontColor", '#706F6F');
            root.style.setProperty("--tab4FontColor", '#706F6F');
            root.style.setProperty("--tab5FontColor", '#706F6F');
            root.style.setProperty("--tab6FontColor", '#706F6F');
            root.style.setProperty("--tab7FontColor", '#706F6F');
            root.style.setProperty("--tab8FontColor", '#706F6F');
            root.style.setProperty("--tab9FontColor", '#706F6F');

            if (document.getElementById("<%=btnrequestordetails.ClientID%>")) {
                document.getElementById("<%=btnrequestordetails.ClientID%>").click();
            }
            if (document.getElementById("<%=btnrequestordetails2.ClientID%>")) {
                document.getElementById("<%=btnrequestordetails2.ClientID%>").click();
            }
        }
        function showpanel2(boolFlag = true) {

            

            const root = document.querySelector(":root"); //grabbing the root element

            root.style.setProperty("--tab1", '#DFDFDF');
            root.style.setProperty("--tab2", '#DFDFDF');
            root.style.setProperty("--tab3", '#2aa7ed');
            root.style.setProperty("--tab4", '#DFDFDF');
            root.style.setProperty("--tab5", '#DFDFDF');
            root.style.setProperty("--tab6", '#DFDFDF');
            root.style.setProperty("--tab7", '#DFDFDF');
            root.style.setProperty("--tab8", '#DFDFDF');
            root.style.setProperty("--tab9", '#DFDFDF');

            root.style.setProperty("--tab1FontColor", '#706F6F');
            root.style.setProperty("--tab2FontColor", '#706F6F');
            root.style.setProperty("--tab3FontColor", 'white');
            root.style.setProperty("--tab4FontColor", '#706F6F');
            root.style.setProperty("--tab5FontColor", '#706F6F');
            root.style.setProperty("--tab6FontColor", '#706F6F');
            root.style.setProperty("--tab7FontColor", '#706F6F');
            root.style.setProperty("--tab8FontColor", '#706F6F');
            root.style.setProperty("--tab9FontColor", '#706F6F');

            if (document.getElementById("<%=btnrequestdetails.ClientID%>")) {
                document.getElementById("<%=btnrequestdetails.ClientID%>").click()
            }
            if (document.getElementById("<%=btnrequestdetails2.ClientID%>")) {
                document.getElementById("<%=btnrequestdetails2.ClientID%>").click()
            }
        }
        function showpanel3(boolFlag = true) {

            
            const root = document.querySelector(":root"); //grabbing the root element

            root.style.setProperty("--tab1", '#DFDFDF');
            root.style.setProperty("--tab2", '#DFDFDF');
            root.style.setProperty("--tab3", '#DFDFDF');
            root.style.setProperty("--tab4", '#2aa7ed');
            root.style.setProperty("--tab5", '#DFDFDF');
            root.style.setProperty("--tab6", '#DFDFDF');
            root.style.setProperty("--tab7", '#DFDFDF');
            root.style.setProperty("--tab8", '#DFDFDF');
            root.style.setProperty("--tab9", '#DFDFDF');

            root.style.setProperty("--tab1FontColor", '#706F6F');
            root.style.setProperty("--tab2FontColor", '#706F6F');
            root.style.setProperty("--tab3FontColor", '#706F6F');
            root.style.setProperty("--tab4FontColor", 'white');
            root.style.setProperty("--tab5FontColor", '#706F6F');
            root.style.setProperty("--tab6FontColor", '#706F6F');
            root.style.setProperty("--tab7FontColor", '#706F6F');
            root.style.setProperty("--tab8FontColor", '#706F6F');
            root.style.setProperty("--tab9FontColor", '#706F6F');

            if (document.getElementById("<%=btnattachment.ClientID%>")) {
                document.getElementById("<%=btnattachment.ClientID%>").click();
            }
            if (document.getElementById("<%=btnattachment2.ClientID%>")) {
                document.getElementById("<%=btnattachment2.ClientID%>").click();
            }
        }
        function showpanel4(boolFlag = true) {

            

            const root = document.querySelector(":root"); //grabbing the root element
            
            root.style.setProperty("--tab1", '#DFDFDF');
            root.style.setProperty("--tab2", '#DFDFDF');
            root.style.setProperty("--tab3", '#DFDFDF');
            root.style.setProperty("--tab4", '#DFDFDF');
            root.style.setProperty("--tab5", '#2aa7ed');
            root.style.setProperty("--tab6", '#DFDFDF');
            root.style.setProperty("--tab7", '#DFDFDF');
            root.style.setProperty("--tab8", '#DFDFDF');
            root.style.setProperty("--tab9", '#DFDFDF');

            root.style.setProperty("--tab1FontColor", '#706F6F');
            root.style.setProperty("--tab2FontColor", '#706F6F');
            root.style.setProperty("--tab3FontColor", '#706F6F');
            root.style.setProperty("--tab4FontColor", '#706F6F');
            root.style.setProperty("--tab5FontColor", 'white');
            root.style.setProperty("--tab6FontColor", '#706F6F');
            root.style.setProperty("--tab7FontColor", '#706F6F');
            root.style.setProperty("--tab8FontColor", '#706F6F');
            root.style.setProperty("--tab9FontColor", '#706F6F');

            if (document.getElementById("<%=btnwaiver.ClientID%>")) {
                document.getElementById("<%=btnwaiver.ClientID%>").click();
            }
            if (document.getElementById("<%=btnwaiver2.ClientID%>")) {
                document.getElementById("<%=btnwaiver2.ClientID%>").click();
            }
        }

        function showpanel5(boolFlag = true) {

            

            const root = document.querySelector(":root"); //grabbing the root element

            root.style.setProperty("--tab1", '#DFDFDF');
            root.style.setProperty("--tab2", '#DFDFDF');
            root.style.setProperty("--tab3", '#DFDFDF');
            root.style.setProperty("--tab4", '#DFDFDF');
            root.style.setProperty("--tab5", '#DFDFDF');
            root.style.setProperty("--tab6", '#2aa7ed');
            root.style.setProperty("--tab7", '#DFDFDF');
            root.style.setProperty("--tab8", '#DFDFDF');
            root.style.setProperty("--tab9", '#DFDFDF');

            root.style.setProperty("--tab1FontColor", '#706F6F');
            root.style.setProperty("--tab2FontColor", '#706F6F');
            root.style.setProperty("--tab3FontColor", '#706F6F');
            root.style.setProperty("--tab4FontColor", '#706F6F');
            root.style.setProperty("--tab5FontColor", '#706F6F');
            root.style.setProperty("--tab6FontColor", 'white');
            root.style.setProperty("--tab7FontColor", '#706F6F');
            root.style.setProperty("--tab8FontColor", '#706F6F');
            root.style.setProperty("--tab9FontColor", '#706F6F');

            if (document.getElementById("<%=btnAssignDocter.ClientID%>")) {
                document.getElementById("<%=btnAssignDocter.ClientID%>").click();
            }
            if (document.getElementById("<%=btnAssignDocter2.ClientID%>")) {
                document.getElementById("<%=btnAssignDocter2.ClientID%>").click();
            }

        }

        function showpanel6(boolFlag = true) {
            

            const root = document.querySelector(":root"); //grabbing the root element

            root.style.setProperty("--tab1", '#DFDFDF');
            root.style.setProperty("--tab2", '#DFDFDF');
            root.style.setProperty("--tab3", '#DFDFDF');
            root.style.setProperty("--tab4", '#DFDFDF');
            root.style.setProperty("--tab5", '#DFDFDF');
            root.style.setProperty("--tab6", '#DFDFDF');
            root.style.setProperty("--tab7", '#2aa7ed');
            root.style.setProperty("--tab8", '#DFDFDF');
            root.style.setProperty("--tab9", '#DFDFDF');

            root.style.setProperty("--tab1FontColor", '#706F6F');
            root.style.setProperty("--tab2FontColor", '#706F6F');
            root.style.setProperty("--tab3FontColor", '#706F6F');
            root.style.setProperty("--tab4FontColor", '#706F6F');
            root.style.setProperty("--tab5FontColor", '#706F6F');
            root.style.setProperty("--tab6FontColor", '#706F6F');
            root.style.setProperty("--tab7FontColor", 'white');
            root.style.setProperty("--tab8FontColor", '#706F6F');
            root.style.setProperty("--tab9FontColor", '#706F6F');

            if (boolFlag) {
                document.getElementById("<%=btnenquiry.ClientID%>").click();
            }
        }

        function showpanel7(boolFlag = true) {

            

            const root = document.querySelector(":root"); //grabbing the root element

            root.style.setProperty("--tab1", '#DFDFDF');
            root.style.setProperty("--tab2", '#DFDFDF');
            root.style.setProperty("--tab3", '#DFDFDF');
            root.style.setProperty("--tab4", '#DFDFDF');
            root.style.setProperty("--tab5", '#DFDFDF');
            root.style.setProperty("--tab6", '#DFDFDF');
            root.style.setProperty("--tab7", '#DFDFDF');
            root.style.setProperty("--tab8", '#2aa7ed');
            root.style.setProperty("--tab9", '#DFDFDF');

            root.style.setProperty("--tab1FontColor", '#706F6F');
            root.style.setProperty("--tab2FontColor", '#706F6F');
            root.style.setProperty("--tab3FontColor", '#706F6F');
            root.style.setProperty("--tab4FontColor", '#706F6F');
            root.style.setProperty("--tab5FontColor", '#706F6F');
            root.style.setProperty("--tab6FontColor", '#706F6F');
            root.style.setProperty("--tab7FontColor", '#706F6F');
            root.style.setProperty("--tab8FontColor", 'white');
            root.style.setProperty("--tab9FontColor", '#706F6F');

            if (boolFlag) {
                document.getElementById("<%=btnpendingitems.ClientID%>").click();
            }

        }

        function showpanel8(boolFlag = true) {

            

            const root = document.querySelector(":root"); //grabbing the root element

            root.style.setProperty("--tab1", '#DFDFDF');
            root.style.setProperty("--tab2", '#DFDFDF');
            root.style.setProperty("--tab3", '#DFDFDF');
            root.style.setProperty("--tab4", '#DFDFDF');
            root.style.setProperty("--tab5", '#DFDFDF');
            root.style.setProperty("--tab6", '#DFDFDF');
            root.style.setProperty("--tab7", '#DFDFDF');
            root.style.setProperty("--tab8", '#DFDFDF');
            root.style.setProperty("--tab9", '#2aa7ed');

            root.style.setProperty("--tab1FontColor", '#706F6F');
            root.style.setProperty("--tab2FontColor", '#706F6F');
            root.style.setProperty("--tab3FontColor", '#706F6F');
            root.style.setProperty("--tab4FontColor", '#706F6F');
            root.style.setProperty("--tab5FontColor", '#706F6F');
            root.style.setProperty("--tab6FontColor", '#706F6F');
            root.style.setProperty("--tab7FontColor", '#706F6F');
            root.style.setProperty("--tab8FontColor", '#706F6F');
            root.style.setProperty("--tab9FontColor", 'white');

            if (boolFlag) {
                document.getElementById("<%=btnProcessHistory.ClientID%>").click();
            }

        }


        function HRNenter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=btnHRNsearch.ClientID%>").click();
                return false;
            }
        }
    </script>

    <script>
        // accordion pluse minus goes here    	  
        jQuery('.accordion-toggle').click(function () {

            var has = jQuery(this);
            if (has.hasClass('collapsed')) {
                jQuery(this).find('i').removeClass('fa-plus');
                jQuery(this).find('i').addClass('fa-minus');
            }
            else {
                jQuery(this).find('i').removeClass('fa-minus');
                jQuery(this).find('i').addClass('fa-plus');
            }
        })

    </script>


    <script type="text/javascript">

        function Biodataenter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=btnbiodatapopupsearch.ClientID%>").click();
                return false;
            }
        }

        function Copyreqenter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=btnbiodatapopupsearchcopyrequets.ClientID%>").click();
                return false;
            }
        }

        function DOClistenter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=btndoctorselectionsearch.ClientID%>").click();
                return false;
            }
        }

        function Popuplistenter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=btnfindddlpopupRecord.ClientID%>").click();
                return false;
            }
        }
    </script>

    <script>

</script>


    <script type="text/javascript">
        function validateCategory() {
            var ddlCategory = document.getElementById('<%= ddlCategory.ClientID %>');
        var fileUpload = document.getElementById('<%= FileUpload1.ClientID %>');

            if (ddlCategory.value === '' && fileUpload.value === '') {
                bootbox.alert('Please select a File for Attachment and Please Choose a Category.');
                return false;
            }

            if (ddlCategory.value === '') {
                bootbox.alert('Please select a category');
                return false;
            }

            if (fileUpload.value === '') {
                bootbox.alert('Please select a File for Attachment');
                return false;
            }

            return true;
        }
    </script> 
</asp:Content>
