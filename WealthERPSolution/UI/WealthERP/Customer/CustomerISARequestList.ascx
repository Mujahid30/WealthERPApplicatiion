<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerISARequestList.ascx.cs"
    Inherits="WealthERP.Customer.CustomerISARequestList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%" class="TableBackground">
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            ISA Status
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="20px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <br />
            <asp:Label ID="lblbranch" runat="server" Font-Bold="true" Font-Size="Small" CssClass="FieldName"
                Text="Branch: "></asp:Label>
            <asp:DropDownList ID="ddlBMBranch" runat="server" OnSelectedIndexChanged="ddlBMBranch_SelectedIndexChanged"
                CssClass="cmbField" Style="vertical-align: middle" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trErrorMessage" align="center" style="width: 100%" runat="server">
        <td align="center" style="width: 100%">
            <div class="failure-msg" style="text-align: center" align="center">
                No ISAQueue Records found!!!!
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <br />
            <telerik:RadGrid ID="gvISArequest" runat="server" CssClass="RadGrid" GridLines="None"
                OnNeedDataSource="gvISArequest_OnNeedDataSource" Width="100%" AllowPaging="True"
                PageSize="10" AllowSorting="True" AutoGenerateColumns="false" ShowStatusBar="true"
                AllowAutomaticDeletes="True" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                Skin="Telerik" EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true"
                AllowFilteringByColumn="true">
                <exportsettings hidestructurecolumns="false" exportonlydata="true" filename="ISAQueuelist">
                </exportsettings>
                <mastertableview commanditemdisplay="None" datakeynames="AISAQ_RequestQueueid,WWFSM_StepCode,StatusCode">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="ISA Request Id" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Width="80px" AllowFiltering="true" DataField="AISAQ_RequestQueueid"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkRQ" runat="server" CssClass="CmbField" OnClick="LnkRQ_Click"
                                    Text='<%#Eval("AISAQ_RequestQueueid") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="AISAQ_Priority" AllowFiltering="false" HeaderStyle-Width="70px"
                            HeaderText="Priority" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AISAQ_date" AllowFiltering="false" HeaderStyle-Width="130px"
                            HeaderText="Request Date/Time">
                            <ItemStyle Width="" Wrap="false" VerticalAlign="Top" HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CustomerName" AllowFiltering="true" HeaderStyle-Width="90px"
                            HeaderText="Customer" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%-- <telerik:GridBoundColumn DataField="RequestTime" AllowFiltering="false" HeaderStyle-Width="70px"
                            HeaderText="Request Time">
                            <ItemStyle Width="" Wrap="false" VerticalAlign="Top" HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="AISAQ_Status" AllowFiltering="true" AutoPostBackOnFilter="true"
                            ShowFilterIcon="False" HeaderStyle-Width="90px" HeaderText="Status">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="StepName" AllowFiltering="false" HeaderStyle-Width="120px"
                            HeaderText="Current Stage">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BranchName" AllowFiltering="false" HeaderStyle-Width="110px"
                            HeaderText="Branch" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AISAQ_ProcessedDate" AllowFiltering="false" HeaderStyle-Width="90px"
                            HeaderText="Process Date/Time " FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CISAA_AccountNumber" AllowFiltering="false" HeaderStyle-Width="90px"
                            HeaderText="ISA No." FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </mastertableview>
                <clientsettings reordercolumnsonclient="True" allowcolumnsreorder="True" enablerowhoverstyle="true">
                    <Resizing AllowColumnResize="true" />
                    <Selecting AllowRowSelect="true" />
                </clientsettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
