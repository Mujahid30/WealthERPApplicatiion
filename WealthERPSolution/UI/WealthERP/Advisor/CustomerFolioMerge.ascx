<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFolioMerge.ascx.cs" Inherits="WealthERP.Advisor.CustomerFolioMerge" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<asp:ScriptManager ID="scptMgr" runat="server" EnablePartialRendering="true">
 <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>


<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };
</script>



<script type="text/javascript">

    function CheckFolioSelected() {
         var Count = 0;        
        Parent = document.getElementById("<%=gvCustomerFolioMerge.ClientID %>");
        var items = Parent.getElementsByTagName('input');
        for (i = 0; i < items.length; i++) {
            if (items[i].checked) {
                Count++
            }
        }
        if (Count == 0) {
            alert("Please select a folio");
            return false;
        }
    }

    function CheckOtherIsCheckedByGVID(spanChk) {
    
        var IsChecked = spanChk.checked;
        var CurrentRdbID = spanChk.id;
        var Chk = spanChk;
        //var n = document.getElementById("gvCustomerFolioMerge").rows.length;
        Parent = document.getElementById("<%=gvCustomerFolioMerge.ClientID %>");
        var items = Parent.getElementsByTagName('input');
        for (i = 0; i < items.length; i++) {
            if (items[i].id != CurrentRdbID && items[i].type == "checkbox") {
                    if (items[i].checked) {
                    items[i].checked = false;
                    alert("Please select one customer at a time.");
                }
            }
        }
    }          
</script>

<link href="/CSS/colorbox.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.CheckField
{
    font-family: Verdana,Tahoma;
    font-weight: normal;
    font-size: 11px;
    color: #16518A;
    
}
</style>

<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Customer Folio"></asp:Label>
            <hr />
        </td>
    </tr>
   
</table>


<table width="70%">
<tr>
<td style="width:100px"></td>
<td style="width:120px" align="right">
    <asp:Label ID="lblAction" runat="server" Text="Action:" CssClass="FieldName"></asp:Label></td>
<td style="width:300px">
    <asp:DropDownList ID="ddlMovePortfolio" runat="server" CssClass="cmbLongField" AutoPostBack="true"
        onselectedindexchanged="ddlMovePortfolio_SelectedIndexChanged">
        <asp:ListItem Value="S">Select</asp:ListItem>
        <asp:ListItem Value="Merge">Folio merge</asp:ListItem>
        <asp:ListItem Value="MtoAC">Folio transfer to another customer</asp:ListItem>
        <asp:ListItem Value="MtoAP">Folio Transfer to another portfolio</asp:ListItem>
    </asp:DropDownList>
</td>
<td style="width:200px">
        <%--<asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" OnClientClick="return CheckFolioSelected();"
        onclick="btnGo_Click" />--%>
    </td>
</tr>

<tr id="trMergeToAnotherAMC" runat="server" visible="false">
        <td style="width:100px"></td>
        <td align="right">
            <asp:Label ID="lblMergeTo" Text="Merge to folio:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAdvisorBranchList" runat="server" CssClass="cmbLongField">
            <asp:ListItem Value="0">Select</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnmerge" CssClass="PCGButton" runat="server" ValidationGroup="vgBtn" OnClick="btnEdit_Click" Text="Submit" OnClientClick="return CheckFolioSelected();"/>
          <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="vgBtn" ControlToValidate="ddlAdvisorBranchList" ValueToCompare="null" ErrorMessage="Field is required" Operator="NotEqual" ></asp:CompareValidator>--%>
        </td>        
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td>
        <asp:Label ID="lblerror" Text="No Folios to merge" CssClass="rfvPCG" Visible="false" runat="server"></asp:Label> 
        </td>
    </tr>
    
    <tr id="trPickCustomer" runat="server" visible="false">
        <td></td>
        <td align="right">
        <asp:Label ID="lblPickCustomer" Text="Customer:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
        <%--<asp:TextBox ID="txtPickCustomer" runat="server" CssClass="txtField" OnValueChanged="txtPickCustomer_ValueChanged" ></asp:TextBox>--%>

              <asp:TextBox ID="txtPickCustomer" runat="server" CssClass="txtLongAddField" 
              AutoComplete="Off"  AutoPostBack="True" 
              ontextchanged="txtPickCustomer_TextChanged" ></asp:TextBox>
              <cc1:TextBoxWatermarkExtender ID="txtPickCustomer_water" TargetControlID="txtPickCustomer" WatermarkText="Enter few characters"
              runat="server" EnableViewState="false"></cc1:TextBoxWatermarkExtender>
              <ajaxToolkit:AutoCompleteExtender ID="txtPickCustomer_autoCompleteExtender" runat="server"
              TargetControlID="txtPickCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
              MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
              CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
              CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
              UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters="" Enabled="True"  />
              
              <span id="Span1" class="spnRequiredField">*</span>
              <%--<span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                few characters of customer name.</span>--%>
                </td>   
        <td>
                <asp:RequiredFieldValidator ID="rfvPickCustomer" ControlToValidate="txtPickCustomer" ErrorMessage="Please select a customer."
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">
                </asp:RequiredFieldValidator>
            </td>
    </tr>

    <tr id="trPickPortfolio" runat="server" visible="false">
        <td style="width:100px"></td>
        <td align="right">
        <asp:Label ID="lblPickPortfolio" Text="Portfolio:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbLongField">
                <asp:ListItem Value="0">Select </asp:ListItem>
            </asp:DropDownList>                
            <span id="Span2" class="spnRequiredField">*
            </span>                
        </td>
        <td>
            <span id="Span4" class="spnRequiredField">
            <span id="Span3" class="spnRequiredField">
         <asp:RequiredFieldValidator ID="rfvddlPortfolio" ControlToValidate="ddlPortfolio" ErrorMessage="Please pick a portfolio"
        Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">
        </asp:RequiredFieldValidator>
            </span>                
            </span>                
        </td>                
    </tr>
   
    <tr id="trBtnSubmit" runat="server" visible="false">
        <td>
        </td>
        <td>
        </td>
        <td>
            <asp:Button ID="btnSubmitPortfolio" CssClass="PCGButton" runat="server" ValidationGroup="btnSubmit" OnClientClick="return CheckFolioSelected();"
            Text="Submit" onclick="btnSubmitPortfolio_Click"/>
        </td>
        <td></td>
        
    </tr>
</table>


<table id="ErrorMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage1" runat="server" visible="true" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>

  <table width="100%">
    <tr id="trFolioStatus" runat="server">
        <td align="center">
            <div id="msgFolioStatus" runat="server" class="success-msg" align="center">
                Folio Moved Successfully
            </div>
        </td>
    </tr>
    <tr id="trMergeFolioStatus" runat="server">
        <td align="center">
            <div id="msgMergeFolio" runat="server" class="success-msg" align="center">
                Folio Merged Successfully
            </div>
        </td>
    </tr>
  </table>

<table width="100%">
 <td class="leftField" align="right">
             
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
     </td>
     <tr>
        <td>
            <asp:Label ID="Label1" class="Field" Text="Note: Select the folios below that needs to be transferred/merged." runat="server"></asp:Label>
        </td>
     </tr>

  <tr>
        <td colspan="3" allign="center" >
            <asp:GridView ID="gvCustomerFolioMerge" runat="server" CellPadding="4" CssClass="GridViewStyle"
                AllowSorting="True"  ShowFooter="true" AutoGenerateColumns="False"
                DataKeyNames="CustomerId,AMCCode,Count,portfilionumber" 
                onselectedindexchanged="gvCustomerFolioMerge_SelectedIndexChanged">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle " />
                <Columns>
                
                 <asp:TemplateField HeaderText="Select">
                     <HeaderTemplate>
                            <asp:Label ID="LblSelect" runat="server" Text=""></asp:Label>
                            <br />
                            <%--<asp:Button ID="lnkSelectAll" Text="All" runat="server"  OnClientClick="return CheckAll();" />--%>
                           <%--<input id="chkBoxAll" class="CheckField" name="CheckAllCustomer" value="Customer" type="checkbox"  />--%>
                        </HeaderTemplate>
                        <ItemTemplate>

                     <asp:checkbox ID="rdbGVRow" oncheckedchanged="rdbGVRow_CheckedChanged" onclick="javascript:CheckOtherIsCheckedByGVID(this);"  runat="server" AutoPostBack="true" />

                  </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" Text="Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCustNameSearch" Width="50%" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_AdvisorCustomerAccounts_btnCustomerSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                           <asp:HyperLink ID="hypCustomerName"  runat="server" OnClick="lnkCustomerName_Click" Text='<%# Eval("CustomerName").ToString() %>'>HyperLink</asp:HyperLink>                         
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblAMCName" runat="server" Text="AMC Name"></asp:Label>                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAMC" runat="server" Text='<%# Eval("AMCName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblcount" runat="server" Text="Folios"></asp:Label><br />
                            <asp:TextBox ID="txtFolioSearch" Width="50%" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_AdvisorCustomerAccounts_btnFolioNumberSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                         <asp:LinkButton ID="hypFolioNo" runat="server" CssClass="CmbField" OnClick="hypFolioNo_Click" Text='<%# Eval("Count").ToString() %>'>
                         </asp:LinkButton>
                           <%-- <asp:Label ID="lblFolioCount" runat="server" Text='<%# Eval("Count").ToString() %>'></asp:Label>--%>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblmergeStatus" runat="server" Text="Merged To"></asp:Label>                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblmergerstatus" ItemStyle-HorizontalAlign="Right" runat="server" Text='<%# Eval("mergerstatus").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>                 
                    
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>

<table width="100%">
 <tr style="width:100%">
    <td colspan="3">
        <table width="100%">
            <tr id="trPager" runat="server" width="100%" >
                <td align="right">
                    <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                </td>
               
            </tr>
        </table>
    </td>
 </tr>
  
 <tr id="trReassignBranch" runat="server">
        <td class="SubmitCell" align="left">
       &nbsp
       &nbsp
       &nbsp
            </td>
      
        
        <td>
        &nbsp
        &nbsp
            <%--<span id="spanAdvisorBranch" class="spnRequiredField" runat="server">*</span>--%>
            
        </td>
        <td>
        &nbsp
        &nbsp
        </td>
    </tr>
    </table>




<%--  <table>
    <tr>
        <td style="width:150px">
            &nbsp;</td>
        <td>    
        <asp:RadioButton ID="rdbMerge" runat="server" Text="Merge" GroupName="FolioMove" Class="FieldName" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:RadioButton ID="rdbMoveFolioToCustomer" runat="server" GroupName="FolioMove" Class="FieldName"
            Text="Move Folio to another Customer" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:RadioButton ID="rdbMoveFolioToPortfolio" runat="server" Checked="true" GroupName="FolioMove" Class="FieldName"
            Text="Move Folio to another Portfolio"/>
        </td>
    </tr>
    
    <tr>
        <td></td>
        <td></td>
    </tr>
  </table>--%>

  

<asp:HiddenField ID="hdnCustomerId" runat="server" Value="0" OnValueChanged="txtPickCustomer_TextChanged" />     
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:Button ID="btnCustomerSearch" runat="server" Text="" onclick="btnCustomerSearch_Click" BorderStyle="None" BackColor="Transparent"/> 
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnBranchFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:Button ID="btnFolioNumberSearch" runat="server" Text="" OnClick="btnFolioNumberSearch_Click" BorderStyle="None" BackColor="Transparent" />