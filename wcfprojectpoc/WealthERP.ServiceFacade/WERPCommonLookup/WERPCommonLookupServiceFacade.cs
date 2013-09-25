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
            ProductAMCListDTO prodAmcList = new ProductAMCListDTO();

            try
            {
                DataSet dsLookupData = boCommRecv.GetProdAmc();

                foreach (DataRow row in dsLookupData.Tables[0].Rows)
                {
                    KeyValuePair<string, string> prodAmc = new KeyValuePair<string, string>(row["PA_AMCCode"].ToString(), row["PA_AMCName"].ToString());
                    prodAmcList.ProductAMCList.Add(prodAmc);
                }
            }
            catch (Exception e)
            {
            }
            response.ServiceResult.IsSuccess = true;
            return response;
        }

        #endregion
    }
}
