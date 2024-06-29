<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FDS001R1V1.aspx.cs" Inherits="CLUSTER_MRTS.FDS001R1V1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Bootstrap 5.2.0 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css?v=1.0" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Customize Bootstrap -->
    <link rel="stylesheet" href="CSS/bootstrap.css" type="text/css" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />

    <!-- Ionicons -->
    <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css" />

    <!-- gridviewstyle -->
    <link rel="stylesheet" href="CSS/gridviewstyle.css" />

    <!-- javascript -->
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="Scripts/Validation.js" type="text/javascript"></script>

    <style>

       
        #divprogress {
            display: table;
            width: 100%;
            margin: 0;
            padding: 10px 10px 0;
            table-layout: fixed;
            counter-reset: step;
            margin-top: 2%;
        }

        .divprogress li {
            list-style-type: none;
            display: table-cell;
            width: 25%; /*for circle distance b/w 1 to another*/
            float: left;
            font-size: 16px;
            position: relative;
            text-align: center;
            font-weight: 400;
            color: #706F6F;
        }

        :root {
            --progressbar_line_color: #DFDFDF;
            --li_done_color: #706F6F;
            --tab1: #DFDFDF;
            --tab2: #DFDFDF;
            --tab3: #DFDFDF;
            --tab4: #DFDFDF;

    --tab1FontColor: #706F6F;
    --tab2FontColor: #706F6F;
    --tab3FontColor: #706F6F;
    --tab4FontColor: #706F6F;
        }

        #divprogress li:before {
            width: 40px;
            height: 40px;
            color: white;
            content: counter(step);
            counter-increment: step;
            line-height: 40px;
            font-size: 18px;
            border: 1px solid #DFDFDF;
            display: block;
            text-align: center;
            margin: 0 auto 10px auto;
            border-radius: 50%;
            background-color: #fff;
        }

        #divprogress #li_InfoRequest:before {
            background-color: var(--tab1);
             color: var(--tab1FontColor);
        }

        #divprogress #li_Attachment:before {
            background-color: var(--tab2);
             color: var(--tab2FontColor);
        }

        #divprogress #li_SelectAction:before {
            background-color: var(--tab3);
             color: var(--tab3FontColor);
        }

        #divprogress #li_ProcessHistory:before {
            background-color: var(--tab4);
             color: var(--tab4FontColor);
        }

        .line {
            flex-grow: 1;
            border-color: blue;
            border: 1px solid #DFDFDF;
            position: relative;
            top: 40px;
            width: 75%;
            margin-left: 12%;
        }


        @media only screen and (max-width: 750px) {
            .Sidegrid {
                margin-top: 25%;
            }
        }

        
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:FileUpload ID="FileUpload2" runat="server" EnableViewState="true" Style="display: none;" />
            <div class="ToolBarcard">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:HiddenField ID="hdfuniqid" runat="server" />
                        <div class="col-xs-12 col-sm-12 col-md-12 text-right ToolBar">
                            
                            <asp:ImageButton ID="imgbtnSaveAsDraft" runat="server" ImageUrl="Images/MenuTemplatesImg/SAVE_MENU.png"
                                ToolTip="Save" CssClass="MenuImageButton" OnClick="imgbtnSaveAsDraft_Click" />
                               
                        </div>
                    </td>
                </tr>
            </table>
                </div>

            <div class="card">
            <table width="100%">
                <tr>
                    <td>
                        <div class="box box-solid Box">
                            <br />
                            <asp:UpdatePanel ID="updPnlPreRequest" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="100%">

                                        <tr>
                                            <td>
                                                <div class="col-xs-12 col-sm-12 col-md-12 text-right">
                                                    <asp:LinkButton ID="LkBtnBack" runat="server" CssClass="LnkbtnPatient" Text="Back to Control Centre" OnClick="LkBtnBack_Click"></asp:LinkButton>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">MR Number </label>

                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtMRNumberHEADER" ReadOnly="True" runat="server" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-2 control-label">Request Number</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtRequestNo" ReadOnly="True" runat="server" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-inline">
                                                        <div class="col-sm-1">
                                                            <div class="form-group">
                                                                <i class="fa fa-flag" style="color: red"></i>
                                                                <span>
                                                                    <asp:Button runat="server" ID="btnCancelTrigger" OnClick="btnCancelTrigger_Click" /> 
                                                                    <asp:CheckBox runat="server" ID="chkpriorityflag" ToolTip="Priority Flag" class="flagchk" Enabled="false" CssClass="ReadOnly form-control" BorderStyle="none" /></span>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">MR Status </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtMRStatus" runat="server" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-2 control-label">Writing and Verifying Status </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWritingandVerifyingStatus" runat="server" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>

            <div class="box box-solid Box">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div id="divprogress" class="divprogress li">
                                <hr class="line" />
                                <li id="li_InfoRequest" title="Information From Request" onclick="showpanel()"><small>
                                    <asp:Button ID="btnInfoRequest" CssClass="aspButton" runat="server" Text="Info Request" OnClick="btnInfoRequest_Click" />
                                </small></li>
                                <li id="li_Attachment" title="Attachment" onclick="showpanel1()"><small>
                                    <asp:Button ID="btnAttachment" CssClass="aspButton" runat="server" Text="Attachment" OnClick="btnAttachment_Click" /></small>
				</li>
                                <li id="li_SelectAction" title="Select Action" onclick="showpanel2()"><small>
                                    <asp:Button ID="btnSelectAction" CssClass="aspButton" runat="server" Text="Select Action" OnClick="btnSelectAction_Click" /></small>
				</li>
                                <li id="li_ProcessHistory" title="Process History" onclick="showpanel3()"><small>
                                    <asp:Button ID="btnProcessHistory" CssClass="aspButton" runat="server" Text="Process History" OnClick="btnProcessHistory_Click" /></small>
				</li>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="container-fluid">
                    <div class="box-body">
                        <div class="row">
                            <br />
                            <div class="tab-content">
                                <%--  for TAb 1 informations start--%>
                                <asp:Panel ID="pnlmenu1" runat="server">
                                    <div class="form-horizontal">
                                        <div class="box-body">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">MRN </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtHRN" MaxLength="50" Wrap="False" ReadOnly="true" runat="server" CssClass=" ReadOnly form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <label for="inputEmail3" class="col-sm-2 control-label">Patient Name </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtName" runat="server" MaxLength="50" ReadOnly="True" CssClass="ReadOnly form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">

                                                <label for="inputEmail3" class="col-sm-2 control-label">Purpose </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtpurpose" MaxLength="50" Wrap="False" ReadOnly="true" runat="server" CssClass=" ReadOnly form-control"></asp:TextBox>
                                                    </div>
                                                       
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">Report Format </label>
                                                <div class="col-sm-3">
                                                   <div class="form-group">
                                                        <asp:TextBox ID="txtreportformat" MaxLength="50" Wrap="False" ReadOnly="true" runat="server" CssClass=" ReadOnly form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Report Type </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtreporttype" MaxLength="50" Wrap="False" ReadOnly="true" runat="server" CssClass=" ReadOnly form-control"></asp:TextBox>
                                                        <span hidden><asp:TextBox ID="txtreporttypeID" MaxLength="50" Wrap="False" ReadOnly="true" runat="server" CssClass=" ReadOnly form-control"></asp:TextBox></span>
                                                    </div>
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">Case/Visit Number </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtcasevisitno" runat="server" ReadOnly="true" CssClass="ReadOnly form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Appointment  Date </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtAppDate" runat="server" ReadOnly="true" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtAppDatefds_CalendarExtender" TargetControlID="txtAppDate" runat="server"
                                                            Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">Accident Date </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtAccidentDate" ReadOnly="true" runat="server" CssClass="ReadOnly form-control"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtAccidentDate_CalendarExtender" TargetControlID="txtAccidentDate" runat="server"
                                                            Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Related MR Ref</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtrelatedMRref" ReadOnly="true" runat="server" CssClass="ReadOnly form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label">Re-Despatch Date </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtReassDate" runat="server" Enabled="false" CssClass=" ReadOnly form-control"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtReassDate_CalendarExtender" TargetControlID="txtReassDate" runat="server"
                                                            Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Conditioning Information for Doctor </label>
                                                <div class="col-sm-8">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtConInforDoctor" runat="server" ReadOnly="true" TextMode="MultiLine"  Style="resize: none" CssClass="ReadOnly form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">EMR </label>
                                                <asp:UpdatePanel ID="updpnlEMR" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <div class="col-sm-3">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:RadioButton ID="rbtEMR" Enabled="false" runat="server" CssClass="ReadOnly Space" AutoPostBack="false" style="position: relative; left: -12px;"  Width="100%" GroupName="rdgrpSelection1" />
                                                                            </td>
                                                                            
                                                                            <td>
                                                                                <label style="position: relative; top: 3px;">Yes</label></td>
                                                                            <td>&nbsp;&nbsp;</td>
                                                                            <td>
                                                                                <asp:RadioButton ID="rbt1EMR" Enabled="false" runat="server" CssClass="ReadOnly Space" AutoPostBack="false"  Width="100%" GroupName="rdgrpSelection1" />
                                                                            </td>
                                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                                            <td>
                                                                                <label style="position: relative; top: 3px;">No</label></td>

                                                                            <td>&nbsp;&nbsp;</td>
                                                                            <td>
                                                                                <asp:RadioButton ID="rbtnboth" Enabled="false" runat="server" CssClass="ReadOnly Space" AutoPostBack="false"  Width="100%" GroupName="rdgrpSelection1" />
                                                                            </td>
                                                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                                                            <td>
                                                                                <label style="position: relative; top: 3px;">Both</label></td>
                                                                        </tr>
                                                                    </table>
                                                            </div>
                                                            
                                                            <label for="inputEmail3" style="margin-left: -5px" class="col-sm-2 control-label">Source of Reference </label>
                                                            <div class="col-sm-3">
                                                                <div class="form-group"> 
                                                                    <asp:DropDownList ID="ddlReference" Enabled="false" runat="server" style="margin-left: -8px" Width="98%" CssClass="ReadOnly form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <asp:Panel ID="pnlRemarksresultgrid" runat="server" Visible="true">
                                                                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed">
                                                                        <div class="box-header with-border TotalRecord">
                                                                            <div class="box-title">
                                                                                <label class="text-right" style="color: white">Remarks Total Record : <span id="lblpnlremRecord" runat="server"></span></label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="box-body">
                                                                            <div class="form">
                                                                                <asp:HiddenField ID="HiddenField13" runat="server" />
                                                                                <div class="table-responsive table--no-card m-b-30">
                                                                                    <asp:GridView ID="gvList" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                                        AutoGenerateColumns="False"
                                                                                        CellPadding="2"
                                                                                        ForeColor="#333333"
                                                                                        HorizontalAlign="Center"
                                                                                        PageSize="10"
                                                                                        CssClass="table table-borderless table-striped table-responsive">
                                                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                                        <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblsno" runat="server" Text="<%#Bind('REFERENCE_1')%>"></asp:Label>
                                                                                                </ItemTemplate>

                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkbtnRemarkID" runat="server" Text="<%#Bind('SHORT_NAME')%>" CommandArgument='<%#Eval("REMARK_ID")+","+ Eval("Request_ID")+","+ Eval("REGRMK_ID")%>' />
                                                                                                </ItemTemplate>
                                                                                                <HeaderTemplate>
                                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                        <tr>
                                                                                                            <td class="gridtabletd">
                                                                                                                <asp:LinkButton ID="lnkbtnEnq_Date1" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                                                    Text="Remarks Templates" Font-Names="Arial" ForeColor="White"
                                                                                                                    CommandArgument="REGRMK_ID"></asp:LinkButton>
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
                                                                                                    <asp:TextBox ID="lblremarks" runat="server" Text="<%#Bind('remarks')%>" TextMode="MultiLine" Style="resize: none; height: 50px;" Width="100%" ReadOnly="true" CssClass="MultiLine_Textbox optional"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                                <HeaderTemplate>
                                                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                                        <tr>
                                                                                                            <td class="gridtabletd">
                                                                                                                <asp:Label ID="lnkbtnname" runat="server" Text="Description"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Target Audience">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblReportPendingItemID" runat="server" CssClass="styleCellsLeft" Text='<%# Eval("TARG_AUD") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="styleCellsHeaderLOVWnd text-left" />
                                                                                                <ItemStyle HorizontalAlign="left" />
                                                                                            </asp:TemplateField>


                                                                                            <asp:TemplateField HeaderText="Remark Date">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblRemarkDate" runat="server" CssClass="styleCellsLeft" Text='<%#Bind("REMARKS_DATE","{0:dd-MM-yyy HH:mm}")%>' ></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="styleCellsHeaderLOVWnd text-left" />
                                                                                                <ItemStyle HorizontalAlign="left" />
                                                                                            </asp:TemplateField>

                                                                                                <asp:TemplateField HeaderText="Modified User">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblModifiedusr" runat="server" CssClass="styleCellsLeft" Text='<%# Eval("MODIFIED_BY") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="styleCellsHeaderLOVWnd text-left" />
                                                                                                <ItemStyle HorizontalAlign="left" />
                                                                                            </asp:TemplateField>

                                                                                        </Columns>
                                                                                        <RowStyle CssClass="GridviewRowStyle" />
                                                                                        <HeaderStyle CssClass="GridHeaderTextforScroll-UnText"></HeaderStyle>
                                                                                        <RowStyle CssClass="GridviewAlternatingRowStyle" />
                                                                                        <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row form-group" hidden>
                                                                                <div class="col-sm-12" style="text-align: center">
                                                                                    <asp:Repeater ID="rptPagerRemarks" runat="server">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                                Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                           <%-- <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Remarks </label>
                                                <div class="col-sm-8">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtRemarksInfo" runat="server" ReadOnly="true" TextMode="MultiLine" Style="resize: none" CssClass="ReadOnly form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <label for="inputEmail3" class="col-sm-2 control-label"> </label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        
                                                    </div>
                                                </div>
                                            </div>--%>

                                        </div>
                                    </div>

                                    <ul class="list-unstyled list-inline pull-right" style="border-radius: 40px;">
                                        <li>
                                            <button type="button" class="btn btngreen next-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px" onclick="showpanel1()">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></button>
                                        </li>
                                    </ul>
                                </asp:Panel>
                                <%--  for TAb 1 informations END--%>

                                <%--  for TAb 2 informations start--%>
                                <asp:Panel ID="pnlmenu2" runat="server">
                                    <div>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <div class="form-group">

                                                        <label for="inputEmail3" class="col-sm-2 control-label">Select File for Attachment </label>
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <asp:FileUpload ID="FileUpload1" runat="server" EnableViewState="true" />
                                                                 <label style="color:red;">Allowed File Extensions :.pdf,.jpg,.jpeg,.PNG</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="form-group">

                                                        <label for="inputEmail3" class="col-sm-2 control-label">Add Remark </label>
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="form-group">

                                                        <label for="inputEmail3" class="col-sm-2 control-label">Category </label>
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory" ErrorMessage="Please select a category." ValidationGroup="AddAttachment"></asp:RequiredFieldValidator>--%>
                                </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>

                                        </table>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-12 text-right" style="margin-left: -15px">
                                                <asp:UpdatePanel ID="UpnlUpload" runat="server">
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkbtnaddattachments" />
                                                    </Triggers>
                                                    <ContentTemplate>

                                                        <asp:LinkButton ID="lnkbtnaddattachments" OnClientClick="return validateCategory();" runat="server" OnClick="lnkbtnaddattachments_Click" CssClass="btn  btngreen" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px"><i class="fa fa-plus" aria-hidden="true"></i> ADD</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnaddattachmentsclear" runat="server" OnClick="lnkbtnaddattachmentsclear_Click" CssClass="btn  btnred" Style="font-size: 13px; line-height: 37px; padding: 0px 15px; border-radius: 15px"><i class="fa fa-times-circle" aria-hidden="true"></i> Clear</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>

                                        <br />
                                        <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed; width: 99%">
                                            <div class="box-header with-border TotalRecord">
                                                <div class="box-title">
                                                    <label class="text-right" style="color: white">Total Records : <span id="lbltotalrecAttachments" runat="server"></span></label>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <div class="form">
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <div class="table-responsive table--no-card m-b-30" id="divid1">
                                                        <asp:GridView ID="gvAttachments" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                            AutoGenerateColumns="False"
                                                            CellPadding="2"
                                                            ForeColor="#333333"
                                                            HorizontalAlign="Center"
                                                            PageSize="10"
                                                            CssClass="table table-borderless table-striped" OnRowDataBound="gvAttachments_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="ltrID" runat="server" Text='<%# Eval("ATTACH_ID") %>' Visible="false"></asp:Literal>
                                                                        <asp:LinkButton ID="lnkbtnattachmentid" runat="server" Text="" CommandArgument='<%#Eval("BE_ID")+","+ Eval("FORM_ID")+","+ Eval("TRANS_ID")+","+ Eval("DOC_NAME")+","+ Eval("DOC_TYPE")+","+ Eval("ATTACH_ID")%>' OnClientClick="showLoader()" OnClick="lnkbtnattachmentid_Click"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:Label ID="lblFilename" runat="server" Text="File Name"></asp:Label>
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
                                                                        <asp:Label ID="lblAttachedDateatt" runat="server" Text='<%#Bind("Created_On", "{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:Label ID="lblAttachedon" runat="server" Text="Attached On"></asp:Label>

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
                                                                        <asp:Label ID="lblAttachedbyatt" runat="server" Text='<%#Bind("Created_By")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:Label ID="lblAttachedBy" runat="server" Text="Attached By"></asp:Label>

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
                                                                        <asp:Label ID="lblremarksatt" runat="server" Text='<%#Bind("REMARKS")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:Label ID="lblRemarks01" runat="server" Text="Remarks"></asp:Label>
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
                                                                        <asp:Label ID="lblCategoryatt" runat="server" Text='<%#Bind("CATEGORY")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td class="gridtabletd">
                                                                                    <asp:Label ID="lblCategory01" runat="server" Text="Category"></asp:Label>

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
                                                                        <asp:Button ID="btnDeleteAddattachments" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDeleteAddattachments_Click" CommandArgument='<%#Eval("BE_ID")+","+ Eval("FORM_ID")+","+ Eval("TRANS_ID")+","+ Eval("DOC_NAME")+","+ Eval("DOC_TYPE")+","+ Eval("ATTACH_ID")%>' />
                                                                        <controlstyle cssclass="btn btnred"></controlstyle>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                            <tr>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle Width="3%" HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                            <RowStyle CssClass="GridviewRowStyle" />
                                                            <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="row form-group">
                                                            <div class="col-sm-12" style="text-align: center">
                                                                <asp:Repeater ID="Repeater1" runat="server">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                            CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div>
                                                </div>
                                            </div>
                                        </div>
                                        <ul class="list-unstyled list-inline pull-right">
                                            <li>
                                                <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px" onclick="showpanel()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button></li>
                                            <li>
                                                <button type="button" class="btn btngreen next-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px" onclick="showpanel2()">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></button>
                                        </ul>
                                    </div>
                                </asp:Panel>
                                <%--  for TAb 2 informations END--%>

                                <%--  for TAb 3 informations starts--%>
                                <asp:Panel ID="pnlmenu3" runat="server">

                                    <div class="form-horizontal">
                                          <div class="box-body">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Please Select Action </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlApplicationStatus" OnSelectedIndexChanged="ddlApplicationStatus_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                         <asp:Button runat="server" Text="Confirm" ID="btnConfirm" OnClick="btnConfirm_Click" Visible="true" CssClass="  btn btngreen " Style="border-radius: 10px" />
                                                    </div>
                                                </div>
                                                </div>
                                                </div>
                                    

                                    <!-- panel RerouteRequest start -->
                                    <asp:Panel ID="pnlRerouteRequest" runat="server" Visible="false">
                                        <h4 style="color: red;">Reroute Request</h4>
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Current Location </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtcurrentloc" ReadOnly="true" runat="server" CssClass=" Readonly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Department OU  </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlDepartmentOU" runat="server" ReadOnly="false" Enabled="true" CssClass="ReadOnly form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Remarks </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Style="resize: none" CssClass="optional form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <!-- panel RerouteRequest End -->

                                    <!-- panel RejectRequest start -->
                                    <asp:Panel ID="pnlRejectRequest" runat="server" Visible="false">
                                        <h4 style="color: red;">Reject Request </h4>
                                        <div class="form-horizontal">
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Location </label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtLocation" runat="server" Text="HIMS" ReadOnly="true" CssClass=" Readonly form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Reject Reason :</label>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtRejectReason" runat="server" CssClass="form-control" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <!-- panel RejectRequest End -->

                                    <!-- panel AssignDoctor & Verifier start -->
                                    <asp:Panel ID="pnlAssignDoctorandverifier" runat="server" Visible="false">
                                        <h4 style="color: red;">Assign Doctor & Verifier </h4>
                                        <div class="form-horizontal">
                                              <div class="box-body">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Assign Doctor</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtAssignDoctor" runat="server" ToolTip="Assign Doctor" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                &nbsp; &nbsp; &nbsp;
                                             <asp:LinkButton ID="LkBtnBios" CssClass="LnkbtnPatient" runat="server" CausesValidation="False"
                                                  OnClick="LkBtnDoc1_Click">Select</asp:LinkButton>
                                            </div>
                                                 </div>

                                            <div class="form-group">
                                                <asp:Panel ID="pnlAssignDoctor" runat="server">
                                                    <div class="box-body">
                                                        <div class="form">
                                                            <asp:HiddenField ID="HiddenField7" runat="server" />
                                                            <div class="table-responsive table--no-card m-b-30">
                                                                <asp:GridView ID="gvassigndoctor" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                    AutoGenerateColumns="False"
                                                                    border-radius="40px"
                                                                    CellPadding="2"
                                                                    ForeColor="#333333"
                                                                    HorizontalAlign="Center"
                                                                    PageSize="10" OnRowDataBound="gvassigndoctor_RowDataBound"
                                                                    CssClass="table table-borderless table-striped">
                                                                    <%-- <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                            <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />--%>
                                                                    <Columns>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkbtnpopupDOCID" runat="server" Text="<%#Bind('EMP_NO')%>" CommandArgument='<%#Eval("EMP_NO")+","+ Eval("UNIQUE_ID")%>'></asp:LinkButton>

                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblid11fx" runat="server" Text="Doctor Employee Number"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmcr_no" runat="server" Text="<%#Bind('DEPT_DESC')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblmcrx" runat="server" Text="Department Name"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblShort_Name" runat="server" Text="<%#Bind('NAME')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblDescriptionx" runat="server" Text="Doctor Name"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmcr" runat="server" Text="<%#Bind('MCR_NO')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblmcrx" runat="server" Text="MCR Number"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblseqno" runat="server" Text="<%#Bind('SEQ_NO')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblseqnox" runat="server" Text="Sequence Number"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblrejrea" runat="server" Text="<%#Bind('REJ_REASON')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblrejreax" runat="server" Text="Reject Reason"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblrejreatime" runat="server" Text="<%#Bind('REJ_TIME_STAMP')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblrejreatimxe" runat="server" Text="Reject TimeStamp"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblremarks" runat="server" Text="<%#Bind('REMARKS')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblremarksx" runat="server" Text="Remarks"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsts" runat="server" Text="<%#Bind('STATUS')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblsts01x" runat="server" Text="Status"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnDeletedocter" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDeletedocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")%>' />
                                                                                <controlstyle cssclass="btn btnred"></controlstyle>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                    <tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnCompletedocter" runat="server" Text="Complete" CssClass="btnselect" OnClick="btnCompletedocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")+","+ Eval("request_id") +","+ Eval("DOC_SEQ_ID")%>' Visible="false" />
                                                                                <controlstyle cssclass="btn btngreen"></controlstyle>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                    <tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnRejectdocter" runat="server" Text="Reject" CssClass="btnDelete" OnClick="btnRejectdocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")+","+ Eval("SEQ_NO")%>' Visible="false" />
                                                                                <controlstyle cssclass="btn btngreen"></controlstyle>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                    <tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                    </Columns>


                                                                    <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                    <RowStyle CssClass="GridviewRowStyle" />
                                                                    <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                </asp:GridView>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </asp:Panel>
                                            </div>

                                              <div class="box-body">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Assign Verifier</label>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtAssignVerifier" runat="server" ToolTip="Assign Verifier" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                &nbsp; &nbsp; &nbsp;
                                             <asp:LinkButton ID="LinkButton1" runat="server" CssClass="LnkbtnPatient" CausesValidation="False" OnClick="LkBtnDoc2_Click">Select</asp:LinkButton>
                                            </div>
                                               </div>

                                            <div class="form-group">
                                                <asp:Panel ID="pnlAssignVerfier" runat="server">
                                                    <div class="box-body">
                                                        <div class="form">
                                                            <asp:HiddenField ID="HiddenField8" runat="server" />
                                                            <div class="table-responsive table--no-card m-b-30">
                                                                <asp:GridView ID="gvassignverifier" runat="server" GridLines="Vertical" RowStyle-Wrap="true"
                                                                    AutoGenerateColumns="False"
                                                                    border-radius="40px"
                                                                    CellPadding="2"
                                                                    ForeColor="#333333"
                                                                    HorizontalAlign="Center"
                                                                    PageSize="10" OnRowDataBound="gvassignverifier_RowDataBound"
                                                                    CssClass="table table-borderless table-striped">
                                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                    <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                                    <Columns>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkbtnpopupDOCIDverifier" runat="server" Text="<%#Bind('EMP_NO')%>" CommandArgument='<%#Eval("EMP_NO")+","+ Eval("UNIQUE_ID")%>'></asp:LinkButton>

                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblid11fverifierz" runat="server" Text="Verifier ID"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblShort_Nameverifier" runat="server" Text="<%#Bind('NAME')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblDescriptionverifierz" runat="server" Text="Verifier Name"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmcr_noverifier" runat="server" Text="<%#Bind('DEPT_DESC')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblmcrverifierz" runat="server" Text="Verifier Department"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

<%--                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmcrverifier" runat="server" Text="<%#Bind('MCR_NO')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblmcrverifierz" runat="server" Text="MCR Number"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>--%>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblseqnoverifier" runat="server" Text="<%#Bind('SEQ_NO')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblseqnoverifierz" runat="server" Text="Sequence Number"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblrejreaverifier" runat="server" Text="<%#Bind('REJ_REASON')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblrejreaverifierz" runat="server" Text="Reject Reason"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblrejreatimeverifier" runat="server" Text="<%#Bind('REJ_TIME_STAMP')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblrejreatimeverifierz" runat="server" Text="Reject TimeStamp"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblremarksverifier" runat="server" Text="<%#Bind('REMARKS')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblremarksverifierz" runat="server" Text="Remarks"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblstsverifier" runat="server" Text="<%#Bind('STATUS')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblsts01verifierx" runat="server" Text="Status"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnDeletedocterverifier" runat="server" Text="Delete" CssClass="btnDelete" OnClick="btnDeletedocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")%>' />
                                                                                <controlstyle cssclass="btn btnred"></controlstyle>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                    <tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnCompletedocterverifier" runat="server" Text="Complete" CssClass="btnselect" OnClick="btnCompletedocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")+","+ Eval("request_id")%>' Visible="false" />
                                                                                <controlstyle cssclass="btn btngreen"></controlstyle>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                    <tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnRejectdocterverifier" runat="server" Text="Reject" CssClass="btnDelete" OnClick="btnRejectdocter_Click" CommandArgument='<%#Eval("UNIQUE_ID")+","+ Eval("VERIFY_REF")+","+ Eval("SEQ_NO")%>' Visible="false" />
                                                                                <controlstyle cssclass="btn btngreen"></controlstyle>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #1ca5b8; border-collapse: collapse;">
                                                                                    <tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <HeaderStyle CssClass="GridHeaderTextforScroll-UnText" />
                                                                    <RowStyle CssClass="GridviewRowStyle" />
                                                                    <AlternatingRowStyle CssClass="GridviewAlternatingRowStyle alternativeFontColor" />
                                                                </asp:GridView>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                      
                                        <!-- Doctor Search Popup start -->
            <asp:UpdatePanel ID="updtpnldocterselection" runat="server" Visible="false">
                <ContentTemplate>

                    <div>
                        <tr>
                            <td>
                                 <asp:LinkButton ID="lnkbtndoctorselection" runat="server" />
                                    <cc1:ModalPopupExtender ID="mdlpopupdoctorselection" runat="server" BackgroundCssClass="modal-background"
                                        DynamicControlID="lnkbtndoctorselection" PopupControlID="pnldoctorselection" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="lnkbtndoctorselection" />
                                 <div class="table-responsive table--no-card m-b-30">
                                    <asp:Panel ID="pnldoctorselection" runat="server">
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
                                                                        <li class="" style="background-color: #e3f6fd; border-radius: 5px;"><a href="#tabPrd_1" data-toggle="tab"
                                                                            style="font-weight: 500; font-size: 15px; color: black">
                                                                            <asp:Label runat="server" ID="lbldoctorselectionTilte"  ></asp:Label>
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
                                                            <asp:ImageButton ID="btndoctorselectionpopupnew" runat="server" Height="28px" ImageUrl="Images/MenuTemplatesImg/NEW_MENU.png"
                                                                Width="28px" ToolTip="New Record" OnClick="btndoctorselectionpopupnew_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                 <asp:ImageButton ID="btndoctorselectionsearch" runat="server" Height="28px" ImageUrl="Images/MenuTemplatesImg/CLEAR_MENU.png"
                                                                                     Width="28px" ToolTip="Search Record" OnClick="btndoctorselectionsearch_Click" />
                                                        </div>
                                                        <asp:Panel ID="pnlverselection" runat="server">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                            </div>
                                                            <div class="col-sm-2">                                                                
                                                            <asp:RadioButton ID="rbtndoctor" OnCheckedChanged="rbtndoctor_CheckedChanged" AutoPostBack="true" runat="server"   GroupName="VERSELECTION"/>&nbsp; Doctor
                                                            </div>
                                                              <div class="col-sm-4">                                                                
                                                            <asp:RadioButton ID="rbtndepsec" runat="server" OnCheckedChanged="rbtndepsec_CheckedChanged" AutoPostBack="true"  GroupName="VERSELECTION"/>&nbsp; Department Secretary
                                                            </div>
                                                               <div class="col-sm-2">                                                                
                                                            <asp:RadioButton ID="rbtnhims" runat="server" OnCheckedChanged="rbtnhims_CheckedChanged" AutoPostBack="true" GroupName="VERSELECTION"/>&nbsp; HIMS
                                                            </div>
                                                        </div>
                                                            <br />
                                                            </asp:Panel>

                                                          <asp:Panel ID="pnlDepartmentOU" runat="server">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-3 control-label">Department OU </label>
                                                            <div class="col-sm-5">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlDepartmentOUDoctersel" runat="server" CssClass="form-control" AutoPostBack="false">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                              </asp:Panel>
                                                 <asp:Panel ID="pnlMCRNumber" runat="server">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                            </div>
                                                            <label for="inputEmail3" class="col-sm-3 control-label">MCR Number </label>
                                                            <div class="col-sm-5">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtMCRNumberdocselection" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return DOClistenter1_click(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                   </asp:Panel>

                                                         <asp:Panel ID="pnlDocempno" runat="server">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                            </div>
                                                         <asp:Label Text="Doctor Employee Number" ID="lbldoctorEmployeeNo" runat="server" class="col-sm-3 control-label"/> 
                                                                   <div class="col-sm-5">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtdocempnumdocselection" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return DOClistenter1_click(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                                </asp:Panel>

                                                        <asp:Panel ID="pnlSecretaryname" runat="server">
                                                          <div class="form-group">
                                                            <div class="col-sm-2">
                                                            </div>
                                                           <asp:Label Text="Secretary Name" ID="lblSecretaryName" runat="server" class="col-sm-3 control-label"/>  
                                                                 <div class="col-sm-5">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtSecretaryname" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return DOClistenter1_click(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                         </div>
                                                               </asp:Panel>

                                                        <asp:Panel ID="pnlDoctorname" runat="server">
                                                         <div class="form-group">
                                                            <div class="col-sm-2">
                                                            </div>
                                                                 <asp:Label Text="Doctor Name" ID="lblDoctorName" runat="server" class="col-sm-3 control-label"/>   
                                                             <div class="col-sm-5">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtDoctorNamedocselection" runat="server" CssClass="form-control" ReadOnly="false" onkeypress="return DOClistenter1_click(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                           </asp:Panel>
                                                   
                                                    <div class="box-body">
                                                        <div class="form">
                                                            <asp:HiddenField ID="HiddenField9" runat="server" />
                                                            <div class="table-responsive table--no-card m-b-30">
                                                                <asp:GridView ID="gvlistdoctorselectionpopup" runat="server" GridLines="Vertical" RowStyle-Wrap="true" AutoGenerateColumns="false"
                                                                    CssClass="table table-borderless table-striped table-earning"  OnRowDataBound="gvlistdoctorselectionpopup_RowDataBound">

                                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                                    <PagerStyle CssClass="GridviewPagerStyle gridview" ForeColor="White" HorizontalAlign="Center" />
                                                                    <Columns>


                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblEMP_NO" runat="server" Text="<%#Bind('EMP_NO')%>" CommandArgument='<%#Eval("EMP_NO")+","+ Eval("Short_Name") +","+ Eval("MCR_NO")+","+ Eval("DEPT_DESC")%>' OnClick="lnkbtnpopupDOCID_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #F8F8F8; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblEMP_NOr" runat="server" ForeColor="White" Text="Doctor Employee Number."></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblShort_Name" runat="server" Text="<%#Bind('Short_Name')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #F8F8F8; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblDescription" runat="server" ForeColor="White" Text="Doctor Name"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmcrno" runat="server" Text="<%#Bind('MCR_NO')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #F8F8F8; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblmcrnoon" runat="server" ForeColor="White" Text="MCR No"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmcr_no" runat="server" Text="<%#Bind('DEPT_DESC')%>"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <table style="padding: 0px; margin: 0px; width: 100%; background-color: #F8F8F8; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td class="gridtabletd">
                                                                                            <asp:Label ID="lblmcr" runat="server" ForeColor="White" Text="Department Name"></asp:Label>
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
                                                        <asp:Repeater ID="Repeater5" runat="server">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkPagedoctorselectionPopup" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                    Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPagedoctorselectionPopup_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                    <div class="modal-footer" style="text-align: center;">
                                                        <asp:Button runat="server" ID="Button1" OnClientClick="showTabs('btnpatientprofile')" Text="Cancel" CssClass="btn btnred" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- Doctor Search Popup End -->
                                    </asp:Panel>

                                    <!-- panel AssignDoctor & Verifier End -->
                                    <ul class="list-unstyled list-inline pull-right">
                                        <li>
                                            <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px" onclick="showpanel1()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button></li>
                                        <li>
                                            <button type="button" class="btn btngreen next-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px" onclick="showpanel3()">Next<i class="fa fa-chevron-right" style="margin-left: 10px;"></i></button>
                                    </ul>
                                </asp:Panel>
                                <%--  for TAb 3 informations END--%>

                                <%--  for TAb 4 informations starts--%>
                                <asp:Panel ID="pnlProcessHistory" runat="server">
                                    <div class="box box-primary box-solid" style="border-radius: 33px; border: 1px dashed #2aa7ed;">
                                        <div class="box-header with-border TotalRecord">
                                            <div class="box-title">
                                                <label class="text-right" style="color: white">Total Record : <span id="lblpnlprocesshistory" runat="server"></span></label>
                                            </div>
                                        </div>
                                        <div class="box-body">
                                            <div class="form">
                                                <asp:HiddenField ID="HiddenField11" runat="server" />
                                                <div class="table-responsive table--no-card m-b-30">
                                                    <asp:GridView ID="gvprocesshistorygrid" runat="server" GridLines="Vertical" RowStyle-Wrap="true" OnRowDataBound="gvprocesshistorygrid_RowDataBound"
                                                        AutoGenerateColumns="False"
                                                        CellPadding="2"
                                                        ForeColor="#333333"
                                                        HorizontalAlign="Center"
                                                        PageSize="10"
                                                        CssClass="table table-borderless table-striped">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPenSeq_ID" runat="server" Text="<%#Bind('SEQ_ID')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #21BACF; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnpendingshortname" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Seq No" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="SEQ_ID"></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblpendingshortname" runat="server" Text="<%#Bind('TASK_SHORT_NAME')%>" CommandArgument='<%#Eval("SEQ_ID")%>'>
                                           
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #21BACF; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnpendingshortname" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Task Name" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="TASK_SHORT_NAME"></asp:LinkButton>
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

                                                                    <asp:Label ID="lblDueDays" runat="server" Text="<%#Bind('SUB_TASK')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnDueDays" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Sub Task" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="SUB_TASK"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblClosedte" runat="server" Text='<%#Bind("START_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Task Start Date" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="START_DATE"></asp:LinkButton>
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

                                                                    <asp:Label ID="lblDaysAllowed" runat="server" Text="<%#Bind('DUE_DAYS')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnDueDays" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Days Allowed" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="DUE_DAYS"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblEND_DATE" runat="server" Text='<%#Bind("END_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Task Due Date" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="END_DATE"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblCOMPLETED_DATE" runat="server" Text='<%#Bind("COMPLETED_DATE", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Completed Date" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="COMPLETED_DATE"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblTRANS_STATUS" runat="server" Text="<%#Bind('TRANS_STATUS')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnprocesssts6014" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Pending Status" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="TRANS_STATUS"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblMODIFIED_BY" runat="server" Text="<%#Bind('MODIFIED_BY')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnMODIFIED_BY" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Modified User" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="MODIFIED_BY"></asp:LinkButton>
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
                                                                    <asp:Label ID="lblRemarks" runat="server" Text="<%#Bind('Remarks')%>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <table style="padding: 0px; margin: 0px; width: 100%; background-color: #00b7d4; border-collapse: collapse;">
                                                                        <tr>
                                                                            <td class="gridtabletd">
                                                                                <asp:LinkButton ID="lnkbtnRemarks" runat="server" Font-Underline="false" Font-Bold="true"
                                                                                    Text="Remarks" Font-Names="Arial"  ForeColor="White"
                                                                                    CommandArgument="Remarks"></asp:LinkButton>
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
                                            <div class="row form-group">
                                                <div class="col-sm-12" style="text-align: center">
                                                    <asp:Repeater ID="rptPagerProcessHistory" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkPageProcessHistory" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                Class='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="lnkPageProcessHistory_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <ul class="list-unstyled list-inline pull-right">
                                        <li>
                                            <button type="button" class="btn btnyellow prev-step" style="font-size: 15px; line-height: 37px; padding: 0px 15px; border-radius: 10px" onclick="showpanel2()"><i class="fa fa-chevron-left" style="margin-right: 10px"></i>Back</button></li>

                                    </ul>
                                </asp:Panel>
                                <%--  for TAb 4 informations END--%>

                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>

               </div>

                <!-- Update doctor Popup start -->
                                <asp:Panel runat="server" ID="pnlupdateremarksandprocess" Visible="false">
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
                                                                <table style="width: 98%; text-align: center" bgcolor="#f4eaff">
                                                                    <tr>
                                                                        <td style="width: 12%"></td>
                                                                        <td align="center">
                                                                            <asp:Label Style="text-align: center" ID="lblreject" runat="server" Text="" ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" style="height: 96px">
                                                                            <table style="width: 100%">
                                                                            </table>
                                                                            <asp:Panel runat="server"> 
                                                                                <table style="width: 100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <label>Reject Reason:&nbsp;&nbsp;</label></td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtProcessCompletedRemarks" MaxLength="50" runat="server" CssClass="form-control" Visible="true"></asp:TextBox></td>
                                                                                            <span hidden><asp:TextBox ID="txtsno" MaxLength="50" runat="server" CssClass="form-control" Visible="true"></asp:TextBox></td></span>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 5%"></td>
                                                                                        <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt;">Are you sure want to update the process?</td>
                                                                                        <td style="width: 12%"></td>
                                                                                    </tr>
                                                                                </table>   
                                                                             </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" style="text-align: center">
                                                                            <asp:Button ID="btnConfirmprocessStatus" runat="server" align="right" CssClass="btn btngreen"
                                                                                Text="Yes" OnClick="btnConfirmprocessStatus_Click" />
                                                                            <asp:Button ID="btnConfirmprocessClose" runat="server" align="right" CssClass="btn btnred"
                                                                                Text="NO" OnClick="btnConfirmprocessClose_Click" />
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
                                </asp:Panel>
                                <!-- Update doctor Popup End -->


            <!-- Update Process Popup start -->
            <asp:Panel runat="server" ID="pnlHIMS">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelModalHIMS" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="btnerrorHIMS" runat="server" />
                                    <cc1:ModalPopupExtender ID="ModelpopuperrorHIMS" runat="server" BackgroundCssClass="modal-background "
                                        DynamicControlID="btnerrorHIMS" PopupControlID="pnlpopuperrorHIMS" DynamicServiceMethod="GetContextKeyGroupHistory" DynamicServicePath="~/WebService.asmx" RepositionMode="RepositionOnWindowResizeAndScroll"
                                        TargetControlID="btnerrorHIMS" />
                                    <asp:Panel ID="pnlpopuperrorHIMS" runat="server" BackColor="#e3f6fd"
                                        EnableTheming="True" Style="text-align: center; resize: none; align-content: flex-start; align-items: flex-start; border: 5px solid #2aa7ed; border-radius: 20px; padding: 10px; " Width="447px">
                                        <center>
                                            <table style="width: 98%; text-align: center" bgcolor="#f4eaff">
                                                <tr>
                                                    <td style="width: 12%"></td>
                                                    <td align="center">
                                                        <asp:Label Style="text-align: center" ID="Label1" runat="server" Text="Update Process" ForeColor="#212529" Font-Bold="True" Font-Names="Arial" Font-Size="20pt"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 96px">
                                                        <table style="width: 100%">
                                                        </table>
                                                        <asp:Panel ID="Panel3" runat="server" Height="67px">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                   
                                                                    <tr>
                                                                        <td style="width: 5%"></td>
                                                                        <td style="font-family: Arial, Helvetica, sans-serif; font-weight: bold; color: orangered; font-size: 12pt;">
                                                                            <asp:Label Text="Are you sure want to update the process?" ID="lblupdateprocesscontent" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <td style="width: 12%"></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align: center">
                                                        <asp:Button ID="btnupdateHIMS" runat="server" align="right" CssClass="btn btngreen"
                                                            Text="YES" OnClick="btnupdateHIMS_Click" />
                                                        <asp:Button ID="btncancelHIMS" runat="server" align="right" CssClass="btn btnred"
                                                            Text="NO" OnClick="btncancelHIMS_Click" />
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
            </asp:Panel>
            <!-- Update Process Popup End -->

                                <!-- Error Popup start -->
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
                                <!-- Error Popup End -->

        </ContentTemplate>
    </asp:UpdatePanel>
    <script>

        /*window.onload = showpanel*/
        const root = document.querySelector(":root"); //grabbing the root element

        root.style.setProperty("--tab1", '#2aa7ed');
        root.style.setProperty("--tab1FontColor", 'white');

        function showpanel() { // This code for showtabs time

            const root = document.querySelector(":root"); //grabbing the root element

            root.style.setProperty("--tab1", '#2aa7ed');
            root.style.setProperty("--tab2", '#DFDFDF');
            root.style.setProperty("--tab3", '#DFDFDF');
            root.style.setProperty("--tab4", '#DFDFDF');

            root.style.setProperty("--tab1FontColor", 'white');
            root.style.setProperty("--tab2FontColor", '#706F6F');
            root.style.setProperty("--tab3FontColor", '#706F6F');
            root.style.setProperty("--tab4FontColor", '#706F6F');

            document.getElementById("<%=btnInfoRequest.ClientID%>").click();
        }
        function showpanel1() {
            const root = document.querySelector(":root"); //grabbing the root element

            root.style.setProperty("--tab1", '#DFDFDF');
            root.style.setProperty("--tab2", '#2aa7ed');
            root.style.setProperty("--tab3", '#DFDFDF');
            root.style.setProperty("--tab4", '#DFDFDF');

            root.style.setProperty("--tab1FontColor", '#706F6F');
            root.style.setProperty("--tab2FontColor", 'white');
            root.style.setProperty("--tab3FontColor", '#706F6F');
            root.style.setProperty("--tab4FontColor", '#706F6F');

            document.getElementById("<%=btnAttachment.ClientID%>").click();
}
function showpanel2() {

    const root = document.querySelector(":root"); //grabbing the root element

    root.style.setProperty("--tab1", '#DFDFDF');
    root.style.setProperty("--tab2", '#DFDFDF');
    root.style.setProperty("--tab3", '#2aa7ed');
    root.style.setProperty("--tab4", '#DFDFDF');

    root.style.setProperty("--tab1FontColor", '#706F6F');
    root.style.setProperty("--tab2FontColor", '#706F6F');
    root.style.setProperty("--tab3FontColor", 'white');
    root.style.setProperty("--tab4FontColor", '#706F6F');

    document.getElementById("<%=btnSelectAction.ClientID%>").click();
}

function showpanel3() {

    const root = document.querySelector(":root"); //grabbing the root element

    root.style.setProperty("--tab1", '#DFDFDF');
    root.style.setProperty("--tab2", '#DFDFDF');
    root.style.setProperty("--tab3", '#DFDFDF');
    root.style.setProperty("--tab4", '#2aa7ed');

    root.style.setProperty("--tab1FontColor", '#706F6F');
    root.style.setProperty("--tab2FontColor", '#706F6F');
    root.style.setProperty("--tab3FontColor", '#706F6F');
    root.style.setProperty("--tab4FontColor", 'white');

    document.getElementById("<%=btnProcessHistory.ClientID%>").click();
}

    </script>

     
    
<script type="text/javascript">
    function validateCategory() {
        var ddlCategory = document.getElementById('<%= ddlCategory.ClientID %>');
        var fileUpload = document.getElementById('<%= FileUpload1.ClientID %>');

        if (ddlCategory.value === '' && fileUpload.value === '') {
            bootbox.alert('Please select a File for Attachment and Please Choose a Category.');
            return false;
        }

        if (ddlCategory.value === '') {
            bootbox.alert('Please select a category');
            return false;
        }

        if (fileUpload.value === '') {
            bootbox.alert('Please select a File for Attachment');
            return false;
        }

        return true;
    }

    function btnCancelClicked() {
        bootbox.dialog({
            size: 'small',
            message: "Do you want to proceed to the next process?",
            buttons: {
                success: {
                    label: "Ok",
                    className: "btn-success",
                    callback: function () {
                        document.getElementById("<%=btnCancelTrigger.ClientID%>").click();
                        }
                    },
                    danger: {
                        label: "Cancel",
                        className: "btn-danger"
                    }
                }
            });
     }
</script>
</asp:Content>
