using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WealthERP.ServiceFacade
{
    //public class BaseServiceFacade
    //{
    //    private static string ipAddress;

    //    /// <summary>
    //    ///   Initializes a new instance of the <see cref="BaseServiceFacade" /> class.
    //    /// </summary>
    //    /// <remarks>
    //    /// </remarks>
    //    static BaseServiceFacade()
    //    {
    //        // Get the System IP Address.
    //        ipAddress = EnvironmentHelper.GetIpAddress();
    //        CreateMappingsForCallContextResponse();
    //    }

    //    /// <summary>
    //    ///   Constructs and throws Fault Exception
    //    /// </summary>
    //    /// <param name="exception"> Exception raised in the caller. </param>
    //    /// <param name="problemCode"> Problem code to report to the caller. </param>
    //    protected FaultException<FaultDetails> HandleFaultException(Exception exception,
    //                                                                string problemCode)
    //    {
    //        return new FaultException<FaultDetails>(
    //            new FaultDetails
    //            {
    //                Version = string.Empty,
    //                ApplicationID = string.Empty,
    //                NodeID = string.Empty,
    //                PlatformID = string.Empty,
    //                ProblemCode = problemCode.IsVoid()
    //                                  ? ProblemCodeConstants.ServiceFailureUnexpectedError
    //                                  : problemCode,
    //                StackTrace = null,
    //                SystemException = exception.Message,
    //                Remedy = null,
    //                Timestamp = DateTime.Now
    //            },
    //            ProblemCode.ServiceFailure_UnexpectedError.ToString());
    //    }

    //    /// <summary>
    //    ///   Handles the fault exception.
    //    /// </summary>
    //    /// <param name="exception"> The exception. </param>
    //    /// <param name="callContext"> The call context. </param>
    //    /// <param name="problemCode"> The problem code. </param>
    //    /// <returns> </returns>
    //    protected FaultException<FaultDetails> HandleFaultException(Exception exception,
    //                                                                CallContext callContext,
    //                                                                string problemCode)
    //    {
    //        Guard.ThrowIfNull(() => callContext);

    //        if (callContext != null)
    //        {
    //            return new FaultException<FaultDetails>(
    //                new FaultDetails
    //                {
    //                    ApplicationID = callContext.ApplicationID,
    //                    NodeID = callContext.NodeID,
    //                    PlatformID = callContext.PlatformID,
    //                    ProblemCode = problemCode,
    //                    StackTrace = exception.StackTrace,
    //                    SystemException = exception.Message,
    //                    Timestamp = DateTime.Now,
    //                    Version = callContext.ExpectedVersion
    //                },
    //                new FaultReason(exception.Message));
    //        }

    //        return null;
    //    }

    //    /// <summary>
    //    ///   Determines whether [is call context valid] [the specified call context].
    //    /// </summary>
    //    /// <param name="callContext"> The call context. </param>
    //    /// <returns> <c>true</c> if [is call context valid] [the specified call context]; otherwise, <c>false</c> . </returns>
    //    protected bool IsCallContextValid(CallContext callContext)
    //    {
    //        if (callContext == null ||
    //            callContext.Context == null ||
    //            callContext.Topic == null ||
    //            callContext.User == null)
    //        {
    //            return false;
    //        }

    //        return true;
    //    }

    //    /// <summary>
    //    ///   Builds the call context for response.
    //    /// </summary>
    //    /// <param name="requestCallContext"> The request call context. </param>
    //    /// <param name="isCallContextValid"> if set to <c>true</c> [is call context valid]. </param>
    //    /// <returns> Response CallContext. </returns>
    //    protected CallContext BuildCallContextForResponse(CallContext requestCallContext,
    //                                                      ref bool isCallContextValid)
    //    {
    //        if (!IsCallContextValid(requestCallContext))
    //        {
    //            isCallContextValid = false;
    //            throw new InvalidOperationException("Invalid parameters in the call context of request object.");
    //        }

    //        isCallContextValid = true;

    //        CallContext responseCallContext = Mapper.Map<CallContext, CallContext>(requestCallContext);
    //        responseCallContext.ApplicationID = CLANConstants.APPLICATION_ID;
    //        responseCallContext.CreationDateTime = DateTime.UtcNow;
    //        // Get namespace of the service contract
    //        responseCallContext.ExpectedVersion = GetContractNamespace(ReflectionHelper.GetCallingMethod().ToString());
    //        responseCallContext.NodeID = ipAddress;
    //        responseCallContext.PlatformID = CLANConstants.PLATFORM_ID;

    //        if (responseCallContext.Topic != null)
    //        {
    //            if (requestCallContext != null && requestCallContext.Topic != null &&
    //                requestCallContext.Topic.BrandingID.IsVoid() == false)
    //            {
    //                responseCallContext.Topic.BrandingID = requestCallContext.Topic.BrandingID;
    //            }

    //            responseCallContext.Topic.OriginatingDomain = Environment.UserDomainName;
    //        }

    //        return responseCallContext;
    //    }



    //    /// <summary>
    //    ///   Gets the contract namespace.
    //    /// </summary>
    //    /// <param name="callingMethodName"> Name of the calling method. </param>
    //    /// <returns> </returns>
    //    protected string GetContractNamespace(string callingMethodName)
    //    {
    //        string contractNamespace = string.Empty;

    //        Type facadeType = GetType();
    //        Type[] facadeInterfaces = facadeType.GetInterfaces();

    //        if (!facadeInterfaces.IsEmpty())
    //        {
    //            Type contractType = facadeInterfaces.FirstOrDefault(
    //                contract => contract.GetMethods().FirstOrDefault(contractMethod => contractMethod.ToString() == callingMethodName) != null);

    //            if (contractType != null)
    //            {
    //                object[] customAttributes = contractType.GetCustomAttributes(true);

    //                if (!customAttributes.IsEmpty())
    //                {
    //                    var serviceContractAttribute =
    //                        (ServiceContractAttribute)customAttributes.FirstOrDefault(customAttribute => customAttribute is ServiceContractAttribute);

    //                    if (serviceContractAttribute != null)
    //                    {
    //                        contractNamespace =
    //                            serviceContractAttribute.
    //                                Namespace;
    //                    }
    //                }
    //            }
    //        }

    //        return contractNamespace;
    //    }

    //    /// <summary>
    //    ///   Builds the application error list.
    //    /// </summary>
    //    /// <param name="errors"> The errors. </param>
    //    /// <returns> </returns>
    //    protected List<ApplicationError> BuildApplicationErrorList(IEnumerable<string> errors)
    //    {
    //        List<ApplicationError> applicationErrorList = (from error in errors
    //                                                       select new ApplicationError
    //                                                       {
    //                                                           ErrorMessageText = error
    //                                                       }).ToList();

    //        return applicationErrorList;
    //    }

    //    /// <summary>
    //    /// Moved from BaseServiceFacde.BuildCallContextForResponse
    //    /// </summary>
    //    private static void CreateMappingsForCallContextResponse()
    //    {
    //        AutomapperHelper.TryCreateMap<CallContext, CallContext>();
    //        AutomapperHelper.TryCreateMap<CallTopic, CallTopic>();
    //        AutomapperHelper.TryCreateMap<UserContext, UserContext>();
    //        AutomapperHelper.TryCreateMap<TracingDetails, TracingDetails>();
    //    }
    //}
    class BaseServiceFacade
    {
    }


}
