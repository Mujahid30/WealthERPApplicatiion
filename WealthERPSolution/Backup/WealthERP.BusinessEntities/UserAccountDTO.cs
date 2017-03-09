using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WealthERP.BusinessEntities
{
    /// <summary>
    ///   User Management DTO
    /// </summary>
    /// <remarks>
    /// </remarks>
    [DataContract]
    [Serializable]
    public class UserAccountDTO
    {
        [DataMember(Order = 0)]
        public string LoginId { get; set; }

        [DataMember(Order = 1)]
        public string Password { get; set; } 
    }
}
