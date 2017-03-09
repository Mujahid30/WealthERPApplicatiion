using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{

    /// <summary>
    ///   WERP Product Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductLookupResponse : BaseResponse
    {
        /// <summary>
        ///   Gets and Sets ProductList
        /// </summary>
        [MessageBodyMember(Order = 0)]
        public ProductListDto ProductListResponse { get; set; }

        public ProductLookupResponse()
        {
            ProductListResponse = new ProductListDto();
        }
    }
}
