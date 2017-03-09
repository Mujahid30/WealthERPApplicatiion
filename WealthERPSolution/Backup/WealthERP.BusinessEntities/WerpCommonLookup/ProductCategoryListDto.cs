using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WealthERP.BusinessEntities
{
    /// <summary>
    ///   Product Category List DTO
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProductCategoryListDto
    {
        [DataMember(Order = 0)]
        public List<KeyValuePair<string, string>> ProductCategoryList { get; set; }
        
        public ProductCategoryListDto()
        {
            ProductCategoryList = new List<KeyValuePair<string, string>>();
        }
    }
}
