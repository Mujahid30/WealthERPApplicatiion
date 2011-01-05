<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FinanceCalculator.aspx.cs" Inherits="FinanceCalculator" %>
<%@ Register Src="~/UserControl/FinanceCalculator.ascx" TagPrefix="UC" TagName="Calculator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<UC:Calculator ID="ucCalculator" runat="server" />
</asp:Content>

