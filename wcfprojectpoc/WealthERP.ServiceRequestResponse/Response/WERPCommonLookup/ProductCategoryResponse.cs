using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{
    /// <summary>
    ///   WERP Common Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductCategoryResponse : BaseResponse
    {
        /// <summary>
        ///   Gets and Sets ProductCategoryList
        /// </summary>
        [MessageBodyMember(Order = 0)]
        public ProductCategoryListDTO ProductCategoryListDTO { get; set; }

        public ProductCategoryResponse()
        {
            ProductCategoryListDTO = new ProductCategoryListDTO();
        }
    }
}
