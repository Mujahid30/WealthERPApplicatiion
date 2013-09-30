using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WealthERP.ServiceRequestResponse
{
    /// <summary>
    ///   WERP Product AMC Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductAmcRequest : BaseRequest
    {
        [MessageBodyMember(Order = 0)]
        public string ProductAmcCode { get; set; }
    }
}
