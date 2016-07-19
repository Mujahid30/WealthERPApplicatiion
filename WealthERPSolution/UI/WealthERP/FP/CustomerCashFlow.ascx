<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCashFlow.ascx.cs" Inherits="WealthERP.FP.CustomerCashFlow" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>
<%--<script type="text/javascript">
    function ShowIsa() {

        var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;
    }
</script>--%>


       

            <table width="100%">
                <tr>
                    <td colspan="6">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Cash Flow Recommendation
                        </div>
                    </td>
                </tr>
            </table>
            
            <tr>
                <td>
                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click"
                        CommandName="EditClick" Visible="False">Edit</asp:LinkButton>
                    <asp:LinkButton ID="lnkBack" runat="server" CommandName="BackClick" CssClass="LinkButtons"
                        OnClick="lnkBack_Click" Visible="False">Back</asp:LinkButton>
                </td>
            </tr>
            <table width="100%">
            
             
                
            
                
                <tr> 
                
                <td align="right" style="width: 15%;">
                        <asp:Label ID="lblptype" runat="server" CssClass="FieldName" Text="Product:"></asp:Label>
                    </td>
                    <td style="width: 23.5%">
                 <asp:DropDownList ID="ddlptype" runat="server" CssClass="cmbField" 
                            AutoPostBack="true" TabIndex="1" 
                            onselectedindexchanged="ddlptype_SelectedIndexChanged">
                           
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tr1" visible="false">
                    <td align="right" style="width: 15%;">
                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="scheme"></asp:Label>
                    </td>
                     <td style="width: 30px;">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField" AutoPostBack="false">
                           </asp:DropDownList>
                           </td>
                    </tr>
                     <tr id="tr2" visible="false">
                     <td align="right" style="width: 15%;">
                        <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Buy/Sell"></asp:Label>
                    </td>
                     <td style="width: 30px;">
                        <asp:DropDownList ID="DropDownList2"  runat="server" CssClass="cmbField" 
                             AutoPostBack="false" 
                             onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                         
                        </asp:DropDownList>
                           </td>
                    </tr>
                    
                    
                   <tr> 
                
                <td align="right" style="width: 15%;">
                        <asp:Label ID="lblpaytype" runat="server" CssClass="FieldName" Text="Payment Type:"></asp:Label>
                    </td>
                    <td style="width: 23.5%">
                 <asp:DropDownList ID="ddlpaytyppe" runat="server" CssClass="cmbField" 
                            AutoPostBack="true"  
                            onselectedindexchanged="ddlpaytyppe_SelectedIndexChanged">
                          
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                      <td align="right">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtStartDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            AutoPostBack="false"  TabIndex="10">
                            <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput1" DisplayDateFormat="dd/MM/yyyy" runat="server" DateFormat="dd/MM/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                       
                    </td>
                    
                      <td align="right">
                        <asp:Label ID="lblEndDate" runat="server"  Text="End Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtEndDate"  CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            AutoPostBack="false"  TabIndex="10">
                            <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput2" DisplayDateFormat="dd/MM/yyyy" runat="server" DateFormat="dd/MM/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                       
                    </td>
                   
                    </tr>
                    <tr id="trfq">
                      <td align="right" style="width: 15%;">
                        <asp:Label ID="lblfrequncy"  runat="server" CssClass="FieldName" Text="Frequency:"></asp:Label>
                    </td>
                    <td style="width: 23.5%">
                 <asp:DropDownList ID="ddlfrequncy" runat="server" CssClass="cmbField" 
                            AutoPostBack="false"  TabIndex="1">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Weekly" Value="WK"></asp:ListItem>
                            <asp:ListItem Text="Yearly" Value="YR"></asp:ListItem> 
                            <asp:ListItem Text="Quarterly" Value="QT"></asp:ListItem>
                            <asp:ListItem Text="Monthly" Value="MN"></asp:ListItem>
                            <asp:ListItem Text="Daily" Value="DA"></asp:ListItem>
                            <asp:ListItem Text="FortNightly" Value="FN"></asp:ListItem>
                            <asp:ListItem Text="HalfYearly" Value="HY"></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                          <td align="right">
                        <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" TabIndex="28"></asp:TextBox>
                    </td>
                    </tr>
                    <tr>
                          
                        <td align="right">
                        <asp:Label ID="lblsumassure" runat="server" Text="Sum Assured:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtsumassure" runat="server" CssClass="txtField" TabIndex="28"></asp:TextBox>
                    </td>     
                    </tr>
                    <tr>
                      <td class="leftField">
                <asp:Label ID="lblRemarks" runat="server" Text="Remarks:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" MaxLength="300" Height="65px"
                    onkeydown="return (event.keyCode!=13);" runat="server" CssClass="txtField" TabIndex="51"></asp:TextBox>
            </td>
        </tr>
        <%--<tr id="trBtnSubmit" runat="server">
            <td align="left" colspan="3">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton"  
                    TabIndex="52" onclick="btnSubmit_Click" />
        
                
            </td>
        </tr>--%>
        <tr>
        <td class="rightField">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerEQAccountAdd_btnSubmit', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerEQAccountAdd_btnSubmit', 'S');"
                        Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return isValid()" />
                    <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" Text="Update" 
                        OnClientClick="return isValidInUpdateCase()" Visible="False" 
                        onclick="btnUpdate_Click" />
                </td>
                  </tr> 
            </table>
       <asp:HiddenField ID="hdnIsSubscripted" runat="server" />
