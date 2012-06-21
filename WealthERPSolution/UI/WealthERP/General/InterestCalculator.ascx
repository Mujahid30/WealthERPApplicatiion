<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InterestCalculator.ascx.cs"
    Inherits="WealthERP.General.InterestCalculator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<cc1:ToolkitScriptManager ID="scriptManager" runat="server">
</cc1:ToolkitScriptManager>
<%--<script type="text/javascript">
    function setHourglass() {
        document.body.style.cursor = 'wait';
    }
</script>--%>
<link href="../Images/FinCalc/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../Images/FinCalc/datepicker.css" rel="stylesheet" type="text/css" />

<script src="../Images/FinCalc/datepicker.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    function displayCalendar() {
        //        alert('Hi');
        //        var datePicker = document.getElementById('datePicker');
        //        datePicker.style.display = 'block';
    }
</script>

<asp:UpdatePanel ID="upPanel" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" border="0" class="mainTable" width="100%">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" width="450px" align="center">
                        <!-- Header -->
                        <tr>
                            <td>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <%--<td width="1%">
                                                <img src="../Images/FinCalc/form_left_top.gif" alt="form" width="14" height="20">
                                            </td>--%>
                                            <td>
                                                <div class="calcTopDiv"  style="text-align: center">
                                                    <asp:Label ID="Label1" runat="server" class="LinkButtons" ForeColor="White">Interest Calculator</asp:Label>
                                                </div>
                                            </td>
                                            <%--<td  height="20" colspan="3" style="padding-left: 14px; text-align:center; background-image: url(&quot;Images/FinCalc/form-corners2.gif&quot;);"
                                                width="98%">                                            
                                                <span class="FieldName">Interest Calculator</span>
                                            </td>--%>
                                            <%--<td width="1%">
                                                <img src="../Images/FinCalc/form_right_top.gif" alt="form" width="14" height="20">
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td class="calcTdSmall">
                                            <asp:Label Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                <div class="calcTopDiv">
                                                    <asp:Label runat="server" class="FieldName">Interest Calculator</asp:Label>
                                                </div>
                                                <div class="calcBottomDiv">
                                                </div>
                                                <div class="calcRectangularDiv ">
                                                </div>
                                            </td>
                                        </tr>--%>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <!-- Instrument Section -->
                        <tr>
                            <td class="calcTd">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <div class="calcRectangularDiv " >
                                                <asp:Label style="margin-left:10px" ID="Label2" runat="server" class="LinkButtons" ForeColor="White">Select Instrument</asp:Label>
                                            </div>
                                            <%--  <td height="20" colspan="3" style="padding-left: 14px; background-image: url(&quot;Images/FinCalc/form-corners2.gif&quot;);"
                                                width="98%">
                                                <span class="FieldName">Select Instrument</span>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td colspan="3" cellpadding="0" cellspacing="0" align="center">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlInstType" CssClass="cmbLongField" runat="server" AutoPostBack="true"
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
                        <%--<tr>
							<td bgcolor="#c5e9fe" height="50px" valign="top">
								<table width="100%" border="0" cellpadding="0" cellspacing="0">
									<tbody>
										<tr>
											<td height="20" colspan="3" style="padding-left: 14px; background-image: url(&quot;images/form-corners2.gif&quot;);"
												width="98%">
												S<span class="highlights">elect Output</span>
											</td>
										</tr>
										<tr >
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
						</tr>--%>
                        <!-- Input Section -->
                        <tr>
                            <td class="calcTdCenter">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <%--<td height="20" colspan="3" style="padding-left: 14px; background-image: url(&quot;Images/FinCalc/form-corners2.gif&quot;);"
                                                width="98%">
                                                <span class="FieldName">Enter Input</span>
                                            </td>--%>
                                            <td>
                                                <div class="calcRectangularDiv">
                                                    <asp:Label style="margin-left:10px" ID="Label4" runat="server" class="LinkButtons" ForeColor="White">Enter Input</asp:Label>
                                                </div>
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
                                                            <asp:Button CssClass="PCGButton" ID="btnClear" Text="Clear" runat="server" OnClick="btnClear_Click">
                                                            </asp:Button>
                                                            <asp:Button CssClass="PCGButton" ID="btnCalculate" Text="Calculate" runat="server"
                                                                OnClick="btnCalculate_Click"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:UpdateProgress ID="Progress" AssociatedUpdatePanelID="upPanel" runat="server">
                                                                <ProgressTemplate>
                                                                    <img id="Img1" alt="" src="../Images/FinCalc/ajax-loader.gif" runat="server" /></ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30px" align="left" valign="bottom">
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="350px" />
                                                        </td>
                                                    </tr>
                                                    <tr style="display: none" id="trError" runat="server">
                                                        <td height="30px" align="center" valign="top">
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
                            <td >
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr  style="margin-left:5PX">
                                            <%--<td height="20" colspan="3" style="padding-left: 14px; background-image: url(&quot;Images/FinCalc/form-corners2.gif&quot;);"
                                                width="98%">
                                                <span class="FieldName">Results</span>
                                            </td>--%>
                                            <td>
                                                <div class="calcRectangularDiv">
                                                    <asp:Label style="margin-left:10px" ID="Label5" runat="server" class="LinkButtons" ForeColor="White">Results</asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="calcTdSmall">
                                            <td colspan="3" align="center">
                                                <table border="0" cellpadding="0" cellspacing="0" width="98%" id="tblResult" runat="server">
                                                    <tr>
                                                        <td align="left" style="padding-left: 30px;">
                                                            <asp:Label Style="font-size: 11px;" ID="lblResult" runat="server"></asp:Label>
                                                            <asp:Label Style="font-size: 11px; font-weight: bold;" ID="lblResultValue" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="calcBottomDiv">
                                                    <asp:Label ID="Label3" runat="server" class="LinkButtons"></asp:Label>
                                                </div>
                                            </td>
                                            <%--<td width="1%">
                                                <img src="../Images/FinCalc/form_left_bottom.gif" alt="form" width="14" height="20">
                                            </td>
                                            <td style="background-image: url(&quot;Images/FinCalc/form-corners2.gif&quot;);"
                                                width="98%">
                                                &nbsp;
                                            </td>
                                            <td width="1%">
                                                <img src="../Images/FinCalc/form_right_bottom.gif" alt="form" width="14" height="20">
                                            </td>--%>
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
