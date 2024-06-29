<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FE0001R1V2.aspx.cs" Inherits="CLUSTER_MRTS.FE0001R1V2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">

    <!-- Bootstrap 5.2.0 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css?v=1.0" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Customize Bootstrap -->
    <link rel="stylesheet" href="CSS/bootstrap.css" type="text/css" />



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
                                    <span hidden>
                                    <asp:ImageButton ID="imgbtnClear" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                                        ToolTip="Search" />

                                    <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                                        ToolTip="Save" />



                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                                        ToolTip="Allow user to delete" />
                                        </span>
                                    <asp:ImageButton ID="imgbtnprint" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                                        ToolTip="Export" OnClick="imgbtnprint_Click" />
                                    <span hidden>
                                    <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                                        ToolTip="Audit Log" />
                                        </span>
                                    <%--<asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                                        ToolTip="Information" />--%>


                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="card">
                <div class="box box-solid" style="border-radius: 20px;">
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
                                                        <td class="gridtabletd">
                                                            <asp:Label ID="lblcolumnname" runat="server" Text="Column Name"></asp:Label>
                                                            <%-- <span style="margin-left:20px"><i class="fa fa-sign-out"></i></i></span>--%>
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
                                                        <td class="gridtabletd">
                                                            <asp:Label ID="lblCondition" runat="server" Text="Condition"></asp:Label>

                                                            <%-- <span style="margin-left:20px"><i class="fa fa-sign-out"></i></span>--%>
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
                                                        <td class="gridtabletd">
                                                            <asp:Label ID="lblsearchInput" runat="server" Text="Value"></asp:Label>
                                                            <%--  <span style="margin-left:20px"><i class="fa fa-sign-out"></i></span>--%>
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
                                    <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                    <RowStyle CssClass="GridviewRowStyle" />
                                    <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
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
                            <label class="text-right" style="color: white">Total Records : <span id="lblTotalRecords" runat="server"></span></label>
                        </div>
                    </div>
                   
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="form">
                            <asp:HiddenField ID="hdnVisiblity" runat="server" />
                            <div class="table-responsive table--no-card m-b-30" id="divid">
                               <asp:GridView ID="gvUserHistory" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
    AutoGenerateColumns="False" OnRowDataBound="gvUserHistory_RowDataBound"
    OnSorting="gvUserHistory_Sorting" CellPadding="2" ForeColor="white"
    HorizontalAlign="Center" PageSize="10" CssClass="table table-borderless table-striped"
    AllowSorting="True">

    <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
    <RowStyle CssClass="GridviewRowStyle" />
    <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
</asp:GridView>
                            </div>
                            <div class="col-lg-12">
                                <div class="row form-group">
                                    <div class="col-sm-12" style="text-align: center">
                                          <asp:Repeater ID="rptPager" runat="server"  OnItemDataBound="rptPager_ItemDataBound">
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

                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanelModal6success" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="btnerrorsuccess" runat="server" />
                                <cc1:ModalPopupExtender ID="Modelpopuperrorsuccess" runat="server" BackgroundCssClass="modal-background"
                                    DynamicControlID="btnerrorsuccess" PopupControlID="pnlpopuperrorsuccess" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                    TargetControlID="btnerrorsuccess" />
                                <asp:Panel ID="pnlpopuperrorsuccess" runat="server" BackColor="#F0F6F6" BorderStyle="Outset"
                                    EnableTheming="True" Style="text-align: center; resize: none; align-content: flex-start; align-items: flex-start;" Width="447px">
                                    <center>
                                        <table style="width: 98%; text-align: left" bgcolor="#F0F6F6">
                                            <tr>

                                                <td style="width: 70%; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #000080;"
                                                    height="30px">
                                                    <asp:Label ID="Label1" runat="server" Text="Info." ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
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

                                                                <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: lighter; color: #808080; font-size: 12pt;">------------------------------------------------------------------------------</td>
                                                            </tr>
                                                            <tr>

                                                                <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: lighter; color: #212529; font-size: 12pt;">
                                                                    <asp:Label ID="Labelsuccesmsg" runat="server" Style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: lighter; color: #212529; font-size: 12pt;"></asp:Label>

                                                                </td>


                                                            </tr>
                                                            <tr>

                                                                <td style="width: 28%; font-family: Arial, Helvetica, sans-serif; font-weight: lighter; color: #808080; font-size: 12pt;">------------------------------------------------------------------------------</td>
                                                            </tr>

                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="text-align: center">
                                                    <asp:Button ID="btCloseSuccess" runat="server" align="right" CssClass="SubmitButtonStyle"
                                                        Text="OK" Width="100px" BorderColor="White" BackColor="#DC3545" />
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
    <script>
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        function BeginRequestHandler(sender, args) {
            if ($get('divid') != null) {
                xPos = $get('divid').scrollLeft;
                yPos = $get('divid').scrollTop;
            }
        }
        function EndRequestHandler(sender, args) {
            if ($get('divid') != null) {
                $get('divid').scrollLeft = xPos;
                $get('divid').scrollTop = yPos;
            }
        }
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function enter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=lbtnSearch.ClientID%>").click();
                return false;
            }
        }

        function TriggerSearch() {
            const input = document.getElementById("<%=lbtnSearch.ClientID%>")
            input.click();
        }
    </script>
</asp:Content>
