<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mapping.ascx.cs" Inherits="WealthERP.Admin.Mapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div runat="server" id="TopDiv">
<asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
 <table style="width:100%">
 <tr >
  <td  class="HeaderCell">
    <label id="lblheader" class="HeaderTextBig" title="Mapping Screen">Mapping Screen</label> 
  </td>
 </tr>
 </table>    
    
</div>
<div id="DivScripMapping" runat="server" style="display:none" >
    <table style="width: 100%"> 
        <tr>
            <td colspan='4'>
                <label id="lbldetails" class="HeaderTextSmall" title="Details">
                    Details</label>
                <hr />
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="lbl1" class="FieldName" title="CODE">
                    CODE:</label>
            </td>
            <td class="rightField">
                <asp:Label ID="lblWERPCODE" class="Field" runat="server" Enabled="false"></asp:Label>
            </td>
            <td class="leftField">
                <label id="lblScripName" class="FieldName" title="Scrip Name">
                    Scrip Name:</label>
            </td>
            <td class="rightField">
                <asp:TextBox CssClass="txtField" ID="txtScripName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="lblIncDate" class="FieldName" title="Incorporation Date">
                    Incorporation Date:</label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtIncdate" CssClass="txtField" runat="server"></asp:TextBox>
            </td>
            <td class="leftField">
                <label id="lblPubDate" class="FieldName" title="Public Issue Date">
                    Public Issue Date:</label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtPublishDate" CssClass="txtField" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="lblMarketlot" class="FieldName" title="Market Lot">
                    Market Lot:</label>
            </td>
            <td class="rightField">
                <asp:TextBox CssClass="txtField" ID="txtMarketLot" runat="server"></asp:TextBox>
            </td>
            <td class="leftField">
                <label id="lblFaceValue" class="FieldName" title="Face Value">
                    Face Value:</label>
            </td>
            <td class="rightField">
                <asp:TextBox CssClass="txtField" ID="txtFaceValue" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="lblBookClosure" title="Book Closure" class="FieldName">
                    Book Closure:</label>
            </td>
            <td class="rightField">
                <asp:TextBox CssClass="txtField" ID="txtBookClouser" runat="server" />
            </td>
            <td class="leftField">
                <label id="lblTicker" class="FieldName" title="Ticker">
                    Ticker:</label>
            </td>
            <td class="rightField">
                <asp:TextBox CssClass="txtField" ID="txtTicker" runat="server"></asp:TextBox>
            </td>
        </tr>
        <cc1:CalendarExtender ID="CalendarExtender0" TargetControlID="txtIncdate" runat="server">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtPublishDate" runat="server">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtBookClouser" runat="server">
        </cc1:CalendarExtender>
    </table>
    <table style="width: 100%">
        <tr>
            <td colspan='4'>
                <label id="lblClassification" class="HeaderTextSmall" title="Classification">
                    Classifcation</label>
                <hr />
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="lblInst" class="FieldName" title="Instrument Category">
                    Instrument Category:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlInstCategory" AutoPostBack="true" runat="server"
                    OnSelectedIndexChanged="OnSelectedIndexChanged_InstCategory">
                </asp:DropDownList>
            </td>
            <td class="leftField">
                <label id="lblSubCat" class="FieldName" title="Sub Category">
                    Sub Category:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlSubCategory" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label class="FieldName" title="Market Cap" id="lblMarCap">
                    Market Cap:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlMarketCap" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <label id="lblExt" class="HeaderTextSmall" title="External Source Mapping">
                    External Source Mapping</label>
                <hr />
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label class="FieldName" id="lblBSE" title="BSE">
                    BSE:</label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtBSE" CssClass="txtField" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label class="FieldName" id="Label2" title="BSE">
                    NSE:</label>
            </td>
            <td class="rightField">
                <asp:TextBox CssClass="txtField" ID="txtNSE" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label class="FieldName" id="Label3" title="cerc">
                    CERC:</label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtCERC" CssClass="txtField" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button CssClass="PCGButton" ID="btnMap2Source" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_Mapping_btnMap2Source');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_Mapping_btnMap2Source');"
                    Text="Map TO New Source" runat="server" />
            </td>
        </tr>
    </table>
</div>
<div id="DivSchemeMapping" runat="server" style="display: none">
    <table style="width: 100%">
        <tr>
            <td colspan='4'>
                <label id="Label4" class="HeaderTextSmall" title="Details">
                    Details</label>
                <hr />
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="lblMFWERP" class="FieldName" title="WERP CODE">
                    WERP CODE:</label>
            </td>
            <td class="rightField">
                <asp:Label class="Field" ID="lblSchMapWerpCode" runat="server" Enabled="false"></asp:Label>
            </td>
            <td class="leftField">
                <label id="lblSchplan" class="FieldName" title="Scheme Plan Name">
                    Scheme Plan Name:</label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtSchmPlnName" CssClass="txtField" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td colspan='4'>
                <label id="Label5" class="HeaderTextSmall" title="Classification">
                    Classifcation</label>
                <hr />
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="Label6" class="FieldName" title="Instrument Category">
                    Instrument Category:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlSchmMapInstCat" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlSchmMapInstCat_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="leftField">
                <label id="Label7" class="FieldName" title="Sub Category">
                    Sub Category:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlSchmMapSubCat" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlSchmMapSubCat_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="Label8" class="FieldName" title="Sub Sub Category">
                    Sub Sub Category:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlSubSubCat" runat="server">
                </asp:DropDownList>
            </td>
            <td class="leftField">
                <label id="Label9" class="FieldName" title="Market Cap">
                    Market Cap:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlSchmMapMrktCap" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
    <tr>
            <td colspan="2">
                <label id="Label1" class="HeaderTextSmall" title="External Source Mapping">
                    External Source Mapping</label>
                <hr />
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="Label10" class="FieldName" title="AMFI">
                    AMFI:</label>
            </td>
            <td class="rightField">
                <asp:TextBox CssClass="txtField" ID="txtAMFI" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="Label11" class="FieldName" title="CAMS">
                    CAMS:</label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtCAMS" CssClass="txtField" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="Label12" class="FieldName" title="KARVY">
                    KARVY:</label>
            </td>
            <td class="rightField">
                <asp:TextBox CssClass="txtField" ID="txtKarvy" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
    </table>
</div>
<table style="width: 100%">
    <tr>
        <td class="SubmitCell">
            <asp:Button ID="btnSubmit" CssClass="PCGButton" Text="Submit" runat="server" OnClick="OnClick_Submit"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_Mapping_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_Mapping_btnSubmit');" />
        </td>
    </tr>
</table>
