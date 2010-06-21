<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DailyValuation.ascx.cs"
    Inherits="WealthERP.Advisor.DailyValuation" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script language="javascript" type="text/javascript">


    function showmessage() {




        var grd_Cb = document.getElementById("<%= gvValuationDate.ClientID %>");
        var arrayEle = new Array(grd_Cb.rows.length - 2);
        Z = 0;
        for (j = 1; j < grd_Cb.rows.length - 1; j++) {

            var cell = grd_Cb.rows[j].cells[0];

            if (grd_Cb.rows[j].cells[2].textContent == 'No') {
                if (grd_Cb.rows[j].cells[0].childNodes[1].checked != true) {


                    arrayEle[Z++] = grd_Cb.rows[j].cells[1].textContent;

                }
            }



        }

        if (Z > 0) {
            var content = 'Valuation for the following dates will be missed out ' + "\n";
            for (l = 0; l < Z; l++) {
                content = content + arrayEle[l] + "\n";
            }
            var bool = window.confirm(content + "\n" + 'Do you wish to continue ?');

            if (bool) {
                document.getElementById("ctrl_DailyValuation_hdnMsgValue").value = 1;
                document.getElementById("ctrl_DailyValuation_hiddenUpdateNetPosition").click();
                return false;
            }
            else {
                document.getElementById("ctrl_DailyValuation_hdnMsgValue").value = 0;
                return true;
            }

        }
        else {
            document.getElementById("ctrl_DailyValuation_hdnMsgValue").value = 1;
            document.getElementById("ctrl_DailyValuation_hiddenUpdateNetPosition").click();
        }








    }
  
   
</script>

<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Daily Valuation"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:RadioButton ID="rbtnEquity" runat="server" AutoPostBack="True" CssClass="cmbField"
                OnCheckedChanged="rbtnEquity_CheckedChanged" Text="Equity" GroupName="DailyValuation" />
        </td>
        <td>
            <asp:RadioButton ID="rbtnMF" runat="server" AutoPostBack="True" CssClass="cmbField"
                OnCheckedChanged="rbtnMF_CheckedChanged" Text="Mutual Fund" GroupName="DailyValuation" />
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr id="trEquity" runat="server">
        <td>
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Trade Date"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddTradeYear" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeYear_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddTradeMonth" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMonth_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trMf" runat="server">
        <td>
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Trade Date"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddTradeMFYear" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMFYear_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddTradeMFMonth" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMFMonth_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trHeader" runat="server">
        <td colspan="3">
            <asp:Label ID="lblDate" runat="server" CssClass="HeaderTextBig" Text="Valuation Date " />
            <hr />
        </td>
    </tr>
 <%--   <tr id="trMFDate" runat="server">
        <td>
            <asp:Label ID="lblMFTradeDate" runat="server" CssClass="FieldName" Text="Last MF Valuation Date :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMFValuationDate" runat="server" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr id="trEQDate" runat="server">
        <td>
            <asp:Label ID="lblTradeDate" runat="server" CssClass="FieldName" Text="Last EQ Valuation Date :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblEQValuationDate" runat="server" CssClass="Field"></asp:Label>
        </td>
    </tr>--%>
    <tr id="trValuation" runat="server">
        <td colspan="2" style="margin-left: 40px">
            <asp:GridView ID="gvValuationDate" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" AllowSorting="True" HorizontalAlign="Center" ShowFooter="true"
                OnRowDataBound="gvValuationDate_RowDataBound">
                <FooterStyle CssClass="FooterStyle" />
                <PagerSettings Visible="False" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <%--<PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />--%>
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBx" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Valuation Date" HeaderText="Valuation Date (dd/mm/yyyy)" />
                    <asp:BoundField DataField="Valuation Status" HeaderText="Valuation Status" />
                </Columns>
            </asp:GridView>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr id="trSubmitButton" runat="server">
        <td>
            <asp:Button ID="Button1" runat="server" Text="Update Net Position" CssClass="PCGLongButton"
                OnClick="Button1_Click" />
            <asp:Button ID="hiddenUpdateNetPosition" runat="server" OnClick="hiddenUpdateNetPosition_Click"
                Text="" BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>
    <tr id="trNote" runat="server">
        <td colspan="2">
            <asp:Label ID="lblNote" Text="Note:   The date highlighted is the last valuation date"
                CssClass="rfvPCG" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<%--<div id="DivPager" runat="server">
    <table style="width: 100%">
        <tr id="trPager" runat="server">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />--%>
<asp:HiddenField ID="hdnMsgValue" runat="server" />
