<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountDeactive.ascx.cs" Inherits="WealthERP.General.AccountDeactive" %>
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
                   <br />
                   <br />
                                       
                   <b>Your account access has been temporarily disabled.</b>      
                   <br />
                    Please contact <b>+91 99001 66306</b> for further details                             
                   </div>
                </td>
            </tr>
            
   </table>
