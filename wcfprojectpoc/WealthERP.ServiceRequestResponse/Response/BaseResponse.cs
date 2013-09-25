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
        public ServiceResultDTO ServiceResult { get; set; }
    }
}
