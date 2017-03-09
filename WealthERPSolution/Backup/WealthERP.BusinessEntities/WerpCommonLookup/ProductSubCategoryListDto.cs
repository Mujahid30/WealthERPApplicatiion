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
    public class ProductSubCategoryListDto
    {
        [DataMember(Order = 0)]
        public List<KeyValuePair<string, string>> ProductSubCategoryList { get; set; }
        
        public ProductSubCategoryListDto()
        {
            ProductSubCategoryList = new List<KeyValuePair<string, string>>();
        }
    }
}
