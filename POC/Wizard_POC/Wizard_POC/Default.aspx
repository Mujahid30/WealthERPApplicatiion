<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Wizard_POC._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <div>
    <table width="100%">
    <tr><td colspan="6">
        <asp:Wizard ID="wFinancialPlanning" runat="server" EnableTheming="True" 
            ActiveStepIndex="0" BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderWidth="1px" 
            Font-Names="Verdana" Font-Size="0.8em" SideBarStyle-Width="15%">
            <StepStyle Font-Size="0.8em" ForeColor="#333333" />
            <WizardSteps>
                <asp:WizardStep ID="wsRiskProfiling" runat="server" Title="Risk Profiling">
                <table width="75%">
                <tr>
                <td colspan="6"><strong>Risk Profiler Questionnaire</strong></td>
                </tr>
                <tr><td colspan="6"><hr /></td></tr>
                <tr>
                <td colspan="6" >1. <asp:Label runat="server" ID="lblQ1" Text="" Font-Bold="true"></asp:Label></td>
                </tr>
                <tr>
                <td><asp:RadioButton ID="rbtnQ1A1" runat="server" AutoPostBack="false" Text="" GroupName="Q1" Visible="false" /> </td>
                <td><asp:RadioButton ID="rbtnQ1A2" runat="server" AutoPostBack="false" Text="" GroupName="Q1" Visible="false" /> </td>
                <td><asp:RadioButton ID="rbtnQ1A3" runat="server" AutoPostBack="false" Text="" GroupName="Q1" Visible="false" /> </td>
                <td><asp:RadioButton ID="rbtnQ1A4" runat="server" AutoPostBack="false" Text="" GroupName="Q1" Visible="false" /> </td>
                <td><asp:RadioButton ID="rbtnQ1A5" runat="server" AutoPostBack="false" Text="" GroupName="Q1" Visible="false" /> </td>
                <td><asp:RadioButton ID="rbtnQ1A6" runat="server" AutoPostBack="false" Text="" GroupName="Q1" Visible="false" /> </td>

                </tr>
                     <tr><td colspan="6"><hr /></td></tr>
                <tr>
                    </caption>
                        <td colspan="6">
                            2.
                            <asp:Label ID="lblQ2" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ2A1" runat="server" GroupName="Q2" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ2A2" runat="server" GroupName="Q2" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ2A3" runat="server" GroupName="Q2" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ2A4" runat="server" GroupName="Q2" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ2A5" runat="server" GroupName="Q2" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ2A6" runat="server" GroupName="Q2" Visible="False" />
                        </td>
                    </tr>
                     <tr><td colspan="6"><hr /></td></tr>
                <tr>
                    <td colspan="6">
                        3.
                        <asp:Label ID="lblQ3" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ3A1" runat="server" GroupName="Q3" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ3A2" runat="server" GroupName="Q3" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ3A3" runat="server" GroupName="Q3" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ3A4" runat="server" GroupName="Q3" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ3A5" runat="server" GroupName="Q3" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ3A6" runat="server" GroupName="Q3" Visible="False" />
                        </td>
                    </tr>
                     <tr><td colspan="6"><hr /></td></tr>
                <tr>
                    <td colspan="6">
                        4.
                        <asp:Label ID="lblQ4" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ4A1" runat="server" GroupName="Q4" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ4A2" runat="server" GroupName="Q4" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ4A3" runat="server" GroupName="Q4" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ4A4" runat="server" GroupName="Q4" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ4A5" runat="server" GroupName="Q4" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ4A6" runat="server" GroupName="Q4" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            5.
                            <asp:Label ID="lblQ5" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ5A1" runat="server" GroupName="Q5" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ5A2" runat="server" GroupName="Q5" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ5A3" runat="server" GroupName="Q5" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ5A4" runat="server" GroupName="Q5" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ5A5" runat="server" GroupName="Q5" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ5A6" runat="server" GroupName="Q5" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            6.
                            <asp:Label ID="lblQ6" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ6A1" runat="server" GroupName="Q6" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ6A2" runat="server" GroupName="Q6" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ6A3" runat="server" GroupName="Q6" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ6A4" runat="server" GroupName="Q6" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ6A5" runat="server" GroupName="Q6" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ6A6" runat="server" GroupName="Q6" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            7.
                            <asp:Label ID="lblQ7" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <img ID="imgPortfolios" src="Images/Portfolios.jpg" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ7A1" runat="server" GroupName="Q7" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ7A2" runat="server" GroupName="Q7" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ7A3" runat="server" GroupName="Q7" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ7A4" runat="server" GroupName="Q7" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ7A5" runat="server" GroupName="Q7" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ7A6" runat="server" GroupName="Q7" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            8.
                            <asp:Label ID="lblQ8" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ8A1" runat="server" GroupName="Q8" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ8A2" runat="server" GroupName="Q8" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ8A3" runat="server" GroupName="Q8" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ8A4" runat="server" GroupName="Q8" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ8A5" runat="server" GroupName="Q8" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ8A6" runat="server" GroupName="Q8" Visible="False" />
                        </td>
                    </tr>
                     <tr><td colspan="6"><hr /></td></tr>
                <tr>
                    <td colspan="6">
                        9.
                        <asp:Label ID="lblQ9" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ9A1" runat="server" GroupName="Q9" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ9A2" runat="server" GroupName="Q9" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ9A3" runat="server" GroupName="Q9" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ9A4" runat="server" GroupName="Q9" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ9A5" runat="server" GroupName="Q9" Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ9A6" runat="server" GroupName="Q9" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            10.
                            <asp:Label ID="lblQ10" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ10A1" runat="server" GroupName="Q10" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ10A2" runat="server" GroupName="Q10" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ10A3" runat="server" GroupName="Q10" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ10A4" runat="server" GroupName="Q10" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ10A5" runat="server" GroupName="Q10" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ10A6" runat="server" GroupName="Q10" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            11.
                            <asp:Label ID="lblQ11" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ11A1" runat="server" GroupName="Q11" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ11A2" runat="server" GroupName="Q11" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ11A3" runat="server" GroupName="Q11" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ11A4" runat="server" GroupName="Q11" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ11A5" runat="server" GroupName="Q11" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ11A6" runat="server" GroupName="Q11" 
                                Visible="False" />
                        </td>
                    </tr>
                     <tr><td colspan="6"><hr /></td></tr>
                <tr>
                    <td colspan="6">
                        12.
                        <asp:Label ID="lblQ12" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtnQ12A1" runat="server" GroupName="Q12" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ12A2" runat="server" GroupName="Q12" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ12A3" runat="server" GroupName="Q12" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ12A4" runat="server" GroupName="Q12" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ12A5" runat="server" GroupName="Q12" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnQ12A6" runat="server" GroupName="Q12" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="6">
                            <asp:Button ID="btnSubmitRisk" runat="server" OnClick="btnSubmitRisk_Click" 
                                Text="Submit" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <strong>Risk Score:</strong><asp:Label ID="lblRScore" runat="server" 
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <strong>Risk Class:</strong><asp:Label ID="lblRClass" runat="server" 
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
                </asp:WizardStep>
                <asp:WizardStep ID="wsGoalCalculator" runat="server" Title="Goal Calculator">
                <table>
                <tr>
                <td><strong>Goal Calculator</strong></td>
                </tr>
                 <tr>
                <td><asp:DropDownList ID="ddlGoal" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlGoal_SelectedIndexChanged">
                <asp:ListItem Text="Select Your Goal" Value="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Education" Value="Education"></asp:ListItem>
                <asp:ListItem Text="Marriage" Value="Marriage"></asp:ListItem>
                <asp:ListItem Text="Home" Value="Home"></asp:ListItem>
                <asp:ListItem Text="Retirement" Value="Retirement"></asp:ListItem>                
                </asp:DropDownList></td>
                <td><asp:Label ID="lblDate" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                <td>
                <div id="divCalculator" runat="server" visible="false">
                <table id="tblCalculator" runat="server" style="border-style:solid">
                <tr>
                <td colspan="2"><asp:Label ID="lblCalculator" Text="Calculator" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label> </td>
                </tr>
                <tr>
                <td align="right"><asp:Label ID="lblObjective" Text="" runat="server" Font-Bold="true"></asp:Label></td>
                <td><asp:DropDownList ID="ddlDependent" runat="server" 
                        OnSelectedIndexChanged="ddlDependent_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="select"></asp:ListItem>
                </asp:DropDownList> </td>
                </tr>
                <tr>
                <td align="right"><asp:Label ID="lblCostToday" Text="" runat="server" Font-Bold="true"></asp:Label></td>
                <td><asp:TextBox ID="txtCostToday" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td align="right"><asp:Label ID="lblRequiredAfter" Text="Required After:" runat="server" Font-Bold="true"></asp:Label></td>
                <td><asp:TextBox ID="txtRequiredAfter" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td align="right"><asp:Label ID="lblCurrentValue" Text="Current Investments Attached:" runat="server"  Font-Bold="true"></asp:Label></td>
                <td><asp:TextBox ID="txtCurrentValue" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td align="right"><asp:Label ID="lblRateInterest" Text="Rate of Interest Earned:" runat="server" Font-Bold="true"></asp:Label> </td>
                <td><asp:TextBox ID="txtRateInterest" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td align="right"><asp:Label ID="lblRequiredRate" Text="Required Rate of Return:" runat="server" Font-Bold="true"></asp:Label> </td>
                <td><asp:TextBox ID="txtRequiredRate" runat="server"></asp:TextBox></td>
                </tr>
                <tr><td colspan="2" align="right"><asp:Button ID="btnCalSubmit" Text="Submit" 
                        runat="server" OnClick="btnCalSubmit_Click" /></td></tr>
                </table>
                <table id="tblOutPut" runat="server" style="border-style:solid">
                 <tr>
                 <td colspan="2"><asp:Label ID="lblOutput" Text="OutPut" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label></td>
                 
                 </tr>
                <tr>
                <td align="right"><asp:Label ID="lblValue" Text="" runat="server" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lblValueResult" Text="" runat="server"></asp:Label></td>
                </tr>
                <tr>
                <td align="right"><asp:Label ID="lblCost" Text="" runat="server"  Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lblCostResult" Text="" runat="server"></asp:Label></td>
                </tr>
                <tr>
                <td align="right"><asp:Label ID="lblSavings" Text="Savings Required Per Month:" runat="server"  Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="lblSavingsResult" Text="" runat="server"></asp:Label></td>
                </tr>
                
                </table>
                </div></td>
                </tr>
                </table>
                </asp:WizardStep>
                <asp:WizardStep ID="wsAssetAllocation" runat="server" Title="Asset Allocation">
                </asp:WizardStep>
            </WizardSteps>
            <SideBarButtonStyle BackColor="#507CD1" Font-Names="Verdana" 
                ForeColor="White" />
            <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" 
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
                ForeColor="#284E98" />
            <SideBarStyle BackColor="#507CD1" Font-Size="0.9em" VerticalAlign="Top" />
            <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" 
                BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="White" 
                HorizontalAlign="Center" />
        </asp:Wizard>
        </td></tr>
        </table>
    </div>
    </form>
</body>
</html>
