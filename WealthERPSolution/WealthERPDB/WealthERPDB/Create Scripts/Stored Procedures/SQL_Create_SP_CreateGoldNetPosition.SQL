/****** Object:  StoredProcedure [dbo].[SP_CreateGoldNetPosition]    Script Date: 06/24/2009 11:13:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateGoldNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateGoldNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateGoldNetPosition]    Script Date: 06/24/2009 11:13:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateGoldNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateGoldNetPosition]
@CP_PortfolioId INT,
@PAIC_AssetInstrumentCategoryCode varchar(4),
@PAG_AssetGroupCode varchar(5),
@XMC_MeasureCode char(5),
@CGNP_Name varchar(50),
@CGNP_PurchaseDate DATETIME,
@CGNP_PurchasePrice numeric(18, 4),
@CGNP_Quantity numeric(10, 4),
@CGNP_PurchaseValue numeric(18, 4),
@CGNP_CurrentPrice numeric(18, 4),
@CGNP_CurrentValue numeric(18, 4),
@CGNP_SellDate DATETIME,
@CGNP_SellPrice NUMERIC(18,4),
@CGNP_SellValue NUMERIC(18,4),
@CGNP_Remark VARCHAR(100),
@CGNP_CreatedBy INT,
@CGNP_ModifiedBy INT
AS

INSERT INTO CustomerGoldNetPosition 
( 
CP_PortfolioId,
PAIC_AssetInstrumentCategoryCode,
PAG_AssetGroupCode,
XMC_MeasureCode,
CGNP_Name,
CGNP_PurchaseDate ,
CGNP_PurchasePrice,
CGNP_Quantity,
CGNP_PurchaseValue,
CGNP_CurrentPrice,
CGNP_CurrentValue,
CGNP_SellDate,
CGNP_SellPrice,
CGNP_SellValue,
CGNP_Remark,
CGNP_CreatedBy,
CGNP_CreatedOn,
CGNP_ModifiedOn,
CGNP_ModifiedBy
)

VALUES
( 
@CP_PortfolioId,
@PAIC_AssetInstrumentCategoryCode,
@PAG_AssetGroupCode,
@XMC_MeasureCode,
@CGNP_Name,
@CGNP_PurchaseDate ,
@CGNP_PurchasePrice,
@CGNP_Quantity,
@CGNP_PurchaseValue,
@CGNP_CurrentPrice,
@CGNP_CurrentValue,
@CGNP_SellDate,
@CGNP_SellPrice,
@CGNP_SellValue,
@CGNP_Remark,
@CGNP_CreatedBy,
CURRENT_TIMESTAMP,
CURRENT_TIMESTAMP,
@CGNP_ModifiedBy
)

' 
END
GO
