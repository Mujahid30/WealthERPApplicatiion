
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateGoldNetPosition]

@CGNP_GoldNPId	int,
@PAIC_AssetInstrumentCategoryCode	varchar(4),
@PAG_AssetGroupCode	varchar(2),
@CP_PortfolioId	int,
@XMC_MeasureCode	varchar(5),
@CGNP_Name	varchar(50),
@CGNP_PurchaseDate	datetime,
@CGNP_PurchasePrice	numeric(18, 4),
@CGNP_Quantity	numeric(10, 4),
@CGNP_PurchaseValue	numeric(18, 4),
@CGNP_CurrentPrice	numeric(18, 4),
@CGNP_CurrentValue	numeric(18, 4),
@CGNP_SellDate	datetime,
@CGNP_SellPrice	numeric(18, 4),
@CGNP_SellValue	numeric(18, 4),
@CGNP_Remark	varchar(100),
@CGNP_ModifiedBy	int

as

update CustomerGoldNetPosition set


PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode,
PAG_AssetGroupCode=@PAG_AssetGroupCode,
CP_PortfolioId=@CP_PortfolioId,
XMC_MeasureCode=@XMC_MeasureCode,
CGNP_Name=@CGNP_Name,
CGNP_PurchaseDate=@CGNP_PurchaseDate,
CGNP_PurchasePrice=@CGNP_PurchasePrice,
CGNP_Quantity=@CGNP_Quantity,
CGNP_PurchaseValue=@CGNP_PurchaseValue,
CGNP_CurrentPrice=@CGNP_CurrentPrice,
CGNP_CurrentValue=@CGNP_CurrentValue,
CGNP_SellDate=@CGNP_SellDate,
CGNP_SellPrice=@CGNP_SellPrice,
CGNP_SellValue=@CGNP_SellValue,
CGNP_Remark=@CGNP_Remark,
CGNP_ModifiedBy=@CGNP_ModifiedBy,
CGNP_ModifiedOn=current_timestamp 

	
where CGNP_GoldNPId=@CGNP_GoldNPId
 