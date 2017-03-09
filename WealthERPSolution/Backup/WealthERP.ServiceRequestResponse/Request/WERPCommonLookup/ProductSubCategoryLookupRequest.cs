using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{
    /// <summary>
    ///   WERP Product Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductSubCategoryLookupRequest : BaseRequest
    {
        /// <summary>
        ///   Gets and Sets ProductCode
        /// </summary>
        /// 
        [MessageBodyMember(Order = 0)]
        public string ProductCode { get; set; }

        /// <summary>
        ///   Gets and Sets CategoryCode
        /// </summary>
        /// 
        [MessageBodyMember(Order = 1)]
        public string CategoryCode { get; set; }

        /// <summary>
        ///   Gets and Sets SubCategoryCode
        /// </summary>
        /// 
        [MessageBodyMember(Order = 2)]
        public string SubCategoryCode { get; set; }
    }
}
