using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WealthERP.BusinessEntities
{
    /// <summary>
    ///   Product List DTO
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProductListDto
    {
        [DataMember(Order = 0)]
        public List<KeyValuePair<string, string>> ProductList { get; set; }
        
        public ProductListDto()
        {
            ProductList = new List<KeyValuePair<string, string>>();
        }
    }
}
