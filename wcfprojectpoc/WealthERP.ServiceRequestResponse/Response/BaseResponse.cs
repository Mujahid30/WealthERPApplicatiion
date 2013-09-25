using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{
    /// <summary>
    ///   BaseResponse
    /// </summary>

    [MessageContract]
    public class BaseResponse
    {
        [MessageBodyMember(Order = 0)]
        public ServiceResultDTO ServiceResultDTO { get; set; }
       
        public BaseResponse()
        {
            ServiceResultDTO = new ServiceResultDTO();
        }
    }
}
