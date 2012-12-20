﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FinancialPlanning.ascx.cs"
    Inherits="WealthERP.Advisor.FinancialPlanning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
<Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</telerik:RadScriptManager>
<style type="text/css">
    .style1
    {
        width: 42px;
    }
</style>

<script type="text/javascript">
    function DeleteConfirmation() {

        var bool = window.confirm('Are you sure you want to delete your Risk profile?');

        if (bool) {
            document.getElementById("ctrl_FinancialPlanning_hdnDeletemsgValue").value = 1;
            document.getElementById("ctrl_FinancialPlanning_hiddenDeleteQuestion").click();

            return false;
        }
        else {
            return false;
        }
    }

</script>

<script id="myScript" language="javascript" type="text/javascript">
    function message(score, rclass) {
        alert(score + rclass);
        alert("Risk Score:" + score + "\n" + "Risk Class:" + rclass);
    }
    function optionvalidation() {
        
        var totalQuestions = <%= totalquestion %>;
        var maximumOptions = <%= optioncount %>;
        
        var isOptionSelected = true;
        var QuestionArray = new Array();
        var OptionArray = new Array();
        var questiontracker = 0;
        var optiontracker = 0;
        var notAnswered = new Array(totalQuestions);
        var notAnsweredDisplay = "";
        


        var optionsArr = new Array(totalQuestions) //later bring this number from Server side
        for (i = 1; i <= totalQuestions; i++)
            optionsArr[i] = new Array(maximumOptions)

        for (var i = 0; i < document.forms[0].elements.length; i++) {

            if (document.forms[0].elements[i].type == "radio" && document.forms[0].elements[i].id.indexOf("rbtnQ") > 0) {
                var optionId = document.forms[0].elements[i].id;
                var optionValue = optionId.substring(optionId.indexOf("rbtnQ") + 5)
                var answer = optionValue.split('A')
               
                optionsArr[answer[0]][answer[1]] = document.forms[0].elements[i].checked;

            }
        }


        for (i = 1; i <= totalQuestions; i++) {
            notAnswered[i] = false;
            var isSelected = false;
            for (j = 1; j < maximumOptions; j++) {
                if (optionsArr[i][j] == true) {
                    notAnswered[i] = true;
                    break;
                }
            }
        }

        for (i = 1; i <= totalQuestions; i++) {
            if (notAnswered[i] == false)
                notAnsweredDisplay += i + ",";
        }
        if (notAnsweredDisplay != "")
        {
            notAnsweredDisplay = notAnsweredDisplay.substr(0, notAnsweredDisplay.length - 1)
            alert("Please select answer for question(s) " + notAnsweredDisplay)
            return false;
       }
     

    }
     }
    }

</script>
<table width="100%">
    <tr>
        <td colspan="3">
            <hr />
        </td>
    </tr>
    <%-- <tr>
        <td width="150px" align="right">
            <asp:Label ID="lblPickCustomer" runat="server" Text="Pick a customer  : " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPickCustomer" runat="server" Width="142px" AutoPostBack="True"
                OnTextChanged="txtPickCustomer_TextChanged" CssClass="txtField" AutoComplete="Off"></asp:TextBox>
            <ajaxtoolkit:autocompleteextender id="txtParentCustomer_autoCompleteExtender" runat="server"
                targetcontrolid="txtPickCustomer" servicemethod="GetCustomerName" servicepath="~/CustomerPortfolio/AutoComplete.asmx"
                minimumprefixlength="1" enablecaching="False" completionsetcount="5" completioninterval="100"
                completionlistcssclass="AutoCompleteExtender_CompletionList" completionlistitemcssclass="AutoCompleteExtender_CompletionListItem"
                completionlisthighlighteditemcssclass="AutoCompleteExtender_HighlightedItem"
                usecontextkey="True" onclientitemselected="GetCustomerId" delimitercharacters=""
                enabled="True" />
        </td>
        <td>
            <asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <hr />
        </td>
    </tr>--%>
</table>
<table width="100%">
    <tr>
        <td>
            <ajaxToolkit:TabContainer ID="tabRiskProfilingAndAssetAllocation" runat="server"
                ActiveTabIndex="0" Width="100%" Style="visibility: visible;">
                <ajaxToolkit:TabPanel ID="tabRiskProfiling" runat="server" 
                    HeaderText="Risk Profiling">
                    <HeaderTemplate>
                        Risk Profile
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table runat="server" id="tblPickOptions" style="text-align: left;" width="100%">
                            <tr>
                                <td style="width:50%;">
                                    &nbsp;
                                </td>
                                <td >
                                    <asp:Button ID="btnDeleteRiskProfile" runat="server" OnClientClick="return DeleteConfirmation()" Text="Remove Risk profile" CssClass="PCGMediumButton" CausesValidation="false" />
                                </td>
                            </tr>
                            </table>
                            <table width="40%">
                            <tr>
                            <td style="width:20%">
                            <asp:Label ID="lblPickRiscProfile" runat="server" CssClass="HeaderTextSmall" Text="Pick Risk Profile Type:"></asp:Label>
                            </td>
                            <td style="width:20%">
                            <asp:DropDownList ID="ddlRiskProfileType" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                                CssClass="cmbField" OnSelectedIndexChanged="ddlRiskProfileType_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Agreed" Value="Agreed" ></asp:ListItem>
                                <asp:ListItem Text="Recommended" Value="Recommended" ></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                            </tr>
                           <%-- <tr align="left" runat="server">
                                <td runat="server" style="float: left">
                                    <br />
                                    <asp:Label ID="lblPickWhat" runat="server" CssClass="HeaderTextSmall" Style="float: right"
                                        Text="Do you wish to: "></asp:Label>
                                </td>
                                <td style="float: left; vertical-align: middle" runat="server">
                                    <br />
                                    <asp:RadioButton ID="rbtnPickRiskclass" AutoPostBack="True" runat="server" CssClass="txtField"
                                        GroupName="RiskProfile" Text="Pick Risk Class" OnCheckedChanged="rbtnPickRiskclass_CheckedChanged" />
                                    <asp:RadioButton ID="rbtnAnsQuestions" GroupName="RiskProfile" AutoPostBack="True"
                                        runat="server" CssClass="txtField" Text="Answers the Questions" OnCheckedChanged="rbtnAnsQuestions_CheckedChanged" />
                                    <br />
                                </td>
                            </tr>--%>
                        </table>
                        <table runat="server" id="tblPickRiskClass" width="40%">
                            <tr runat="server">
                                <td runat="server" style="width:20%">
                                    <br />
                                    <asp:Label ID="lblPickRiskPlass" runat="server" Text="Pick Risk Class:" CssClass="HeaderTextSmall"></asp:Label>
                                </td>
                                <td runat="server" style="width:20%">
                                    <br />
                                    <asp:DropDownList ID="ddlPickRiskClass" runat="server" CssClass="cmbField" Style="vertical-align: middle">
                                    </asp:DropDownList>
                                    <span id="Span12" runat="server" class="spnRequiredField">*</span>
                                    <asp:CompareValidator ID="ddlPickRiskClass_CompareValidator" runat="server" ControlToValidate="ddlPickRiskClass"
                                        ErrorMessage="<br />Please select a Risk Class" Operator="NotEqual" ValueToCompare="Select Risk Class"
                                        Display="Dynamic" CssClass="cvPCG">
                                    </asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr runat="server" id="trRiskProfiler">
                                <td runat="server" >
                                    <br />
                                    <asp:Label ID="lblRiskProfiler" Text="Risk Profiler Questionnaire" runat="server"
                                        CssClass="HeaderTextSmall"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--<table id="tblRiskScore" runat="server" style="background-color: #D3D3D3; border: thin solid #FF0000"--%>
                                    <table id="tblRiskScore" runat="server" visible="False" width="100%">
                                       <%-- <tr id="Tr1" runat="server">
                                            <td id="Td1" runat="server">
                                                <strong>Risk Score:</strong><asp:Label ID="lblRScore" runat="server" CssClass="FieldName"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td id="Td2" runat="server">
                                                <strong>Risk Class:</strong><asp:Label ID="lblRClass" runat="server" CssClass="FieldName"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td id="Td3" runat="server">
                                                <strong>Risk Profile Date:</strong><asp:Label ID="lblRiskProfileDate" runat="server"
                                                    CssClass="FieldName" Visible="False"></asp:Label>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                        <td style="width:50%;">
                                        <table id="tblAgreed" runat="server" width="100%" style="background-color: #D3D3D3; border: thin solid #FF0000">
                                        <tr>
                                        <td id="tdAgreedRClass" runat="server">
                                                <strong>Risk Class:</strong><asp:Label ID="lblAgreedRClass" runat="server" CssClass="FieldName"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td id="tdARDate" runat="server">
                                                <strong>Risk Profile Date:</strong><asp:Label ID="lblARDate" runat="server"
                                                    CssClass="FieldName" Visible="False"></asp:Label>
                                         </td>
                                         </tr>
                                        </table>
                                        </td>
                                        <td style="width:50%;">
                                        <table  id="tblRecommended" runat="server" width="100%" style="background-color: #D3D3D3; border: thin solid #FF0000">
                                        <tr>
                                        <td id="tdRRclass" runat="server">
                                                <strong>Risk Class:</strong><asp:Label ID="lblRRclass" runat="server" CssClass="FieldName"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td id="tdRRDate" runat="server">
                                                <strong>Risk Profile Date:</strong><asp:Label ID="lblRRDate" runat="server"
                                                    CssClass="FieldName" Visible="False"></asp:Label>
                                         </td>
                                         </tr>
                                         <tr>
                                         <td id="tdRRScore" runat="server">
                                                <strong>Risk Score:</strong><asp:Label ID="lblRRScore" runat="server" CssClass="FieldName"
                                                    Visible="False"></asp:Label>
                                         </td>
                                         <td></td>
                                         </tr>
                                        </table>
                                        </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trRiskProfilingParagraph" runat="server">
                                <td width="100%" colspan="6" runat="server">
                                    <asp:Label ID="lblRiskProfilingParagraph" runat="server" CssClass="GridViewCmbField"
                                        Style="white-space: inherit"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div runat="server" id="divQuestionAnswers">
                                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnSubmitRisk" runat="server" CssClass="PCGButton" OnClick="btnSubmitRisk_Click"
                                                    Text="Submit" OnClientClick="return optionvalidation()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnSubmitForPickRiskclass" runat="server" CssClass="PCGButton" Text="Submit"
                                                    OnClick="btnSubmitForPickRiskclass_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tabAssetAllocation" runat="server" 
                    HeaderText="Asset Allocation">
                    <HeaderTemplate>
                        Asset Allocation
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table width="100%">
                            <tr style="float: left">
                                <td>
                                    <asp:Label ID="lblApprovedByCustomerOn" runat="server" Text="Approved By Customer On : "
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtApprovedByCustomerOn" runat="server" CssClass="Field"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calApprovedByCustomerOn" runat="server" Enabled="True"
                                        Format="dd/MM/yyyy" TargetControlID="txtApprovedByCustomerOn">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtApprovedByCustomerOn_WaterMarkText"
                                        runat="server" Enabled="True" TargetControlID="txtApprovedByCustomerOn" WatermarkText="dd/mm/yyyy">
                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvAssetAllocation" runat="server" AllowSorting="True" OnRowDataBound="gvAssetAllocation_RowDataBound"
                                    AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" HorizontalAlign="Center">                                
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <Columns>
                                <asp:TemplateField HeaderText="Class" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblClass" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Class") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                 </asp:TemplateField>
                                 
                                 <asp:TemplateField HeaderText="Current(%)" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrentPctg" runat="server" CssClass="cmbField" 
                                                Text='<%#Eval("CurrentPercentage") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                 </asp:TemplateField>
                                 
                                  <asp:TemplateField HeaderText="Recommended(%)" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecommendedPctg" runat="server" CssClass="cmbField" 
                                                Text='<%#Eval("RecommendedPercentage") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                 </asp:TemplateField>
                                 
                                 <asp:TemplateField HeaderText="Indicator">
                                    <ItemTemplate>                         
                                        <asp:Image ID="imgActionIndicator" ImageAlign="Middle" runat="server" />
                                    </ItemTemplate>                           
                                     <ItemStyle HorizontalAlign="Center" />
                                 </asp:TemplateField>
                                 
                                 <asp:TemplateField HeaderText="Action Needed(%)" >
                                        <ItemTemplate>                                            
                                            <asp:Label ID="lblActionPctg" runat="server" CssClass="cmbField" 
                                                Text='<%#Eval("ActionNeeded") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                 </asp:TemplateField>
                                 
                                 <asp:TemplateField HeaderText="Current(Rs.)" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentRs" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CurrentRs") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                 </asp:TemplateField>
                                 
                                 <asp:TemplateField HeaderText="Recommended(Rs.)" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecommendedRs" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("RecommendedRs") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                 </asp:TemplateField>
                                 
                                 <asp:TemplateField HeaderText="Action Needed(Rs.)" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblActionRs" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("ActionRs") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                 </asp:TemplateField>
                               </Columns>
                               <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" VerticalAlign="Top" />
                               <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Center" />                                
                               <RowStyle CssClass="RowStyle" />
                               <SelectedRowStyle CssClass="SelectedRowStyle" />
                            </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAgeErrormsg" runat="server" CssClass="cmbField" 
                                    Font-Bold="True" Visible="False" 
                                    Text="No Age to display chart. Please Fill Date of Birth in profile!"></asp:Label>
                            </td>
                        </tr>
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblRecommondedChart" runat="server" CssClass="HeaderTextSmall" Text="Recommended Asset Allocation Chart"></asp:Label>
                                </td>
                                <td>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblCurrentChart" runat="server" CssClass="HeaderTextSmall" Text="Current Asset Allocation Chart"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Chart ID="cActualAsset" runat="server" Height="200px" Width="400px">
                                        <Series>
                                            <asp:Series ChartArea="caActualAsset" Name="sActualAsset">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="caActualAsset">
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                                <td id="trCurrentAssetAllocation" runat="server" style="padding: 0px; float: left">
                                    <asp:Chart ID="ChartCurrentAsset" runat="server" Height="200px" Width="400px">
                                        <Series>
                                            <asp:Series ChartArea="caActualAsset" Name="sActualAsset">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="caActualAsset">
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                            </tr>
                            <tr id="trCustomerAssetText" runat="server">
                                <td runat="server" colspan="5" width="100%">
                                    <asp:Label ID="lblCustomerParagraph" runat="server" CssClass="Field" BackColor="Transparent"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAge" runat="server" CssClass="FieldName" Text="Based on Customer profile Age is "></asp:Label>
                                    <asp:Label ID="lblAgeResult" runat="server" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblRiskClass" runat="server" CssClass="FieldName" Text="Based on  Risk Assessment Customer Risk class is "></asp:Label>
                                    <asp:Label ID="lblRClassRs" runat="server" CssClass="FieldName"></asp:Label>
                                    <asp:Label ID="lblRiskScore" runat="server" CssClass="FieldName" Text="  &amp;  Risk Score is  "></asp:Label>
                                    <asp:Label ID="lblRscoreAA" runat="server" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblChartErrorDisplay" runat="server" Text="No Age to display chart. Fill Date of Birth!"
                                        Visible="False"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                
                  <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" 
                    HeaderText="Asset Allocation">
                    <HeaderTemplate>
                        Model Portfolio
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table width="100%">
                            <tr style="float: left">
                            <td>
                            <asp:Label ID="lblModelPortfolio" runat="server" CssClass="FieldName" Text="Select Model Portfolio :"></asp:Label>
                            </td>
                                <td>
                                <asp:DropDownList ID="ddlModelPortFolio" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlModelPortFolio_OnSelectedIndexChanged"></asp:DropDownList>
                                </td>
                                </tr>
                                </table>
                             
                                <br />
                                
                                <table id="tableGrid" runat="server" class="TableBackground" width="100%">
                                

    <tr>
        <td>
    <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None" AllowPaging="True" 
    PageSize="20" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false" 
    AllowAutomaticInserts="false" 
    AllowAutomaticUpdates="false" HorizontalAlign="NotSet">
        <MasterTableView>
            <Columns>
                <%--<telerik:GridClientSelectColumn UniqueName="SelectColumn"/>--%>               
                <telerik:GridBoundColumn  DataField="PASP_SchemePlanName"  HeaderText="Scheme Name" UniqueName="PASP_SchemePlanName" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_AllocationPercentage"  HeaderText="Weightage" UniqueName="AMFMPD_AllocationPercentage">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn  DataField="PASP_SchemePlanCode" Visible="false" HeaderText="SchemePlanCode" UniqueName="PASP_SchemePlanCode">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top"  />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_AddedOn"  HeaderText="Started Date" UniqueName="AMFMPD_AddedOn">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_SchemeDescription"  HeaderText="Description" UniqueName="AMFMPD_SchemeDescription">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
           <%-- <table id="tblArchive" runat="server">
            <tr>
            <td class="leftField">
                        <asp:Label ID="lblArchive" runat="server" CssClass="FieldName" Text="Reason for Archiving:"></asp:Label>
                    </td>
                    <td class="rightField">                         
                        <asp:DropDownList ID="ddlArchive" runat="server" CssClass="cmbField">               
                        </asp:DropDownList>
                    </td>
            </tr>
            </table>--%>                    
        </MasterTableView>
        <ClientSettings>
            <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
        </ClientSettings>
    </telerik:RadGrid>   
    </td>
    </tr>
    </table>
    
                                </ContentTemplate>
                                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hidGoalCount" runat="server" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnDeletemsgValue" runat="server" />
<div style="visibility: hidden">
    <asp:Button ID="hiddenDeleteQuestion" runat="server" onclick="hiddenDeleteQuestion_Click" BorderStyle="None" BackColor="Transparent" /> 
</div>