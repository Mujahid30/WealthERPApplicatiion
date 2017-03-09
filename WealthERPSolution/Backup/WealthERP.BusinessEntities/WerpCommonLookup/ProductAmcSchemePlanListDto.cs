using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WealthERP.BusinessEntities
{
    /// <summary>
    ///   Product Scheme Plan List DTO
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProductAmcSchemePlanListDto
    {
        [DataMember(Order = 0)]
        public string ProductCode { get; set; }

        [DataMember(Order = 1)]
        public string ProductAmcCode { get; set; }

        [DataMember(Order = 2)]
        public string ProductCategoryCode { get; set; }

        [DataMember(Order = 3)]
        public string ProductSubCategoryCode { get; set; }

        [DataMember(Order = 3)]
        public string ProductSubSubCategoryCode { get; set; }

        [DataMember(Order = 5)]
        public int ProductAmcSchemePlanCode { get; set; }

        [DataMember(Order = 6)]
        public string ProductAmcSchemePlanName { get; set; }

        public ProductAmcSchemePlanListDto()
        {
        }
    }
}
