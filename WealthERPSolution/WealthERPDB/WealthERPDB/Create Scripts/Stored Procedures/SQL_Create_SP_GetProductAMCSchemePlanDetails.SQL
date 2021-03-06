/****** Object:  StoredProcedure [dbo].[SP_GetProductAMCSchemePlanDetails]    Script Date: 06/24/2009 12:03:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductAMCSchemePlanDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetProductAMCSchemePlanDetails]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductAMCSchemePlanDetails]    Script Date: 06/24/2009 12:03:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductAMCSchemePlanDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetProductAMCSchemePlanDetails]
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
	dbo.FN_GetSchemeCode(@WERPCODE,''AMFI'') as AMFICODE,
	dbo.FN_GetSchemeCode(@WERPCODE,''CAMS'') as CAMSCODE,
	dbo.FN_GetSchemeCode(@WERPCODE,''KARVY'') as KARVYCODE
	from ProductAMCSchemePlan A 
	WHERE A.PASP_SchemePlanCode = @WERPCODE
END
' 
END
GO
