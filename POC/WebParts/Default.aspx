<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/PortfolioWebpart.ascx" TagName="PortfolioWebPart" TagPrefix="uc5" %>

<%@ Register Src="ConsumerWebPart.ascx" TagName="ConsumerWebPart" TagPrefix="uc4" %>

<%@ Register Src="ProviderWebPart.ascx" TagName="ProviderWebPart" TagPrefix="uc3" %>

<%@ Register Src="ProgrammableWebPart.ascx" TagName="ProgrammableWebPart" TagPrefix="uc2" %>

<%@ Register Src="Welcome.ascx" TagName="Welcome" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>WealthERP.com - WebParts Demo</title>
    <style>
        <!--
            td { font-family: Arial,Verdana,Tahoma; font-size: 10pt}
            .TitleBar
            {
                background-image: url("Images/titlebargradient.jpg");
                background-repeat: repeat-x;
                background-position: 0 0;
                color: White
            }

        -->
    </style>
    
    
</head>
<body topmargin="0" leftmargin="0" bottommargin="0" rightmargin="0">
    <form id="form1" runat="server">
    <div>
        <asp:WebPartManager ID="WebPartManager1" runat="server">
        </asp:WebPartManager>
    
    </div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table cellpadding="4" cellspacing="0" width="100%" height="64px">
                        <tr>
                            <td align="left" valign="middle">
                                <strong><span style="font-size: 16pt;">WealthERP.com WebParts Home</span></strong>
                                <br />
                                <asp:Label ID="lblStatus" ForeColor="Red" runat="server"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <span style="color: #9090ff">
                                    <asp:LinkButton ID="LinkButton1" ForeColor="#9090ff" Text="Browse"
                                    style="Text-Decoration: none" Runat="server" OnClick="LinkButton1_Click" /> |
                                    <asp:LinkButton ID="LinkButton2" ForeColor="#9090ff" Text="Design" 
                                    style="Text-Decoration: none" Runat="server" OnClick="LinkButton2_Click" /> |
                                    <asp:LinkButton ID="LinkButton3" ForeColor="#9090ff" Text="Catalog"
                                    style="Text-Decoration: none" Runat="server" OnClick="LinkButton3_Click" /> |
                                    <asp:LinkButton ID="LinkButton4" ForeColor="#9090ff" Text="Properties"
                                    style="Text-Decoration: none" Runat="server" OnClick="LinkButton4_Click" /> |
                                    <asp:LinkButton ID="LinkButton5" ForeColor="#9090ff" Text="Connections"
                                    style="Text-Decoration: none" Runat="server" OnClick="LinkButton5_Click" />&nbsp;
                                </span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#c0c0ff" height="1px">
                </td>
            </tr>
            <tr>
                <td >
                    <table cellpadding="4" cellspacing="0" width="3px">
                        <tr>
                            <td width="1px" align="left" valign="top">
                                <asp:WebPartZone ID="WebPartZone1" runat="server" HeaderText="Zone 1" PartTitleStyle-CssClass="TitleBar" PartChromeType="TitleOnly">
<PartTitleStyle CssClass="TitleBar"></PartTitleStyle>
                                    <ZoneTemplate>
                                        <uc1:Welcome ID="Welcome1" Title="Welcome to WealthERP.com" runat="server" OnLoad="Welcome1_Load" />
                                    </ZoneTemplate>
                                    
                                </asp:WebPartZone>
                            </td>
                            <td width="1px" align="left" valign="top">
                                 <asp:WebPartZone ID="WebPartZone2" runat="server" PartTitleStyle-CssClass="TitleBar" PartChromeType="TitleOnly" HeaderText="Zone 2">
                                    <ZoneTemplate>
                                     <asp:Calendar ID="myCalendar" Title="Calendar" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px">
                                         <DayStyle Width="14%" />
                                         <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                             ForeColor="#333333" Width="1%" />
                                         <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                                         <TodayDayStyle BackColor="#CCCC99" />
                                         <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                                         <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White"
                                             Height="14pt" />
                                         <OtherMonthDayStyle ForeColor="#999999" />
                                         <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333"
                                             Height="10pt" />
                                     </asp:Calendar>
                                    </ZoneTemplate> 
                                </asp:WebPartZone>
                                
                            </td>
                            <td width="1px" align="left" valign="top">
                            <asp:WebPartZone ID="WebPartZone3" runat="server" HeaderText="Zone 3" PartTitleStyle-CssClass="TitleBar" PartChromeType="TitleOnly">
                        <ZoneTemplate>
                            <uc1:Welcome ID="Welcome2" Title="Welcome" runat="server" />
                        </ZoneTemplate>
                        <PartTitleStyle CssClass="TitleBar" />
                    </asp:WebPartZone>
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                    
                    <table cellpadding="4" cellspacing="0" width="3px">
                        <tr>                    
                            <td width="1px" align="left" valign="top">
                                <asp:CatalogZone ID="CatalogZone1" runat="server" BackColor="#F7F6F3" BorderColor="#CCCCCC"
                                    BorderWidth="1px" Font-Names="Verdana" Padding="6">
                                    <HeaderVerbStyle Font-Bold="False" Font-Size="0.8em" Font-Underline="False" ForeColor="#333333" />
                                    <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                                    <FooterStyle BackColor="#E2DED6" HorizontalAlign="Right" />
                                    <PartChromeStyle BorderColor="#E2DED6" BorderStyle="Solid" BorderWidth="1px" />
                                    <InstructionTextStyle Font-Size="0.8em" ForeColor="#333333" />
                                    <LabelStyle Font-Size="0.8em" ForeColor="#333333" />
                                    <ZoneTemplate>
                                        <asp:PageCatalogPart ID="PageCatalogPart1" runat="server" />
                                        <asp:DeclarativeCatalogPart ID="DeclarativeCatalogPart1" runat="server">
                                            <WebPartsTemplate>
                                                <uc3:ProviderWebPart ID="ProviderWebPart1" runat="server" Title = "Provider Web Part"/>
                                                <uc4:ConsumerWebPart ID="ConsumerWebPart1" runat="server" Title = "Consumer Web Part"/>
                                                <uc5:PortfolioWebPart ID="PortfolioWebPart1" runat="server" Title = "Portfolio Web Part"/>
                                             </WebPartsTemplate>
                                        </asp:DeclarativeCatalogPart>
                                        <asp:ImportCatalogPart ID="ImportCatalogPart1" runat="server" />
                                    </ZoneTemplate>
                                    
                                    <PartLinkStyle Font-Size="0.8em" />
                                    <SelectedPartLinkStyle Font-Size="0.8em" />
                                    <VerbStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#E2DED6" Font-Bold="True" Font-Size="0.8em" ForeColor="#333333" />
                                    <EmptyZoneTextStyle Font-Size="0.8em" ForeColor="#333333" />
                                    <EditUIStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                                    <PartStyle BorderColor="#F7F6F3" BorderWidth="5px" />
                                </asp:CatalogZone>
                            </td>
                            <td width="1px" align="left" valign="top">
                                <asp:EditorZone ID="EditorZone1" runat="server" BackColor="#FFFBD6" BorderColor="#CCCCCC"
                                    BorderWidth="1px" Font-Names="Verdana" Padding="6">
                                    <LabelStyle Font-Size="0.8em" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#FFCC66" Font-Bold="True" Font-Size="0.8em" ForeColor="#333333" />
                                    <ZoneTemplate>
                                        <asp:AppearanceEditorPart ID="AppearanceEditorPart1" runat="server" />
                                        <asp:BehaviorEditorPart ID="BehaviorEditorPart1" runat="server" />
                                        <asp:LayoutEditorPart ID="LayoutEditorPart1" runat="server" />
                                    </ZoneTemplate>
                                    <HeaderVerbStyle Font-Bold="False" Font-Size="0.8em" Font-Underline="False" ForeColor="#333333" />
                                    <PartChromeStyle BorderColor="#FFCC66" BorderStyle="Solid" BorderWidth="1px" />
                                    <PartStyle BorderColor="#FFFBD6" BorderWidth="5px" />
                                    <FooterStyle BackColor="#FFCC66" HorizontalAlign="Right" />
                                    <EditUIStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                                    <InstructionTextStyle Font-Size="0.8em" ForeColor="#333333" />
                                    <ErrorStyle Font-Size="0.8em" />
                                    <VerbStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                                    <EmptyZoneTextStyle Font-Size="0.8em" ForeColor="#333333" />
                                    <PartTitleStyle Font-Bold="True" Font-Size="0.8em" ForeColor="#333333" />
                                </asp:EditorZone>
                            </td>
                            <td width="1px" align="left" valign="top">
                            </td>
                        </tr>
                    </table>
                    <asp:ConnectionsZone ID="ConnectionsZone1" runat="server" BackColor="#F7F6F3" BorderColor="#CCCCCC"
                        BorderWidth="1px" Font-Names="Verdana" Padding="6">
                        <FooterStyle BackColor="#E2DED6" HorizontalAlign="Right" />
                        <VerbStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                        <InstructionTextStyle Font-Size="0.8em" ForeColor="#333333" />
                        <EditUIStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                        <LabelStyle Font-Size="0.8em" ForeColor="#333333" />
                        <HeaderStyle BackColor="#E2DED6" Font-Bold="True" Font-Size="0.8em" ForeColor="#333333" />
                        <HeaderVerbStyle Font-Bold="False" Font-Size="0.8em" Font-Underline="False" ForeColor="#333333" />
                    </asp:ConnectionsZone>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
