/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPropertyAccounts]    Script Date: 06/24/2009 12:01:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPropertyAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerPropertyAccounts]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPropertyAccounts]    Script Date: 06/24/2009 12:01:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPropertyAccounts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerPropertyAccounts]
@CP_PortfolioId INT,
@PAG_AssetGroupCode varchar(2),
@PAIC_AssetInstrumentCategoryCode varchar(4),
@PAISC_AssetInstrumentSubCategoryCode varchar(5)

AS

SELECT * FROM CustomerPropertyAccount WHERE CP_PortfolioId=@CP_PortfolioId AND PAG_AssetGroupCode=@PAG_AssetGroupCode AND PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode AND PAISC_AssetInstrumentSubCategoryCode=@PAISC_AssetInstrumentSubCategoryCode' 
END
GO
