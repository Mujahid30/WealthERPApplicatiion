<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FinanceCalculator.ascx.cs"
	Inherits="UserControl_FinanceCalculator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<cc1:ToolkitScriptManager ID="scriptManager" runat="server">
</cc1:ToolkitScriptManager>
<asp:UpdatePanel ID="upPanel" runat="server">
	<ContentTemplate>
		<table cellpadding="0" cellspacing="0" border="0" class="mainTable" width="100%">
			<tr>
				<td>
					<table cellpadding="0" cellspacing="0" border="0" width="380" align="center">
						<!-- Header -->
						<tr>
							<td bgcolor="#c5e9fe" height="30px" valign="top">
								<table width="100%" border="0" cellpadding="0" cellspacing="0">
									<tbody>
										<tr>
											<td width="1%">
												<img src="images/form_left_top.gif" alt="form" width="14" height="20">
											</td>
											<td align="center" style="background-image: url(&quot;images/form-corners2.gif&quot;);"
												width="98%">
												I<span class="highlights">nterest Calculator</span>
											</td>
											<td width="1%">
												<img src="images/form_right_top.gif" alt="form" width="14" height="20">
											</td>
										</tr>
									</tbody>
								</table>
							</td>
						</tr>
						<!-- Instrument Section -->
						<tr>
							<td bgcolor="#c5e9fe" height="50px" valign="top">
								<table width="100%" border="0" cellpadding="0" cellspacing="0">
									<tbody>
										<tr>
											<td height="20" colspan="3" style="padding-left: 14px; background-image: url(&quot;images/form-corners2.gif&quot;);"
												width="98%">
												S<span class="highlights">elect Instrument</span>
											</td>
										</tr>
										<tr>
											<td colspan="3" cellpadding="0" cellspacing="0" align="center">
												<table>
													<tr>
														<td>
															<asp:DropDownList ID="ddlInstType" CssClass="dropdownlist" runat="server" AutoPostBack="true"
																OnSelectedIndexChanged="ddlInstType_SelectedIndexChanged" Width="250px">
															</asp:DropDownList>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</tbody>
								</table>
							</td>
						</tr>
						<!-- Output Section -->
						<tr>
							<td bgcolor="#c5e9fe" height="50px" valign="top">
								<table width="100%" border="0" cellpadding="0" cellspacing="0">
									<tbody>
										<tr>
											<td height="20" colspan="3" style="padding-left: 14px; background-image: url(&quot;images/form-corners2.gif&quot;);"
												width="98%">
												S<span class="highlights">elect Output</span>
											</td>
										</tr>
										<tr>
											<td colspan="3" align="center">
												<table>
													<tr>
														<td>
															<asp:DropDownList ID="ddlInstOutputType" runat="server" CssClass="dropdownlist" AutoPostBack="true"
																OnSelectedIndexChanged="ddlInstOutputType_SelectedIndexChanged" Width="320px">
															</asp:DropDownList>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</tbody>
								</table>
							</td>
						</tr>
						<!-- Input Section -->
						<tr>
							<td height="235px" bgcolor="#c5e9fe" valign="top">
								<table width="100%" border="0" cellpadding="0" cellspacing="0">
									<tbody>
										<tr>
											<td height="20" colspan="3" style="padding-left: 14px; background-image: url(&quot;images/form-corners2.gif&quot;);"
												width="98%">
												E<span class="highlights">nter Input</span>
											</td>
										</tr>
										<tr>
											<td colspan="3" align="center">
												<table cellpadding="0" cellspacing="0" border="0" width="98%" id="tblInput" runat="server"
													style="display: none" visible="false">
													<tr>
														<td>
														    <asp:UpdatePanel ID="cntltemplate" runat="server" UpdateMode="Always">
														        
															    <ContentTemplate>
															        <div id="pnlInputFields" runat="server">
															        </div>
															    </ContentTemplate>
															</asp:UpdatePanel>
														</td>
													</tr>
													<tr valign="bottom">
														<td colspan="2" valign="bottom" align="center">
															<asp:Button Style="font-size: 10px;" ID="btnClear" Text="Clear" runat="server" OnClick="btnClear_Click">
															</asp:Button>
															<asp:Button Style="font-size: 10px;" ID="btnCalculate" Text="Calculate" runat="server"
																OnClick="btnCalculate_Click"></asp:Button>
														</td>
													</tr>
													<tr>
														<td>
															<asp:UpdateProgress ID="Progress" AssociatedUpdatePanelID="upPanel" runat="server">
																<ProgressTemplate><img alt="" src="../images/ajax-loader.gif" runat="server"/></ProgressTemplate>
															</asp:UpdateProgress>
														</td>
													</tr>
													<tr>
														<td height="60px" align="left" valign="bottom">
															<asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="350px" />
														</td>
													</tr>
													<tr style="display: none" id="trError" runat="server">
														<td height="60px" align="center" valign="top">
															<asp:Label ID="lblMessage" CssClass="errorLabel" runat="server" EnableViewState="false"></asp:Label>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</tbody>
								</table>
							</td>
						</tr>
						<!-- Results Section -->
						<tr>
							<td bgcolor="#c5e9fe" valign="top">
								<table width="100%" border="0" cellpadding="0" cellspacing="0">
									<tbody>
										<tr>
											<td height="20" colspan="3" style="padding-left: 14px; background-image: url(&quot;images/form-corners2.gif&quot;);"
												width="98%">
												R<span class="highlights">esults</span>
											</td>
										</tr>
										<tr>
											<td colspan="3" align="center">
												<table border="0" cellpadding="0" cellspacing="0" width="98%" id="tblResult" runat="server">
													<tr>
														<td align="center">
															<asp:Label Style="font-size: 11px;" ID="lblResult" runat="server"></asp:Label>
															<asp:Label Style="font-size: 11px; font-weight: bold;" ID="lblResultValue" runat="server"></asp:Label>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td width="1%">
												<img src="images/form_left_bottom.gif" alt="form" width="14" height="20">
											</td>
											<td style="background-image: url(&quot;images/form-corners2.gif&quot;);" width="98%">
												&nbsp;
											</td>
											<td width="1%">
												<img src="images/form_right_bottom.gif" alt="form" width="14" height="20">
											</td>
										</tr>
									</tbody>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</ContentTemplate>
</asp:UpdatePanel>
