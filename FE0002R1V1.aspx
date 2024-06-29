<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FE0002R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FE0002R1V1" %>

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

  
    <script src="Scripts/Validation.js" type="text/javascript"></script>

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnl6" runat="server">
        <ContentTemplate>
             <asp:HiddenField ID="hdnClickEvent" runat="server" />
             
                        <asp:HiddenField ID="hdnPopupDropdownValue" runat="server" />
                        <asp:HiddenField ID="hdnPopuptxtboxValue" runat="server" />
                        <asp:HiddenField ID="hdnPopuptxtboxnameValue" runat="server" />
                        <asp:HiddenField ID="hdnPopupupdatepnlValue" runat="server" />
            <div class="ToolBarcard">
             <div class="row ToolBar">
                    <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                       <%-- <asp:ImageButton ID="imgbtnSearch" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                     ToolTip="Clear All the Entries" OnClick="imgBtnNew_Click" />
                        <asp:ImageButton ID="imgbtnClear" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                            ToolTip="Search" OnClick="imgBtnSearch_Click" />
                 
              
                <asp:ImageButton ID="imgbtnSave" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                    ToolTip="Save Record" />
                 
                 <asp:ImageButton ID="imgbtnDelete" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                     ToolTip="Delete Record" />--%>
                 <asp:ImageButton ID="imgbtnprint" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                     ToolTip="Export" /> 
                 <%-- <asp:ImageButton ID="imgbtnAudit" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                      ToolTip="View the Summary of Workflow Status" />--%>
                  <%--<asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                      ToolTip="Information" />--%>
                    </div>
                </div>
</div>
            <div class="card">
            <div class="box box-solid" style="border-radius: 10px; padding: 5px;">
               <br />
                <div class="container-fluid">
                    <table style="padding: 1px; margin: 1px; width: 100%; text-align: left; table-layout: auto; border-collapse: collapse;">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlGeneralDetails" Width="90%" runat="server" Style="margin-left: 20px;">
                                    <div class="form-horizontal">
                                        <div class="box-body">

                                            <div class="form-group">
                                                <div class="bootcolsm1">
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">MRN</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHRN" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">Patient Name</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtPatName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="bootcolsm1">
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">Date From</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtFrmDate_CalendarExtender" TargetControlID="txtFrmDate" runat="server"
                                                            Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">Date To</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" TargetControlID="txtToDate" runat="server"
                                                            Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="bootcolsm1">
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">Report Type</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel ID="updpnltxtRecTypeeID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtRecTypeename" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtRecTypeeID" CssClass="form-control"></asp:TextBox>

                                                                            </span>
                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                     <asp:ImageButton AlternateText="RECTYPE_txtRecTypeeID_txtRecTypeename_updpnltxtRecTypeeID" runat="server" ID="imgbtntrigerRECTYPE" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="RECTYPE_txtRecTypeeID_txtRecTypeename" runat="server" ID="imgbtnClearRECTYPE" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                        <%--<asp:DropDownList ID="ddlReportType" runat="server" ToolTip="Institution ID" AutoPostBack="true" CssClass="bootformcontrol">
                                                        </asp:DropDownList>--%>
                                                    </div>
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">Enquiry Status</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlEnqStatus" runat="server" ToolTip="Enquiry Status"  CssClass="bootformcontrol">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <br />
                    
                    <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-top: -25px">
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn  btn-info" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i> Search</asp:LinkButton>
                        <asp:LinkButton ID="lbtnClear" runat="server" CssClass="btn  btnred" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px; width: 90px" OnClick="lbtnClear_Click"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear</asp:LinkButton>
                    </div>
                     <br />
                </div>
            </div>


            <asp:Panel ID="pnlresultgrid" runat="server" Visible="false">
                <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                    <div class="box-header with-border TotalRecord">
                        <div class="box-title">
                            <label class="text-right" style="color: white">Total Records : <span id="lblTotalRecords" runat="server"></span></label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="form">
                            <asp:HiddenField ID="hdnVisiblity" runat="server" />
                            <div class="table-responsive table--no-card m-b-30" id="divid">
                                <asp:GridView ID="gvList" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                    AutoGenerateColumns="False"
                                    CellPadding="2"
                                    ForeColor="#333333"
                                    HorizontalAlign="Center"
                                    PageSize="10" OnRowDataBound="gvList_RowDataBound"
                                    CssClass="table table-borderless table-striped">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnREQUEST_IDGrid" runat="server" Text="<%#Bind('REQUEST_ID')%>" CommandArgument='<%#Eval("REQUEST_ID")%>' OnClick="lnkbtnREQUEST_IDGrid_Click">
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="Linkbutton1" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Request Number" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="REQUEST_ID">
                                                            </asp:LinkButton>
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
                                                <asp:Label ID="lblordno" runat="server" Text="<%#Bind('HRN_ID')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnprocesssts7" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="MRN" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="HRN_ID"></asp:LinkButton>
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
                                                <asp:Label ID="lblTransDate" runat="server" Text='<%#Bind("PAT_SHORT_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnHeadershortName9" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Patient Name" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="PAT_SHORT_NAME"></asp:LinkButton>
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

                                                <asp:Label ID="lblEXPECTED_DATE" runat="server" Text='<%#Bind("ENQ_DATE", "{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkEXPECTED_DATE" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Enquiry Date Time" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="ENQ_DATE"></asp:LinkButton>
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
                                                <asp:Label ID="lbljobsts" runat="server" Text="<%#Bind('CALLERS_ENQUIRY')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnprocesssts6" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Last Enquiry Description" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="CALLERS_ENQUIRY"></asp:LinkButton>
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
                                                <asp:Label ID="lblSTAFFS_RESPONSE" runat="server" Text="<%#Bind('STAFFS_RESPONSE')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnprocesssts6" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Last Follow up action" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="STAFFS_RESPONSE"></asp:LinkButton>
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
                                                <asp:Label ID="lblREFERENCE_1" runat="server" Text="<%#Bind('REFERENCE_1')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnprocesssts6" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Enquiry Status" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="REFERENCE_1"></asp:LinkButton>
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
                            
                            <div class="col-lg-12">
                                <div class="row form-group">
                                    <div class="col-sm-12" style="text-align: center">
                                        <asp:Repeater ID="rptPagergvInvoicegvList" runat="server">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPagegvList" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                    CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPagegvList_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
                </div>
            <div>
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
                                            </div>

              <!--DDL Popup start -->
            <asp:Panel runat="server" ID="Panel3" Visible="false">
                <asp:LinkButton ID="lnkbtnddlpopup" runat="server"></asp:LinkButton>
                <asp:LinkButton ID="lbtnCancelddlpopup" runat="server"></asp:LinkButton>
                <ajax:ModalPopupExtender ID="mdlpnlddlpopup" BehaviorID="mdlpnlddlpopup" runat="server" TargetControlID="lnkbtnddlpopup" CancelControlID="lbtnCancelddlpopup" PopupControlID="pnlddlpopup" BackgroundCssClass="modal-background"></ajax:ModalPopupExtender>

                <div class="table-responsive table--no-card m-b-30">
                    <asp:Panel ID="pnlddlpopup" runat="server">
                        <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                            <div class="modal-dialog modal-lg" style="width: 70%;">
                                <div class="modal-content PopupModelContent">
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
                                                            <asp:ImageButton ID="ImageButton2" runat="server" Height="38px" ImageUrl="Images/PopColse.png"
                                                                Width="38px" ToolTip="Close" />
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
                                                                                                            <label for="inputEmail3" class="col-sm-2 control-label" style="margin-top: 10px">Search</label>
                                                                                                            <div class="col-sm-3" style="margin-top: 10px;">
                                                                                                                <div class="form-group">
                                                                                                                    <asp:TextBox ID="txtddlpopupvalue" runat="server" CssClass="form-control" ReadOnly="false" Width="200px" onkeypress="return Popuplistenter1_click(event);"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>

                                                                                                            <div class="col-sm-3" style="margin-left: 70px; margin-top: 10px;">
                                                                                                                <div class="form-group">
                                                                                                                    <asp:Button runat="server" ID="btnfindddlpopupRecord" CssClass="form-control btn-primary" Width="130px" style="color: white !important" OnClick="btnfindddlpopupRecord_Click" Text="Find" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

