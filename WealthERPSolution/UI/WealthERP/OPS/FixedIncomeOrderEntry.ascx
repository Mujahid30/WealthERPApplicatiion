


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FixedIncomeOrderEntry.ascx.cs"
    Inherits="WealthERP.OPS.FixedIncomeOrderEntry" %>
 
  

 <table runat="server" width="100%">
   
    <tr id="trCatIss" runat="server">
        <td class="leftField" style="width:20%">
            <asp:Label ID="Label3" runat="server" Text="Category: " CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 20%">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                 OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Enabled="false">
            </asp:DropDownList>
            <span id="SpanddlCategory" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorddlCategory" runat="server" ControlToValidate="ddlCategory"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select Category"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
         <td style="width:5%"></td>  
        <td align="right" style="width: 15%" Visible="false" runat="server">
            <asp:Label ID="Label4" runat="server" Text="Issuer: " CssClass="FieldName"></asp:Label>
        </td>
        <td  style="width: 50%" align="left" Visible="false" runat="server">
            <asp:DropDownList ID="ddlIssuer" runat="server" CssClass="cmbField" AutoPostBack="true"
                  OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged" >
            </asp:DropDownList>
            <span id="SpanddlIssuer" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorIssuer" runat="server" ControlToValidate="ddlIssuer"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Issuer"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="SchemeSeries" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label5" runat="server" Text="Scheme/Bond: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="SpanddlScheme" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlScheme"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Scheme"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td style="width:5%"></td>        
        <td align="right" style="width: 10%">
            <asp:Label ID="Label6" runat="server" Text="Series: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 35%">
            <asp:DropDownList ID="ddlSeries" runat="server" CssClass="Field" AutoPostBack="true"
                Width="150px" OnSelectedIndexChanged="ddlSeries_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="SpanddlSeries" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorSeries" runat="server" ControlToValidate="ddlSeries"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Series"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
       
    </tr>
    
     <tr id="TRTransSer" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label1" runat="server" Text="Transaction Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 20%">
            <asp:DropDownList ID="ddlTranstype" runat="server" CssClass="cmbField" AutoPostBack="true"
                  OnSelectedIndexChanged="ddlTranstype_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
                <asp:ListItem Text="New" Value="New"></asp:ListItem>
                <asp:ListItem Text="Renewal" Value="Renewal"></asp:ListItem>
            </asp:DropDownList>
            <span id="SpanddlTranstype" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorTranstype" runat="server" ControlToValidate="ddlTranstype"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Transaction Type"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
         <td style="width:5%"></td>  
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label15" runat="server" Text="Series Details: " CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 35%">
            <asp:TextBox ID="txtSeries" runat="server" CssClass="txtField" ReadOnly="true" Visible="false" ></asp:TextBox>
             <asp:Label ID="Label10" runat="server"  CssClass="txtField"></asp:Label>
            <%-- <span id="Span1" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlAMCList"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an AMC"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
        </td>
         
    </tr>
    <tr id="trSchemeOpFreq" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label16" runat="server" Text="Scheme Option: " CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 20%">
            <asp:DropDownList ID="ddlSchemeOption" runat="server" CssClass="cmbField" AutoPostBack="true"
                 OnSelectedIndexChanged="ddlSchemeOption_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Cummulative" Value="Cummulative"></asp:ListItem>
                <asp:ListItem Text="Non Cummulative" Value="NonCummulative"></asp:ListItem>
                <asp:ListItem Text="Annual income plan" Value="AIncPlan"></asp:ListItem>
            </asp:DropDownList>
            <span id="SpanddlSchemeOption" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorSchemeOption" runat="server" ControlToValidate="ddlSchemeOption"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an  SchemeOption"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
         <td style="width:5%"></td>  
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label17" runat="server" Text="Frequency: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 35%">
            <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                Width="150px" OnSelectedIndexChanged="ddlFrequency_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                <asp:ListItem Text="Quarterly" Value="Quarterly"></asp:ListItem>
                <asp:ListItem Text="yearly" Value="Yearly"></asp:ListItem>
                <asp:ListItem Text="Half yearly" Value="Hfyearly"></asp:ListItem>
            </asp:DropDownList>
            <span id="SpanddlFrequency" runat="server" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorFrequency" runat="server" ControlToValidate="ddlFrequency"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Frequency"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td colspan="2">
        </td>
    </tr>
    
    
    <tr id="trDepPaypriv" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label21" runat="server" Text="Deposit Payable to:" CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 20%">
            <asp:CheckBox ID="ChkFirstholder" runat="server" CssClass="txtField" Text="First holder">
            </asp:CheckBox>
            <asp:CheckBox ID="ChkEORS" runat="server" CssClass="txtField" Text="Either or survivor">
            </asp:CheckBox>
        </td>
        <td style="width:5%"></td>  
        <td  align="right"  style="width: 10%">
            <asp:Label ID="Label22" runat="server" Text="Privilege:" CssClass="FieldName"></asp:Label>
        </td>
       <%--  <td style="width:5%"></td>  --%>
        <td   style="width: 35%">
            <asp:CheckBox ID="ChkSeniorcitizens" runat="server" CssClass="txtField" Text="Senior Citizen">
            </asp:CheckBox>
            <asp:CheckBox ID="ChkWidow" runat="server" CssClass="txtField" Text="Widow"></asp:CheckBox>
            <asp:CheckBox ID="ChkArmedForcePersonnel" runat="server" CssClass="txtField" Text="Armed Force Personnel">
            </asp:CheckBox>
            <asp:CheckBox ID="CHKExistingrelationship" runat="server" CssClass="txtField" Text="Existing Relationship">
            </asp:CheckBox>
        </td>
         
    </tr>
    <tr id="trOrder" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblOrderNumber" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 20%">
            <asp:Label ID="lblGetOrderNo" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
        <td style="width:5%"></td> 
        <td class="leftField" style="width: 10%" runat="server" visible="false">
            <asp:Label ID="lblOrderDate" runat="server" Text="Order Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 35%" runat="server" visible="false"> 
            <telerik:RadDatePicker ID="txtOrderDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                AutoPostBack="true" OnSelectedDateChanged="txtOrderDate_DateChanged">
                <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="SpantxtOrderDate" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorOrderDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtOrderDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOrderDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select order date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            
        </td>
        
    </tr>
    
    
    <tr id="trARDate" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblApplicationNumber" runat="server" Text="Application No.:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td style="width:5%"></td> 
        <td class="leftField" style="width: 10%">
            <asp:Label ID="Label7" runat="server" Text="Application Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 35%">
            <telerik:RadDatePicker ID="txtApplicationDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01" AutoPostBack="true" OnSelectedDateChanged="txtApplicationDt_DateChanged">
                <Calendar ID="Calendar3" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="SpantxtApplicationDate" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorApplicationDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtOrderDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatororderdate" ControlToValidate="txtApplicationDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Application date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            
        </td>
         
    </tr>
  <%--  id="trPayAmt"--%>
    <tr  runat="server">
     <td class="leftField" style="width: 20%" runat="server" visible="false" >
            <asp:Label ID="Label18" runat="server" Text="Maturity Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td  style="width: 20%" runat="server" visible="false">
            <telerik:RadDatePicker ID="txtMaturDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                OnSelectedDateChanged="txtMaturDate_DateChanged"
                AutoPostBack="true">
                <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="SpantxtMaturDate" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorMaturDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtMaturDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMaturDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Maturity Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            
        </td>
           <td style="width:5%"></td>
           
        <td colspan="2" style="width: 35%" align="center">
        <asp:Label ID="Label11" runat="server" style="color: red"></asp:Label>
        </td>
    </tr>
    
    <tr id="trDepRen" runat="server">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblgetPan" runat="server" Text="Existing Deposit Receipt No.:" CssClass="FieldName"></asp:Label>
        </td>
        <td  style="width: 20%">
            <asp:TextBox ID="txtExistDepositreceiptno" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" />
        </td>
         <td style="width:5%"></td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblBranch" runat="server" Text="Renewal Amount: " CssClass="FieldName" OnTextChanged="OnPayAmtTextchanged"></asp:Label>
        </td>
        <td   style="width: 35%">
            <asp:TextBox ID="txtRenAmt" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" />
        </td>
        
    </tr>
    
     <tr id="trQtyAndAmt" runat="server">
         <td class="leftField" style="width: 20%">
            <asp:Label ID="lb1Qty" runat="server" Text="Qty:" CssClass="FieldName">           
            </asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtQty" runat="server" CssClass="txtField"  
                AutoPostBack="True" OnTextChanged="OnQtytchanged" />
                 <span id="Span1" runat="server" class="spnRequiredField">*</span>
                 <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPayAmt"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Enter FD Amount"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
                  <asp:RequiredFieldValidator ID="ReqQty" ControlToValidate="txtQty"
                CssClass="rfvPCG" ErrorMessage="<br />Please Enter Qty" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td style="width:5%"></td>
        <td align="right" style="width: 15%">
            <asp:Label ID="lb1PurchaseAmt" runat="server" Text="Purchase Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 30%">
            <asp:TextBox ID="TxtPurAmt" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" />
        </td>
       
    </tr>
    
    <tr id="trMatAmtDate" runat="server" visible="false">
         <td class="leftField" style="width: 20%">
            <asp:Label ID="Label8" runat="server" Text="FD Amount:" CssClass="FieldName">           
            </asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtPayAmt" runat="server" CssClass="txtField" OnTextChanged="OnPayAmtTextchanged"
                AutoPostBack="True" />
                 <span id="SpanPayAmt" runat="server" class="spnRequiredField">*</span>
                 <asp:CompareValidator ID="CompareValidatorPayamt" runat="server" ControlToValidate="txtPayAmt"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Enter FD Amount"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPayAmt"
                CssClass="rfvPCG" ErrorMessage="<br />Please Enter FD Amount" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td style="width:5%"></td>
        <td align="right" style="width: 15%">
            <asp:Label ID="Label20" runat="server" Text="Maturity Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td   style="width: 30%">
            <asp:TextBox ID="txtMatAmt" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" />
        </td>
       
    </tr>
    </table> 
    <table>
     
    <tr runat="server" visible="false">
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
               Joint Holder/Nominee Details
            </div>
        </td>
    </tr>
    <tr runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label2" runat="server" Text="Pick:" CssClass="FieldName" Visible="false" ></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:CheckBox ID="CheckBox2" runat="server" CssClass="txtField" Text="Joint Holder" Visible="false" >
            </asp:CheckBox>
            <asp:CheckBox ID="CheckBox3" runat="server" CssClass="txtField" Text="Nominee" Visible="false" ></asp:CheckBox>
        </td>
    </tr>
    <tr>
     <td>
     
     <asp:Panel ID="Panel1" runat="server" class="Landscape" Width="100%" Height="100%"
    ScrollBars="None" Visible="false">
    <table width="100%" cellspacing="10">
        <tr>
            <td colspan="3">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Demat Details
                </div>
            </td>
        </tr>
        <tr id="tdlnkbtn" runat="server">
            <td class="leftField" style="width: 20%">
                <asp:LinkButton ID="lnkBtnDemat" runat="server" OnClick="lnkBtnDemat_onClick" CssClass="LinkButtons"
                    Text="Click to select Demat Details"></asp:LinkButton>
            </td>
            <td id="Td1" class="rightField" style="width: 20%" colspan="2">
                <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                    AlternateText="Add Demat Account" runat="server" ToolTip="Click here to Add Demat Account"
                    OnClientClick="return openpopupAddDematAccount()" Height="15px" Width="15px"
                    TabIndex="3"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td class="leftField" style="width: 25%">
                <asp:Label ID="lblDpClientId" runat="server" Text="Beneficiary Acct No:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="txtDematid" Enabled="false" onkeydown="return (event.keyCode!=13);"
                    runat="server" CssClass="txtField"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtDematid"
                    ErrorMessage="<br />Please Select Demat from the List" Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlJointHolderNominee" runat="server" class="Landscape" Width="100%"
    Height="80%" ScrollBars="None" Visible="false">
    <table width="100%" cellspacing="10">
        <tr>
            <td>
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Joint Holder/Nominee Details
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="gvAssociate" runat="server" CssClass="RadGrid" GridLines="Both"
                    Visible="false" Width="90%" AllowPaging="True" PageSize="20" AllowSorting="True"
                    AutoGenerateColumns="false" ShowStatusBar="true" Skin="Telerik">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="Family Associates" Excel-Format="ExcelML">
                    </ExportSettings>
                    <%--<MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None">--%>
                    <MasterTableView DataKeyNames="CDAA_Id,CEDA_DematAccountId,CDAA_Name,CDAA_PanNum,Sex,CDAA_DOB,RelationshipName,AssociateType,CDAA_AssociateTypeNo,CDAA_IsKYC,SexShortName,CDAA_AssociateType,XR_RelationshipCode"
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemSettings-ShowRefreshButton="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="CDAA_Name" HeaderText="Member name" UniqueName="AssociateName"
                                SortExpression="AssociateName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AssociateType" HeaderText="Associate Type" UniqueName="AssociateType"
                                SortExpression="AssociateType">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_PanNum" HeaderText="PAN Number" UniqueName="CDAA_PanNum"
                                SortExpression="CDAA_PanNum">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_IsKYC" HeaderText="IsKYC" UniqueName="CDAA_IsKYC"
                                SortExpression="CDAA_IsKYC">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_DOB" HeaderText="Date Of Birth" UniqueName="CDAA_DOB"
                                SortExpression="CDAA_DOB" DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RelationshipName" HeaderText="Relationship" AllowFiltering="false"
                                UniqueName="RelationshipName" SortExpression="RelationshipName">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
     
     
     
     
     </td>
    </tr>
     
     <tr id="trJoingHolding" runat="server" visible="false">
                <td class="leftField">
                    <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="Joint Holding:"></asp:Label>
                </td>
                <td >
                    <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                        Text="Joint Holder" OnCheckedChanged="rbtnYes_CheckedChanged" AutoPostBack="true" />
                    <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                        Text="Nominee" AutoPostBack="true" OnCheckedChanged="rbtnYes_CheckedChanged" Checked="true" />
                </td>
            </tr>
            <tr id="trModeOfHolding" runat="server" visible="false">
                <td class="leftField">
                    <asp:Label ID="lblModeOfHolding" runat="server" CssClass="FieldName" Text="Mode Of Holding:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlModeofHOldingFI" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                   
                </td>
            </tr>
             
           
            <tr id="trJoinHolders" runat="server" visible="false">
                <td colspan="2">
                    <asp:Label ID="lblJointHolders" runat="server" CssClass="HeaderTextSmall" Text=""></asp:Label>
                 <%--   <hr />--%>
                </td>
            </tr>
            <tr id="trJointHolderGrid" runat="server" visible="false">
                <td colspan="2">
                    <asp:GridView ID="gvJointHoldersList" runat="server" AutoGenerateColumns="False"
                        Width="60%" CellPadding="4" DataKeyNames="MemberCustomerId, AssociationId" AllowSorting="True"
                        ShowFooter="true" CssClass="GridViewStyle">
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr id="trNoJointHolders" runat="server" visible="false">
                <td class="Message" colspan="2">
                    <asp:Label ID="lblNoJointHolders" runat="server" Text="You have no Joint Holder"
                        CssClass="FieldName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr id="trError" runat="server" visible="false">
        <td colspan="2" class="SubmitCell">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </td>
    </tr>
            <tr id="trNomineeCaption" runat="server" visible="false">
                <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Nominees
                    </div>
                </td>
            </tr>
             
           <tr id="trNominees" runat="server" visible="false">
                <td colspan="2">
                    <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Width="60%" ShowFooter="true" DataKeyNames="MemberCustomerId, AssociationId"
                        AllowSorting="True" CssClass="GridViewStyle">
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId0" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr> 
      <tr id="trNoNominee" runat="server" visible="false">
                <td class="Message" colspan="2">
                    <asp:Label ID="lblNoNominee" runat="server" Text="You have no Associations" CssClass="FieldName"></asp:Label>
                </td>
            </tr> 
   
    <tr>
    
    <td>
    
    <telerik:RadWindow ID="rwDematDetails" runat="server" VisibleOnPageLoad="false" Height="230px"
    Width="800px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Resize, Close, Move"
    Title="Select Demat " RestrictionZoneID="radWindowZone" OnClientShow="setCustomPosition"
    Top="120" Left="70">
    <ContentTemplate>
        <table>
            <tr>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="gvDematDetailsTeleR" runat="server" AllowAutomaticInserts="false"
                        AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Height="150px"
                        EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" fAllowAutomaticDeletes="false"
                        GridLines="none" ShowFooter="false" ShowStatusBar="false" Skin="Telerik">
                        <%--<HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>--%>
                        <ExportSettings HideStructureColumns="true">
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" DataKeyNames="CEDA_DematAccountId,CEDA_DPClientId"
                            Width="99%">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="Action" HeaderStyle-Width="30px"
                                    UniqueName="Action">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDematId" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="CEDA_DPName" HeaderStyle-Width="67px"
                                    HeaderText="DP Name" ShowFilterIcon="false" SortExpression="CEDA_DPName" UniqueName="CEDA_DPName">
                                    <HeaderStyle Width="67px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="CEDA_DepositoryName" HeaderStyle-Width="140px" HeaderText="Depository Name"
                                    ShowFilterIcon="false" SortExpression="CEDA_DepositoryName" UniqueName="CEDA_DepositoryName">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="67px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="CEDA_DPClientId" HeaderStyle-Width="67px" HeaderText="Beneficiary Acct No"
                                    ShowFilterIcon="false" SortExpression="CEDA_DPClientId" UniqueName="CEDA_DPClientId">
                                    <HeaderStyle Width="120px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="CEDA_DPId" HeaderStyle-Width="67px" HeaderText="DP Id" ShowFilterIcon="false"
                                    SortExpression="CEDA_DPId" UniqueName="CEDA_DPId">
                                    <HeaderStyle Width="140px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="140px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="XMOH_ModeOfHolding"
                                    HeaderStyle-Width="145px" HeaderText="Mode of holding" ShowFilterIcon="false"
                                    SortExpression="XMOH_ModeOfHolding" UniqueName="XMOH_ModeOfHolding">
                                    <HeaderStyle Width="145px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="CEDA_AccountOpeningDate"
                                    DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="145px" HeaderText="Account Opening Date"
                                    ShowFilterIcon="false" SortExpression="CEDA_AccountOpeningDate" UniqueName="CEDA_AccountOpeningDate"
                                    Visible="false">
                                    <HeaderStyle Width="145px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" InsertImageUrl="Update.gif"
                                    UpdateImageUrl="Update.gif">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="true" ScrollHeight="70px" UseStaticHeaders="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnAddDemat" runat="server" Text="Accept" CssClass="PCGButton" OnClick="btnAddDemat_Click"
                        CausesValidation="false" OnClientClick="javascript:return  TestCheckBox();" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</telerik:RadWindow>
    
    </td>
    </tr>
    
</table>



<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged1" />

<asp:HiddenField ID="hdnPortfolioId" runat="server" />
  
<asp:HiddenField ID="hdnDefaulteInteresRate" runat="server" />
<asp:HiddenField ID="hdnOrderId" runat="server" />
<asp:HiddenField ID="hdnNewFileName" runat="server" />
<asp:HiddenField ID="hdnFrequency" runat="server" />
 <asp:HiddenField ID="hdnMintenure" runat="server" />
 <asp:HiddenField ID="hdnMaxtenure" runat="server" />
 
 