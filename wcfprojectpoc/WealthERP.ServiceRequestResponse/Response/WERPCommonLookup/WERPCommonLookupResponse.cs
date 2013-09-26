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
        public ProductAMCListDTO ProductAMCListDTO { get; set; }

        /// <summary>
        ///   Gets and Sets ProductList
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public ProductListDTO ProductListDTO { get; set; }

        public WERPCommonLookupResponse()
        {
            ProductAMCListDTO = new ProductAMCListDTO();
            ProductListDTO = new ProductListDTO();
        }
    }
}
