<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FPSectional.ascx.cs" Inherits="WealthERP.Reports.FPSectional" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
 
</script>

<table border="0" width="100%">
    <tr>
        <td colspan="3">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextSmall" Text="Financial Planning Reports"></asp:Label>
            <hr />
        </td>
        
    </tr>  
    
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
     <asp:CheckBox ID="chkInsurance" runat="server" CssClass="cmbField" Text="Insurance Details" />  
    </td>
    </tr>
   
      
    <tr>
    <td style="width:3%">
    </td> 
   <td style="width:44%;white-space:nowrap">
     <asp:CheckBox ID="chkProfileSummary" runat="server" CssClass="cmbField" Text="Profile Summary" />    
    </td>
    <td style="width:50%;white-space:nowrap">
     <asp:CheckBox ID="chkCurrentObservation" runat="server" CssClass="cmbField" Text="Current Situation and Observations" /> 
    </td>
    </tr>
    
    
    
      
    <tr>
    <td style="width:3%">
    </td> 
    <td style="width:44%;white-space:nowrap">
    <asp:CheckBox ID="chkFinancialHealth" runat="server" CssClass="cmbField" Text="Financial Health" /> 
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
    <asp:CheckBox ID="chkGoalProfile" runat="server" CssClass="cmbField" Text="Goal Profiling" />      
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
 
 
 
 