using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{

    /// <summary>
    ///   WERP Product Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductCategoryLookupResponse : BaseResponse
    {
        /// <summary>
        ///   Gets and Sets CategoryList
        /// </summary>
        [MessageBodyMember(Order = 0)]
        public ProductCategoryListDto ProductCategoryList { get; set; }

        public ProductCategoryLookupResponse()
        {
            ProductCategoryList = new ProductCategoryListDto();
        }
    }
}
