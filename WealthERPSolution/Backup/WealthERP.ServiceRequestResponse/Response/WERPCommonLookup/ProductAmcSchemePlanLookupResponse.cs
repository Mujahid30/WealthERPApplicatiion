using System.ServiceModel;
using WealthERP.BusinessEntities;
using System.Collections.Generic;

namespace WealthERP.ServiceRequestResponse
{
    /// <summary>
    ///   WERP Product Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductAmcSchemePlanLookupResponse : BaseResponse
    {
        /// <summary>
        ///   Gets and Sets CategoryList
        /// </summary>
        [MessageBodyMember(Order = 0)]
        public List<ProductAmcSchemePlanListDto> DtoProductAmcSchemePlanList { get; set; }

        public ProductAmcSchemePlanLookupResponse()
        {
            DtoProductAmcSchemePlanList = new List<ProductAmcSchemePlanListDto>();
        }
    }
}
