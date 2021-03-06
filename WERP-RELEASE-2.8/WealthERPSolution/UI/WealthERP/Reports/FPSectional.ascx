﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FPSectional.ascx.cs" Inherits="WealthERP.Reports.FPSectional" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    
</asp:ScriptManager>
<script language="javascript" type="text/javascript">

    function CheckBoxListSelect() {
        var checkAll = document.getElementById("chkCheckAll");
        //    var unCheckAll = document.getElementById("chkUncheckAll");   
        //    if (unCheckAll.checked == true) {
        //        checkAll.checked = false;
        //        unCheckAll.checked = false;
        //        unCheckAll.disabled = true;
        //        checkAll.disabled = false;

        //    } else if (checkAll.checked == true ) {
        //        unCheckAll.checked = false;
        //        checkAll.checked = false;
        //        checkAll.disabled = true;       
        //        unCheckAll.disabled = false;
        //        
        //    }  
        //       
        var chkBoxList = document.getElementById("tblFPsectinal");
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            //        if (state == "false") {            
            //            chkBoxCount[i].checked = false;           
            //        }
            //        else
            //            chkBoxCount[i].checked = true;

            if (checkAll.checked == true) {
                if (chkBoxCount[i].checked == false) {
                    if (chkBoxCount[i].id != "ctrl_FPSectional_chkCover_page" || chkBoxCount[i].id != "ctrl_FPSectional_chkTableContent")
                        chkBoxCount[i].checked = true;
                }
            }
            else if (checkAll.checked == false) {
                if (chkBoxCount[i].checked == true) {
                    if (chkBoxCount[i].id != "ctrl_FPSectional_chkCover_page" || chkBoxCount[i].id != "ctrl_FPSectional_chkTableContent")
                        chkBoxCount[i].checked = false;

                }
            }
        }

        return false;
    }

    function CustomerValidate(type) {
        if (type == 'pdf') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=2";
        } else {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=3";
        }

        setTimeout(function() {
            window.document.forms[0].target = '';
            window.document.forms[0].action = "ControlHost.aspx?pageid=FPSectional";
        }, 500);
        return true;

    }


//    function CalculateDiscountRate() {
//        var investmentRT = document.getElementById('<%=txtInvestmentReturn.ClientID %>').value;
//        var inflationRate = document.getElementById('<%=txtInflation.ClientID %>').value;
//        var dicountRate = (((1 + investmentRT) / (1 + inflationRate)) - 1) * 100;
//        document.getElementById('<%=txtDR.ClientID %>').value = dicountRate;
//        alert(dicountRate);      
//    }
 
</script>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<%--<telerik:RadScriptManager ID="RdScriptManager1" runat="server">
</telerik:RadScriptManager>--%>

<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Financial Planning Reports"></asp:Label>
<br />

<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record saved Successfully
            </div>
        </td>
    </tr>
</table>
<telerik:RadTabStrip ID="RadTabStripFPProjection" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="FPrepotsMulti" SelectedIndex="0" EnableViewState="true">
    <Tabs>
        <telerik:RadTab runat="server" Text="HLV Assumptions" Value="Assumptions" Selected="true" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Recommendation" Value="Customized_Text" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Report Generation" Value="Report_Generation" TabIndex="2">
        </telerik:RadTab>        
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik"
    EnableEmbeddedSkins="false">
</telerik:RadAjaxLoadingPanel>
<telerik:RadMultiPage ID="FPrepotsMulti" EnableViewState="true" runat="server" SelectedIndex="0">
 <telerik:RadPageView ID="RadPageView2" runat="server">
        <asp:Panel ID="pnlAssumption" runat="server">
        <br />
   <table>
   <tr><td></td><td></td><td></td>
   <td align="right">
       <asp:Label ID="lblInflation" runat="server" Text="Inflation(%) :" CssClass="FieldName"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="txtInflation" runat="server" CssClass="txtField"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" CssClass="FieldName" ControlToValidate="txtInflation"
                                        ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfInflation" runat="server" CssClass="FieldName" Display="Dynamic" ValidationGroup="vgSubmit" ControlToValidate="txtInflation" ErrorMessage="Please Enter Assumption"></asp:RequiredFieldValidator>
   </td>
   </tr>
      <tr><td></td><td></td><td></td>
   <td align="right">
       <asp:Label ID="lblInvestmentReturn" runat="server" Text="Investment Return(%) :"  CssClass="FieldName"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="txtInvestmentReturn" runat="server"  CssClass="txtField" 
           ontextchanged="txtInvestmentReturn_TextChanged" AutoPostBack="true"></asp:TextBox>
       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="FieldName" ControlToValidate="txtInvestmentReturn"
                                        ErrorMessage="Please Enter Numeric Value" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="FieldName" Display="Dynamic" ValidationGroup="vgSubmit" ControlToValidate="txtInvestmentReturn" ErrorMessage="Please Enter Assumption"></asp:RequiredFieldValidator>
   </td>
   </tr>
      <tr><td></td><td></td><td></td>
   <td align="right">
       <asp:Label ID="lblDR" runat="server" Text="Discount Rate(%) :" CssClass="FieldName"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="txtDR" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
   
                                        <br />
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="FieldName" Display="Dynamic" ValidationGroup="vgSubmit" ControlToValidate="txtDR" ErrorMessage="Please Enter Assumption"></asp:RequiredFieldValidator>
   </td>
   </tr></table><br /><table>
   <tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
     <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vgSubmit" CssClass="PCGButton" OnClick="btnSubmit_OnClick" />
  
      <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_OnClick" CssClass="PCGButton" /></td> </tr>
   </table>
   <%--<table>
   <tr>
   <td>
       <asp:Label ID="Label4" runat="server" Text="Discount Rate:"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="TextBox4" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
   </td>
   </tr>
   <tr>
   <td>
       <asp:Label ID="Label5" runat="server" Text="Retirement Age:"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="TextBox5" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
   </td>
   </tr>
   <tr>
   <td>
       <asp:Label ID="Label6" runat="server" Text="3rdAssumption:"></asp:Label>
   </td>
   <td>
       <asp:TextBox ID="TextBox6" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
   </td>
   </tr>
   </table>--%>
   

      </asp:Panel>
 </telerik:RadPageView>
 
 <telerik:RadPageView ID="RadPageView1" runat="server">
      
       <asp:Panel ID="pnlCustomizedtext" runat="server">
       <table width="100%">
       <tr>
       <td colspan="2">
       &nbsp;&nbsp;&nbsp;&nbsp;
       </td>
       </tr>
       
       <tr>
       <td align="right">
       <asp:Label ID="lblRMPara1" runat="server" Text="Paragraph One" CssClass="FieldName">
       </asp:Label>
       </td>    
       <td align="left">
       <asp:TextBox ID="txtParagraph1" runat="server" CssClass="txtField" Height="85px" Width="750px" TextMode="MultiLine"></asp:TextBox>
       </td>
       </tr>
       
       <tr>
       <td align="right">
       <asp:Label ID="lblRMPara2" runat="server" Text="Paragraph Two" CssClass="FieldName">
       </asp:Label>
       </td>    
       <td align="left">
       <asp:TextBox ID="txtParagraph2" runat="server" CssClass="txtField" Height="85px" Width="750px" TextMode="MultiLine"></asp:TextBox>
       </td>
       </tr>
       
       <tr>
       <td align="right">
       <asp:Label ID="lblRMPara3" runat="server" Text="Paragraph Three" CssClass="FieldName">
       </asp:Label>
       </td>    
       <td align="left">
       <asp:TextBox ID="txtParagraph3" runat="server" CssClass="txtField" Height="85px" Width="750px" TextMode="MultiLine"></asp:TextBox>
       </td>
       </tr>
       
       <tr>
       <td align="right">
       <asp:Label ID="lblRMPara4" runat="server" Text="Paragraph Four" CssClass="FieldName">
       </asp:Label>
       </td>    
       <td align="left">
       <asp:TextBox ID="txtParagraph4" runat="server" CssClass="txtField" Height="85px" Width="750px" TextMode="MultiLine"></asp:TextBox>
       </td>
       </tr>
       
       <tr>
       <td align="right">
       <asp:Label ID="lblRMPara5" runat="server" Text="Paragraph Five" CssClass="FieldName">
       </asp:Label>
       </td>    
       <td align="left">
       <asp:TextBox ID="txtParagraph5" runat="server" CssClass="txtField" Height="85px" Width="750px" TextMode="MultiLine"></asp:TextBox>
       </td>
       </tr>
       
       <tr>
       <td align="right">       
       &nbsp;&nbsp;&nbsp;&nbsp;
       </td>    
       <td align="left">
       <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSave_OnClick" />
       <asp:Button ID="btnEditRMRec" runat="server" Text="Edit" CssClass="PCGButton" 
               onclick="btnEditRMRec_Click" />        
       </td>
       </tr>
       
       </table>

      </asp:Panel>
      
      
      
 </telerik:RadPageView>
 
 <telerik:RadPageView ID="RadPageView3" runat="server">
        <asp:Panel ID="pnlReportGeneration" runat="server">

<table>
  <%--  <tr>
        <td colspan="3">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextSmall" Text="Financial Planning Reports"></asp:Label>
            <hr />
        </td>
        
    </tr>  --%>
    
      <tr>
      <td style="width:3%">
      </td> 
      <td >
      <input id="chkCheckAll"  name="Select All" value="Customer" type="checkbox" onclick="CheckBoxListSelect()" />
       <asp:Label ID="lblCheckAll" class="Field" Text="Check All" runat="server"></asp:Label>           
      </td>
      
      <td >
            
      </td>
      </tr>
      
      <tr>
      <td style="width:3%">
      </td> 
      <td colspan="2">
      <div id="Div1">
      <fieldset style="height: 30%; width: 50%;">
       <legend class="HeaderTextSmall">Select the sections to generate the report</legend>
       <table width="100%" id="tblFPsectinal">
        <tr>
            <td style="width:3%">
            </td> 
            <td style="width:44%;white-space:nowrap">
            <%-- <input type="checkbox" name="FPSectonal" value="1">Cover page<br>--%>
             <asp:CheckBox ID="chkCover_page" runat="server" CssClass="cmbField" Text="Cover page" Checked="true" Enabled="false" />
            </td>
            <td style="width:50%;white-space:nowrap">
             <asp:CheckBox ID="chkIncome_Expense" runat="server" CssClass="cmbField" Text="Income and Expense Summary" />
            </td>
            
        </tr>    
      
         <tr>
            <td style="width:3%">
            </td> 
            <td style="width:44%;white-space:nowrap">
             <asp:CheckBox ID="chkRM_Messgae" runat="server" CssClass="cmbField" Text="RM Message" />
            </td>
            <td style="width:50%;white-space:nowrap">
             <asp:CheckBox ID="chkCash_Flows" runat="server" CssClass="cmbField" Text="Cash Flows" />
            </td>
         </tr>
         
         
         <tr>
    <td style="width:3%">
    </td> 
    <td style="width:44%;white-space:nowrap">
     <asp:CheckBox ID="chkImage" runat="server" CssClass="cmbField" Text="Image" /> 
    </td>
    <td style="width:50%;white-space:nowrap">
   <asp:CheckBox ID="chkNetWorthSummary" runat="server" CssClass="cmbField" Text="Net Worth Summary" />
    </td>
    </tr>
    
      
    <tr>
    <td style="width:3%">
    </td> 
    <td style="width:44%;white-space:nowrap">
     <asp:CheckBox ID="chkTableContent" runat="server" CssClass="cmbField" Text="Table of Content" Enabled="false" /> 
    </td>
    <td style="width:50%;white-space:nowrap">
    <asp:CheckBox ID="chkRiskProfile" runat="server" CssClass="cmbField" Text="Risk profile & Portfolio allocation" />
    </td>
    </tr>
    
      
    <tr>
    <td style="width:3%">
    </td> 
    <td style="width:44%;white-space:nowrap">
     <asp:CheckBox ID="chkFPIntroduction" runat="server" CssClass="cmbField" Text="FP Introduction" />
    </td>
    <td style="width:50%;white-space:nowrap">
     <asp:CheckBox ID="chkInsurance" runat="server" CssClass="cmbField" Text="Life Insurance" />  
    </td>
    </tr>
   
      
    <tr>
    <td style="width:3%">
    </td> 
   <td style="width:44%;white-space:nowrap">
     <asp:CheckBox ID="chkProfileSummary" runat="server" CssClass="cmbField" Text="Profile Summary" />    
    </td>
    <td style="width:50%;white-space:nowrap">
     <asp:CheckBox ID="chkGeneralInsurance" runat="server" CssClass="cmbField" Text="General Insurance" />
    </td>
    </tr>
    
    
    
      
    <tr>
    <td style="width:3%">
    </td> 
    <td style="width:44%;white-space:nowrap">
    <asp:CheckBox ID="chkFinancialHealth" runat="server" CssClass="cmbField" Text="Financial Health" /> 
    </td>
    <td style="width:50%;white-space:nowrap">
    <asp:CheckBox ID="chkCurrentObservation" runat="server" CssClass="cmbField" Text="Current Situation and Observations" />    
    </td>
    </tr>  
    
    
    <tr>
    <td style="width:3%">
    </td> 
    <td style="width:44%;white-space:nowrap">
    <%-- <asp:CheckBox ID="chkKeyAssumptions" runat="server" CssClass="cmbField" Text="Key Assumptions" />  --%>
    <asp:CheckBox ID="chkGoalProfile" runat="server" CssClass="cmbField" Text="Goal Profiling" />      
    </td>
    <td style="width:50%;white-space:nowrap">
   <asp:CheckBox ID="chkDisclaimer" runat="server" CssClass="cmbField" Text="Disclaimer" />   
    </td>
    </tr>   
    
    <tr>
    <td style="width:3%">
    </td> 
    <td style="width:44%;white-space:nowrap">
    <%-- <asp:CheckBox ID="chkKeyAssumptions" runat="server" CssClass="cmbField" Text="Key Assumptions" />  --%>
   <%-- <asp:CheckBox ID="CheckBox1" runat="server" CssClass="cmbField" Text="Goal Profiling" />   --%>   
    </td>
    <td style="width:50%;white-space:nowrap">
     <asp:CheckBox ID="chkNotes" runat="server" CssClass="cmbField" Text="Notes" /> 
    </td>
    </tr>   
    
         
       </table>
      </fieldset>
      </div>
      </td>
      </tr>
    
    
    <tr>
    <td style="width:3%">
    </td> 
    <td style="width:50%">
    <asp:Button ID="btnViewReport" runat="server" Text="View Report" 
     PostBackUrl="~/Reports/Display.aspx?mail=0" CssClass="PCGMediumButton" />&nbsp;&nbsp;
     <asp:Button ID="btnViewInPDF" runat="server" Text="Export To PDF" OnClientClick="return CustomerValidate('pdf')"
     PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PCGMediumButton" />
    </td>
    <td style="width:50%">
     
    </td>
    </tr>
 </table>
      </asp:Panel>
 </telerik:RadPageView>
 </telerik:RadMultiPage>
 
 
 
 