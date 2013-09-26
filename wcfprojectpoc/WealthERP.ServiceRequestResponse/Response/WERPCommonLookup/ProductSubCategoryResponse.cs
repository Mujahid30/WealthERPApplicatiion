using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse.Response.WERPCommonLookup
{
    /// <summary>
    ///   WERP Common Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductSubCategoryResponse : BaseResponse
    {
        /// <summary>
        ///   Gets and Sets ProductSubCategoryList
        /// </summary>
        [MessageBodyMember(Order = 0)]
        public ProductSubCategoryListDTO ProductSubCategoryListDTO { get; set; }

        public ProductSubCategoryResponse()
        {
            ProductSubCategoryListDTO = new ProductSubCategoryListDTO();
        }
    }
}
