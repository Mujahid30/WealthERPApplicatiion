<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedMFFolio.ascx.cs"
    Inherits="WealthERP.Uploads.RejectedMFFolio" %>


<script type="text/javascript" src="../Scripts/JScript.js"></script>
<asp:ScriptManager  runat="server"></asp:ScriptManager>

<script language="javascript" type="text/javascript">
    function checkAllFPBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvCAMSProfileReject.Items.Count %>');
        var gvAssociation = document.getElementById('<%= gvCAMSProfileReject.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkBx";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBxAll");
     
            if (mainChkBox.checked == false) {
                mainChkBox.checked = true;

            }
            else {
                mainChkBox.checked = false;
            }

       

        //get an array of input types in the gridview
        var inputTypes = gvAssociation.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template            
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    } 


    </script>

<script>
    function ShowPopup() {
        var form = document.forms[0];
        var folioId = "";
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                    hiddenField = form.elements[i].id.replace("chkBx", "hdnchkBx");
                    hiddenFieldValues = document.getElementById(hiddenField).value;
                    var splittedValues = hiddenFieldValues.split("-");
                    if (count == 1) {
                        folioId = splittedValues[0];
                    }
                    else {
                        folioId = folioId + "~" + splittedValues[0];
                    }
                    RejectReasonCode = splittedValues[1];
                }
            }
        }
        //        if (count > 1) {
        //            alert("You can select only one record at a time.")
        //            return false;
        //        }
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
        window.open('Uploads/MapToCustomers.aspx?Folioid=' + folioId + '', '_blank', 'width=550,height=450,scrollbars=yes,location=no')
        return false;
    }
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause

        var gvControl = document.getElementById('<%= gvCAMSProfileReject.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkBx";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBxAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

<script language="javascript" type="text/javascript">

    //    Function to call btnReprocess_Click method to refresh user control

    function Reprocess() {
        
        document.getElementById('<%= btnReprocess.ClientID %>').click();
    }
    </script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Mutual Fund Folio Rejects
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReprocessComplete" runat="server" class="success-msg" align="center"
                visible="false">
                Reprocess successfully Completed
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReprocessincomplete" runat="server" class="failure-msg" align="center"
                visible="false">
                Reprocess Failed!
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td class="leftField">
            <asp:LinkButton ID="LinkInputRejects" runat="server" Text="View Input Rejects" CssClass="LinkButtons"
                OnClick="LinkInputRejects_Click"></asp:LinkButton>
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                    OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
            </td>
        </tr>
        <%--<tr>
            <td id="trSelectAllFPGrid" runat="server" colspan="2" style="padding-left: 8px">
                <input id="chkSelectAllpages" name="Select All across pages" value="Customer" type="checkbox"
                    onclick="checkAllFPBoxes();" />
                 <asp:CheckBox ID="chkSelectAllpages" runat="server" CssClass="FieldName" Text="Select All across pages"/>
                <asp:Label ID="lblAllpages" class="Field" Text="Select all across pages" runat="server"></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Panel ID="pnlMFPortfolioHoldings" runat="server">
                    <telerik:RadGrid ID="gvCAMSProfileReject" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false" ExportSettings-FileName="MF Folio Reject Details"
                        OnNeedDataSource="gvCAMSProfileReject_OnNeedDataSource">
                        <ExportSettings HideStructureColumns="true">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="MFFolioStagingId,MainStagingId,ProcessID" Width="100%"
                            AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                                    <HeaderTemplate>
                                        <input id="chkBxAll" name="chkBxAll" type="checkbox" onclick="checkAllBoxes()" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBx" runat="server" />
                                        <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("MFFolioStagingId").ToString() + "-" +  Eval("RejectReasonCode").ToString()%>' />
                                        <asp:HiddenField ID="hdnBxProcessID" runat="server" Value='<%# Eval("ProcessID").ToString() %>' />
                                        <asp:HiddenField ID="hdnBxStagingId" runat="server" Value='<%# Eval("MFFolioStagingId").ToString() %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnSave" CssClass="FieldName" OnClick="btnSave_Click" runat="server"
                                            Text="Save" />
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="RejectReason" DataField="RejectReason" UniqueName="RejectReason"
                                    SortExpression="RejectReason" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProcessId" DataField="ProcessId" UniqueName="ProcessId"
                                    SortExpression="ProcessId" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="InvName" DataField="InvName" UniqueName="InvName"
                                    SortExpression="InvName" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="PANNumber" HeaderText="PAN Number" SortExpression="PANNumber"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPan" runat="server" CssClass="txtField" Text='<%# Bind("PANNumber") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtPanMultiple" CssClass="txtField" runat="server" />
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Folio" DataField="Folio" SortExpression="Folio"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFolio" CssClass="txtField" runat="server" Text='<%# Bind("Folio") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtFolioMultiple" CssClass="txtField" runat="server" />
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="IsRejected" DataField="IsRejected" UniqueName="IsRejected"
                                    SortExpression="IsRejected" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <%--<asp:GridView ID="gvCAMSProfileReject" runat="server" AutoGenerateColumns="False"
                        Width="100%" CellPadding="4" ShowFooter="true" CssClass="GridViewStyle" AllowSorting="true"
                        DataKeyNames="MFFolioStagingId,MainStagingId,ProcessID">
                        <FooterStyle CssClass="FooterStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                    <input id="chkBxAll" name="chkBxAll" type="checkbox" onclick="checkAllBoxes()" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBx" runat="server" />
                                    <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("MFFolioStagingId").ToString() + "-" +  Eval("RejectReasonCode").ToString()%>' />
                                    <asp:HiddenField ID="hdnBxProcessID" runat="server" Value='<%# Eval("ProcessID").ToString() %>' />
                                    <asp:HiddenField ID="hdnBxStagingId" runat="server" Value='<%# Eval("MFFolioStagingId").ToString() %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnSave" CssClass="FieldName" OnClick="btnSave_Click" runat="server"
                                        Text="Save" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblRejectReason" runat="server" Text="Reject Reason"></asp:Label>
                                    <asp:DropDownList ID="ddlRejectReason" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRejectReasonHeader" runat="server" Text='<%# Eval("RejectReason").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHdrProcessId" runat="server" Text="Process Id"></asp:Label>
                                    <asp:DropDownList ID="ddlProcessId" AutoPostBack="true" CssClass="GridViewCmbField"
                                        runat="server" OnSelectedIndexChanged="ddlProcessId_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessId").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
          
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNameHeader" Width="180px" runat="server" Text='<%# Eval("InvName").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                   
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblPan" runat="server" Text="PAN Number"></asp:Label>
                                    <asp:TextBox ID="txtPanSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedCAMSProfile_btnGridSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPan" runat="server" CssClass="txtField" Text='<%# Bind("PANNumber") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPanMultiple" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblFolio" runat="server" Text="Folio"></asp:Label>
                                    <asp:TextBox ID="txtFolioSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedCAMSProfile_btnGridSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFolio" CssClass="txtField" runat="server" Text='<%# Bind("Folio") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFolioMultiple" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                         
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblIsRejected" runat="server" Text="Is Rejected"></asp:Label>
                                    <asp:DropDownList ID="ddlIsRejected" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlIsRejected_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIsRejectedHeader" runat="server" Text='<%# Eval("IsRejected").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>--%>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
<table width="100%">
    <tr id="trReprocess" runat="server">
        <td class="SubmitCell">
            <asp:Button ID="btnReprocess" OnClick="btnReprocess_Click" runat="server" Text="Reprocess"
                CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RejectedMFFolio_btnReprocess','L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RejectedMFFolio_btnReprocess','L');" />
            <asp:Button ID="btnMapToCustomer" runat="server" CssClass="PCGLongButton" Text="Map to Customer"
                OnClientClick="return ShowPopup()" />
            <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" OnClick="btnDelete_Click"
                Text="Delete Records" />
        </td>
    </tr>
    <tr id="trProfileMessage" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblEmptyMsg" class="FieldName" runat="server" Text="There are no records to be displayed!">
            </asp:Label>
        </td>
    </tr>
   <%-- <tr id="trErrorMessage" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Message" runat="server">
            </asp:Label>
        </td>
    </tr>--%>
</table>
<%--<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>--%>
<%--<asp:Button ID="btnGridSearch" runat="server" Text="" OnClick="btnGridSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSortProcessID" runat="server" Value="WERPCUstomerName ASC" />
<asp:HiddenField ID="hdnPANFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnIsCustomerExistingFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnIsRejectedFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />--%>
