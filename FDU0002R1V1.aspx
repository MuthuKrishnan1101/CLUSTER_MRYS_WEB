<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FDU0002R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FDU0002R1V1" %>
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
            <div class="ToolBarcard">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">
                        <asp:ImageButton ID="imgbtnClear" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                            ToolTip="Add New" OnClick="imgbtnClear_Click" />
                 <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                     ToolTip="Show all records" OnClick="imgBtnSearch_Click" />
              
                <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                    ToolTip="Save" />
                 
                 <asp:ImageButton ID="imgbtnDelete" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                     ToolTip="Delete" />
                 <asp:ImageButton ID="imgbtnprint" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                     ToolTip="Export" />
                  <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                      ToolTip="Audit Log" />
                  <%--<asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                      ToolTip="Information" />--%>
                    </div>
                </div>
                </div>
            <div class="card">
                <div class="box box-solid" style="border-radius: 30px;">
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
                                                <label for="inputEmail3" class="col-sm-2 control-label">Group Name </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtgroupname" runat="server" CssClass="form-control" autocomplete="off"  onkeypress="return Biodataenter1_click(event);"></asp:TextBox>
                                                    </div>
                                                </div>
                                                
                                               <%-- <label for="inputEmail3" class="col-sm-2 control-label">Patient Name</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtPatName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>--%>

                                           <%-- <div class="form-group">
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
                                            </div>--%>

                                            <%--<div class="form-group">
                                                <div class="bootcolsm1">
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">Action By</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlactby" runat="server" ToolTip="Institution ID" AutoPostBack="true" CssClass="bootformcontrol">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                                <%--<label for="inputEmail3" class="col-sm-2 control-label">Enquiry Status</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlEnqStatus" runat="server" ToolTip="Institution ID" AutoPostBack="true" CssClass="bootformcontrol">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                            </div>

                                            
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
          

            <asp:Panel ID="pnlresultgrid" runat="server" Visible="false">
                <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                    <div class="box-header with-border TotalRecord">
                        <div class="box-title">
                            <label class="text-right" style="color: white">Total Records : <span id="lblTotalRecords" runat="server"></span></label>
                        </div>
                    </div>
                    <br />
                    <div class="box-body">
                        <div class="form">
                            <asp:HiddenField ID="hdnVisiblity" runat="server" />
                            <div class="table-responsive table--no-card m-b-30" id="divid">
                                <asp:GridView ID="gvList" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                    AutoGenerateColumns="False"
                                    CellPadding="2"
                                    ForeColor="#333333"
                                    HorizontalAlign="Center"
                                    PageSize="10"
                                    CssClass="table table-borderless table-striped">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnbe_id" runat="server" Text="<%#Bind('grp_name')%>" CommandArgument='<%#Eval("grp_id")%>' OnClick="lnkbtnREQUEST_IDGrid_Click">
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnreqno" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Group Name" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="grp_name">
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
                                                <asp:Label ID="lblheseq" runat="server" Text="<%#Bind('HR_SEQUENCE')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtngrp_name" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="HR Sequence" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="grp_name"></asp:LinkButton>
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
                                                <asp:Label ID="lblBE_ID" runat="server" Text='<%#Bind("BE_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnpatname" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Institution" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="BE_ID"></asp:LinkButton>
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

                                                <asp:Label ID="lblcreated_on" runat="server" Text='<%#Bind("created_on", "{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnrequestor" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Created Date" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="created_on"></asp:LinkButton>
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
                                                <asp:Label ID="lblcreated_by" runat="server" Text="<%#Bind('created_by')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnreporttype" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Created By" Font-Names="Arial"  ForeColor="White"
                                                                CommandArgument="CALLERS_ENQUIRY"></asp:LinkButton>
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
                <!-- Update Process Popup start -->
            <asp:Panel runat="server" ID="pnlupdateremarksandprocess" Visible="false">
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
                                                       <asp:DropDownList ID="ddlstaffworklist" runat="server" ToolTip="Institution ID" AutoPostBack="true" CssClass="bootformcontrol">
                                                        </asp:DropDownList>
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
                                                                        <asp:TextBox ID="txtProcessCompletedRemarks" MaxLength="20" runat="server" CssClass="form-control" Visible="true"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%"></td>
                                                                    <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt;">Are you sure you want to Transfer The Request?</td>
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
                                                    <td colspan="3" style="text-align: center">
                                                        <asp:Button ID="btnConfirmprocessStatus" runat="server" align="right" CssClass="btn btn-success"
                                                            Text="Yes" OnClick="btnConfirmprocessStatus_Click" />
                                                        <asp:Button ID="btnConfirmprocessClose" runat="server" align="right" CssClass="btn btn-danger"
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function Biodataenter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=imgbtnSearch.ClientID%>").click();
                 return false;
             }
         }
         
    </script>
</asp:Content>
