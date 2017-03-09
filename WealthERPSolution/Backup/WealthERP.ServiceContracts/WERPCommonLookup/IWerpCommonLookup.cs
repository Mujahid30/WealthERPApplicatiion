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
        ///   Gets the Product List.
        /// </summary>
        /// <param name="ProductType"> The Product Type Request. </param>
        /// <returns> Product List Response </returns>
        [OperationContract]
        ProductLookupResponse GetProductList(ProductLookupRequest request);

        /// <summary>
        ///   Gets the Product AMC List.
        /// </summary>
        /// <param name="ProductType"> The Product Type Request. </param>
        /// <returns> Product AMC List Response </returns>
        [OperationContract]
        ProductAmcLookupResponse GetProductAmcList(ProductAmcLookupRequest request);

        /// <summary>
        ///   Gets the Product Category List.
        /// </summary>
        /// <param name="ProductType"> The Product Type Request. </param>
        /// <returns> Product AMC List Response </returns>
        [OperationContract]
        ProductCategoryLookupResponse GetProductCategoryList(ProductCategoryLookupRequest request);

        /// <summary>
        ///   Gets the Product Sub-Category List.
        /// </summary>
        /// <param name="ProductType"> The Product Type Request. </param>
        /// <returns> Product AMC List Response </returns>
        [OperationContract]
        ProductSubCategoryLookupResponse GetProductSubCategoryList(ProductSubCategoryLookupRequest request);

        /// <summary>
        /// Gets the Product SchemePlan List.
        /// </summary>
        /// <param name="ProductType"> The Product Type Request. </param>
        /// <returns> Product AMC List Response </returns>
        [OperationContract]
        ProductAmcSchemePlanLookupResponse GetProductAmcSchemePlanList(ProductAmcSchemePlanLookupRequest request);
    }
}
