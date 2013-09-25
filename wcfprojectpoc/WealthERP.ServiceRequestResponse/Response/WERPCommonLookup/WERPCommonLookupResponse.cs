using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{

    /// <summary>
    ///   WERP Common Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class WERPCommonLookupResponse : BaseResponse
    {
        /// <summary>
        ///   Gets and Sets ProductAMCList
        /// </summary>
        [MessageBodyMember(Order = 0)]
        public ProductAMCListDTO ProductAMCListResponse { get; set; }
    }
}
