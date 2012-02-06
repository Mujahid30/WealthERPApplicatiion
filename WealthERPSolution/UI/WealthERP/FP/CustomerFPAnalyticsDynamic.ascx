<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPAnalyticsDynamic.ascx.cs" Inherits="WealthERP.FP.CustomerFPAnalyticsDynamic" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
 
 <table width="100%">
            <tr>
               <td>
                              
               <br />
               <br />
               <br />
               <br />
               <br />
               
               </td>
            </tr>
            
            <tr id="trSumbitSuccess" runat="server">
                <td align="center">
                   <div id="msgRecordStatus" class="accountLock" align="center">                        
                   <b>Sorry, your account is restricted, Please contact Customer Care for further assistance</b>
                    <br />
                    ~~~~~Customer Care Contact Details~~~~~
                    <br />
                    Vijay N Shenoy
                    <br />
                    Ampsys Consulting Pvt.Ltd.
                    <br />
                    Ph: 080 - 32429514.
                    <br />
                    Mob: +91 9663305249.
                    <br />                    
                   </div>
                </td>
            </tr>
            
   </table>