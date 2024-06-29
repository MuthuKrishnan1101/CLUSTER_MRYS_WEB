<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FB0001R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FB0001R1V1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="System.Data" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Bootstrap 5.2.0 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css?v=1.0" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Customize Bootstrap -->
    <link rel="stylesheet" href="CSS/bootstrap.css" type="text/css" />

    <!-- gridviewstyle -->
     <link rel="stylesheet" href="CSS/gridviewstyle.css" />

    <style>
        
        .table-hover tbody tr:hover td {
            background-color: #2aa7ed;
        }
        .nav > li > a:active, .nav > li > a:focus, .nav > li > a:hover {
           /* background: #aa97dd !important;*/
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
            cursor: default !important;
            background-color: #2aa7ed !important;
            border: 1px solid #2aa7ed !important;
            border-bottom-color: transparent !important;
        }

        .nav-tabs li a {
            border: 1px solid #2aa7ed !important;
            font-weight: 500 !important;
            font-size: 16px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnl11" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnOutboundId" runat="server" />
            <asp:HiddenField ID="hdnClickcancel" runat="server" />
            <asp:HiddenField ID="hdnisEntre" runat="server" />
            <asp:HiddenField ID="hdnProcessStatus" runat="server" />
            <asp:HiddenField ID="hdnClickEvent" runat="server" />
             
                        <asp:HiddenField ID="hdnPopupDropdownValue" runat="server" />
                        <asp:HiddenField ID="hdnPopuptxtboxValue" runat="server" />
                        <asp:HiddenField ID="hdnPopuptxtboxnameValue" runat="server" />
                        <asp:HiddenField ID="hdnPopupupdatepnlValue" runat="server" />
            <div class="ToolBarcard">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">
                    <asp:ImageButton ID="imgbtnNew" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                        ToolTip="Add New" OnClick="imgbtnClear_Click" Visible="false" /> 
                 <asp:ImageButton ID="imgbtnClear" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                     ToolTip="Show all records" OnClick="Btnblockbillingsearch_Click" Visible="false" />
                  <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                      ToolTip="Save" OnClick="imgbtnSave_Click" Visible="false" />
                  
                 <asp:ImageButton ID="imgbtnDelete" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                     ToolTip="Delete" OnClick="imgbtnDelete_Click" Visible="false" />
                 <asp:ImageButton ID="imgbtnprint" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                     ToolTip="Export" OnClick="imgbtnprint_Click" Visible="false" />
                  <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                      ToolTip="Audit Log" OnClick="imgbtnAudit_Click" Visible="false" />
                 <%-- <asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                      ToolTip="Information" Visible="false" />--%>
                 
                 
                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                      <Triggers>
                          <asp:PostBackTrigger ControlID="imgbtnAudit" />
                      </Triggers>
                      <ContentTemplate>
                          <span hidden>
                              <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" />&nbsp;&nbsp;    </span>
                      </ContentTemplate>
                  </asp:UpdatePanel>

                </div>
 </div>
                </div>
            <div class="card">
                <div class="col-md-12">

                    <div class="nav-tabs-custom">
                        <div class="box box-primary box-solid" style="border: 1px solid #8acef9">
                            <div class="box-header" style="background-color: #8acef9">
                                <asp:UpdatePanel ID="upnlTabs" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hfTabID" runat="server" Value="#roles" />
                                        <ul class="nav nav-tabs" id="myTabs">
                                            <li class="active"><a href="#tab_1" id="tab1" data-toggle="tab" style="font-weight: 100; font-size: 20px; border-color: rgba(221, 221, 221, 0.5490196078431373);" onclick="activatetab('#tab_1');">Block Billing Search </a></li>
                                            <li id="litab2" runat="server"><a id="tab2" href="#tab_2" data-toggle="tab" style="font-weight: 100; font-size: 20px; border-color: rgba(221, 221, 221, 0.5490196078431373);" onclick="activatetab('#tab_2');"> Invoice Generation </a></li>
                                            <asp:LinkButton ID="lbtnClickOrder" runat="server"></asp:LinkButton>
                                        </ul>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="box-body">
                                <div class="tab-content">
                                    <!-- 1st tab open -->
                                    <div class="tab-pane active" id="tab_1">
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                                    <ContentTemplate>
                                                        <!--  HEADERS open-->
                                                        <div class="col-sm-12" runat="server" id="Div1">
                                                            <div class="col-sm-4">
                                                                <h1>
                                                                    <asp:Label for="inputEmail3" class="col-sm-2 control-label" ID="Label2" Style="margin-left: 74px;" runat="server" Visible="false"></asp:Label></h1>

                                                            </div>
                                                            <div class="col-sm-5">
                                                            </div>
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-1" id="Div2" runat="server" visible="false">
                                                            </div>

                                                        </div>
                                                        <!--  HEADERS close-->
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Invoice No</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtInv" runat="server" CssClass="bootformcontrol" ReadOnly="false" ToolTip="ID"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Patient Name</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSName" runat="server" CssClass="bootformcontrol" ReadOnly="false" ToolTip="Patient Name"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Request Number</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtReqNo" runat="server" CssClass="bootformcontrol" ReadOnly="false" ToolTip="MR Number"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">MRN</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtHRNID" runat="server" CssClass="bootformcontrol" ReadOnly="false" ToolTip="MRN"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Invoice Date From</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtDateFrmSearch" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDateFrmSearch" runat="server"
                                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Invoice Date To</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtDateToSearch" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDateToSearch" runat="server"
                                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Requestor Type</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlRequestorType" runat="server" ToolTip="Paid at"  CssClass="form-control ">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Requestor</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:UpdatePanel ID="updpnltxtReqID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtReqname" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtReqID" CssClass="form-control"></asp:TextBox>

                                                                            </span>
                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                     <asp:ImageButton AlternateText="REQ_txtReqID_txtReqname_updpnltxtReqID" runat="server" ID="imgbtntrigerREQ" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="REQ_txtReqID_txtReqname" runat="server" ID="imgbtnClearREQ" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                           <%-- <asp:DropDownList ID="ddlReq" runat="server" ToolTip="#" AutoPostBack="true" CssClass="form-control ">
                                                            </asp:DropDownList>--%>
                                                        </div>
                                                    </div>
                                                    &nbsp;                                                      
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Payment Status</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="#" CssClass="form-control ">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label"></label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btns-search btn-info" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i> Search</asp:LinkButton>
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="btn btns-save btnred" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lbtnClear_Click"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear</asp:LinkButton>
                            </div>
                                                &nbsp;
                                                <asp:Panel ID="pnlblockbillingsearch" runat="server" Visible="false">
                                                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed; width: 99%">
                                                        <div class="box-header with-border TotalRecord">
                                                            <div class="box-title">
                                                                <label class="text-right" style="color: white">Total Records : <span id="lbltotalrecblockbillingsearch" runat="server"></span></label>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="box-body">
                                                            <div class="form">
                                                                <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                                                <div class="table-responsive table--no-card m-b-30">
                                                                    <asp:GridView ID="gvgridblockbillingsearch" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                        AutoGenerateColumns="False"
                                                                        CellPadding="2"
                                                                        ForeColor="#333333"
                                                                        HorizontalAlign="Center"
                                                                        PageSize="10"
                                                                        CssClass="table table-borderless table-striped">

                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkbtnblockbillingsearchGrid" runat="server" Text="<%#Bind('Inv_ID')%>" CommandArgument='<%#Eval("Inv_ID")%>'
                                                                                        OnClick="lnkbtnblockbillingsearchGrid_Click">
                                                                                    </asp:LinkButton>

                                                                                    
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnInvoice" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Invoice No" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Inv_ID" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblInvoice_Da" runat="server" Text='<%#Bind("invoice_date", "{0:dd-MM-yyyy }")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnInvoiceDte" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Invoice Date" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="invoice_date" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="Label1" runat="server" Text="<%#Bind('Requestor')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtRequestor" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Requestor" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Requestor" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>



                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblTotal_Amt" runat="server" Text="<%#Bind('Total_Amt')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnTotalAmt" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Total Amount" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Total_Amt" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblpaymentStatus" runat="server" Text='<%#Bind("Payment_Status", "{0:dd-MM-yyyy }")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnFromDate" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Payment Status " Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Payment_Status" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPaymentDate" runat="server" Text='<%#Bind("Payment_Date", "{0:dd-MM-yyyy }")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnToDate" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Payment Date" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Payment_Date" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle   HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblGenerated" runat="server" Text="<%#Bind('Invoice_Generated_BY')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnGenerated" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Invoice Generated By" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Invoice_Generated_BY" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
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
                                                                <div class="col-lg-12">
                                                                    <div class="row form-group">
                                                                        <div class="col-sm-12" style="text-align: center">
                                                                            <asp:Repeater ID="rptPagerblockbillingsearch" runat="server">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkPageblockbillingsearch" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPageblockbillingsearch_Click">
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
                                                <asp:Panel ID="pnlblockbillingsearchDetail" runat="server" Visible="false">
                                                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed; width: 99%">
                                                        <div class="box-header with-border TotalRecord">
                                                            <div class="box-title">
                                                                <label class="text-right" style="color: white">Total Records : <span id="intTotalRecordrecblockbillingsearchDetail" runat="server"></span></label>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="box-body">
                                                            <div class="form">
                                                                <asp:HiddenField ID="HiddenField4" runat="server" />
                                                                <div class="table-responsive table--no-card m-b-30">
                                                                    <asp:GridView ID="gvgridblockbillingDetail" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                        AutoGenerateColumns="False"
                                                                        CellPadding="2"
                                                                        ForeColor="#333333"
                                                                        HorizontalAlign="Center"
                                                                        PageSize="10"
                                                                        CssClass="table table-borderless table-striped" OnRowDataBound="gvgridblockbillingsearch_RowDataBound">

                                                                        <Columns>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblRequestor" runat="server" Text="<%#Bind('MR_ID')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnRequestor" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="MR Number" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="MR_ID" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkbtnRequest_IDgvlist" Visible="false" runat="server" Text="<%#Bind('Request_ID')%>" CommandArgument='<%#Eval("Request_ID")%>'
                                                                                        OnClick="lnkbtnRequest_IDgvlist_Click">
                                                                                    </asp:LinkButton>
                                                                                     <asp:label ID="lblblockillingsearch" runat="server" Visible="false" Text="<%#Bind('Request_ID')%>">
                                                                                        
                                                                                    </asp:label>

                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="LnkbtnSortInvoiceGrid" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Request No" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Request_ID" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblHRNid" runat="server" Text="<%#Bind('HRN_ID')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnhrnid" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="MRN" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="PatName" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblPatName" runat="server" Text="<%#Bind('PatName')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnPatName" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Patient Name" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="PatName" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblRequestedDate" runat="server" Text="<%#Bind('RequestedBy')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnRequestedDate" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Requestor" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="RequestedBy" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lbldepartment" runat="server" Text="<%#Bind('department')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtndepartment" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Department" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="department " OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblAmount01" runat="server" Text="<%#Bind('Amount')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnAmount01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="MR Amount" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Amount" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblStatuso" runat="server" Text="<%#Bind('Status')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbStatust" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="MR Status" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Status" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblForwardedDate" runat="server" Text='<%#Bind("ReportCompletedDate", "{0:dd-MM-yyyy }")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnFrom_Date" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Forwarded Date" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="ReportCompletedDate" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
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
                                                                            <asp:Repeater ID="rptPagerblockbillingsearchDetail" runat="server">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkPageblockbillingsearchDetail" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPageblockbillingsearchDetail_Click">
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
                                        </div>
                                    </div>
                                    <!--1st tab close -->

                                    <!-- 2nd tab open -->
                                    <div class="tab-pane" id="tab_2">
                                        


                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <asp:UpdatePanel ID="upnl" runat="server">

                                                    <ContentTemplate>
                                                        <!--  HEADERS open-->
                                                        <div class="col-sm-12" runat="server" id="idRep">
                                                            <div class="col-sm-4">
                                                                <h1>
                                                                    <asp:Label for="inputEmail3" class="col-sm-2 control-label" ID="lblId" Style="margin-left: 74px;" runat="server" Visible="false"></asp:Label></h1>

                                                            </div>
                                                            <div class="col-sm-5">
                                                            </div>
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-1" id="btncan" runat="server" visible="false">
                                                            </div>

                                                        </div>
                                                        <!--  HEADERS close-->
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>


                                                <div class="form-group">

                                                    <label for="inputEmail3" class="col-sm-2 control-label">Date From </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtDateFrm" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtdateform_CalendarExtender" TargetControlID="txtDateFrm" runat="server"
                                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Date To </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtdateto_CalendarExtender" TargetControlID="txtDateTo" runat="server"
                                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Requestor</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:UpdatePanel ID="updpnltxtRequestorID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtRequestorname" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtRequestorID" CssClass="form-control"></asp:TextBox>

                                                                            </span>
                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                     <asp:ImageButton AlternateText="REQUESTOR_txtRequestorID_txtRequestorname_updpnltxtRequestorID" runat="server" ID="imgbtntrigerREQUESTOR" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="REQUESTOR_txtRequestorID_txtRequestorname" runat="server" ID="imgbtnClearREQUESTOR" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                            <%--<asp:DropDownList ID="ddlRequestor" runat="server" ToolTip="Requestor" CssClass="form-control ">
                                                            </asp:DropDownList>--%>

                                                        </div>
                                                    </div>

                                                    <div class="col-sm-1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label"></label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:Button ID="BtnGen" runat="server" CssClass="btn btngreen" Style="font-size: 14px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="BtnGen_Click" Text="Generate" />

                                                        </div>
                                                    </div>
                                                </div>

                                                &nbsp;
                                                <asp:Panel ID="pnlBBGeneration" runat="server" Visible="false">
                                                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #ad9ed6; width: 99%">
                                                        <div class="box-header with-border TotalRecord" style="left: 1px; top: 0px;">
                                                            <div class="box-title">
                                                                <label class="text-right" style="color: white">Total Records : <span id="lblTotalRecordsBBGeneration" runat="server"></span></label>
                                                            </div>
                                                        </div>
                                                     
                                                        <div class="box-body">
                                                            <div class="form">
                                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                <div class="table-responsive table--no-card m-b-30">
                                                                    <asp:GridView ID="gvgridBBGeneration" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                        AutoGenerateColumns="False"
                                                                        CellPadding="2"
                                                                        ForeColor="#333333"
                                                                        HorizontalAlign="Center"
                                                                        PageSize="10"
                                                                        CssClass="table table-borderless table-striped">

                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkbtnBBGenerationGrid" runat="server" Text="<%#Bind('Inv_ID')%>" CommandArgument='<%#Eval("Inv_ID")%>'
                                                                                        OnClick="lnkbtnBBGenerationGrid_Click">
                                                                                    </asp:LinkButton>

                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnInvoice01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Invoice No" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Inv_ID" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblInvoice_Date01" runat="server" Text='<%#Bind("invoice_date", "{0:dd-MM-yyyy }")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnInvoiceDte01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Invoice Date" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="invoice_date" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblRequestor01" runat="server" Text="<%#Bind('Requestor')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtRequestor01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Requestor" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Requestor" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFrom_Date01" runat="server" Text='<%#Bind("From_Date", "{0:dd-MM-yyyy }")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnFromDate01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="From Date" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="From_Date" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblToDate01" runat="server" Text='<%#Bind("ToDate", "{0:dd-MM-yyyy }")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnToDate01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="To Date" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="ToDate" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblTotal_Amt01" runat="server" Text="<%#Bind('Total_Amt')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnTotalAmt01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Total Amount" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Total_Amt" OnClick="LnkbtnSort_Click">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
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
                                                                            <asp:Repeater ID="rptPagergvBBGeneration" runat="server">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkPagegvInvoice" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPagegvInvoice_Click">
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

                                                <asp:Panel ID="pnlBBgenerateDetail" runat="server" Visible="false">
                                                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed; width: 99%">
                                                        <div class="box-header with-border TotalRecord">
                                                            <div class="box-title">
                                                                <label class="text-right" style="color: white">Total Records : <span id="intTotalRecordpaymentgenerateDetail" runat="server"></span></label>
                                                            </div>
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="form">
                                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                                                <div class="table-responsive table--no-card m-b-30">
                                                                    <asp:GridView ID="gvGridBBgenerateDetail" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                        AutoGenerateColumns="False" OnRowDataBound="gvGridBBgenerateDetail_RowDataBound"
                                                                        CellPadding="2"
                                                                        ForeColor="#333333"
                                                                        HorizontalAlign="Center"
                                                                        PageSize="10"
                                                                        CssClass="table table-borderless table-striped">

                                                                        <Columns>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblRequestor01" runat="server" Text="<%#Bind('MR_ID')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnRequestor01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="MR Number" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="MR_ID">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkbtnRequest_IDBBgenerateDetail" runat="server" Text="<%#Bind('Request_ID')%>" CommandArgument='<%#Eval("Request_ID")%>'
                                                                                        OnClick="lnkbtnRequest_IDBBgenerateDetail_Click">
                                                                                    </asp:LinkButton>

                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="LnkbtnSortInvoiceGrid01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Request No" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Request_ID">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblPatName01" runat="server" Text="<%#Bind('PatName')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnPatName01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Patient Name" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="PatName">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblRequestedDate01" runat="server" Text="<%#Bind('RequestedBy')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnRequestedDate01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Request Date" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="RequestedBy">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblAmount01" runat="server" Text="<%#Bind('Amount')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnAmount01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="MR Amount" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Amount">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="lblStatuso01" runat="server" Text="<%#Bind('Status')%>"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbStatust01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="MR Status" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="Status">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblForwardedDate01" runat="server" Text='<%#Bind("ReportCompletedDate", "{0:dd-MM-yyyy }")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderTemplate>
                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                        <tr>
                                                                                            <td class="gridtabletd">
                                                                                                <asp:LinkButton ID="lnkbtnFrom_Date01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                    Text="Forwarded Date" Font-Names="Arial"  ForeColor="White"
                                                                                                    CommandArgument="ReportCompletedDate">
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle  HorizontalAlign="Left" />
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
                                                                            <asp:Repeater ID="rptPagerBBgenerateDetail" runat="server">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkPagegvList" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPageBBgenerateDetail_Click">
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
                                        </div>

                                    </div>
                                    <!-- 2nd tab close -->


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
           </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- 2n tab popup close -->
    <div>
        <tr>
            <td>
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
            </td>
        </tr>


    </div>

    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script src="js/jquery-1.7.js" type="text/javascript"></script>
    <script src="js/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="js/1.8.9/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="bower_components/jquery/dist/jquery.min.js"></script>

    <script>
        function activatetab(tid) {
            document.getElementById("<%=hfTabID.ClientID%>").value = tid;
            SetTab(tid);
            ClickProductTab();
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {

            prm.add_endRequest(function (sender, e) {
                SetTab(document.getElementById("<%=hfTabID.ClientID %>").value);
            });
        }
        function SetTab(tabid) {
            $('#myTabs a[href="' + tabid + '"]').tab('show');
        }
        function ClickTab1() {
            document.getElementById('tab1').click();
        }
        function Clicktab2() {
            document.getElementById('tab2').click();
        }

        function ClickTab3() {
            document.getElementById('tab3').click();
        }


        function SetTarget() {
            document.forms[0].target = "_blank";
        }

        function ResetTarget() {
            document.forms[0].target = "_self";
        }

        function SetSession() {

            document.getElementById("<%= hdnClickcancel.ClientID %>").value = "1";
        }
        //need to check
        function keyUPQty(txt) {
            __doPostBack("txtQty", "txtQty_TextChanged");
        }


        function ClickProductTab() {
            if (document.getElementById("<%=hfTabID.ClientID %>").value == "#tab_3")
                document.getElementById("<%=lbtnClickOrder.ClientID%>").click();
        }

    </script>
</asp:Content>
