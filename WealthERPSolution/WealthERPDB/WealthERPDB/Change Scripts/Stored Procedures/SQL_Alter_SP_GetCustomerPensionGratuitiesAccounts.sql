-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
ALTER PROCEDURE [dbo].[SP_GetCustomerPensionGratuitiesAccounts]  
@CP_PortfolioId INT,  
@PAG_AssetGroupCode varchar(5),  
@PAIC_AssetInstrumentCategoryCode varchar(5)  
  
AS  
  
SELECT * FROM dbo.CustomerPensionandGratuitiesAccount  CPGA

WHERE 
CPGA.CP_PortfolioId=@CP_PortfolioId AND CPGA.PAG_AssetGroupCode=@PAG_AssetGroupCode AND 
CPGA.PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode 