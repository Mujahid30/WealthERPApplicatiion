<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskGoalClass.ascx.cs" Inherits="WealthERP.Research.RiskGoalClass" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager> 
    <br /> 
<table class="TableBackground" style="width: 100%;">
    <tr>
        <td>
            <asp:Label ID="lblSetUpClass" runat="server" CssClass="HeaderTextBig" Text="selection of Risk or Goal class"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table>
    <tr>
        <td></td>
        <td class="leftField">
            <asp:Label ID="lblSelectClassType" runat="server" CssClass="FieldName" Text="Select Class type:"></asp:Label>
        </td> 
        <td class="rightField">
            <asp:DropDownList ID="ddlClassType" runat="server" CssClass="cmbField" 
                AutoPostBack="true" onselectedindexchanged="ddlClassType_SelectedIndexChanged">
                <asp:ListItem Value="10" Text="Select"></asp:ListItem>
                <asp:ListItem Value="0" Text="Goal Class"></asp:ListItem>
                <asp:ListItem Value="1" Text="Risk Class"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
    
<table>
    <tr>
        <td>
        </td>
        <td>
             <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None" AllowPaging="True" PageSize="20" 
             AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" 
             OnItemDataBound="RadGrid1_ItemDataBound" OnUpdateCommand="RadGrid1_UpdateCommand"  OnItemCommand="RadGrid1_ItemCommand"
             OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand" 
             AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="XRC_RiskClassCode">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="XRC_RiskClassCode" EditMode="PopUp">
            <Columns>
                <telerik:GridEditCommandColumn>
                </telerik:GridEditCommandColumn>
                <%--<telerik:GridBoundColumn UniqueName="RiskClass" HeaderText="Risk Class" DataField="RiskClass">
                    <HeaderStyle Width="60px"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Description" HeaderText="Description" DataField="Description">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="LastName" HeaderText="LastName" DataField="LastName">
                </telerik:GridBoundColumn>--%>
               
                <telerik:GridBoundColumn UniqueName="XRC_RiskClass" HeaderText="Class" DataField="XRC_RiskClass">
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn UniqueName="ARC_RiskText" HeaderText="Description" DataField="ARC_RiskText">
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Are you sure you want to Remove this Record?" UniqueName="column">
                </telerik:GridButtonColumn>
            </Columns>
            
            <EditFormSettings  InsertCaption="Add new item" FormTableStyle-HorizontalAlign="Center" PopUpSettings-Modal="true" 
            PopUpSettings-ZIndex="10" CaptionDataField="XRC_RiskClassCode" EditFormType="Template">
                <FormTemplate>
                    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">                        
                        <tr id="trRiskCodeddl" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblPickClass" runat="server" Text="Pick a Risk/Goal class :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                               <asp:DropDownList ID="ddlPickRiskClass" CssClass="cmbField" runat="server">                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trRiskGoaltextBox" runat="server">
                            <td class="leftField">
                            <asp:Label ID="lblRiskGoal" runat="server" Text="Risk/Goal class :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtRiskCode" CssClass="txtField" ReadOnly="true" Text='<%# Bind( "XRC_RiskClass") %>' runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblDescription" runat="server" Text="Description :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtDescription" Text='<%# Bind( "ARC_RiskText") %>' CssClass="txtField" runat="server" 
                                Height="80px" Width="260px" TextMode="MultiLine">
                                </asp:TextBox>
                            </td>
                        </tr> 
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="Button1" CssClass="PCGButton" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                </asp:Button>&nbsp;
                                <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
        </ClientSettings>
    </telerik:RadGrid>     
        </td>
    </tr>
</table>


    