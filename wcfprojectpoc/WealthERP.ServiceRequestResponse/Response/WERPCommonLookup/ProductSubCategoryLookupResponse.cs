using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{
    /// <summary>
    ///   WERP Product Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductSubCategoryLookupResponse : BaseResponse
    {
        /// <summary>
        ///   Gets and Sets CategoryList
        /// </summary>
        [MessageBodyMember(Order = 0)]
        public ProductSubCategoryListDto DtoProductSubCategoryList { get; set; }

        public ProductSubCategoryLookupResponse() {
            DtoProductSubCategoryList = new ProductSubCategoryListDto();
        }
    }
}
