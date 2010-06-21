ALTER PROCEDURE [dbo].[SP_GetProductAMCSchemePlanDetails]
(
@WERPCODE int 
)

AS 
 BEGIN
  SET NOCOUNT ON;
SELECT
	A.PASP_SchemePlanCode as SchemePlanCode ,
	A.PASP_SchemePlanName as SchemePlanName,
	A.PAIC_AssetInstrumentCategoryCode as InstrumentCategoryCode,
	A.PAISC_AssetInstrumentSubCategoryCode as InstrumentSubCategoryCode,
	A.PAISSC_AssetInstrumentSubSubCategoryCode as InstrumentSubSubCategoryCode,
--	A.PSC_SectorId as SectorID,
--	A.PMCC_MarketCapClassificationCode as MarketCap,
	dbo.FN_GetSchemeCode(@WERPCODE,'AMFI') as AMFICODE,
	dbo.FN_GetSchemeCode(@WERPCODE,'CAMS') as CAMSCODE,
	dbo.FN_GetSchemeCode(@WERPCODE,'KARVY') as KARVYCODE
	from ProductAMCSchemePlan A 
	WHERE A.PASP_SchemePlanCode = @WERPCODE
END
 