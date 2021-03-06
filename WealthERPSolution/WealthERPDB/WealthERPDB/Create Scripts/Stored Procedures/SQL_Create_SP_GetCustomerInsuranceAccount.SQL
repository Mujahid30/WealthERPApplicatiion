/****** Object:  StoredProcedure [dbo].[SP_GetCustomerInsuranceAccount]    Script Date: 06/24/2009 12:00:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerInsuranceAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerInsuranceAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerInsuranceAccount]    Script Date: 06/24/2009 12:00:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerInsuranceAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetCustomerInsuranceAccount]
@CIA_AccountId INT

AS
BEGIN
	SELECT 
		CIA.*,
		PIC.PAIC_AssetInstrumentCategoryName
	FROM 
		CustomerInsuranceAccount AS CIA
		INNER JOIN
		dbo.ProductAssetInstrumentCategory AS PIC
		ON CIA.PAIC_AssetInstrumentCategoryCode = PIC.PAIC_AssetInstrumentCategoryCode AND CIA.PAG_AssetGroupCode = PIC.PAG_AssetGroupCode
	WHERE 
		@CIA_AccountId = CIA_AccountId	
END
' 
END
GO
