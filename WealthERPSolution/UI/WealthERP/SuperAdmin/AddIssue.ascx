<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddIssue.ascx.cs" Inherits="WealthERP.SuperAdmin.AddIssue" %>
<style type="text/css">
    .blinkColors
    {
       font-size:small;
    }
</style>
<%--<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddIssueTracker.ascx.cs" Inherits="WealthERP.SuperAdmin.AddIssueTracker" %>--%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:ScriptManager ID="smanager" runat="server"></asp:ScriptManager>




<script type="text/javascript">
    function ValidateStauts(Source, args) {        
        var status = document.getElementById('<%= ddlIssueStatus.ClientID %>').value;
        var txtDate = document.getElementById('<%= dtSolveDate.ClientID %>').value;
        if (status == 3) {
            if (txtDate == "")
                args.IsValid = false;
            else
                args.IsValid = true;
        }        
    } 
</script>






<table>
<tr>
    <td><asp:Label ID="Label5" CssClass="HeaderTextBig" runat="server" Text="Customer Issue Details"></asp:Label></td>
</tr>
</table>

<hr />
<table id="tblErrorMassage" width="100%" visible="false" cellpadding="0" cellspacing="0" runat="server">
    <tr>
        <td align="center">
            <div class="success-msg" id="ErrorMessage" runat="server" align="center">
            </div>
        </td>
    </tr>
 </table>
<table id="tblAdvisor">
    <tr>
        <td class="leftField">  
            <asp:Label ID="Label1" class="FieldName" Text="Advisor :" runat="server" />
        </td>
        <td class="rightField">
            <asp:DropDownList CssClass="cmbField" ID="ddlAdviser" runat="server" 
                onselectedindexchanged="ddlAdviser_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <span class="spnRequiredField">*</span>
             <br />     
            <asp:CompareValidator ID="ddlAdviser_CompareValidator" ValidationGroup="vgBtnSubmit" runat="server" ControlToValidate="ddlAdviser"
                ErrorMessage="Please select an Advisor" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG">
            </asp:CompareValidator>
        </td>
        <td class="leftField"> 
            
            <asp:Label ID="lblAdviserPhoneNumber" class="FieldName" Text="Phone :" runat="server" />
               
            </td>
        <td  class="rightField">
            <asp:Label ID="AdviserPhoneNo" class="FieldName" runat="server"></asp:Label>
        </td>
        <td class="lefttField">  
            <asp:Label ID="lblAdviserEmailId" class="FieldName" Text="Email :" runat="server" />
        </td>
        <td class="rightField">
            <asp:Label runat="server"  class="FieldName" ID="AdviserEmail"></asp:Label>
        </td>
        <td></td>
        <td class="leftField"><asp:Label Text="Issue Status :" ID="lblIssueStatus" runat="server"  class="FieldName" ></asp:Label></td>
        <td class="rightField"><asp:Label Font-Bold="true"  CssClass="blinkColors" ID="lblOpenClose" runat="server" ></asp:Label></td>
    </tr>
     <tr>
        <td class="leftField" valign="top">  
            <asp:Label ID="Label2" class="FieldName" Text="Contact Person :" runat="server" />
        </td>
        <td class="rightField">
            <asp:TextBox CssClass="txtField" ID="txtCustomerName" runat="server"></asp:TextBox>
            <span class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ErrorMessage="Please Enter Contact Person" ValidationGroup="vgBtnSubmit" ControlToValidate="txtCustomerName" CssClass="cvPCG"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField">  
            <asp:Label ID="Label3" class="FieldName" Text="Phone :" runat="server" />
        </td>
        <td  class="rightField">
            <asp:TextBox ID="txtCustomerPhone" CssClass="txtField" runat="server"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator Display="Dynamic" ValidationExpression="^((\\+91-?)|0)?[0-9]{10}$" runat="server" CssClass="cvPCG" ErrorMessage="Enter A Valid Phone" ControlToValidate="txtCustomerPhone" ValidationGroup="vgBtnSubmit"></asp:RegularExpressionValidator>
        </td>
        <td class="lefttField" align="right">  
            <asp:Label ID="Label4" class="FieldName" Text="Email :" runat="server" />
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCustomerEmail" CssClass="txtField" runat="server"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator Display="Dynamic" runat="server" CssClass="cvPCG" ControlToValidate="txtCustomerEmail" ErrorMessage="Enter A Valid EMail" ValidationExpression="^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$" ValidationGroup="vgBtnSubmit"></asp:RegularExpressionValidator>
        </td>
        <td></td>
         <td class="leftField"><asp:Label class="FieldName" ID="lblLevel" Text="Active :" 
                 runat="server" ></asp:Label></td>
         <td class="rightField"><asp:Label ID="lblTypes" Font-Bold="true" CssClass="blinkColors" runat="server"></asp:Label></td>
    </tr>
    
    <tr>
        <td class="leftField"><asp:Label class="FieldName" runat="server" Text="Select Functionality :" ID="lblRole"></asp:Label></td>
        <td class="rightField"><asp:DropDownList CssClass="cmbField" runat="server" 
                ID="ddlRole" AutoPostBack="true" 
                onselectedindexchanged="ddlRole_SelectedIndexChanged"></asp:DropDownList>
                <span class="spnRequiredField">*</span>
                <br />     
            <asp:CompareValidator ID="CompareValidator1" ValidationGroup="vgBtnSubmit" runat="server" ControlToValidate="ddlRole"
                ErrorMessage="Please select a Role" Operator="NotEqual" ValueToCompare="Select User Role"
                Display="Dynamic" CssClass="cvPCG">
            </asp:CompareValidator>
        </td>
        <td class="leftField"></td>
        <td class="rightField">
                <asp:DropDownList CssClass="cmbField" runat="server" 
                ID="ddlTreeNode" AutoPostBack="true" 
                onselectedindexchanged="ddlTreeNode_SelectedIndexChanged">
                </asp:DropDownList>
                <span class="spnRequiredField">*</span>
                <br />     
            <asp:CompareValidator ID="CompareValidator2" ValidationGroup="vgBtnSubmit" runat="server" ControlToValidate="ddlTreeNode"
                ErrorMessage="Please select a Tree Node" Operator="NotEqual" ValueToCompare="Select Tree Node"
                Display="Dynamic" CssClass="cvPCG">
            </asp:CompareValidator>
                
        </td>
        <td class="leftField"></td>
        <td class="rightField"><asp:DropDownList CssClass="cmbField" runat="server" 
                ID="ddlSubNode" AutoPostBack="true" 
                onselectedindexchanged="ddlSubNode_SelectedIndexChanged">
                </asp:DropDownList><br />
                <%--<asp:CompareValidator ID="CompareValidator3" ValidationGroup="vgBtnSubmit" runat="server" ControlToValidate="ddlSubNode"
                ErrorMessage="Please select a Sub Node" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG">
                </asp:CompareValidator>--%>
        </td>
        <td class="leftField"></td>
        <td class="rightField">
                <asp:DropDownList CssClass="cmbField" runat="server" ID="ddlSubSubNode">
                </asp:DropDownList><br />
                <%--<asp:CompareValidator ID="CompareValidator4" ValidationGroup="vgBtnSubmit" runat="server" ControlToValidate="ddlSubSubNode"
                ErrorMessage="Please select a SubSubNode" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG">
                </asp:CompareValidator>--%>
        </td>
    </tr>
</table>
   
<table id="tblAdvisorSecondPanel">
<tr>
    
    <td class="leftField" valign="top"><asp:Label class="FieldName" ID="Label6" Text="Description :" runat="server"></asp:Label></td>
    <td class="rightField"><asp:TextBox CssClass="txtField" ID="txtDescription" 
            runat="server" TextMode="MultiLine"  Width="300px" Height="100px" 
            ></asp:TextBox>
            <span style="vertical-align:top" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator CssClass="cvPCG" Display="Dynamic" ControlToValidate="txtDescription" ErrorMessage="Enter The Description" ValidationGroup="vgBtnSubmit" runat="server"></asp:RequiredFieldValidator>
            </td> 
              
</tr>
</table>
<table id="tblAdvisorThirdPanel">
    <tr>
        <td class="leftField">
            <asp:Label ID="lblIssueNo" class="FieldName" Text="Issue No :" runat="server"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox CssClass="txtField" ID="txtIssueCode" runat="server"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label ID="lblIssueDate" class="FieldName" Text="Issue Added Date :" runat="server"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox runat="server"  CssClass="txtField" ID="txtIssueDate"></asp:TextBox>
            <cc1:CalendarExtender ID="txtIssueDate_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtIssueDate">
            </cc1:CalendarExtender>
        </td>
        <td><asp:Label class="FieldName" ID="lblReportedDate" Text="Reported Date :" runat="server"></asp:Label></td>
                            <td>
                 <telerik:RadDatePicker ID="txtReportedDate"   CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                        Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                 </telerik:RadDatePicker>
                 <span class="spnRequiredField">*</span>
                 <br />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vgBtnSubmit" runat="server" CssClass="cvPCG" ErrorMessage="Enter A Date" Display="Dynamic" ControlToValidate="txtReportedDate"></asp:RequiredFieldValidator>
                 
                    </td>
                <td><asp:Label ID="lblSolveDate" runat="server" Text="Closed Date" class="FieldName"></asp:Label></td>
                <td>
                    <telerik:RadDatePicker ID="dtSolveDate"   CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                        Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                 </telerik:RadDatePicker>
                 <br />
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateStauts"
                     ErrorMessage="Please Select a Closed Date" CssClass="cvPCG" ValidationGroup="vgBtnSubmit" Display="Dynamic"></asp:CustomValidator>
                </td>
    </tr>
    <tr>
    <td class="leftField"><asp:Label class="FieldName" ID="lblIssueType" Text="Issue Type :" runat="server"></asp:Label></td>
    <td class="rightField"><asp:DropDownList CssClass="cmbField" ID="ddlIssueType" runat="server">
        </asp:DropDownList>
        <span class="spnRequiredField">*</span>
        <br />     
            <asp:CompareValidator ID="CompareValidator5" ValidationGroup="vgBtnSubmit" runat="server" ControlToValidate="ddlIssueType"
                ErrorMessage="Please select an Issue Type" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG">
            </asp:CompareValidator>    
    </td>
    <td class="leftField"><asp:Label class="FieldName" ID="lblReportedVia" Text="Reported Via:" runat="server"></asp:Label></td>
    <td class="rightField"><asp:DropDownList CssClass="cmbField" ID="ddlReportedBy" runat="server">
        <asp:ListItem>Select</asp:ListItem>
        <asp:ListItem>Phone </asp:ListItem>
        <asp:ListItem>Email</asp:ListItem>
        <asp:ListItem>Visit</asp:ListItem>
        <asp:ListItem>Skype</asp:ListItem>
        <asp:ListItem>Others</asp:ListItem>
        </asp:DropDownList>
        <span class="spnRequiredField">*</span>
        <br />     
            <asp:CompareValidator ID="CompareValidator6" ValidationGroup="vgBtnSubmit" runat="server" ControlToValidate="ddlReportedBy"
                ErrorMessage="Please select Reported Via" Operator="NotEqual" ValueToCompare="Select"
                Display="Dynamic" CssClass="cvPCG">
            </asp:CompareValidator>     
    </td>
        <td  class="leftField"><asp:Label class="FieldName" ID="Label8" Text="Priority :" runat="server"></asp:Label></td>
        <td  class="rightField">    
        <asp:DropDownList CssClass="cmbField" ID="ddlCustomerPriority" runat="server"></asp:DropDownList></td>
        <td class="leftField"><asp:Label class="FieldName" runat="server" ID="lblVersionReadOnly" Text="Release :"></asp:Label></td>
        <td><asp:TextBox ID="txtVersionReadOnly" CssClass="txtField" runat="server"></asp:TextBox></td>

<%--        <td colspan="5">
        <td colspan="3"></td>
--%></tr>
</table>

<br />
<hr />
<table>
<tr>
    <td><asp:Label ID="lblLevel1" CssClass="HeaderTextBig" runat="server" Text="Level 1(Customer Support )"></asp:Label></td>
</tr>
</table>

<table>




</table>
<table>
            
            <tr>

                <td ><asp:Label class="FieldName" ID="lblComments" Text="Comments :" runat="server"></asp:Label></td>
                <td ><asp:TextBox CssClass="txtField" ID="txtComments" runat="server" TextMode="MultiLine"  Width="300px" Height="100px"></asp:TextBox>
                <span style="vertical-align:top" class="spnRequiredField">*</span>
                <br />
                <asp:RequiredFieldValidator runat="server" CssClass="cvPCG" ControlToValidate="txtComments" ErrorMessage="Enter Your Comments" ValidationGroup="vgBtnSubmit"></asp:RequiredFieldValidator>
                </td>
                <td valign="top">
                    <table>
                    <tr>
                            <td class="leftField"><asp:Label class="FieldName" ID="lblReportedBy" Text="Author :" runat="server"></asp:Label></td>
                            <td><asp:TextBox CssClass="txtField" ID="txtReportedBy" runat="server"></asp:TextBox>
                            <span class="spnRequiredField">*</span>
                            <br />
                            <asp:RequiredFieldValidator runat="server" CssClass="cvPCG" ErrorMessage="Enter Your Name" Display="Dynamic" ControlToValidate="txtReportedBy" ValidationGroup="vgBtnSubmit"></asp:RequiredFieldValidator>
                            </td>
                             <td class="leftField"><asp:Label class="FieldName" runat="server" ID="lblChkCSClose" Text="Status :"></asp:Label></td>
                <td class="rightField"><asp:DropDownList OnSelectedIndexChanged="SetToFirstLevel" runat="server" ID="ddlIssueStatus" 
                        CssClass="cmbField" AutoPostBack="true" >
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Open</asp:ListItem>
                    <asp:ListItem>Close</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
                         
                    </tr>
                        <tr>
                <td class="leftField"><asp:Label  class="FieldName" ID="lblReportTolevel2" Text="Escalate :" runat="server"></asp:Label></td>
                <td class="rightField"><asp:DropDownList ID="ddlReportFromCS" runat="server" CssClass="cmbField" >
                    </asp:DropDownList>
                </td>
                <td class="leftField">
                    <asp:Label class="FieldName" ID="lblPriority" Text="Priority :" runat="server"></asp:Label>
                </td>
                 <td class="rightField">
                    <asp:DropDownList CssClass="cmbField" ID="ddlPriority" runat="server">
                    </asp:DropDownList>
                 </td>
                
            </tr>
            <tr>
               <td></td><td></td>
                <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"  
                                CssClass="PCGButton" onclick="btnSubmit_Click" ValidationGroup="vgBtnSubmit"/>
                            </td>
                <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update"  
                                CssClass="PCGButton" onclick="btnUpdate_Click" ValidationGroup="vgBtnSubmit"/>        
                        </td>
            </tr>
        </table>
    </td>
</tr>
<tr>
    <td colspan="9"></td>
</tr>

</table>
<br />
<hr />
<table>
<tr>
    <td><asp:Label ID="lblLevel2" CssClass="HeaderTextBig" runat="server" Text="Level 2(Quality Team)"></asp:Label></td>
</tr>
</table>
<table>
<tr>
    <td ><asp:Label class="FieldName" ID="lblQAComments" runat="server" Text="Comments :"></asp:Label></td>
    <td ><asp:TextBox CssClass="txtField" runat="server" 
            ID="txtQAComments" TextMode="MultiLine" Width="300px" Height="100px" 
            ></asp:TextBox>
            <span style="vertical-align:top" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator CssClass="cvPCG" ValidationGroup="vgBtnQSubmit" runat="server" ControlToValidate="txtQAComments" ErrorMessage="Enter Your Comments"></asp:RequiredFieldValidator>
            </td>    
    <td valign="top">
        <table>
            <tr>
                <td class="leftField" ><asp:Label class="FieldName" Text="Author :" ID="lblRepliedBy" runat="server"></asp:Label></td>
                <td class="rightField"><asp:TextBox CssClass="txtField" runat="server" ID="txtRepliedBy"></asp:TextBox>
                <span class="spnRequiredField">*</span>
                <br />
                <asp:RequiredFieldValidator ValidationGroup="vgBtnQSubmit" CssClass="cvPCG" runat="server" ControlToValidate="txtRepliedBy" ErrorMessage="Enter Your Name" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField"><asp:Label class="FieldName" Text="Reported Date :" ID="lblRepliedDate" runat="server"></asp:Label></td>
                <td class="rightField">                    
                  <telerik:RadDatePicker   CssClass="txtField" ID="txtQAReportedDate" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                        Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                 </telerik:RadDatePicker>
                    <span class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" CssClass="cvPCG" ErrorMessage="Enter A Date" ControlToValidate="txtQAReportedDate" ValidationGroup="vgBtnQSubmit"></asp:RequiredFieldValidator>
                    
                </td>
                
            </tr>
            <tr>
                 <td  class="leftField"><asp:Label class="FieldName" Text="Escalate :" ID="lblReportTolevel1" runat="server"></asp:Label></td>
                <td class="rightField"><asp:DropDownList ID="ddlReportQA" runat="server" CssClass="cmbField">
                </asp:DropDownList></td>
                <td class="leftField"><asp:Label class="FieldName" runat="server" ID="lblReleaseVersion" Text="Release :"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txteleaseVersion" CssClass="txtField" runat="server"></asp:TextBox>
                    <%--<span class="spnRequiredField">*</span>                    
                    <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="vgBtnQSubmit" CssClass="cvPCG" runat="server" ControlToValidate="txteleaseVersion" ErrorMessage="Enter Release Number" Display="Dynamic"></asp:RequiredFieldValidator>
                --%>
                </td>
         
                
                              
            </tr>
            <tr>
            <td></td><td></td>
                <td>              
                    <asp:Button runat="server" ID="QASubmit" Text="Submit" CssClass="PCGButton" onclick="QASubmit_Click" ValidationGroup="vgBtnQSubmit"/>
                </td>
                <td>
                     <asp:Button ID="btnQAUpdate" runat="server" Text="Update"  
                        CssClass="PCGButton" onclick="btnQAUpdate_Click" ValidationGroup="vgBtnQSubmit"/>
                </td> 
           </tr>
        </table>
    </td>
</tr>
<tr>
    
</tr>
<tr>
    <td colspan="9"></td>
</tr>

</table>
<br />
<hr />

<table>
<tr>
    <td><asp:Label ID="lblLevel3" CssClass="HeaderTextBig" Text="Level 3(Technology)" runat="server"></asp:Label></td>
</tr>
</table>
<table>
<tr>
    <td class="leftField" valign="top"><asp:Label ID="lblComment" class="FieldName" runat="server" Text="Comments :"></asp:Label></td>
    <td class="rightField"><asp:TextBox CssClass="txtField" runat="server" ID="txtDevComments" Height="100px" Width="300px" TextMode="MultiLine"></asp:TextBox>
    <span style="vertical-align:top" class="spnRequiredField">*</span>
    <br />
    <asp:RequiredFieldValidator ValidationGroup="vgBtnTSubmit" runat="server" CssClass="cvPCG" ControlToValidate="txtDevComments" ErrorMessage="Enter your comments" Display="Dynamic"></asp:RequiredFieldValidator>
    </td>
    <td valign="top">
         <table>
            <tr>
                <td class="leftField"><asp:Label class="FieldName" Text="Author :" ID="lblDevRepliedBy" runat="server"></asp:Label></td>
                <td class="rightField"><asp:TextBox CssClass="txtField" runat="server" ID="txtDevRepliedBy"></asp:TextBox>
                <span class="spnRequiredField">*</span>
                <br />
                <asp:RequiredFieldValidator runat="server" CssClass="cvPCG" ControlToValidate="txtDevRepliedBy" ValidationGroup="vgBtnTSubmit" ErrorMessage="Enter Your Name" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField"><asp:Label class="FieldName" Text="Reported Date :" ID="lblDevRepliedDate" runat="server"></asp:Label></td>
                <td class="rightField">
                    <telerik:RadDatePicker   CssClass="txtField" ID="txtDEVReportedDate" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                        Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                 </telerik:RadDatePicker>
                 <span class="spnRequiredField">*</span>
                 <br />
                 <asp:RequiredFieldValidator ControlToValidate="txtDEVReportedDate" CssClass="cvPCG" runat="server" ValidationGroup="vgBtnTSubmit" ErrorMessage="Enter a Date" Display="Dynamic"></asp:RequiredFieldValidator>
                 
                </td>
                
            </tr>
            <tr>
                 <td class="leftField"><asp:Label class="FieldName" Text="Escalate :" ID="Label11" runat="server"></asp:Label></td>
                <td class="rightField"><asp:DropDownList ID="ddlReportDEV" runat="server" CssClass="cmbField">
                </asp:DropDownList>
                <td>
                    <asp:Button runat="server" Text="submit" ID="btnDEVSubmit"  
                        CssClass="PCGButton" onclick="btnDEVSubmit_Click" ValidationGroup="vgBtnTSubmit"/>
                        </td>
                        <td>
                        <asp:Button ID="btnDevUpdate" runat="server" Text="Update"  
                        CssClass="PCGButton" onclick="btnDevUpdate_Click" ValidationGroup="vgBtnTSubmit"/>
                </td>
            </tr>
        </table>
    </td>
</tr>



<tr>
<td></td>
    <td></td>
    <td colspan="2"></td>
</tr>
</table>



