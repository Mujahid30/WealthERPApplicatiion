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

        #endregion
    }
}
