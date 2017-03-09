<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubBrokerCustomerAssociationSync.ascx.cs" Inherits="WealthERP.Advisor.SubBrokerCustomerAssociationSync" %>
<table width="100%">
        <tr>
            <td>
                <div class="divPageHeading">
                    <table width="100%">
                        <tr>
                            <td align="left">
                               Synchronize Customer Subbroker Association
                            </td>
                           
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    
     <table width="100%">
     <tr>
     <td  align="right">
                    <asp:Label ID="lblSource" runat="server" Text="Select Source:" CssClass="FieldName"></asp:Label>
                </td>
   <td class="rightData">
                    <asp:DropDownList ID="ddSource" runat="server" CssClass="cmbField" AutoPostBack="false">
                        <asp:ListItem Value="All">All</asp:ListItem>
                        <asp:ListItem Value="Order">Order</asp:ListItem>
                        <asp:ListItem Value="Transactions">Transactions</asp:ListItem>                        
                         <asp:ListItem Value="Folio">Folio</asp:ListItem>
                    </asp:DropDownList>
                </td>
     </tr>
      
     <tr>
                 
                <td  >
                    <asp:Button ID="btnGo" runat="server" Text="Go" onclick="btnGo_Click" 
                        CssClass="PCGButton" />
                </td>
            </tr>
  
     </table>
     
     