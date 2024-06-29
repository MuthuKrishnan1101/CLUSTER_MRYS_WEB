<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FA0034R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FA0034R1V1" %>

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


            <div class="ToolBarcard">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">
                        <asp:ImageButton ID="imgbtnNew" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                            ToolTip="Add New" OnClick="imgbtnNew_Click" />
                        <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                            ToolTip="Show all records" OnClick="imgbtnSearch_Click" />
                  <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                      ToolTip="Save" OnClick="imgbtnSave_Click" />

                        <%--<asp:ImageButton ID="imgbtnDelete" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                            ToolTip="Allow user to delete" OnClick="imgbtnDelete_Click" />
                        <asp:ImageButton ID="imgbtnprint" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                            ToolTip="Allow user to print the Master in Excel" />--%>

                        <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                            ToolTip="Audit Log" OnClick="imgbtnAudit_Click" />
                        <%--<asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
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

            <div class="">
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
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Institution ID </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtID" Style="text-transform: uppercase;" runat="server" ToolTip="Institution ID" CssClass="bootformcontrol InputUppercase" ReadOnly="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Institution Name  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSName" runat="server" ToolTip="Institution Name" CssClass="bootformcontrol" ReadOnly="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Institution Address  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtinsaddress" runat="server" ToolTip="Institution Address" TextMode="MultiLine" Style="height: 100px" CssClass="optional bootformcontrol MultiLine_Textbox" ReadOnly="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">

                                                    <label for="inputEmail3" class="col-sm-2 control-label">Telephone Number  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txttelephno" runat="server" ToolTip="Telephone Number" BackColor="White" CssClass=" bootformcontrol "></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Fax Number  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtfaxno" runat="server" MaxLength="30" ToolTip="Fax Number" CssClass="bootformcontrol optional  "></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">

                                                    <label for="inputEmail3" class="col-sm-2 control-label">Email Address  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtemail" runat="server" ToolTip="Email Address" MaxLength="2000" BackColor="White" CssClass=" bootformcontrol "></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Website  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtwebsite" runat="server" MaxLength="30" ToolTip="Website" CssClass="bootformcontrol optional  "></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Rounding Type  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <span hidden>
                                                                <asp:DropDownList ID="ddlBO" runat="server" ToolTip="Rounding Type" AutoPostBack="true" CssClass="bootformcontrol">
                                                                </asp:DropDownList></span>
                                                            <asp:DropDownList ID="ddlroundingtype" runat="server" ToolTip="Rounding Type" AutoPostBack="true" CssClass="bootformcontrol">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Round to Nearest  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtround" runat="server" ToolTip="Round To Nearest" CssClass="optional bootformcontrol" ReadOnly="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Hospital Code (SMR)</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txthoscodesmr" runat="server" ToolTip="Hospital Code (SMR)" CssClass="bootformcontrol" ReadOnly="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Hospital Code (SAP)</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txthoscodesap" runat="server" ToolTip="Hospital Code (SAP)" CssClass="bootformcontrol" ReadOnly="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Notification Send To Batch  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:CheckBox ID="chknodification" runat="server" ToolTip="Notification" CssClass="optional bootformcontrol" Width="40px" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">SMTP  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">

                                                            <asp:TextBox ID="txtRemarks" runat="server" ToolTip="SMTP" CssClass="bootformcontrol optional" ReadOnly="false"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Reg No  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">

                                                            <asp:TextBox ID="txtregno" runat="server" ToolTip="Reg No" CssClass="bootformcontrol optional" ReadOnly="false"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Inclusive GST </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:CheckBox ID="chkDelMark" runat="server" ToolTip="Inclusive GST" CssClass="optional bootformcontrol" Width="40px" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Assign Request Base on Last 2 Digit   </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:CheckBox ID="chkAssignRequest" runat="server" ToolTip="Assign Request" CssClass="optional bootformcontrol" Width="40px" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Document Generate With Logo   </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:CheckBox ID="chkdocgenwithlogo" runat="server" ToolTip="Document Generate With Logo" CssClass="optional bootformcontrol" Width="40px" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Send to SMR   </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:CheckBox ID="chcksendtosmr" runat="server" ToolTip="Document Generate With Logo" CssClass="optional bootformcontrol" Width="40px" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <%--  <div class="form-group">
                                             <div class="bootcolsm1">
                                             </div>
                                             <label for="inputEmail3" class="col-sm-2 control-label">Institution Logo Upload</label>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:FileUpload ID="FileUpload1" runat="server" EnableViewState="true" />
                                                 </div>
                                             </div>
                                             <div class="col-sm-3">
                                                 <div class="form-group">
                                                     <asp:UpdatePanel ID="UpnlUpload" runat="server">
                                                         <Triggers>
                                                             <asp:PostBackTrigger ControlID="btnphotoupload" />
                                                         </Triggers>
                                                         <ContentTemplate>
                                                             <asp:Button runat="server" Text="Upload" ID="btnphotoupload"  OnClick="btnphotoupload_Click" CssClass="btn btns-search btn-success" />
                                                         </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                 </div>
                                             </div>
                                         </div>

                                          <div class="form-group">
                                             <div class="bootcolsm1">
                                             </div>
                                              <asp:Label ID="lblinstimg" runat="server" Text="Institution Image" class="col-sm-2 control-label"></asp:Label>
                                              <div class="col-sm-3">
                                                 <div class="form-group">
                                                    <asp:Image ID="imgInstPic" runat="server" Width="16px" />
                                                 </div>
                                             </div>
                                         </div>--%>
                                            </div>
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
