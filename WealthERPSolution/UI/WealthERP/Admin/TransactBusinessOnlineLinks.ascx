<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactBusinessOnlineLinks.ascx.cs" Inherits="WealthERP.Admin.TransactBusinessOnlineLinks" %>


<table width="100%">
    <tr>
        <td class="HeaderTextBig" colspan="2">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Transact/Business Links"></asp:Label>
            <hr />
        </td>
    </tr>
     <tr>
        <td>
            <asp:Label ID="lblNote" runat="server" Font-Size="Small" CssClass="cmbField" Text="Note: Please Contact customer care if you would like to add or remove links." ></asp:Label>
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
                                        <asp:ImageButton ID="imgbtnLinks" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="NavigateToLink" onclick="imgbtnLinks_Click" ImageUrl='<%# Eval("AL_LinkImagePath").ToString() %>' runat="server" />
                                        <br /><br />
                                    </ItemTemplate>                           
                                     <ItemStyle BorderColor="Transparent" HorizontalAlign="Center" />
                                 </asp:TemplateField>
                        </Columns>
                        
                        
                    </asp:GridView>
        </td>
    </tr>
</table>