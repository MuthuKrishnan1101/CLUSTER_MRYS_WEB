<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FB0002R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FB0002R1V1" %>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnl6" runat="server">
        <ContentTemplate>

            <div class="ToolBarcard" hidden>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">
                        <asp:ImageButton ID="imgbtnClear" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                            ToolTip="Add New" OnClick="imgbtnClear_Click" />

                        <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                            ToolTip="Show all records" OnClick="imgbtnSearch_Click" />
                             
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
                                        <table style="padding: 1px; margin: 1px; width: 100%; text-align: left; table-layout: auto; border-collapse: collapse;">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel1" Width="90%" runat="server" Style="margin-left: 20px;">
                                                        <div class="form-horizontal">
                                                            <div class="box-body">
                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Request Number</label>
                                                                    <div class="col-sm-3">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtRequestNo" runat="server" CssClass="bootformcontrol" ToolTip="MR Number"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                    </div>
                                                                    <label for="inputEmail3" class="col-sm-2 control-label">MRN</label>
                                                                    <div class="col-sm-3">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtHRN" runat="server" CssClass="bootformcontrol" ToolTip="MRN"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Receipt Number</label>
                                                                    <div class="col-sm-3">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtReceipt" runat="server" CssClass="bootformcontrol" ToolTip="Receipt Number"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                    </div>
                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Paid At</label>
                                                                    <div class="col-sm-3">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="ddlPaidAt" runat="server" CssClass="bootformcontrol">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Received Date From</label>
                                                                    <div class="col-sm-3">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtDateFrm" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="txtdateform_CalendarExtender" TargetControlID="txtDateFrm" runat="server"
                                                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                    </div>
                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Received Date To</label>
                                                                    <div class="col-sm-3">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="txtdateto_CalendarExtender" TargetControlID="txtDateTo" runat="server"
                                                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-2 control-label">External Receipt No</label>
                                                                    <div class="col-sm-3">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtExtNo" runat="server" CssClass="bootformcontrol" ToolTip="External Receipt No."></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                    </div>
                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Cheque No</label>
                                                                    <div class="col-sm-3">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass="bootformcontrol" ToolTip="Cheque No."></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-2 control-label">Payee Name</label>
                                                                    <div class="col-sm-3">
                                                                        <div class="form-group">
                                                                            <asp:TextBox ID="txtName" runat="server" CssClass="bootformcontrol" ToolTip="Patient Name"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                 <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btns-search btn-info" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i> Search</asp:LinkButton>
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="btn btns-save btnred" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lbtnClear_Click"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear</asp:LinkButton>
                            </div>
                                                                &nbsp;                                                      
                                                            </div>
                                                        </div>
                                                    </asp:Panel>

                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <asp:Panel ID="pnlgrid" runat="server" Visible="false">
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
                                        <asp:GridView ID="gvUserHistory" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                            AutoGenerateColumns="False" OnRowDataBound="gvUserHistory_RowDataBound"
                                            CellPadding="2"
                                            ForeColor="#333333"
                                            HorizontalAlign="Center"
                                            PageSize="10"
                                            CssClass="table table-borderless table-striped">

                                            <Columns>


                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnReceiptNo" runat="server" Text="<%#Bind('ReceiptNo')%>" OnClick="lnkbtnReceiptNo_Click" CommandArgument='<%#Eval("ReceiptNo")+","+ Eval("RequestNo")+","+ Eval("Payee_Name")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                            <tr>
                                                                <td class="gridtabletd">
                                                                    <asp:LinkButton ID="lnkbtnReceiptNo" runat="server" Font-Underline="false" Font-Bold="true"
                                                                        Text="Receipt No" Font-Names="Arial" ForeColor="White"
                                                                        CommandArgument="PayRcpt.Receipt_id" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                        <asp:Label ID="lblRequestNo" runat="server" Text="<%#Bind('RequestNo')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                            <tr>
                                                                <td class="gridtabletd">
                                                                    <asp:LinkButton ID="lnkbtnRequestNo" runat="server" Font-Underline="false" Font-Bold="true"
                                                                        Text="Request No" Font-Names="Arial" ForeColor="White"
                                                                        CommandArgument="Reg.Request_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                <asp:Label ID="lblmrnno" runat="server" Text="<%#Bind('HRN_ID')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:LinkButton ID="lnkbtnpmrn" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="MRN" Font-Names="Arial" ForeColor="White"
                                                                CommandArgument="Pat.HRN_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                        <asp:Label ID="lblPayee_Name" runat="server" Text="<%#Bind('Payee_Name')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                            <tr>
                                                                <td class="gridtabletd">
                                                                    <asp:LinkButton ID="lnkDELIVERY_DATE" runat="server" Font-Underline="false" Font-Bold="true"
                                                                        Text="Payee Name" Font-Names="Arial" ForeColor="White"
                                                                        CommandArgument="PayRcpt.Payee_Name" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                        <asp:Label ID="lblPayType" runat="server" Text="<%#Bind('PayType')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                            <tr>
                                                                <td class="gridtabletd">
                                                                    <asp:LinkButton ID="lnkDELIVERY_DATE02" runat="server" Font-Underline="false" Font-Bold="true"
                                                                        Text="Payment Type" Font-Names="Arial" ForeColor="White"
                                                                        CommandArgument="PayTyp.Short_Name" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                        <asp:Label ID="lblRequestDate" runat="server" Text='<%#Bind("Request_date","{0:dd-MM-yyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                            <tr>
                                                                <td class="gridtabletd">
                                                                    <asp:LinkButton ID="lnkRequestDate" runat="server" Font-Underline="false" Font-Bold="true"
                                                                        Text="Request Date" Font-Names="Arial" ForeColor="White"
                                                                        CommandArgument="Reg.Request_date" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblPaymentDate" runat="server" Text='<%#Bind("PaymentDate","{0:dd-MM-yyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                            <tr>
                                                                <td class="gridtabletd">
                                                                    <asp:LinkButton ID="lnkPaymentDate" runat="server" Font-Underline="false" Font-Bold="true"
                                                                        Text="Payment Date" Font-Names="Arial" ForeColor="White"
                                                                        CommandArgument="PayRcpt.Rcvd_Date" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                </asp:TemplateField> 

                                                <asp:TemplateField>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblAmount" runat="server" Text="<%#Bind('Amount')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                            <tr>
                                                                <td class="gridtabletd">
                                                                    <asp:LinkButton ID="lnkAmount" runat="server" Font-Underline="false" Font-Bold="true"
                                                                        Text="Amount" Font-Names="Arial" ForeColor="White"
                                                                        CommandArgument="PayRcpt.Rcvd_Amt" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                        <asp:Label ID="lblPaid_At" runat="server" Text="<%#Bind('Paid_At')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                            <tr>
                                                                <td class="gridtabletd">
                                                                    <asp:LinkButton ID="lnkMESSAGE_ON_CAKE" runat="server" Font-Underline="false" Font-Bold="true"
                                                                        Text="Paid At" Font-Names="Arial" ForeColor="White"
                                                                        CommandArgument="pa.Short_Name" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblPaymentStatus" runat="server" Text="<%#Bind('Payment_status')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                            <tr>
                                                                <td class="gridtabletd">
                                                                    <asp:LinkButton ID="lnkbtnPaymentStatus" runat="server" Font-Underline="false" Font-Bold="true"
                                                                        Text="Payment Status" Font-Names="Arial" ForeColor="White"
                                                                        CommandArgument="Reg.Mr_status" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                                        <asp:Label ID="lblMRStatus" runat="server" Text="<%#Bind('Mr_status')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                            <tr>
                                                                <td class="gridtabletd">
                                                                    <asp:LinkButton ID="lnkbtnlMRStatus" runat="server" Font-Underline="false" Font-Bold="true"
                                                                        Text="MR Status" Font-Names="Arial" ForeColor="White"
                                                                        CommandArgument="Reg.Mr_status" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                            </div>
                        </div>
                    </asp:Panel>

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
