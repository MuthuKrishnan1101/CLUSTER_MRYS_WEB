<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FD0001R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FD0001R1V1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">

    <!-- Bootstrap 5.2.0 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css?v=1.0" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Customize Bootstrap -->
    <link rel="stylesheet" href="CSS/bootstrap.css" type="text/css" />

    <!-- Popup -->
    <link href="CSS/design.css" rel="stylesheet" type="text/css" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Ionicons -->
    <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css" />

    <!-- gridviewstyle -->
    <link rel="stylesheet" href="CSS/gridviewstyle.css" />

    <!-- javascript -->
    <script src="Scripts/Validation.js" type="text/javascript"></script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnl6" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnClickEvent" runat="server" />

            <div class="ToolBarcard">
                <table width="100%">
                    <tr>
                        <td>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">
                                    <asp:ImageButton ID="imgbtnNew" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                                        ToolTip="Add New" OnClick="imgbtnNew_Click" />
                                     
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                                        ToolTip="Delete" OnClick="imgbtnDelete_Click" />
                                     
                                </div>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="card">
            <div class="box box-solid" style="border-radius: 30px;">
                <br />
                <div class="box" style="border-radius: 32px; border-style: none; border-bottom-style: none">
                    <div class="row">
                        <div class="adjustment">
                            <asp:GridView ID="gvAdvancedSearch" runat="server" OnRowDataBound="gvAdvancedSearch_RowDataBound" GridLines="Vertical" RowStyle-Wrap="true" Style="width: 65%; margin-left: 40px; border-radius: 20px;"
                                AutoGenerateColumns="false"
                                CellPadding="2"
                                ForeColor="#333333"
                                HorizontalAlign="Center"
                                CssClass="table  table-striped table-responsive">
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                <Columns>



                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfSNo" runat="server" Value="<%#Bind('SNo')%>" />
                                            <asp:DropDownList ID="ddlColumnName" runat="server" CssClass="GridviewMandatoryDropdownStyle" Style="border: 1px solid #0f6cbd"
                                                Width="95%" Height="22px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; border-collapse: collapse;">
                                                <tr class="success">
                                                    <td class="gridtabletd" width="50px">
                                                        <asp:Label ID="lblcolumnname" runat="server" Text="Column Name"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-position: bottom; background-repeat: repeat-x; background-image: url('Images/GridviewHeader.png'); line-height: 11px; vertical-align: bottom;"></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlCondition" runat="server" CssClass="GridviewMandatoryDropdownStyle" Style="border: 1px solid #0f6cbd"
                                                Width="95%" Height="22px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: none; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd" width="50px">
                                                        <asp:Label ID="lblCondition" runat="server" Text="Condition"></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-position: bottom; background-repeat: repeat-x; background-image: url('Images/GridviewHeader.png'); line-height: 11px; vertical-align: bottom;"></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSearchInput" Text="" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Tahoma" Font-Size="9pt" Width="90%" Height="22px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: none; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd" width="50px">
                                                        <asp:Label ID="lblsearchInput" runat="server" Text="Value"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-position: bottom; background-repeat: repeat-x; background-image: url('Images/GridviewHeader.png'); line-height: 11px; vertical-align: bottom;"></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="GridviewRowStyle" />
                                <HeaderStyle CssClass="GridHeaderTextforScroll-UnText"></HeaderStyle>
                                <RowStyle CssClass="GridviewRowStyle" />
                                <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                        <asp:LinkButton ID="lnkbtnAdd" runat="server" CssClass="btn  btngreen" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lnkbtnAdd_Click"><i class="fa fa-plus-circle" aria-hidden="true"></i> Add</asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnRemove" runat="server" CssClass="btn  btnyellow" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lnkbtnRemove_Click"><i class="fa fa-trash" aria-hidden="true"></i> Remove </asp:LinkButton>
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn  btn-info" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i> Search</asp:LinkButton>
                        <asp:LinkButton ID="lnkbtnClear" runat="server" CssClass="btn  btnred" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px;width: 90px" OnClick="lnkbtnClear_Click"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear </asp:LinkButton>&nbsp;&nbsp;
                    </div>
                </div>
                <br />
            </div>

            <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                <div class="box-header with-border TotalRecord">
                    <div class="box-title">
                        <label class="text-right" style="color: white">Total Record : <span id="lblTotalRecords" runat="server"></span></label>
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

                                <Columns>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectLeft" runat="server" CssClass="form-control"  OnCheckedChanged="chkSelectAllLeft_CheckedChanged"
                                                AutoPostBack="True" />
                                            <asp:HiddenField ID="hfIDsLeft" runat="server" />
                                        </ItemTemplate>
                                        <%--<HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAllLeft" runat="server"  />
                                        </HeaderTemplate>--%>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" CssClass="text-left" Width="70px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnRequestID" runat="server" Text="<%#Bind('Request_ID')%>" CommandArgument='<%#Eval("Request_ID")+","+ Eval("HRN_ID")%>'
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
                                        <ItemStyle Width="1%" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Label ID="lblHRN_ID" runat="server" Text="<%#Bind('HRN_ID')%>"></asp:Label>
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
                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
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
                                                            Text="Date Request" Font-Names="Arial" ForeColor="White"
                                                            CommandArgument="Receive_Date" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                            <asp:Label ID="lblEXPECTED_DATE" runat="server" Text='<%#Bind("SentDate", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd">
                                                        <asp:LinkButton ID="lnkEXPECTED_DATE" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Date Refer" Font-Names="Arial" ForeColor="White"
                                                            CommandArgument="SentDate" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="2%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Label ID="lblSentDate" runat="server" Text='<%#Bind("REASSESMENT_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd">
                                                        <asp:LinkButton ID="lnkSentDate" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Date Re-Refer" Font-Names="Arial" ForeColor="White"
                                                            CommandArgument="reassess_date" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="2%" HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>
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
                                        <ItemStyle Width="2%" HorizontalAlign="Left" />
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
                                        <ItemStyle Width="2%" HorizontalAlign="Left" />
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
                                        <ItemStyle Width="2%" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Label ID="lblsts1" runat="server" Text="<%#Bind('STATUS')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd">
                                                        <asp:LinkButton ID="lnkbtnstws1" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Status" Font-Names="Arial" ForeColor="White"
                                                            CommandArgument="STATUS" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Priority Flag">
                                        <ItemTemplate>
                                            <asp:Image ID="imggridflag01" runat="server" Visible="false" ImageUrl="Images/PriorityFlag.png" Style="height: 35px; width: 35px;"></asp:Image>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="styleCellsHeaderLOVWnd  text-left" />
                                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlassigndby" runat="server" CssClass="GridviewMandatoryDropdownStyle" OnSelectedIndexChanged="ddlassigndby_SelectedIndexChanged" AutoPostBack="true" Style="border: 1px solid #0f6cbd"
                                                Width="150px" Height="20px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: none; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd" width="50px">
                                                        <asp:Label ID="lblCondition" runat="server" Text=""></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-position: bottom; background-repeat: repeat-x; background-image: url('Images/GridviewHeader.png'); line-height: 11px; vertical-align: bottom;"></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemStyle Width="13%" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Label ID="lblactionBy" runat="server" Text="<%#Bind('ACTION_BY')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd">
                                                        <asp:LinkButton ID="lnkbtnstsw1" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Action By" Font-Names="Arial" ForeColor="White"
                                                            CommandArgument="ACTION_BY" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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

                                            <asp:Label ID="lbltransferfrom" runat="server" Text="<%#Bind('Transfer_from')%>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #aa97dd; border-collapse: collapse;">
                                                <tr>
                                                    <td class="gridtabletd">
                                                        <asp:LinkButton ID="lnkbtnsts1" runat="server" Font-Underline="false" Font-Bold="true"
                                                            Text="Transfer From" Font-Names="Arial" ForeColor="White"
                                                            CommandArgument="Transfer_from" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                        <div style="text-align: right"> 
                             <asp:Button Text="Assign" ID="btnAssign" CssClass="btn btngreen" runat="server" OnClick="btnAssign_Click" />
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

                                                <td style="width: 90%; text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #000080;"
                                                    height="30px">
                                                    <asp:Label ID="Label1" runat="server" Text="Redirect to Registration Screen" ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
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
