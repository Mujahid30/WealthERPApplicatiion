/****** Object:  StoredProcedure [dbo].[SP_AddMutualFundNetPosition]    Script Date: 06/24/2009 10:58:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AddMutualFundNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_AddMutualFundNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddMutualFundNetPosition]    Script Date: 06/24/2009 10:58:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AddMutualFundNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'  
  
CREATE PROCEDURE [dbo].[SP_AddMutualFundNetPosition]  
   
   @CMFA_AccountId int  
           ,@PASP_SchemePlanCode int  
           ,@CMFNP_NetHoldings numeric(18,4)  
           ,@CMFNP_MarketPrice numeric(18,4)  
           ,@CMFNP_ValuationDate datetime  
           ,@CMFNP_SalesQuantity numeric(18,4)  
           ,@CMFNP_RealizedSaleProceeds numeric(18,4)  
           ,@CMFNP_AveragePrice numeric(18,4)  
           ,@CMFNP_RealizedPNL numeric(18,4)  
           ,@CMFNP_CostOfSales numeric(18,4)  
           ,@CMFNP_NetCost numeric(18,4)  
           ,@CMFNP_CurrentValue numeric(18,4)  
           ,@CMFNP_CreatedBy int    
           ,@CMFNP_ModifiedBy int  
     ,@CMFNP_MFNPId int OUTPUT  
as  
  
INSERT INTO [dbo].[CustomerMutualFundNetPosition]  
           ([CMFA_AccountId]  
           ,[PASP_SchemePlanCode]  
           ,[CMFNP_NetHoldings]  
           ,[CMFNP_MarketPrice]  
           ,[CMFNP_ValuationDate]  
           ,[CMFNP_SalesQuantity]  
           ,[CMFNP_RealizedSaleProceeds]  
           ,[CMFNP_AveragePrice]  
           ,[CMFNP_RealizedP/L]  
           ,[CMFNP_CostOfSales]  
           ,[CMFNP_NetCost]  
           ,[CMFNP_CurrentValue]  
           ,[CMFNP_CreatedBy]             
           ,[CMFNP_ModifiedBy]  
           ,[CMFNP_CreatedOn]  
           ,[CMFNP_ModifiedOn])  
     VALUES  
(@CMFA_AccountId  
,@PASP_SchemePlanCode  
,@CMFNP_NetHoldings  
,@CMFNP_MarketPrice  
,@CMFNP_ValuationDate  
,@CMFNP_SalesQuantity  
,@CMFNP_RealizedSaleProceeds  
,@CMFNP_AveragePrice  
,@CMFNP_RealizedPNL  
,@CMFNP_CostOfSales  
,@CMFNP_NetCost  
,@CMFNP_CurrentValue  
,@CMFNP_CreatedBy    
,@CMFNP_ModifiedBy   
,CURRENT_TIMESTAMP  
,CURRENT_TIMESTAMP)  
  
SELECT @CMFNP_MFNPId=SCOPE_IDENTITY()  
  
  ' 
END
GO
