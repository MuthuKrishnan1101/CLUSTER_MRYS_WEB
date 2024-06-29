<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FC0003R1V2.aspx.cs" Inherits="CLUSTER_MRTS.WebForm1" %>
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
     <style>        .lblusername {       margin-top: -5px!important}    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel ID="upnl6" runat="server">
        <ContentTemplate>
         
            <div class="ToolBarcard">
        <div class="row">
             <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">
                 <asp:ImageButton ID="imgbtnNew" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                     ToolTip="Add New" OnClick="imgbtnNew_Click" /> 
                 <asp:ImageButton ID="imgbtnClear" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                      ToolTip="Show all records" OnClick="imgbtnClear_Click" />
                  <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                        ToolTip="Save" OnClick="imgbtnSave_Click"/>
                  
                 <asp:ImageButton ID="imgbtnDelete" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                     ToolTip="Delete"  OnClick="imgbtnDelete_Click"/>
                 <asp:ImageButton ID="imgbtnprint" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                       ToolTip="Export" /> 
                  <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                      ToolTip="Audit Log" OnClick="imgbtnAudit_Click" />
                  <%--<asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                      ToolTip="Information"   />--%>
                 
                 
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="imgbtnAudit" />
                    </Triggers>
                    <ContentTemplate>
                     <span hidden>  <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" />&nbsp;&nbsp;    </span>              
                    </ContentTemplate>
                </asp:UpdatePanel>
                
             </div>
            </div>
       </div>
            <div class="card">
                <div class="box box-solid" style="border-radius: 20px">
                 <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-top: 5px">
                                                <asp:LinkButton ID="LkBtnBack" runat="server" CssClass="LnkbtnPatient" Text="Back to MR Payment" OnClick="LkBtnBack_Click"></asp:LinkButton>
                                            </div>
                 <div class="container-fluid">
                 <table style="padding: 1px; margin: 1px; width: 100%; text-align: left; table-layout: auto; border-collapse: collapse;">
                     <tr>
                         <td>
                             <asp:Panel ID="pnlNormalUpdate" Width="90%" Visible="false" runat="server" Style="margin-left: 20px;">
                                 <div class="form-horizontal">
                                     <div class="box-body">
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label"> MR No </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtB1RequestNo" runat="server" ReadOnly="true" CssClass="bootformcontrol ReadOnly" ToolTip="MR No"></asp:TextBox>
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label"> MR Status </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtB1MRStatus" runat="server" CssClass="bootformcontrol ReadOnly" ReadOnly="true" ToolTip="Name"></asp:TextBox>
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label"> Receipt No </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                      <asp:TextBox ID="txtB1ReceiptNo" runat="server" CssClass="bootformcontrol ReadOnly" ReadOnly="true"></asp:TextBox>
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>

                                              <div class="col-sm-3">
                                                   <div class="form-group">
                                                      <asp:Button ID="btnB1GetReceiptInfo" CssClass="btn Submit" Text="Search" runat="server" OnClick="BtnGetReceipt_Click" /> 
                                                 </div>
                                             </div>
                                            
                                         </div>
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label"> old Payee Name </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     
                                                     <asp:TextBox ID="txtB1OldPayName" runat="server" ReadOnly="true" CssClass="bootformcontrol ReadOnly "></asp:TextBox>
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                             
                                         </div>
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label">  Payment Type </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                      <asp:DropDownList ID="ddlB1PayType" runat="server" ReadOnly="true" CssClass="bootformcontrol ReadOnly" ToolTip="Institution ID"  OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" >
                                                         <asp:ListItem></asp:ListItem>
                                                         <asp:ListItem>YES</asp:ListItem>
                                                         <asp:ListItem>NO</asp:ListItem>
                                                         <asp:ListItem>Others</asp:ListItem>
                                                     </asp:DropDownList>
                                                       
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                         </div>
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label"> Old Bank Name  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                      <asp:DropDownList ID="ddlB1OldBank" runat="server" Enabled="false" CssClass="bootformcontrol ReadOnly" ToolTip="Institution ID">
                                                         <asp:ListItem></asp:ListItem>
                                                         <asp:ListItem>YES</asp:ListItem>
                                                         <asp:ListItem>NO</asp:ListItem>
                                                         <asp:ListItem>Others</asp:ListItem>
                                                     </asp:DropDownList>
                                                       
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label"> Old Cheque No </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtB1OldCardNo" runat="server" ReadOnly="true" CssClass="bootformcontrol ReadOnly "  onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="form-group">
                                             
                                             <label for="inputEmail3" class="col-sm-3 control-label"> New Payee Name   <i class="fa fa-star" aria-hidden="true" style="font-size: 10px; color: red;"></i> </label>
                                            
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                      <asp:TextBox ID="txtB1NewPayName" runat="server" CssClass="bootformcontrol ReadOnly "></asp:TextBox>
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                         </div>
                                         <div class="form-group">
                                              <label for="inputEmail3" class="col-sm-3 control-label"> New Bank Name  <i class="fa fa-star" aria-hidden="true" style="font-size: 10px; color: red;"></i> </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                      <asp:DropDownList ID="ddlB1NewBank" runat="server" CssClass="bootformcontrol optional" ToolTip="Institution ID">
                                                         <asp:ListItem></asp:ListItem>
                                                         <asp:ListItem>YES</asp:ListItem>
                                                         <asp:ListItem>NO</asp:ListItem>
                                                         <asp:ListItem>Others</asp:ListItem>
                                                     </asp:DropDownList>
                                                       
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label"> New  Cheque No  <i class="fa fa-star" aria-hidden="true" style="font-size: 10px; color: red;"></i> </label><div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtB1NewCardNo" runat="server" CssClass="bootformcontrol optional "  onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                 </div>
                                             </div>

                                         </div>
                                 </div>
                                    
                                     <br />
                                     <label style="color: black; font-weight: bold; font-size: 14px"> (Fields Marked as <i class="fa fa-star" aria-hidden="true" style="font-size: 10px; color: red;"></i> are Mandatory and editable before supervisor's closing)</label>
                                     <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-top: 5px">
                                         <asp:Button ID="btnB1UpdateReceiptInfo" CssClass="btn btngreen" Text="Update" runat="server" OnClick="BtnUpdateReceiptInfo_Click" /> 
                                         <br />
                                         <br />
                                            </div>
                                     
                                     </div>
                             </asp:Panel>


                             <asp:Panel ID="pnlSupervisorUpdate"  Visible="false" Width="90%" runat="server" Style="margin-left: 20px;">
                                 <div class="form-horizontal">
                                     <div class="box-body">
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label"> MR No  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtB2RequestNo" runat="server" CssClass="bootformcontrol ReadOnly" ReadOnly="true" ToolTip="MR No"></asp:TextBox>
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label"> MR Status  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtB2MRStatus" runat="server" CssClass="bootformcontrol ReadOnly" ReadOnly="true" ToolTip="MR Status "></asp:TextBox>
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label"> Receipt No  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                    
                                                      <asp:TextBox ID="txtB2ReceiptNo" runat="server" CssClass="bootformcontrol ReadOnly " ReadOnly="true"></asp:TextBox>
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                              <label for="inputEmail3" class="col-sm-2 control-label"></label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtB2MRPymtID" runat="server" Visible="false" CssClass="bootformcontrol ReadOnly" ToolTip="Name"></asp:TextBox>
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label"> Payee Name  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     
                                                     <asp:TextBox ID="txtB2PayName" runat="server" ReadOnly="true" CssClass="bootformcontrol ReadOnly "></asp:TextBox>
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                             
                                         </div>
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label"> Old Payment Type  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                      <asp:DropDownList ID="ddlB2PayTypeOld" runat="server" Enabled="false" CssClass="bootformcontrol ReadOnly" ToolTip="Institution ID">
                                                         <asp:ListItem></asp:ListItem>
                                                         <asp:ListItem>YES</asp:ListItem>
                                                         <asp:ListItem>NO</asp:ListItem>
                                                         <asp:ListItem>Others</asp:ListItem>
                                                     </asp:DropDownList>
                                                       
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                         </div>
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label"> Old Bank Name   </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                      <asp:DropDownList ID="ddlB2OldBank" runat="server" Enabled="false"  CssClass="bootformcontrol ReadOnly" ToolTip="Institution ID">
                                                         <asp:ListItem></asp:ListItem>
                                                         <asp:ListItem>YES</asp:ListItem>
                                                         <asp:ListItem>NO</asp:ListItem>
                                                         <asp:ListItem>Others</asp:ListItem>
                                                     </asp:DropDownList>
                                                       
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label"> Old Cheque No  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtB2OldCardNo" runat="server" ReadOnly="true" CssClass="bootformcontrol ReadOnly "  onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="form-group">
                                             
                                             <label for="inputEmail3" class="col-sm-3 control-label"> New Payee Name   <i class="fa fa-star" aria-hidden="true" style="font-size: 10px; color: red;"></i> </label>
                                            
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                      <asp:TextBox ID="txtB2NewPayName" runat="server" CssClass="bootformcontrol ReadOnly "></asp:TextBox>
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                         </div>
                                         <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-3 control-label"> New Payment Type  <i class="fa fa-star" aria-hidden="true" style="font-size: 10px; color: red;"></i> </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                      <asp:DropDownList ID="ddlB2NewPayType" runat="server" CssClass="bootformcontrol ReadOnly" ToolTip="Institution ID">
                                                         <asp:ListItem></asp:ListItem>
                                                         <asp:ListItem>YES</asp:ListItem>
                                                         <asp:ListItem>NO</asp:ListItem>
                                                         <asp:ListItem>Others</asp:ListItem>
                                                     </asp:DropDownList>
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                            
                                         </div>
                                         <div class="form-group">
                                              <label for="inputEmail3" class="col-sm-3 control-label"> New Bank Name  <i class="fa fa-star" aria-hidden="true" style="font-size: 10px; color: red;"></i> </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                      <asp:DropDownList ID="ddlB2NewBank" runat="server" CssClass="bootformcontrol optional" ToolTip="Institution ID">
                                                         <asp:ListItem></asp:ListItem>
                                                         <asp:ListItem>YES</asp:ListItem>
                                                         <asp:ListItem>NO</asp:ListItem>
                                                         <asp:ListItem>Others</asp:ListItem>
                                                     </asp:DropDownList>
                                                       
                                                 </div>
                                             </div>
                                             <div class="col-sm-1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label"> New  Cheque No  <i class="fa fa-star" aria-hidden="true" style="font-size: 10px; color: red;"></i> </label><div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtB2NewCardNo" runat="server" CssClass="bootformcontrol optional "  onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                 </div>
                                             </div>

                                         </div>
                                 </div>
                                    
                                     <br />
                                     <label style="color: black; font-weight: bold; font-size: 14px"> (Fields Marked as <i class="fa fa-star" aria-hidden="true" style="font-size: 10px; color: red;"></i> are Mandatory and editable before supervisor's closing)</label>
                                     <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-top: 5px">
                                         <asp:Button ID="btnUpdateReceiptInfo2" CssClass="btn btngreen" Text="Update" runat="server" OnClick="BtnUpdatePaymentType_Click" />
                                         <asp:Button ID="btnB1SoftDelete" CssClass="btn btnred" Text="Delete" runat="server" OnClick="BtnSoftDelete_Click" Visible="false" />
                                             <cc1:ConfirmButtonExtender ID="btnB1SoftDelete_ConfirmButtonExtender" 
                                    runat="server" ConfirmText="Are you sure you want to soft delete this receipt?" 
                                    TargetControlID="btnB1SoftDelete">
                                </cc1:ConfirmButtonExtender>
                                         <br />
                                         <br />
                                            </div>
                                     
                                     </div>
                             </asp:Panel>

                             
                             
                         </td>
                     </tr>
                 </table>
             </div>
                 </div>
                </div>

            <asp:Panel ID="pnlExternalSystemDetails" Width="90%" runat="server" Style="margin-left: 20px;" Visible="false" Enabled="false">
                                 <div class="form-horizontal">
                                     <div class="box-body">
                                        
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
            </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>
