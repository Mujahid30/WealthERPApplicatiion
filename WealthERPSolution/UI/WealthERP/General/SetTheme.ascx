<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetTheme.ascx.cs" Inherits="WealthERP.General.SetTheme" %>
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
<script type="text/javascript">
    function ShowIsa() {

        var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;
    }
</script>


       

            <table width="100%">
                <tr>
                    <td colspan="6">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Cash Flow Recommendation
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%">
            
             
                
            
                
                <tr> 
                
                <td align="right" style="width: 15%;">
                        <asp:Label ID="lblptype" runat="server" CssClass="FieldName" Text="Product:"></asp:Label>
                    </td>
                    <td style="width: 23.5%">
                 <asp:DropDownList ID="ddlptype" runat="server" CssClass="cmbField" 
                            AutoPostBack="true" TabIndex="1">
                           
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tr1" visible="false">
                    <td align="right" style="width: 15%;">
                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text=""></asp:Label>
                    </td>
                     <td style="width: 30px;">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField" AutoPostBack="true">
                           </asp:DropDownList>
                           </td>
                    </tr>
                     <tr id="tr2" visible="false">
                     <td align="right" style="width: 15%;">
                        <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text=""></asp:Label>
                    </td>
                     <td style="width: 30px;">
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="cmbField" AutoPostBack="true">
                           </asp:DropDownList>
                           </td>
                    </tr>
                    
                    
                   <tr> 
                
                <td align="right" style="width: 15%;">
                        <asp:Label ID="lblpaytype" runat="server" CssClass="FieldName" Text="Payment Type:"></asp:Label>
                    </td>
                    <td style="width: 23.5%">
                 <asp:DropDownList ID="ddlpaytyppe" runat="server" CssClass="cmbField" 
                            AutoPostBack="true" TabIndex="1">
                           <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Installment" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Lumpsum" Value="2"></asp:ListItem> 
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
                            AutoPostBack="true"  TabIndex="10">
                            <Calendar UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" runat="server" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                       
                    </td>
                    
                      <td align="right">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtEndDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            AutoPostBack="true"  TabIndex="10">
                            <Calendar UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" runat="server" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                       
                    </td>
                    </tr>
                    <tr>
                      <td align="right" style="width: 15%;">
                        <asp:Label ID="lblfrequncy" runat="server" CssClass="FieldName" Text="Frequency:"></asp:Label>
                    </td>
                    <td style="width: 23.5%">
                 <asp:DropDownList ID="ddlfrequncy" runat="server" CssClass="cmbField" 
                            AutoPostBack="true" TabIndex="1">
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
                        <asp:Label ID="lblsumassure" runat="server" Text="Sum Assure:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtsumassure" runat="server" CssClass="txtField" TabIndex="28"></asp:TextBox>
                    </td>     
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblSelect" runat="server" Text="Select Theme :" CssClass="FieldName"></asp:Label>
            <asp:DropDownList runat="server" ID="ddlTheme" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlTheme_SelectedIndexChanged">
                <asp:ListItem Value="0">Select a Theme</asp:ListItem>
                <asp:ListItem Value="BlackAndWhite">Black & white</asp:ListItem>
                <asp:ListItem Value="Blue">Blue</asp:ListItem>
                <asp:ListItem Value="Desert">Desert</asp:ListItem>
                <asp:ListItem Value="Green">Green</asp:ListItem>
                <asp:ListItem Value="Maroon">Maroon</asp:ListItem>
                <asp:ListItem Value="Purple">Purple</asp:ListItem>
                <asp:ListItem Value="Yellow">Yellow</asp:ListItem>
                <asp:ListItem Value="LightPurple">LightPurple</asp:ListItem>
                <asp:ListItem Value="SBICAP">Light Blue</asp:ListItem>
                <asp:ListItem Value="SBIOnLine">SBI Blue</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>


