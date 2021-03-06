/****** Object:  StoredProcedure [dbo].[SP_GetProductEquityScripDetails]    Script Date: 06/24/2009 12:03:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductEquityScripDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetProductEquityScripDetails]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductEquityScripDetails]    Script Date: 06/24/2009 12:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductEquityScripDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetProductEquityScripDetails]
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
	dbo.FN_GetExchangeCode(@WERPCODE,''BSE'') as BSECODE,
	dbo.FN_GetExchangeCode(@WERPCODE,''NSE'') as NSECODE,
	dbo.FN_GetExchangeCode(@WERPCODE,''CERC'') as CERCCODE
	from ProductEquityMaster A 
	WHERE A.PEM_ScripCode = @WERPCODE
END
' 
END
GO
