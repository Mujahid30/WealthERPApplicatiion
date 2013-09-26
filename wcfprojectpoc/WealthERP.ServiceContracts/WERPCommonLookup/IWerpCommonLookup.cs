using System.ServiceModel;
using WealthERP.ServiceRequestResponse;
using WealthERP.ServiceRequestResponse.Response.WERPCommonLookup;
using WealthERP.ServiceRequestResponse.Request.WERPCommonLookup;


namespace WealthERP.ServiceContracts
{
    /// <summary>
    ///   Product AMC List Contract Interface
    /// </summary>
    [ServiceContract]
    public interface IWerpCommonLookupContract
    {
        /// <summary>
        ///   Gets the Product List.
        /// </summary>
        /// <param name="ProductType"> The Product Type Request. </param>
        /// <returns> Product List Response </returns>
        [OperationContract]
        WERPCommonLookupResponse GetProductList(WERPCommonLookupRequest request);

        /// <summary>
        ///   Gets the Product AMC List.
        /// </summary>
        /// <param name="ProductType"> The Product Type Request. </param>
        /// <returns> Product AMC List Response </returns>
        [OperationContract]
        WERPCommonLookupResponse GetProductAMCList(WERPCommonLookupRequest request);

        /// <summary>
        ///   Gets the Product Category List Product.
        /// </summary>
        /// <param name="ProductType"> The Product Type Request. </param>
        /// <returns> Product Category List Response </returns>
        [OperationContract]
        ProductCategoryResponse GetProductCategoryList(ProductCategoryRequest request);
    }

}
