-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerGovtSavingsAccount]

@CGSA_AccountId INT

AS

SELECT CGSA.*,PAIC.PAIC_AssetInstrumentCategoryName FROM dbo.CustomerGovtSavingAccount AS CGSA 
INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC 
ON CGSA.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
WHERE CGSA_AccountId=@CGSA_AccountId

  