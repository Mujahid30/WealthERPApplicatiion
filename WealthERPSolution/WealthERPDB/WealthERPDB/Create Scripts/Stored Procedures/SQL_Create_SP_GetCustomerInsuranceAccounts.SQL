/****** Object:  StoredProcedure [dbo].[SP_GetCustomerInsuranceAccounts]    Script Date: 06/24/2009 12:00:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerInsuranceAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerInsuranceAccounts]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerInsuranceAccounts]    Script Date: 06/24/2009 12:00:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerInsuranceAccounts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerInsuranceAccounts]
@CP_PortfolioId INT,
@PAG_AssetGroupCode	varchar(2)
AS

BEGIN
	
	SELECT 
		CIA.*,
		PC.PAIC_AssetInstrumentCategoryName
	FROM 
		dbo.CustomerInsuranceAccount AS CIA
		INNER JOIN
		dbo.ProductAssetInstrumentCategory AS PC
		ON CIA.PAIC_AssetInstrumentCategoryCode = PC.PAIC_AssetInstrumentCategoryCode AND CIA.PAG_AssetGroupCode = PC.PAG_AssetGroupCode
		
	WHERE 
		CIA.CP_PortfolioId = @CP_PortfolioId 
		AND 
		CIA.PAG_AssetGroupCode = @PAG_AssetGroupCode	
	
END

' 
END
GO
