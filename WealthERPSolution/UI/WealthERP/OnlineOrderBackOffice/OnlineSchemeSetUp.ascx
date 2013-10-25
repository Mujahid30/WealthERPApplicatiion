<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineSchemeSetUp.ascx.cs" Inherits="WealthERP.OnlineOrderBackOffice.OnlineSchemeSetUp" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            Online Scheme Setup
                        </td>
                    </tr>
                 </table>
              </div>
            </td>
         </tr>
      </table>
      <table width="100%">
      <tr >
      <td >
          <asp:Label ID="lblProduct" runat="server" Text="Product" CssClass="FieldName"> </asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbfield" AutoPostBack="true">
               <asp:ListItem Text="Select" Value="Select" Selected="true" />
                  <asp:ListItem Text="Mutual Funds" />
          </asp:DropDownList></td>
          <td>
              <asp:Label ID="lblToadd" runat="server" Text="Do You Wish To Add" CssClass="FieldName"></asp:Label>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:CheckBox ID="chkonline" runat="server"  Text="Online Scheme" CssClass="cmbFielde"/>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:CheckBox ID="chkoffline" runat="server" Text="Offline Scheme" CssClass="cmbField"/></td>
      </tr>
      <tr><td>
          <asp:Label ID="lblScname" runat="server" Text="Scheme Name" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtScname" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblAcode" runat="server" Text="AMFI Code" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="TextBox2" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      
      </tr>
      <tr><td>
      <asp:Label ID="lblAmc" runat="server" Text="AMC" CssClass="FieldName"> </asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbfield" AutoPostBack="true">
               
          </asp:DropDownList>
      </td>
      <td>
      <asp:Label ID="lblcategory" runat="server" Text="Category" CssClass="FieldName"> </asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:DropDownList ID="ddlcategory" runat="server" CssClass="cmbfield" AutoPostBack="true">
               
          </asp:DropDownList>
   </td>
      </tr>
      <tr>
      <td>
          <asp:Label ID="lblFvalue" runat="server" Text="Face Value" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtFvale" runat="server" CssClass="cmbFielde"></asp:TextBox>
           </td>
           <td>
            <asp:Label ID="lblScategory" runat="server" Text="Sub Category" CssClass="Sub Category"> </asp:Label>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:DropDownList ID="ddlScategory" runat="server" CssClass="cmbfield" AutoPostBack="true">
               
          </asp:DropDownList>
           </td>
      </tr>
      
      <tr><td>
      <asp:Label ID="lblSctype" runat="server" Text="Scheme Type" CssClass="FieldName"> </asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="ddlSctype" runat="server" CssClass="cmbfield" AutoPostBack="true">
               
          </asp:DropDownList>
      </td>
      <td>
      <asp:Label ID="lblSScategory" runat="server" Text="Sub Sub Category" CssClass="FieldName"> </asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:DropDownList ID="ddlSScategory" runat="server" CssClass="cmbfield" AutoPostBack="true">
               
          </asp:DropDownList>
   </td>
      </tr>
      <tr><td>
      <asp:Label ID="lblOption" runat="server" Text="Option" CssClass="FieldName"> </asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:DropDownList ID="ddlOption" runat="server" CssClass="cmbfield" AutoPostBack="true">
               
          </asp:DropDownList>
      </td>
      
      <td>
      <asp:Label ID="lblDFrequency" runat="server" Text="Dividend Frequency" CssClass="FieldName"> </asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlDFrequency" runat="server" CssClass="cmbfield" AutoPostBack="true">
               
          </asp:DropDownList>
   </td>
      </tr>
      <tr><td>
      <asp:Label ID="lblBname" runat="server" Text="Bank Name" CssClass="FieldName"> </asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="ddlBname" runat="server" CssClass="cmbfield" AutoPostBack="true">
               
          </asp:DropDownList>
      </td>
      <td>
       <asp:Label ID="lblBranch" runat="server" Text="Branch" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
           <asp:TextBox ID="txtBranch" runat="server" CssClass="cmbFielde"></asp:TextBox>
      </td>
      </tr>
      <tr><td>
       <asp:Label ID="lblACno" runat="server" Text="Account Number" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtACno" runat="server" CssClass="cmbFielde"></asp:TextBox>
           </td>
           <td>
           <asp:CheckBox ID="chkInfo" runat="server" Text="Is Info" CssClass="cmbFielde"/>
           </td>
           </tr>
           <tr>
           <td><asp:Label ID="lblNfostartdate" runat="server" Text="NFO Start Date" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <telerik:RadDatePicker ID="txtReceivedDate" CssClass="txtField" runat="server" Culture="English (United States)"
                AutoPostBack="true" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                MinDate="1900-01-01" TabIndex="5">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                    <SpecialDays>
                        <%-- <telerik:RadCalendarDay Repeatable="Today" ItemStyle-BackColor="Red" />--%>
                    </SpecialDays>
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="appRecidRequiredFieldValidator" ControlToValidate="txtReceivedDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select NFO Date"
                Display="Dynamic" runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
           </td>
           <td>
           <asp:Label ID="lblNfoEnddate" runat="server" Text="NFO End Date" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <telerik:RadDatePicker ID="RadDatePicker1" CssClass="txtField" runat="server" Culture="English (United States)"
                AutoPostBack="true" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                MinDate="1900-01-01" TabIndex="5">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                    <SpecialDays>
                        <%-- <telerik:RadCalendarDay Repeatable="Today" ItemStyle-BackColor="Red" />--%>
                    </SpecialDays>
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtReceivedDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select NFO End Date"
                Display="Dynamic" runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
           </td>
           </tr>
           <tr>
           <td>
          <asp:Label ID="lblLIperiod" runat="server" Text="Lock In Period" CssClass="FieldName"></asp:Label>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtLIperiod" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblCOtime" runat="server" Text="Cut Off Time" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtCOtime" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           </tr>
            <tr>
           <td>
          <asp:Label ID="lblEload" runat="server" Text="Entry Load %" CssClass="FieldName"></asp:Label>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtEload" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblELremark" runat="server" Text="Entry Load Remark" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
           <asp:TextBox ID="txtELremark" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           </tr>
            <tr>
           <td>
          <asp:Label ID="lblExitLoad" runat="server" Text="Exit Load %" CssClass="FieldName"></asp:Label>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtExitLoad" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblExitLremark" runat="server" Text="Exit Load Remark" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtExitLremark" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           </tr>
           <tr><td>
           <asp:CheckBox ID="ChkISPurchage" runat="server" Text="Is Purchage Available" CssClass="cmbFielde"/>
           &nbsp;
           <asp:CheckBox ID="ChkISRedeem" runat="server" Text="Is Redeem Available" CssClass="cmbFielde"/>
           &nbsp;
           <asp:CheckBox ID="ChkISSIP" runat="server" Text="Is SIP Available" CssClass="cmbFielde"/>
           </td>
           <td>
           <asp:CheckBox ID="ChkISSWP" runat="server" Text="Is SWP Available" CssClass="cmbFielde"/>
           &nbsp;
           <asp:CheckBox ID="ChkISSwitch" runat="server" Text="Is Switch Available" CssClass="cmbFielde"/>
           &nbsp;
           <asp:CheckBox ID="ChkISSTP" runat="server" Text="Is STP Available" CssClass="cmbFielde"/>
           </td>
           </tr>
           <tr>
            <td>
          <asp:Label ID="lblInitalPamount" runat="server" Text="Initial Purchase Amount" CssClass="FieldName"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtInitalPamount" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblIMultipleamount" runat="server" Text="Inital Multiple Amount" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
           <asp:TextBox ID="txtIMultipleamount" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           </tr>
           <tr>
           <td>
          <asp:Label ID="lblAdditionalPamount" runat="server" Text="Additional Purchase Amount" CssClass="FieldName"></asp:Label>
                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtAdditional" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblAddMultipleamount" runat="server" Text="Additional Multiple Amount" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtAddMultipleamount" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           </tr>
           <tr>
           <td>
          <asp:Label ID="lblMinRedemption" runat="server" Text="Min Redemption Amount" CssClass="FieldName"></asp:Label>
                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtMinRedemption" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblRedemptionmultiple" runat="server" Text="Redemption Multiple Amount" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;
           <asp:TextBox ID="txtRedemptionmultiple" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           </tr>
           <tr>
            <td>
          <asp:Label ID="lblMinRedemptionUnits" runat="server" Text="Min Redemption Units" CssClass="FieldName"></asp:Label>
                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
           <asp:TextBox ID="txtMinRedemptioUnits" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblRedemptionMultiplesUnits" runat="server" Text="Redemption Multiples Units" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp; &nbsp;
           <asp:TextBox ID="txtRedemptionMultiplesUnits" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           
           </tr>
           <tr>
            <td>
          <asp:Label ID="lblMinSwitchAmount" runat="server" Text="Min Switch Amount" CssClass="FieldName"></asp:Label>
                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
           <asp:TextBox ID="txtMinSwitchAmount" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblMinSwitchUnits" runat="server" Text="Min Switch Units" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtMinSwitchUnits" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           </tr>
           <tr> <td>
          <asp:Label ID="lblSwitchMultipleAmount" runat="server" Text="Switch Multiple Amount" CssClass="FieldName"></asp:Label>
                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtSwitchMultipleAmount" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblSwitchMultipleUnits" runat="server" Text="Switch Multiples Units" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtSwitchMultipleUnits" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           </tr>
           <tr>
           <td>
      <asp:Label ID="lblGenerationfreq" runat="server" Text="File Generation Freq" CssClass="FieldName"> </asp:Label>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:DropDownList ID="ddlGenerationfreq" runat="server" CssClass="cmbfield" AutoPostBack="true">
               
          </asp:DropDownList>
      </td>
      <td>
      <asp:Label ID="lblRT" runat="server" Text="R&T" CssClass="FieldName"> </asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
          <asp:DropDownList ID="ddlRT" runat="server" CssClass="cmbfield" AutoPostBack="true">
               
          </asp:DropDownList>
   </td>
           </tr>
           <tr><td>
           <asp:CheckBox ID="ChkISactive" runat="server" Text="Is Active" CssClass="cmbFielde"/>
           </td>
           <td>
           <asp:Label ID="lblNACustomerType" runat="server" Text="Not Allowed Customer Type" CssClass="FieldName"> </asp:Label>
           &nbsp;&nbsp;
           <asp:CheckBox ID="ChkNRI" runat="server" Text="NRI" CssClass="cmbFielde"/>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:CheckBox ID="ChkBO" runat="server" Text="BO" CssClass="cmbFielde"/>
           </td>
           </tr>
           <tr>
           <td>
          <asp:Label ID="lblSecuritycode" runat="server" Text="Security Code" CssClass="FieldName"></asp:Label>
                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtSecuritycode" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
      <td>
      <asp:Label ID="lblMaxinvestment" runat="server" Text="Max Investment" CssClass="FieldName"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtinvestment" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           </tr>
           <tr>
           <td>
      <asp:Label ID="lblESSchemecode" runat="server" Text="Extarnal System Scheme Code" CssClass="FieldName"></asp:Label>
               &nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtESSchemecode" runat="server" CssClass="cmbFielde"></asp:TextBox></td>
           </tr>
           <tr>
           <td >
               <asp:Button ID="btnsubmit" runat="server" Text="Submit"  CssClass="PCGButton"/>
           </td>
           </tr>
      </table>

