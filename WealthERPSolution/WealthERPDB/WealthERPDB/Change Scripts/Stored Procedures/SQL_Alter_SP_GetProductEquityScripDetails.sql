ALTER PROCEDURE [dbo].[SP_GetProductEquityScripDetails]
(
@WERPCODE int 
)

AS 
 BEGIN
  SET NOCOUNT ON;
SELECT
	A.PEM_ScripCode as ScripCode,
	A.PEM_CompanyName as ScripName,
	A.PEM_Ticker as Ticker,
	A.PEM_Incorporation as IncorporationDate,
	A.PEM_PublicIssueDate as PublicIssueDate,
	A.PEM_MarketLot as MarketLot,
	A.PEM_FaceValue as FaceValue,
	A.PEM_BookClosure as BookClosure,
	A.PAIC_AssetInstrumentCategoryCode as AssetInstrumentCategoryCode ,
	A.PAISC_AssetInstrumentSubCategoryCode as AssetInstrumentSubCategoryCode ,
--	A.PSC_SectorId as SectorId,
--	A.PMCC_MarketCapClassificationCode as MarketCap,
	dbo.FN_GetExchangeCode(@WERPCODE,'BSE') as BSECODE,
	dbo.FN_GetExchangeCode(@WERPCODE,'NSE') as NSECODE,
	dbo.FN_GetExchangeCode(@WERPCODE,'CERC') as CERCCODE
	from ProductEquityMaster A 
	WHERE A.PEM_ScripCode = @WERPCODE
END
 