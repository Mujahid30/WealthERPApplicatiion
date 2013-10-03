using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{

    /// <summary>
    ///   WERP Common Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductAmcLookupResponse : BaseResponse
    {
        /// <summary>
        ///   Gets and Sets ProductAMCList
        /// </summary>
        [MessageBodyMember(Order = 0)]
        public ProductAmcListDto ProductAmcListResponse { get; set; }

        public ProductAmcLookupResponse()
        {
            ProductAmcListResponse = new ProductAmcListDto();
        }
    }
}
