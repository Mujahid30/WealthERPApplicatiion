using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{
    /// <summary>
    ///   BaseResponse
    /// </summary>

    [MessageContract]
    public class BaseRequest
    {
        [MessageBodyMember(Order = 0)]
        public UserAccountDTO UserAccountDetails { get; set; }
    }
}
