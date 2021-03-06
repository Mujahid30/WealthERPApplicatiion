/****** Object:  StoredProcedure [dbo].[SP_GetInsurancePortfolio]    Script Date: 06/24/2009 12:03:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInsurancePortfolio]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetInsurancePortfolio]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetInsurancePortfolio]    Script Date: 06/24/2009 12:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInsurancePortfolio]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetInsurancePortfolio]
(
	@CP_PortfolioId INT,
	@CurrentPage INT =1,
	@SortOrder VARCHAR(20) =''PAIC_AssetInstrumentCategoryName ASC''
)
AS

SET NOCOUNT ON

DECLARE @intStartRow int; 
DECLARE @intEndRow int;
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;

WITH Entries AS
(	
	SELECT 
		CIN.*,PAIC.PAIC_AssetInstrumentCategoryName,
		ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = ''Category DESC''
							THEN PAIC.PAIC_AssetInstrumentCategoryName END DESC,
							CASE WHEN @SortOrder = ''Category ASC''
							THEN PAIC.PAIC_AssetInstrumentCategoryName END ASC,
							CASE WHEN @SortOrder = ''Particulars DESC''
							THEN CINP_Name END DESC,
							CASE WHEN @SortOrder = ''Particulars ASC''
							THEN CINP_Name END ASC
							 ) as RowNum    
	FROM 
		CustomerInsuranceNetPosition AS CIN
		INNER JOIN
		CustomerInsuranceAccount AS CIA
		ON CIN.CIA_AccountId = CIA.CIA_AccountId
		INNER JOIN 
		dbo.ProductAssetInstrumentCategory AS PAIC
		ON CIA.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	WHERE
		CIA.CP_PortfolioId = @CP_PortfolioId
)
	
SELECT * FROM Entries WHERE  RowNum BETWEEN @intStartRow AND @intEndRow 	

SELECT COUNT(*) 	FROM 
		CustomerInsuranceNetPosition AS CIN
		INNER JOIN
		CustomerInsuranceAccount AS CIA
		ON CIN.CIA_AccountId = CIA.CIA_AccountId
		INNER JOIN 
		dbo.ProductAssetInstrumentCategory AS PAIC
		ON CIA.PAIC_AssetInstrumentCategoryCode = PAIC.PAIC_AssetInstrumentCategoryCode
	WHERE
		CIA.CP_PortfolioId = @CP_PortfolioId
	
SET NOCOUNT OFF
' 
END
GO
