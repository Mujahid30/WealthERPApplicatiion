using System.ServiceModel;
using WealthERP.BusinessEntities;

namespace WealthERP.ServiceRequestResponse
{
    /// <summary>
    /// WERP Product Scheme Plan Lookup  Request
    /// </summary>

    [MessageContract(IsWrapped = false)]
    public class ProductAmcSchemePlanLookupRequest : BaseRequest 
    {
        /// <summary>
        ///   Gets and Sets ProductCode
        /// </summary>
        /// 
        [MessageBodyMember(Order = 0)]
        public string ProductCode { get; set; }

        /// <summary>
        ///   Gets and Sets ProductAmcCode
        /// </summary>
        /// 
        [MessageBodyMember(Order = 1)]
        public string ProductAmcCode { get; set; }

        /// <summary>
        ///   Gets and Sets CategoryCode
        /// </summary>
        /// 
        [MessageBodyMember(Order = 2)]
        public string CategoryCode { get; set; }

        /// <summary>
        ///   Gets and Sets SubCategoryCode
        /// </summary>
        /// 
        [MessageBodyMember(Order = 3)]
        public string SubCategoryCode { get; set; }

        /// <summary>
        ///   Gets and Sets SubSubCategoryCode
        /// </summary>
        /// 
        [MessageBodyMember(Order = 4)]
        public string SubSubCategoryCode { get; set; }
    }
}
