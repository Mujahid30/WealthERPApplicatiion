<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadManagementAdd.ascx.cs"
    Inherits="WealthERP.Advisor.LeadManagementAdd" %>
<table border="0">
    <tr>
        <td class="HeaderCell" colspan="4" style="height:30px">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Lead Management"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Is lead an existing client:"></asp:Label>
            
        </td>
        <td>
            <asp:RadioButton ID="RadioButton1" runat="server" Text="Yes" CssClass="Field" GroupName="ExistingClient"  />
            <asp:RadioButton ID="RadioButton2" runat="server" Text="No" CssClass="Field"  GroupName="ExistingClient" Checked="true" />
        </td>
        <td class="leftField">
             <asp:Label ID="Label9" runat="server" CssClass="FieldName"  Text="if yes, Existing Customer:"></asp:Label>
        </td>
        <td>
            <asp:TextBox CssClass="txtField"   ID="TextBox9" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td  class="leftField">
             <asp:Label ID="Label8" runat="server" Text="Lead Name" CssClass="FieldName"></asp:Label>
             :</td>
        <td>
            <asp:TextBox CssClass="txtField"  ID="TextBox" runat="server" ></asp:TextBox>
        </td>
        <td class="leftField">
             <asp:Label ID="Label2" runat="server" Text="Product Interest:" CssClass="FieldName"></asp:Label>
             </td>
        <td>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
             <asp:Label ID="Label3" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
             </td>
        <td>
            <asp:TextBox CssClass="txtField"   ID="TextBox2" runat="server"></asp:TextBox>
        </td>
        <td class="leftField">
             <asp:Label ID="Label4" runat="server" Text="Capture Date:" CssClass="FieldName"></asp:Label>
             </td>
        <td>
            <asp:TextBox CssClass="txtField"   ID="TextBox5" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
        <td class="leftField">
             <asp:Label ID="Label5" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
             </td>
        <td>
            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Converted" />
                <asp:ListItem Text="In Process" />
                <asp:ListItem Text="Rejected" />
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4">
        
        <asp:Label ID="Label17" runat="server" Text="Contact Details" CssClass="HeaderTextSmall"></asp:Label>
            
        </td>
    </tr>
    <tr>
        <td class="leftField">
             <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Mobile no:" ></asp:Label>
        </td>
        <td>
            <asp:TextBox CssClass="txtField"   ID="TextBox3" runat="server"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName"  Text="Email Id:" ></asp:Label>
        </td>
        <td>
            <asp:TextBox CssClass="txtField"   ID="TextBox6" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="Telephone no:"></asp:Label>
        </td>
        <td>
            <asp:TextBox CssClass="txtField"   ID="TextBox4" runat="server"></asp:TextBox>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td colspan="4">
        <asp:Label ID="Label18" runat="server" Text="Questions for Life insurance" CssClass="HeaderTextSmall"></asp:Label>
           
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label16" runat="server" CssClass="FieldName" Text="I need coverage amount of (Rs)*:"></asp:Label>
        </td>
        <td>
            <asp:TextBox CssClass="txtField"   ID="TextBox7" runat="server"></asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Age of the person to be covered is (years)*:"></asp:Label>
        </td>
        <td>
            <asp:TextBox CssClass="txtField"   ID="TextBox8" runat="server"></asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        
        <td class="leftField">
            <asp:Label ID="Label12" runat="server" CssClass="FieldName" Text="Gender:"></asp:Label>
        </td>
        <td>
            <asp:RadioButton ID="RadioButton3" runat="server" Text="Male" GroupName="gender" CssClass="Field" />
            <asp:RadioButton ID="RadioButton4" runat="server" Text="Female" GroupName="gender" CssClass="Field" />
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
       
        <td class="leftField"> 
            <asp:Label ID="Label13" runat="server" CssClass="FieldName" Text="Do you want to pay premium annually or at once (single)?:"></asp:Label>
        </td>
        <td>
            <asp:RadioButton ID="RadioButton5" runat="server" Text="Annually" GroupName="annual"  CssClass="Field" Checked="true" />
            <asp:RadioButton ID="RadioButton6" runat="server" Text="At Once" GroupName="annual"  CssClass="Field"  />
        </td>
        <td>
        </td>
         <td>
        </td>
    </tr>
    <tr>
      
        <td class="leftField">
            <asp:Label ID="Label14" runat="server" CssClass="FieldName" Text="Number of coverage years:"></asp:Label>
        </td>
        <td colspan="2">
            <asp:TextBox CssClass="txtField"   ID="TextBox12" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
       
        <td class="leftField">
            <asp:Label ID="Label15" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
        </td>
        <td>
            <asp:TextBox CssClass="txtField"   ID="TextBox11" runat="server" Height="100px" Width="200px"></asp:TextBox>
        </td>
        <td>
        </td>
         <td>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton"  />
        </td>
    </tr>
</table>
