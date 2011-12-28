<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskScore.ascx.cs" Inherits="WealthERP.Research.RiskScore" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script type="text/javascript">

    function Validate() {
        var assumptionValue = document.getElementById('ctrl_CustomerAssumptionsPreferencesSetup_RadGrid1_ctl00__2').value;
        return true;
    }

</script>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager> 
<br /> 
    
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="RiskScoreCalculationId" 
    SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Risk Score" onclick="HideStatusMsg()"
            Value="RiskScore" TabIndex="0" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Risk Questions and Scores" onclick="HideStatusMsg()"
            Value="RiskQuestionsandScores" TabIndex="1">
        </telerik:RadTab>        
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadMultiPage ID="RiskScoreId" EnableViewState="true" runat="server" SelectedIndex="0">
<telerik:RadPageView ID="RadPageView1" runat="server">
<asp:Panel ID="pnlRiskScore" runat="server">
<br />
<table style="width:75%">
    <tr>
        <td>
        </td>
        <td>
            <telerik:RadGrid ID="RadGrid1" runat="server" CssClass="RadGrid" GridLines="None"
            AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False" Skin="Telerik"
            ShowStatusBar="true" AllowAutomaticDeletes="false" AllowAutomaticInserts="False"  OnItemDataBound="RadGrid1_ItemDataBound"
        OnInsertCommand="RadGrid1_InsertCommand" OnUpdateCommand="RadGrid1_UpdateCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
         OnItemCommand="RadGrid1_ItemCommand" OnPreRender="RadGrid1_PreRender" AllowAutomaticUpdates="False" HorizontalAlign="NotSet" DataKeyNames="XRC_RiskClassCode">            
            <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="XRC_RiskClassCode">
                <Columns>
                    <telerik:GridEditCommandColumn>
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn UniqueName="XRC_RiskClass" HeaderText="Risk Class" DataField="XRC_RiskClass" >
                    </telerik:GridBoundColumn>                  
                    <telerik:GridBoundColumn UniqueName="WRPR_RiskScoreLowerLimit" HeaderText="Lower Limit" DataField="WRPR_RiskScoreLowerLimit" >
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="WRPR_RiskScoreUpperLimit" HeaderText="Upper Limit" DataField="WRPR_RiskScoreUpperLimit" >
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn UniqueName="CPA_Value" HeaderText="Value" DataField="CPA_Value" >
                    </telerik:GridBoundColumn>--%>
                     <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column">
                    </telerik:GridButtonColumn>
                </Columns>
                <EditFormSettings  InsertCaption="Add new item" FormTableStyle-HorizontalAlign="Center" 
                PopUpSettings-Modal="true" PopUpSettings-ZIndex="60" CaptionFormatString="Edit Risk ClassCode: {0}"
                EditFormType="Template">
                <%--CaptionDataField="CPA_Value"--%>
                    <FormTemplate>
                        <table id="Table1" cellspacing="1" cellpadding="1" width="250" border="0">                        
                        <tr>
                            <td>
                                <asp:Label ID="lblPickClass" runat="server" Text="Pick a Risk class :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                               <asp:DropDownList ID="ddlPickRiskClass" runat="server">                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblLowerrLimit" runat="server" Text="Lowerr Limit :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLowerrLimit" Text='<%# Bind( "WRPR_RiskScoreLowerLimit") %>' runat="server">
                                </asp:TextBox>
                            </td>
                        </tr> 
                        <tr>
                            <td>
                                <asp:Label ID="lblUpperLimit" runat="server" Text="Upper Limit :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUpperLimit" Text='<%# Bind( "WRPR_RiskScoreUpperLimit") %>' runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>                                           
                    </table>
                        <table style="width: 80%">
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                        runat="server" CssClass="PCGButton" OnClientClick="return Validate()" ValidationGroup="vgbtnSubmit" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
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
</asp:Panel>
</telerik:RadPageView>
    
 
<telerik:RadPageView ID="RadPageView2" runat="server">
<asp:Panel ID="Panel2" runat="server">
<br/>
<table style="width:75%">
    <tr>
        <td>
        </td>
        
    </tr>
</table>

</asp:Panel>
</telerik:RadPageView>         
</telerik:RadMultiPage>