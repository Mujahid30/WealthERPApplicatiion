<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfoLinks.ascx.cs" Inherits="WealthERP.Admin.InfoLinks" %>

<table width="100%">
    <tr>
        <td class="HeaderTextBig" colspan="2">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Info Links"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Size="Small" CssClass="cmbField" Text="Note: Please Contact customer care if you would like to add or remove links." ></asp:Label>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgNoRecords" runat="server" class="failure-msg" align="center">
            </div>
        </td>
    </tr>
</table>
<table width="100%" class="TableBackground">
   
    <tr>
        <td>
        <br />
            <asp:GridView ID="gvAdviserLinks" ShowHeader="false" runat="server" DataKeyNames="AL_LinkId" 
                AutoGenerateColumns="False" BorderStyle="None" BorderColor="Transparent"
                        Font-Size="Small" HorizontalAlign="Center" 
                        EnableViewState="true" onrowcommand="gvAdviserLinks_RowCommand">
                        
                        <Columns>
                            
                                <asp:TemplateField>
                                    <ItemTemplate>  
                                        <asp:LinkButton ID="lnkLinks" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# Eval("AL_Link").ToString() %>' CommandName="NavigateToLink" ></asp:LinkButton>
                                        <br /><br />
                                    </ItemTemplate>                           
                                     <ItemStyle BorderColor="Transparent" HorizontalAlign="Center" />
                                 </asp:TemplateField>
                                   
                        </Columns>
                    </asp:GridView>
        </td>
    </tr>
</table>