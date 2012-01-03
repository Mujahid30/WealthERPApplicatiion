<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskGoalClass.ascx.cs" Inherits="WealthERP.Research.RiskGoalClass" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager> 
    <br /> 
    
    <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None" AllowPaging="True" PageSize="20" 
    AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" 
    OnItemDataBound="RadGrid1_ItemDataBound" OnDataBound="RadGrid1_DataBound" OnUpdateCommand="RadGrid1_UpdateCommand"  OnItemCommand="RadGrid1_ItemCommand"
    OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand" OnPreRender="RadGrid1_PreRender" 
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
                <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>
            </Columns>
            
            <EditFormSettings  InsertCaption="Add new item" FormTableStyle-HorizontalAlign="Center" PopUpSettings-Modal="true" 
            PopUpSettings-ZIndex="60" CaptionDataField="XRC_RiskClassCode" EditFormType="Template">
                <FormTemplate>
                    <table id="Table1" cellspacing="1" cellpadding="1" width="250" border="0">                        
                        <tr id="trRiskCodeddl" runat="server">
                            <td>
                                <asp:Label ID="lblPickClass" runat="server" Text="Pick a Risk/Goal class :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                               <asp:DropDownList ID="ddlPickRiskClass" runat="server">                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trRiskGoaltextBox" runat="server">
                            <td>
                            <asp:Label ID="lblRiskGoal" runat="server" Text="Risk/Goal class :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRiskCode" ReadOnly="true" Text='<%# Bind( "XRC_RiskClass") %>' runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text="Description :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" Text='<%# Bind( "ARC_RiskText") %>' runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>                                           
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                </asp:Button>&nbsp;
                                <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
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

    