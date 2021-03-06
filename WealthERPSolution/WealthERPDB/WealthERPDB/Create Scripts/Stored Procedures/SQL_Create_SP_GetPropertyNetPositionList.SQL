/****** Object:  StoredProcedure [dbo].[SP_GetPropertyNetPositionList]    Script Date: 06/24/2009 12:04:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetPropertyNetPositionList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetPropertyNetPositionList]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPropertyNetPositionList]    Script Date: 06/24/2009 12:04:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetPropertyNetPositionList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetPropertyNetPositionList]
(
@CP_PortfolioId INT,
@CurrentPage INT =1,
@SortOrder VARCHAR(20)=''SP_GetPropertyNetPositionList ASC''
)
AS
BEGIN
DECLARE @intStartRow int; 
DECLARE @intEndRow int;
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;
WITH Entries AS
(
SELECT 
	CPNP.*,
	PSC.PAISC_AssetInstrumentSubCategoryName,
		ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = ''SubCategory DESC''
							THEN PSC.PAISC_AssetInstrumentSubCategoryName END DESC,
							CASE WHEN @SortOrder = ''SubCategory ASC''
							THEN PSC.PAISC_AssetInstrumentSubCategoryName END ASC,
							CASE WHEN @SortOrder = ''Particulars DESC''
							THEN CPNP_Name END DESC,
							CASE WHEN @SortOrder = ''Particulars ASC''
							THEN CPNP_Name END ASC,
							CASE WHEN @SortOrder = ''City DESC''
							THEN CPNP_PropertyCity END DESC,
							CASE WHEN @SortOrder = ''City ASC''
							THEN CPNP_PropertyCity END ASC,
							CASE WHEN @SortOrder = ''PurchaseDate DESC''
							THEN CPNP_PurchaseDate END DESC,
							CASE WHEN @SortOrder = ''PurchaseDate ASC''
							THEN CPNP_PurchaseDate END ASC,
							CASE WHEN @SortOrder = ''PurchaseCost DESC''
							THEN CPNP_PurchasePrice END DESC,
							CASE WHEN @SortOrder = ''PurchaseCost ASC''
							THEN CPNP_PurchasePrice END ASC,
							CASE WHEN @SortOrder = ''CurrentValue DESC''
							THEN CPNP_CurrentValue END DESC,
							CASE WHEN @SortOrder = ''CurrentValue ASC''
							THEN CPNP_CurrentValue END ASC
						) as RowNum 
		
	
	FROM dbo.CustomerPropertyNetPosition AS CPNP
	INNER JOIN dbo.CustomerPropertyAccount AS CPA
	ON CPNP.CPA_AccountId = CPA.CPA_AccountId
	INNER JOIN ProductAssetInstrumentSubCategory AS PSC
	ON CPA.PAISC_AssetInstrumentSubCategoryCode = PSC.PAISC_AssetInstrumentSubCategoryCode AND CPA.PAIC_AssetInstrumentCategoryCode = PSC.PAIC_AssetInstrumentCategoryCode AND CPA.PAG_AssetGroupCode = PSC.PAG_AssetGroupCode
	WHERE CPA.CP_PortfolioId = @CP_PortfolioId
)

SELECT * FROM Entries where RowNum BETWEEN @intStartRow AND @intEndRow

SELECT COUNT(*) FROM dbo.CustomerPropertyNetPosition AS CPNP
INNER JOIN dbo.CustomerPropertyAccount AS CPA
ON CPNP.CPA_AccountId = CPA.CPA_AccountId
INNER JOIN ProductAssetInstrumentSubCategory AS PSC
ON CPA.PAISC_AssetInstrumentSubCategoryCode = PSC.PAISC_AssetInstrumentSubCategoryCode AND CPA.PAIC_AssetInstrumentCategoryCode = PSC.PAIC_AssetInstrumentCategoryCode AND CPA.PAG_AssetGroupCode = PSC.PAG_AssetGroupCode
WHERE CPA.CP_PortfolioId = @CP_PortfolioId

END
' 
END
GO
