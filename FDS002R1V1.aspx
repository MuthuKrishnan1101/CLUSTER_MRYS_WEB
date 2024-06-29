<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FDS002R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FDS002R1V1" %>

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

    <style>
        .btnTabs {
            padding: 10px;
            border-top-left-radius: 8px;
            border-top-right-radius: 8px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnl6" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnClickEvent" runat="server" />
            <asp:HiddenField ID="hdnfrom" runat="server" />
                        <asp:HiddenField ID="hdnto" runat="server" />
                        <asp:HiddenField ID="hdnpageIndex" runat="server" />
            <div class="ToolBarcard">
                <table width="100%">
                    <tr>
                        <td>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">
                                    <asp:ImageButton ID="imgbtnNew" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                                        ToolTip="Add New" OnClick="imgbtnNew_Click" />
                                      
                                         <asp:ImageButton ID="imgbtnSave" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                                             ToolTip="Save" OnClick="imgbtnSave_Click" />
                                          
                                </div>
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
                                <HeaderStyle CssClass="GridHeaderTextforScroll-UnText"  Font-Bold="True" ForeColor="white"></HeaderStyle>
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
            <div style="width: 100%;">
                <div class="box box-primary" style="border-radius: 30px; border: 1px dashed #2aa7ed">
                    <div class="box-header" style="padding: 10px !important; background-color: #2aa7ed !important; border-radius: 30px;">
                        <asp:UpdatePanel ID="upnlTabs" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfTabID" runat="server" Value="#tab_1" />
                                  <span hidden> <asp:Button ID="btntriggertab1" runat="server" Text="ALL" OnClick="btntriggertab1_Click " CssClass="btnTabs" BackColor="#397279" ForeColor="white" />&nbsp;</span>
                                    <asp:Button ID="btntriggertab2" runat="server" Text="Pending Assign" OnClick="btntriggertab2_Click" CssClass="btnTabs" BackColor="#397279" ForeColor="white" />&nbsp;
                                    <asp:Button ID="btntriggertab3" runat="server" Text="Writing Report Tracking" OnClick="btntriggertab3_Click" CssClass="btnTabs" BackColor="#63bff7" ForeColor="white" />&nbsp;
                                    <asp:Button ID="btntriggertab4" runat="server" Text="Pending Report Verification" OnClick="btntriggertab4_Click" CssClass="btnTabs" BackColor="#63bff7" ForeColor="white" />&nbsp;
                                    <asp:Button ID="btntriggertab5" runat="server" Text="Pending Release" OnClick="btntriggertab5_Click" CssClass="btnTabs" BackColor="#63bff7" ForeColor="white" />&nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                  
                    <div class="box-body">
                        <div class="tab-content pl-3 pt-2" id="nav-tabContent">
                            <!-- tab detail 1 -->
                            <div class="tab-pane fade active show" id="custom-nav-home" role="tabpanel" aria-labelledby="custom-nav-home-tab">
                                <div class="row Gridtable">
                                   
                                    <div class="form-horizontal" style="margin-right: 20px; margin-left: 20px;margin-top: -10px;">
                                        <!-- /.box-header -->
                                        <div class="box-body">
                                            <div class="form">
                                                <div class="table-responsive table--no-card m-b-30" id="divid">
                                                    <asp:GridView ID="gvUserHistory" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                        AutoGenerateColumns="False"
                                                        CellPadding="2"
                                                        ForeColor="#333333"
                                                        HorizontalAlign="Center"
                                                        PageSize="10"
                                                        CssClass="table table-borderless table-striped" OnRowDataBound="gvUserHistory_RowDataBound">
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last"/>
                                                        <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="UNIQUE_ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbluniqid" runat="server" Visible="false" Text='<%#Eval("Request_ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="text-left" Width="70px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbtnRequest_ID" runat="server" Text="<%#Bind('Request_ID')%>" CommandArgument='<%#Bind("request_id")%>' OnClick="lnkbtnRequest_ID_Click"></asp:LinkButton>

                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkRequest_ID" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Request No" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="Request_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblMR_ID" runat="server" Text="<%#Bind('MR_ID')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkMR_ID" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="MR No" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="MR_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemStyle  HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpat_name" runat="server" Text="<%#Bind('PATIENT_NAME')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnpat_name" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Patient Name" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="PATIENT_NAME" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRequestor" runat="server" Text="<%#Bind('REQ_NAME')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnRequestor" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Requestor" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="RptReq_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblRptTyp_ID" runat="server" Text="<%#Bind('REPORT_TYPE')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnRptTyp_ID" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Report Type" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="RptTyp_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemStyle  HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                           <%-- <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVolume" runat="server" Text="<%#Bind('VOLUME_OF_CN')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnVolume" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Volume Of CN" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="VOLUME_OF_CN" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemStyle   HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEMR" runat="server" Text="<%#Bind('EMR')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnEMR" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="EMR" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="EMR" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblRptPur_ID" runat="server" Text="<%#Bind('PURPOSES')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnRptPur_ID" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Purpose" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="RptPur_ID" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblRsts" runat="server" Text="<%#Bind('MR_STATUS')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnMR_STATUS" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Status" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="MR_STATUS" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblAction" runat="server" Text="<%#Bind('MODIFIED_BY')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnAction" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Action By" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="MODIFIED_BY" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblDepartments" runat="server" Text="<%#Bind('DEPARTMENTS')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnpDepartments" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Departments" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="DEPARTMENT" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblPending" runat="server" Text="<%#Bind('PENDING_ITEMS')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnpcodeadd" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Pending Items" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="PENDING_ITEMS" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemStyle   HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Priority Flag">
                                                                <ItemTemplate>
                                                                   <asp:Image ID="imggridflag02" runat="server" Visible="false" ImageUrl="Images/PriorityFlag.png" Style="height: 35px; width: 35px;"></asp:Image>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="styleCellsHeaderLOVWnd  text-left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstartdate" runat="server"  Text='<%#Bind("START_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnstartdate" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Process Start Date" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="START_DATE" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblenddate" runat="server"  Text='<%#Bind("END_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnenddate" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Process End Date" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="END_DATE" OnClick="LnkbtnSort_Click"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblnoofdays" runat="server" Text="<%#Bind('NO_OF_DAYS')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnnoofdays" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="No Of Days" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="NO_OF_DAYS" OnClick="LnkbtnSort_Click"></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemStyle   HorizontalAlign="Left" />
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
                            </div>
                        </div>
                        <div class="row">
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
                                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function ClickTab1() {
            document.getElementById("<%=btntriggertab1.ClientID%>").click();
            return true;
        }
        function ClickTab2() {
            document.getElementById("<%=btntriggertab2.ClientID%>").click();
            return true;
        }
        function ClickTab3() {
            document.getElementById("<%=btntriggertab3.ClientID%>").click();
            return true;
        }
        function ClickTab4() {
            document.getElementById("<%=btntriggertab4.ClientID%>").click();
            return true;
        }
        function ClickTab5() {
            document.getElementById("<%=btntriggertab5.ClientID%>").click();
            return true;
        }
    </script>
</asp:Content>
