    <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="WealthERP.General.Header" %>



    <style type="text/css">
        .style1
        {
            width: 201px;
        }
        .style2
        {
            width: 556px;
        }
    </style>
<table width="100%">
    <tr>
    <td colspan="3" valign="top">
    <table width="100%" style="height: 30px">
    <tr style="height:auto">
    <td class="style1" >
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logo.jpg" />
    </td>
        
    <td class="style2">
        
    </td>
    <td valign="bottom">
        <asp:LinkButton ID="LinkButtonSignIn" runat="server" 
            OnClientClick="loadcontrol('Userlogin');" 
            onclick="LinkButtonSignIn_Click1" >Sign In</asp:LinkButton>
&nbsp;
        <asp:LinkButton ID="LinkButtonContactUs" runat="server">Contact Us</asp:LinkButton>
&nbsp;
        <asp:LinkButton ID="LinkButtonHelp" runat="server">Help</asp:LinkButton>
    </td>

    </tr>
    </table>
    </td>
    </tr>
    <tr>
    <td colspan="3" bgcolor=Gainsboro height="5px">
    
    </td>
    </tr>
    <tr style="height:20px; border:border 2 #0000FF">
    <td colspan="3">
        <table width="100%">
            <tr>
                <td style="width:17%">
                </td>
                <td>
                <div id="menu_bar">
         <ul id="nav" class="dropdown dropdown-horizontal">
	    <li id="n-home"><a href="./">Home</a></li>
	    <li id="n-music"><a href="./" class="dir">Knowledge Centre</a>
		    <ul>
			    <li ><a href="Advisor.aspx" class="dir">Market Monitor</a>
				    <ul>
					    <li ><a href="./">Debt</a></li>
					    <li><a href="./">Equity</a></li>
					    <li><a href="./">Macro Economic</a></li>
					    <li><a href="./">International</a></li>
				    </ul>
			    </li>
			    <li><a href="./" class="dir">Advisor Speak</a>
				    <ul>
					    <li class="first"><a href="./">Hemant Rustagi</a></li>
					    <li><a href="./">Bhagavat</a></li>
					    <li><a href="./">Wangde</a></li>
    					
				    </ul>
			    </li>
			    <li><a href="./">Risk Managment</a></li>
			    <li><a href="./" class="dir">3rd party research reports</a>
				    <ul>
					    <li class="first"><a href="./">Equity</a></li>
					    <li><a href="./">MF</a></li>
					    <li class="first"><a href="./">Debt</a></li>
					    <li><a href="./">Markets</a></li>
				    </ul>
			    </li>
			    <li><a href="./">Risk Managment</a></li>
			    <li><span class="dir">PCG Ratings</span>
				    <ul>
					    <li class="first"><a href="./">MFs</a></li>
					    <li><a href="./">Discover &amp; Equities</a></li>
					    <li><a href="./">Banks</a></li>
    					
				    </ul>
			    </li>
			    <li><span class="dir">Legendary Investors</span></li>
			    <li><span class="dir">Investment Principles</span></li>
			    <li><a href="./">Quiz</a></li>
		    </ul>
	    </li>
	    <li ><a href="./" class="dir">Financial planning</a>
		    <ul>
			    <li class="first"><a href="./">Lite</a></li>
			    <li><a href="./">Basic</a></li>
			    <li><a href="./">Mid</a></li>
			    <li><a href="./" class="dir">Comprehensive</a>
			    <ul>
					    <li class="first"><a href="./">My Profile</a></li>
					    <li><a href="./">My Portfolio</a></li>
					    <li class="first"><a href="./">My Plan</a></li>
					    <li><a href="./">My Income-Expenses</a></li>
					    <li><a href="./">Reports</a></li>
				    </ul>
			    </li>
			    <li><a href="./">Alerts</a></li>
			    <li><a href="./">Task Planning</a></li>
			    <li><a href="./">Investment Policy</a></li>
		    </ul>
	    </li>
	    <li id="n-news"><a href="./" class="dir">Investment tools</a>
		    <ul>
			    <li ><a href="./" class="dir">Rating Models</a>
			        <ul>
					    <li><a href="./">Mutual Funds</a></li>
					    <li><a href="./">Equities</a></li>
					    <li><a href="./">Banks</a></li>
					    <li><a href="./">Properties</a></li>
				    </ul>
				    </li>
			    <li ><a href="./" class="dir">Model Insurance & Investment Portfolios </a>
			        <ul>
					    <li class="first"><a href="./">Questionnaire</a></li>
    					
    					
				    </ul>
			    </li>
			    <li class="first"><a href="./">Filter Models</a></li>
			    <li class="first"><a href="./">Portfolio Builder with back testing</a></li>
			    <li class="first"><a href="./">Equity/  MF Scheme Analysis</a></li>
			    <li class="first"><a href="./">Equity/MF  Data</a></li>
			    <li class="first"><a href="./">Insurance selector</a></li>
			    <li class="first"><a href="./">Calculators</a></li>
			    <li class="first"><a href="./">Risk Management</a></li>
		    </ul>
	    </li>
    	
    </ul>   
            
        </div>   
                </td>
            </tr>
        </table>
        </td>
    </tr>
    </table>