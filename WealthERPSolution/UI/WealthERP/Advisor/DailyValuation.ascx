<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DailyValuation.ascx.cs"
    Inherits="WealthERP.Advisor.DailyValuation" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />

<script language="javascript" type="text/javascript">
    function CheckboxCheck() {
        
        var count = 0;
        var gvControl = document.getElementById('<%= gvCustomerlist.ClientID %>');
        var gvChkBoxControl = "CheckBox1";

        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0) {
                if (inputTypes[i].checked)
                    count++;
            }
        }
        if (count == 0) {
            alert("Please select a record");
            return false;
        } 
    }
    </script>


<script language="javascript" type="text/javascript">
    function showmessage() {
        var grd_Cb = document.getElementById("<%= gvValuationDate.ClientID %>");
        var arrayEle = new Array(grd_Cb.rows.length - 2);
        Z = 0;
        for (j = 1; j < grd_Cb.rows.length - 1; j++) {

            var cell = grd_Cb.rows[j].cells[0];

            if (grd_Cb.rows[j].cells[2].textContent == 'No') {
                if (grd_Cb.rows[j].cells[0].childNodes[1].checked != true) {


                    arrayEle[Z++] = grd_Cb.rows[j].cells[1].textContent;

                }
            }



        }

        if (Z > 0) {
            var content = 'Valuation for the following dates will be missed out ' + "\n";
            for (l = 0; l < Z; l++) {
                content = content + arrayEle[l] + "\n";
            }
            var bool = window.confirm(content + "\n" + 'Do you wish to continue ?');

            if (bool) {
                document.getElementById("ctrl_DailyValuation_hdnMsgValue").value = 1;
                document.getElementById("ctrl_DailyValuation_hiddenUpdateNetPosition").click();
                return false;
            }
            else {
                document.getElementById("ctrl_DailyValuation_hdnMsgValue").value = 0;
                return true;
            }

        }
        else {
            document.getElementById("ctrl_DailyValuation_hdnMsgValue").value = 1;
            document.getElementById("ctrl_DailyValuation_hiddenUpdateNetPosition").click();
        }
    }
    //    function CheckAll() {
    //        alert("abc..");
    //        var panel = document.getElementById('<%= gvValuationDate.ClientID %>');
    //        var chkArray = gvValuationDate.getElementsByTagName("input");
    //        alert(chkArray);
    //        for (var i = 0; i < chkArray.length; i++) {
    //            if (chkArray[i].type == "checkbox") {
    //                alert(chkArray[i]);
    //                if (chkArray[i].checked == false) {

    //                    chkArray[i].checked = true;
    //                }
    //                else
    //                    chkArray[i].checked = false;
    //            }
    //        }
    //    };
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvValuationDate.Rows.Count %>');
        var gvControl = document.getElementById('<%= gvValuationDate.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkBx";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBoxAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template            
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    } 
    
   
</script>

<script language="javascript" type="text/javascript">
    function Check() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvCustomerlist.Items.Count %>');
        var gvControl = document.getElementById('<%= gvCustomerlist.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "CheckBox1";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template            
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    } 
</script>

<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell" colspan="2">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Daily Valuation"></asp:Label>
            <hr />
        </td>        
    </tr>
    <tr>
        <%--<td>
            <asp:RadioButton ID="rbtnEquity" runat="server" AutoPostBack="True" CssClass="cmbField"
                OnCheckedChanged="rbtnEquity_CheckedChanged" Text="Equity" GroupName="DailyValuation" />
        </td>
        <td>
            <asp:RadioButton ID="rbtnMF" runat="server" AutoPostBack="True" CssClass="cmbField"
                OnCheckedChanged="rbtnMF_CheckedChanged" Text="Mutual Fund" GroupName="DailyValuation" />
        </td>--%>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblSelectValuation" runat="server" CssClass="FieldName" Text="Select Valuation Type : "></asp:Label>
        </td>
        <td class="rightField" style="width: 85%">
            <asp:DropDownList ID="ddlValuationTypes" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlValuationTypes_OnSelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
               <%-- <asp:ListItem Text="Mutual Fund" Value="MF"></asp:ListItem>--%>
                <asp:ListItem Text="Equity" Value="EQ"></asp:ListItem>
                <asp:ListItem Text="Financial Profile" Value="FP"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trEquity" runat="server">
        <td class="leftField">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Trade Date:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddTradeYear" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeYear_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddTradeMonth" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMonth_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trMf" runat="server">
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Trade Date:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddTradeMFYear" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMFYear_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddTradeMFMonth" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMFMonth_SelectedIndexChanged">
            </asp:DropDownList>
            <%--<asp:DropDownList ID="ddlTradeDay" runat="server" CssClass="cmbField" AutoPostBack="false">
            </asp:DropDownList>--%>
        </td>
    </tr>
    <tr id="trHeader" runat="server">
        <td colspan="3">
            <asp:Label ID="lblDate" runat="server" CssClass="HeaderTextBig" Text="Valuation Date " />
            <hr />
        </td>
    </tr>
    <%--   <tr id="trMFDate" runat="server">
        <td>
            <asp:Label ID="lblMFTradeDate" runat="server" CssClass="FieldName" Text="Last MF Valuation Date :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMFValuationDate" runat="server" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr id="trEQDate" runat="server">
        <td>
            <asp:Label ID="lblTradeDate" runat="server" CssClass="FieldName" Text="Last EQ Valuation Date :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblEQValuationDate" runat="server" CssClass="Field"></asp:Label>
        </td>
    </tr>--%>
    <tr id="trValuation" runat="server">
        <td colspan="2" style="margin-left: 40px">
            <asp:GridView ID="gvValuationDate" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" AllowSorting="True" HorizontalAlign="Center" ShowFooter="true"
                OnRowDataBound="gvValuationDate_RowDataBound">
                <FooterStyle CssClass="FooterStyle" />
                <PagerSettings Visible="False" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <%--<PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />--%>
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBx" runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="chkBoxAll" name="vehicle" value="Bike" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Valuation Date" HeaderText="Valuation Date (dd/mm/yyyy)" />
                    <asp:BoundField DataField="Valuation Status" HeaderText="Valuation Status" />
                </Columns>
            </asp:GridView>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
       <tr id="trFPSync" runat="server">
        <td colspan="2">
        <div style="padding-top:2px; width:98%">
            <telerik:RadGrid ID="gvCustomerlist" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="98%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" AllowMultiRowSelection="true" OnNeedDataSource="gvCustomerlist_OnNeedDataSource">
                <MasterTableView DataKeyNames="CustomerId" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false">
                    <Columns>
                        <%--<telerik:GridClientSelectColumn UniqueName="ClientSelectColumn"  /> --%>
                        <telerik:GridTemplateColumn UniqueName="View" HeaderText="View" AllowFiltering="false" DataField="View">
                            <HeaderTemplate>
                                <input type="checkbox" id="chkAll" name="chkAll" onclick="Check();" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Cust_Comp_Name" CurrentFilterFunction="Contains"  HeaderText="Customer Name" ShowFilterIcon="true" UniqueName="CustomerName">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PAN Number" HeaderText="Pan Number" ShowFilterIcon="true" UniqueName="PanNumber">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnFPSync" CssClass="PCGButton" Text="Go" runat="server" OnClick="FPSync_OnClick" OnClientClick="return CheckboxCheck();">
            </asp:Button>
        </td>
    </tr>
    <tr id="trSubmitButton" runat="server">
        <td>
            <asp:Button ID="Button1" runat="server" Text="Update Net Position" CssClass="PCGLongButton"
                OnClick="Button1_Click" />
            <asp:Button ID="hiddenUpdateNetPosition" runat="server" OnClick="hiddenUpdateNetPosition_Click"
                Text="" BorderStyle="None" BackColor="Transparent" />
           
        </td>
    </tr>
    <tr id="trNote" runat="server">
        <td colspan="2">
          <%--  <asp:Label ID="lblNote" Text="Note:   The date highlighted is the last valuation date"
                CssClass="rfvPCG" runat="server"></asp:Label>--%>
                  <asp:Label ID="lblNote" 
                  Text="Note: </br> 1)Mutual fund valuation will affect all the Mutual Fund Net Position with the Current Value.
                  </br>
                  2)Equity valuation will affect all the Equity Data with the Current Value.
                  </br>
                  3)Financial Valuation will affect all the Asset's Data with their Current Value"
                CssClass="rfvPCG" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<%--<div id="DivPager" runat="server">
    <table style="width: 100%">
        <tr id="trPager" runat="server">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />--%>
<asp:HiddenField ID="hdnMsgValue" runat="server" />
