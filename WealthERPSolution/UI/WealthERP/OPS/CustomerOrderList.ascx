<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerOrderList.ascx.cs" Inherits="WealthERP.OPS.CustomerOrderList" %>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvOrderList.Rows.Count %>');
        var gvControl = document.getElementById('<%= gvOrderList.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "cbRecons";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBxWerpAll");

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
<table width="100%" class="TableBackground">
            <tr>
                <td class="HeaderCell">
                    <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Order Approvals"></asp:Label>
                    <hr />
                </td>
            </tr>
        </table>
        <table width="60%">
        <tr>
        <td align="right">
        <asp:Label ID="lblAssetType" runat="server" CssClass="FieldName" Text="Asset Type :"></asp:Label>
        </td>
        <td align="left">
        <asp:Label ID="lblMF" runat="server" CssClass="FieldName" Text="Mutual Fund"></asp:Label>
        </td>
        <td align="right">
        <asp:Label ID="lblOrderType" runat="server" CssClass="FieldName" Text="Order Type: "></asp:Label>
        </td>
        <td>
        <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="cmbField" AutoPostBack="true"
                onselectedindexchanged="ddlOrderType_SelectedIndexChanged">
        <asp:ListItem Text="Pending Order" Value=1 Selected="True"></asp:ListItem>
        <asp:ListItem Text="Approved" Value=0></asp:ListItem>
        </asp:DropDownList>
        </td>
        </tr>
        
        
        <tr><td colspan="4"></td></tr>
        <tr>
        <td colspan="4">
    <table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
    <td align="center" >
    <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
    </div>
    </td>
    </tr>
 </table>
        </td>
        </tr>
        <tr>
        <td colspan="4">
        <table width="100%">
         <tr>
        <td>
            <asp:GridView ID="gvOrderList" runat="server" AutoGenerateColumns="False"  AllowSorting="True" 
                CellPadding="4" CssClass="GridViewStyle" DataKeyNames="CMOT_MFOrderId" OnRowCommand="gvOrderList_RowCommand" ShowFooter="True" >
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                                         <HeaderTemplate>
                                         <asp:Label ID="lblchkBxSelect" runat="server" Text="Select<br />"></asp:Label>
                                         <input id="chkBxWerpAll" name="chkBxWerpAll" type="checkbox" onclick="checkAllBoxes()" />
                                         </HeaderTemplate>
                                            <ItemTemplate>
                                              <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                                            </ItemTemplate>
                     </asp:TemplateField>  
                    <%--<asp:BoundField DataField="CMOT_MFOrderId" HeaderText="OrderId" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />--%>
                    <asp:ButtonField DataTextField="CMOT_MFOrderId" CommandName="ViewOrderDetails" ButtonType="Link"
                            HeaderText="OrderId" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="PASP_SchemePlanName" HeaderText="Scheme" ItemStyle-Wrap="false" >
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                     </asp:BoundField>
                    <asp:BoundField DataField="WMTT_TransactionClassificationName" HeaderText="Transaction Type"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <HeaderStyle HorizontalAlign="Center" />
                     <ItemStyle HorizontalAlign="left"></ItemStyle>
                     </asp:BoundField>
                    <asp:BoundField DataField="CMOT_OrderDate" HeaderText="Order Date" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"  DataFormatString="{0:d}">
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CMOT_Amount" HeaderText="Amount" ItemStyle-Wrap="false" DataFormatString="{0:n}">
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Right"></ItemStyle>
                     </asp:BoundField>
                </Columns>
                
            </asp:GridView>
        </td>
    </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td colspan="4"></td>
        </tr>
        <tr>
        <td>
        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="PCGMediumButton" 
                onclick="btnApprove_Click"  />
        </td>
        <td></td>
        </tr>
        <tr>
        <td colspan="4">
        </td>
        </tr>
        <tr>
        <td colspan="4">
        <asp:Label ID="lblNote" runat="server" Text="Note: Pending order needs clients approval." Font-Size="Small" CssClass="cmbField"></asp:Label>
        </td>
        </tr>
        </table>