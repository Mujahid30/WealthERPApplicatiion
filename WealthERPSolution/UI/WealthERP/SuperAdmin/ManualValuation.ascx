<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManualValuation.ascx.cs" Inherits="WealthERP.SuperAdmin.ManualValuation" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<style type="text/css">
        .module1
        {
            background-color: #dff3ff;
            border: 1px solid #c6e1f2;
        }
    </style>


<table width="100%">
     <tr>
        <td class="HeaderCell" colspan="2">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Manual Valuation"></asp:Label>
            <hr />
        </td>
     </tr> 
     <tr>
       
        
        <td>
         <asp:RadioButton ID="rbtnMF" runat="server" AutoPostBack="True" 
                CssClass="cmbField" Text="Mutual Fund" GroupName="ManualValuation" 
                oncheckedchanged="rbtnMF_CheckedChanged" />
           
        </td>
        
        <td>
            <asp:RadioButton ID="rbtnEquity" runat="server" AutoPostBack="True" 
                CssClass="cmbField" Text="Equity" GroupName="ManualValuation" 
                oncheckedchanged="rbtnEquity_CheckedChanged" />
        </td>
       
    </tr>    
    <tr id="trEquity" runat="server">
        <td>
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Trade Date"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddTradeYear" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeYear_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddTradeMonth" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMonth_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlTradeDate" runat="server" CssClass="cmbField" 
                AutoPostBack="True" onselectedindexchanged="ddlTradeDate_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trMF" runat="server">
        <td>
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Trade Date"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddTradeMFYear" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMFYear_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddTradeMFMonth" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMFMonth_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlTradeMFDate" runat="server" CssClass="cmbField" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlTradeMFDate_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    
      
</table>

<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Height="275px" ScrollBars="Horizontal">
<table width="100%" class="TableBackground">
   
    <tr>
    <td>
    
    <asp:GridView ID="gvAdviserList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" AllowSorting="True" HorizontalAlign="Center" ShowHeader="true" ShowFooter="true" DataKeyNames="AdviserId">
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
                            <input id="chkBoxAll"  name="vehicle" value="Bike" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AdviserId" HeaderText="AdviserId" />
                    <asp:BoundField DataField="OrgName" HeaderText="Organisation" />
                    <asp:BoundField DataField="ValuationStatus" HeaderText="Valuation Status" />
                </Columns>
            </asp:GridView>
    
            
    </td>
    </tr>
      
   
 </table>
 </asp:Panel>
 <table>
   <tr>
    <td>
    <asp:Button ID="btnRunValuation" runat="server" Text="Run Valuation" CssClass="PCGLongButton" 
    onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerUpload_btn_Upload','L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerUpload_btn_Upload','L');"
            onclick="btnRunValuation_Click" Visible="false" />
    </td>
   </tr>    
 </table>
    

 

 
   

