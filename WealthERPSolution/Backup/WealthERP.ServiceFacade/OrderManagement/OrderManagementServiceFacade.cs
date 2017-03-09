using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using WealthERP.BusinessEntities;
using WealthERP.ServiceRequestResponse;


namespace WealthERP.ServiceFacade.OrderManagement
{
//     <summary>
//       Personal Settings Service Facade
//     </summary>
//     <remarks>
//     </remarks>
//    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, IgnoreExtensionDataObject = true)]
//    public class PersonalSettingsServiceFacade : BaseServiceFacade,
//                                                 IPersonalSettingsContract
//    {
//        private const string PERSONALSETTINGS_ERROR = "personalSettingsRequest";
//        private const string PSR_SCREEN_REQUEST = "psrScreenRequest";

//        #region IPersonalSettingsContract Members

//        /// <summary>
//        ///   Gets the personal settings.
//        /// </summary>
//        /// <param name="personalSettingsRequest"> The personal settings request. </param>
//        /// <returns> Personal Settings Response. </returns>
//        public PersonalSettingsResponse GetPersonalSettings(PersonalSettingsRequest personalSettingsRequest)
//        {
//            var personalSettingsResponse = new PersonalSettingsResponse();
//            List<ApplicationError> applicationErrorsList = null;
//            CallContext responseCallContext = null;
//            var problemCode = string.Empty;
//            var isCallContextValid = false;

//            try
//            {
//                if (personalSettingsRequest == null)
//                {
//                    throw new ArgumentNullException(PERSONALSETTINGS_ERROR);
//                }

//                personalSettingsResponse.CallContext = BuildCallContextForResponse(personalSettingsRequest.CallContext,
//                                                                                   ref isCallContextValid);
//                personalSettingsResponse.PersonalSettings =
//                    BusinessComponentFactory.PersonalSettingsBusinessComponent.GetPersonalSettings(personalSettingsRequest.PersonalSettings.UserId,
//                                                                                                   ref applicationErrorsList,
//                                                                                                   ref problemCode,
//                                                                                                   personalSettingsRequest.CallContext,
//                                                                                                   ref responseCallContext);
//                personalSettingsResponse.ApplicationErrorList = applicationErrorsList;

//                if (responseCallContext != null)
//                {
//                    personalSettingsResponse.CallContext = responseCallContext;
//                }
//            }
//            catch (SecurityException securityException)
//            {
//                if (problemCode.IsVoid() == false)
//                {
//                    problemCode = ProblemCode.CLAN_SF_Exception.ToString();
//                }

//                if (personalSettingsRequest != null)
//                {
//                    ExceptionHandler.HandleException(securityException,
//                                                     personalSettingsRequest.CallContext.MessageID);
//                }

//                throw HandleFaultException(securityException,
//                                           personalSettingsResponse.CallContext,
//                                           problemCode);
//            }
//            catch (Exception exception)
//            {
//                if (problemCode.IsVoid() == false)
//                {
//                    problemCode = ProblemCode.APP_Execution_Error.ToString();
//                }

//                ExceptionHandler.HandleException(exception,
//                                                 isCallContextValid
//                                                     ? personalSettingsRequest.CallContext.MessageID
//                                                     : String.Empty);
//                throw HandleFaultException(exception,
//                                           personalSettingsResponse.CallContext,
//                                           problemCode);
//            }

//            return personalSettingsResponse;
//        }

    //    /// <summary>
    //    ///   Saves the personal settings.
    //    /// </summary>
    //    /// <param name="personalSettingsRequest"> The personal settings request. </param>
    //    public void SavePersonalSettings(PersonalSettingsRequest personalSettingsRequest)
    //    {
    //        var personalSettingsResponse = new PersonalSettingsResponse();
    //        List<ApplicationError> applicationErrorsList = null;
    //        string problemCode = null;
    //        CallContext responseCallContext = null;
    //        var isCallContextValid = false;
    //        try
    //        {
    //            if (personalSettingsRequest == null)
    //            {
    //                throw new ArgumentNullException(PERSONALSETTINGS_ERROR);
    //            }

    //            personalSettingsResponse.CallContext = BuildCallContextForResponse(personalSettingsRequest.CallContext,
    //                                                                               ref isCallContextValid);
    //            BusinessComponentFactory.PersonalSettingsBusinessComponent.SavePersonalSettings(personalSettingsRequest.PersonalSettings,
    //                                                                                            ref applicationErrorsList,
    //                                                                                            ref problemCode,
    //                                                                                            personalSettingsRequest.CallContext,
    //                                                                                            ref responseCallContext);
    //            personalSettingsResponse.ApplicationErrorList = applicationErrorsList;

    //            if (responseCallContext != null)
    //            {
    //                personalSettingsResponse.CallContext = responseCallContext;
    //            }
    //        }

    //        catch (SecurityException securityException)
    //        {
    //            if (problemCode.IsVoid() == false)
    //            {
    //                problemCode = ProblemCode.CLAN_SF_Exception.ToString();
    //            }

    //            if (personalSettingsRequest != null)
    //            {
    //                ExceptionHandler.HandleException(securityException,
    //                                                 personalSettingsRequest.CallContext.MessageID);
    //            }
    //            throw HandleFaultException(securityException,
    //                                       personalSettingsResponse.CallContext,
    //                                       problemCode);
    //        }
    //        catch (Exception exception)
    //        {
    //            if (problemCode.IsVoid() == false)
    //            {
    //                problemCode = ProblemCode.APP_Execution_Error.ToString();
    //            }

    //            ExceptionHandler.HandleException(exception,
    //                                             isCallContextValid
    //                                                 ? personalSettingsRequest.CallContext.MessageID
    //                                                 : string.Empty);
    //            throw HandleFaultException(exception,
    //                                       personalSettingsResponse.CallContext,
    //                                       problemCode);
    //        }
    //    }

    //    /// <summary>
    //    ///   Get the Default Screen values.
    //    /// </summary>
    //    /// <param name="psrScreenRequest"> The Default Screen request. </param>
    //    /// <returns> Get Default Screens Response </returns>
    //    public PersonalSettingsResponse GetScreens(PersonalSettingsRequest psrScreenRequest)
    //    {
    //        var psrScreenResponse = new PersonalSettingsResponse();
    //        List<ApplicationError> applicationErrorsList = null;
    //        CallContext responseCallContext = null;
    //        var problemCode = string.Empty;
    //        var isCallContextValid = false;

    //        try
    //        {
    //            if (psrScreenRequest == null)
    //            {
    //                throw new ArgumentNullException(PSR_SCREEN_REQUEST);
    //            }

    //            psrScreenResponse.CallContext = BuildCallContextForResponse(psrScreenRequest.CallContext,
    //                                                                        ref isCallContextValid);
    //            var defaultScreenDTO =
    //                BusinessComponentFactory.PersonalSettingsBusinessComponent.GetDefaultScreen(psrScreenRequest.PersonalSettings.UserCountry,
    //                                                                                            ref applicationErrorsList,
    //                                                                                            ref problemCode,
    //                                                                                            psrScreenRequest.CallContext,
    //                                                                                            ref responseCallContext);
    //            psrScreenResponse = new PersonalSettingsResponse
    //            {
    //                PersonalSettings = new PersonalSettingsDTO
    //                {
    //                    DefaultScreenCollection = defaultScreenDTO
    //                },
    //                ApplicationErrorList = applicationErrorsList,
    //                CallContext = responseCallContext
    //            };
    //        }
    //        catch (SecurityException securityException)
    //        {
    //            if (problemCode.IsVoid() == false)
    //            {
    //                problemCode = ProblemCode.APP_Execution_Error.ToString();
    //            }
    //            if (psrScreenRequest != null)
    //            {
    //                ExceptionHandler.HandleException(securityException,
    //                                                 psrScreenRequest.CallContext.MessageID);
    //            }
    //            throw HandleFaultException(securityException,
    //                                       psrScreenResponse.CallContext,
    //                                       problemCode);
    //        }
    //        catch (Exception exception)
    //        {
    //            if (problemCode.IsVoid() == false)
    //            {
    //                problemCode = ProblemCode.APP_Execution_Error.ToString();
    //            }
    //            ExceptionHandler.HandleException(exception,
    //                                             isCallContextValid
    //                                                 ? psrScreenRequest.CallContext.MessageID
    //                                                 : String.Empty);
    //            throw HandleFaultException(exception,
    //                                       psrScreenResponse.CallContext,
    //                                       problemCode);
    //        }
    //        return psrScreenResponse;
    //    }

    //    #endregion
    //}


   //------------------------------------------------------------------------------------------------------------------------
}
