using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WealthERP.ServiceRequestResponse.Request.WERPCommonLookup
{
    /// <summary>
    ///   WERP Common Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductSubCategoryRequest : BaseRequest
    {
        [MessageBodyMember(Order = 0)]
        public string ProductCategoryCode { get; set; }
    }
}
