using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WealthERP.ServiceRequestResponse;
using WealthERP.BusinessEntities;
using WealthERP.ServiceContracts;
using System.Collections.Specialized;
using System.Data;
using BoCommon;

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
        private CommonLookupBo BoCommonLookup;

        public WERPCommonLookupServiceFacade()
        {
            BoCommonLookup = new CommonLookupBo();
        }

        #region IWerpCommonLookupContract Members

         //<summary>
         //  Gets the personal settings.
         //</summary>
         //<param name="personalSettingsRequest"> The personal settings request. </param>
         //<returns> Personal Settings Response. </returns>

     

        #endregion

        #region IWerpCommonLookupContract Members

        //<summary>
        //  Gets the list of Products
        //</summary>
        //<param name="ProductRequest"> The personal settings request. </param>
        //<returns> ProductAmcLookupResponse </returns>
        ProductLookupResponse IWerpCommonLookupContract.GetProductList(ProductLookupRequest ProductRequest)
        {
            ProductLookupResponse response = new ProductLookupResponse();

            try
            {
                DataTable dtProductList;

                if (string.IsNullOrEmpty(ProductRequest.ProductCode))  { 
                    dtProductList = BoCommonLookup.GetProductList(); 
                }
                else { 
                    dtProductList = BoCommonLookup.GetProductList(ProductRequest.ProductCode.Trim()); 
                }

                foreach (DataRow row in dtProductList.Rows) {
                    response.ProductListResponse.ProductList.Add(new KeyValuePair<string, string>(row["PAG_AssetGroupCode"].ToString(), row["PAG_AssetGroupName"].ToString()));
                }
                response.SetServiceResult(true, WerpErrorDto.E_SUCCESS, null);
            }
            catch (FormatException ex)
            {
                response.SetServiceResult(false, WerpErrorDto.E_INVALID_INPUT, ex.Message);
            }
            catch (DataException ex)
            {
                response.SetServiceResult(false, WerpErrorDto.E_DATABASE, ex.Message);
            }
            catch (Exception ex)
            {
                response.SetServiceResult(false, WerpErrorDto.E_GENERIC, ex.Message);
            }
            finally
            {
            }
            return response;
        }

        //<summary>
        //  Gets the list of Product AMC
        //</summary>
        //<param name="pAmcRequest"> The personal settings request. </param>
        //<returns> ProductAmcLookupResponse </returns>
        ProductAmcLookupResponse IWerpCommonLookupContract.GetProductAmcList(ProductAmcLookupRequest pAmcRequest)
        {
            ProductAmcLookupResponse response = new ProductAmcLookupResponse();

            try
            {
                DataTable dtAmcData;

                if (string.IsNullOrEmpty(pAmcRequest.AmcCode)) { dtAmcData = BoCommonLookup.GetProdAmc(); }
                else { dtAmcData = BoCommonLookup.GetProdAmc(int.Parse(pAmcRequest.AmcCode)); }

                foreach (DataRow row in dtAmcData.Rows)
                {
                    response.ProductAmcListResponse.ProductAMCList.Add(new KeyValuePair<string, string>(row["PA_AMCCode"].ToString(), row["PA_AMCName"].ToString()));
                }
                response.SetServiceResult(true, WerpErrorDto.E_SUCCESS, null);
            }
            catch (FormatException ex)
            {
                response.SetServiceResult(false, WerpErrorDto.E_INVALID_INPUT, ex.Message);
            }
            catch (DataException ex)
            {
                response.SetServiceResult(false, WerpErrorDto.E_DATABASE, ex.Message);
            }
            catch (Exception ex)
            {
                response.SetServiceResult(false, WerpErrorDto.E_GENERIC, ex.Message);
            }
            finally
            {
            }
            return response;
        }

        //<summary>
        //Gets the list of Product Category
        //</summary>
        //<param name="LookupRequest"> The personal settings request. </param>
        //<returns> ProductAmcLookupResponse </returns>
        ProductCategoryLookupResponse IWerpCommonLookupContract.GetProductCategoryList(ProductCategoryLookupRequest LookupRequest)
        {
            ProductCategoryLookupResponse response = new ProductCategoryLookupResponse();

            try
            {
                DataTable dt;

                string ProductCode = string.IsNullOrEmpty(LookupRequest.ProductCode) ? null : LookupRequest.ProductCode.Trim();
                string CategoryCode = string.IsNullOrEmpty(LookupRequest.CategoryCode) ? null : LookupRequest.CategoryCode.Trim();

                dt = BoCommonLookup.GetCategoryList(ProductCode, CategoryCode);

                foreach (DataRow row in dt.Rows) {
                    response.ProductCategoryList.ProductCategoryList.Add(new KeyValuePair<string, string>(row["PAIC_AssetInstrumentCategoryCode"].ToString(), row["PAIC_AssetInstrumentCategoryName"].ToString()));
                }
                response.SetServiceResult(true, WerpErrorDto.E_SUCCESS, null);
            }
            catch (FormatException ex)
            {
                response.SetServiceResult(false, WerpErrorDto.E_INVALID_INPUT, ex.Message);
            }
            catch (DataException ex)
            {
                response.SetServiceResult(false, WerpErrorDto.E_DATABASE, ex.Message);
            }
            catch (Exception ex)
            {
                response.SetServiceResult(false, WerpErrorDto.E_GENERIC, ex.Message);
            }
            finally
            {
            }
            return response;
        }
        #endregion
    }
}
