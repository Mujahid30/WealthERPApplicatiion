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
    public class ProductAmcResponse : BaseResponse
    {
        /// <summary>
        ///   Gets and Sets ProductCategoryList
        /// </summary>
        [MessageBodyMember(Order = 0)]
        public ProductAmcListDTO ProductAmcListDTO { get; set; }

        public ProductAmcResponse()
        {
            ProductAmcListDTO = new ProductAmcListDTO();
        }
    }
}
