﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WealthERP.ServiceRequestResponse
{
    /// <summary>
    ///   WERP Common Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductCategoryRequest : BaseRequest
    {
        [MessageBodyMember(Order = 0)]
        public string ProductType { get; set; }
    }
}
