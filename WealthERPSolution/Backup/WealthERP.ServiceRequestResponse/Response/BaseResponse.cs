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
        public ServiceResultDTO ServiceResult { get; set; }

        public BaseResponse()
        {
            ServiceResult = new ServiceResultDTO();
        }

        public void SetServiceResult(bool IsSuccess, int AppCode, string SysErrMessage)
        {
            ServiceResult.IsSuccess = IsSuccess;
            ServiceResult.Code = AppCode;
            ServiceResult.AppMessage = WerpErrorDto.GetAppMessage(AppCode);
            ServiceResult.SystemMessage = string.IsNullOrEmpty(SysErrMessage) ? "" : SysErrMessage;
        }
    }
}
