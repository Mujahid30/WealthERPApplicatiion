using System.ServiceModel;

using WealthERP.ServiceRequestResponse;


namespace WealthERP.ServiceContracts
{
    /// <summary>
    ///   Product AMC List Contract Interface
    /// </summary>
    [ServiceContract]
    public interface IWerpCommonLookupContract
    {
        /// <summary>
        ///   Gets the Product AMC List.
        /// </summary>
        /// <param name="ProductType"> The Product Type Request. </param>
        /// <returns> Product AMC List Response </returns>
        [OperationContract]
        ProductAmcLookupResponse GetProductAmcList(ProductAmcLookupRequest request);
    }
}
