<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FB0004R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FB0004R1V1" %>

<%@ Import Namespace="System.Data" %>

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
                        <asp:ImageButton ID="imgbtnClear" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                            ToolTip="Add New" />

                        <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                            ToolTip="Show all records" />

                        <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                            ToolTip="Save" />

                        <asp:ImageButton ID="imgbtnDelete" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                            ToolTip="Delete" />

                        <asp:ImageButton ID="imgbtnprint" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                            ToolTip="Export" />

                        <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                            ToolTip="Audit Log" />

                        <%--<asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                            ToolTip="Information" />--%>
                    </div>
                </div>
            </div>

            <div class="card">
            <div class="box box-solid" style="border-radius: 30px;">
                <br />
                <div class="row">
                    <div class="col-xs-11 col-sm-11 col-md-11 text-right" style="margin-left: 2%;">
                        <asp:LinkButton ID="lbtnadd" runat="server" CssClass="btn btngreen" Style="font-size: 16px; line-height: 37px; padding: 0px 15px; border-radius: 15px;">Import</asp:LinkButton>
                    </div>
                </div>
                <br />

                <div class="container-fluid">
                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                        <div class="box-header with-border TotalRecord">
                            <div class="box-title">
                                <label class="text-right" style="color: white">Batch Payment Status Update<span id="lblTotalRecords" runat="server"></span></label>
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <div class="form">
                                <asp:HiddenField ID="hdnVisiblity" runat="server" />
                                <div class="table-responsive table--no-card m-b-30" id="divid">
                                    <asp:GridView ID="gvUserHistory" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                        AutoGenerateColumns="False"
                                        CellPadding="2"
                                        ForeColor="#333333"
                                        HorizontalAlign="Center"
                                        PageSize="10"
                                        CssClass="table table-borderless table-striped">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnUserID" runat="server" Text="<%#Bind('ORD_ID')%>" CommandArgument='<%#Eval("ORD_ID")+","+ Eval("ORDI_ID")+","+ Eval("TYPE_OF_TRANSPORT")%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnusrid" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Value Date" Font-Names="Arial" ForeColor="White"
                                                                    CommandArgument="ORD_ID"></asp:LinkButton>
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
                                                    <asp:Label ID="lblordno" runat="server" Text="<%#Bind('ORDI_ID')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnprocesssts7" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Transaction Description" Font-Names="Arial" ForeColor="White"
                                                                    CommandArgument="ORDI_ID"></asp:LinkButton>
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
                                                    <asp:Label ID="lblTransDate" runat="server" Text='<%#Bind("TRANS_DATE", "{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnHeadershortName9" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Transaction Description 2" Font-Names="Arial" ForeColor="White"
                                                                    CommandArgument="TRANS_DATE"></asp:LinkButton>
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
                                                    <asp:Label ID="lblEXPECTED_DATE" runat="server" Text='<%#Bind("EXPECTED_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkEXPECTED_DATE" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Preferred Reference" Font-Names="Arial" ForeColor="White"
                                                                    CommandArgument="EXPECTED_DATE"></asp:LinkButton>
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
                                                    <asp:Label ID="lbljobsts" runat="server" Text="<%#Bind('PROCESS_STATUS')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnprocesssts6" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Amount" Font-Names="Arial" ForeColor="White"
                                                                    CommandArgument="PROCESS_STATUS"></asp:LinkButton>
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
                                                    <asp:Label ID="lbljobsts017" runat="server" Text="<%#Bind('JOB_STATUS')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="MR Reference" Font-Names="Arial" ForeColor="White"
                                                                    CommandArgument="JOB_STATUS"></asp:LinkButton>
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
                                                    <asp:Label ID="lblConsigneeName" runat="server" Text="<%#Bind('BECONG_NAME')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnpConsigneeName" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="MR Created Date" Font-Names="Arial" ForeColor="White"
                                                                    CommandArgument="BECONG_NAME"></asp:LinkButton>
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
                                                    <asp:Label ID="lblConsigneepscodeadd" runat="server" Text="<%#Bind('BECONG_ADDRESS')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnpcodeadd" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="MR Fee" Font-Names="Arial" ForeColor="White"
                                                                    CommandArgument="BECONG_ADDRESS"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemStyle Width="13%" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConsigneepscode" runat="server" Text="<%#Bind('BECONG_ZIP_CODE')%>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                        <tr>
                                                            <td class="gridtabletd">
                                                                <asp:LinkButton ID="lnkbtnpcode" runat="server" Font-Underline="false" Font-Bold="true"
                                                                    Text="Status" Font-Names="Arial" ForeColor="White"
                                                                    CommandArgument="BECONG_ZIP_CODE"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemStyle Width="4%" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                        <RowStyle CssClass="GridviewRowStyle" />
                                        <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-11 col-sm-11 col-md-11 text-right" style="margin-left: 2%; top: 0px; left: 1px;">
                        <asp:LinkButton ID="lnkbtnsave" runat="server" CssClass="btn btnyellow" Style="font-size: 16px; line-height: 37px; padding: 0px 15px; border-radius: 15px;">Save</asp:LinkButton>
                    </div>
                </div>
                 <br />
                <div class="table-responsive table--no-card m-b-30">
                    <asp:Panel ID="pnlPdtPlt22" runat="server" Visible="false">
                        <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                            <div class="modal-dialog modal-lg" style="width: 50%;">
                                <div class="modal-content"
                                    style="border: Solid 3px #304863; border-left: Solid 15px #304863; border-right: Solid 15px #304863; border-bottom: solid 25px #304863; height: auto; min-height: 517px; margin-top: 81px;">
                                    <div class="text-center" style="background-color: #304863; padding-top: 3px; padding-bottom: 3px">
                                        <h4 class="modal-title" style="font-weight: bold; font-size: 16px; color: white"></h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="nav-tabs-custom">
                                            <div class="box box-primary box-solid">
                                                <div class="box-header " style="left: 0px; top: 0px; background-color: #8572b7">
                                                    <ul class="nav nav-tabs">
                                                        <li class="" style="background-color: #f4eaff"><a href="#tabPrd_1" data-toggle="tab"
                                                            style="font-weight: 100; font-size: 20px; color: black;">
                                                            <asp:Label runat="server" ID="lbl2ndLvlTabTilte" Text="Tracking Screen"></asp:Label>
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
                                    <br />
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                            </div>
                                            <label for="inputEmail3" class="col-sm-2 control-label">Requestor</label>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtSName" runat="server" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                            </div>
                                            <label for="inputEmail3" class="col-sm-2 control-label">Requestor Name</label>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                            </div>
                                            <label for="inputEmail3" class="col-sm-2 control-label">Amount</label>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtamount" runat="server" CssClass="bootformcontrol"
                                                        ReadOnly="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="box-body">
                                        <div class="form">
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <div class="table-responsive table--no-card m-b-30" id="popuppanel">
                                                <asp:GridView ID="GridView1" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                    AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" HorizontalAlign="Center"
                                                    PageSize="10" CssClass="table table-borderless table-striped">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" />
                                                                <asp:HiddenField ID="hfIDs" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="2%" HorizontalAlign="Left" />
                                                            <ItemStyle Width="2%" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnUserID" runat="server" Text="<%#Bind('ORD_ID')%>"
                                                                    CommandArgument='<%#Eval("ORD_ID")+","+ Eval("ORDI_ID")+","+ Eval("TYPE_OF_TRANSPORT")%>'>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table
                                                                    style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd">
                                                                            <asp:LinkButton ID="lnkbtnusrid" runat="server" Font-Underline="false"
                                                                                Font-Bold="true" Text="Request No" Font-Names="Arial"
                                                                                ForeColor="White" CommandArgument="ORD_ID"></asp:LinkButton>
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
                                                                <asp:Label ID="lblordno" runat="server" Text="<%#Bind('ORDI_ID')%>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table
                                                                    style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd">
                                                                            <asp:LinkButton ID="lnkbtnprocesssts7" runat="server" Font-Underline="false"
                                                                                Font-Bold="true" Text="Amount" Font-Names="Arial"
                                                                                ForeColor="White" CommandArgument="ORDI_ID"></asp:LinkButton>
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
                                                                <asp:Label ID="lblTransDate" runat="server"
                                                                    Text='<%#Bind("TRANS_DATE", "{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table
                                                                    style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd">
                                                                            <asp:LinkButton ID="lnkbtnHeadershortName9" runat="server"
                                                                                Font-Underline="false" Font-Bold="true" Text="Payment Status" Font-Names="Arial"
                                                                                ForeColor="White" CommandArgument="TRANS_DATE">
                                                                            </asp:LinkButton>
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
                                                                <asp:Label ID="lblEXPECTED_DATE" runat="server"
                                                                    Text='<%#Bind("EXPECTED_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table
                                                                    style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd">
                                                                            <asp:LinkButton ID="lnkEXPECTED_DATE" runat="server" Font-Underline="false"
                                                                                Font-Bold="true" Text="Requestor" Font-Names="Arial"
                                                                                ForeColor="White" CommandArgument="EXPECTED_DATE"></asp:LinkButton>
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
                                                                <asp:Label ID="lbljobsts" runat="server" Text="<%#Bind('PROCESS_STATUS')%>">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <table
                                                                    style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td class="gridtabletd">
                                                                            <asp:LinkButton ID="lnkbtnprocesssts6" runat="server" Font-Underline="false"
                                                                                Font-Bold="true" Text="Requestor Name" Font-Names="Arial"
                                                                                ForeColor="White" CommandArgument="PROCESS_STATUS"></asp:LinkButton>
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
                                        </div>
                                    </div>
                                    <div class="modal-footer" style="text-align: center;">
                                        <asp:Repeater ID="rptPager" runat="server">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>'
                                                    CommandArgument='<%# Eval("Value") %>'
                                                    CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="modal-footer" style="text-align: center;">
                                        <asp:Button runat="server" ID="btnCancelCongWH_Name" Text="Cancel" CssClass="btn btn-danger" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
                 </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

