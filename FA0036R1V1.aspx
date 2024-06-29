<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FA0036R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FA0036R1V1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
    <!-- Bootstrap 5.2.0 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css?v=1.0" />

    <!-- Customize Bootstrap -->
    <link rel="stylesheet" href="CSS/bootstrap.css" type="text/css" />

    <!-- Popup -->
    <link href="CSS/design.css" rel="stylesheet" type="text/css" />

   <!-- gridviewstyle -->
     <link rel="stylesheet" href="CSS/gridviewstyle.css" />

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
                 <asp:ImageButton ID="imgbtnNew" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                     ToolTip="Add New" OnClick="imgbtnNew_Click" />
                 <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                      ToolTip="Show all records" OnClick="imgbtnSearch_Click"  /> 
                  <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                        ToolTip="Save" OnClick="imgbtnSave_Click"/>
                  
                 <%--<asp:ImageButton ID="imgbtnDelete" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                     ToolTip="Allow user to delete"  OnClick="imgbtnDelete_Click"/>
                 <asp:ImageButton ID="imgbtnprint" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                       ToolTip="Allow user to print the Master in Excel" OnClick="imgbtnprint_Click" />--%>

                  <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                      ToolTip="Audit Log" OnClicK="imgbtnAudit_Click" />
                  <%--<asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                      ToolTip="Information"   />--%>
             </div>
</div>
            </div>
            <div class="">
             <div class="box box-solid" style="border-radius: 20px">
              <div class="container-fluid">
                  <table style="padding: 1px; margin: 1px; width: 100%; text-align: left; table-layout: auto; border-collapse: collapse;">
                     <tr>
                         <td>
                             <asp:Panel ID="pnlGeneralDetails" Width="90%" runat="server" style="margin-left: 20px;">
                                 <div class="form-horizontal">
                                      <div class="box-body">
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-2 control-label"> Specialty ID  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                   <asp:TextBox ID="txtspecialtyid" style="text-transform: uppercase;" runat="server" ToolTip="Specialty ID" CssClass="form-control" ReadOnly="false" ></asp:TextBox>
                                                        
                                                     
                                                 </div>
                                             </div>
                                             <%--<div class="col-sm-1">
                                             </div>--%>
                                             
                                         </div>
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-2 control-label">Specialty Description  </label>
                                             <div class="col-sm-8">
                                                 <div class="form-group">
                                                    
                                                      <asp:TextBox ID="txtspecdesc" runat="server" ToolTip="Specialty Description" CssClass="form-control" ReadOnly="false" TextMode="MultiLine"  style="resize: none;height:70px;border-radius:15px;text-transform:uppercase;"  ></asp:TextBox>
                                                 </div>
                                             </div>
                                            <%-- <div class="col-sm-1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label">Gender</label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:DropDownList ID="ddlGender" runat="server" CssClass="bootformcontrol optional ">
                                                         <asp:ListItem></asp:ListItem>
                                                         <asp:ListItem>Male</asp:ListItem>
                                                         <asp:ListItem>Female</asp:ListItem>
                                                         <asp:ListItem>Transgender</asp:ListItem>
                                                     </asp:DropDownList>
                                                 </div>
                                             </div>--%>
                                         </div>
                                           <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-2 control-label">Order ID  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtorderid" runat="server" ToolTip="Order ID" CssClass="bootformcontrol optional" ReadOnly="false" style="text-transform: uppercase;" onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                 </div>
                                             </div>
                                                  </div>
                                              <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-2 control-label"> Delete Mark  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                   <asp:CheckBox ID="chkDelMark" runat="server" ToolTip="Delete Mark" CssClass=" bootformcontrol" Width="40px" />    
                                                     
                                                 </div>
                                             </div>
                                                  </div>
                                           <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-2 control-label">Institution </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                    <asp:DropDownList ID="ddlinstitution" runat="server" ToolTip="Institution ID" AutoPostBack="true" CssClass="ReadOnly bootformcontrol">
                                                     </asp:DropDownList>
                                                 </div>
                                             </div>
                                                  </div>
                                         </div>

                                         <%--<div class="form-group">
                                             <div class="bootcolsm1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label">Content to be shown in Report</label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtContent" runat="server" ToolTip="Remark" CssClass="form-control" ReadOnly="false" TextMode="MultiLine"  style="resize: none"  ></asp:TextBox>
                                                 </div>
                                             </div>--%>
                                             <%--<label for="inputEmail3" class="col-sm-3 control-label">Content to be shown in Report</label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtContent" runat="server" ToolTip="Remark" CssClass="form-control" ReadOnly="false" TextMode="MultiLine"  style="resize: none"  ></asp:TextBox>
                                                 </div>
                                             </div>--%>
                                              
                                            
                                        
                                     </div>
                                     
                                 </asp:Panel>
                             </td>
                         </tr>
                             </table>
                  </div>
            </div>
         </div>

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
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
