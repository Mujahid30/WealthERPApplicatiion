using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WealthERP.BusinessEntities
{
    /// <summary>
    ///   Service Result DTO
    /// </summary>
    [DataContract]
    [Serializable]
    public class ServiceResultDTO
    {
        [DataMember(Order = 0)]
        public bool  IsSuccess { get; set; }

        [DataMember(Order = 1)]
        public int Code { get; set; }

        [DataMember(Order = 2)]
        public string AppMessage { get; set; }

        [DataMember(Order = 3)]
        public string SystemMessage { get; set; }

        public ServiceResultDTO()
        {
            IsSuccess = false;
            Code = -1;
        }
    }
}
