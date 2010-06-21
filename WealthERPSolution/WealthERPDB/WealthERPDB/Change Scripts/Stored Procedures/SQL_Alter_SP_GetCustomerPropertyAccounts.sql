-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerPropertyAccounts]
@CP_PortfolioId INT,
@PAG_AssetGroupCode varchar(2),
@PAIC_AssetInstrumentCategoryCode varchar(4),
@PAISC_AssetInstrumentSubCategoryCode varchar(5)

AS

SELECT * FROM CustomerPropertyAccount WHERE CP_PortfolioId=@CP_PortfolioId AND PAG_AssetGroupCode=@PAG_AssetGroupCode AND PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode AND PAISC_AssetInstrumentSubCategoryCode=@PAISC_AssetInstrumentSubCategoryCode 