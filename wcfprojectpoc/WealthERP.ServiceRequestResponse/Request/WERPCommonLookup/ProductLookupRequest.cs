using System.ServiceModel;

using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{
    /// <summary>
    ///   WERP Product Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductLookupRequest : BaseRequest
    {
        /// <summary>
        ///   Gets and Sets ProductAMCList
        /// </summary>
        /// 
        [MessageBodyMember(Order = 0)]
        public string ProductCode { get; set; }
    }
}
