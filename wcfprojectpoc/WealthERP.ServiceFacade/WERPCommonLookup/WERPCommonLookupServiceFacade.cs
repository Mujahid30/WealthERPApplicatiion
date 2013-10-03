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

        ProductAmcLookupResponse IWerpCommonLookupContract.GetProductAmcList(ProductAmcLookupRequest request)
        {
            ProductAmcLookupResponse response = new ProductAmcLookupResponse();
            CommisionReceivableBo boCommRecv = new CommisionReceivableBo();

            try
            {
                DataSet dsLookupData;

                if (string.IsNullOrEmpty(request.AmcCode)) { dsLookupData = boCommRecv.GetProdAmc(); }
                else { dsLookupData = boCommRecv.GetProdAmc(int.Parse(request.AmcCode)); }

                foreach (DataRow row in dsLookupData.Tables[0].Rows) {
                    response.ProductAmcListResponse.ProductAMCList.Add(new KeyValuePair<string, string>(row["PA_AMCCode"].ToString(), row["PA_AMCName"].ToString()));
                }
                response.ServiceResult.IsSuccess = true;
            }
            catch (DataException ex)
            {
                response.ServiceResult.IsSuccess = false;
                response.ServiceResult.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.ServiceResult.IsSuccess = false;
                response.ServiceResult.Message = ex.Message;
            }
            finally
            {
            }
            return response;
        }
        #endregion
    }
}
