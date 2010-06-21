
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_UpdateCollectibleNetPosition]
@CCNP_CollectibleNPId	INT,
@PAIC_AssetInstrumentCategoryCode varchar(5),
@CCNP_Name varchar(50),
@CCNP_PurchaseDate datetime,
@CCNP_PurchaseValue numeric(18, 3),
@CCNP_CurrentValue numeric(18, 3),
@CCNP_Remark varchar(100),
@CCNP_ModifiedBy int


AS
UPDATE CustomerCollectibleNetPosition SET 

PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode,
CCNP_Name=@CCNP_Name,
CCNP_PurchaseDate=@CCNP_PurchaseDate,
CCNP_PurchaseValue=@CCNP_PurchaseValue,
CCNP_CurrentValue=@CCNP_CurrentValue,
CCNP_Remark=@CCNP_Remark,
CCNP_ModifiedBy=@CCNP_ModifiedBy


WHERE CCNP_CollectibleNPId=@CCNP_CollectibleNPId

 