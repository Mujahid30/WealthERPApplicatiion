/****** Object:  StoredProcedure [dbo].[SP_CreateCollectiblesNetPosition]    Script Date: 06/24/2009 11:03:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCollectiblesNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateCollectiblesNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCollectiblesNetPosition]    Script Date: 06/24/2009 11:03:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCollectiblesNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateCollectiblesNetPosition]
	@PAIC_AssetInstrumentCategoryCode char(5),
	@PAG_AssetGroupCode char(5),
	@CP_PortfolioId INT,	
	@CCNP_Name varchar(50),
	@CCNP_PurchaseDate datetime,
	@CCNP_PurchaseValue numeric(18, 3),
	@CCNP_CurrentValue numeric(18, 3),
	@CCNP_Remark VARCHAR(100),
	@CCNP_CreatedBy INT,
	@CCNP_ModifiedBy INT
	
AS

INSERT INTO dbo.CustomerCollectibleNetPosition (
	PAIC_AssetInstrumentCategoryCode,
	PAG_AssetGroupCode,
	CP_PortfolioId,
	CCNP_Name,
	CCNP_PurchaseDate,
	CCNP_PurchaseValue,
	CCNP_CurrentValue,
	CCNP_Remark,
	CCNP_CreatedBy,
	CCNP_CreatedOn,
	CCNP_ModifiedBy,
	CCNP_ModifiedOn
) VALUES ( 
	@PAIC_AssetInstrumentCategoryCode,
	@PAG_AssetGroupCode,
	@CP_PortfolioId,
	@CCNP_Name,
	@CCNP_PurchaseDate,
	@CCNP_PurchaseValue,
	@CCNP_CurrentValue,
	@CCNP_Remark,
	@CCNP_CreatedBy,
	CURRENT_TIMESTAMP,
	@CCNP_ModifiedBy,
	CURRENT_TIMESTAMP ) ' 
END
GO
