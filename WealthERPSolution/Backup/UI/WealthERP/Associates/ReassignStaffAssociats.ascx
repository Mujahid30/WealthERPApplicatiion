<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReassignStaffAssociats.ascx.cs"
    Inherits="WealthERP.Associates.ReassignStaffAssociats" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function GetStaffCode(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= txtStaffId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
</script>

<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Staff Re-Assign
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr id="tr1" runat="server" visible="true">
        <td style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Select Reporting Manager
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="right">
            <asp:Label ID="lblUser" runat="server" CssClass="FieldName" Text="User Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlStaff" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlStaff_OnSelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Staff" Value="Staff"></asp:ListItem>
                <asp:ListItem Text="Associate" Value="Associates"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblChannel" runat="server" CssClass="FieldName" Text="Channel:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlChannel" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlChannel_OnSelectedIndexChanged"
                AutoPostBack="true" Width="230px">
            </asp:DropDownList>
            <span id="Span27" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvddlChannel" runat="server" ErrorMessage="Please Select Channel"
                CssClass="rfvPCG" ControlToValidate="ddlChannel" ValidationGroup="btnbasicsubmit"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblTitleChannel" runat="server" Text="Title" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 260px;">
            <asp:DropDownList ID="ddltitlechannelId" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddltitlechannelId_OnSelectedIndexChanged" Width="230px">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvddltitlechannelId" runat="server" ErrorMessage="Please Select Title"
                CssClass="rfvPCG" ControlToValidate="ddltitlechannelId" ValidationGroup="btnbasicsubmit"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td align="right" style="width: 150px;">
            <asp:Label ID="lblNewReporting" runat="server" CssClass="FieldName" Text="New Reporting Manager:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNewReporting" runat="server" CssClass="txtField" AutoPostBack="false"
                AutoComplete="Off" Width="250px" TabIndex="2" OnTextChanged="txtNewReporting_OnTextChanged"></asp:TextBox>
            <span id="Span1" class="spnRequiredField"></span>
            <cc1:TextBoxWatermarkExtender ID="txtWaterMarktxtNewReporting" TargetControlID="txtNewReporting"
                WatermarkText="Enter few characters Reporting Manager" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtNewReporting_autoCompleteExtender" runat="server"
                TargetControlID="txtNewReporting" ServiceMethod="GetRMStaffList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="1" CompletionInterval="0"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetStaffCode" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNewReporting"
                ErrorMessage="<br />Please Enter Reporting Manager" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
        <%--  <asp:DropDownList ID="ddlReportingManager" runat="server" CssClass="cmbField">OnClientItemSelected="txtStaffId"
            </asp:DropDownList>--%>
    </tr>
</table>
<table width="100%">
    <tr id="tr4" runat="server" visible="true">
        <td style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Select Staff Reporting to Manager
            </div>
        </td>
    </tr>
</table>
<table>
    <tr style="margin-top: 0px;">
        <td align="right">
            &nbsp;&nbsp
            <asp:Label ID="lblTitle" runat="server" Text="Reporting Manager:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlTitle" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlTitle_OnSelectedIndexChanged"
                AutoPostBack="true" Width="200px">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvddlTitle" runat="server" ErrorMessage="Please Select Title"
                CssClass="rfvPCG" ControlToValidate="ddlTitle" ValidationGroup="btnbasicsubmit"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Label ID="lblOldReportingManager" runat="server" CssClass="FieldName" Text="Source Reporting Manager"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblSelectBranch" runat="server" CssClass="FieldName" Text="Existing Staff">
            </asp:Label>
        </td>
        <td></td>
        <td>
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Mapped Staff">
            </asp:Label>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="chkSourceStaff" runat="server" Text="Check All" OnCheckedChanged="chkSourceStaff_OnCheckedChanged"
                CssClass="FieldName" AutoPostBack="true" Visible="false" />
        </td>
        <td>
            <asp:CheckBox ID="ChkExistingStaff" runat="server" Text="Check All" OnCheckedChanged="ChkExistingStaff_OnCheckedChanged"
                CssClass="FieldName" AutoPostBack="true" Visible="false" />
        </td>
        <td>
            <asp:CheckBox ID="chkNewStaff" runat="server" Text="Check All" OnCheckedChanged="chkNewStaff_OnCheckedChanged"
                CssClass="FieldName" AutoPostBack="true" Visible="false" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
       
            <asp:ListBox ID="radStaffList" runat="server" Height="200px" Width="250px" SelectionMode="Multiple" />
          
   <asp:ImageButton ID="btnStaffList" ImageUrl="~/Images/rightArrow1.png" runat="server" ImageAlign="Top" 
                Height="25px" Width="25px" OnClick="btnStaffList_Click" ToolTip="To Right"></asp:ImageButton>
         
            
          
        </td>
        <td>
    <asp:ListBox ID="ExistingStaffList" runat="server" Height="200px" Width="250px" SelectionMode="Multiple" />

          
            
        </td>
         <td>
                 <table>
                   <tr>
                     <td>
                  <asp:Button ID="RightArrow" runat="server" Text=">" Width="45px" onclick="RightArrow_Click"  />
                      </td>
                      </tr>
                      <tr>
                <td>
                <asp:Button ID="LeftArrow" runat="server" Text="<" Width="45px" onclick="LeftArrow_Click" />
                   </td>
                   </tr>
                  <tr>
                   <td>
             <asp:Button ID="RightShift" runat="server" Text=">>" Width="45px" onclick="RightShift_Click" />
                   </td>
                  </tr>
              <tr>
                   <td>
               <asp:Button ID="LeftShift" runat="server" Text="<<" Width="45px" onclick="LeftShift_Click" />
           </td>
                  </tr> 
                  </table>
                  </td>
                  
        <td>
                            <asp:ListBox ID="MappedStaffList" runat="server" Height="200px" Width="250px" SelectionMode="Multiple" />

         
        </td>
    </tr>
    <tr visible="True">
        <div class="clearfix" style="margin-bottom: 1em;">
            <asp:Panel ID="PLStaff" runat="server" Style="float: left; padding-left: 150px;"
                Visible="true">
            </asp:Panel>
        </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_OnClick"
                ValidationGroup="btnbasicsubmit" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="txtStaffId" runat="server" />
