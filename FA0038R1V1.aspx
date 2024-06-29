<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FA0038R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FA0038R1V1" %>
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

    <!-- Date Validation check in JS-->
    <script src="Scripts/Validation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnl6" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnClickEvent" runat="server" />
            
                <div class="ToolBarcard">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">

                        <asp:ImageButton ID="imgbtnNew" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                            ToolTip="Add New" OnClick="imgbtnNew_Click" />

                 <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                     ToolTip="Show all records" OnClick="imgbtnSearch_Click" />

                 <%-- <asp:ImageButton ID="imgbtnSave" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                      ToolTip="Save" OnClick="imgbtnSave_Click" /> 

                  

                 <asp:ImageButton ID="imgbtnDelete" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                     ToolTip="Allow user to delete" OnClick="imgbtnDelete_Click" />

                 <asp:ImageButton ID="imgbtnprint" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                     ToolTip="Allow user to print the Master in Excel" />--%>
                  
                  <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                      ToolTip="Audit Log" OnClick="imgbtnAudit_Click" />
                 
                 <%-- <asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                      ToolTip="Information" />--%>

                 
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                      <Triggers>
                          <asp:PostBackTrigger ControlID="imgbtnAudit" />
                      </Triggers>
                      <ContentTemplate>
                          <span hidden>
                              <asp:ImageButton ID="ImageButton1" runat="server" OnClick="imgbtnAudit_Click" />&nbsp;&nbsp;    </span>
                      </ContentTemplate>
                  </asp:UpdatePanel>

                    </div>
                    </div>
                    </div>
             <div class="box box-solid" style="border-radius: 20px">
                    <div class="container-fluid">
                        <table style="padding: 1px; margin: 1px; width: 100%; text-align: left; table-layout: auto; border-collapse: collapse;">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlGeneralDetails" Width="90%" runat="server" Style="margin-left: 20px;">
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                               <%-- <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Institution</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlinst" ToolTip="Institution" runat="server" Style="background-color: #ddf5ff;" CssClass="form-control ">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Institution ID  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:DropDownList ID="ddlBO" runat="server" ToolTip="Institution ID" AutoPostBack="true" CssClass="ReadOnly bootformcontrol">
                                                     </asp:DropDownList>
                                                 </div>
                                             </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">GST  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtgst" runat="server" ToolTip="GST" CssClass=" bootformcontrol" ReadOnly="false"  onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                 <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Tax Code   </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txttaxcode" runat="server" ToolTip="Group Name" CssClass="optional bootformcontrol" ReadOnly="false"  onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Fee Effective Date  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                           <asp:TextBox ID="txtrepname" runat="server" ToolTip="Group Name" CssClass="optional bootformcontrol" ReadOnly="false"  onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox> 
                                                             <cc1:CalendarExtender ID="txtunavbFrom_CalendarExtender" runat="server" Format="dd-MM-yyyy" TargetControlID="txtrepname"></cc1:CalendarExtender>                                                                                       
                                                                                                                                                    
                                                                                                                                                   
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                     <label for="inputEmail3" class="col-sm-2 control-label">Tax Allowed  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:CheckBox ID="chkDelMark" runat="server" ToolTip="Tax Allowed" CssClass="optional bootformcontrol" Width="40px"/>
                                                 </div>
                                             </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                            <asp:Button ID="btnthirdleveladd" runat="server" style="border-radius: 10px;" CssClass="btn btngreen text-right" Text="Add" OnClick="btnthirdleveladd_Click" />
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
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
           <asp:Panel runat="server" ID="pnlRemarksresultgrid" Visible="true">
            <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed; width: 99%">
                <div class="box-header with-border TotalRecord">
                    <div class="box-title">
                        <label class="text-right" style="color: white">Total Records : <span id="TotalrecPatient" runat="server"></span></label>
                    </div>
                </div>
                <div class="box-body">
                    <div class="form">
                        <asp:HiddenField ID="hdnVisiblity" runat="server" />
                        <div class="table-responsive table--no-card m-b-30" id="divid">
                            <asp:GridView ID="gvNonMRList" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                AutoGenerateColumns="False"
                                CellPadding="2"
                                ForeColor="#333333"
                                HorizontalAlign="Center"
                                PageSize="10"
                                CssClass="table table-borderless table-striped">
                                <Columns>
                                      <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnUserID" runat="server" Text="<%#Bind('GST')%>" OnClick="lnkbtnUserID_Click" CommandArgument='<%#Eval("BE_ID")+","+ Eval("GST")+","+ Eval("EFFECTIVE_DATE")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd">
                                                        <asp:LinkButton ID="lnkbtnusrid" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="GST" Font-Names="Arial"  ForeColor="White"
                                                            CommandArgument="GST" OnClick="lnkbtnusrid_Click"></asp:LinkButton>
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
                                            <asp:Label ID="lblordno" runat="server" Text="<%#Bind('INT_TAX_CODE')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd">
                                                        <asp:LinkButton ID="lnkbtnprocesssts7" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Tax Code" Font-Names="Arial"  ForeColor="White"
                                                            CommandArgument="INT_TAX_CODE"></asp:LinkButton>
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
                                           <asp:Label ID="lblordnEffectivedateo" runat="server"  Text='<%#Bind("EFFECTIVE_DATE", "{0:dd-MM-yyyy  }")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd">
                                                        <asp:LinkButton ID="lnkbtnusrid" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Fee Effective Date" Font-Names="Arial"  ForeColor="White"
                                                            CommandArgument="EFFECTIVE_DATE" OnClick="lnkbtnusrid_Click"></asp:LinkButton>
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
                                          <asp:Label ID="lblTAX_ALLOWED" runat="server" Text="<%#Bind('TAX_ALLOWED')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd">
                                                        <asp:LinkButton ID="lnkbtnTAX_ALLOWED" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Tax Allowed" Font-Names="Arial"  ForeColor="White"
                                                            CommandArgument="TAX_ALLOWED" OnClick="lnkbtnusrid_Click"></asp:LinkButton>
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
                                              <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDelete_Click" CommandArgument='<%#Eval("GST")+","+ Eval("EFFECTIVE_DATE")%>'/>
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
               </asp:Panel>
             
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
