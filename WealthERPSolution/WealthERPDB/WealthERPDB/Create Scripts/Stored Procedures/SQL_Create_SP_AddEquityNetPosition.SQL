/****** Object:  StoredProcedure [dbo].[SP_AddEquityNetPosition]    Script Date: 06/24/2009 10:58:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AddEquityNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_AddEquityNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddEquityNetPosition]    Script Date: 06/24/2009 10:58:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AddEquityNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'  
  
CREATE PROCEDURE [dbo].[SP_AddEquityNetPosition]  
   
   @PEM_ScripCode int  
           ,@CETA_AccountId int  
           ,@CENP_ValuationDate datetime  
           ,@CENP_NetHoldings numeric(18,4)  
           ,@CENP_AveragePrice numeric(18,4)  
           ,@CENP_MarketPrice numeric(18,4)  
           ,@CENP_RealizedPNL numeric(18,4)  
           ,@CENP_CostOfSales numeric(18,4)  
           ,@CENP_NetCost numeric(18,4)  
           ,@CENP_SpeculativeSaleQuantity numeric(18,4)  
           ,@CENP_DeliverySaleQuantity numeric(18,4)  
           ,@CENP_SaleQuantity numeric(18,4)  
           ,@CENP_RealizedPNLForSpeculative numeric(18,4)  
           ,@CENP_RealizedPNLForDelivery numeric(18,4)  
           ,@CENP_CostOfSalesForSpeculative numeric(18,4)  
           ,@CENP_CostofSalesforDelivery numeric(18,4)  
           ,@CENP_Deliverysaleproceeds numeric(18,4)  
           ,@CENP_Speculativesalesproceeds numeric(18,4)  
           ,@CENP_CurrentValue numeric(18,4)  
           ,@CENP_CreatedBy int             
           ,@CENP_ModifiedBy int             
     ,@CENP_EquityNPId int OUTPUT  
as  
  
INSERT INTO [dbo].[CustomerEquityNetPosition]  
           ([PEM_ScripCode]  
           ,[CETA_AccountId]  
           ,[CENP_ValuationDate]  
           ,[CENP_NetHoldings]  
           ,[CENP_AveragePrice]  
           ,[CENP_MarketPrice]  
           ,[CENP_RealizedP/L]  
           ,[CENP_CostOfSales]  
           ,[CENP_NetCost]  
           ,[CENP_SpeculativeSaleQuantity]  
           ,[CENP_DeliverySaleQuantity]  
           ,[CENP_SaleQuantity]  
           ,[CENP_RealizedP/LForSpeculative]  
           ,[CENP_RealizedP/LForDelivery]  
           ,[CENP_CostOfSalesForSpeculative]  
           ,[CENP_CostofSalesforDelivery]  
           ,[CENP_Deliverysaleproceeds]  
           ,[CENP_Speculativesalesproceeds]  
           ,[CENP_CurrentValue]  
           ,[CENP_CreatedBy]  
           ,[CENP_CreatedOn]  
           ,[CENP_ModifiedBy]  
           ,[CENP_ModifiedOn])  
     VALUES  
           (@PEM_ScripCode  
           ,@CETA_AccountId  
           ,@CENP_ValuationDate  
           ,@CENP_NetHoldings  
           ,@CENP_AveragePrice  
           ,@CENP_MarketPrice  
           ,@CENP_RealizedPNL  
           ,@CENP_CostOfSales  
           ,@CENP_NetCost  
           ,@CENP_SpeculativeSaleQuantity  
           ,@CENP_DeliverySaleQuantity  
           ,@CENP_SaleQuantity  
           ,@CENP_RealizedPNLForSpeculative  
           ,@CENP_RealizedPNLForDelivery  
           ,@CENP_CostOfSalesForSpeculative  
           ,@CENP_CostofSalesforDelivery  
           ,@CENP_Deliverysaleproceeds  
           ,@CENP_Speculativesalesproceeds  
           ,@CENP_CurrentValue  
           ,@CENP_CreatedBy       
           ,current_timestamp   
           ,@CENP_ModifiedBy    
           ,current_timestamp     
     )  
  
  
SELECT @CENP_EquityNPId=SCOPE_IDENTITY()' 
END
GO
