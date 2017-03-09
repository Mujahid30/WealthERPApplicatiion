<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPRecommendation.ascx.cs" Inherits="WealthERP.FP.CustomerFPRecommendation" %>




       <asp:Panel ID="pnlCustomizedtext" runat="server">
       <table width="100%">
       <tr>
          <td colspan="2">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                <tr>
                    <td align="left">Recommendation</td>
                    <td align="right">
                    <asp:LinkButton ID="btnEditRMRec" runat="server" Text="Edit" CssClass="LinkButtons" 
               OnClick="btnEditRMRec_Click"></asp:LinkButton>
                    </td>
                </tr>
                </table>
            </div>
        </td>
    </tr>
      
       <tr>
       <td colspan="2">
       &nbsp;&nbsp;&nbsp;&nbsp;
       </td>
       </tr>
       
       <tr>
       <td align="right">
       <asp:Label ID="lblRMPara1" runat="server" Text="Paragraph One" CssClass="FieldName">
       </asp:Label>
       </td>    
       <td align="left">
       <asp:TextBox ID="txtParagraph1" runat="server" CssClass="txtField" Height="85px" Width="750px" TextMode="MultiLine"></asp:TextBox>
       </td>
       </tr>
       
       <tr>
       <td align="right">
       <asp:Label ID="lblRMPara2" runat="server" Text="Paragraph Two" CssClass="FieldName">
       </asp:Label>
       </td>    
       <td align="left">
       <asp:TextBox ID="txtParagraph2" runat="server" CssClass="txtField" Height="85px" Width="750px" TextMode="MultiLine"></asp:TextBox>
       </td>
       </tr>
       
       <tr>
       <td align="right">
       <asp:Label ID="lblRMPara3" runat="server" Text="Paragraph Three" CssClass="FieldName">
       </asp:Label>
       </td>    
       <td align="left">
       <asp:TextBox ID="txtParagraph3" runat="server" CssClass="txtField" Height="85px" Width="750px" TextMode="MultiLine"></asp:TextBox>
       </td>
       </tr>
       
       <tr>
       <td align="right">
       <asp:Label ID="lblRMPara4" runat="server" Text="Paragraph Four" CssClass="FieldName">
       </asp:Label>
       </td>    
       <td align="left">
       <asp:TextBox ID="txtParagraph4" runat="server" CssClass="txtField" Height="85px" Width="750px" TextMode="MultiLine"></asp:TextBox>
       </td>
       </tr>
       
       <tr>
       <td align="right">
       <asp:Label ID="lblRMPara5" runat="server" Text="Paragraph Five" CssClass="FieldName">
       </asp:Label>
       </td>    
       <td align="left">
       <asp:TextBox ID="txtParagraph5" runat="server" CssClass="txtField" Height="85px" Width="750px" TextMode="MultiLine"></asp:TextBox>
       </td>
       </tr>
       
       <tr>
       <td align="right">       
       &nbsp;&nbsp;&nbsp;&nbsp;
       </td>    
       <td align="left">
       <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSave_OnClick" />
              
       </td>
       </tr>
       
       </table>

      </asp:Panel>