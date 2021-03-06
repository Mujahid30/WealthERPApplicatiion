/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPensionGratuitiesAccounts]    Script Date: 06/24/2009 12:00:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPensionGratuitiesAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerPensionGratuitiesAccounts]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPensionGratuitiesAccounts]    Script Date: 06/24/2009 12:00:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerPensionGratuitiesAccounts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_GetCustomerPensionGratuitiesAccounts]  
@CP_PortfolioId INT,  
@PAG_AssetGroupCode varchar(5),  
@PAIC_AssetInstrumentCategoryCode varchar(5)  
  
AS  
  
SELECT * FROM dbo.CustomerPensionandGratuitiesAccount  CPGA

WHERE 
CPGA.CP_PortfolioId=@CP_PortfolioId AND CPGA.PAG_AssetGroupCode=@PAG_AssetGroupCode AND 
CPGA.PAIC_AssetInstrumentCategoryCode=@PAIC_AssetInstrumentCategoryCode' 
END
GO
