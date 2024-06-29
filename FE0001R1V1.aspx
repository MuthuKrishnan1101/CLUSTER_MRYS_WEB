<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FE0001R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FE0001R1V1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnl6" runat="server">
        <ContentTemplate>
             <asp:HiddenField ID="HiddenField1" runat="server" />
             
                        <asp:HiddenField ID="hdnPopupDropdownValue" runat="server" />
                        <asp:HiddenField ID="hdnPopuptxtboxValue" runat="server" />
                        <asp:HiddenField ID="hdnPopuptxtboxnameValue" runat="server" />
                        <asp:HiddenField ID="hdnPopupupdatepnlValue" runat="server" />
                        <asp:HiddenField ID="hdnfrom" runat="server" />
                        <asp:HiddenField ID="hdnto" runat="server" />
                        <asp:HiddenField ID="hdnpageIndex" runat="server" />

            <div class="ToolBarcard">
                <div class="row ToolBar">
                    <div class="col-xs-12 col-sm-12 col-md-12 text-right">

                        <asp:ImageButton ID="imgbtnprint" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                            ToolTip="Export" />

                       <%-- <asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                            ToolTip="Information" />--%>
                    </div>
                </div>
            </div>
            <div class="card">
                <asp:HiddenField ID="hdnClickEvent" runat="server" />
                <div class="" style="border-radius: 30px; background-color: white; padding: 15px; box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;">
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                     <label for="inputEmail3" class="control-label mb-1" style="color: black">Request Number</label>
                                    <asp:TextBox runat="server" ID="txtReqNo" CssClass="form-control" onkeypress="return enter1_click(event);"></asp:TextBox>
                                   
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                      <label for="inputEmail3" class="control-label mb-1" style="color: black">MR Status</label>
                                    <asp:DropDownList ID="ddlMRStatus" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                    </asp:DropDownList>
                                   <%-- <label for="inputEmail3" class="control-label mb-1" style="color: black">Request Category  </label>
                                    <asp:DropDownList ID="ddlRecType" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                    </asp:DropDownList>--%>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                    <label for="inputEmail3" class="control-label mb-1" style="color: black">MRN</label>
                                    <asp:TextBox ID="txtHRN" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                    <label for="inputEmail3" class="control-label mb-1" style="color: black">Patient Name</label>
                                    <asp:TextBox ID="txtPatName" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="row">

                            <div class="col-sm-5 col-lg-2" style="margin-top: 2px;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="control-label mb-1" style="color: black"> Created Date From  </label>
                                    <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtFrmDate_CalendarExtender" TargetControlID="txtFrmDate" runat="server"
                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                  <label for="inputEmail3" class="control-label mb-1" style="color: black"> Created Date To</label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtToDate_CalendarExtender" TargetControlID="txtToDate" runat="server"
                                        Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                    <label for="inputEmail3" class="control-label mb-1" style="color: black">Waiver Application </label>
                                    <asp:DropDownList ID="ddlWApp" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                        <asp:ListItem Text="" Value=""> </asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="YES"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="NO"></asp:ListItem>
                                    </asp:DropDownList>

                                   
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                     <label for="inputEmail3" class="control-label mb-1" style="color: black"> Block Billing </label>
                                    <asp:DropDownList ID="ddlblockbiining" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="YES">Yes</asp:ListItem>
                                        <asp:ListItem Value="NO">No</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                   
                                </div>
                            </div>
                        </div>


                         <div class="row">

                            <div class="col-sm-5 col-lg-2" style="margin-top: 2px;">
                                <div class="form-group">
                                    <label for="inputEmail3" class="control-label mb-1" style="color: black"> Requestor  </label>
                                    <asp:UpdatePanel ID="updpnltxtrequestor01ID" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtrequestor01name" CssClass="form-control wrap" onkeypress="return isDropDown(event)" OnPaste="JavaScript:return RestrictCopyPasteDropDown();"></asp:TextBox>

                                                                            <span hidden>
                                                                                <asp:TextBox runat="server" ID="txtrequestor01ID" CssClass="form-control"></asp:TextBox>

                                                                            </span>
                                                                            <div class="ddlimgbtn col-xs-12 col-sm-12 col-md-12 text-right">
                                                                                <span class="downarrrow">
                                                                                     <asp:ImageButton AlternateText="REQUESTOR01_txtrequestor01ID_txtrequestor01name_updpnltxtrequestor01ID" runat="server" ID="imgbtntrigerREQUESTOR01" ImageUrl="images/arrow 2.png" Height="14px" Width="18px" OnClick="imgbtntrigerPopup_Click" />
                                                                                </span>

                                                                                <span class="CrossMark">
                                                                                    <asp:ImageButton AlternateText="REQUESTOR01_txtrequestor01ID_txtrequestor01name" runat="server" ID="imgbtnClearREQUESTOR01" ImageUrl="images/cross mark.png" Height="9px" Width="12px" OnClick="imgbtnCleardropdowntxtboxvalue_Click" />
                                                                                </span>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                  <%-- <asp:DropDownList ID="ddlrequestor01" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                    </asp:DropDownList>--%>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                  <label for="inputEmail3" class="control-label mb-1" style="color: black">Requestor Type</label>
                                    <asp:DropDownList ID="ddlRequestorType" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                    <label for="inputEmail3" class="control-label mb-1" style="color: black"> Report type  </label>
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
                                    <%--<asp:DropDownList ID="ddlRecType" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                    </asp:DropDownList>--%>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2">
                                <div class="form-group">
                                  <label for="inputEmail3" class="control-label mb-1" style="color: black">Request Type </label>
                                    <asp:DropDownList ID="ddlReqType" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            
                        </div>


                        <div class="row">
                            <div class="col-sm-5 col-lg-2" style="margin-top: 2px;">
                                <div class="form-group">
                                   <label for="inputEmail3" class="control-label mb-1" style="color: black"> Received From  </label>
                                    <asp:DropDownList ID="ddlreceivedfrom" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2" style="margin-top: 2px;">
                                <div class="form-group">
                                     <label for="inputEmail3" class="control-label mb-1" style="color: black"> Delivery Mode </label>
                                    <asp:DropDownList ID="ddldeliverymode" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2" style="margin-top: 2px;">
                                <div class="form-group">
                                     <label for="inputEmail3" class="control-label mb-1" style="color: black">Department OU </label>
                                    <asp:DropDownList ID="ddlDepartmentOU" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2" style="margin-top: 2px;">
                                <div class="form-group">
                                  <label for="inputEmail3" class="control-label mb-1" style="color: black">Application Status </label>
                                    <asp:DropDownList ID="ddlApplicationStatus" runat="server" CssClass="form-control" onkeypress="return enter1_click(event);">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="Active">Active</asp:ListItem>
                                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-5 col-lg-2" style="margin-top: 2px;">
                                <div class="form-group">
                                    
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btns-search btn-info" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i> Search</asp:LinkButton>
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="btn btns-save btnred" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lbtnClear_Click"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear</asp:LinkButton>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>
                <br />
                <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed; box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;">
                    <div class="box-header with-border TotalRecord">
                        <div class="box-title">
                            <label class="text-right" style="color: white">Total Records : <span id="lblTotalRecords" runat="server"></span></label>
                        </div>
                    </div>
                  
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="form">
                            <asp:HiddenField ID="hdnVisiblity" runat="server" />
                            <div class="table-responsive table--no-card m-b-30">
                                <asp:GridView ID="gvUserHistory" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                    AutoGenerateColumns="False"
                                    CellPadding="2"
                                    ForeColor="#333333"
                                    HorizontalAlign="Center"
                                    PageSize="10"
                                    CssClass="table table-borderless table-striped" OnRowDataBound="gvUserHistory_RowDataBound">
                                    <%--<PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />--%>
                                    <Columns>


                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnUserID" runat="server" Text="<%#Bind('Request_ID')%>" CommandArgument='<%#Eval("Request_ID")+","+ Eval("HRN_ID")%>'
                                                    OnClick="lnkbtnUserID_Click"></asp:LinkButton>

                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnusrid" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Request Number" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="Request_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                <asp:Label ID="lblordno" runat="server" Text="<%#Bind('HRN_ID')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnprocesssts7" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="MRN" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="HRN_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                <asp:Label ID="lblTransDate" runat="server" Text='<%#Bind("Receive_Date", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnHeadershortName9" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Request Date" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="Receive_Date" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                <asp:Label ID="lblEXPECTED_DATE" runat="server" Text='<%#Bind("SentDate", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkEXPECTED_DATE" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Despatch Date" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="SentDate" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                <asp:Label ID="lblSentDate" runat="server" Text='<%#Bind("REASSESMENT_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkSentDate" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Re-Despatch Date" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="REASSESMENT_DATE" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                <asp:Label ID="lbljobsts" runat="server" Text="<%#Bind('Requestor')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnprocesssts6" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Requestor" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="Requestor" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                <asp:Label ID="lbljobsts017" runat="server" Text="<%#Bind('received_from')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Received From" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="received_from" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                <asp:Label ID="lbljobtype" runat="server" Text="<%#Bind('Dept')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnprocesssts5" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Department" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="Dept" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                <asp:Label ID="lbljobst1s" runat="server" Text="<%#Bind('Doc_Name')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnprocessst1s6" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Doctor" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="Doc_Name" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                <asp:Label ID="lblsts1" runat="server" Text="<%#Bind('STATUS')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnsts1" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Status" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="STATUS" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="9%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Image ID="imggridflag01" runat="server" Visible="false" ImageUrl="Images/PriorityFlag.png" Style="height: 35px; width: 35px;"></asp:Image>
                                            </ItemTemplate>
                                             <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnpriority" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Priority Flag" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="PRIORITY_FLAG" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="btnselect" OnClick="btnSelect_Click" CommandArgument='<%#Eval("Request_ID")+","+ Eval("HRN_ID")%>' />
                                                <controlstyle cssclass="btn btnred"></controlstyle>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #ad9ed6; border-collapse: collapse;">
                                                    <tr>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>

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
                                                    CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPage_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>


                        </div>
                    </div>
                </div>

                <asp:Panel runat="server" ID="pnlbiodatta" Visible="false">
                    <asp:LinkButton ID="lnkbtnProducts2" runat="server"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnCancel22" runat="server"></asp:LinkButton>
                    <ajax:ModalPopupExtender ID="mpePdtPlt2" BehaviorID="mpePdtPlt12" runat="server" TargetControlID="lnkbtnProducts2" CancelControlID="lbtnCancel22" PopupControlID="pnlPdtPlt22" BackgroundCssClass="modal-background"></ajax:ModalPopupExtender>

                    <asp:Panel ID="pnlPdtPlt22" runat="server" Width="950">
                        <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                            <div class="modal-dialog modal-lg panelBiodata" style="width: 75%;">
                                <div class="modal-content PopupModelContent">
                                    <div class="text-center" style="background-color: #304863;">
                                        <h4 class="modal-title" style="font-weight: bold; font-size: 16px; color: white"></h4>
                                    </div>
                                    <div class="modal-body" style="border-radius: 20px;">
                                        <div class="nav-tabs-custom">
                                            <div class="box box-primary box-solid" style="border: 2px solid #2aa7ed">
                                                <div class="box-header Popboxheader" style="">
                                                    <ul class="nav nav-tabs">
                                                        <li class="active" style="background-color: #e3f6fd; border-radius: 5px;"><a href="#tabPrd_1" data-toggle="tab" style="font-weight: 500; font-size: 15px; bordproductr: rgba(221, 221, 221, 0.5490196078431373);">
                                                            <asp:Label runat="server" ID="lbl2ndLvlTabTilte" Text="Filter"></asp:Label>
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
                                    <table width="100%" border="0">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td width="24">
                                                <asp:TextBox ID="txt" runat="server" CssClass="form-control" Width="200"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;&nbsp;
                                                <asp:ImageButton ID="imgbtnNew" runat="server" class="MenuImageButton" ImageUrl="Images/search (1).png"
                                                    ToolTip="Create or Return To Fresh New Page" Width="30" Height="30" Style="margin-top: 10px;" />
                                            </td>
                                            <td style="float: right">
                                                <asp:ImageButton ID="imgbtnSave" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                                                    ToolTip="Save Record" Style="margin-top: 10px;" />
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    <div class="box-body">
                                        <div class="form">
                                            <div class="table-responsive table--no-card m-b-30" id="divid">
                                                <asp:GridView ID="GridView1" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                    AutoGenerateColumns="False"
                                                    CellPadding="2"
                                                    ForeColor="#333333"
                                                    HorizontalAlign="Center"
                                                    PageSize="10"
                                                    CssClass="table table-borderless table-striped" OnRowDataBound="gvUserHistory_RowDataBound">
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                    <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkPP" AutoPostBack="true" CssClass="form-control" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="text-left" Width="70px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequest_ID" runat="server" Text="<%#Bind('Request_ID')%>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd">
                                                                            <asp:LinkButton ID="lnkRequest_ID" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                Text="S/No." Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                                                CommandArgument="Request_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                <asp:Label ID="lblMR_ID" runat="server" Text="<%#Bind('MR_ID')%>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd">
                                                                            <asp:LinkButton ID="lnkMR_ID" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                Text="Data Description" Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                                                CommandArgument="MR_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                <asp:Label ID="lblpat_name" runat="server" Text="<%#Bind('PATIENT_NAME')%>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd">
                                                                            <asp:LinkButton ID="lnkbtnpat_name" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                Text="Data Type" Font-Names="Arial" Font-Size="12pt" ForeColor="White"
                                                                                CommandArgument="PATIENT_NAME" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemStyle Width="1%" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                    <RowStyle CssClass="GridviewRowStyle" />
                                                    <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="row form-group">
                                            <div class="col-sm-12" style="text-align: center">
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                            CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPage_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="modal-footer" style="text-align: center;">
                                        <asp:Button runat="server" ID="btnCancelCongWH_Name" Text="Cancel" CssClass="btn btnred" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                </asp:Panel>
            </div>

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
                                        <table style="width: 100%; text-align: center" bgcolor="#f4eaff">
                                            <tr>

                                                <td style="width: 100%; text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #000080;"
                                                    height="30px">
                                                    <asp:Label ID="Label1" runat="server" Text="Redirect to Registration <br> Screen" ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
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
                                                                <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt;">There is no pending Request for this Condition, Do you want to create a new Request ?</td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="text-align: center">
                                                    <asp:Button ID="btnconfirmok" runat="server" align="right" CssClass="btn btngreen"
                                                        Text="Yes" OnClick="btnconfirmok_Click" />
                                                    <asp:Button ID="Button1" runat="server" align="right" CssClass="btn btnred"
                                                        Text="NO" />
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
                                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="38px" ImageUrl="Images/PopColse.png"
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
                                                                                                                    <asp:Button runat="server" ID="btnfindddlpopupRecord" CssClass="form-control btn-primary" Width="130px" style="color: white !important; font-size: 16px" OnClick="btnfindddlpopupRecord_Click" Text="Find" />
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
    <script>
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        function BeginRequestHandler(sender, args) {
            if ($get('divid') != null) {
                xPos = $get('divid').scrollLeft;
                yPos = $get('divid').scrollTop;
            }
        }
        function EndRequestHandler(sender, args) {
            if ($get('divid') != null) {
                $get('divid').scrollLeft = xPos;
                $get('divid').scrollTop = yPos;
            }
        }
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function enter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=lbtnSearch.ClientID%>").click();
                return false;
            }
        }

    </script>
</asp:Content>

