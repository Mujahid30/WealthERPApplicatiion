using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WealthERP.BusinessEntities
{
    /// <summary>
    ///   Product AMC List DTO
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProductAmcListDto
    {
        [DataMember(Order = 0)]      
        public List<KeyValuePair<string, string>> ProductAMCList { get; set; }

        public ProductAmcListDto()
        {
            ProductAMCList = new List<KeyValuePair<string, string>>();
        }

    }
}
