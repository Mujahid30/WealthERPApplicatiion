/****** Object:  StoredProcedure [dbo].[SP_GetProductMarketCapClassification]    Script Date: 06/24/2009 12:04:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductMarketCapClassification]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetProductMarketCapClassification]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductMarketCapClassification]    Script Date: 06/24/2009 12:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductMarketCapClassification]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE SP_GetProductMarketCapClassification
AS
BEGIN
	SET NOCOUNT ON;
	Select PMCC_MarketCapClassificationCode ,PMCC_CapClassification
	From ProductMarketCapClassification

END
' 
END
GO
