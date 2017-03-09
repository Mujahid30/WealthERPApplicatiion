    <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PriceListMonitor.ascx.cs" Inherits="WealthERP.SuperAdmin.PriceListMonitor" %>
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>



<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this price?');

        if (bool) {
            document.getElementById("ctrl_PriceListMonitor_hdnMsgValue").value = 1;
            document.getElementById("ctrl_PriceListMonitor_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_PriceListMonitor_hdnMsgValue").value = 0;
            document.getElementById("ctrl_PriceListMonitor_hiddenassociation").click();
            return true;

        }
    }
</script>

<script type="text/javascript">
    function TestCheckBoxForDelete() {
        var TargetBaseControl = null;
        try {
            //get target base control.
            TargetBaseControl = document.getElementById('<%= this.GridViewDetails.ClientID %>');
        }
        catch (err) {
            TargetBaseControl = null;
        }
        if (TargetBaseControl == null) return false;

        //get target child control.

        var TargetChildControl = "chkSelect";

        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
            Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked)
            return true;

        alert('Please select a price to delete!');
        return false;
    }
</script>


<script type="text/javascript">
    function TestCheckBoxForEdit() {
        var TargetBaseControl = null;
        try {
            //get target base control.
            TargetBaseControl = document.getElementById('<%= this.GridViewDetails.ClientID %>');
        }
        catch (err) {
            TargetBaseControl = null;
        }
        if (TargetBaseControl == null) return false;

        //get target child control.
        
        var TargetChildControl = "chkSelect";

        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
            Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked)
            return true;

        alert('Please select a price to edit!');
        return false;
    }
</script>
   
   <script type="text/javascript" language="javascript">
       function CheckOne(obj) {
           var grid = obj.parentNode.parentNode.parentNode;
           var inputs = grid.getElementsByTagName("input");
           for (var i = 0; i < inputs.length; i++) {
               if (inputs[i].type == "checkbox") {
                   if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                       inputs[i].checked = false;
                   }
               }
           }
       }
    </script>  
   
    <script type="text/javascript">

        function WindowPopup() {
            window.open("http://www.ncdex.com/MarketData/SpotPrice.aspx", "myWindow",
    "status = 1, height = 500, width = 500, resizable = 0, scrollbars=1")
        }
    </script>
    
    <script type="text/javascript">

        function checkdate(txtDate) {
            var objDate,  // date object initialized from the txtDate string
            mSeconds, // txtDate in milliseconds
            day,      // day
            month,    // month
            year;     // year
            // date length should be 10 characters (no more no less)
            if (txtDate.length !== 10) {
                return false;
            }
            // third and sixth character should be '/'
            if (txtDate.substring(2, 3) !== '/' || txtDate.substring(5, 6) !== '/') {
                return false;
            }
            // extract month, day and year from the txtDate (expected format is mm/dd/yyyy)
            // subtraction will cast variables to integer implicitly (needed
            // for !== comparing)
            day = txtDate.substring(0, 2) - 1; // because months in JS start from 0
            month = txtDate.substring(3, 5) - 0;
            year = txtDate.substring(6, 10) - 0;
            // test year range
            if (year < 1000 || year > 3000) {
                return false;
            }
            // convert txtDate to milliseconds
            mSeconds = (new Date(year, month, day)).getTime();
            // initialize Date() object from calculated milliseconds
            objDate = new Date();
            objDate.setTime(mSeconds);
            // compare input date and parts from Date() object
            // if difference exists then date isn't valid
            if (objDate.getFullYear() !== year ||
            objDate.getMonth() !== month ||
            objDate.getDate() !== day) {
                return false;
            }
            // otherwise return true
            return true;
        }

     
        function checkEmptyTextbox() {              
            
                if (document.getElementById("<%=txtDate.ClientID%>").value == "") {
                    alert("please enter a date");
                    document.getElementById("<%=txtDate.ClientID%>").focus();
                    return false;
                    
                }
                if (document.getElementById("<%=txtPrice.ClientID%>").value == "") {
                    alert("please enter a price");
                    document.getElementById("<%=txtPrice.ClientID%>").focus();
                    return false;

                }

                // define date string to test
                var txtDate = document.getElementById("<%=txtDate.ClientID%>").value;
                // check date and print message
                if (checkdate(txtDate)) {
                    return true;
                }
                else {
                    alert('Invalid date format!');
                    document.getElementById("<%=txtDate.ClientID%>").focus();
                    return false;
                }

               

            }

    </script>


<%--<asp:UpdatePanel ID="updatePanel1" runat="server"><ContentTemplate>--%>
    <table width="1012px">
    <tr>
    <td class="HeaderCell">
        <asp:Label ID="lblPriceMonitoring" CssClass="HeaderTextBig" runat="server" Text="Price Maintenance"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
    <hr />
    </td>
    </tr>
    </table>
    <table>
    <tr>
    <td class="rightField">
        <asp:Label class="FieldName" ID="lblProduct" runat="server" Text="Product :"></asp:Label>

        <asp:DropDownList ID="DDLProductList" runat="server" CssClass="cmbField">
            <asp:ListItem Selected="True">Gold</asp:ListItem>
        </asp:DropDownList>
    </td>
    </tr>
    <tr><td></td></tr>
    <tr><td></td></tr>
    <tr><td></td></tr>
    <tr><td></td></tr>
    </table>
         
    <%--<asp:UpdatePanel ID="showBetweenDatesPanel" runat="server" onload="btnShowBetweendates_Click" 
        UpdateMode="Conditional"><ContentTemplate>
    --%>    
    
    
    <table width="50%">
    <%--<tr id="trNoRecords" runat="server">
        <td>
            <asp:Label ID="Label1" class="Error" runat="server" Visible="false" Text="No Records Found...!"></asp:Label>
        </td>
    </tr>--%>
    <tr>
    <td class="leftField">
        <asp:Label class="FieldName" ID="lblFrom" runat="server" Text="From :"></asp:Label>
    </td>
    <td class="rightField">
        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField" ValidationGroup="btnShowBetweendates" ></asp:TextBox>
            <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
            TargetControlID="txtFromDate"  Format="dd/MM/yyyy" Enabled="True">
        </cc1:CalendarExtender>
        
        <asp:CompareValidator id="cmpValidatorFutureDate" ValidationGroup="btnShowBetweendates"
                        ControlToValidate="txtFromDate" Operator="LessThanEqual" Type="Date" CssClass="cvPCG"
                        runat="server" ErrorMessage="Date Can't be in future" Display="Dynamic" ></asp:CompareValidator>
          
          <asp:CompareValidator ID="CompareValidatorTextBox1" runat="server"     
    ControlToValidate="txtFromDate"
    Type="Date"
    Operator="DataTypeCheck"
    ErrorMessage="Please Enter Valid From Date" ValidationGroup="btnShowBetweendates" Display="Dynamic" CssClass="cvPCG"
        />      
        <asp:RequiredFieldValidator CssClass="cvPCG" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate" ErrorMessage="please enter from date" ValidationGroup="btnShowBetweendates" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        
    <td class="leftField">
        <asp:Label class="FieldName" ID="lblTo" runat="server" Text="To :"></asp:Label>
    </td>
    <td class="rightField">
        <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField" name="mydate" ValidationGroup="btnShowBetweendates" ></asp:TextBox>
        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"  
            Enabled="True" TargetControlID="txtToDate" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        
        <asp:CompareValidator id="cmpCompareValidatorToDate" ValidationGroup="btnShowBetweendates"
                        ControlToValidate="txtToDate" Operator="LessThanEqual" Type="Date" CssClass="cvPCG"
                        runat="server" ErrorMessage="Date Can't be in future" Display="Dynamic" ></asp:CompareValidator>
                        
                         <asp:CompareValidator ID="CompareValidator1" runat="server"     
    ControlToValidate="txtToDate"
    Type="Date"
    Operator="DataTypeCheck"
    ErrorMessage="Please Enter Valid To Date" ValidationGroup="btnShowBetweendates" Display="Dynamic" CssClass="cvPCG"/>
    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="To Date should not be less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnShowBetweendates"></asp:CompareValidator>
    <asp:RequiredFieldValidator CssClass="cvPCG" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate" ErrorMessage="please enter to date" ValidationGroup="btnShowBetweendates" Display="Dynamic"></asp:RequiredFieldValidator>
    </td>
    <td>
    
    </td>
    </tr>

  <tr>
  <td colspan="5"></td>
  </tr>
    
    <tr>
  <td colspan="5"></td>
  </tr>
     <tr>
  <td colspan="5"></td>
  </tr> <tr>
  <td colspan="5"></td>
  </tr>
    <tr>
    <td></td>
    <td><asp:Button ID="btnShowBetweendates" runat="server" Text="Go" 
            onclick="btnShowBetweendates_Click" CssClass="PCGButton" ValidationGroup="btnShowBetweendates"/>     
        </td>
        <td></td>
        <td>
        
        </td><td>
        
        </td>
    </tr>
    </tr>
    </table>
  <%--  </ContentTemplate></asp:UpdatePanel>--%>
    <table id="tblErrorMassage" width="100%" visible="false" cellpadding="0" cellspacing="0" runat="server">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" align="center">
            </div>
        </td>
    </tr>
 </table>
    
    <table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Gold price has been deleted Successfully.
            </div>
        </td>
    </tr>
</table>

<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatusForEdit" runat="server" class="success-msg" align="center" visible="false">
                Gold price has been updated Successfully.
            </div>
        </td>
    </tr>
</table>

<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatusForSubmit" runat="server" class="success-msg" align="center" visible="false">
                Gold price has been Submitted Successfully.
            </div>
        </td>
    </tr>
</table>

    <table>
    <tr id="trNoRecords" runat="server">
        <td>
            <asp:Label ID="lblMsg" class="Error" runat="server" Visible="false" Text="No Records Found...!"></asp:Label>
        </td>
    </tr>
    
    </table>
        <table>
    <tr>
    <td>
    <%--<asp:UpdatePanel runat="server" onload="btnShowBetweendates_Click" 
            UpdateMode="Conditional"><ContentTemplate>--%>
    <asp:Panel ID="Panel" runat="server" Width="600px">
    <table width="100%"><tr align="center"><td align="center">
    <tr id="trPagger" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
          <asp:GridView ID="GridViewDetails" runat="server" CssClass="GridViewStyle" 
              AutoGenerateColumns="False" DataKeyNames="PG_ID"
           CellPadding="4" ShowHeader="true" ShowFooter="true" 
              Width="100%" AllowSorting="true" 
            onrowdatabound="GridViewDetails_RowDataBound">
           
                                <RowStyle CssClass="RowStyle"  />
                        <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
        <Columns>
        <asp:TemplateField HeaderText="Select" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Width="5px" HeaderStyle-ForeColor="White"> 
                     <ItemTemplate>
                 <asp:CheckBox ID="chkSelect" runat="server" onclick="CheckOne(this)" />
                 </ItemTemplate>
                 
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"> 
        <HeaderTemplate>
                                <asp:Label ID="lblParent" runat="server" Text="Date"></asp:Label>
                                <br />
                               <asp:TextBox Text='<%# hdnDateFilter.Value %>' ID="txtDateSearch" CssClass="txtField" runat="server" OnTextChanged="txtDateSearch_TextChanged" AutoPostBack="true"/>
                               <cc1:CalendarExtender ID="txtDateSearchExtender" runat="server" 
            Enabled="True" TargetControlID="txtDateSearch" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                            </HeaderTemplate>
                     <ItemTemplate>
                 <asp:Label ID="lblPGDate" runat="server" Text='<%#Eval("PG_Date", "{0:d}")%>'></asp:Label>
                 </ItemTemplate>
                 
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Price" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" >
                     <ItemTemplate >
                     <asp:Label ID="lblPGPrice" runat="server" Text='<%#Eval("PG_Price")%>'></asp:Label>
                 </ItemTemplate>
                 
        </asp:TemplateField>
        </Columns>
        </asp:GridView>
        </td></tr></table>
        <table width="60%">
                    <tr align="center">
                        <td align="center">
                            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                        </td>
                    </tr>
                </table>
        </asp:Panel>
<%--</ContentTemplate></asp:UpdatePanel>--%>
        </td>
    </tr>
    </table>

     <table>
      <tr>
    
    <td>
    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="PCGButton"  OnClientClick="javascript:return TestCheckBoxForEdit();" onclick="btnEdit_Click" 
            />
    </td>
    <td>
    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="PCGButton" OnClientClick="javascript:return TestCheckBoxForDelete();" onclick="btnDelete_Click" />
    </td>
    <td>
    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="PCGButton" OnClick="btnAdd_Click"/>
    </td>
    
   <td><asp:Label class="FieldName" ID="lblMessage" runat="server"></asp:Label></td>
    </tr>
    </table>



    <table>

    <tr>
    <td>
      
       <asp:Panel runat="server" ID="AddEditDetails" Height="100px" Width="900px">

    <table>
    <tr>
    <td class="leftField">
        <asp:Label class="FieldName" ID="lblDate" runat="server" Text="Date :"></asp:Label>
    </td>
    <td class="rightField">
        <asp:TextBox ID="txtDate" runat="server" CssClass="txtField" ValidationGroup="btnSubmit"></asp:TextBox>
        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
    </td>
    <td style="font-size:x-small">
    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" ErrorMessage="Enter a Date" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
    </td>
    <td style="font-size:x-small">
          <asp:CompareValidator ID="CompareValidator2" runat="server"     
    ControlToValidate="txtDate"
    Type="Date"
    Operator="DataTypeCheck"
    ErrorMessage="Enter Valid Date" ValidationGroup="btnSubmit"
        />
    </td>
    </tr>
    <tr>
    <td class="leftField">
        <asp:Label class="FieldName" ID="lblPrice" runat="server" Text="Price :"></asp:Label>
    </td>

    <td class="rightField">
        <asp:TextBox ID="txtPrice" runat="server" CssClass="txtField" ValidationGroup="btnSubmit"></asp:TextBox>
    </td>
    <td  style="font-size:x-small">
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrice" ErrorMessage="Enter a Price" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
        
    </td>
    <td style="font-size:x-small">
    <asp:CompareValidator
        id="cmpPrice"
        ControlToValidate="txtPrice"
        Text="Enter Valid Price"
        Operator="DataTypeCheck"
        Type="Currency"
        Runat="server" ValidationGroup="btnSubmit"/>
    </td>
    </tr>
    <tr>
    <td>

    </td>
    <td style="font-size:x-small">
        <asp:Button CssClass="PCGButton" ID="btnSubmit" runat="server" Text="Submit" 
            onclick="btnSubmit_Click" ValidationGroup="btnSubmit" />
            <asp:Button CssClass="PCGButton" ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" ValidationGroup="btnReset" />
           
    </td>
   
    <td style="font-size:x-small">
                                    
                                    <asp:LinkButton ID="lnkClickHereForPrice" runat="server" OnClientClick="WindowPopup()">Latest gold price</asp:LinkButton>
                                        
                                </td>
       
   
    </tr>
    <tr>
    <td></td>
    <td>
     
    </td>
    </tr>
    <tr><td></td></tr>
    <tr>
    <td></td>
    <td>
    <asp:Label runat="server" Text="Note: Gold price as per 100 gms" class="FieldName"></asp:Label>
    </td>
    </tr>
    </table>
    </asp:Panel>
    
    </td>
    </tr> 
    
</table>
<%--</ContentTemplate></asp:UpdatePanel>--%>
<asp:HiddenField ID="hdnDateFilter" runat="server" EnableViewState="true"/>
<asp:HiddenField ID="hdnRecordCount" runat="server"  EnableViewState="true"/>
<asp:HiddenField ID="hdnCurrentPage" runat="server"  EnableViewState="true"/>
<asp:HiddenField ID="hdnCount" runat="server"  EnableViewState="true"/>
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdnButtonClick" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />


<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />
    
    <asp:HiddenField ID="hdnGoalId" runat="server" />