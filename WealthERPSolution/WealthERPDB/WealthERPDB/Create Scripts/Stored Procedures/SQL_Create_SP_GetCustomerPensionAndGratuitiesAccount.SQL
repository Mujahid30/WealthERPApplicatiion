/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPensionAndGratuitiesAccount]    Script Date: 06/24/2009 12:00:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPensionAndGratuitiesAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerPensionAndGratuitiesAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPensionAndGratuitiesAccount]    Script Date: 06/24/2009 12:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPensionAndGratuitiesAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetCustomerPensionAndGratuitiesAccount]
@CPGA_AccountId INT
AS
SELECT 
	CPGA.*,
	PC.PAIC_AssetInstrumentCategoryName 
FROM 
	CustomerPensionandGratuitiesAccount AS CPGA
	INNER JOIN
	ProductAssetInstrumentCategory AS PC
	ON
	CPGA.PAIC_AssetInstrumentCategoryCode = PC.PAIC_AssetInstrumentCategoryCode AND CPGA.PAG_AssetGroupCode = PC.PAG_AssetGroupCode
WHERE 
	CPGA_AccountId = @CPGA_AccountId' 
END
GO
