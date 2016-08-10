<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOnlineSchemeManager.ascx.cs"
    Inherits="WealthERP.OPS.MFOnlineSchemeManager" %>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css">

<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>
<script >
    window.onload = function() {
    var username = '<%= Session["ExchangeMode"] %>';
        
        var dvOnlineMessage, dvDematMessage;
        dvOnlineMessage = document.getElementById('dvOnlineMessage')
        dvDematMessage = document.getElementById('dvDematMessage')
        if (username == "Demat") {
            dvDematMessage.style.display = 'block';
            dvOnlineMessage.style.display = 'none';
            
        }
        else {
            dvDematMessage.style.display = 'block';
            dvOnlineMessage.style.display = 'none';
        }

    };
</script>
<div id="demo" class="row" style="margin-left: 10%; margin-top: 5%; margin-bottom: 2%;
    margin-right: 10%; padding-top: 1%; padding-bottom: 1%; background-color: #2480C7;
    height: 10%">
    <div  style="margin-left: 5%; font-size: 20px;  display:none" class="col-md-10 col-xs-10 col-sm-10"  id="dvOnlineMessage">
        <img src="../Images/imagesyellow.png" width="50px" height="50px"/>
    
        <font color="#fff" ><b>Orders can not be placed between 5:00PM to 9:00PM, Further order can be placed after 9:00PM</b></font>
    </div>
    <div  style="margin-left: 5%; font-size: 20px; display:none" class="col-md-10 col-xs-10 col-sm-10" id="dvDematMessage">
        <img src="../Images/imagesyellow.png" width="50px" height="50px"/>
    
        <font color="#fff" ><b>Currently closed for orders.  Orders can only be placed during Exchange market hours between 9 am and 3 pm.</b></font>
    </div>
</div>
