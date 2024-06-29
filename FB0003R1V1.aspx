<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FB0003R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FB0003R1V1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Bootstrap 5.2.0 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css?v=1.0" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Customize Bootstrap -->
    <link rel="stylesheet" href="CSS/bootstrap.css" type="text/css" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Ionicons -->
    <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css" />

    <!-- gridviewstyle -->
    <link rel="stylesheet" href="CSS/gridviewstyle.css" />

    <style>
        @media only screen and (max-width: 479px) {
            .btns-close {
                margin-bottom: 10px;
                margin-left: 34%;
            }

            .btns-supervisor {
                margin-left: 15%;
            }
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpnlUser" runat="server">
        <ContentTemplate>
             
            <div class="card">
                <div class="box box-solid" style="border-radius: 30px;">
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
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Date</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control ReadOnly" autocomplete="off" Enabled="false"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtdateform_CalendarExtender" TargetControlID="txtDate" runat="server"
                                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Close By</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtCloseBy" runat="server" CssClass="form-control ReadOnly" autocomplete="off" Enabled="false"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label"></label>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <asp:LinkButton ID="btncloseall" OnClick="btncloseall_Click" runat="server" CssClass="btn btnred" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;"> Close All</asp:LinkButton>
                                                            <asp:LinkButton ID="btnSupervisorCounterReport" OnClick="btnSupervisorCounterReport_Click" runat="server" CssClass="btn btns-supervisor btngreen" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;"> Supervisor Counter Report</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>


                <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                    <div class="box-header with-border TotalRecord">
                        <div class="box-title">
                            <label class="text-right" style="color: white">Total Record : <span id="lblTotalRecords" runat="server"></span></label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="form">
                            <asp:HiddenField ID="hdnVisiblity" runat="server" />
                            <div class="table-responsive table--no-card m-b-30" id="divid">
                                <asp:GridView ID="gvPymt" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                    AutoGenerateColumns="False"
                                    CellPadding="2"
                                    ForeColor="#333333"
                                    HorizontalAlign="Center"
                                    PageSize="50"
                                    CssClass="table table-borderless table-striped">

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayTyp_ID" runat="server" Text="<%#Bind('PayTyp_ID')%>"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:Label ID="lblPayTyp_ID01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Payment Mode" Font-Names="Arial" ForeColor="White"></asp:Label>

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

                                                <asp:Label ID="lblAmount_Available" runat="server" Text="<%#Bind('AMOUNT_AVAILABLE')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:Label ID="lblPayTyp_ID01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Available Amount S$" Font-Names="Arial" ForeColor="White"></asp:Label>

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
                                                <asp:Label ID="lblActual_Amount" runat="server" Text="<%#Bind('ACTUAL_AMOUNT')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:Label ID="lblPayTyp_ID01" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Actual Amount" Font-Names="Arial" ForeColor="White"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Order Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRefNo" runat="server" Visible="False" Text="<%# Bind('Ref_No') %>" Style="text-align: right; width: 90px" CssClass="form-control" AutoPostBack="true" onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="fteQty" runat="server" TargetControlID="txtRefNo" FilterType="Numbers, Custom" ValidChars="." />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                        <tr>
                                            <td style="background-color: #00b7d4">
                                                <asp:Label ID="lblPayTyp_ID01" runat="server" Font-Underline="false" Font-Bold="true"
                                                    Text="Reference No." Font-Names="Arial"  ForeColor="White"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="right" Width="90px" />
                            </asp:TemplateField>--%>
                                    </Columns>
                                    <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                    <RowStyle CssClass="GridviewRowStyle" />
                                    <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                   
                    <div class="box-body" style="margin-top: -20px;">
                        <div class="form">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <div class="table-responsive table--no-card m-b-30" id="divid1">
                                <asp:GridView ID="gvList" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                    AutoGenerateColumns="False"
                                    CellPadding="2"
                                    ForeColor="#333333"
                                    HorizontalAlign="Center"
                                    CssClass="table table-borderless table-striped">

                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPymtCounter_ID" runat="server" Text="<%#Bind('PymtCounter_ID')%>"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:Label ID="lblPymtCounter_ID" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Payment Counter" Font-Names="Arial" ForeColor="White"></asp:Label>

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

                                                <asp:Label ID="lblTrans_Status" runat="server" Text="<%#Bind('Trans_Status')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                    <tr>
                                                        <td class="gridtabletd">
                                                            <asp:Label ID="lblTrans_Status" runat="server" Font-Underline="false" Font-Bold="true"
                                                                Text="Status" Font-Names="Arial" ForeColor="White"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
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
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="lnkbtnsupcounter" runat="server" />
                        <cc1:ModalPopupExtender ID="modelpopupsupervisercounter" runat="server" BackgroundCssClass="modal-background"
                            DynamicControlID="lnkbtnsupcounter" PopupControlID="Panelsupcounter" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                            TargetControlID="lnkbtnsupcounter" />
                        <asp:Panel ID="Panelsupcounter" runat="server" CssClass="modalPopup" align="center" EnableTheming="True" Style="text-align: center" Width="400px">

                            <table width="98%">
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: red; padding-left: 30px">
                                        <asp:Label ID="lblsupcounter" runat="server" Visible="true" Text="Please click 'Yes' to confirm close or 'No' to cancel"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="line-height: 11px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 50px">
                                        <asp:Button ID="btnyes" runat="server" CssClass="btn btngreen" Visible="true" OnClick="BtnCloseConfirm_Click"
                                            Text="Yes" Width="100px" />
                                        &nbsp;<asp:Button ID="btnno" runat="server" CssClass="btn btnred" Visible="true" OnClick="btnno_Click"
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

            <br />
              <asp:Panel runat="server" ID="pnlcopyrequest" Visible="false">
                <asp:LinkButton ID="lnkbtnpopup" runat="server" />
                <cc1:ModalPopupExtender ID="mdlpopupsupervisor" runat="server" BackgroundCssClass="modal-background"
                    DynamicControlID="lnkbtnpopup" PopupControlID="pnlPdtcopyrequest" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                    TargetControlID="lnkbtnpopup" />
                <div class="table-responsive table--no-card m-b-30">
                    <asp:Panel ID="pnlPdtcopyrequest" runat="server" Visible="true">
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
                                                        <li class="" style="background-color:#e3f6fd; border-radius: 5px;"><a href="#tabPrd_1" data-toggle="tab"
                                                            style="font-weight: 500; font-size: 15px; color: black">
                                                            <asp:Label runat="server" ID="lbl2ndLvlTabTiltecopyrequest" Text="Supervisor Counter Report"></asp:Label>
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
                                        <div class="box-body">
                                        <div class="form-group" style="display: flex; justify-content: center; align-items: center;position: relative; left: 50px;">
                                            <table width="45%">
                                                <tr>
                                                    <td> <asp:RadioButton ID="rbsummary" runat="server" Checked="true" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" /></td>
                                                    <td>
                                                        <label for="inputEmail3" class="col-sm-1 control-label"> Summary </label>
                                                    </td>
                                                    <td>
                                                         <asp:RadioButton ID="rbDetail" runat="server" CssClass="form-control" BorderStyle="none" GroupName="ReportSelection" Width="38" />
                                                    </td>
                                                    <td>
                                                         <label for="inputEmail3" class="col-sm-1 control-label"> Detail </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                           <br />
                                       <div class="form-group">
                                        <div class="col-sm-2">
                                        </div>
                                        <label for="inputEmail3" class="col-sm-2 control-label"> User Name   </label>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                               <asp:DropDownList ID="ddlactionby" runat="server" CssClass="form-control" AutoPostBack="false">
                                               </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                            <div class="form-group">
                                        <div class="col-sm-2">
                                        </div>
                                        <label for="inputEmail3" class="col-sm-2 control-label"> Close Date   </label>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                               <asp:TextBox ID="txtcloseDate" runat="server" CssClass="form-control"  autocomplete="off"></asp:TextBox>
                                               <cc1:CalendarExtender ID="CalendarExtender12" TargetControlID="txtcloseDate" runat="server"
                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                      <div style="text-align:center">
                                          <asp:Button ID="btnexport" OnClick="btnexport_Click" CssClass="btn btngreen" Width="170px" runat="server" Text="Export" />
                                    </div>
                                     <br />
                                    <div class="modal-footer" style="text-align: center; display: none">
                                        <asp:Button runat="server" ID="btnCancelcopyreq" Text="Cancel" CssClass="btn btnred" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>

            </asp:Panel>
            <br />

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
</asp:Content>

