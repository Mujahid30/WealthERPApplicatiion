<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedMFFolioStaging.ascx.cs" Inherits="WealthERP.SuperAdmin.RejectedMFFolioStaging" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>


<script type="text/javascript" src="../Scripts/JScript.js"></script>

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
        window.open('Uploads/MapToCustomers.aspx?Folioid=' + folioId + '', 'mywindow', 'width=550,height=450,scrollbars=yes,location=no')
        return true;
    }
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvCAMSProfileReject.Rows.Count %>');
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

<asp:Panel ID="pnl" DefaultButton="btnGridSearch" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table style="width: 100%" class="TableBackground">
        <tr>
            <td>
                
               <asp:Label Text="Mutual Fund Folio Rejects" ID="lblHeader" CssClass="HeaderTextBig" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                    OnClick="lnkBtnBack_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="leftField">
            <asp:LinkButton ID="LinkInputRejects" runat="server" 
                Text="View Input Rejects" CssClass="LinkButtons" 
                onclick="LinkInputRejects_Click"></asp:LinkButton>
                <%--<asp:LinkButton runat="server" ID="LinkButton1" CssClass="LinkButtons" Text="Back"
                    OnClick="lnkProfile_Click"></asp:LinkButton>--%>
                <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvCAMSProfileReject" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ShowFooter="true" CssClass="GridViewStyle" AllowSorting="true"
                    DataKeyNames="MFFolioStagingId,MainStagingId,ProcessID" >
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
                                <asp:Label ID="lblAdviserName" runat="server" Text="Adviser"></asp:Label>
                                <asp:DropDownList ID="ddlAdviserName" AutoPostBack="true" CssClass="cmbField"
                                    runat="server" OnSelectedIndexChanged="ddlAdviserName_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAdviserNameDate" runat="server" Text='<%# Eval("A_OrgName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="true"></HeaderStyle>
                        <ItemStyle Wrap="true"></ItemStyle>
                    </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrProcessId" runat="server" Text="Process Id"></asp:Label>
                                <asp:DropDownList ID="ddlProcessId" AutoPostBack="true" CssClass="GridViewCmbField" runat="server" 
                                OnSelectedIndexChanged="ddlProcessId_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessId").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <%--<asp:BoundField DataField="ProcessID" HeaderText="ProcessId" />--%>
                        <%--<asp:BoundField DataField="WERPCUstomerName" HeaderText="WERP Name" SortExpression="WERPCUstomerName" />--%>
                        <%--<asp:BoundField DataField="CustomerExists" HeaderText="Is Customer Existing" />--%>
                        <%--<asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblCustomerExists" runat="server" Text="Is Customer Existing"></asp:Label>
                                <asp:DropDownList ID="ddlCustomerExists" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCustomerExists_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerExistsHeader" runat="server" Text='<%# Eval("CustomerExists").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                <%--<asp:TextBox ID="txtNameSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedCAMSProfile_btnGridSearch');" />--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNameHeader" Width="180px" runat="server" Text='<%# Eval("InvName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="NAME" HeaderText="Name" />--%>
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
                        <%--<asp:BoundField DataField="AMC" HeaderText="AMC" />--%>
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
                </asp:GridView>
                
               </td>
               </tr>
               </table>
             </asp:Panel>
        <tr id="trReprocess" runat="server">
            <td class="SubmitCell">
                <asp:Button ID="btnReprocess" OnClick="btnReprocess_Click" runat="server" Text="Reprocess"
                    CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RejectedMFFolio_btnReprocess','L');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RejectedMFFolio_btnReprocess','L');" />
                <asp:Button ID="btnMapToCustomer" runat="server" CssClass="PCGLongButton" Text="Map to Customer"
                    OnClientClick="return ShowPopup()" Visible="false"/>
                <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" 
                 OnClick="btnDelete_Click" Text="Delete Records" />
            </td>
        </tr>
        <tr id="trProfileMessage" runat="server" visible="false">
            <td class="Message">
                <asp:Label ID="lblEmptyMsg" class="FieldName" runat="server" Text="There are no records to be displayed!">
                </asp:Label>
            </td>
        </tr>
        <tr id="trErrorMessage" runat="server" visible="false">
            <td class="Message">
                <asp:Label ID="lblError" CssClass="HeaderTextBig" runat="server">
                </asp:Label>
            </td>
        </tr>
 
    <div id="DivPager" runat="server" style="display: none">
        <table style="width: 100%">
            <tr align="center">
                <td>
                    <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnGridSearch" runat="server" Text="" OnClick="btnGridSearch_Click"
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
    <asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnAdviserFilter" runat="server" Visible="false" />

