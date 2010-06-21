<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquityMapToCustomers.aspx.cs"
    Inherits="WealthERP.Uploads.EquityMapToCustomers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Map To Customer</title>
    <style>
        .maroon
        {
           font-size: 12px;
           color: Maroon;
        }
    </style>
</head>
<body class="TDBackground">
    <form id="form1" runat="server">
    <div>
        <table class="TDBackground" width="100%">
            <tr>
                <td>
                    <table style="border: solid 1px maroon" width="40%" runat="server" ID="tblSearch">
                        <tr >
                            <td class="FieldName" style="height: 40px">
                                Customer Name
                            </td>
                            <td>
                                <asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
                            </td>
                            
                            
                            <td>
                                <asp:Button ID="btnSearch" runat="server" CssClass="PCGButton" Text="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="center">
                                <span style="font-size: 11px;" class="maroon">Enter few characters of First Name or
                                    Last Name or Both.</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false" class="maroon" ></asp:Label>
                    <asp:HyperLink ID="hlClose" runat="server" class="maroon" NavigateUrl="#" onClick="javascript:window.close();return false;"><br />Close Window</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:GridView ID="gvCustomers" runat="server" CellPadding="4" CssClass="GridViewStyle"
                        AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No customers found."
                        OnRowCommand="gvCustomers_RowCommand" DataKeyNames="CustomerId">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle " />
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="WERP Name" />
                            <asp:BoundField DataField="PANNum" HeaderText="WERP Pan No" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="mapToCusomer"
                                        CommandArgument='<%# Bind("CustomerId") %>' Text="Map to Customer"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center"> 
                    <asp:Label ID="lblRefine" class="maroon" runat="server" Text="More than 100 customers found,please refine query."
                        Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
