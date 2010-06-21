-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerCashAndSavingsAccount]
@CCSA_AccountId INT
AS
SELECT CCSA.*,PAIC.PAIC_AssetInstrumentCategoryName FROM CustomerCashSavingsAccount  AS CCSA
INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC 
ON CCSA.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode AND CCSA.PAG_AssetGroupCode = PAIC.PAG_AssetGroupCode

WHERE @CCSA_AccountId=CCSA_AccountId 