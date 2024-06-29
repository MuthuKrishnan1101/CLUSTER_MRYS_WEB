<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FA0035R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FA0035R1V1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <!-- Bootstrap 5.2.0 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css?v=1.0" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Customize Bootstrap -->
    <link rel="stylesheet" href="CSS/bootstrap.css" type="text/css" />
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
                      ToolTip="Show all records" OnClick="imgbtnSearch_Click" />
                  <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                        ToolTip="Save" OnClick="imgbtnSave_Click"/>
                  
                 <%--<asp:ImageButton ID="imgbtnDelete" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                     ToolTip="Allow user to delete"  OnClick="imgbtnDelete_Click"/>
                 <asp:ImageButton ID="imgbtnprint" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                       ToolTip="Allow user to print the Master in Excel" />--%>

                  <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                      ToolTip="Audit Log" OnClick="imgbtnAudit_Click" />
                 <%-- <asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                      ToolTip="Information"   />--%>
                 
                 
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="imgbtnAudit" />
                    </Triggers>
                    <ContentTemplate>
                     <span hidden>  <asp:ImageButton ID="ImageButton1" runat="server" OnClick="imgbtnAudit_Click" />&nbsp;&nbsp;    </span>              
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
                                         <div class="form-group">
                                             <div class="bootcolsm1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label">ID  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtID" runat="server" ToolTip="ID" CssClass="bootformcontrol InputUppercase" ReadOnly="false" style="text-transform: uppercase;"></asp:TextBox>
                                                 </div>
                                             </div>                                              
                                         </div>

                                         <div class="form-group">
                                             <div class="bootcolsm1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label">Description </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtSName" runat="server" ToolTip="Description" CssClass="bootformcontrol" ReadOnly="false" style="text-transform: uppercase;"></asp:TextBox>
                                                 </div>
                                             </div>
                                         </div>

                                         <div class="form-group">
                                             <div class="bootcolsm1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label" >Enable Department User  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:CheckBox ID="chkdeptaccess" runat="server" ToolTip="Enable Department user" CssClass="optional bootformcontrol" Width="40px"/>
                                                 </div>
                                             </div>
                                         </div>
                                          <div class="form-group">
                                             <label for="inputEmail3" class="col-sm-2 control-label">Order ID </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtorderid" runat="server" ToolTip="Order ID" CssClass="bootformcontrol optional" ReadOnly="false" style="text-transform: uppercase;" onkeypress="return isNumber(event)" OnPaste="JavaScript:return RestrictCopyPaste();"></asp:TextBox>
                                                 </div>
                                             </div>
                                                  </div>
                                         <div class="form-group">
                                             <div class="bootcolsm1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label">Remarks  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     
                                                     <asp:TextBox ID="txtRemarks" runat="server" ToolTip="Remark" CssClass="optional bootformcontrol Optional MultiLine_Textbox" ReadOnly="false" TextMode="MultiLine" style="text-transform: uppercase; height:100px;" ></asp:TextBox>
                                                      
                                                 </div>
                                             </div>
                                         </div>
                                          

                                         <div class="form-group">
                                             <div class="bootcolsm1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label" >Delete Mark  </label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:CheckBox ID="chkDelMark" runat="server" ToolTip="Delete Mark" CssClass="optional bootformcontrol" Width="40px"/>
                                                 </div>
                                             </div>
                                         </div>
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

        
    
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>