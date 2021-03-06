/****** Object:  StoredProcedure [dbo].[SP_CreatePersonalNetPosition]    Script Date: 06/24/2009 11:13:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreatePersonalNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreatePersonalNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreatePersonalNetPosition]    Script Date: 06/24/2009 11:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreatePersonalNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreatePersonalNetPosition]
@CP_PortfolioId INT,
@PAISC_AssetInstrumentSubCategoryCode varchar(6),
@PAIC_AssetInstrumentCategoryCode varchar(4),
@PAG_AssetGroupCode varchar(2),
@CPNP_Name varchar(50),
@CPNP_PurchaseDate DATETIME,
@CPNP_Quantity numeric(5, 0),
@CPNP_PurchasePrice numeric(18, 3),
@CPNP_PurchaseValue numeric(18, 3),
@CPNP_CurrentPrice numeric(18, 3),
@CPNP_CurrentValue numeric(18, 3),
@CPNP_CreatedBy INT,
@CPNP_ModifiedBy INT 

AS
 INSERT INTO dbo.CustomerPersonalNetPosition (
 	CP_PortfolioId,
 	PAISC_AssetInstrumentSubCategoryCode,
 	PAIC_AssetInstrumentCategoryCode,
 	PAG_AssetGroupCode,
 	CPNP_Name,
 	CPNP_PurchaseDate,
 	CPNP_Quantity,
 	CPNP_PurchasePrice,
 	CPNP_PurchaseValue,
 	CPNP_CurrentPrice,
 	CPNP_CurrentValue,
 	CPNP_CreatedBy,
 	CPNP_CreatedOn,
 	CPNP_ModifiedBy,
 	CPNP_ModifiedOn
 )
 VALUES ( 
	@CP_PortfolioId,
 	@PAISC_AssetInstrumentSubCategoryCode,
 	@PAIC_AssetInstrumentCategoryCode,
 	@PAG_AssetGroupCode,
 	@CPNP_Name,
 	@CPNP_PurchaseDate,
 	@CPNP_Quantity,
 	@CPNP_PurchasePrice,
 	@CPNP_PurchaseValue,
 	@CPNP_CurrentPrice,
 	@CPNP_CurrentValue,
 	@CPNP_CreatedBy,
 	CURRENT_TIMESTAMP,
 	@CPNP_ModifiedBy,
 	CURRENT_TIMESTAMP
 
 	) 

' 
END
GO
