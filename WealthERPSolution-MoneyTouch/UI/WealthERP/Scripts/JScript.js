function GetXmlHttpObject() {
    var xmlHttp = null;
    try {
        // Firefox, Opera 8.0+, Safari
        xmlHttp = new XMLHttpRequest();
    }
    catch (e) {
        // Internet Explorer
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
    }
    return xmlHttp; 
}

function loadcontrol(controlid, logintrue) {

    if (controlid != null)
        parent.PageMethods.AjaxSetSession("Current_PageID", controlid);

    if (logintrue != "none" && logintrue != "login") {
        var c_src = "ControlHost.aspx" + logintrue + "&pageid=";
    }
    else {
        var c_src = "ControlHost.aspx?pageid=";
    }
    var url = c_src + controlid;    


    setTimeout('parent.document.getElementById("mainframe").src="' + url + '"', 25);
    //parent.document.getElementById("mainframe").src = url;

    if (controlid == "IFAAdminMainDashboard") {

        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "block";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";



        loadlinks("AdvisorLeftPane");

    }
    else if (controlid == "LoanScheme") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    }
    else if (controlid == "AdvisorRMBMDashBoard" && logintrue == "none") {

        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "block";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";

        loadlinks("AdvisorRMBMLeftpane");

    }

    else if (controlid == "AdvisorBMDashBoard" && logintrue == "none") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "block";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";

        loadlinks("AdvisorBMLeftpane");
    }

    else if (controlid == "PortfolioDashboard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "block";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";

        loadlinks("PortfolioLeftPane");
    }

    else if (controlid == "AdvisorRMDashBoard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "block";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";

        loadlinks("AdvisorRMLeftpane");

    }
    else if (controlid == "BMDashBoard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "block";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";


        loadlinks("BMLeftpane");

    }
    else if (controlid == "BMRMDashBoard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "block";
        parent.document.getElementById("AdminHeader").style.display = "none";

        loadlinks("BMRMLeftpane");
    }
    else if (controlid == "RMDashBoard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "block";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";

        //  parent.document.getElementById("AdvisorLogo").src = SourcePath;
        loadlinks("RMLeftPane");
    }
    else if (controlid == "CustomerIndividualDashboard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";


        loadlinks("CustomerIndividualLeftPane");
    }
    else if (controlid == "RMCustomerIndividualDashboard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "block";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";


        loadlinks("RMCustomerIndividualLeftPane");
    }
    else if (controlid == "CustomerNonIndividualDashboard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "block";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";


        loadlinks("CustomerNonIndividualLeftPane");
    }
    else if (controlid == "RMCustomerNonIndividualDashboard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "block";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";

        loadlinks("RMCustomerNonIndividualLeftPane");
    }
    else if (controlid == "AdvisorRMCustIndiDashboard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "block";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";

        loadlinks("RMCustomerIndividualLeftPane");
    }
    else if (controlid == "AdviserRMCustNonIndiDashboard") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "block";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";

        loadlinks("RMCustomerNonIndividualLeftPane");
    }
    else if (controlid == "RMAlertNotifications" && logintrue == "none") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "block";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";

        loadlinks("AlertsLeftPane")
    }

    else if (controlid == "AdminUpload") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "block";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";

        loadlinks("LeftPanel_Links");

    }
    else if (controlid == "SessionExpired") {
        parent.document.getElementById("GeneralMenu").style.display = "block";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";

        loadlinks("LeftPanel_Links");

    }

    else if (controlid == "Userlogin") {
        setHeaderLinksFromControl("", "", "Sign In", "false");
    }

    else if (controlid == "UserSettings") {
        loadlinks("UserSettingsLeftPane");
    }
}
function loadsearchcontrol(controlid, searchtype, searchstring) {

    if (controlid != null)
        parent.PageMethods.AjaxSetSession("Current_PageID", controlid);

    if (searchtype == "Branch" && searchstring != "") {
        var c_src = "ControlHost.aspx?Branch=" + searchstring + "&pageid=";
    }
    else if (searchtype == "RM" && searchstring != "") {
        var c_src = "ControlHost.aspx?RM=" + searchstring + "&pageid=";

    }
    else if (searchtype == "AdviserCustomer" && searchstring != "") {
        var c_src = "ControlHost.aspx?Customer=" + searchstring + "&pageid=";
    }
    else if (searchtype == "Customer" && searchstring != "") {

        var c_src = "ControlHost.aspx?Customer=" + searchstring + "&pageid=";
    }
    else {
        var c_src = "ControlHost.aspx?pageid=";


    }
    var url = c_src + controlid;


    setTimeout('parent.document.getElementById("mainframe").src="' + url + '"', 25);
    //parent.document.getElementById("mainframe").src = url;
}



function loginloadcontrol(controlid, logintrue, UserName, SourcePath, BranchLogoSourcePath) {
    if (controlid != null)
        parent.PageMethods.AjaxSetSession("Current_PageID", controlid);

    var c_src = "ControlHost.aspx?pageid=";
    var url = c_src + controlid;

    // Check for Browser Type
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ")

    if (msie > 0)      // If Internet Explorer
    {
        if (parent.document.readyState == "complete") {
            setTimeout('parent.document.getElementById("mainframe").src="' + url + '"', 25);
            parent.document.getElementById("mainframe").src = url;
        }
        if (controlid == "IFF") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            loadlinks("SuperAdminLeftPane");
        }
        else if (controlid == "MFAdminDashboard") {
            loadlinks("LOBMFAdminLeftPane");
        }

        else if (controlid == "AdvisorDashBoard") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "block";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                //parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }

            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("AdvisorLeftPane");

        }
        else if (controlid == "AdvisorBMDashBoard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "block";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                //parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("AdvisorBMLeftpane");
        }
        else if (controlid == "AdvisorRMDashBoard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }

            loadlinks("AdvisorRMLeftpane");
        }
        else if (controlid == "AdvisorRMBMDashBoard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }

            loadlinks("AdvisorRMBMLeftpane");
        }
        else if (controlid == "RMDashBoard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "block";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                //  parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            if (BranchLogoSourcePath != "") {
                // parent.document.getElementById("BranchLogo").src = BranchLogoSourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("RMLeftPane");
        }
        else if (controlid == "BMRMDashBoard") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "block";
            if (SourcePath != "") {
                //  parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            if (BranchLogoSourcePath != "") {
                // parent.document.getElementById("BranchLogo").src = BranchLogoSourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("BMRMLeftpane");
        }
        else if (controlid == "CustomerIndividualDashboard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("CustomerIndividualLeftPane");
        }
        else if (controlid == "AdvisorRMCustIndiDashboard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                //parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("CustomerIndividualLeftPane");
        }
        else if (controlid == "RMCustomerIndividualDashboard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("RMCustomerIndividualLeftPane");
        }
        //    else if (controlid == "AdvisorRMCustIndiDashboard" && logintrue == "login") {
        //        parent.document.getElementById("GeneralMenu").style.display = "none";
        //        parent.document.getElementById("AdvisorHeader").style.display = "none";        
        //        parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
        //        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        //        parent.document.getElementById("RMHeader").style.display = "none";
        //        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        //        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        //        parent.document.getElementById("BMHeader").style.display = "none";
        //        parent.document.getElementById("AdminHeader").style.display = "none";
        //        if (SourcePath != "") {
        //            // parent.document.getElementById("AdvisorLogo").src = SourcePath;
        //        }
        //        parent.document.getElementById("SwitchRolesHeader").style.display = "none";

        //        loadlinks("CustomerIndividualLeftPane");
        //    }
        else if (controlid == "CustomerNonIndividualDashboard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "block";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("CustomerNonIndividualLeftPane");

        }
        else if (controlid == "RMCustomerNonIndividualDashboard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "block";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                //parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("RMCustomerNonIndividualLeftPane");
        }
        else if (controlid == "GeneralHome" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "block";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            loadlinks("LeftPanel_Links");
        }
        else if (controlid == "BMDashBoard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "block";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            if (BranchLogoSourcePath != "") {
                // parent.document.getElementById("BranchLogo").src = BranchLogoSourcePath;    
            }

            loadlinks("BMLeftpane");
        }
        else if (controlid == "AdminUpload") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "block";
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            loadlinks("LeftPanel_Links");

        }
    }
    else                // If another browser
    {
        setTimeout('parent.document.getElementById("mainframe").src="' + url + '"', 25);
        if (controlid == "IFF") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            loadlinks("SuperAdminLeftPane");
        }
        else if (controlid == "MFAdminDashboard") {
            loadlinks("LOBMFAdminLeftPane");
        }

        else if (controlid == "AdvisorDashBoard") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "block";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                //parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }

            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("AdvisorLeftPane");

        }
        else if (controlid == "AdvisorBMDashBoard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "block";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                //parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("AdvisorBMLeftpane");
        }
        else if (controlid == "AdvisorRMDashBoard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }

            loadlinks("AdvisorRMLeftpane");
        }
        else if (controlid == "AdvisorRMBMDashBoard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }

            loadlinks("AdvisorRMBMLeftpane");
        }
        else if (controlid == "RMDashBoard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "block";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                //  parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            if (BranchLogoSourcePath != "") {
                // parent.document.getElementById("BranchLogo").src = BranchLogoSourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("RMLeftPane");
        }
        else if (controlid == "BMRMDashBoard") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "block";
            if (SourcePath != "") {
                //  parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            if (BranchLogoSourcePath != "") {
                // parent.document.getElementById("BranchLogo").src = BranchLogoSourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("BMRMLeftpane");
        }
        else if (controlid == "CustomerIndividualDashboard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("CustomerIndividualLeftPane");
        }
        else if (controlid == "AdvisorRMCustIndiDashboard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                //parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("CustomerIndividualLeftPane");
        }
        else if (controlid == "RMCustomerIndividualDashboard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("RMCustomerIndividualLeftPane");
        }
        //    else if (controlid == "AdvisorRMCustIndiDashboard" && logintrue == "login") {
        //        parent.document.getElementById("GeneralMenu").style.display = "none";
        //        parent.document.getElementById("AdvisorHeader").style.display = "none";        
        //        parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
        //        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        //        parent.document.getElementById("RMHeader").style.display = "none";
        //        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        //        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        //        parent.document.getElementById("BMHeader").style.display = "none";
        //        parent.document.getElementById("AdminHeader").style.display = "none";
        //        if (SourcePath != "") {
        //            // parent.document.getElementById("AdvisorLogo").src = SourcePath;
        //        }
        //        parent.document.getElementById("SwitchRolesHeader").style.display = "none";

        //        loadlinks("CustomerIndividualLeftPane");
        //    }
        else if (controlid == "CustomerNonIndividualDashboard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "block";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("CustomerNonIndividualLeftPane");

        }
        else if (controlid == "RMCustomerNonIndividualDashboard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "block";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            if (SourcePath != "") {
                //parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinks("RMCustomerNonIndividualLeftPane");
        }
        else if (controlid == "GeneralHome" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "block";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            loadlinks("LeftPanel_Links");
        }
        else if (controlid == "BMDashBoard" && logintrue == "login") {
            parent.document.getElementById("GeneralMenu").style.display = "block";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "none";
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            if (BranchLogoSourcePath != "") {
                // parent.document.getElementById("BranchLogo").src = BranchLogoSourcePath;    
            }

            loadlinks("BMLeftpane");
        }
        else if (controlid == "AdminUpload") {
            parent.document.getElementById("GeneralMenu").style.display = "none";
            parent.document.getElementById("AdvisorHeader").style.display = "none";
            parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
            parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            parent.document.getElementById("RMHeader").style.display = "none";
            parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            parent.document.getElementById("BMHeader").style.display = "none";
            parent.document.getElementById("AdminHeader").style.display = "block";
            parent.document.getElementById("SwitchRolesHeader").style.display = "none";
            if (SourcePath != "") {
                // parent.document.getElementById("AdvisorLogo").src = SourcePath;
            }
            loadlinks("LeftPanel_Links");

        }
        //parent.document.getElementById("mainframe").src = url;
    }
    
    setHeaderLinksFromControl(UserName, "Sign Out", "", "false");
}

function loadlinks(controlid) {
    
    if (controlid != null)
        parent.PageMethods.AjaxSetLinksSession("Current_LinkID", controlid);

    var c_src = "ControlLeftHost.aspx?pageid=";
    var url = c_src + controlid;
    

    // Check for Browser Type
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ")

    if (msie > 0)      // If Internet Explorer
    {
//        if (parent.document.readyState == "complete") {
//            parent.document.getElementById("leftframe").src = url;
        //        }
        setTimeout('parent.document.getElementById("leftframe").src="' + url + '"', 25);
    }
    else                // If another browser
    {
        setTimeout('parent.document.getElementById("leftframe").src="' + url + '"', 25);
        //parent.document.getElementById("leftframe").src = url;
    }

}


function loadfrommenu(controlid, logintrue) {//, PageForm, MenuControlName
    
    if (controlid != null)
        parent.PageMethods.AjaxSetSession("Current_PageID", controlid);
    if (logintrue != "none" && logintrue != "login") {
        var c_src = "ControlHost.aspx" + logintrue + "&pageid=";
    }
    else {
        var c_src = "ControlHost.aspx?pageid=";
    }
    var url = c_src + controlid;

    //var c_src = "ControlHost.aspx?pageid=";
    //var url = c_src + controlid;

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ")

    if (msie > 0)      // If Internet Explorer
    {
        if (document.readyState == "complete") {

            document.getElementById("mainframe").src = url;

            if (controlid == "IFAAdminMainDashboard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "block";
                document.getElementById("SwitchRolesHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";

                loadlinksfromDefault("AdvisorLeftPane");
            }
            else if (controlid == "AdvisorProfile" && logintrue == "login") {
                loadlinksfromDefault("AdvisorLeftPane");
            }
            else if (controlid == "ViewLOB" && logintrue == "login") {
                loadlinksfromDefault("AdvisorLeftPane");
            }
            else if (controlid == "ViewBranches" && logintrue == "login") {
                loadlinksfromDefault("AdvisorLeftPane");
            }
            /*else if (controlid == "ViewRM" && logintrue == "login") {
            loadlinksfromDefault("AdvisorLeftPane");
            }*/
            else if (controlid == "RMAlertNotifications") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "block";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";
                document.getElementById("SwitchRolesHeader").style.display = "none";

                loadlinksfromDefault("AlertsLeftPane");
            }
            /**/
            else if (controlid == "AdvisorRMBMDashBoard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("SwitchRolesHeader").style.display = "block";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";

                loadlinksfromDefault("AdvisorRMBMLeftpane");
            }
            else if (controlid == "AdvisorBMDashBoard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "block";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";

                loadlinksfromDefault("AdvisorBMLeftpane");
            }
            else if (controlid == "AdvisorRMDashBoard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "block";
                document.getElementById("SwitchRolesHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";

                loadlinksfromDefault("AdvisorRMLeftpane");
            }
            else if (controlid == "BMRMDashBoard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "block";
                document.getElementById("AdminHeader").style.display = "none";

                loadlinksfromDefault("BMRMLeftpane");
            }
            else if (controlid == "BMDashBoard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "block";
                document.getElementById("AdminHeader").style.display = "none";
                document.getElementById("SwitchRolesHeader").style.display = "none";

                loadlinksfromDefault("BMLeftpane");
            }
            else if (controlid == "RMDashBoard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "block";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";
                document.getElementById("SwitchRolesHeader").style.display = "none";

                loadlinksfromDefault("RMLeftPane");
            }
            else if (controlid == "PortfolioDashboard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("SwitchRolesHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "block";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";

                loadlinksfromDefault("PortfolioLeftPane");
            }
            else if (controlid == "CustomerIndividualDashboard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "block";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";
                document.getElementById("SwitchRolesHeader").style.display = "none";


                loadlinksfromDefault("CustomerIndividualLeftPane");
            }
            else if (controlid == "RMCustomerIndividualDashboard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "block";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";
                document.getElementById("SwitchRolesHeader").style.display = "none";

                loadlinksfromDefault("RMCustomerIndividualLeftPane");
            }
            else if (controlid == "AdvisorRMCustIndiDashboard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "block";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";
                document.getElementById("SwitchRolesHeader").style.display = "none";

                loadlinksfromDefault("RMCustomerIndividualLeftPane");
            }
            else if (controlid == "CustomerNonIndividualDashboard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "block";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";
                document.getElementById("SwitchRolesHeader").style.display = "none";

                loadlinksfromDefault("CustomerNonIndividualLeftPane");
            }
            else if (controlid == "RMCustomerNonIndividualDashboard" && logintrue == "login") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "block";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "none";
                document.getElementById("SwitchRolesHeader").style.display = "none";

                loadlinksfromDefault("RMCustomerNonIndividualLeftPane");
            }
            else if (controlid == "ViewRMDetails" && logintrue == "login") {
                loadlinksfromDefault("RMLeftPane");
            }
            else if (controlid == "RMCustomer" && logintrue == "login") {
                loadlinksfromDefault("RMLeftPane");
            }
            else if (controlid == "AdminUpload") {

                document.getElementById("GeneralMenu").style.display = "none";
                document.getElementById("AdvisorHeader").style.display = "none";
                document.getElementById("CustomerIndividualHeader").style.display = "none";
                document.getElementById("CustomerNonIndividualHeader").style.display = "none";
                document.getElementById("RMHeader").style.display = "none";
                document.getElementById("RMCLientHeaderIndividual").style.display = "none";
                document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
                document.getElementById("BMHeader").style.display = "none";
                document.getElementById("AdminHeader").style.display = "block";
                document.getElementById("SwitchRolesHeader").style.display = "none";

                loadlinksfromDefault("LeftPanel_Links");

            }
            else if (controlid == "FinancialPlanning" && logintrue == "login") {

                loadlinks("FinancialPlanningLeftPane");

                document.getElementById("mainframe").src = url;
            }

        }
    }
    else                // If another browser
    {
        setTimeout('parent.document.getElementById("mainframe").src="' + url + '"', 25);
        //parent.document.getElementById("mainframe").src = url;

        if (controlid == "IFAAdminMainDashboard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "block";
            document.getElementById("SwitchRolesHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";

            loadlinksfromDefault("AdvisorLeftPane");
        }
        else if (controlid == "AdvisorProfile" && logintrue == "login") {
            loadlinksfromDefault("AdvisorLeftPane");
        }
        else if (controlid == "FinancialPlanning" && logintrue == "login") {
            
        document.getElementById("mainframe").src = url;        
        loadlinksfromDefault("FinancialPlanningLeftPane");
        
        }
        else if (controlid == "ViewLOB" && logintrue == "login") {
            loadlinksfromDefault("AdvisorLeftPane");
        }
        else if (controlid == "ViewBranches" && logintrue == "login") {
            loadlinksfromDefault("AdvisorLeftPane");
        }
        /*else if (controlid == "ViewRM" && logintrue == "login") {
        loadlinksfromDefault("AdvisorLeftPane");
        }*/
        else if (controlid == "RMAlertNotifications") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "block";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";
            document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinksfromDefault("AlertsLeftPane");
        }
        /**/
        else if (controlid == "AdvisorRMBMDashBoard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("SwitchRolesHeader").style.display = "block";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";

            loadlinksfromDefault("AdvisorRMBMLeftpane");
        }
        else if (controlid == "AdvisorBMDashBoard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "block";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";

            loadlinksfromDefault("AdvisorBMLeftpane");
        }
        else if (controlid == "AdvisorRMDashBoard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "block";
            document.getElementById("SwitchRolesHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";

            loadlinksfromDefault("AdvisorRMLeftpane");
        }
        else if (controlid == "BMRMDashBoard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "block";
            document.getElementById("AdminHeader").style.display = "none";

            loadlinksfromDefault("BMRMLeftpane");
        }
        else if (controlid == "BMDashBoard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "block";
            document.getElementById("AdminHeader").style.display = "none";
            document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinksfromDefault("BMLeftpane");
        }
        else if (controlid == "RMDashBoard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "block";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";
            document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinksfromDefault("RMLeftPane");
        }
        else if (controlid == "PortfolioDashboard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("SwitchRolesHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "block";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";

            loadlinksfromDefault("PortfolioLeftPane");
        }
        else if (controlid == "CustomerIndividualDashboard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "block";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";
            document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinksfromDefault("CustomerIndividualLeftPane");
        }
        else if (controlid == "RMCustomerIndividualDashboard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "block";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";
            document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinksfromDefault("RMCustomerIndividualLeftPane");
        }
        else if (controlid == "AdvisorRMCustIndiDashboard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "block";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";
            document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinksfromDefault("RMCustomerIndividualLeftPane");
        }
        else if (controlid == "CustomerNonIndividualDashboard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "block";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";
            document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinksfromDefault("CustomerNonIndividualLeftPane");
        }
        else if (controlid == "RMCustomerNonIndividualDashboard" && logintrue == "login") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "block";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "none";
            document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinksfromDefault("RMCustomerNonIndividualLeftPane");
        }
        else if (controlid == "ViewRMDetails" && logintrue == "login") {
            loadlinksfromDefault("RMLeftPane");
        }
        else if (controlid == "RMCustomer" && logintrue == "login") {
            loadlinksfromDefault("RMLeftPane");
        }
        else if (controlid == "AdminUpload") {

            document.getElementById("GeneralMenu").style.display = "none";
            document.getElementById("AdvisorHeader").style.display = "none";
            document.getElementById("CustomerIndividualHeader").style.display = "none";
            document.getElementById("CustomerNonIndividualHeader").style.display = "none";
            document.getElementById("RMHeader").style.display = "none";
            document.getElementById("RMCLientHeaderIndividual").style.display = "none";
            document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
            document.getElementById("BMHeader").style.display = "none";
            document.getElementById("AdminHeader").style.display = "block";
            document.getElementById("SwitchRolesHeader").style.display = "none";

            loadlinksfromDefault("LeftPanel_Links");

        }
    }
}

function logoutloadcontrol(controlid, logoPath, branchLogoPath) {
    debugger;

    if (controlid != null)
        PageMethods.AjaxSetSession("Current_PageID", controlid);

    var c_src = "ControlHost.aspx?pageid=";
    var url = c_src + controlid;

    setTimeout('document.getElementById("mainframe").src="' + url + '"', 25);
    //parent.document.getElementById("mainframe").src = url;

    setHeaderLinksFromControl("", "", "Sign In", "true");

    document.getElementById("GeneralMenu").style.display = "block";
    document.getElementById("AdvisorHeader").style.display = "none";
    document.getElementById("CustomerIndividualHeader").style.display = "none";
    document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    document.getElementById("RMHeader").style.display = "none";
    document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    document.getElementById("BMHeader").style.display = "none";
    document.getElementById("AdminHeader").style.display = "none";
    document.getElementById("SwitchRolesHeader").style.display = "none";

//    document.getElementById("AdvisorLogo").src = logoPath;
//    document.getElementById("BranchLogo").src = branchLogoPath;
    loadlinksfromDefault("LeftPanel_Links");

}

function loginloadcontrolfromDefault(controlid, logintrue, UserName) {
    
    if (controlid != null) {
        PageMethods.AjaxSetSession("Current_PageID", controlid);
    }

    var c_src = "ControlHost.aspx?pageid=";
    var url = c_src + controlid;

    setTimeout('document.getElementById("mainframe").src="' + url + '"', 25);
    //document.getElementById("mainframe").src = url;

    if (controlid == "AdvisorDashBoard") {
        document.getElementById("GeneralMenu").style.display = "none";
        document.getElementById("AdvisorHeader").style.display = "block";
        document.getElementById("CustomerIndividualHeader").style.display = "none";
        document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        document.getElementById("RMHeader").style.display = "none";
        document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        document.getElementById("BMHeader").style.display = "none";
        document.getElementById("AdminHeader").style.display = "none";
        loadlinksfromDefault("AdvisorLeftPane");
    }
    else if (controlid == "RMDashBoard" && logintrue == "login") {
        document.getElementById("GeneralMenu").style.display = "none";
        document.getElementById("AdvisorHeader").style.display = "none";
        document.getElementById("CustomerIndividualHeader").style.display = "none";
        document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        document.getElementById("RMHeader").style.display = "block";
        document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        document.getElementById("BMHeader").style.display = "none";
        document.getElementById("AdminHeader").style.display = "none";
        loadlinksfromDefault("RMLeftPane");
    }
    else if (controlid == "CustomerIndividualDashboard" && logintrue == "login") {
        document.getElementById("GeneralMenu").style.display = "none";
        document.getElementById("AdvisorHeader").style.display = "none";
        document.getElementById("CustomerIndividualHeader").style.display = "block";
        document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        document.getElementById("RMHeader").style.display = "none";
        document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        document.getElementById("BMHeader").style.display = "none";
        document.getElementById("AdminHeader").style.display = "none";
        loadlinksfromDefault("CustomerIndividualLeftPane");
    }
    else if (controlid == "RMCustomerIndividualDashboard" && logintrue == "login") {
        document.getElementById("GeneralMenu").style.display = "none";
        document.getElementById("AdvisorHeader").style.display = "none";
        document.getElementById("CustomerIndividualHeader").style.display = "block";
        document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        document.getElementById("RMHeader").style.display = "none";
        document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        document.getElementById("BMHeader").style.display = "none";
        document.getElementById("AdminHeader").style.display = "none";
        loadlinksfromDefault("RMCustomerIndividualLeftPane");
    }
    else if (controlid == "CustomerNonIndividualDashboard" && logintrue == "login") {
        document.getElementById("GeneralMenu").style.display = "none";
        document.getElementById("AdvisorHeader").style.display = "none";
        document.getElementById("CustomerIndividualHeader").style.display = "none";
        document.getElementById("CustomerNonIndividualHeader").style.display = "block";
        document.getElementById("RMHeader").style.display = "none";
        document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        document.getElementById("BMHeader").style.display = "none";
        document.getElementById("AdminHeader").style.display = "none";
        loadlinksfromDefault("CustomerNonIndividualLeftPane");
    }
    else if (controlid == "RMCustomerNonIndividualDashboard" && logintrue == "login") {
        document.getElementById("GeneralMenu").style.display = "none";
        document.getElementById("AdvisorHeader").style.display = "none";
        document.getElementById("CustomerIndividualHeader").style.display = "none";
        document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        document.getElementById("RMHeader").style.display = "none";
        document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        document.getElementById("RMCLientHeaderNonIndividual").style.display = "block";
        document.getElementById("BMHeader").style.display = "none";
        document.getElementById("AdminHeader").style.display = "none";
        loadlinksfromDefault("RMCustomerNonIndividualLeftPane");
    }
    else if (controlid == "GeneralHome" && logintrue == "login") {
        document.getElementById("GeneralMenu").style.display = "block";
        document.getElementById("AdvisorHeader").style.display = "none";
        document.getElementById("CustomerIndividualHeader").style.display = "none";
        document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        document.getElementById("RMHeader").style.display = "none";
        document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        document.getElementById("BMHeader").style.display = "none";
        document.getElementById("AdminHeader").style.display = "none";
        loadlinksfromDefault("LeftPanel_Links");
    }

    else if (controlid == "AdminUpload" && logintrue == "login") {
        document.getElementById("GeneralMenu").style.display = "none";
        document.getElementById("AdvisorHeader").style.display = "none";
        document.getElementById("CustomerIndividualHeader").style.display = "none";
        document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        document.getElementById("RMHeader").style.display = "none";
        document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        document.getElementById("BMHeader").style.display = "none";
        document.getElementById("AdminHeader").style.display = "block";
        loadlinksfromDefault("LeftPanel_Links");
    }

    if (logintrue == "login") {
        setHeaderLinksFromControl(UserName, "Sign Out", "", "true");
    }
    else {
        setHeaderLinksFromControl(UserName, "", "Sign In", "true");
        loadlinksfromDefault("LeftPanel_Links");
    }
}

function loadlinksfromDefault(controlid) {
    
    if (controlid != null)
        PageMethods.AjaxSetLinksSession("Current_LinkID", controlid);
    
    var c_src = "ControlLeftHost.aspx?pageid=";
    var url = c_src + controlid;
    
    setTimeout('document.getElementById("leftframe").src="' + url + '"', 25);
    //document.getElementById("leftframe").src = url;

}

function setHeaderLinksFromControl(username, signOutText, signInText, IsParent) {
    

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ")

    if (IsParent == "false") {
        if (signInText != "") {

            //parent.document.getElementById("lnkHelp").style.display = 'none';
            parent.document.getElementById("LinkButtonUserSettings").style.display = 'none';

        }
        else {

            //parent.document.getElementById("lnkHelp").style.display = 'inline';
            parent.document.getElementById("LinkButtonUserSettings").style.display = 'inline';
        }
        if (msie > 0)      // If Internet Explorer
        {

            parent.document.getElementById("lblUserName").innerText = username;
            parent.document.getElementById("lblSignOut").innerText = signOutText;
            parent.document.getElementById("LinkButtonSignIn").innerText = signInText;
        }
        else                 // If another browser
        {
            parent.document.getElementById("lblUserName").textContent = username;
            parent.document.getElementById("lblSignOut").textContent = signOutText;
            parent.document.getElementById("LinkButtonSignIn").textContent = signInText;
        }
    }
    else if (IsParent == "true") {
        if (signInText != "") {

           // document.getElementById("lnkHelp").style.display = 'none';
            document.getElementById("LinkButtonUserSettings").style.display = 'none';
        }
        else {
            //document.getElementById("lnkHelp").style.visible = 'inline';
            document.getElementById("LinkButtonUserSettings").style.visible = 'inline';
        }
        if (msie > 0)      // If Internet Explorer
        {
            document.getElementById("lblUserName").innerText = username;
            document.getElementById("lblSignOut").innerText = signOutText;
            document.getElementById("LinkButtonSignIn").innerText = signInText;
        }
        else                // If another browser
        {
            setTimeout('parent.document.getElementById("lblUserName").textContent ="' + username + '"', 25);
            //parent.document.getElementById("lblUserName").textContent = username;
            setTimeout('parent.document.getElementById("lblSignOut").textContent ="' + signOutText + '"', 25);
           // parent.document.getElementById("lblSignOut").textContent = signOutText;
            setTimeout('parent.document.getElementById("LinkButtonSignIn").textContent ="' + signInText + '"', 25);
            //parent.document.getElementById("LinkButtonSignIn").textContent = signInText;

        }
    }   
}

function ChangeButtonCss(str, submitButton, size) {
    var action = str;
    var ButtonClientID = submitButton;
    if (action == 'hover') {
        if (size == 'S') {
            document.getElementById(submitButton).className = "PCGHoverButton";
        }
        else if (size == 'M') {
            document.getElementById(submitButton).className = "PCGHoverMediumButton";
        }
        else if (size == 'L') {
            document.getElementById(submitButton).className = "PCGHoverLongButton";
        }
    }
    else {
        if (size == 'S') {
            document.getElementById(submitButton).className = "PCGButton";
        }
        else if (size == 'M') {
            document.getElementById(submitButton).className = "PCGMediumButton";
        }
        else if (size == 'L') {
            document.getElementById(submitButton).className = "PCGLongButton";
        }

    }
}

function JSdoPostback(e, btnID) {


    var unicode = e.charCode ? e.charCode : e.keyCode;

    if (unicode == 13) {


        var btn2 = document.getElementById(btnID);
        btn2.click();

        return false;
    }
    else {
        return true;
    }
}

function loadcontrolCustomer(controlid, logintrue) {
   
    if (controlid != null)
        parent.PageMethods.AjaxSetSession("Current_PageID", controlid);

    if (logintrue != "none" && logintrue != "login") {
        var c_src = "ControlHost.aspx" + logintrue + "&pageid=";
    }
    else {
        var c_src = "ControlHost.aspx?pageid=";
    }
    var url = c_src + controlid;


    setTimeout('parent.document.getElementById("mainframe").src="' + url + '"', 25);
    //parent.document.getElementById("mainframe").src = url;
    if (controlid == "LoanSchemeView") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    }
    else {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "none";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    }
    //loadlinks("CustomerIndividualLeftPane");

    if (controlid == "Userlogin") {
        setHeaderLinksFromControl("", "", "Sign In", "false");
    }

    else if (controlid == "UserSettings") {
        loadlinks("UserSettingsLeftPane");
    }
    
}

function loadtopmenu(menutype) {   
    if (menutype == "AdvisorLeftPane") {
        parent.document.getElementById("GeneralMenu").style.display = "none";
        parent.document.getElementById("AdvisorHeader").style.display = "block";
        parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
        parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
        parent.document.getElementById("RMHeader").style.display = "none";
        parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
        parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
        parent.document.getElementById("BMHeader").style.display = "none";
        parent.document.getElementById("AdminHeader").style.display = "none";
        parent.document.getElementById("SwitchRolesHeader").style.display = "none";
        parent.document.getElementById("SuperAdminHeader").style.display = "none";

    }
    else if (menutype == "AdvisorBMLeftpane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "AdvisorRMBMLeftpane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "AdvisorRMLeftpane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "AlertsLeftPane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "BMLeftpane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "block";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "BMRMLeftpane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "CustomerIndividualLeftPane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "block";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "CustomerNonIndividualLeftPane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "block";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "RMCustomerIndividualLeftPane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "block";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "RMCustomerNonIndividualLeftPane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "block";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "RMLeftPane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "block";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "none";

}
else if (menutype == "SuperAdminLeftPane") {
    parent.document.getElementById("GeneralMenu").style.display = "none";
    parent.document.getElementById("AdvisorHeader").style.display = "none";
    parent.document.getElementById("CustomerIndividualHeader").style.display = "none";
    parent.document.getElementById("CustomerNonIndividualHeader").style.display = "none";
    parent.document.getElementById("RMHeader").style.display = "none";
    parent.document.getElementById("RMCLientHeaderIndividual").style.display = "none";
    parent.document.getElementById("RMCLientHeaderNonIndividual").style.display = "none";
    parent.document.getElementById("BMHeader").style.display = "none";
    parent.document.getElementById("AdminHeader").style.display = "none";
    parent.document.getElementById("SwitchRolesHeader").style.display = "none";
    parent.document.getElementById("SuperAdminHeader").style.display = "block";

}
}