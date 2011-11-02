<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoldPriceList.ascx.cs" Inherits="WealthERP.Admin.GoldPriceList" %>
 <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
  <%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
   <asp:ScriptManager ID="ScriptManager1" runat="server" />
   
   <table style="width:60%">
        <tr>
            <td class="HeaderCell">
               
                   <asp:Label ID="lblheader" runat="server" Class="HeaderTextBig" Text="Gold Price Query"></asp:Label>
                   
            </td>
        </tr>
        <tr><td></td></tr>
        <tr><td></td></tr>
        <tr><td></td></tr>
    </table>
   
  <table width="60%">
          <tr>
          <td></td>
        <td class="leftField">
            <asp:Label class="FieldName" ID="lblFrom" runat="server" Text="From :"></asp:Label>
        </td>
        <td class="rightField" style="width:5px">
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField" 
                ValidationGroup="btnShowBetweendates" ></asp:TextBox>
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
  <td colspan="6"></td>
  </tr>
    
    <tr>
  <td colspan="6"></td>
  </tr>
     <tr>
  <td colspan="6"></td>
  </tr> 
    <tr>
    <td colspan="2"></td>
    <td><asp:Button ID="btnShowBetweendates" runat="server" Text="Go" 
            onclick="btnShowBetweendates_Click" CssClass="PCGButton" ValidationGroup="btnShowBetweendates"/>     
        </td>
        <td></td>
        <td colspan="2">
        
        </td>
       
    </tr>
   
    </table>
    
     <table  id="tblErrorMassage" width="100%" visible="false" cellpadding="0" cellspacing="0" runat="server">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" align="center">
            </div>
        </td>
    </tr>
 </table>
    <table width="50%" style="margin-left:50">
     <tr align="right" id="trPagger" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    </table>
    <table style="margin-left:50;" width="50%">
    <tr >
    <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
            <td>
   
          <asp:GridView ID="GridViewDetails" runat="server" CssClass="GridViewStyle" 
              AutoGenerateColumns="False" DataKeyNames="PG_ID"
           CellPadding="4" ShowHeader="true" ShowFooter="true" 
              Width="100%" AllowSorting="true" 
            onrowdatabound="GridViewDetails_RowDataBound">
           
                                <RowStyle CssClass="RowStyle"  />
                        <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
        <Columns>
       
        <asp:TemplateField HeaderText="Date" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"> 
        <HeaderTemplate>
                                <asp:Label ID="lblParent" runat="server" Text="Date"></asp:Label>
                              
                              
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
        </td>
        </tr>
        
        </table>
        <table width="55%">
                    <tr align="center">
                        <td align="center">
                            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                        </td>
                    </tr>
                </table>
        
        
        <asp:HiddenField ID="hdnRecordCount" runat="server"  EnableViewState="true"/>
        <asp:HiddenField ID="hdnCurrentPage" runat="server"  EnableViewState="true"/>
        <asp:HiddenField ID="hdnCount" runat="server"  EnableViewState="true"/>