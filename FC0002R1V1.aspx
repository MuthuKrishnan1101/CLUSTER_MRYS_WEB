<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FC0002R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FC0002R1V1" %>

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
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
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
                            ToolTip="Save" OnClick="imgbtnSave_Click" />


                        <span hidden>
                        <asp:ImageButton ID="imgbtnDelete" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/DELETE_MENU.png"
                            ToolTip="Delete" OnClick="imgbtnDelete_Click" />

                        <asp:ImageButton ID="imgbtnprint" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/PRINT_MENU.png"
                            ToolTip="Export" />
                            </span>
                        <asp:ImageButton ID="imgbtnAudit" runat="server" CssClass="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/AUDIT_MENU.png"
                            ToolTip="Audit Log" OnClick="imgbtnAudit_Click" />

                       <%-- <asp:ImageButton ID="imgbtnInfo" runat="server" class="MenuImageButton" ImageUrl="Images/MenuTemplatesImg/INFO_MENU.png"
                            ToolTip="Information" />--%>

                    </div>
                </div>
            </div>
            <div class="card">
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
                                                    <label for="inputEmail3" class="col-sm-2 control-label"> MRN  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtHRN" MaxLength="50" Wrap="False" runat="server" CssClass="form-control" onkeypress="return HRNenter1_click(event);"></asp:TextBox>
                                                            <span hidden>
                                                                <asp:TextBox ID="txtpatientID" MaxLength="50" Wrap="False" runat="server" CssClass="form-control" onkeypress="return HRNenter1_click(event);"></asp:TextBox></span>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="LkBtnBios" runat="server" CausesValidation="False" CssClass="LnkbtnPatient"
                                                             OnClick="LkBtnBios_Click">Bio Data</asp:LinkButton>
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-2 control-label">Patient </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtPatName" runat="server" ToolTip="Patient" CssClass="ReadOnly bootformcontrol" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">From Date </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtFromDate" runat="server" ToolTip="From Date" MaxLength="10" CssClass="optional form-control" autocomplete="off" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calActDate1" TargetControlID="txtFromDate" runat="server"
                                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2"></div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">To Date </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtToDate" runat="server" ToolTip="To Date" MaxLength="10" CssClass="optional form-control" autocomplete="off" onkeypress="return isDatePicker(event)" OnPaste="JavaScript:return RestrictCopyPasteDatePicker();"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtToDate" runat="server"
                                                                Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Description </label>
                                                    <div class="col-sm-10">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSpecialInformation" TextMode="MultiLine" runat="server" ToolTip="Description" Height="150" CssClass="form-control" Style="resize: none;" ReadOnly="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="bootcolsm1">
                                                    </div>
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Delete Mark  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:CheckBox ID="chkDelMark" runat="server" ToolTip="Delete Mark" CssClass=" bootformcontrol" BorderStyle="none" />
                                                        </div>
                                                    </div>
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
           
                                         <!--Bio Data Popup Start-->
            <div>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkbtnProducts2" runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel22" runat="server"></asp:LinkButton>
                            <ajax:ModalPopupExtender ID="mpePdtPlt2" BehaviorID="mpePdtPlt12" runat="server" TargetControlID="lnkbtnProducts2" CancelControlID="lbtnCancel22" PopupControlID="pnlPdtPlt22" BackgroundCssClass="modal-background"></ajax:ModalPopupExtender>
                            <div class="table-responsive table--no-card m-b-30">
                                <asp:Panel ID="pnlPdtPlt22" runat="server">
                                    <div class="modal bs-example-modal-lg in display-block" style="overflow: scroll;">
                                        <div class="modal-dialog modal-lg" style="width: 50%;">
                                            <div class="modal-content PopupModelContent">
                                                <div class="text-center" style="background-color: #304863;">
                                                    <h4 class="modal-title" style="font-weight: bold; font-size: 16px; color: white"></h4>
                                                </div>
                                                <div class="modal-body" style="border-radius: 20px;">
                                                    <div class="nav-tabs-custom">
                                                        <div class="box box-primary box-solid">
                                                            <div class="box-header Popboxheader" style="">
                                                                <ul class="nav nav-tabs">
                                                                    <li class="" style="background-color: #e3f6fd"><a href="#tabPrd_1" data-toggle="tab"
                                                                        style="font-weight: 500; font-size: 15px; color: black">
                                                                        <asp:Label runat="server" ID="lbl2ndLvlTabTilte" Text="Bio-Data"></asp:Label>
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

                                                <div class="form-horizontal">
                                                    <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                                       <%-- <asp:ImageButton ID="btnbiodatapopupnew" runat="server" Height="28px" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                                                            Width="28px" ToolTip="New Record" OnClick="btnbiodatapopupnew_Click" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                                                 <asp:ImageButton ID="btnbiodatapopupsearch" runat="server" Height="28px" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                                                                                     Width="28px" ToolTip="Search Record" OnClick="btnbiodatapopupsearch_Click" />
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-2">
                                                        </div>
                                                        <label for="inputEmail3" class="col-sm-2 control-label">MRN </label>
                                                        <div class="col-sm-5">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txthrnbiodata" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return Biodataenter1_click(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-2">
                                                        </div>
                                                        <label for="inputEmail3" class="col-sm-2 control-label">Name </label>
                                                        <div class="col-sm-5">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txthrnNamebiodata" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return Biodataenter1_click(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="box-body">
                                                    <div class="form">
                                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                                        <div class="table-responsive table--no-card m-b-30" id="popuppanel">
                                                            <asp:GridView ID="gvlistbiodatapopup" runat="server" GridLines="Vertical" RowStyle-Wrap="true" AutoGenerateColumns="false"
                                                                CssClass="table table-borderless table-striped table-earning">

                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                                <Columns>

                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkbtnpopupHRN" runat="server" Text="<%#Bind('hrn_id')%>" CommandArgument='<%#Eval("hrn_id")+","+ Eval("short_name")+","+ Eval("patient_id")%>' OnClick="lnkbtnpopupHRN_Click"></asp:LinkButton>

                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                <tr>
                                                                                    <td class="gridtabletd" cssclass="GridHeaderText" width="50px">
                                                                                        <asp:Label ID="lblid11f" runat="server" Text="MRN" ForeColor="White"></asp:Label>
                                                                                    </td>
                                                                                </tr>

                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblordno" runat="server" Text="<%#Bind('short_name')%>"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse">
                                                                                <tr>
                                                                                    <td class="gridtabletd" cssclass="GridHeaderText" width="50px">
                                                                                        <asp:Label ID="lblstrgeresf" runat="server" Text="Name" ForeColor="White"></asp:Label>
                                                                                    </td>
                                                                                </tr>

                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
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
                                                <div class="modal-footer" style="text-align: center;">
                                                    <asp:Button runat="server" ID="btnCancelCongWH_Name" OnClientClick="showTabs('btnpatientprofile')" Text="Cancel" CssClass="btn btnred" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <!--Bio Data Popup End-->
             <div>
                 <table width="100%">
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
                 </table>
             </div>

           

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function Biodataenter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=btnbiodatapopupsearch.ClientID%>").click();
                return false;
            }
        }

        function HRNenter1_click(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("<%=LkBtnBios.ClientID%>").click();
                return false;
            }
        }
    </script>
</asp:Content>
