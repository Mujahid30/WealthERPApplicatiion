<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DailyValuationMonitor.ascx.cs" Inherits="WealthERP.SuperAdmin.DailyValuationMonitor" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    
</asp:ScriptManager>

<table width="100%">
     <tr>
        <td colspan="6">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="Daily Valuation Monitor"></asp:Label>
            <hr />
        </td>
  </tr>
<tr>
<td align="Left" style="width:158px" valign="top">
<asp:Label ID="lblAction" runat="server" Text="Select Type of Monitoring: "  CssClass="FieldName" Width="100%"></asp:Label>
</td>
<td>
<asp:DropDownList ID="ddlAction" runat="server" CssClass="cmbField">
<asp:ListItem Text="Select" Value="Select"></asp:ListItem>
<asp:ListItem Text="Adviser Valuation" Value="AumMis"></asp:ListItem>
<asp:ListItem Text="Duplicates" Value="DuplicateMis"></asp:ListItem>
<asp:ListItem Text="MF Rejects" Value="mfRejects"></asp:ListItem>
</asp:DropDownList>
<span id="Span7" class="spnRequiredField">*</span>
<asp:CompareValidator ID="cmpamc" runat="server" ErrorMessage="<br />Please select an Action"
   ValidationGroup="MFSubmit" ControlToValidate="ddlAction" Operator="NotEqual"
   CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
</td>
</tr>

<tr>
<%-- <td class="style1" colspan="2">
 
   <asp:RadioButton ID="rbtnPickDate" Class="cmbField" Checked="True" runat="server" Text="Pick a Date" GroupName="Date" onclick="DisplayDates('DATE_RANGE')" />
   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <asp:RadioButton ID="rbtnPickPeriod" Class="cmbField" runat="server" Text="Pick a Period" GroupName="Date" onclick="DisplayDates('PERIOD')"/>
 </td>--%>
<td class="style1" colspan="2">
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date"  />
            <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date"  />
            <asp:Label ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
</td>
</tr>
</table>
  <%--<table id="tblRange">
      <tr>
         <td valign="middle" align="left">             
         <asp:Label ID="lblFromDate" Text="From:" runat="server" CssClass="FieldName"></asp:Label>
         <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox>
          <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
          </ajaxToolkit:CalendarExtender>
           <ajaxToolkit:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server" TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy" Enabled="True">
           </ajaxToolkit:TextBoxWatermarkExtender>
         </td>
         <td valign="middle" align="left">
          <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
          <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
         <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
          </ajaxToolkit:CalendarExtender>
          <ajaxToolkit:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server" TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy" Enabled="True">
            </ajaxToolkit:TextBoxWatermarkExtender>
        </td>
      </tr>
</table>--%>
<table id="tblRange" runat="server" >
    <tr id="trRange" visible="false" runat="server">
        <td align="left" valign="top">
            <asp:Label ID="lblFromDate" runat="server" width="50" CssClass="FieldName">From:</asp:Label>
            </td>
            <td valign="top">
            <asp:TextBox ID="txtFromDate" runat="server" style="vertical-align: middle" Width="150" CssClass="txtField"></asp:TextBox>
            <span id="spnFrom" class="spnRequiredField">*</span>
            <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td valign="top" align="left">
            <asp:Label ID="lblToDate" runat="server" width="50" CssClass="FieldName">To:</asp:Label>
            </td>
            <td valign="top">
            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField" Width="150"> 
            </asp:TextBox>
            <span id="spnTo" class="spnRequiredField">*</span>
            <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />To Date should not be less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>

    </tr>

     <tr id="trPeriod" visible="false" runat="server">
        <td>
            <asp:Label ID="lblPeriod" runat="server" Width="50"  CssClass="FieldName">Period:</asp:Label>
            </td>
            <td>
            <asp:DropDownList ID="ddlPeriod" runat="server" Width="150"  AutoPostBack="true" CssClass="cmbField" >
            </asp:DropDownList>
            <span id="spnPeriod" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />Please select a Period"
            ValidationGroup="MFSubmit" ControlToValidate="ddlPeriod" Operator="NotEqual"
            CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
  <tr><td>&nbsp;</td></tr>
    </table>
                        
                        
<%-- <table ID="tblPeriod" runat="server" style="display:none">
    <tr>
      <td valign="top">
          <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period: </asp:Label>
      </td>
      <td valign="top">
          <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField"></asp:DropDownList>
           <span id="Span4" class="spnRequiredField">*</span>
      </td>
  </tr>
</table>--%>
<table width="100%">
<tr>
<td colspan="2">
<asp:Button ID="btnGo" runat ="server" CssClass="PCGButton"  Text="Go" ValidationGroup="MFSubmit"
        onclick="btnGo_Click"/>
</td>
</tr>
<tr>
 <td class="leftField" align="right">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>
   <asp:GridView ID="gvDuplicateCheck" runat="server" CellPadding="4" CssClass="GridViewStyle" 
      AllowSorting="True"  ShowFooter="true" AutoGenerateColumns="False" DataKeyNames="A_AdviserId,CMFNP_ValuationDate,PASP_SchemePlanCode,CMFA_AccountId,CMFNP_NetHoldings">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle " />
                <Columns>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Select">
                        <ItemTemplate>
                             <asp:CheckBox ID="chkDelete" runat="server" />              
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Valuation Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                             <asp:Label ID="lblValuationDate"  runat="server" Text='<%# Eval("CMFNP_ValuationDate","{0:d}").ToString() %>' ></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right">
                    <HeaderTemplate>
                                <asp:Label ID="lblAdviserIdData" runat="server" Text="AdviserID"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlAdviserId" AutoPostBack="true" CssClass="cmbField"
                                    runat="server" OnSelectedIndexChanged="ddlAdviserIdDuplicate_SelectedIndexChanged"  >
                                </asp:DropDownList>
                            </HeaderTemplate>
                        <ItemTemplate>
                             <asp:Label ID="lblAdviserID" runat="server" Text='<%# Eval("A_AdviserId").ToString() %>'></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                       <HeaderTemplate>
                                <asp:Label ID="lblOrganizationData" runat="server" Text="Adviser Name"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlOrganization" AutoPostBack="true" CssClass="cmbField"
                                    runat="server"  OnSelectedIndexChanged="ddlOrganization_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOrganization" runat="server" Text='<%# Eval("A_OrgName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="true"></HeaderStyle>
                        <ItemStyle Wrap="true"></ItemStyle>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="No of Duplicates" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                             <asp:Label ID="lblDuplicate" runat="server" Text='<%# Eval("Duplicate").ToString() %>'></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    
                     <asp:TemplateField ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                     <HeaderTemplate>
                                <asp:Label ID="lblFolioNoData" runat="server" Text="Folio Number"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlFolioNo" AutoPostBack="true" CssClass="cmbField"
                                    runat="server"  OnSelectedIndexChanged="ddlFolioNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                        <ItemTemplate>
                             <asp:Label ID="lblFolioNo" runat="server" Text='<%# Eval("CMFA_FolioNum").ToString() %>'></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle Wrap="true"></HeaderStyle>
                        <ItemStyle Wrap="true"></ItemStyle>
                    </asp:TemplateField>
                    
                                        
                    <asp:TemplateField ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                    <HeaderTemplate>
                                <asp:Label ID="lblSchemeName" runat="server" Text="Scheme Name"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlScheme" AutoPostBack="true" CssClass="cmbField"
                                    runat="server" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                        <ItemTemplate>
                             <asp:Label ID="lblScheme" runat="server" Text='<%# Eval("PASP_SchemePlanName").ToString() %>'></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                      
                     <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Holdings" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                             <asp:Label ID="lblHoldings" runat="server" Text='<%# Eval("CMFNP_NetHoldings","{0:n}").ToString() %>'></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField> 
                      
                    <%--  <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Creation Date">
                        <ItemTemplate>
                             <asp:Label ID="lblCreation" runat="server" Text='<%# Eval("CMFNP_CreatedOn","{0:d}").ToString() %>' DataFormatString="{0:d}"></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>--%>
                    
                    
                      
                </Columns>
            </asp:GridView>
</td>
</tr>
<tr>
<td>
<asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="PCGButton"  
        onclick="btnDelete_Click" />
</td>
</tr>

<tr>
<td>
<table width="50%">
<tr>
 <td  class="leftField" align="right" >
            <asp:Label ID="lblPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalPage" class="Field" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>

<asp:GridView  ID="gvAumMis" runat="server" CellPadding="4" CssClass="GridViewStyle" AllowSorting="True" HeaderStyle-Width="50%" ShowFooter="true" AutoGenerateColumns="False" >
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle " />
                <Columns>
                <asp:BoundField DataField="CMFNP_ValuationDate" HeaderText="Valuation Date" DataFormatString="{0:d}" 
                 HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="A_AdviserId" HeaderText="Adviser Id"  ItemStyle-HorizontalAlign="Right"  />
                <asp:TemplateField>
                       <HeaderTemplate>
                                <asp:Label ID="lblAdviserNameDate" runat="server" Text="Adviser Name"></asp:Label>
                                <asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                    runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAdviserNameDate" runat="server" Text='<%# Eval("A_OrgName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="true"></HeaderStyle>
                        <ItemStyle Wrap="true"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AUM" HeaderText="AUM"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                </Columns>

</asp:GridView>
</td>
</tr>
</table>
</td>

</tr>
<tr>
<td>
<asp:Panel ID="pnlReject" runat="server" class="Landscape" Width="60%" ScrollBars="Horizontal">
<table style="width: 100%;" class="TableBackground">
<tr>
 <td  class="leftField" align="right" width="95%">
            <asp:Label ID="lblRejectCount" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblRejectTotal" class="Field" runat="server"></asp:Label>
</td>
</tr>
    <tr>
        <td>
            <asp:GridView ID="gvMFRejectedDetails" runat="server" AutoGenerateColumns="False"  AllowSorting="True" 
                CellPadding="4" CssClass="GridViewStyle" HeaderStyle-Width="100%" 
                 ShowFooter="True">
                <FooterStyle CssClass="FooterStyle"  />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                   
                    <asp:BoundField DataField="CMFTS_CreatedOn" HeaderText="Process Date" ItemStyle-HorizontalAlign="Center" 
                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-Width="25px" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="right" HeaderStyle-Width="20px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
                            <HeaderTemplate>
                                <asp:Label ID="lbladviserId" runat="server" Text="AdviserId"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlAdviserId" CssClass="cmbLongField" AutoPostBack="true" Width="80px"
                                    runat="server"  OnSelectedIndexChanged="ddlAdviserId_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbladviser"  runat="server" Text='<%# Eval("A_AdviserId").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="A_OrgName" HeaderText="Organization Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="20px" 
                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="WUXFT_XMLFileName" HeaderText="File NameSource Type"  ItemStyle-HorizontalAlign="left" HeaderStyle-Width="20px" 
                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="CMFTS_FolioNum" HeaderText="Folio number" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="20px" 
                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="CMFTS_SchemeCode" HeaderText="Scheme" ItemStyle-HorizontalAlign="right" HeaderStyle-Width="20px" 
                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  />
                    <asp:BoundField DataField="PASP_SchemePlanName" HeaderText="Scheme name" ItemStyle-HorizontalAlign="left" 
                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Width="225px" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="CMFTS_TransactionClassificationCode" HeaderText="Transaction type" ItemStyle-HorizontalAlign="left" 
                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-Width="20px"  />
                    <asp:BoundField DataField="CMFTS_TransactionDate" HeaderText="Transaction date" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="25px" 
                    
                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                     <asp:BoundField DataField="CMFTS_Price" HeaderText="Price" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" HeaderStyle-Width="20px" 
                     ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="CMFTS_Units" HeaderText="Units" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" HeaderStyle-Width="20px" 
                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="CMFTS_Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" HeaderStyle-Width="20px" 
                     ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                    
                    <asp:TemplateField ItemStyle-HorizontalAlign="left" HeaderStyle-Width="10px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"  >
                            <HeaderTemplate>
                                <asp:Label ID="lblRejectReason" runat="server" Text="Reject Reason"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlRejectReason" CssClass="cmbLongField" AutoPostBack="true" Width="80%"
                                    runat="server"  OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRejectReasonHeader"  runat="server" Text='<%# Eval("WRR_RejectReasonDescription").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                        
                        <asp:TemplateField ItemStyle-HorizontalAlign="right" ItemStyle-Width="10px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
                            <HeaderTemplate>
                                <asp:Label ID="lblProcessId" runat="server" Text="ProcessId"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlProcessId" CssClass="cmbLongField" AutoPostBack="true" Width="80px"
                                    runat="server"  OnSelectedIndexChanged="ddlProcessId_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProcess"  runat="server" Text='<%# Eval("Adul_ProcessId").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    
                </Columns>
                
            </asp:GridView>
        </td>
    </tr>
</table>
</asp:Panel>
</td>
</tr>

</table>

<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
    <td align="center">
    <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
    </div>
    </td>
    </tr>
 </table>
 
 <table width="100%">
 <tr style="width:100%">
    <td colspan="3">
        <table width="100%">
            <tr id="trpagerDuplicate" runat="server" width="100%" >
                <td align="right">
                    <Pager:Pager ID="mypagerDuplicate" runat="server"></Pager:Pager>
                </td>
               
            </tr>
        </table>
    </td>
 </tr>

</table>


 <table width="50%">
 <tr style="width:100%">
    <td colspan="3">
        <table width="100%">
            <tr id="trmypagerAUM" runat="server" width="100%" >
                <td align="right">
                    <Pager:Pager ID="mypagerAUM" runat="server"></Pager:Pager>
                </td>
               
            </tr>
        </table>
    </td>
 </tr>

</table>
 <table width="100%">
 <tr style="width:100%">
    <td colspan="3">
        <table width="100%">
            <tr id="trPagerReject" runat="server" width="100%" >
                <td align="right">
                    <Pager:Pager ID="pgrReject" runat="server"></Pager:Pager>
                </td>
               
            </tr>
        </table>
    </td>
 </tr>

</table>


<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeNameFilter" runat="server" Visible="false" />

<asp:HiddenField ID="hdnAdviserNameAUMFilter" runat="server" Visible="false" />

<asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAdviserIdFilter" runat="server" Visible="false" />

<asp:HiddenField ID="hdnAdviserIdDupli" runat="server" Visible="false" />
<asp:HiddenField ID="hdnOrgNameDupli" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioiNoDupli" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeDupli" runat="server" Visible="false" />
