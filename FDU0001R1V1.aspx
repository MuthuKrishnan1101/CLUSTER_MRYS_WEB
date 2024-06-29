<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FDU0001R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FDU0001R1V1" %>

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

    <!-- javascript -->
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <style>
       /* .lblusername {
       margin-top: -5px!important
}*/
    </style>
    
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnl6" runat="server">
        <ContentTemplate>
           
            <asp:HiddenField ID="hdnClickEvent" runat="server" /> 
            <asp:HiddenField ID="hfGroupID" runat="server" />

             <div class="ToolBarcard">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">

                    <asp:ImageButton ID="imgbtnNew" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                        ToolTip="Add New" OnClick="imgbtnNew_Click" />

                 <asp:ImageButton ID="imgbtnClear" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                     ToolTip="Show all records" OnClick="imgbtnClear_Click" />

                  <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                      ToolTip="Save" OnClick="imgbtnSave_Click" />

                 <asp:ImageButton ID="imgbtnDelete" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                     ToolTip="Delete" OnClick="imgbtnDelete_Click" />

                 <asp:ImageButton ID="imgbtnprint" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                     ToolTip="Export" />
                  
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
                              <asp:ImageButton ID="ImageButton1" runat="server" />&nbsp;&nbsp; </span>
                      </ContentTemplate>
                  </asp:UpdatePanel>

                </div>
            </div>
</div>
            
                 <div class="card">
            <div class="box box-solid Box" style="padding: 10px;">
                <div class="box-body">
                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <span class="accordion-toggle" data-toggle="collapse" data-parent="toggle" href="#collapseOne">
                                <i class="fa fa-minus"></i>&nbsp;General Details
                            </span>
                        </div>
                        <div id="collapseOne" class="accordion-body collapse in">
                            <div class="accordion-inner">
                                <asp:Panel ID="pnlGeneralDetails" Width="90%" runat="server" Style="margin-left: 20px;">
                                    <div class="form-horizontal">
                                        <div class="box-body">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Group Name </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtGroupName" runat="server" ToolTip="ID" CssClass="bootformcontrol InputUppercase" ReadOnly="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">HR Sequence </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHRSequence" runat="server" ToolTip="Cancellation Reasons" CssClass="bootformcontrol" ReadOnly="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>


                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <span class="accordion-toggle" data-toggle="collapse" data-parent="toggle" href="#collapseTwo">
                                <i class="fa fa-minus"></i>&nbsp;Team Member Detail
                            </span>
                        </div>
                        <div id="collapseTwo" class="accordion-body collapse in">
                            <div class="accordion-inner">
                                <br />
                                <asp:Panel ID="Panel1" Width="90%" runat="server" Style="margin-left: 20px;">
                                    <div class="form-horizontal">
                                        <div class="box-body">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Name </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtuserNAme" runat="server" ToolTip="ID" CssClass="bootformcontrol InputUppercase" ReadOnly="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">User ID </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtuserID" runat="server" ToolTip="Cancellation Reasons" CssClass="bootformcontrol" ReadOnly="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-0"></div>
                                                <asp:Button Text="search" CssClass="btn btn-info" runat="server" ID="btnsearch" OnClick="btnsearch_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <table width="100%">
                                        <tr>
                                            <td> 
                                                <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                                                    
                                                    <div class="box-header with-border TotalRecord">
                                                        <div class="box-title">
                                                            <label class="text-right" style="color: white">Total Records : <span id="lblTotalRecordsLeft" runat="server"></span></label>
                                                        </div>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="form">
                                                            <div class="table-responsive table--no-card m-b-30">
                                                                <asp:GridView ID="gvAvailableStaffs" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                    AutoGenerateColumns="False"
                                                                    CellPadding="2"
                                                                    ForeColor="#333333"
                                                                    HorizontalAlign="Center" OnRowDataBound="gvAvailableStaffs_RowDataBound"
                                                                    PageSize="10"
                                                                    CssClass="table table-borderless table-striped">
                                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                    <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelectLeft" runat="server" />
                                                                                <asp:HiddenField ID="hfIDsLeft" runat="server" />
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkSelectAllLeft" runat="server" OnCheckedChanged="chkSelectAllLeft_CheckedChanged"
                                                                                    AutoPostBack="True" />
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="text-left" Width="70px" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbluser_idleft" runat="server" Text="<%#Bind('user_id')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                             <tr>
                                                                                                                 <td class="gridtabletd"" cssclass="GridHeaderText" width="50px">
                                                                                            <asp:LinkButton ID="lnkuser_id" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                Text="User ID" Font-Names="Arial"  ForeColor="White"
                                                                                                CommandArgument="user_id" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUser_nameleft" runat="server" Text="<%#Bind('User_name')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                 <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                             <tr>
                                                                                                                 <td class="gridtabletd"" cssclass="GridHeaderText" width="50px">
                                                                                            <asp:LinkButton ID="lnkMR_ID" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                Text="User Name" Font-Names="Arial"  ForeColor="White"
                                                                                                CommandArgument="User_name" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer" style="text-align: center;">
                                                    <asp:Repeater ID="Repeater2" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPage_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                                       
                                                </div> 
                                                
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                            <td>
                                                <asp:Button CssClass="btn btngreen" Text=">>" ID="btnAdd" OnClick="btnAdd_Click" runat="server" />
                                            </td>
                                            <td>
                                                <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                                                    <div class="box-header with-border TotalRecord">
                                                        <div class="box-title">
                                                            <label class="text-right" style="color: white">Total Records : <span id="lblTotalRecordsright" runat="server"></span></label>
                                                        </div>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="form">
                                                            <div class="table-responsive table--no-card m-b-30" id="divid">
                                                                <asp:GridView ID="gvAddedStaffs" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                    AutoGenerateColumns="False"
                                                                    CellPadding="2"
                                                                    ForeColor="#333333"
                                                                    HorizontalAlign="Center" OnRowDataBound="gvAddedStaffs_RowDataBound"
                                                                    PageSize="10"
                                                                    CssClass="table table-borderless table-striped">
                                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                    <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                                    <Columns>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelectRight" runat="server" />
                                                                                <asp:HiddenField ID="hfIDsRight" runat="server" />
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkSelectAllRight" runat="server" OnCheckedChanged="chkSelectAllRight_CheckedChanged"
                                                                                    AutoPostBack="True" />
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="text-left" Width="70px" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbluser_idRight" runat="server" Text="<%#Bind('user_id')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                               <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                             <tr>
                                                                                                                 <td class="gridtabletd"" cssclass="GridHeaderText" width="50px">
                                                                                            <asp:LinkButton ID="lnkuser_idRight" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                Text="User ID" Font-Names="Arial"  ForeColor="White"
                                                                                                CommandArgument="user_id" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUser_nameRight" runat="server" Text="<%#Bind('User_name')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                             <tr>
                                                                                                                 <td class="gridtabletd"" cssclass="GridHeaderText" width="50px">
                                                                                            <asp:LinkButton ID="lnkMR_IDRight" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                Text="User Name" Font-Names="Arial"  ForeColor="White"
                                                                                                CommandArgument="User_name" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                        </div>
                                                    </div>

                                                   <%-- <div class="modal-footer" style="text-align: center;">
                                                        <asp:Repeater ID="RepeaterRight" runat="server">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkPageRight" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                    Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPageRight_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>--%>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button CssClass="btn btngreen" ID="btnRemove" OnClick="btnRemove_Click" Text="<<" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td align="right">
                                                <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                                    <asp:Button Text="Save Team" CssClass="btn btn-primary" runat="server" />
                                                    <asp:Button Text="Export" CssClass="btn btngreen" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </asp:Panel>



                            </div>
                        </div>
                    </div>

                    </ContentTemplate>
                        </asp:UpdatePanel>
                </div>
            </div>
                </div>

            <div>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                </table>
            </div>


            <script>
                // accordion pluse minus goes here    	  
                jQuery('.accordion-toggle').click(function () {

                    var has = jQuery(this);
                    if (has.hasClass('collapsed')) {
                        jQuery(this).find('i').removeClass('fa-plus');
                        jQuery(this).find('i').addClass('fa-minus');
                    }
                    else {
                        jQuery(this).find('i').removeClass('fa-minus');
                        jQuery(this).find('i').addClass('fa-plus');
                    }
                })

            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
