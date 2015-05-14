<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFSchemeRelateInformation.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFSchemeRelateInformation" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>

<script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>

<asp:ScriptManager ID="scriptmanager" runat="server">
    <%--<Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>--%>
</asp:ScriptManager>
<style type="text/css">
    .h6
    {
        font-stretch: narrower;
    }
    .dvRow
    {
        font-family:Segoe UI; font-size:14px; color:#565656; padding-bottom:1%
    }
    .style1
    {
        width: 37%;
    }
    .leftField
    {
        font-weight: bold;
    }
    .DivBody
    {
        background: #ffffff; /* Old browsers */
        background: -moz-linear-gradient(top,  #ffffff 0%, #e5e5e5 100%); /* FF3.6+ */
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#ffffff), color-stop(100%,#e5e5e5)); /* Chrome,Safari4+ */
        background: -webkit-linear-gradient(top,  #ffffff 0%,#e5e5e5 100%); /* Chrome10+,Safari5.1+ */
        background: -o-linear-gradient(top,  #ffffff 0%,#e5e5e5 100%); /* Opera 11.10+ */
        background: -ms-linear-gradient(top,  #ffffff 0%,#e5e5e5 100%); /* IE10+ */
        background: linear-gradient(to bottom,  #ffffff 0%,#e5e5e5 100%); /* W3C */
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#ffffff', endColorstr='#e5e5e5',GradientType=0 ); /* IE6-9 */
        box-shadow: 10px 10px 5px grey;
    }
    .divRow
    {
        background: #ffffff;
        background: -moz-linear-gradient(top,  #ffffff 0%, #f6f6f6 47%, #ededed 100%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#ffffff), color-stop(47%,#f6f6f6), color-stop(100%,#ededed));
        background: -webkit-linear-gradient(top,  #ffffff 0%,#f6f6f6 47%,#ededed 100%);
        background: -o-linear-gradient(top,  #ffffff 0%,#f6f6f6 47%,#ededed 100%);
        background: -ms-linear-gradient(top,  #ffffff 0%,#f6f6f6 47%,#ededed 100%);
        background: linear-gradient(to bottom,  #ffffff 0%,#f6f6f6 47%,#ededed 100%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#ffffff', endColorstr='#ededed',GradientType=0 );
    }
    .header
    {
        background: #93cede;
        background: -moz-linear-gradient(top,  #93cede 0%, #75bdd1 41%, #49a5bf 100%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#93cede), color-stop(41%,#75bdd1), color-stop(100%,#49a5bf));
        background: -webkit-linear-gradient(top,  #93cede 0%,#75bdd1 41%,#49a5bf 100%);
        background: -o-linear-gradient(top,  #93cede 0%,#75bdd1 41%,#49a5bf 100%);
        background: -ms-linear-gradient(top,  #93cede 0%,#75bdd1 41%,#49a5bf 100%);
        background: linear-gradient(to bottom,  #93cede 0%,#75bdd1 41%,#49a5bf 100%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#93cede', endColorstr='#49a5bf',GradientType=0 );
    }
</style>

<script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>


<script src="../Scripts/jquery.min.js" type="text/javascript"></script>



<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css"
    type="text/css" />

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>

<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"
    type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<%--<script type="text/javascript">
    $(document).ready(function() {
        GetDropDownData();
        GetSchemeLists();
    });

    function GetDropDownData() {
        // Get the DropDownList.
        var ddlAMC = $('#ddlAMC');
        var tableName = "AMCList";
        $.ajax({
            type: "POST",
            url: "../CustomerPortfolio/AutoComplete.asmx/GetAMCList",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(response) {
                var Dropdown = $('#<%=ddlAMC.ClientID %>');
                Dropdown.append(new Option("All", 0));
                $.each(response.d, function(index, item) {
                    Dropdown.append(new Option(item.Text, item.Value));
                });
            }
        });
    }
    $(document).ready(function() {
        GetSchemeLists();
    });
    function GetSchemeLists() {
        // Get the DropDownList.
        var ddlScheme = $('#ddlScheme');
        var tableName = "SchemeList";
        $.ajax({
            type: "POST",
            url: "../CustomerPortfolio/AutoComplete.asmx/GetSchemeList",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(response) {
                var Dropdown1 = $('#<%=ddlScheme.ClientID %>');
                Dropdown1.append(new Option("All", 0));
                $.each(response.d, function(index, item) {
                    Dropdown1.append(new Option(item.Text, item.Value));
                });
            }
        });
    }
    function AMCSelection() {
        // Get the DropDownList.
        var ddlScheme = $('#ddlScheme');
        var tableName = "SchemeList";
        var IndexValuebond = document.getElementById('<%=ddlAMC.ClientID %>');
        var SelectedValbond = IndexValuebond.value;

        var AmcCodes = {
            SelectedValbond: SelectedValbond
        }
        $.ajax({
            type: "POST",
            url: "../CustomerPortfolio/AutoComplete.asmx/GetAMCWiseSchemeList",
            data: JSON.stringify(AmcCodes),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(response) {
                var Dropdown1 = $('#<%=ddlScheme.ClientID %>');
                Dropdown1.append(new Option("All", 0));
                $.each(response.d, function(index, item) {
                    Dropdown1.append(new Option(item.Text, item.Value));
                });
            }
        });

    }
</script>--%>

<script type="text/javascript">
    //        $(document).ready(function() {
    //        GetSchemeLists();
    //        });
       
</script>

 <table class="tblMessage" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="divOnlinePageHeading" style="height:25px;">
                        <div class="divClientAccountBalance" id="divClientAccountBalance" runat="server">
                            <asp:Label ID="Label1" runat="server" Text="Available Limits:" CssClass="BalanceLabel"> </asp:Label>
                            <asp:Label ID="lblAvailableLimits" runat="server" Text="" CssClass="BalanceAmount"></asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
  </table>

<div class="row " style="margin-left: 10%; margin-top: 1%; margin-bottom: 2%; padding-top: 1%;">
    <div class="col-md-3">
        <button type="button" class="btn btn-primary btn-primary">
            Scheme Details <span class="glyphicon glyphicon-th-list"></span>
        </button>
    </div>
    <div class="col-md-3">
        <a href="#" class="btn btn-primary btn-primary">NFO List <span class="glyphicon glyphicon-th-list">
        </span></a>
    </div>
    <div class="col-md-3">
        <a href="#" class="btn btn-primary btn-primary">View Watch list <span class="glyphicon glyphicon-list-alt">
        </span></a>
    </div>
    <div class="col-md-3">
        <a href="#" class="btn btn-primary btn-primary">Top Ten Schemes <span class="glyphicon glyphicon-th-list">
        </span></a>
    </div>
</div>
<div id="demo" class="row " style="margin-left: 11%; margin-top: 1%; margin-bottom: 2%;
    margin-right: 5%; border: 2px solid #a1a1a1; padding-top: 1%; padding-bottom: 1%">
    <div class="col-md-12 col-xs-12 col-sm-12">
        <div class="col-md-7">
            <b>AMC:</b>
            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="form-control input-sm" Width="100%"
                OnSelectedIndexChanged="ddlAMC_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAMC" runat="server" ControlToValidate="ddlAMC" InitialValue="0" ErrorMessage="Please Select AMC" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-5">
            <b>Category: </b>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control input-sm">
            </asp:DropDownList>
        </div>
    </div>
    <div class="col-md-12">
        <div class="col-md-8">
            <b>Scheme:</b>
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control input-sm"
                class="form-control">
            </asp:DropDownList>
        </div>
        <div class="col-md-4" style="margin-top: 2%">
            <asp:Button ID="Button1" runat="server" class="btn btn-sm btn-primary" Text="GO"
                OnClick="Go_OnClick"></asp:Button>
        </div>
    </div>
</div>
<%--<div class="row container">
  <h2>Basic Modal Example</h2>
  <!-- Trigger the modal with a button -->
  <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Open Modal</button>
</div>--%>
<!-- Modal -->
<%--<div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Modal Header</h4>
        </div>
        <div class="modal-body">
          <p>Some text in the modal.</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>--%>
<div id="dvSchemeDetailsl" runat="server" class="container-fluid" style="margin-left: 11%;
    margin-bottom: 2%; margin-right: 5%; border: 2px solid #a1a1a1;" visible="false">
    <h4 style="padding-left: 40%; font-weight: bolder;">
        Scheme Details</h4>
    <asp:Repeater ID="rpSchemeDetails" runat="server" OnItemCommand="rpSchemeDetails_OnItemCommand">
        <ItemTemplate>
            <div class="row " style="margin-left: 2%; margin-right: 2%; margin-bottom: 2%; margin-top: 2%;
                border: 2px solid #a1a1a1;">
                <div class="col-sm-12">
                    <div class="row " style="background-color: #286090; margin-bottom: 2%; color: Black">
                        <div class="col-sm-10">
                            <h4>
                                <asp:LinkButton ID="lbSchemeName" runat="server" ToolTip="Click To view Details Information"
                                    CssClass="btn-primary"  CommandName="schemeDetails" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'> <%# Eval("PASP_SchemePlanName")%>
                                </asp:LinkButton>
                                
                            </h4>
                        </div>
                        <div class="col-sm-2 " style="margin-top: 1%">
                            <asp:LinkButton ID="lbAddToWatch" runat="server" CssClass="btn-primary" CommandName="addToWatch" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'> Add To Watch <span class="glyphicon glyphicon-dashboard">
                            </span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbViewWatch" runat="server" CssClass="btn-primary"  CommandName="viewWatchList" Visible="false">In WatchList <span class="glyphicon glyphicon-dashboard">
                            </span></asp:LinkButton>
                        </div>
                        
                    </div>
                    <div class="row" style="border-bottom:dotted 2px #a1a1a1; padding-bottom:2%">
                        <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2" id="dvRating">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <asp:Image runat="server" ID="imgRatingOvelAll" ImageUrl='<%# string.Format("../Images/MorningStarRating/RatingOverall/{0}.{1}", Eval("SchemeRatingOverall"),"png")%>' />
                            </div>
                        </div>
                        <div class="col-sm-12 col-sm-12 col-md-6 col-lg-6" style="border-right:dotted 2px #a1a1a1">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 fk-font-12">
                                <strong>AMC:
                                    <%# Eval("PA_AMCName")%>
                                </strong>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 dvRow" >
                                Fund Manager Rating: <a href="#">Rating</a>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 dvRow">
                                Last NAV Date:
                                <%# Eval("CMFNP_NAVDate")%>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 dvRow">
                                Initial Amount( Regular / SIP):
                                <%# Eval("[InitialPrice SIP]")%>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 dvRow" >
                                Additional Purchase ( Regular / SIP):
                                <%# Eval("[AdditionalPrice SIP]")%>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 dvRow">
                                Scheme And Benchmark Peformance( Over 1/3/5 years): 
                            </div>
                        </div>
                        <div class="col-sm-12 col-sm-12 col-md-4 col-lg-4">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <b>Category:
                                    <%# Eval("PAIC_AssetInstrumentCategoryName")%></b>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 dvRow">
                                Exit Load:
                                <%# Eval("PASPD_ExitLoadPercentage")%>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 dvRow">
                                FMC Charge:
                                <%# Eval("PASPD_ExitLoadPercentage")%>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 dvRow">
                                Cut-Off Time:
                                <%# Eval("PASPD_CutOffTime")%>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 dvRow">
                                Recommendation: Buy
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 dvRow">
                                Third Column, sixth Cell
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-left: 30%; margin-right: 2%; margin-bottom: 2%; margin-top: 2%">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <asp:LinkButton ID="lbBuy" runat="server" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>' CommandName="Buy" CssClass="btn btn-primary btn-primary" Visible='<%# Eval("IsSchemePurchege")%>'> Buy <span class="glyphicon glyphicon-shopping-cart">
                            </span></asp:LinkButton>
                            &nbsp &nbsp
                            <asp:LinkButton ID="lbAddPurchase" runat="server" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>' CommandName="ABY" CssClass="btn btn-primary btn-success"> Additional Purchase <span class="glyphicon glyphicon-plus-sign">
                            </span></asp:LinkButton>
                            &nbsp &nbsp <asp:LinkButton ID="lbSIP" runat="server" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>' CommandName="SIP" CssClass="btn btn-primary btn-info" Visible='<%# Eval("IsSchemeSIPType")%>'> SIP <span class="glyphicon glyphicon-th-list">
                            </span></asp:LinkButton> &nbsp &nbsp
                            <asp:LinkButton ID="lbRedem" runat="server" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>' CommandName="Sell" CssClass="btn btn-primary btn-danger" Visible="false"> Redemption <span class="glyphicon glyphicon-minus-sign">
                            </span></asp:LinkButton> 
                            
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

