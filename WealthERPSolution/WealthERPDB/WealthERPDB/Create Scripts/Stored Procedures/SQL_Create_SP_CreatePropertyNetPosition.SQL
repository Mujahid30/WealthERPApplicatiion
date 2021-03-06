/****** Object:  StoredProcedure [dbo].[SP_CreatePropertyNetPosition]    Script Date: 06/24/2009 11:13:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreatePropertyNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreatePropertyNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreatePropertyNetPosition]    Script Date: 06/24/2009 11:13:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreatePropertyNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_CreatePropertyNetPosition] 
@CPA_AccountId INT,
@PAISC_AssetInstrumentSubCategoryCode VARCHAR(6),
@PAIC_AssetInstrumentCategoryCode VARCHAR(4),
@PAG_AssetGroupCode VARCHAR(2),
@XMC_MeasureCode VARCHAR(5),
@CPNP_Name VARCHAR(50),
@CPNP_PropertyAdrLine1 varchar(30),
@CPNP_PropertyAdrLine2 varchar(30),
@CPNP_PropertyAdrLine3 varchar(30),
@CPNP_PropertyCity varchar(30),
@CPNP_PropertyState varchar(30),
@CPNP_PropertyCountry varchar(30),
@CPNP_PropertyPinCode numeric(6, 0),
@CPNP_PurchaseDate DATETIME,
@CPNP_PurchasePrice numeric(18, 3),
@CPNP_Quantity numeric(18, 5),
@CPNP_CurrentPrice numeric(18, 3),
@CPNP_CurrentValue numeric(18, 3),
@CPNP_PurchaseValue numeric(18, 3),
@CPNP_SellDate DATETIME,
@CPNP_SellPrice numeric(18, 3),
@CPNP_SellValue numeric(18, 3),
@CPNP_Remark VARCHAR(100),
@CPNP_CreatedBy INT,
@CPNP_ModifiedBy INT

AS

INSERT INTO dbo.CustomerPropertyNetPosition (
	CPA_AccountId,
	PAISC_AssetInstrumentSubCategoryCode,
	PAIC_AssetInstrumentCategoryCode,
	PAG_AssetGroupCode,
	XMC_MeasureCode,
	CPNP_Name,
	CPNP_PropertyAdrLine1,
	CPNP_PropertyAdrLine2,
	CPNP_PropertyAdrLine3,
	CPNP_PropertyCity,
	CPNP_PropertyState,
	CPNP_PropertyCountry,
	CPNP_PropertyPinCode,
	CPNP_PurchaseDate,
	CPNP_PurchasePrice,
	CPNP_Quantity,
	CPNP_CurrentPrice,
	CPNP_CurrentValue,
	CPNP_PurchaseValue,
	CPNP_SellDate,
	CPNP_SellPrice,
	CPNP_SellValue,
	CPNP_Remark,
	CPNP_CreatedBy,
	CPNP_CreatedOn,
	CPNP_ModifiedOn,
	CPNP_ModifiedBy
) 
VALUES ( 
	@CPA_AccountId,
	@PAISC_AssetInstrumentSubCategoryCode,
	@PAIC_AssetInstrumentCategoryCode,
	@PAG_AssetGroupCode,
	@XMC_MeasureCode,
	@CPNP_Name,
	@CPNP_PropertyAdrLine1,
	@CPNP_PropertyAdrLine2,
	@CPNP_PropertyAdrLine3,
	@CPNP_PropertyCity,
	@CPNP_PropertyState,
	@CPNP_PropertyCountry,
	@CPNP_PropertyPinCode,
	@CPNP_PurchaseDate,
	@CPNP_PurchasePrice,
	@CPNP_Quantity,
	@CPNP_CurrentPrice,
	@CPNP_CurrentValue,
	@CPNP_PurchaseValue,
	@CPNP_SellDate,
	@CPNP_SellPrice,
	@CPNP_SellValue,
	@CPNP_Remark,
	@CPNP_CreatedBy,
	CURRENT_TIMESTAMP,
	CURRENT_TIMESTAMP,
	@CPNP_ModifiedBy
	) ' 
END
GO
