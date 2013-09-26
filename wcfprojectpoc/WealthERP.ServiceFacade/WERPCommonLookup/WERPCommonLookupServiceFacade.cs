using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WealthERP.ServiceRequestResponse;
using WealthERP.BusinessEntities;
using WealthERP.ServiceContracts;
using BoCommon;
using BoCommisionManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using WealthERP.ServiceRequestResponse.Response.WERPCommonLookup;
using WealthERP.ServiceRequestResponse.Request.WERPCommonLookup;



namespace WealthERP.ServiceFacade
{
    /// <summary>
    ///   WERP Common Lookup Service Facade
    /// </summary>
    /// <remarks>
    /// </remarks>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, IgnoreExtensionDataObject = true)]
    public class WERPCommonLookupServiceFacade : IWerpCommonLookupContract
    {
        private const string PERSONALSETTINGS_ERROR = "personalSettingsRequest";
        private const string PSR_SCREEN_REQUEST = "psrScreenRequest";

        #region IWerpCommonLookupContract Members

         //<summary>
         //  Gets the personal settings.
         //</summary>
         //<param name="personalSettingsRequest"> The personal settings request. </param>
         //<returns> Personal Settings Response. </returns>

     

        #endregion

        #region IWerpCommonLookupContract Members

        WERPCommonLookupResponse IWerpCommonLookupContract.GetProductAMCList(WERPCommonLookupRequest request)
        {
            WERPCommonLookupResponse response = new WERPCommonLookupResponse();
            CommisionReceivableBo boCommRecv = new CommisionReceivableBo();

            try
            {
                DataSet dsLookupData = boCommRecv.GetProdAmc();

                foreach (DataRow row in dsLookupData.Tables[0].Rows)
                {
                    if (response.ServiceResultDTO.IsSuccess == false) { response.ServiceResultDTO.IsSuccess = true; }

                    KeyValuePair<string, string> prodAmc = new KeyValuePair<string, string>(row["PA_AMCCode"].ToString(), row["PA_AMCName"].ToString());
                    response.ProductAMCListDTO.ProductAMCList.Add(prodAmc);
                }
                
            }
            catch (Exception e)
            {
            }
            
            return response;
        }

        WERPCommonLookupResponse IWerpCommonLookupContract.GetProductList(WERPCommonLookupRequest request)
        {
            WERPCommonLookupResponse response = new WERPCommonLookupResponse();
            CommisionReceivableBo boCommRecv = new CommisionReceivableBo();

            try
            {
                DataSet dsLookupData = boCommRecv.GetProductType();

                foreach (DataRow row in dsLookupData.Tables[0].Rows)
                {
                    if (response.ServiceResultDTO.IsSuccess == false) { response.ServiceResultDTO.IsSuccess = true; }

                    KeyValuePair<string, string> prodType = new KeyValuePair<string, string>(row["PAG_AssetGroupCode"].ToString(), row["PAG_AssetGroupName"].ToString());
                    response.ProductListDTO.ProductList.Add(prodType);
                }

            }
            catch (Exception e)
            {
            }

            return response;
        }

        ProductCategoryResponse IWerpCommonLookupContract.GetProductCategoryList(ProductCategoryRequest request)
        {
            ProductCategoryResponse response = new ProductCategoryResponse();
            CommisionReceivableBo commBo = new CommisionReceivableBo();

            DataSet dsCats = commBo.GetCategories(request.ProductType);

            foreach (DataRow row in dsCats.Tables[0].Rows) { 
                response.ProductCategoryListDTO.ProductCategoryList.Add(new KeyValuePair<string, string>(row["PAIC_AssetInstrumentCategoryCode"].ToString(), row["PAIC_AssetInstrumentCategoryName"].ToString()));
            }
            return response;
        }

        ProductSubCategoryResponse IWerpCommonLookupContract.GetProductSubCategoryList(ProductSubCategoryRequest request)
        {
            ProductSubCategoryResponse response = new ProductSubCategoryResponse();
            CommisionReceivableBo commBo = new CommisionReceivableBo();

            DataSet dsCats = commBo.GetSubCategories(request.ProductCategoryCode);

            foreach (DataRow row in dsCats.Tables[0].Rows)
            {
                response.ProductSubCategoryListDTO.ProductSubCategoryList.Add(new KeyValuePair<string, string>(row["PAISC_AssetInstrumentSubCategoryCode"].ToString(), row["PAISC_AssetInstrumentSubCategoryName"].ToString()));
            }
            return response;
        }
        #endregion
    }
}
