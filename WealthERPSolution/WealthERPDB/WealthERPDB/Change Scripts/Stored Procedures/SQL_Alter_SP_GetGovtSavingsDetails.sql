
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetGovtSavingsDetails]

@CGSNP_GovtSavingNPId INT

AS

SELECT CGSNP.*,PAIC.PAIC_AssetInstrumentCategoryName FROM  dbo.CustomerGovtSavingNetPosition AS CGSNP
INNER JOIN dbo.ProductAssetInstrumentCategory AS PAIC
ON CGSNP.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
WHERE CGSNP_GovtSavingNPId=@CGSNP_GovtSavingNPId
 