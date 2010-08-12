<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorProfile.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorProfile" %>


<style type="text/css">
    .style1
    {
        height: 23px;
    }
</style>


<table style="width: 100%;" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Advisor Profile"></asp:Label>
            <hr />
        </td>
    </tr>
    </table>

<table style="width: 100%;" class="TableBackground">
   
   
    <%--<tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="Label20" runat="server" CssClass="HeaderTextBig" Text="Advisor Profile"></asp:Label>
            </td>
        </tr>--%>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 30%">
            <asp:Label ID="lblOrganizationName" runat="server" Text="Name Of the Organization / Individual :"
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:Label ID="lblOrgName" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblContactName" runat="server" CssClass="FieldName" Text="Contact Person Name :"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:Label ID="lblContactPerson" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="Label4" runat="server" CssClass="HeaderTextSmall" Text="Office Address "></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblLine1" runat="server" CssClass="FieldName" Text="Line1 (House No/ Building) :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblLine_1" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Line2 ( Street) :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblLine_2" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Line3  (Area) :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblLine_3" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="style7">
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="City :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCity" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="style7">
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPincode" runat="server" CssClass="FieldName" Text="Pin Code :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPin" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="style7">
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="State :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblstate" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="leftField">
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Country :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCountry" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="style7">
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="Label12" runat="server" CssClass="HeaderTextSmall" Text="Contact Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhone1" runat="server" CssClass="FieldName" Text="Telephone Number 1 :"
                BorderStyle="None"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPhNumber1" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="style7">
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhone2" runat="server" CssClass="FieldName" Text="Telephone Number 2 :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPhNumber2" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="style7">
        </td>
    </tr>
    <tr>
        <td class="leftAddressField">
            <asp:Label ID="Label16" runat="server" CssClass="FieldName" Text="Fax :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblFax" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="style7">
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblMobileNumber" runat="server" CssClass="FieldName" Text="Mobile Number :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMobile" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="style7">
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMail" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        </tr>
        <tr>
        <td class="leftField">
        <asp:Label ID="lblwebsite" runat="server" CssClass="FieldName" Text="Website :"></asp:Label>
        </td>
        <td class="rightField">
        <asp:HyperLink ID="lblwsite" runat="server" CssClass="LinkButtons" Target="_blank"></asp:HyperLink>
        </td>
        <td class="style7">
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Additional Information"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label18" runat="server" CssClass="FieldName" Text="Business Type :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblBusinessType" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="style7">
        </td>
    </tr>
    <tr>
    <td class="leftField">
    <asp:Label ID="lblmodletype" runat="server" CssClass="FieldName" Text="Associate Model Type:"></asp:Label>
    </td>
    <td class="rightField">
    <asp:Label ID="lblmtype" runat="server" Text="" CssClass="Field"></asp:Label>
    </td> 
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label19" runat="server" CssClass="FieldName" Text="Multibranch Network :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMultiBranch" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
        <td class="style7">
        </td>
    </tr>
</table>
